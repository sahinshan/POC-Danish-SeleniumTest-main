using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class DataFormsProxy
    {
        public DataFormsProxy()
        {
            _IDataForms = new DataForms();
        }


        private IDataForms _IDataForms;


        public Entities.Portal.DataForm GetDataForm(Guid websiteId, Guid formId, string SecurityToken, string PortalUserToken)
        {
            return _IDataForms.GetDataForm(websiteId, formId, SecurityToken, PortalUserToken);
        }
        public Entities.Portal.DataForm GetDataForm(Guid websiteId, Guid formId, Guid id, string SecurityToken, string PortalUserToken)
        {
            return _IDataForms.GetDataForm(websiteId, formId, id, SecurityToken, PortalUserToken);
        }
        public Entities.Portal.DataForm GetDataForm(Guid websiteId, string businessObjectName, string formName, string SecurityToken, string PortalUserToken)
        {
            return _IDataForms.GetDataForm(websiteId, businessObjectName, formName, SecurityToken, PortalUserToken);
        }
        public Entities.Portal.DataForm GetDataForm(Guid websiteId, string businessObjectName, string formName, Guid id, string SecurityToken, string PortalUserToken)
        {
            return _IDataForms.GetDataForm(websiteId, businessObjectName, formName, id, SecurityToken, PortalUserToken);
        }
        public Entities.Portal.DataForm GetDataFormByRequest(Guid websiteId, Entities.Portal.RetrievePortalDataFormRequest request, string SecurityToken, string PortalUserToken)
        {
            return _IDataForms.GetDataFormByRequest(websiteId, request, SecurityToken, PortalUserToken);
        }
        public string Save(Guid websiteId, Entities.Portal.PortalRecord record, string SecurityToken, string PortalUserToken)
        {
            return _IDataForms.Save(websiteId, record, SecurityToken, PortalUserToken);
        }

    }
}
