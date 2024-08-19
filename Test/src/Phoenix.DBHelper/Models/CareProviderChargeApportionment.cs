using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderChargeApportionment : BaseClass
    {

        public string TableName = "CareProviderChargeApportionment";
        public string PrimaryKeyName = "CareProviderChargeApportionmentId";


        public CareProviderChargeApportionment()
        {
            AuthenticateUser();
        }

        public CareProviderChargeApportionment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetById(Guid CareProviderChargeApportionmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderChargeApportionmentId", ConditionOperatorType.Equal, CareProviderChargeApportionmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCareProviderChargeApportionmentByID(Guid CareProviderChargeApportionmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderChargeApportionmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCareProviderChargeApportionment(Guid OwnerId, Guid OwningBusinessUnitId, string Name, Guid PersonId, DateTime StartDate, DateTime? EndDate, int ServiceTypeId, Guid? CareProviderPersonContractId, Guid? CareProviderPersonContractServiceId, int AportionmentTypeId, int Validated)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(dataObject, "servicetypeid", ServiceTypeId);
            AddFieldToBusinessDataObject(dataObject, "CareProviderPersonContractId", CareProviderPersonContractId);
            AddFieldToBusinessDataObject(dataObject, "CareProviderPersonContractServiceId", CareProviderPersonContractServiceId);
            AddFieldToBusinessDataObject(dataObject, "AportionmentTypeId", AportionmentTypeId);
            AddFieldToBusinessDataObject(dataObject, "Validated", Validated);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public void UpdateStartDate(Guid CareProviderChargeApportionmentId, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEndDate(Guid CareProviderChargeApportionmentId, DateTime? EndDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderChargeApportionment(Guid CareProviderChargeApportionmentId)
        {
            this.DeleteRecord(TableName, CareProviderChargeApportionmentId);
        }

        public void UpdateValidated(Guid CareProviderChargeApportionmentId, bool validated)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderChargeApportionmentId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "validated", validated);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
