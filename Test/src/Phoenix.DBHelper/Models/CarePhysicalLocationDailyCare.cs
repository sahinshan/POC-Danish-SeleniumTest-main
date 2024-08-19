using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CarePhysicalLocationDailyCare : BaseClass
    {

        private string TableName = "CarePhysicalLocationDailyCare";
        private string PrimaryKeyName = "CarePhysicalLocationDailyCareid";


        public CarePhysicalLocationDailyCare()
        {
            AuthenticateUser();
        }

        public CarePhysicalLocationDailyCare(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCarePhysicalLocationDailyCare(Guid CarePhysicalLocationId, int OptionSetValueId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "carephysicallocationid", CarePhysicalLocationId);
            AddFieldToBusinessDataObject(dataObject, "optionsetvalueid", OptionSetValueId);

            return this.CreateRecord(dataObject);
        }

        //Method to Get By CarePhysicalLocationId and OptionSetValueId
        public List<Guid> GetByCarePhysicalLocationIdAndOptionSetValueId(Guid CarePhysicalLocationId, int OptionSetValueId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "carephysicallocationid", ConditionOperatorType.Equal, CarePhysicalLocationId);
            this.BaseClassAddTableCondition(query, "optionsetvalueid", ConditionOperatorType.Equal, OptionSetValueId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetById(Guid CarePhysicalLocationDailyCareid, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CarePhysicalLocationDailyCareid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
