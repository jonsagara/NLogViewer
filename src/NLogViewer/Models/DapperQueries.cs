namespace NLogViewer.Models
{
    public static class DapperQueries
    {
        private const string LogTable = "Logs";

        public static readonly string GetTop100Logs = $@"SELECT TOP 100 * FROM {LogTable} ORDER BY Id DESC";

        public static readonly string TruncateLogs = $@"TRUNCATE TABLE [{LogTable}]";

        public static readonly string GetLog = $@"SELECT * FROM {LogTable} WHERE Id = @Id";
    }
}