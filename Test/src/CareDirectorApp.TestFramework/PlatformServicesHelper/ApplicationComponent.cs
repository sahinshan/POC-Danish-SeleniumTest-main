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
    public class ApplicationComponent : BaseClass
    {

        public ApplicationComponent(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetApplicationComponentByComponentID(Guid ComponentId)
        {
            var query = this.GetDataQueryObject("ApplicationComponent", false, "ApplicationComponentId");
            
            this.AddReturnField(query, "ApplicationComponent", "ApplicationComponentId");

            this.AddTableCondition(query, "ComponentId", ConditionOperatorType.Equal, ComponentId);

            return ExecuteDataQueryAndExtractGuidFields(query, "ApplicationComponentId");
        }

        public Dictionary<string, object> GetApplicationComponentByID(Guid ApplicationComponentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("ApplicationComponent", false, "ApplicationComponentId");
            this.AddReturnFields(query, "ApplicationComponent", FieldsToReturn);

            this.AddTableCondition(query, "ApplicationComponentId", ConditionOperatorType.Equal, ApplicationComponentId);
            
            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void CreateApplicationComponent(Guid ApplicationId, Guid ComponentId, string componentidtablename, string componentidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ApplicationComponent", "ApplicationComponentId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ApplicationId", ApplicationId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ComponentId", ComponentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "componentidtablename", componentidtablename);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "componentidname", componentidname);

            this.CreateRecord(buisinessDataObject);
        }

        public void DeleteApplicationComponent(Guid ApplicationComponentId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ApplicationComponent", "ApplicationComponentId");

            this.DeleteRecord("ApplicationComponent", ApplicationComponentId);
        }
    }
}
