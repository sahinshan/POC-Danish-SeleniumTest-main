using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ApplicationComponent : BaseClass
    {
        public string TableName { get { return "ApplicationComponent"; } }
        public string PrimaryKeyName { get { return "ApplicationComponentid"; } }


        public ApplicationComponent()
        {
            AuthenticateUser();
        }

        public ApplicationComponent(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateApplicationComponent(Guid ApplicationId, Guid ComponentId, string componentidtablename, string componentidname)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ApplicationId", ApplicationId);
            AddFieldToBusinessDataObject(dataObject, "ComponentId", ComponentId);
            AddFieldToBusinessDataObject(dataObject, "componentidtablename", componentidtablename);
            AddFieldToBusinessDataObject(dataObject, "componentidname", componentidname);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByComponentID(Guid ComponentId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ComponentId", ConditionOperatorType.Equal, ComponentId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetApplicationComponentByID(Guid ApplicationComponentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("ApplicationComponent", false, "ApplicationComponentId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "ApplicationComponentId", ConditionOperatorType.Equal, ApplicationComponentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteApplicationComponent(Guid ApplicationComponentId)
        {
            this.DeleteRecord(TableName, ApplicationComponentId);
        }
    }
}
