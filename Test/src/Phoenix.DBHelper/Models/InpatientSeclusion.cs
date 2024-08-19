using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientSeclusion : BaseClass
    {
        public string TableName { get { return "InpatientSeclusion"; } }
        public string PrimaryKeyName { get { return "InpatientSeclusionid"; } }


        public InpatientSeclusion()
        {
            AuthenticateUser();
        }

        public InpatientSeclusion(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid Personid)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, Personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid InpatientSeclusionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientSeclusionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAdmissionMethodByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateSeclusionRecord(Guid ownerid, Guid OwningBusinessUnitId, Guid personId, Guid CaseId, Guid InpatientSeclusionReasonId, string RationaleForSeclusion,
                                    Guid CommencedById, Guid CommencedApprovedById, DateTime SeclusionDateTime, DateTime SeclusionReviewPlannedDateTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personId", personId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "InpatientSeclusionReasonId", InpatientSeclusionReasonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RationaleForSeclusion", RationaleForSeclusion);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CommencedById", CommencedById);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CommencedApprovedById", CommencedApprovedById);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SeclusionDateTime", SeclusionDateTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SeclusionReviewPlannedDateTime", SeclusionReviewPlannedDateTime);


            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteInpatientSeclusion(Guid InpatientSeclusionid)
        {
            this.DeleteRecord(TableName, InpatientSeclusionid);
        }
    }
}
