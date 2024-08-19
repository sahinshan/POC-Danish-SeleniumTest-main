using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffReview : BaseClass
    {

        public string TableName = "StaffReview";
        public string PrimaryKeyName = "StaffReviewId";


        public StaffReview()
        {
            AuthenticateUser();
        }

        public StaffReview(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByRoleId(Guid roleid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "roleid", ConditionOperatorType.Equal, roleid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserId(Guid regardinguserid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardinguserid", ConditionOperatorType.Equal, regardinguserid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetStaffReviewIdByModifiedUserIdandStaffReviewName(Guid modifiedby, string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "modifiedby", ConditionOperatorType.Equal, modifiedby);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }
        public List<Guid> GetByStaffReviewTypeId(Guid reviewtypeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "reviewtypeid", ConditionOperatorType.Equal, reviewtypeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserId(Guid regardinguserid, DateTime completeddate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardinguserid", ConditionOperatorType.Equal, regardinguserid);
            this.BaseClassAddTableCondition(query, "completeddate", ConditionOperatorType.Equal, completeddate);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetStaffReviewByID(Guid StaffReviewId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, StaffReviewId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateStaffReview(Guid regardinguserid, Guid roleid, Guid reviewtypeid, Guid? reviewedbyid, int statusid, DateTime? completeddate, DateTime? duedate, DateTime? nextreviewdate, TimeSpan? reviewstarttime, TimeSpan? reviewendtime, int? outcomeid, Guid ownerid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "regardinguserid", regardinguserid);
            AddFieldToBusinessDataObject(dataObject, "roleid", roleid);
            AddFieldToBusinessDataObject(dataObject, "reviewtypeid", reviewtypeid);
            AddFieldToBusinessDataObject(dataObject, "reviewedbyid", reviewedbyid);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "completeddate", completeddate);
            AddFieldToBusinessDataObject(dataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(dataObject, "nextreviewdate", nextreviewdate);
            AddFieldToBusinessDataObject(dataObject, "reviewstarttime", reviewstarttime);
            AddFieldToBusinessDataObject(dataObject, "reviewendtime", reviewendtime);
            AddFieldToBusinessDataObject(dataObject, "outcomeid", outcomeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);


            return this.CreateRecord(dataObject);
        }

        public Guid CreateStaffReview(Guid regardinguserid, Guid roleid, Guid reviewtypeid, Guid? reviewedbyid, int statusid, Guid ownerid, DateTime? nextreviewdate = null, TimeSpan? reviewstarttime = null, TimeSpan? reviewendtime = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "regardinguserid", regardinguserid);
            AddFieldToBusinessDataObject(dataObject, "roleid", roleid);
            AddFieldToBusinessDataObject(dataObject, "reviewtypeid", reviewtypeid);
            AddFieldToBusinessDataObject(dataObject, "reviewedbyid", reviewedbyid);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "nextreviewdate", nextreviewdate);
            AddFieldToBusinessDataObject(dataObject, "reviewstarttime", reviewstarttime);
            AddFieldToBusinessDataObject(dataObject, "reviewendtime", reviewendtime);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);


            return this.CreateRecord(dataObject);
        }

        public void UpdateAppointmentStartAndEndTime(Guid StaffReviewId, TimeSpan reviewstarttime, TimeSpan reviewendtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, StaffReviewId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewstarttime", reviewstarttime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewendtime", reviewendtime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteStaffReview(Guid StaffReviewId)
        {
            this.DeleteRecord(TableName, StaffReviewId);
        }
    }
}
