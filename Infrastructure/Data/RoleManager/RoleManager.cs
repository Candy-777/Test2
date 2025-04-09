using Core.Domain.Entities;
using Core.Interfaces;
using Dapper;
using System.Transactions;


namespace Infrastructure.Data.RoleManager

{
    public class RoleManager
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly DapperContext _dapperContext;

        public RoleManager(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, DapperContext dapperContext, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;   
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _dapperContext = dapperContext;
        }

        public async Task<int> CreateNewRole(string name)
        {
            var role = new Role { Name = name };
            var result = await _roleRepository.Add(role);
            return result;
        }

        // вернёт массив с названиями ролей для пользователя
        public async Task<IEnumerable<string>> GetUserRoles (User user)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var sql = @"
                            SELECT r.name
                            FROM public.roles r
                            INNER JOIN public.user_roles ur ON ur.role_id = r.id
                            WHERE ur.user_id = @UserId";
                var result = await connection.QueryAsync<string>(sql, new { UserId = user.Id });
                return result;

            }
        }

        // создаёт связь пользователя с ролью через промежуточную таблицу хз зачем транзакция но может быть нужна
        public async Task<bool> AddRoleToUser (User user, string role)
        {
            var _role = await _roleRepository.GetRoleByName(role);
            if (_role == null) 
            {
                throw new Exception("Role not exist");
            }

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        var userRole = new User_Role
                        {
                            Role_Id = _role.Id,
                            User_Id = user.Id
                        };

                        var result = await _userRoleRepository.Add(userRole);
                        if (result > 0)
                        {

                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            throw new Exception("Failed to insert user-role relationship.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Откат транзакции
                         transaction.Rollback();
                        // Логирование ошибки с подробностями
                        throw new Exception("An error occurred while adding the role to the user.", ex);
                    }
                }
            }
        }
    }
}
