using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class AppointmentRequiredAttendee : BaseClass
    {

        public string TableName = "AppointmentRequiredAttendee";
        public string PrimaryKeyName = "AppointmentRequiredAttendeeId";
        

        public AppointmentRequiredAttendee(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateAppointmentRequiredAttendee(Guid AppointmentId, Guid RegardingId, string RegardingIdTableName, string RegardingIdName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "AppointmentId", AppointmentId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingId", RegardingId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdTableName", RegardingIdTableName);
            AddFieldToBusinessDataObject(buisinessDataObject, "RegardingIdName", RegardingIdName);
            
            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetAppointmentRequiredAttendeeByAppointmentID(Guid AppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "AppointmentId", ConditionOperatorType.Equal, AppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeleteAppointmentRequiredAttendee(Guid AppointmentRequiredAttendeeId)
        {
            this.DeleteRecord(TableName, AppointmentRequiredAttendeeId);
        }
    }
}
