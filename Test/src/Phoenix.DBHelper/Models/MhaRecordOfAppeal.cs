using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MhaRecordOfAppeal : BaseClass
    {

        private string tableName = "MhaRecordOfAppeal";
        private string primaryKeyName = "MhaRecordOfAppealId";

        public MhaRecordOfAppeal()
        {
            AuthenticateUser();
        }

        public MhaRecordOfAppeal(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMhaRecordOfAppeal(Guid ownerid, Guid personid, Guid personmhalegalstatusid, Guid personmhaappealid,
            int appealtypeid, DateTime proposedstartddateandtime, DateTime proposedenddateandtime, bool inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personmhalegalstatusid", personmhalegalstatusid);
            AddFieldToBusinessDataObject(dataObject, "personmhaappealid", personmhaappealid);
            AddFieldToBusinessDataObject(dataObject, "proposedstartddateandtime", proposedstartddateandtime);
            AddFieldToBusinessDataObject(dataObject, "proposedenddateandtime", proposedenddateandtime);

            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetMhaRecordOfAppealByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetMhaRecordOfAppealByMhaLegalStatusIdID(Guid personmhalegalstatusid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personmhalegalstatusid", ConditionOperatorType.Equal, personmhalegalstatusid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetMhaRecordOfAppealByMhaLegalStatusIdAndAppealType(Guid personmhalegalstatusid, int appealtypeid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personmhalegalstatusid", ConditionOperatorType.Equal, personmhalegalstatusid);

            this.BaseClassAddTableCondition(query, "appealtypeid", ConditionOperatorType.Equal, appealtypeid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid MhaRecordOfAppealId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "MhaRecordOfAppealId", ConditionOperatorType.Equal, MhaRecordOfAppealId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMhaRecordOfAppeal(Guid MhaRecordOfAppealId)
        {
            this.DeleteRecord(tableName, MhaRecordOfAppealId);
        }



    }
}
