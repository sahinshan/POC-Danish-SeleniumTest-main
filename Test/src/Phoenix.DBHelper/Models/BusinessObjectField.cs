using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BusinessObjectField : BaseClass
    {
        public string TableName { get { return "BusinessObjectField"; } }
        public string PrimaryKeyName { get { return "BusinessObjectFieldId"; } }


        public BusinessObjectField()
        {
            AuthenticateUser();
        }

        public BusinessObjectField(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBusinessObjectFieldByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);



            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBusinessObjectFieldByName(string Name, Guid businessobjectid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);
            this.BaseClassAddTableCondition(query, "businessobjectid", ConditionOperatorType.Equal, businessobjectid);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBusinessObjectByID(Guid BusinessObjectId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "BusinessObjectFieldId", ConditionOperatorType.Equal, BusinessObjectId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
