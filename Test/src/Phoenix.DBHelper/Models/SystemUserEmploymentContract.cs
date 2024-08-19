using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserEmploymentContract : BaseClass
    {

        public string TableName = "SystemUserEmploymentContract";
        public string PrimaryKeyName = "SystemUserEmploymentContractId";


        public SystemUserEmploymentContract()
        {
            AuthenticateUser();
        }

        public SystemUserEmploymentContract(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetSystemUserEmploymentContractByName(string name)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSystemUserEmploymentContractByID(Guid SystemUserEmploymentContractId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserEmploymentContractId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime? startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, decimal? ContractHoursPerWeek = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);

            if (startdate.HasValue)
                AddFieldToBusinessDataObject(dataObject, "startdate", startdate.Value);

            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "ContractHoursPerWeek", ContractHoursPerWeek);
            AddFieldToBusinessDataObject(dataObject, "description", "Description");
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", "1");
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime? startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, DateTime? endDate, string description = "Test")
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", endDate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", "1");
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            //AddFieldToBusinessDataObject(dataObject, "inactive", inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime? startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, DateTime? endDate, Guid staffreviewrequirementsids)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", endDate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "staffreviewrequirementsids", staffreviewrequirementsids);
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", "1");
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            //AddFieldToBusinessDataObject(dataObject, "inactive", inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, int? ContractHoursPerWeek, string description = "Test", string fixedworkingpatterncycle = "1")
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            if (ContractHoursPerWeek.HasValue)
                AddFieldToBusinessDataObject(dataObject, "contracthoursperweek", ContractHoursPerWeek.Value);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", fixedworkingpatterncycle);
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, int ContractHoursPerWeek, List<Guid> BookingTypes, string fixedworkingpatterncycle = "1")
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "contracthoursperweek", ContractHoursPerWeek);
            AddFieldToBusinessDataObject(dataObject, "description", "...");
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", fixedworkingpatterncycle);
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            dataObject.MultiSelectBusinessObjectFields["bookingtypeid"] = new MultiSelectBusinessObjectDataCollection();
            foreach (Guid bookingTypeId in BookingTypes)
            {
                dataObject.MultiSelectBusinessObjectFields["bookingtypeid"].Add(new MultiSelectBusinessObjectData
                {
                    ReferenceId = bookingTypeId,
                    ReferenceIdTableName = "cpbookingtype",
                    ReferenceName = "bookingtype",
                });
            }

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime? startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, int ContractHoursPerWeek, List<Guid> BookingTypes, List<Guid> workat, string fixedworkingpatterncycle = "1")
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "contracthoursperweek", ContractHoursPerWeek);
            AddFieldToBusinessDataObject(dataObject, "description", "...");
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", fixedworkingpatterncycle);
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            dataObject.MultiSelectBusinessObjectFields["bookingtypeid"] = new MultiSelectBusinessObjectDataCollection();
            foreach (Guid bookingTypeId in BookingTypes)
            {
                dataObject.MultiSelectBusinessObjectFields["bookingtypeid"].Add(new MultiSelectBusinessObjectData
                {
                    ReferenceId = bookingTypeId,
                    ReferenceIdTableName = "cpbookingtype",
                    ReferenceName = "bookingtype",
                });
            }

            dataObject.MultiSelectBusinessObjectFields["workat"] = new MultiSelectBusinessObjectDataCollection();
            foreach (Guid workAtTeamId in workat)
            {
                dataObject.MultiSelectBusinessObjectFields["workat"].Add(new MultiSelectBusinessObjectData
                {
                    ReferenceId = workAtTeamId,
                    ReferenceIdTableName = "team",
                    ReferenceName = "team",
                });
            }

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, Guid availablebookingtypes, string description = "Test")
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "availablebookingtypes", availablebookingtypes);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", "1");
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateSystemUserEmploymentContract(Guid systemuserid, DateTime? startdate, Guid careproviderstaffroletypeid, Guid ownerid, Guid employmentcontracttypeid, DateTime? endDate, int? statusId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", endDate);
            AddFieldToBusinessDataObject(dataObject, "careproviderstaffroletypeid", careproviderstaffroletypeid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "employmentcontracttypeid", employmentcontracttypeid);
            AddFieldToBusinessDataObject(dataObject, "workyearstart", "01/01");
            AddFieldToBusinessDataObject(dataObject, "workyearend", "31/12");
            AddFieldToBusinessDataObject(dataObject, "description", "...");
            AddFieldToBusinessDataObject(dataObject, "fixedworkingpatterncycle", "1");
            AddFieldToBusinessDataObject(dataObject, "isentitledtoannualleaveaccrual", false);

            AddFieldToBusinessDataObject(dataObject, "StatusId", statusId);

            return this.CreateRecord(dataObject);
        }

        public void UpdateStartDate(Guid SystemUserEmploymentContractId, DateTime? startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePayrollId(Guid SystemUserEmploymentContractId, string payroll)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "payroll", payroll);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEndDate(Guid SystemUserEmploymentContractId, DateTime? enddate, Guid? contractendreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "contractendreasonid", contractendreasonid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFixedWorkingPatternCycle(Guid SystemUserEmploymentContractId, int fixedworkingpatterncycle)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "fixedworkingpatterncycle", fixedworkingpatterncycle);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteSystemUserEmploymentContract(Guid SystemUserEmploymentContractId)
        {
            this.DeleteRecord(TableName, SystemUserEmploymentContractId);
        }

        public List<Guid> GetBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetContractsForUser(Guid SystemUserId, Guid careproviderstaffroletypeid, Guid employmentcontracttypeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);
            this.BaseClassAddTableCondition(query, "careproviderstaffroletypeid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, careproviderstaffroletypeid);
            this.BaseClassAddTableCondition(query, "employmentcontracttypeid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, employmentcontracttypeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid systemuseremploymentcontractid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, systemuseremploymentcontractid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateCanWorksAt(Guid SystemUserEmploymentContractId, List<Guid> workat)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);


            buisinessDataObject.MultiSelectBusinessObjectFields["workat"] = new MultiSelectBusinessObjectDataCollection();
            foreach (Guid workAtTeamId in workat)
            {
                buisinessDataObject.MultiSelectBusinessObjectFields["workat"].Add(new MultiSelectBusinessObjectData
                {
                    ReferenceId = workAtTeamId,
                    ReferenceIdTableName = "team",
                    ReferenceName = "team",
                });
            }

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCanWorksAt(Guid SystemUserEmploymentContractId, List<Guid> workat)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);

            buisinessDataObject.MultiSelectBusinessObjectFields["workat"] = new MultiSelectBusinessObjectDataCollection();
            foreach (Guid workAtTeamId in workat)
            {
                buisinessDataObject.MultiSelectBusinessObjectFields["workat"].Remove(new MultiSelectBusinessObjectData
                {
                    ReferenceId = workAtTeamId,
                    ReferenceIdTableName = "team",
                    ReferenceName = "team",
                });
            }

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOwnerId(Guid SystemUserEmploymentContractId, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            this.UpdateRecord(buisinessDataObject);
        }

        //Method to Update ContractHoursPerWeek
        public void UpdateContractHoursPerWeek(Guid SystemUserEmploymentContractId, decimal? ContractHoursPerWeek)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "contracthoursperweek", ContractHoursPerWeek);

            this.UpdateRecord(buisinessDataObject);
        }

        //Method to update Annual Leave section fields
        public void UpdateAnnualLeaveFields(Guid SystemUserEmploymentContractId, bool isentitledtoannualleaveaccrual, Guid? defaultholidayyearid,
            bool? isnonstandardannualleaveentitlement, int? entitlementunitid, decimal? accruedannualleave,
            decimal? standardworkingweekhours)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserEmploymentContractId);
            AddFieldToBusinessDataObject(buisinessDataObject, "isentitledtoannualleaveaccrual", isentitledtoannualleaveaccrual);
            if (defaultholidayyearid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "defaultholidayyearid", defaultholidayyearid);
            if (isnonstandardannualleaveentitlement.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "isnonstandardannualleaveentitlement", isnonstandardannualleaveentitlement);
            if (entitlementunitid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "entitlementunitid", entitlementunitid);
            if (accruedannualleave.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "accruedannualleave", accruedannualleave);
            if (standardworkingweekhours.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "standardworkingweekhours", standardworkingweekhours);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
