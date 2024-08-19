using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ReferralPriority : BaseClass
    {
        public string TableName { get { return "ReferralPriority"; } }
        public string PrimaryKeyName { get { return "ReferralPriorityid"; } }


        public ReferralPriority()
        {
            AuthenticateUser();
        }

        public ReferralPriority(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateReferralPriority(Guid ownerid, string Name, DateTime StartDate, int PriorityCategoryId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "PriorityCategoryId", PriorityCategoryId);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateReferralPriority(Guid ReferralPriorityid, Guid ownerid, string Name, DateTime StartDate, int PriorityCategoryId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ReferralPriorityid", ReferralPriorityid);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "PriorityCategoryId", PriorityCategoryId);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetByName(string Name)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ReferralPriorityId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ReferralPriorityId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteReferralPriority(Guid ReferralPriorityid)
        {
            this.DeleteRecord(TableName, ReferralPriorityid);
        }
    }
}
