using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DemographicsTitle : BaseClass
    {
        public string TableName { get { return "DemographicsTitle"; } }
        public string PrimaryKeyName { get { return "DemographicsTitleid"; } }


        public DemographicsTitle()
        {
            AuthenticateUser();
        }

        public DemographicsTitle(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDemographicsTitle(string Name, DateTime startdate, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetDemographicsTitleByID(Guid DemographicsTitleId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("DemographicsTitle", false, "DemographicsTitleId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "DemographicsTitleId", ConditionOperatorType.Equal, DemographicsTitleId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
