using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using CareDirector.Sdk.Enums;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPayrollBatch : BaseClass
    {
        private string TableName = "careproviderpayrollbatch";
        private string PrimaryKeyName = "careproviderpayrollbatchid";

        public CareProviderPayrollBatch()
        {
            AuthenticateUser();
        }

        public CareProviderPayrollBatch(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Dictionary<string, object> GetById(Guid CareProviderPayrollBatchId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderPayrollBatchId);

            foreach (string field in Fields)
                this.AddReturnField(query, TableName, field);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByProviderId(Guid providerid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            query.AddThisTableRelationship("CareProviderPayrollBatchProvider", "careproviderpayrollbatchid", "careproviderpayrollbatchid", JoinOperator.InnerJoin, "CareProviderPayrollBatchProvider");

            AddRelatedTableCondition(query, "CareProviderPayrollBatchProvider", "providerid", ConditionOperatorType.Equal, providerid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("payrollbatchnumber", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCareProviderPayrollBatch(Guid CareProviderPayrollBatchID)
        {
            this.DeleteRecord(TableName, CareProviderPayrollBatchID);
        }



    }
}
