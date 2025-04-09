using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }

        // внешний ключ на юзера
        public int User_Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }

    }
}
