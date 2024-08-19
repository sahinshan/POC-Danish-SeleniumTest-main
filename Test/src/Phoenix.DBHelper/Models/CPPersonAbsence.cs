using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonAbsence : BaseClass
    {

        private string tableName = "cppersonabsence";
        private string primaryKeyName = "cppersonabsenceid";

        public CPPersonAbsence()
        {
            AuthenticateUser();
        }

        public CPPersonAbsence(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPPersonAbsenceId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnFields(query, tableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPPersonAbsenceId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public Guid CreateCPPersonAbsence(Guid ownerid, Guid personid, DateTime actualstartdateandtime, List<Guid> careproviderpersoncontracts,
            Guid absencereasonid, DateTime notifieddateandtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdateandtime", actualstartdateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "absencereasonid", absencereasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "notifieddateandtime", notifieddateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "deallocatestafffromscheduledbookings", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"] = new MultiSelectBusinessObjectDataCollection();

            if (careproviderpersoncontracts != null && careproviderpersoncontracts.Count > 0)
            {
                foreach (Guid careproviderpersoncontractId in careproviderpersoncontracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careproviderpersoncontractId,
                        ReferenceIdTableName = "careproviderpersoncontract",
                        ReferenceName = "Test"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"].Add(dataRecord);
                }
            }

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCPPersonAbsence(Guid ownerid, Guid personid, DateTime plannedstartdatetime, DateTime? actualstartdateandtime, List<Guid> careproviderpersoncontracts,
            Guid absencereasonid, DateTime notifieddateandtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdatetime", plannedstartdatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdateandtime", actualstartdateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "absencereasonid", absencereasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "notifieddateandtime", notifieddateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "deallocatestafffromscheduledbookings", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"] = new MultiSelectBusinessObjectDataCollection();

            if (careproviderpersoncontracts != null && careproviderpersoncontracts.Count > 0)
            {
                foreach (Guid careproviderpersoncontractId in careproviderpersoncontracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careproviderpersoncontractId,
                        ReferenceIdTableName = "careproviderpersoncontract",
                        ReferenceName = "Test"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"].Add(dataRecord);
                }
            }

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCPPersonAbsence(Guid ownerid, Guid personid, DateTime? plannedstartdatetime, DateTime? actualstartdateandtime, DateTime? actualenddateandtime, DateTime? plannedenddateandtime, List<Guid> careproviderpersoncontracts,
           Guid absencereasonid, DateTime notifieddateandtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdatetime", plannedstartdatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdateandtime", actualstartdateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualenddateandtime", actualenddateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddateandtime", plannedenddateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "absencereasonid", absencereasonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "notifieddateandtime", notifieddateandtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "deallocatestafffromscheduledbookings", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"] = new MultiSelectBusinessObjectDataCollection();

            if (careproviderpersoncontracts != null && careproviderpersoncontracts.Count > 0)
            {
                foreach (Guid careproviderpersoncontractId in careproviderpersoncontracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careproviderpersoncontractId,
                        ReferenceIdTableName = "careproviderpersoncontract",
                        ReferenceName = "Test"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"].Add(dataRecord);
                }
            }

            return CreateRecord(buisinessDataObject);
        }

        public void UpdatedeAllocateStaffFromScheduledBookings(Guid cppersonabsenceid, bool deallocatestafffromscheduledbookings)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, cppersonabsenceid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "deallocatestafffromscheduledbookings", deallocatestafffromscheduledbookings);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatedePlannedstartdateNTime(Guid cppersonabsenceid, DateTime plannedstartdatetime, List<Guid> careproviderpersoncontracts)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, cppersonabsenceid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdatetime", plannedstartdatetime);
            buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"] = new MultiSelectBusinessObjectDataCollection();

            if (careproviderpersoncontracts != null && careproviderpersoncontracts.Count > 0)
            {
                foreach (Guid careproviderpersoncontractId in careproviderpersoncontracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careproviderpersoncontractId,
                        ReferenceIdTableName = "careproviderpersoncontract",
                        ReferenceName = "Test"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"].Add(dataRecord);
                }
            }


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatedePlannedenddateNTime(Guid cppersonabsenceid, DateTime plannedenddateandtime, List<Guid> careproviderpersoncontracts)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, cppersonabsenceid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddateandtime", plannedenddateandtime);

            buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"] = new MultiSelectBusinessObjectDataCollection();

            if (careproviderpersoncontracts != null && careproviderpersoncontracts.Count > 0)
            {
                foreach (Guid careproviderpersoncontractId in careproviderpersoncontracts)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careproviderpersoncontractId,
                        ReferenceIdTableName = "careproviderpersoncontract",
                        ReferenceName = "Test"
                    };
                    buisinessDataObject.MultiSelectBusinessObjectFields["careproviderpersoncontractid"].Add(dataRecord);
                }
            }

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
