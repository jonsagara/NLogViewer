namespace NLogViewer.Models
{
    public static class DapperQueries
    {
        private const string LogTable = "Logs";

        public static readonly string HomeIndexTop100Logs = $@"SELECT TOP 100 * FROM {LogTable} ORDER BY Id DESC";
    }
}