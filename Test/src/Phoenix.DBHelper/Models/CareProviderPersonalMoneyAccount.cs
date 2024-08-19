using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonalMoneyAccount : BaseClass
    {

        public string TableName = "careproviderpersonalmoneyaccount";
        public string PrimaryKeyName = "careproviderpersonalmoneyaccountid";

        public CareProviderPersonalMoneyAccount()
        {
            AuthenticateUser();
        }

        public CareProviderPersonalMoneyAccount(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderPersonalMoneyAccount(Guid personid, Guid accounttypeid, string accountname, Guid ownerid, bool inactive = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "accounttypeid", accounttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "accountname", accountname);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderPersonalMoneyAccountId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderPersonalMoneyAccountId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
