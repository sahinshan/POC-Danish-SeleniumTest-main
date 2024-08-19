using CareDirector.Sdk.Enums;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPSchedulingSetup : BaseClass
    {

        public string TableName = "cpschedulingsetup";
        public string PrimaryKeyName = "cpschedulingsetupid";


        public CPSchedulingSetup()
        {
            AuthenticateUser();
        }

        public CPSchedulingSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public void UpdateCPScheduleSetup(Guid cpschedulingsetupid, Guid defaultbookingtypeforpersonabsenceid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "defaultbookingtypeforpersonabsenceid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, defaultbookingtypeforpersonabsenceid);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCheckStaffAvailability(Guid cpschedulingsetupid, int checkstaffavailabilityid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "checkstaffavailabilityid", checkstaffavailabilityid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDefaultBookingStaffRoleType(Guid cpschedulingsetupid, bool defaultbookingtypeperstaffroletype)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "defaultbookingtypeperstaffroletype", defaultbookingtypeperstaffroletype);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCPScheduleSetupModeOfCareDelivery(Guid cpschedulingsetupid, int modeofcaredeliveryid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "modeofcaredeliveryid", DataType.Integer, BusinessObjectFieldType.Unknown, false, modeofcaredeliveryid);


            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetCPSchedulingSetupByPlannedBookingPrecision(int PlannedBookingPrecision)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "plannedbookingprecision", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PlannedBookingPrecision);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAllActiveRecords()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, false);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateAutoRefresh(Guid cpschedulingsetupid, bool autorefresh)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "autorefresh", DataType.Boolean, BusinessObjectFieldType.Unknown, false, autorefresh);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAutoRefreshInterval(Guid cpschedulingsetupid, int? autorefreshinterval)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "autorefreshinterval", DataType.Integer, BusinessObjectFieldType.Unknown, false, autorefreshinterval);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDeleteReasonRequiredSchedule(Guid cpSchedulingSetupId, bool DeleteReasonRequiredSchedule)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpSchedulingSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "deletereasonrequiredschedule", DeleteReasonRequiredSchedule);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDeleteReasonRequiredDiary(Guid cpSchedulingSetupId, bool DeleteReasonRequiredDiary)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpSchedulingSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "deletereasonrequireddiary", DeleteReasonRequiredDiary);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateUpdateBookingEndDayDateTime(Guid cpschedulingsetupid, bool bookingstartchangeaction)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "bookingstartchangeaction", bookingstartchangeaction);

            this.UpdateRecord(buisinessDataObject);
        }

        //update contracthoursvalidationcontractedid field
        public void UpdateContractedField(Guid cpschedulingsetupid, int contracthoursvalidationcontractedid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contracthoursvalidationcontractedid", contracthoursvalidationcontractedid);

            this.UpdateRecord(buisinessDataObject);
        }

        //method to Update Salaried Field with the field being contracthoursvalidationsalariedid
        public void UpdateSalariedField(Guid cpschedulingsetupid, int contracthoursvalidationsalariedid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contracthoursvalidationsalariedid", contracthoursvalidationsalariedid);

            this.UpdateRecord(buisinessDataObject);
        }

        //method to Update Hourly Field with the field being contracthoursvalidationsalariedid
        public void UpdateHourlyField(Guid cpschedulingsetupid, int contracthoursvalidationhourlyid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contracthoursvalidationhourlyid", contracthoursvalidationhourlyid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateUseBookingTypeClashActions(Guid cpschedulingsetupid, bool usebookingtypeclashactions, int? doublebookingactionid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpschedulingsetupid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "usebookingtypeclashactions", usebookingtypeclashactions);

            if (doublebookingactionid.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "doublebookingactionid", doublebookingactionid.Value);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
