using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DiaryBookingToPeople : BaseClass
    {

        private string tableName = "DiaryBookingToPeople";
        private string primaryKeyName = "DiaryBookingToPeopleId";

        public DiaryBookingToPeople()
        {
            AuthenticateUser();
        }

        public DiaryBookingToPeople(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Dictionary<string, object> GetDiaryBookingToPeopleByID(Guid DiaryBookingToPeopleId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, DiaryBookingToPeopleId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByDiaryId(Guid diaryid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "diaryid", ConditionOperatorType.Equal, diaryid);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, tableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);

        }

        public Guid CreateDiaryBookingToPeople(Guid ownerid, Guid diaryid, Guid personid, Guid careproviderpersoncontractid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "diaryid", diaryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersoncontractid", careproviderpersoncontractid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateDiaryBookingToPeople(Guid ownerid, Guid diaryid, Guid personid, Guid careproviderpersoncontractid, Guid contractserviceid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "diaryid", diaryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersoncontractid", careproviderpersoncontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "contractserviceid", contractserviceid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateDiaryBookingToPeople(Guid ownerid, Guid owningbussinessunitid, string title, Guid diaryid, Guid personid, Guid careproviderpersoncontractid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbussinessunitid", owningbussinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

            AddFieldToBusinessDataObject(buisinessDataObject, "diaryid", diaryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersoncontractid", careproviderpersoncontractid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "overridebookingcharge", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public void DeleteDiaryBookingToPeople(Guid DiaryBookingToPeopleId)
        {

            this.DeleteRecord(tableName, DiaryBookingToPeopleId);
        }

    }
}
