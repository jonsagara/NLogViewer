namespace NLogViewer.Models
{
    public static class LogDatabase
    {
        /// <summary>
        /// We prefix the connection strings in web.config just in case there are other connection strings
        /// that are not pointing to log databases.
        /// </summary>
        public const string ConnectionStringPrefix = "log-database-";
    }
}