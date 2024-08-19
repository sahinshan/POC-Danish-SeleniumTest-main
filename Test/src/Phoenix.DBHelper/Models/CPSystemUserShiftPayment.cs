using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using CareDirector.Sdk.Enums;

namespace Phoenix.DBHelper.Models
{
    public class CPSystemUserShiftPayment : BaseClass
    {
        private string TableName = "cpsystemusershiftpayment";
        private string PrimaryKeyName = "cpsystemusershiftpaymentid";

        public CPSystemUserShiftPayment()
        {
            AuthenticateUser();
        }

        public CPSystemUserShiftPayment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Dictionary<string, object> GetById(Guid CPSystemUserShiftPaymentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPSystemUserShiftPaymentId);

            foreach (string field in Fields)
                this.AddReturnField(query, TableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByPayrollBatchId(Guid careproviderpayrollbatchid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            this.BaseClassAddTableCondition(query, "careproviderpayrollbatchid", ConditionOperatorType.Equal, careproviderpayrollbatchid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCPSystemUserShiftPayment(Guid CPSystemUserShiftPaymentID)
        {
            this.DeleteRecord(TableName, CPSystemUserShiftPaymentID);
        }



    }
}
