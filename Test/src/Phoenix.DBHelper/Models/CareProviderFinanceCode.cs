using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceCode : BaseClass
    {

        private string tableName = "CareProviderFinanceCode";
        private string primaryKeyName = "CareProviderFinanceCodeId";

        public CareProviderFinanceCode()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceCode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByDescriptionAndCode(string description, string code)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "description", ConditionOperatorType.Equal, description);
            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.Equal, code);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Guid CreateCareProviderFinanceCode(Guid ownerid, Guid financecodelocationid, string description, string code)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financecodelocationid", financecodelocationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "description", description);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByCode(int code)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.Equal, code);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderFinanceCodeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnFields(query, tableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceCodeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllCareProviderFinanceCode()
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnField(query, tableName, primaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public void DeleteCareProviderFinanceCodeRecord(Guid CareProviderServiceDetailId)
        {
            this.DeleteRecord(tableName, CareProviderServiceDetailId);
        }
    }
}

