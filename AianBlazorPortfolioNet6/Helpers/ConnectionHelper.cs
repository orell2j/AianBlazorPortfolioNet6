using System;
using Npgsql;
using NpgsqlTypes; // if needed
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient; // Not required for PostgreSQL; included for clarity if needed

namespace AianBlazorPortfolioNet6.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            // Get the default connection string from configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            // Get the DATABASE_URL environment variable (used on Railway, Heroku, etc.)
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return string.IsNullOrEmpty(databaseUrl)
                ? connectionString
                : BuildConnectionString(databaseUrl);
        }

        // Build the connection string from the DATABASE_URL (for example, from Railway)
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            return builder.ToString();
        }
    }
}
