using Core.Domain.Entities;
using Core.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DapperContext context) : base(context)
        {
        }

        public async Task<User> GetByUsername(string username)
        {
            using (var connection = _context.CreateConnection()) 
            {
                var sql = "SELECT * FROM public.users WHERE username = @Username";
                var result = await connection.QueryFirstOrDefaultAsync<User>(sql,new {Username = username});
                return result;
            }
        }

        
    }
}
