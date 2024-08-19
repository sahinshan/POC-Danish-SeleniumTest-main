using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class DataViewProxy
    {
        public DataViewProxy()
        {
            _DataView = new DataView();
        }


        private IDataView _DataView;


        public Entities.Portal.RetrievePortalDataViewResponse GetView(Guid websiteId, Entities.Portal.RetrievePortalDataViewRequest DataViewRequest, string SecurityToken, string PortalUserToken)
        {
            return _DataView.GetView(websiteId, DataViewRequest, SecurityToken, PortalUserToken);
        }

        public Entities.Portal.RetrievePortalDataViewLookupResponse GetLookupView(Guid websiteId, Entities.Portal.RetrievePortalDataViewRequest DataViewRequest, string SecurityToken, string PortalUserToken)
        {
            return _DataView.GetLookupView(websiteId, DataViewRequest, SecurityToken, PortalUserToken);
        }
    }
}
