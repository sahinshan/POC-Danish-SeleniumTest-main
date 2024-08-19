using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPRegularCareTaskSchedule : BaseClass
    {

        private string tableName = "CPRegularCareTaskSchedule";
        private string primaryKeyName = "CPRegularCareTaskScheduleId";

        public CPRegularCareTaskSchedule()
        {
            AuthenticateUser();
        }

        public CPRegularCareTaskSchedule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCPRegularCareTaskId(Guid regularcaretaskid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "regularcaretaskid", ConditionOperatorType.Equal, regularcaretaskid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



        public Guid CreateCPBookingRegularCareTask(Guid ownerid, Guid owningbusinessunitid, string title, Guid personid, Guid regularcaretaskid, Guid careplanid, DateTime startdate, TimeSpan selecttimeforcaretobegiven, int recurrencepatternid, int recureveryxday, DateTime? lastrundate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regularcaretaskid", regularcaretaskid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careplanid", careplanid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "selecttimeforcaretobegiven", selecttimeforcaretobegiven);

            AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recureveryxday", recureveryxday);
            AddFieldToBusinessDataObject(buisinessDataObject, "lastrundate", lastrundate);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateStartDate(Guid CPRegularCareTaskScheduleId, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPRegularCareTaskScheduleId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);


            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEndDate(Guid CPRegularCareTaskScheduleId, DateTime enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPRegularCareTaskScheduleId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);


            this.UpdateRecord(buisinessDataObject);
        }


    }
}
