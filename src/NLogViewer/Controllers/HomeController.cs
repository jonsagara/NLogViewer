using Dapper;
using NLogViewer.Models;
using StackExchange.Profiling;
using System.Linq;
using System.Web.Mvc;
using NLogViewer.Extensions;

namespace NLogViewer.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var model = new HomeIndexModel();
            var profiler = MiniProfiler.Current;

            using (profiler.Step("Getting logs from database"))
            using (var sqlConn = CreateProfiledDbConnection())
            {
                model.Logs.AddRange(sqlConn.Query<Log>(DapperQueries.GetTop100Logs));
            }

            model.SelectedLogDatabase = GetSelectedConnectionStringName();

            var dbConnStrings = LogDatabaseConnectionStrings
                .Select(css => new SelectListItem { Text = css.Name.ToFriendlyLogDatabaseName(), Value = css.Name });
            model.LogDatabases.AddRange(dbConnStrings);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HomeIndexModel model)
        {
            if (IsConnectionStringInWebConfig(model.SelectedLogDatabase))
            {
                SetConnectionStringCookie(model.SelectedLogDatabase);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TruncateLogs()
        {
            var profiler = MiniProfiler.Current;

            using (profiler.Step("Truncating logs in database"))
            using (var sqlConn = CreateProfiledDbConnection())
            {
                sqlConn.Execute(DapperQueries.TruncateLogs);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}