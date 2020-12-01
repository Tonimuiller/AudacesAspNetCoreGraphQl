using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackendChallenge.Core.Model;
using BackendChallenge.Core.Repository;
using Dapper;

namespace BackendChallenge.Dal.Repository
{
    public class CheckTargetRepository : SqLiteRepository, ICheckTargetRepository
    {
        public async Task<IEnumerable<CheckTarget>> GetCheckTargetByPeriodAsync(DateTime start, DateTime end)
        {
            if (!File.Exists(this.DbFile))
            {
                return new List<CheckTarget>();
            }

            using(var connection = this.GetCheckTargetDbConnection())
            {
                return await connection.QueryAsync<CheckTarget>(SelectSql, 
                    new 
                    { 
                        start = start.ToString("yyyy-MM-dd"), 
                        end = end.ToString("yyyy-MM-dd") 
                    });
            }
        }
        public async Task SaveCheckTargetAsync(CheckTarget checkTarget)
        {
            if (!File.Exists(DbFile))
            {
                await this.CreateDatabaseAsync();
            }

            if (checkTarget == null)
            {
                throw new ArgumentNullException(nameof(checkTarget));
            }

            using (var connection = this.GetCheckTargetDbConnection())
            {
                checkTarget.Creation = DateTime.Now;
                await connection.ExecuteAsync(InsertSql, checkTarget);
            }
        }

        private static string SelectSql => @"
            Select 
                Id, 
                Creation, 
                Target, 
                SequenceSerialized 
            From 
                CheckTarget 
            Where
                date(Creation) BETWEEN @start AND @end
        ";

        private static string InsertSql => @"
            Insert Into
                CheckTarget (Creation, Target, SequenceSerialized)
            Values
                (@Creation, @Target, @SequenceSerialized)
        ";
    }
}
