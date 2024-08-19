using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface IDataForms
    {
        Entities.Portal.DataForm GetDataForm(Guid websiteId, Guid formId, string SecurityToken, string PortalUserToken);
        Entities.Portal.DataForm GetDataForm(Guid websiteId, Guid formId, Guid id, string SecurityToken, string PortalUserToken);
        Entities.Portal.DataForm GetDataForm(Guid websiteId, string businessObjectName, string formName, string SecurityToken, string PortalUserToken);
        Entities.Portal.DataForm GetDataForm(Guid websiteId, string businessObjectName, string formName, Guid id, string SecurityToken, string PortalUserToken);
        Entities.Portal.DataForm GetDataFormByRequest(Guid websiteId, Entities.Portal.RetrievePortalDataFormRequest request, string SecurityToken, string PortalUserToken);
        string Save(Guid websiteId, Entities.Portal.PortalRecord record, string SecurityToken, string PortalUserToken);
    }
}
