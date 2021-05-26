using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using oauth2testbed.Database.Tables;
using System;
using System.IO;

namespace oauth2testbed.Database
{
    public class DatabaseContext : DbContext
    {
        #region Properties

        /// <summary>
        /// Full path to storage file, after set.
        /// </summary>
        private static string storagePath { get; set; }

        /// <summary>
        /// Full path to storage file.
        /// </summary>
        public static string StoragePath =>
            storagePath ??= Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData,
                    Environment.SpecialFolderOption.Create),
                "oauth2testbed.storage.sqlite.db");

        #endregion

        #region Instance configuration

        /// <summary>
        /// Setup configuration for database context.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={StoragePath}");
        }

        #endregion

        #region DbSets

        /// <summary>
        /// Clients.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        #endregion

        #region Helper functions

        /// <summary>
        /// Create the needed database tables.
        /// </summary>
        public static void CreateTables()
        {
            // If the file already exist, skip creation.
            if (File.Exists(StoragePath))
            {
                return;
            }

            // Open the connection to the file and create the table, if it doesn't exist yet.
            using var connection = new SqliteConnection($"Data Source={StoragePath}");
            connection.Open();

            // Create the Clients table.
            using var command = connection.CreateCommand();
            command.CommandText = 
                "CREATE TABLE IF NOT EXISTS [Clients] (" +
                "  [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                "  [Identifier] NVARCHAR(64) NOT NULL," +
                "  [Created] NVARCHAR(32) NOT NULL," +
                "  [Flow] NVARCHAR(32) NOT NULL," +
                "  [ClientId] NVARCHAR(32) NOT NULL," +
                "  [ClientSecret] NVARCHAR(64) NOT NULL," +
                "  [Username] NVARCHAR(32) NOT NULL," +
                "  [Password] NVARCHAR(32) NOT NULL," +
                "  [Scope] NVARCHAR(64) NOT NULL," +
                "  [RedirectUrls] NVARCHAR(2048)" +
                ");";

            command.ExecuteNonQuery();
        }

        #endregion
    }
}