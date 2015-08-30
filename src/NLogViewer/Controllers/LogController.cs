using Dapper;
using NLogViewer.Extensions;
using NLogViewer.Models;
using StackExchange.Profiling;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NLogViewer.Controllers
{
    public class LogController : BaseController
    {
        public async Task<ActionResult> Detail(int id)
        {
            var model = new LogDetailModel { LogDatabaseName = GetSelectedConnectionStringName().ToFriendlyLogDatabaseName() };
            var profiler = MiniProfiler.Current;

            using (profiler.Step("Getting log details from database"))
            using (var conn = CreateProfiledDbConnection())
            {
                model.Log = (await conn.QueryAsync<Log>(DapperQueries.GetLog, new { Id = id }))
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