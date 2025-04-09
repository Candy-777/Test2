using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{

    // таблица для связывания многие ко многим
    public class User_Role
    {
        public int Id { get; set; }

        // внешний ключ на юзера
        public int User_Id { get; set; }
        // внешний ключ на роль
        public int Role_Id { get; set; }
    }
}
