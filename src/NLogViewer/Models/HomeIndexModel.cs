using System.Collections.Generic;
using System.Web.Mvc;
using NLogViewer.Extensions;

namespace NLogViewer.Models
{
    public class HomeIndexModel
    {
        public List<Log> Logs { get; private set; }

        public string SelectedLogDatabase { get; set; }
        public List<SelectListItem> LogDatabases { get; private set; }

        public string SelectedLogDatabaseName => SelectedLogDatabase?.ToFriendlyLogDatabaseName();

        public HomeIndexModel()
        {
            Logs = new List<Log>();
            LogDatabases = new List<SelectListItem>();
        }
    }
}