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
    public class CaseAction : BaseClass
    {

        public string TableName = "CaseAction";
        public string PrimaryKeyName = "CaseActionId";
        

        public CaseAction(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetByHealthAppointmentID(Guid HealthAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "HealthAppointmentId", ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCaseAction(Guid CaseActionId)
        {
            this.DeleteRecord(TableName, CaseActionId);
        }
    }
}
