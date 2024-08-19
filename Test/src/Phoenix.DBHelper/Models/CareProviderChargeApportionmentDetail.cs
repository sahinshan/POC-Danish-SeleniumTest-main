using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderChargeApportionmentDetail : BaseClass
    {

        public string TableName = "CareProviderChargeApportionmentDetail";
        public string PrimaryKeyName = "CareProviderChargeApportionmentDetailId";


        public CareProviderChargeApportionmentDetail()
        {
            AuthenticateUser();
        }

        public CareProviderChargeApportionmentDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByChargeApportionmentId(Guid ChargeApportionmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargeApportionmentId", ConditionOperatorType.Equal, ChargeApportionmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByChargeApportionmentDetailId(Guid CareProviderChargeApportionmentDetailId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderChargeApportionmentDetailId", ConditionOperatorType.Equal, CareProviderChargeApportionmentDetailId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCareProviderChargeApportionmentDetailID(Guid CareProviderChargeApportionmentDetailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderChargeApportionmentDetailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCareProviderChargeApportionmentDetail(Guid OwnerId, Guid OwningBusinessUnitId, string Name, Guid ChargeApportionmentId, int Priority, Guid PayerId, string payeridtablename, string payeridname, bool Balance, int? Amount = null, int? Percentage = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "ChargeApportionmentId", ChargeApportionmentId);
            AddFieldToBusinessDataObject(dataObject, "Priority", Priority);
            AddFieldToBusinessDataObject(dataObject, "PayerId", PayerId);
            AddFieldToBusinessDataObject(dataObject, "payeridtablename", payeridtablename);
            AddFieldToBusinessDataObject(dataObject, "payeridname", payeridname);
            AddFieldToBusinessDataObject(dataObject, "Balance", Balance);
            AddFieldToBusinessDataObject(dataObject, "Amount", Amount);
            AddFieldToBusinessDataObject(dataObject, "Percentage", Percentage);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public void UpdateBalanceOption(Guid CareProviderChargeApportionmentId, bool Balance)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Balance", Balance);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAmount(Guid CareProviderChargeApportionmentId, int? Amount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Amount", Amount);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePercentage(Guid CareProviderChargeApportionmentId, int? Percentage)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Percentage", Percentage);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePriority(Guid CareProviderChargeApportionmentId, int Priority)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Priority", Priority);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePayer(Guid CareProviderChargeApportionmentId, Guid PayerId, string payeridtablename, string payeridname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PayerId", PayerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "payeridtablename", payeridtablename);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "payeridname", payeridname);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderChargeApportionmentDetail(Guid CareProviderChargeApportionmentDetailId)
        {
            this.DeleteRecord(TableName, CareProviderChargeApportionmentDetailId);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderChargeApportionmentDetailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderChargeApportionmentDetailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
