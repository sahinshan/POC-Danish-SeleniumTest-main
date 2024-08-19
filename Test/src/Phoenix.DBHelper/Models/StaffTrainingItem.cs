using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffTrainingItem : BaseClass
    {

        public string TableName = "stafftrainingitem";
        public string PrimaryKeyName = "stafftrainingitemid";

        public StaffTrainingItem()
        {
            AuthenticateUser();
        }

        public StaffTrainingItem(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateStaffTrainingItem(Guid ownerid, string name, DateTime startdate, int trainingtypeid = 1)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "trainingtypeid", trainingtypeid);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return CreateRecord(dataObject);
        }

        public List<Guid> GetStaffTrainingItemByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByStaffTrainingItemID(Guid ServiceElement1id, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceElement1id);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteStaffTrainingItem(Guid stafftrainingitemid)
        {
            this.DeleteRecord(TableName, stafftrainingitemid);
        }

        public void UpdateInactiveStatus(Guid stafftrainingitemid, bool? InactiveStatus)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, stafftrainingitemid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", InactiveStatus);
        }
    }
}
