using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IWebsiteResourceFiles
    {
        string GetResourceFileByName(Guid WebsiteID, string ResourceFileName, string SecurityToken);
    }
}
