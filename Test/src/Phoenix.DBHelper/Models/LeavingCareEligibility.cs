using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LeavingCareEligibility : BaseClass
    {

        private string TableName = "LeavingCareEligibility";
        private string PrimaryKeyName = "LeavingCareEligibilityId";

        public LeavingCareEligibility()
        {
            AuthenticateUser();
        }

        public LeavingCareEligibility(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLeavingCareEligibility(string Name, DateTime StartDate, Guid OwnerId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForUserSelection", true);
            AddFieldToBusinessDataObject(dataObject, "CanBeUpdated", true);
            AddFieldToBusinessDataObject(dataObject, "ValidForBannerIcon", true);
            AddFieldToBusinessDataObject(dataObject, "ValidForCalculation", true);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
