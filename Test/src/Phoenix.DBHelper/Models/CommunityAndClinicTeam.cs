using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CommunityAndClinicTeam : BaseClass
    {
        public string TableName = "CommunityAndClinicTeam";
        public string PrimaryKeyName = "CommunityAndClinicTeamId";

        public CommunityAndClinicTeam()
        {
            AuthenticateUser();
        }

        public CommunityAndClinicTeam(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCommunityAndClinicTeam(Guid ownerid, Guid providerid, Guid teamid, string title, string comments)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "teamid", teamid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "comments", comments);
            AddFieldToBusinessDataObject(buisinessDataObject, "supportspathwayrttrules", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByTitle(string Title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetByComments(string comments)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "comments", ConditionOperatorType.Equal, comments);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public Dictionary<string, object> GetByID(Guid CommunityAndClinicTeamId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CommunityAndClinicTeamId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCommunityAndClinicTeam(Guid CommunityAndClinicTeamId)
        {
            this.DeleteRecord(TableName, CommunityAndClinicTeamId);
        }

    }
}
