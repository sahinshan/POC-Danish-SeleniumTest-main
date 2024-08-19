using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DataRestriction : BaseClass
    {

        private string tableName = "DataRestriction";
        private string primaryKeyName = "DataRestrictionId";

        public DataRestriction()
        {
            AuthenticateUser();
        }

        public DataRestriction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDataRestriction(string RestrcitionName, int AccessTypeId, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "RestrcitionName", RestrcitionName);
            AddFieldToBusinessDataObject(dataObject, "AccessTypeId", AccessTypeId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }


        public Guid CreateDataRestriction(Guid PrimaryKeyName, string RestrcitionName, int AccessTypeId, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, primaryKeyName, PrimaryKeyName);
            AddFieldToBusinessDataObject(dataObject, "RestrcitionName", RestrcitionName);
            AddFieldToBusinessDataObject(dataObject, "AccessTypeId", AccessTypeId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "RestrcitionName", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetDataRestrictionByID(Guid DataRestrictionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DataRestrictionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
