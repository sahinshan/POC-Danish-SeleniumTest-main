
using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinancialDetailDisregard : BaseClass
    {
        public FinancialDetailDisregard()
        {
            AuthenticateUser();
        }

        public FinancialDetailDisregard(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetFinancialDetailDisregardByID(Guid FinancialDetailDisregardId)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailDisregard", false, "FinancialDetailDisregardId");
            this.BaseClassAddTableCondition(query, "FinancialDetailDisregardId", ConditionOperatorType.Equal, FinancialDetailDisregardId);
            this.AddReturnField(query, "FinancialDetailDisregard", "FinancialDetailDisregardid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailDisregardId");
        }


        public List<Guid> GetFinancialDetailDisregard(Guid FinancialDetailId, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetailDisregard", false, "FinancialDetailDisregardId");

            this.BaseClassAddTableCondition(query, "FinancialDetailId", ConditionOperatorType.Equal, FinancialDetailId);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, "FinancialDetailDisregard", "FinancialDetailDisregardid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailDisregardId");
        }


        public void UpdateFinancialDetailDisregard(Guid FinancialDetailDisregardId, DateTime EndDate, int AuthorityID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinancialDetailDisregard", "FinancialDetailDisregardid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinancialDetailDisregardid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinancialDetailDisregardId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AuthorityID", DataType.Decimal, BusinessObjectFieldType.Unknown, false, AuthorityID);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteFinancialDetailDisregard(Guid FinancialDetailDisregardID)
        {
            this.DeleteRecord("FinancialDetailDisregard", FinancialDetailDisregardID);
        }



    }
}
