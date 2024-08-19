using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseClosureReason : BaseClass
    {

        public string TableName = "CaseClosureReason";
        public string PrimaryKeyName = "CaseClosureReasonId";


        public CaseClosureReason()
        {
            AuthenticateUser();
        }

        public CaseClosureReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseClosureReason(Guid ownerid, string Name, string LegacyId, string Code, DateTime StartDate, int BusinessTypeId, bool ValidForRejection, Guid? RTTTreatmentStatusId = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "LegacyId", LegacyId);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "BusinessTypeId", BusinessTypeId);
            AddFieldToBusinessDataObject(dataObject, "ValidForRejection", ValidForRejection);

            AddFieldToBusinessDataObject(dataObject, "RTTTreatmentStatusId", RTTTreatmentStatusId);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseClosureReasonByID(Guid CaseClosureReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseClosureReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseClosureReason(Guid CaseClosureReasonId)
        {
            this.DeleteRecord(TableName, CaseClosureReasonId);
        }
    }
}
