using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseFormOutcomeType : BaseClass
    {

        private string TableName = "CaseFormOutcomeType";
        private string PrimaryKeyName = "CaseFormOutcomeTypeId";

        public CaseFormOutcomeType()
        {
            AuthenticateUser();
        }

        public CaseFormOutcomeType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseFormOutcomeType(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime StartDate, DateTime? EndDate, bool Inactive = false, bool ApplicableToAllDocuments = true)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(dataObject, "ApplicableToAllDocuments", ApplicableToAllDocuments);

            return this.CreateRecord(dataObject);
        }

        public void UpdateInactive(Guid CaseFormId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseFormId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByCaseFormOutcomeTypeId(Guid CaseFormOutcomeTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseFormOutcomeTypeId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseFormOutcomeTypeRecord(Guid CaseFormOutcomeTypeId)
        {
            this.DeleteRecord(TableName, CaseFormOutcomeTypeId);
        }

    }
}
