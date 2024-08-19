using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserDairy : BaseClass
    {

        public string TableName = "UserDiary";
        public string PrimaryKeyName = "UserDiaryId";


        public UserDairy()
        {
            AuthenticateUser();
        }

        public UserDairy(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }





        public Guid createUserDairy(Guid systemuserid, Guid ownerid, Guid userworkscheduleid, Guid applicantid, Guid recruitmentroleapplicationid,
            DateTime startdate, DateTime enddate, int minutesfrom, int minutesto)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "userworkscheduleid", userworkscheduleid);
            AddFieldToBusinessDataObject(businessDataObject, "applicantid", applicantid);
            AddFieldToBusinessDataObject(businessDataObject, "recruitmentroleapplicationid", recruitmentroleapplicationid);
            AddFieldToBusinessDataObject(businessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(businessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(businessDataObject, "minutesfrom", minutesfrom);
            AddFieldToBusinessDataObject(businessDataObject, "minutesto", minutesto);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "deleted", false);

            return this.CreateRecord(businessDataObject);
        }


        public List<Guid> GetUserDairyByUserID(Guid systemuserid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "systemuserid", ConditionOperatorType.Equal, systemuserid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetUserDairyApplicantID(Guid applicantid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "applicantid", ConditionOperatorType.Equal, applicantid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetUserDairybyWorkScheduleID(Guid userworkscheduleid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, userworkscheduleid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetUserDairyByID(Guid UserDairyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, UserDairyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeleteUserDairy(Guid UserDairyId)
        {
            this.DeleteRecord(TableName, UserDairyId);
        }
    }
}
