using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class ApplicationLinkedBusinessModule : BaseClass
    {

        public ApplicationLinkedBusinessModule(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetApplicationLinkedBusinessModule(Guid ApplicationId, Guid BusinessModuleId)
        {
            var query = this.GetDataQueryObject("ApplicationLinkedBusinessModule", false, "ApplicationLinkedBusinessModuleId");
            
            this.AddReturnField(query, "ApplicationLinkedBusinessModule", "ApplicationLinkedBusinessModuleId");

            this.AddTableCondition(query, "ApplicationId", ConditionOperatorType.Equal, ApplicationId);
            this.AddTableCondition(query, "BusinessModuleId", ConditionOperatorType.Equal, BusinessModuleId);

            return ExecuteDataQueryAndExtractGuidFields(query, "ApplicationLinkedBusinessModuleId");
        }

        public void UpdateInactiveField(Guid ApplicationLinkedBusinessModuleId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ApplicationLinkedBusinessModule", "ApplicationLinkedBusinessModuleId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ApplicationLinkedBusinessModuleId", ApplicationLinkedBusinessModuleId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
