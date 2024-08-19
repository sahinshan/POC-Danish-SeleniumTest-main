using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DisabilityType : BaseClass
    {

        private string TableName = "DisabilityType";
        private string PrimaryKeyName = "DisabilityTypeId";

        public DisabilityType()
        {
            AuthenticateUser();
        }

        public DisabilityType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDisabilityType(Guid OwnerId, string Name, DateTime StartDate, bool ApplicableForMHSDS = false, bool IsNoneKnown = false, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(dataObject, "ApplicableForMHSDS", ApplicableForMHSDS);
            AddFieldToBusinessDataObject(dataObject, "IsNoneKnown", IsNoneKnown);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDisabilityTypeName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByDisabilityTypeId(Guid DisabilityTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, DisabilityTypeId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteDisabilityTypeRecord(Guid DisabilityTypeId)
        {
            this.DeleteRecord(TableName, DisabilityTypeId);
        }

    }
}