using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingDiaryStaff : BaseClass
    {

        private string tableName = "CPBookingDiaryStaff";
        private string primaryKeyName = "CPBookingDiaryStaffId";

        public CPBookingDiaryStaff()
        {
            AuthenticateUser();
        }

        public CPBookingDiaryStaff(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByCPBookingDiaryId(Guid cpbookingdiaryid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpbookingdiaryid", ConditionOperatorType.Equal, cpbookingdiaryid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPBookingDiaryStaffByID(Guid CPBookingDiaryStaffId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPBookingDiaryStaffId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPBookingDiaryStaff(Guid ownerid, string name, Guid cpbookingdiaryid, Guid? systemuseremploymentcontractid, Guid? systemuserid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpbookingdiaryid", cpbookingdiaryid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuseremploymentcontractid", systemuseremploymentcontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public void DeleteCPBookingDiaryStaff(Guid CPBookingDiaryStaffId)
        {

            DeleteRecord(tableName, CPBookingDiaryStaffId);
        }

    }
}
