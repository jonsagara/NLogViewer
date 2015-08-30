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
                model.Logs.AddRange(sqlConn.Query<Log>(DapperQueries.HomeIndexTop100Logs));
            }

            model.SelectedLogDatabase = GetSelectedConnectionStringName();

            var dbConnStrings = LogDatabaseConnectionStrings
                .Select(css => new SelectListItem { Text = css.Name.ToFriendlyLogDatabaseName(), Value = css.Name });
            model.LogDatabases.AddRange(dbConnStrings);

            return View(model);
        }
    }
}