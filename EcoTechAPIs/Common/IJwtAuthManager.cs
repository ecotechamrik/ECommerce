using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoTechAPIs.Common
{
    public interface IJwtAuthManager
    {
        string AuthenticateUser(string username, string password);
    }
}
