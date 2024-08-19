using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FAContributionException : BaseClass
    {
        public FAContributionException()
        {
            AuthenticateUser();
        }

        public FAContributionException(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        private string tableName = "FAContributionException";
        private string primaryKeyName = "FAContributionExceptionId";


        public Guid CreateFAContributionException(Guid ownerid, Guid exceptionreasonid, Guid debtorbatchgroupingid, Guid contributiontypeid, Guid recoverymethodid, Guid contributionid, Guid personid, Guid payeeid,
            string payeeidtablename, string payeeidname, string notetext)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "exceptionreasonid", exceptionreasonid);
            AddFieldToBusinessDataObject(dataObject, "debtorbatchgroupingid", debtorbatchgroupingid);
            AddFieldToBusinessDataObject(dataObject, "contributiontypeid", contributiontypeid);
            AddFieldToBusinessDataObject(dataObject, "recoverymethodid", recoverymethodid);
            AddFieldToBusinessDataObject(dataObject, "contributionid", contributionid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "payeeid", payeeid);
            AddFieldToBusinessDataObject(dataObject, "payeeidtablename", payeeidtablename);
            AddFieldToBusinessDataObject(dataObject, "payeeidname", payeeidname);
            AddFieldToBusinessDataObject(dataObject, "notetext", notetext);
            AddFieldToBusinessDataObject(dataObject, "updateglcode", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetContributionExpectionsByContributionID(Guid FAContributionID)
        {
            DataQuery query = this.GetDataQueryObject("FAContributionException", false, "facontributionexceptionid");
            this.BaseClassAddTableCondition(query, "ContributionId", ConditionOperatorType.Equal, FAContributionID);
            this.AddReturnField(query, "FAContributionException", "FAContributionExceptionid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FAContributionExceptionid");
        }


        public void DeleteFAContributionException(Guid FAContributionExceptionID)
        {
            this.DeleteRecord("FAContributionException", FAContributionExceptionID);
        }

    }
}
