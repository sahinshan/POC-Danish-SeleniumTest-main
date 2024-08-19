using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingScheduleStaff : BaseClass
    {

        public string TableName = "CPBookingScheduleStaff";
        public string PrimaryKeyName = "CPBookingScheduleStaffId";


        public CPBookingScheduleStaff()
        {
            AuthenticateUser();
        }

        public CPBookingScheduleStaff(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBySystemUserId(Guid SystemUserId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBySystemUserEmploymentContractId(Guid SystemUserEmploymentContractId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserEmploymentContractId", ConditionOperatorType.Equal, SystemUserEmploymentContractId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCPBookingScheduleId(Guid CPBookingScheduleId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBookingScheduleId", ConditionOperatorType.Equal, CPBookingScheduleId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCPBookingScheduleIdAndEmploymentContractId(Guid CPBookingScheduleId, Guid? SystemUserEmploymentContractId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBookingScheduleId", ConditionOperatorType.Equal, CPBookingScheduleId);
            this.BaseClassAddTableCondition(query, "SystemUserEmploymentContractId", ConditionOperatorType.Equal, SystemUserEmploymentContractId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetById(Guid CPBookingScheduleStaffId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPBookingScheduleStaffId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPBookingScheduleStaff(Guid OwnerId, Guid CPBookingScheduleId, Guid? SystemUserEmploymentContractId, Guid? SystemUserId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "CPBookingScheduleId", CPBookingScheduleId);

            if (SystemUserEmploymentContractId.HasValue)
                AddFieldToBusinessDataObject(dataObject, "SystemUserEmploymentContractId", SystemUserEmploymentContractId.Value);

            if (SystemUserId.HasValue)
                AddFieldToBusinessDataObject(dataObject, "SystemUserId", SystemUserId.Value);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }

        public void UpdateSystemUserEmploymentContract(Guid CPBookingScheduleStaffId, Guid SystemUserEmploymentContractId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CPBookingScheduleStaffId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserEmploymentContractId", SystemUserEmploymentContractId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCPBookingScheduleStaff(Guid CPBookingScheduleStaffId)
        {
            this.DeleteRecord(TableName, CPBookingScheduleStaffId);
        }

    }
}
