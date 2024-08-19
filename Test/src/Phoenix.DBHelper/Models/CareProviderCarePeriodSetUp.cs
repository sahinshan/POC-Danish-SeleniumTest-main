using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderCarePeriodSetUp : BaseClass
    {

        public string TableName = "CareProviderCarePeriodSetUp";
        public string PrimaryKeyName = "CareProviderCarePeriodSetUpId";

        public CareProviderCarePeriodSetUp()
        {
            AuthenticateUser();
        }

        public CareProviderCarePeriodSetUp(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByTitle(string Title)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderCarePeriod(Guid ownerid, string title, TimeSpan starttime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderCarePeriod(Guid CareProviderCarePeriodSetUpId)
        {
            this.DeleteRecord(TableName, CareProviderCarePeriodSetUpId);
        }
    }
}
