using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TokenManager
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        // Min
        public int AccessTokenLifeTime { get; set; }
        // days
        public int RefreshTokenLifeTime { get; set; }
    }
}
