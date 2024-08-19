using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MailMerge : BaseClass
    {

        public string TableName = "MailMerge";
        public string PrimaryKeyName = "MailMergeId";

        public MailMerge()
        {
            AuthenticateUser();
        }

        public MailMerge(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public void UpdatePrintFileType(Guid MailMergeId, int printfiletypeid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, MailMergeId);
            AddFieldToBusinessDataObject(dataObject, "printfiletypeid", printfiletypeid);

            this.UpdateRecord(dataObject);
        }

    }
}
