using Dapper;
using NLogViewer.Extensions;
using NLogViewer.Models;
using StackExchange.Profiling;
using System.Linq;
using System.Web.Mvc;

namespace NLogViewer.Controllers
{
    public class LogController : BaseController
    {
        public ActionResult Detail(int id)
        {
            var model = new LogDetailModel { LogDatabaseName = GetSelectedConnectionStringName().ToFriendlyLogDatabaseName() };
            var profiler = MiniProfiler.Current;

            using (profiler.Step("Getting log details from database"))
            using (var sqlConn = CreateProfiledDbConnection())
            {
                model.Log = sqlConn
                    .Query<Log>(DapperQueries.GetLog, new { Id = id })
                    .SingleOrDefault();
            }

            if (model.Log == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }
    }
}