using System;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace BackendChallenge.Dal.Repository
{
    public abstract class SqLiteRepository
    {
        protected string DbFile => $"{Environment.CurrentDirectory}\\CheckTargetDb.sqlite";
        protected SQLiteConnection GetCheckTargetDbConnection() => new SQLiteConnection($"Data Source={DbFile}");
        protected async Task CreateDatabaseAsync()
        {
            using (var connection = GetCheckTargetDbConnection())
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"create table CheckTarget
                (
                    Id                    integer primary key AUTOINCREMENT,
                    Creation              datetime not null,
                    Target                integer not null,
                    SequenceSerialized    varchar(10000) not null                    
                )";
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
