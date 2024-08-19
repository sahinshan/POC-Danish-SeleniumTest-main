using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChargeforServicesSetup : BaseClass
    {
        public string TableName = "ChargeforServicesSetup";
        public string PrimaryKeyName = "ChargeforServicesSetupId";

        public ChargeforServicesSetup()
        {
            AuthenticateUser();
        }

        public ChargeforServicesSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateChargeforServicesSetup(Guid ownerid, Guid chargingruletypeid, Guid serviceelement1id, Guid serviceelement2id, Guid? financeclientcategoryid, Guid rateunitid, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargingruletypeid", chargingruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement2id", serviceelement2id);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "financeclientcategoryid", financeclientcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargeatid", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "percentofactualchargeatrate", 100);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", 0);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", 0);


            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetChargeforServicesSetupByID(Guid ChargeforServicesSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ChargeforServicesSetupId);
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
        public List<Guid> GetChargeforServicesSetup(Guid ChargingRuleTypeId, Guid ServiceElement1Id, Guid ServiceElement2Id, Guid FinanceClientCategoryId, Guid RateUnitId, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleTypeId);
            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "ServiceElement2Id", ConditionOperatorType.Equal, ServiceElement2Id);
            this.BaseClassAddTableCondition(query, "FinanceClientCategoryId", ConditionOperatorType.Equal, FinanceClientCategoryId);
            this.BaseClassAddTableCondition(query, "RateUnitId", ConditionOperatorType.Equal, RateUnitId);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetChargeforServicesSetup(Guid ChargingRuleTypeId, Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleTypeId);
            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "ServiceElement2Id", ConditionOperatorType.Equal, ServiceElement2Id);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void UpdateChargeforServicesSetup(Guid ChargeforServicesSetupId, DateTime EndDate, int ChargeAtId, int PercentofActualChargeatRate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ChargeforServicesSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ChargeAtId", DataType.Integer, BusinessObjectFieldType.Unknown, false, ChargeAtId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PercentofActualChargeatRate", DataType.Integer, BusinessObjectFieldType.Unknown, false, PercentofActualChargeatRate);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteChargeforServicesSetup(Guid ChargeforServicesSetupID)
        {
            this.DeleteRecord(TableName, ChargeforServicesSetupID);
        }



    }
}
