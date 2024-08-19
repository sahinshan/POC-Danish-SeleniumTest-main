using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IDataView
    {
        Entities.Portal.RetrievePortalDataViewResponse GetView(Guid websiteId, Entities.Portal.RetrievePortalDataViewRequest DataViewRequest, string SecurityToken, string PortalUserToken);
        Entities.Portal.RetrievePortalDataViewLookupResponse GetLookupView(Guid websiteId, Entities.Portal.RetrievePortalDataViewRequest DataViewRequest, string SecurityToken, string PortalUserToken);
        
    }
}
