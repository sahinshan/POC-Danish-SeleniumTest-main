using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MASHEpisodeForm : BaseClass
    {

        public string TableName = "MASHEpisodeForm";
        public string PrimaryKeyName = "MASHEpisodeFormId";


        public MASHEpisodeForm()
        {
            AuthenticateUser();
        }

        public MASHEpisodeForm(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMASHEpisodeFormRecord(Guid ownerid, Guid documentid, Guid mashepisodeid, Guid personid, DateTime startdate, int assessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "documentid", documentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "mashepisodeid", mashepisodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "sdeexecuted", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "answersinitialized", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", 0);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByMashEpisodeId(Guid mashepisodeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "mashepisodeid", ConditionOperatorType.Equal, mashepisodeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByMashEpisodeId(Guid mashepisodeid, DateTime startdate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "mashepisodeid", ConditionOperatorType.Equal, mashepisodeid);

            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.GreaterEqual, startdate.Date);
            this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.LessEqual, startdate.Date.AddHours(23).AddMinutes(59));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByMashEpisodeIdAndFormType(Guid mashepisodeid, Guid FormTypeID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "mashepisodeid", ConditionOperatorType.Equal, mashepisodeid);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, FormTypeID);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public List<Guid> GetByMashEpisodeIdAndFormType(Guid mashepisodeid, Guid FormTypeID, DateTime StartDate)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "mashepisodeid", ConditionOperatorType.Equal, mashepisodeid);
            this.BaseClassAddTableCondition(query, "DocumentId", ConditionOperatorType.Equal, FormTypeID);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetMASHEpisodeFormByID(Guid MASHEpisodeFormId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MASHEpisodeFormId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateStatus(Guid MASHEpisodeFormId, int assessmentstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, MASHEpisodeFormId);

            AddFieldToBusinessDataObject(buisinessDataObject, "assessmentstatusid", assessmentstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteMASHEpisodeForm(Guid MASHEpisodeFormId)
        {
            this.DeleteRecord(TableName, MASHEpisodeFormId);
        }
    }
}
