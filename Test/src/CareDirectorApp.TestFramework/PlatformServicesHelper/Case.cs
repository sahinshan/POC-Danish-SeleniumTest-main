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
    public class Case : BaseClass
    {
        public string TableName { get { return "Case"; } }
        public string PrimaryKeyName { get { return "CaseId"; } }


        public Case(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Dictionary<string, object> GetCaseByID(string CaseId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("Case", false, "CaseId");
            this.AddReturnFields(query, "ServiceProvision", FieldsToReturn);

            this.AddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);
            
            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateCaseAcceptedIdField(Guid CaseId, int caseacceptedid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CaseId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caseacceptedid", caseacceptedid);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
