using Dapper;
using RC.Dapper.Api.Core;
using RC.Dapper.Api.Core.Interface;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RC.Dapper.Api.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private StringBuilder query;
        private readonly string tableName;
        private readonly ConfigurationApplication configurationApplication;

        public BaseRepository(string tableName, ConfigurationApplication configurationApplication)
        {
            this.tableName = tableName;
            this.configurationApplication = configurationApplication;
        }

        public async Task<T> GetAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT {GenerateSelectQuery()} FROM {tableName} WITH (NOLOCK) WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{tableName} with id [{id}] could not be found.");

                return result;
            }
        }

        public async Task InsertAsync(T entity)
        {
            var insertQuery = GenerateInsertQuery();

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, entity);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var updateQuery = GenerateUpdateQuery();

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, entity);
            }
        }

        public async Task DeleteRowAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id=@Id", new { Id = id });
            }
        }

        public async Task<int> SaveRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            using (var connection = CreateConnection())
            {
                inserted += await connection.ExecuteAsync(query, list);
            }

            return inserted;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageSize, int pageActual)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<T>($"SELECT {GenerateSelectQuery()} FROM {tableName} WITH (NOLOCK) ORDER BY Id OFFSET {pageActual - 1} ROWS FETCH NEXT {pageSize} ROWS ONLY");
                if (result == null)
                    throw new KeyNotFoundException($"{tableName} not be found.");

                return result;
            }
        }


        private string GenerateSelectQuery()
        {
            query = new StringBuilder();
            var properties = MethodExtension.GenerateListOfProperties(GetProperties);

            foreach (var property in properties)
            {
                query.Append($"[{property}],");
            }

            return query.Remove(query.Length - 1, 1).ToString();
        }

        private string GenerateInsertQuery()
        {
            query = new StringBuilder($"INSERT INTO {tableName} ").Append("(");

            var properties = MethodExtension.GenerateListOfProperties(GetProperties);

            foreach (var property in properties)
            {
                if (!property.Equals("Id"))
                    query.Append($"[{property}],");
            }

            query.Remove(query.Length - 1, 1).Append(") VALUES (");

            foreach (var property in properties)
            {
                if (!property.Equals("Id"))
                    query.Append($"@{property},");
            }

            return query.Remove(query.Length - 1, 1).Append(")").ToString();
        }

        private string GenerateUpdateQuery()
        {
            query = new StringBuilder($"UPDATE {tableName} SET ");
            var properties = MethodExtension.GenerateListOfProperties(GetProperties);

            foreach (var property in properties)
            {
                if (!property.Equals("Id"))
                    query.Append($"{property}=@{property},");
            }

            return query.Remove(query.Length - 1, 1).Append(" WHERE Id=@Id").ToString();
        }

        private SqlConnection SqlConnection()
        {
            return new SqlConnection(this.configurationApplication.ConnectionDeveloper);
        }

        private IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }

        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

    }
}