using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class EmailTo : BaseClass
    {

        public string TableName = "EmailTo";
        public string PrimaryKeyName = "EmailToId";


        public EmailTo()
        {
            AuthenticateUser();
        }

        public EmailTo(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateEmailTo(Guid emailid, Guid regardingid, string regardingidtablename, string regardingidname)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "emailid", emailid);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByRegardingID(Guid RegardingId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByEmailAndRegardingID(Guid EmailId, Guid RegardingId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "EmailId", ConditionOperatorType.Equal, EmailId);
            this.BaseClassAddTableCondition(query, "RegardingId", ConditionOperatorType.Equal, RegardingId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByEmailID(Guid EmailId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "EmailId", ConditionOperatorType.Equal, EmailId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid EmailToId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, EmailToId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeleteEmailTo(Guid EmailToId)
        {
            this.DeleteRecord(TableName, EmailToId);
        }
    }
}
