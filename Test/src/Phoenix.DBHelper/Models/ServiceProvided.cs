using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvided : BaseClass
    {
        public string TableName { get { return "ServiceProvided"; } }
        public string PrimaryKeyName { get { return "ServiceProvidedid"; } }

        public ServiceProvided()
        {
            AuthenticateUser();
        }

        public ServiceProvided(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvided(Guid OwnerId, Guid ResponsibleUserId, Guid ProviderId, Guid ServiceElement1Id, Guid ServiceElement2Id, Guid? FinanceClientCategoryId, Guid? CurrentRankingId, Guid? GLCodeId, int ApprovalStatusId, bool UsedInFinance = true)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderId", ProviderId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement2Id", ServiceElement2Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "FinanceClientCategoryId", FinanceClientCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CurrentRankingId", CurrentRankingId);
            AddFieldToBusinessDataObject(buisinessDataObject, "GLCodeId", GLCodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ApprovalStatusId", ApprovalStatusId);
            AddFieldToBusinessDataObject(buisinessDataObject, "NegotiatedRatesApply", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "UsedInFinance", UsedInFinance);
            AddFieldToBusinessDataObject(buisinessDataObject, "ContractTypeId", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", 0);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvided(Guid OwnerId, Guid ResponsibleUserId, Guid ProviderId, Guid? ServiceElement1Id, Guid ServiceElement2Id, Guid? ServiceElement3Id, Guid? FinanceClientCategoryId, Guid? CurrentRankingId, Guid? GLCodeId, int ApprovalStatusId, bool UsedInFinance, int ContractTypeId = 1, bool NegotiatedRatesApply = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderId", ProviderId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement2Id", ServiceElement2Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement3Id", ServiceElement3Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "FinanceClientCategoryId", FinanceClientCategoryId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CurrentRankingId", CurrentRankingId);
            AddFieldToBusinessDataObject(buisinessDataObject, "GLCodeId", GLCodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ApprovalStatusId", ApprovalStatusId);
            AddFieldToBusinessDataObject(buisinessDataObject, "NegotiatedRatesApply", NegotiatedRatesApply);
            AddFieldToBusinessDataObject(buisinessDataObject, "UsedInFinance", UsedInFinance);
            AddFieldToBusinessDataObject(buisinessDataObject, "ContractTypeId", ContractTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", 0);

            return CreateRecord(buisinessDataObject);
        }


        public void UpdateStatus(Guid ServiceProvidedId, int approvalStatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvidedId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ApprovalStatusId", approvalStatusId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByProviderId(Guid ProviderId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ProviderId", ConditionOperatorType.Equal, ProviderId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByProviderId(Guid ProviderId, int approvalStatusId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ProviderId", ConditionOperatorType.Equal, ProviderId);
            this.BaseClassAddTableCondition(query, "ApprovalStatusId", ConditionOperatorType.Equal, approvalStatusId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByProviderId_SeviceElement1_ServiceElement2(Guid ProviderId, Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ProviderId", ConditionOperatorType.Equal, ProviderId);
            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "ServiceElement2Id", ConditionOperatorType.Equal, ServiceElement2Id);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceProvidedid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvidedid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceProvided(Guid ServiceProvidedID)
        {
            this.DeleteRecord(TableName, ServiceProvidedID);
        }

        public void UpdateInactiveStatus(Guid ServiceProvidedId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvidedId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
