using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ScheduleSetup : BaseClass
    {

        public string TableName = "ScheduleSetup";
        public string PrimaryKeyName = "ScheduleSetupId";

        public ScheduleSetup()
        {
            AuthenticateUser();
        }

        public ScheduleSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetScheduleSetupByID(Guid ScheduleSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ScheduleSetupId);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateScheduleSetup(Guid ownerid,
            Guid scheduletypeid, Guid chargingruletypeid, DateTime startdate, int percentageallocation)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "scheduletypeid", scheduletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargingruletypeid", chargingruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "percentageallocation", percentageallocation);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "assessmentcategoryid", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "calculatesavingscredit", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "applychargesforwholeweek", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rounddowncharge", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "showcostonschedule", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "adjusteddays", -1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contributionpersoncharge", true);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contributionnonpersoncharge", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "suspendchargeincrease", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdayid", 1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateScheduleSetup(Guid ownerid, Guid chargingruletypeid, Guid scheduletypeid,
            DateTime startdate, DateTime? enddate, int assessmentcategoryid, bool rounddowncharge, bool calculatesavingscredit, int adjusteddays, int percentageallocation, bool showcostonschedule,
            bool contributionpersoncharge, bool contributionnonpersoncharge,
            bool suspendchargeincrease)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //General
            this.AddFieldToBusinessDataObject(buisinessDataObject, "chargingruletypeid", chargingruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "scheduletypeid", scheduletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "assessmentcategoryid", assessmentcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rounddowncharge", rounddowncharge);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "calculatesavingscredit", calculatesavingscredit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "adjusteddays", adjusteddays);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "percentageallocation", percentageallocation);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "showcostonschedule", showcostonschedule);

            //Please specify if following records are required before authorisation
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contributionpersoncharge", contributionpersoncharge);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contributionnonpersoncharge", contributionnonpersoncharge);

            //Suspend Charge Increases
            this.AddFieldToBusinessDataObject(buisinessDataObject, "suspendchargeincrease", suspendchargeincrease);

            //Other
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetScheduleSetup(Guid ChargingRuleID, Guid ScheduleTypeID)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleID);
            this.BaseClassAddTableCondition(query, "ScheduleTypeId", ConditionOperatorType.Equal, ScheduleTypeID);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetScheduleSetup(Guid ChargingRuleID, Guid ScheduleTypeID, DateTime StartDate, DateTime EndDate)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ChargingRuleTypeId", ConditionOperatorType.Equal, ChargingRuleID);
            this.BaseClassAddTableCondition(query, "ScheduleTypeId", ConditionOperatorType.Equal, ScheduleTypeID);
            this.BaseClassAddTableCondition(query, "StartDate", ConditionOperatorType.Equal, StartDate);
            this.BaseClassAddTableCondition(query, "EndDate", ConditionOperatorType.Equal, EndDate);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateScheduleSetup(Guid ScheduleSetupId, DateTime EndDate, bool RoundDownCharge, bool CalculateSavingsCredit, int AdjustedDays)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, ScheduleSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", DataType.Date, BusinessObjectFieldType.Unknown, false, EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RoundDownCharge", DataType.Boolean, BusinessObjectFieldType.Unknown, false, RoundDownCharge);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CalculateSavingsCredit", DataType.Boolean, BusinessObjectFieldType.Unknown, false, CalculateSavingsCredit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AdjustedDays", DataType.Integer, BusinessObjectFieldType.Unknown, false, AdjustedDays);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteScheduleSetup(Guid ScheduleSetupID)
        {
            this.DeleteRecord(TableName, ScheduleSetupID);
        }

    }
}
