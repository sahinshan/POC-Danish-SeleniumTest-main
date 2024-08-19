using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PaymentTypeCode : BaseClass
    {
        public string TableName = "PaymentTypeCode";
        public string PrimaryKeyName = "PaymentTypeCodeId";

        public PaymentTypeCode()
        {
            AuthenticateUser();
        }

        public PaymentTypeCode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreatePaymentTypeCode(string Name, DateTime StartDate, Guid OwnerId, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

    }
}
