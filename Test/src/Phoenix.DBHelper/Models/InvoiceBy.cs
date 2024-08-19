using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InvoiceBy : BaseClass
    {

        public string TableName = "InvoiceBy";
        public string PrimaryKeyName = "InvoiceById";

        public InvoiceBy()
        {
            AuthenticateUser();
        }

        public InvoiceBy(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateInvoiceBy(Guid OwnerId, string Name, DateTime StartDate, bool IsUsedInSupplierPayments, bool IsUsedInCarerPayments, bool IsUsedInDebtors, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "IsUsedInSupplierPayments", IsUsedInSupplierPayments);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInCarerPayments", IsUsedInCarerPayments);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInDebtors", IsUsedInDebtors);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }


    }
}
