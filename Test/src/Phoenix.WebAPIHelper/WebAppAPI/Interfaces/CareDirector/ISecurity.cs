using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    public interface ISecurity
    {
        string Authenticate();

        string Authenticate(string EnvironmentID, string UserName, string Password);
    }
}
