using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingScheduleDeletionReason : BaseClass
    {

        public string TableName = "CPBookingScheduleDeletionReason";
        public string PrimaryKeyName = "CPBookingScheduleDeletionReasonId";


        public CPBookingScheduleDeletionReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPBookingScheduleDeletionReason(Guid OwnerId, string name, int code, DateTime StartDate, DateTime? EndDate)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(businessDataObject, "Name", name);
            AddFieldToBusinessDataObject(businessDataObject, "code", code);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "EndDate", EndDate);

            AddFieldToBusinessDataObject(businessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetCPBookingScheduleDeletionReasonByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByCPBookingScheduleDeletionReasonId(Guid CPBookingScheduleDeletionReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPBookingScheduleDeletionReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllCPBookingScheduleDeletionReason()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
