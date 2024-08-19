using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ExtractName : BaseClass
    {

        public string TableName = "ExtractName";
        public string PrimaryKeyName = "ExtractNameId";

        public ExtractName()
        {
            AuthenticateUser();
        }

        public ExtractName(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateExtractName(Guid OwnerId, String Name, DateTime StartDate, bool IsUsedInSupplierPayments, bool IsUsedInCarerPayments, bool IsUsedInDebtors)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInSupplierPayments", IsUsedInSupplierPayments);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInCarerPayments", IsUsedInCarerPayments);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInDebtors", IsUsedInDebtors);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 1);

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
