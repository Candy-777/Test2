using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class User
    {
       public int Id { get; set; }
       public string Username { get; set; }
       public string Department { get; set; }
       public string PasswordHash { get; set; }
       
     
    }
}
