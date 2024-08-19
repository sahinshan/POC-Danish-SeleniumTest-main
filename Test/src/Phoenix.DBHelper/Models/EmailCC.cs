using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class EmailCc : BaseClass
    {

        public string TableName = "EmailCc";
        public string PrimaryKeyName = "EmailCcId";


        public EmailCc()
        {
            AuthenticateUser();
        }

        public EmailCc(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateEmailCc(Guid emailid, Guid regardingid, string regardingidtablename, string regardingidname)
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

        public Dictionary<string, object> GetByID(Guid EmailCcId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, EmailCcId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeleteEmailCc(Guid EmailCcId)
        {
            this.DeleteRecord(TableName, EmailCcId);
        }
    }
}
