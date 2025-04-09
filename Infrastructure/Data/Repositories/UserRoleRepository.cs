using Core.Domain.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class UserRoleRepository : BaseRepository<User_Role> , IUserRoleRepository
    {
        public UserRoleRepository(DapperContext context) : base(context)
        {
        }
    }
}
