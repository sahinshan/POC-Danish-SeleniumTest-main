using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Phoenix.DBHelper.Models
{
    public class ScheduledJob : BaseClass
    {
        public string TableName = "ScheduledJob";
        public string PrimaryKeyName = "ScheduledJobId";

        public ScheduledJob()
        {
            AuthenticateUser();
        }

        public ScheduledJob(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public int GetScheduledJobStatusByScheduledJobID(Guid ScheduledJobID)
        {
            DataQuery query = this.GetDataQueryObject("ScheduledJob", false, "ScheduledJobId");
            this.BaseClassAddTableCondition(query, "ScheduledJobId", ConditionOperatorType.Equal, ScheduledJobID);
            this.AddReturnField(query, "ScheduledJob", "StatusId");

            return this.ExecuteDataQueryAndExtractIntFields(query, "StatusId").FirstOrDefault();
        }

        public List<Guid> GetScheduledJobByScheduledJobName(string ScheduledJobName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, ScheduledJobName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPartialName(string ScheduledJobName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Contains, ScheduledJobName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetScheduledJobByRecordId(Guid RecordId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "RecordId", ConditionOperatorType.Equal, RecordId);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateScheduledJobEveryMinuteField(Guid ScheduledJobId, int everyminutevalue)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(businessDataObject, PrimaryKeyName, ScheduledJobId);
            this.AddFieldToBusinessDataObject(businessDataObject, "everyminute", everyminutevalue);
            this.UpdateRecord(businessDataObject);
        }

        public void UpdateScheduledJobInactiveStatus(Guid ScheduledJobId, bool inactive)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(businessDataObject, PrimaryKeyName, ScheduledJobId);
            this.AddFieldToBusinessDataObject(businessDataObject, "inactive", inactive);
            this.UpdateRecord(businessDataObject);
        }

        public void WaitForScheduledJobIdleState(Guid ScheduledJobID)
        {
            Thread.Sleep(4000);

            int count = 0;
            int idleStatusID = 1;
            int statusID = GetScheduledJobStatusByScheduledJobID(ScheduledJobID);

            while (statusID != idleStatusID)
            {
                count++;
                if (count > 120)
                    throw new Exception("Job status was not Idle after 360 seconds");

                System.Threading.Thread.Sleep(3000); //wait for 3 second
                statusID = GetScheduledJobStatusByScheduledJobID(ScheduledJobID); //get the job status again
            }
        }


    }
}
