using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class IncomeSupportSetup : BaseClass
    {
        public string TableName = "IncomeSupportSetup";
        public string PrimaryKeyName = "IncomeSupportSetupId";

        public IncomeSupportSetup()
        {
            AuthenticateUser();
        }

        public IncomeSupportSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateIncomeSupportSetup(Guid ownerid, Guid incomesupporttypeid,
            DateTime startdate, DateTime? enddate, int agefrom, int ageto,
            decimal amount, decimal jointamount, decimal minimumguaranteeamount, decimal jointminimumguaranteeamount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "incomesupporttypeid", incomesupporttypeid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "agefrom", agefrom);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ageto", ageto);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "amount", amount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointamount", jointamount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "minimumguaranteeamount", minimumguaranteeamount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointminimumguaranteeamount", jointminimumguaranteeamount);

            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByID(Guid IncomeSupportSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, IncomeSupportSetupId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetIncomeSupportSetup(Guid IncomeSupportTypeId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "IncomeSupportTypeId", ConditionOperatorType.Equal, IncomeSupportTypeId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ChargingRuleID"></param>
        /// <param name="AuthorityID">1: IS ; 2: LA; 3: Both</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public List<Guid> GetIncomeSupportSetup(Guid IncomeSupportTypeId, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "IncomeSupportTypeId", ConditionOperatorType.Equal, IncomeSupportTypeId);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void UpdateIncomeSupportSetup(Guid IncomeSupportSetupId,
            decimal Amount, decimal JointAmount, DateTime EndDate, decimal MinimumGuaranteeAmount, decimal JointMinimumGuaranteeAmount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, IncomeSupportSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Amount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, Amount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "JointAmount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, JointAmount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "MinimumGuaranteeAmount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, MinimumGuaranteeAmount);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "JointMinimumGuaranteeAmount", DataType.Decimal, BusinessObjectFieldType.Unknown, false, JointMinimumGuaranteeAmount);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteIncomeSupportSetup(Guid IncomeSupportSetupID)
        {
            this.DeleteRecord(TableName, IncomeSupportSetupID);
        }



    }
}
