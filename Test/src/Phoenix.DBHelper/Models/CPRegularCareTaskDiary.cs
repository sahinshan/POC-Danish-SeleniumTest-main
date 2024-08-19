using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPRegularCareTaskDiary : BaseClass
    {

        private string tableName = "CPRegularCareTaskDiary";
        private string primaryKeyName = "CPRegularCareTaskDiaryId";

        public CPRegularCareTaskDiary()
        {
            AuthenticateUser();
        }

        public CPRegularCareTaskDiary(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonIdAndCarePlanId(Guid personid, Guid careplanid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "careplanid", ConditionOperatorType.Equal, careplanid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByPersonIdAndCarePlanId(Guid personid, Guid careplanid,int statusid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "careplanid", ConditionOperatorType.Equal, careplanid);
            this.BaseClassAddTableCondition(query, "statusid", ConditionOperatorType.Equal, statusid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid cpRegularCareTaskDiaryID, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, cpRegularCareTaskDiaryID);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateRegularCareTaskDiary(Guid ownerid, Guid owningbusinessunitid, Guid personid, Guid careplanid,int statusid,DateTime startdate,TimeSpan starttime,Guid regularcaretaskid,string title)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careplanid", careplanid);
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "regularcaretaskid", regularcaretaskid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

            return this.CreateRecord(buisinessDataObject);
        }




    }
}
