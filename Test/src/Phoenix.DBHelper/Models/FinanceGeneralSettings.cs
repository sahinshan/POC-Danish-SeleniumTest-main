using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinanceGeneralSettings : BaseClass
    {
        private string TableName = "FinanceGeneralSettings";
        private string PrimaryKeyName = "FinanceGeneralSettingsId";

        public FinanceGeneralSettings()
        {
            AuthenticateUser();
        }

        public FinanceGeneralSettings(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinanceGeneralSettings(Guid ownerid, int financemoduleid, bool isfullfinancialyear, DateTime financetransactionsupto)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "financemoduleid", financemoduleid);
            AddFieldToBusinessDataObject(businessDataObject, "isfullfinancialyear", isfullfinancialyear);
            AddFieldToBusinessDataObject(businessDataObject, "financetransactionsupto", financetransactionsupto);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateFinanceGeneralSettings(Guid ownerid, Guid owningbusinessunitid, int financemoduleid, bool isfullfinancialyear, DateTime financetransactionsupto)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(businessDataObject, "financemoduleid", financemoduleid);
            AddFieldToBusinessDataObject(businessDataObject, "isfullfinancialyear", isfullfinancialyear);
            AddFieldToBusinessDataObject(businessDataObject, "financetransactionsupto", financetransactionsupto);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetByName(string FinanceGeneralSettingsName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, FinanceGeneralSettingsName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByOwnerIdAndFinanceModuleId(Guid OwnerId, int FinanceModuleId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "OwnerId", ConditionOperatorType.Equal, OwnerId);
            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByBusinessUnitIdAndFinanceModuleId(Guid OwnerId, Guid OwningBusinessUnitId, int FinanceModuleId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "OwnerId", ConditionOperatorType.Equal, OwnerId);
            this.BaseClassAddTableCondition(query, "OwningBusinessUnitId", ConditionOperatorType.Equal, OwningBusinessUnitId);
            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetFinanceGeneralSettingsByFinanceModuleId(int FinanceModuleId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetFinanceGeneralSettingsByFinanceModuleIdAndFinanceTransactionsUpToDate(int FinanceModuleId, DateTime FinanceTransactionsUpTo)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "FinanceModuleId", ConditionOperatorType.Equal, FinanceModuleId);

            this.BaseClassAddTableCondition(query, "FinanceTransactionsUpTo", ConditionOperatorType.Equal, FinanceTransactionsUpTo);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid FinanceGeneralSettingsId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, FinanceGeneralSettingsId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


    }
}
