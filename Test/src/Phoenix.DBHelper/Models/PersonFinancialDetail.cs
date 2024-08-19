
using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonFinancialDetail : BaseClass
    {
        public PersonFinancialDetail()
        {
            AuthenticateUser();
        }

        public PersonFinancialDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreatePersonFinancialDetail(Guid OwnerId, Guid PersonId, Guid FinancialDetailId, Guid FrequencyOfReceiptId, DateTime StartDate, DateTime EndDate, int FinancialDetailTypeId, decimal Amount, bool BeingReceived, bool ExcludeFromDWPCalculation, bool ShowReferenceInSchedule, bool Inactive)
        {

            BusinessData record = GetBusinessDataBaseObject("PersonFinancialDetail", "PersonFinancialDetailId");

            this.AddFieldToBusinessDataObject(record, "OwnerId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, OwnerId);
            this.AddFieldToBusinessDataObject(record, "PersonId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, PersonId);
            this.AddFieldToBusinessDataObject(record, "FinancialDetailId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FinancialDetailId);
            this.AddFieldToBusinessDataObject(record, "FrequencyOfReceiptId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FrequencyOfReceiptId);

            this.AddFieldToBusinessDataObject(record, "StartDate", DataType.Date, BusinessObjectFieldType.Unknown, false, StartDate);
            this.AddFieldToBusinessDataObject(record, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);

            this.AddFieldToBusinessDataObject(record, "FinancialDetailTypeId", DataType.Integer, BusinessObjectFieldType.Unknown, false, FinancialDetailTypeId);

            this.AddFieldToBusinessDataObject(record, "Amount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, Amount);

            this.AddFieldToBusinessDataObject(record, "BeingReceived", DataType.Boolean, BusinessObjectFieldType.Unknown, false, BeingReceived);
            this.AddFieldToBusinessDataObject(record, "ExcludeFromDWPCalculation", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ExcludeFromDWPCalculation);
            this.AddFieldToBusinessDataObject(record, "ShowReferenceInSchedule", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ShowReferenceInSchedule);
            this.AddFieldToBusinessDataObject(record, "Inactive", DataType.Boolean, BusinessObjectFieldType.Unknown, false, Inactive);


            return this.CreateRecord(record);
        }



        public List<Guid> GetPersonFinancialDetailByID(Guid PersonFinancialDetailId)
        {
            DataQuery query = this.GetDataQueryObject("PersonFinancialDetail", false, "PersonFinancialDetailId");
            this.BaseClassAddTableCondition(query, "PersonFinancialDetailId", ConditionOperatorType.Equal, PersonFinancialDetailId);
            this.AddReturnField(query, "PersonFinancialDetail", "PersonFinancialDetailid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "PersonFinancialDetailId");
        }


        public List<Guid> GetPersonFinancialDetail(Guid PersonId, Guid FinancialDetailId, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject("PersonFinancialDetail", false, "PersonFinancialDetailId");

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);
            this.BaseClassAddTableCondition(query, "FinancialDetailId", ConditionOperatorType.Equal, FinancialDetailId);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, "PersonFinancialDetail", "PersonFinancialDetailid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "PersonFinancialDetailId");
        }


        public void UpdatePersonFinancialDetail(Guid PersonFinancialDetailId, Guid FrequencyOfReceiptId, decimal Amount, DateTime StartDate, DateTime EndDate,
            decimal? GrossValue, decimal? OutstandingLoan, int? PercentageOwnership, Guid? PropertyDisregardTypeId, bool ExcludeFromDWPCalculation)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("PersonFinancialDetail", "PersonFinancialDetailid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonFinancialDetailid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, PersonFinancialDetailId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "FrequencyOfReceiptId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, FrequencyOfReceiptId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Amount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, Amount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", DataType.Date, BusinessObjectFieldType.Unknown, false, StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "GrossValue", DataType.Decimal, BusinessObjectFieldType.Unknown, false, GrossValue);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OutstandingLoan", DataType.Decimal, BusinessObjectFieldType.Unknown, false, OutstandingLoan);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PercentageOwnership", DataType.Integer, BusinessObjectFieldType.Unknown, false, PercentageOwnership);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PropertyDisregardTypeId", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, PropertyDisregardTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ExcludeFromDWPCalculation", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ExcludeFromDWPCalculation);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ApplicationDate", DataType.Date, BusinessObjectFieldType.Unknown, false, null);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", DataType.Boolean, BusinessObjectFieldType.Unknown, false, false);


            this.UpdateRecord(buisinessDataObject);
        }


        public void DeletePersonFinancialDetail(Guid PersonFinancialDetailID)
        {
            this.DeleteRecord("PersonFinancialDetail", PersonFinancialDetailID);
        }



    }
}
