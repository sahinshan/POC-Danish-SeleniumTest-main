using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class inpatientSeclusionReview : BaseClass
    {
        public string TableName { get { return "inpatientSeclusionReview"; } }
        public string PrimaryKeyName { get { return "inpatientSeclusionReviewid"; } }


        public inpatientSeclusionReview()
        {
            AuthenticateUser();
        }

        public inpatientSeclusionReview(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid inpatientSeclusionReviewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, inpatientSeclusionReviewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAdmissionMethodByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateLeaveAWOLRecord(Guid ownerid, Guid OwningBusinessUnitId, Guid personId, Guid CaseId, DateTime AdmissionDateTime, Guid inpatientleavetypeid, DateTime AgreedLeaveDateTime,
                                        DateTime AgreedReturnDateTime, string whoauthorisedleaveidtablename, Guid whoauthorisedleaveid, string whoauthorisedleaveidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personId", personId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CaseId", CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AdmissionDateTime", AdmissionDateTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inpatientleavetypeid", inpatientleavetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AgreedLeaveDateTime", AgreedLeaveDateTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AgreedReturnDateTime", AgreedReturnDateTime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "whoauthorisedleaveidtablename", whoauthorisedleaveidtablename);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "whoauthorisedleaveid", whoauthorisedleaveid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "whoauthorisedleaveidname", whoauthorisedleaveidname);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteinpatientSeclusionReview(Guid inpatientSeclusionReviewid)
        {
            this.DeleteRecord(TableName, inpatientSeclusionReviewid);
        }
    }
}
