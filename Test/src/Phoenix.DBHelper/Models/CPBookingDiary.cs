using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingDiary : BaseClass
    {

        private string tableName = "CPBookingDiary";
        private string primaryKeyName = "CPBookingDiaryId";

        public CPBookingDiary()
        {
            AuthenticateUser();
        }

        public CPBookingDiary(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCPBookingTypeId(Guid cpbookingtypeid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpbookingtypeid", ConditionOperatorType.Equal, cpbookingtypeid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetCPBookingIdByCreator(Guid createdby)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, tableName));
            this.BaseClassAddTableCondition(query, "createdby", ConditionOperatorType.Equal, createdby);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByScheduleid(Guid cpbookingscheduleid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpbookingscheduleid", ConditionOperatorType.Equal, cpbookingscheduleid);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, tableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPBookingDiaryByID(Guid CPBookingDiaryId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPBookingDiaryId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetCPBookingDiaryByID(Guid CPBookingDiaryId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, true, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPBookingDiaryId);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPBookingDiary(Guid ownerid, Guid owningbusinessunitid, string title, Guid cpbookingtypeid, Guid locationid, DateTime plannedstartdate, TimeSpan plannedStartTime, DateTime plannedenddate, TimeSpan plannedEndTime, string staff, int planneddurationminutes, int planneddurationhours, string people, int? cpbookingdiarynumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingtypeid", cpbookingtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "locationid", locationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedStartTime", plannedStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedEndTime", plannedEndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "planneddurationminutes", planneddurationminutes);
            AddFieldToBusinessDataObject(buisinessDataObject, "planneddurationhours", planneddurationhours);
            AddFieldToBusinessDataObject(buisinessDataObject, "people", people);

            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingdiarynumber", cpbookingdiarynumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "adhoc", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "confirmed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Isdeleted", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "overrideholidayduration", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "isscheduled", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "genderpreferenceid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "staff", staff);



            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCPBookingDiary(Guid ownerid, string title, Guid cpbookingtypeid, Guid locationid, DateTime plannedstartdate, TimeSpan plannedStartTime, DateTime plannedenddate, TimeSpan plannedEndTime, int planneddurationminutes, int planneddurationhours, Guid trainingrequirementid, Guid? systemusertrainingitemid, Guid trainingitemid, int? cpbookingdiarynumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingtypeid", cpbookingtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "locationid", locationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedStartTime", plannedStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedEndTime", plannedEndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "planneddurationminutes", planneddurationminutes);
            AddFieldToBusinessDataObject(buisinessDataObject, "planneddurationhours", planneddurationhours);
            AddFieldToBusinessDataObject(buisinessDataObject, "trainingrequirementid", trainingrequirementid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemusertrainingitemid", systemusertrainingitemid);
            AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);

            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingdiarynumber", cpbookingdiarynumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "adhoc", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "confirmed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Isdeleted", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "overrideholidayduration", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "isscheduled", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "genderpreferenceid", 1);



            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCPBookingDiary(Guid ownerid, Guid owningbusinessunitid, string title, Guid? cpbookingscheduleid, Guid cpbookingtypeid, Guid locationid, DateTime actualstartdate, TimeSpan actualStartTime, DateTime plannedstartdate, TimeSpan plannedStartTime, DateTime plannedenddate, TimeSpan plannedEndTime, string staff, int planneddurationminutes, int planneddurationhours, string people, int? cpbookingdiarynumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingscheduleid", cpbookingscheduleid);

            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingtypeid", cpbookingtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "locationid", locationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualstartdate", actualstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "actualStartTime", actualStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedStartTime", plannedStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedEndTime", plannedEndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "planneddurationminutes", planneddurationminutes);
            AddFieldToBusinessDataObject(buisinessDataObject, "planneddurationhours", planneddurationhours);
            AddFieldToBusinessDataObject(buisinessDataObject, "people", people);

            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingdiarynumber", cpbookingdiarynumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "adhoc", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "confirmed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Isdeleted", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "overrideholidayduration", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "isscheduled", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "genderpreferenceid", 1);
            AddFieldToBusinessDataObject(buisinessDataObject, "staff", staff);



            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateCPBookingDiary(Guid ownerid, Guid owningbusinessunitid, string title, Guid cpbookingtypeid, Guid locationid, DateTime plannedstartdate, TimeSpan plannedStartTime, DateTime plannedenddate, TimeSpan plannedEndTime, Guid? careproviderserviceid = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingtypeid", cpbookingtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "locationid", locationid);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedStartTime", plannedStartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "plannedEndTime", plannedEndTime);

            if (careproviderserviceid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "careproviderserviceid", careproviderserviceid.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "adhoc", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "confirmed", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "Isdeleted", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "overrideholidayduration", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "isscheduled", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "genderpreferenceid", 1);

            return this.CreateRecord(buisinessDataObject);
        }


        public void DeleteCPBookingDiary(Guid cpbookingdiaryid)
        {

            this.DeleteRecord(tableName, cpbookingdiaryid);
        }

        public void UpdateCPBookingDiary(Guid CPBookingDiaryId, DateTime plannedstartdate, DateTime plannedenddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPBookingDiaryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "plannedstartdate", plannedstartdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "plannedenddate", plannedenddate);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCPBookingDiaryStartTime(Guid CPBookingDiaryId, TimeSpan plannedStartTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPBookingDiaryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "plannedStartTime", plannedStartTime);


            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByLocationId(Guid locationid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "locationid", ConditionOperatorType.Equal, locationid);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, tableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);

        }

        public List<Guid> GetByCPBookingScheduleId(Guid CPBookingScheduleId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpBookingscheduleid", ConditionOperatorType.Equal, CPBookingScheduleId);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, tableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByBookingScheduleAndPlannedStartTime(Guid cpbookingscheduleid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpbookingscheduleid", ConditionOperatorType.Equal, cpbookingscheduleid);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, tableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);

        }

        public void UpdateGenderPreference(Guid CPBookingDiaryId, int genderpreferenceid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPBookingDiaryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "genderpreferenceid", genderpreferenceid);

            this.UpdateRecord(buisinessDataObject);
        }

        //Update confirmed field
        public void UpdateConfirmed(Guid CPBookingDiaryId, bool confirmed)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPBookingDiaryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "confirmed", confirmed);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
