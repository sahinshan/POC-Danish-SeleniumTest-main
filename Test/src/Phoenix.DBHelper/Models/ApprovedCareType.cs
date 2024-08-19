using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ApprovedCareType : BaseClass
    {
        public string TableName = "ApprovedCareType";
        public string PrimaryKeyName = "ApprovedCareTypeId";

        public ApprovedCareType()
        {
            AuthenticateUser();
        }

        public ApprovedCareType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateApprovedCareType(
            Guid providerid, int approvalstatusid, Guid serviceelement1id, Guid caretypeid,
            bool malegenderaccommodated, bool femalegenderaccommodated, int capacity, DateTime startdate,
            int maleagefrom, int maleageto, int femaleagefrom, int femaleageto,
            bool restrictionsapply, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "approvalstatusid", approvalstatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caretypeid", caretypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "malegenderaccommodated", malegenderaccommodated);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "femalegenderaccommodated", femalegenderaccommodated);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "capacity", capacity);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "maleagefrom", maleagefrom);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "maleageto", maleageto);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "femaleagefrom", femaleagefrom);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "femaleageto", femaleageto);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "restrictionsapply", restrictionsapply);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);


            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByProvider(Guid providerid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetApprovedCareTypeByID(Guid ApprovedCareTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ApprovedCareTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteApprovedCareType(Guid ApprovedCareTypeId)
        {
            this.DeleteRecord(TableName, ApprovedCareTypeId);
        }

    }
}
