using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Dapper;

namespace Infrastructure.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DapperContext _context;
        private readonly string _tableName;

        public BaseRepository(DapperContext context)
        {
            _context = context;
            _tableName = typeof(T).Name.ToLower() + "s"; // Приводим к нижнему регистру
        }

        public async Task<int> Add(T entity)
        {
            using var connection = _context.CreateConnection();
            var properties = typeof(T).GetProperties().Where(p => p.Name.ToLower() != "id"); // Исключаем Id
            var columns = string.Join(", ", properties.Select(p => p.Name.ToLower()));
            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({values}) RETURNING id";
            var result = await connection.ExecuteScalarAsync<int>(sql, entity);
            return result ;
        }

        public async Task<T> Get(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = $"SELECT * FROM {_tableName} WHERE id = @Id";
            return await connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var sql = $"SELECT * FROM {_tableName}";
            return await connection.QueryAsync<T>(sql);
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = _context.CreateConnection();
            var sql = $"DELETE FROM {_tableName} WHERE id = @Id";
            var result = await connection.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }

        public async Task<bool> Update(T entity)
        {
            using var connection = _context.CreateConnection();
            var properties = typeof(T).GetProperties().Where(p => p.Name.ToLower() != "id");
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name.ToLower()} = @{p.Name}"));

            var sql = $"UPDATE {_tableName} SET {setClause} WHERE id = @Id";
            var result = await connection.ExecuteAsync(sql, entity);
            return result > 0;
        }
    }
}
