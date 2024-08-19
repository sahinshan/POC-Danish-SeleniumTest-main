using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserWorkSchedule : BaseClass
    {

        public string TableName = "UserWorkSchedule";
        public string PrimaryKeyName = "UserWorkScheduleId";


        public UserWorkSchedule()
        {
            AuthenticateUser();
        }

        public UserWorkSchedule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateUserWorkSchedule(string title, Guid systemuserid, Guid ownerid, Guid recurrencepatternid,
            DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "unavailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AdHoc", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateUserWorkSchedule(string title, Guid systemuserid, Guid ownerid, Guid recurrencepatternid, Guid systemuseremploymentcontractid, Guid availabilitytypesid,
           DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime, Guid recruitmentroleapplicationid, Guid applicantid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuseremploymentcontractid", systemuseremploymentcontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "availabilitytypesid", availabilitytypesid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "unavailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AdHoc", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "recruitmentroleapplicationid", recruitmentroleapplicationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "applicantid", applicantid);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateUserWorkSchedule(string title, Guid? systemuserid, Guid ownerid, Guid recurrencepatternid, Guid? systemuseremploymentcontractid, Guid availabilitytypesid,
           DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime, Guid recruitmentroleapplicationid, Guid applicantid, int weeknumber)

        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuseremploymentcontractid", systemuseremploymentcontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "availabilitytypesid", availabilitytypesid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "unavailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AdHoc", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "recruitmentroleapplicationid", recruitmentroleapplicationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "applicantid", applicantid);
            AddFieldToBusinessDataObject(buisinessDataObject, "weeknumber", weeknumber);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateUserWorkSchedule(Guid systemuserid, Guid ownerid, Guid recurrencepatternid, Guid systemuseremploymentcontractid, Guid availabilitytypesid, DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime, int weeknumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);

            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuseremploymentcontractid", systemuseremploymentcontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "availabilitytypesid", availabilitytypesid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "weeknumber", weeknumber);

            AddFieldToBusinessDataObject(buisinessDataObject, "unavailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AdHoc", 0);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateUserWorkSchedule(string title, Guid systemuserid, Guid ownerid, Guid recurrencepatternid, Guid systemuseremploymentcontractid, Guid availabilitytypesid,
            DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime, int? weeknumber, bool adhoc = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuseremploymentcontractid", systemuseremploymentcontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "availabilitytypesid", availabilitytypesid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "unavailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "weeknumber", weeknumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "AdHoc", adhoc);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetUserWorkScheduleByUserID(Guid systemuserid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemuserid", ConditionOperatorType.Equal, systemuserid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetUserRoleApplicantID(Guid applicantid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            this.BaseClassAddTableCondition(query, "applicantid", ConditionOperatorType.Equal, applicantid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public List<Guid> GetByApplicantID(Guid ApplicantId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ApplicantId", ConditionOperatorType.Equal, ApplicantId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetUserWorkScheduleByID(Guid UserWorkScheduleId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, UserWorkScheduleId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetUserWorkScheduleByApplicantID(Guid applicantid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, applicantid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetUserWorkScheduleByUserID_StartDate_Title(Guid systemuserid, string startDate, string title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemuserid", ConditionOperatorType.Equal, systemuserid);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startDate);
            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Contains, title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetUserWorkScheduleByUserID_StartDate_Title_StartEndTime(Guid systemuserid, string startDate, string title, string startTime, string endTime)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemuserid", ConditionOperatorType.Equal, systemuserid);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.Equal, startDate);
            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Contains, title);
            this.BaseClassAddTableCondition(query, "starttime", ConditionOperatorType.Contains, startTime);
            this.BaseClassAddTableCondition(query, "endtime", ConditionOperatorType.Contains, endTime);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetUserWorkScheduleByTitle(string title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateEndDate(Guid UserWorkScheduleId, DateTime? enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, UserWorkScheduleId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteUserWorkSchedule(Guid UserWorkScheduleId)
        {
            this.DeleteRecord(TableName, UserWorkScheduleId);
        }
    }
}
