using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CommunityClinicDiaryViewSetup : BaseClass
    {
        public string TableName = "CommunityClinicDiaryViewSetup";
        public string PrimaryKeyName = "CommunityClinicDiaryViewSetupId";

        public CommunityClinicDiaryViewSetup()
        {
            AuthenticateUser();
        }

        public CommunityClinicDiaryViewSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCommunityClinicDiaryViewSetup(Guid ownerid, Guid communityandclinicteamid, string title, DateTime startdate, TimeSpan starttime, TimeSpan endtime, int individualsperday, int groupsperday, int individualspergroup)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "communityandclinicteamid", communityandclinicteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "individualsperday", individualsperday);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupsperday", groupsperday);
            AddFieldToBusinessDataObject(buisinessDataObject, "individualspergroup", individualspergroup);
            AddFieldToBusinessDataObject(buisinessDataObject, "availableonbankholidays", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupbookingallowed", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCommunityClinicDiaryViewSetup(Guid ownerid, Guid communityandclinicteamid, string title, DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime, int individualsperday, int groupsperday, int individualspergroup)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "communityandclinicteamid", communityandclinicteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "individualsperday", individualsperday);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupsperday", groupsperday);
            AddFieldToBusinessDataObject(buisinessDataObject, "individualspergroup", individualspergroup);
            AddFieldToBusinessDataObject(buisinessDataObject, "availableonbankholidays", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupbookingallowed", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCommunityClinicDiaryViewSetupWithNoHomeVisit(Guid ownerid, Guid communityandclinicteamid, string title, DateTime startdate, TimeSpan starttime, TimeSpan endtime, int individualsperday, int groupsperday, int individualspergroup, Guid providerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            // AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "communityandclinicteamid", communityandclinicteamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "individualsperday", individualsperday);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupsperday", groupsperday);
            AddFieldToBusinessDataObject(buisinessDataObject, "individualspergroup", individualspergroup);
            AddFieldToBusinessDataObject(buisinessDataObject, "availableonbankholidays", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "groupbookingallowed", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }



        public List<Guid> GetByCommunityClinicTeam(Guid communityandclinicteamid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "communityandclinicteamid", ConditionOperatorType.Equal, communityandclinicteamid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByTitle(string Title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CommunityClinicDiaryViewSetupId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CommunityClinicDiaryViewSetupId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCommunityClinicDiaryViewSetup(Guid CommunityClinicDiaryViewSetupId)
        {
            this.DeleteRecord(TableName, CommunityClinicDiaryViewSetupId);
        }

    }
}
