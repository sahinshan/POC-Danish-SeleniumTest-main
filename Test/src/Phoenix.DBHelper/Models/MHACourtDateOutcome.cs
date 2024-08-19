using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MHACourtDateOutcome : BaseClass
    {

        private string tableName = "MHACourtDateOutcome";
        private string primaryKeyName = "MHACourtDateOutcomeId";

        public MHACourtDateOutcome()
        {
            AuthenticateUser();
        }

        public MHACourtDateOutcome(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMHACourtDateOutcome(Guid ownerid, Guid mhalegalstatusid, Guid personid, DateTime courtappearancedatetime)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "mhalegalstatusid", mhalegalstatusid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "courtappearancedatetime", courtappearancedatetime);
            AddFieldToBusinessDataObject(dataObject, "courtdocumentsattached", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);



            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetMHACourtDateOutcomeByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetMHACourtDateOutcomeByID(Guid MHACourtDateOutcomeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "MHACourtDateOutcomeId", ConditionOperatorType.Equal, MHACourtDateOutcomeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMHACourtDateOutcome(Guid MHACourtDateOutcomeID)
        {
            this.DeleteRecord(tableName, MHACourtDateOutcomeID);
        }



    }
}
