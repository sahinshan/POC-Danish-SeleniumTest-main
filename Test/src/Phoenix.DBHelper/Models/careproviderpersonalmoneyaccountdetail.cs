using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonalMoneyAccountDetail : BaseClass
    {

        public string TableName = "CareProviderPersonalMoneyAccountDetail";
        public string PrimaryKeyName = "CareProviderPersonalMoneyAccountDetailid";

        public CareProviderPersonalMoneyAccountDetail()
        {
            AuthenticateUser();
        }

        public CareProviderPersonalMoneyAccountDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderPersonalMoneyAccountDetail(Guid careproviderpersonalmoneyaccountid, DateTime date, Guid entrytypeid, decimal amount, Guid? cashtakenbyid, bool cancellation,
            Guid ownerid, string reference, decimal runningbalance, Guid? observedbyid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersonalmoneyaccountid", careproviderpersonalmoneyaccountid);
            AddFieldToBusinessDataObject(buisinessDataObject, "date", date);
            AddFieldToBusinessDataObject(buisinessDataObject, "entrytypeid", entrytypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "amount", amount);
            AddFieldToBusinessDataObject(buisinessDataObject, "cashtakenbyid", cashtakenbyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cancellation", cancellation);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "reference", reference);
            AddFieldToBusinessDataObject(buisinessDataObject, "runningbalance", runningbalance);
            AddFieldToBusinessDataObject(buisinessDataObject, "observedbyid", observedbyid);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonalMoneyAccountId(Guid careproviderpersonalmoneyaccountid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderpersonalmoneyaccountid", ConditionOperatorType.Equal, careproviderpersonalmoneyaccountid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderPersonalMoneyAccountDetailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderPersonalMoneyAccountDetailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
