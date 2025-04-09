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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DapperContext context) : base(context)
        {
        }

        public async Task<Role> GetRoleByName(string role)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "SELECT * FROM public.roles WHERE name = @Role";
                var result = await connection.QueryFirstOrDefaultAsync<Role>(sql, new { Role = role });
                return result;
            }
        }
    }
}
