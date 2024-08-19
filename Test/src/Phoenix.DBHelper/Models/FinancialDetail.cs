
using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FinancialDetail : BaseClass
    {
        public FinancialDetail()
        {
            AuthenticateUser();
        }

        public FinancialDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetFinancialDetailByID(Guid FinancialDetailId)
        {
            DataQuery query = this.GetDataQueryObject("FinancialDetail", false, "FinancialDetailId");
            this.BaseClassAddTableCondition(query, "FinancialDetailId", ConditionOperatorType.Equal, FinancialDetailId);
            this.AddReturnField(query, "FinancialDetail", "FinancialDetailid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialDetailId");
        }


        public void UpdateFinancialDetail(Guid FinancialDetailId, string Code, string GovCode)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("FinancialDetail", "FinancialDetailid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FinancialDetailid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinancialDetailId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Code", DataType.Date, BusinessObjectFieldType.Unknown, false, Code);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "GovCode", DataType.Decimal, BusinessObjectFieldType.Unknown, false, GovCode);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteFinancialDetail(Guid FinancialDetailID)
        {
            this.DeleteRecord("FinancialDetail", FinancialDetailID);
        }



    }
}
