using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingType : BaseClass
    {

        private string tableName = "CPBookingType";
        private string primaryKeyName = "CPBookingTypeId";

        public CPBookingType()
        {
            AuthenticateUser();
        }

        public CPBookingType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPBookingTypeByID(Guid CPBookingTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPBookingTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateBookingType(string name, int BookingTypeClassId, int? Duration, TimeSpan? DefaultStartTime, TimeSpan? DefaultEndTime,
            int WorkingContractedTime, bool? IsAbsence, int? capduration = null, DateTime? validfromdate = null, DateTime? validtodate = null, int? cpbookingchargetypeid = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingTypeClassId", BookingTypeClassId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Duration", Duration);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultStartTime", DefaultStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultEndTime", DefaultEndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingCountClassId", WorkingContractedTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsAbsence", IsAbsence);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingchargetypeid", cpbookingchargetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "capduration", capduration);

            AddFieldToBusinessDataObject(buisinessDataObject, "validfromdate", validfromdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "validtodate", validtodate);

            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingchargetypeid", cpbookingchargetypeid);

            AddFieldToBusinessDataObject(buisinessDataObject, "UnallocatedDisplayColourId", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "customallocateddisplaycolourid", "#458045");
            AddFieldToBusinessDataObject(buisinessDataObject, "AssumeStaffAvailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AnnualLeave", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "OpenEndedAllowed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateBookingType(string name, int BookingTypeClassId, int Duration, TimeSpan DefaultStartTime, TimeSpan DefaultEndTime,
            int WorkingContractedTime, bool IsAbsence, int? capduration = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingTypeClassId", BookingTypeClassId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Duration", Duration);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultStartTime", DefaultStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultEndTime", DefaultEndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingCountClassId", WorkingContractedTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsAbsence", IsAbsence);
            AddFieldToBusinessDataObject(buisinessDataObject, "capduration", capduration);

            AddFieldToBusinessDataObject(buisinessDataObject, "UnallocatedDisplayColourId", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "customallocateddisplaycolourid", "#458045");
            AddFieldToBusinessDataObject(buisinessDataObject, "AssumeStaffAvailable", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AnnualLeave", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "OpenEndedAllowed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateBookingType(string name, int BookingTypeClassId, int Duration, TimeSpan DefaultStartTime, TimeSpan DefaultEndTime,
            int WorkingContractedTime, bool IsAbsence, bool istraining, bool AssumeStaffAvailable, bool OpenEndedAllowed, bool AnnualLeave, bool isserviceusertraining)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingTypeClassId", BookingTypeClassId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Duration", Duration);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultStartTime", DefaultStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultEndTime", DefaultEndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingCountClassId", WorkingContractedTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "capduration", null);

            AddFieldToBusinessDataObject(buisinessDataObject, "UnallocatedDisplayColourId", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "customallocateddisplaycolourid", "#458045");

            AddFieldToBusinessDataObject(buisinessDataObject, "IsAbsence", IsAbsence);
            AddFieldToBusinessDataObject(buisinessDataObject, "istraining", istraining);
            AddFieldToBusinessDataObject(buisinessDataObject, "AssumeStaffAvailable", AssumeStaffAvailable);

            AddFieldToBusinessDataObject(buisinessDataObject, "OpenEndedAllowed", OpenEndedAllowed);
            AddFieldToBusinessDataObject(buisinessDataObject, "AnnualLeave", AnnualLeave);
            AddFieldToBusinessDataObject(buisinessDataObject, "isserviceusertraining", isserviceusertraining);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public void UpdateIsAbsence(Guid CPBookingTypeId, bool IsPersonAbsence)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CPBookingTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsPersonAbsence", DataType.Boolean, BusinessObjectFieldType.Unknown, false, IsPersonAbsence);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateValidTo(Guid CPBookingTypeId, DateTime? validtodate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CPBookingTypeId);
            if (validtodate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validtodate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, validtodate.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validtodate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateValidFromAndValidTo(Guid CPBookingTypeId, DateTime? validfromdate, DateTime? validtodate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CPBookingTypeId);

            if (validfromdate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validfromdate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, validfromdate.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validfromdate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, null);

            if (validtodate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validtodate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, validtodate.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validtodate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateValidFrom(Guid CPBookingTypeId, DateTime? validfromdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CPBookingTypeId);
            if (validfromdate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validfromdate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, validfromdate.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "validfromdate", DataType.Boolean, BusinessObjectFieldType.Unknown, false, null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWorkingContractedTime(Guid CPBookingTypeId, int? bookingcountclassid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CPBookingTypeId);
            if (bookingcountclassid.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "bookingcountclassid", DataType.Boolean, BusinessObjectFieldType.Unknown, false, bookingcountclassid.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "bookingcountclassid", DataType.Boolean, BusinessObjectFieldType.Unknown, false, null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCapDuration(Guid CPBookingTypeId, int? capduration)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CPBookingTypeId);
            if (capduration.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "capduration", DataType.Boolean, BusinessObjectFieldType.Unknown, false, capduration.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "capduration", DataType.Boolean, BusinessObjectFieldType.Unknown, false, null);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
