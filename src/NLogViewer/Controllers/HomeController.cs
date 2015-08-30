using Dapper;
using NLogViewer.Models;
using StackExchange.Profiling;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NLogViewer.Extensions;

namespace NLogViewer.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            var model = new HomeIndexModel();

            using (MiniProfiler.Current.Step("Getting logs from database"))
            using (var conn = CreateProfiledDbConnection())
            {
                model.Logs.AddRange(
                    await conn
                        .QueryAsync<Log>(DapperQueries.GetTop100Logs)
                    );
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
        public async Task<ActionResult> TruncateLogs()
        {
            using (MiniProfiler.Current.Step("Truncating logs in database"))
            using (var conn = CreateProfiledDbConnection())
            {
                await conn.ExecuteAsync(DapperQueries.TruncateLogs);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}