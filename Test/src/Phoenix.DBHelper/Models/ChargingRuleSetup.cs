using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChargingRuleSetup : BaseClass
    {

        public string TableName = "ChargingRuleSetup";
        public string PrimaryKeyName = "ChargingRuleSetupId";

        public ChargingRuleSetup()
        {
            AuthenticateUser();
        }

        public ChargingRuleSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateChargingRuleSetup(Guid ownerid, Guid chargingruletypeid, int authorityid, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargingruletypeid", chargingruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "authorityid", authorityid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "agefrom", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ageto", 80);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "singleminimumcapitallimit", 100);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "singlemaximumcapitallimit", 50000);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointminimumcapitallimit", 300);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointmaximumcapitallimit", 100000);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargerate", 100);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "foreach", 50);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "savingscreditapply", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateChargingRuleSetup(Guid ownerid, Guid chargingruletypeid, int authorityid, DateTime startdate,
            int agefrom, int ageto,
            decimal singleminimumcapitallimit, decimal singlemaximumcapitallimit, decimal jointminimumcapitallimit, decimal jointmaximumcapitallimit,
            int chargerate, int foreachvalue)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargingruletypeid", chargingruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "authorityid", authorityid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "agefrom", agefrom);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ageto", ageto);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "singleminimumcapitallimit", singleminimumcapitallimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "singlemaximumcapitallimit", singlemaximumcapitallimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointminimumcapitallimit", jointminimumcapitallimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "jointmaximumcapitallimit", jointmaximumcapitallimit);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargerate", chargerate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "foreach", foreachvalue);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "savingscreditapply", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetChargingRuleSetupByID(Guid ChargingRuleSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ChargingRuleSetupId);
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
        public List<Guid> GetChargingRuleSetup(Guid ChargingRuleID, int AuthorityID, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleID);
            this.BaseClassAddTableCondition(query, "AuthorityID", ConditionOperatorType.Equal, AuthorityID);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetChargingRuleSetup(Guid ChargingRuleID, int AuthorityID)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleID);
            this.BaseClassAddTableCondition(query, "AuthorityID", ConditionOperatorType.Equal, AuthorityID);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void UpdateChargingRuleSetup(Guid ChargingRuleSetupId, DateTime EndDate, int SingleMinimumCapitalLimit, int SingleMaximumCapitalLimit, int JointMinimumCapitalLimit, int JointMaximumCapitalLimit, int ChargeRate, int ForEach)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ChargingRuleSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SingleMinimumCapitalLimit", DataType.Integer, BusinessObjectFieldType.Unknown, false, SingleMinimumCapitalLimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SingleMaximumCapitalLimit", DataType.Integer, BusinessObjectFieldType.Unknown, false, SingleMaximumCapitalLimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "JointMinimumCapitalLimit", DataType.Integer, BusinessObjectFieldType.Unknown, false, JointMinimumCapitalLimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "JointMaximumCapitalLimit", DataType.Integer, BusinessObjectFieldType.Unknown, false, JointMaximumCapitalLimit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ChargeRate", DataType.Integer, BusinessObjectFieldType.Unknown, false, ChargeRate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ForEach", DataType.Integer, BusinessObjectFieldType.Unknown, false, ForEach);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteChargingRuleSetup(Guid ChargingRuleSetupID)
        {
            this.DeleteRecord(TableName, ChargingRuleSetupID);
        }



    }
}
