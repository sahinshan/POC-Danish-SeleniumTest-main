using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CWLookupBtn_roleid : BaseClass
    {

        public string TableName = "StaffReview";
        public string PrimaryKeyName = "StaffReviewId";


        public CWLookupBtn_roleid()
        {
            AuthenticateUser();
        }

        public CWLookupBtn_roleid(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetStaffReviewById(Guid Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetStaffReviewByInpatientBayId(Guid InpatientBayId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InpatientBayId", ConditionOperatorType.Equal, InpatientBayId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetStaffReviewByBednumber(string Bednumber)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Bednumber", ConditionOperatorType.Equal, Bednumber);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetStaffReviewByID(Guid StaffReviewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, StaffReviewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCWLookupBtn_roleid(string name, Guid regardinguserid, Guid roleid, Guid reviewtypeid, DateTime completeddate, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "regardinguserid", regardinguserid);
            AddFieldToBusinessDataObject(dataObject, "roleid", roleid);
            AddFieldToBusinessDataObject(dataObject, "reviewtypeid", reviewtypeid);
            AddFieldToBusinessDataObject(dataObject, "completeddate", completeddate);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);

            return this.CreateRecord(dataObject);
        }

        public void DeleteStaffReview(Guid StaffReviewId)
        {
            this.DeleteRecord(TableName, StaffReviewId);
        }
    }
}
