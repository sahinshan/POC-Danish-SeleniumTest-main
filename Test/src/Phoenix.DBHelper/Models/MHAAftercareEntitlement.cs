using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MHAAftercareEntitlement : BaseClass
    {

        private string tableName = "MHAAftercareEntitlement";
        private string primaryKeyName = "MHAAftercareEntitlementId";

        public MHAAftercareEntitlement()
        {
            AuthenticateUser();
        }

        public MHAAftercareEntitlement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMHAAftercareEntitlement(Guid ownerid, Guid? personid, Guid? relatedmhalegalstatusid,
            Guid? responsibleorganisationid, bool? previoussectionrecorded, DateTime? eligibilitystarteddatetime,
            DateTime? eligibilityendeddatetime, bool? endentitlement)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "relatedmhalegalstatusid", relatedmhalegalstatusid);
            AddFieldToBusinessDataObject(dataObject, "responsibleorganisationid", responsibleorganisationid);
            AddFieldToBusinessDataObject(dataObject, "previoussectionrecorded", previoussectionrecorded);
            AddFieldToBusinessDataObject(dataObject, "eligibilitystarteddatetime", eligibilitystarteddatetime);
            AddFieldToBusinessDataObject(dataObject, "eligibilityendeddatetime", eligibilityendeddatetime);
            AddFieldToBusinessDataObject(dataObject, "endentitlement", endentitlement);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetMHAAftercareEntitlementByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetMHAAftercareEntitlementByRelatedMHALegalStatusID(Guid RelatedMHALegalStatusID)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "relatedmhalegalstatusid", ConditionOperatorType.Equal, RelatedMHALegalStatusID);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid MHAAftercareEntitlementId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "MHAAftercareEntitlementId", ConditionOperatorType.Equal, MHAAftercareEntitlementId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteMHAAftercareEntitlement(Guid MHAAftercareEntitlementId)
        {
            this.DeleteRecord(tableName, MHAAftercareEntitlementId);
        }

    }
}
