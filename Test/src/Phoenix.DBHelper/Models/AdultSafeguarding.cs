using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using CareWorks.Foundation.SystemEntities;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AdultSafeguarding : BaseClass
    {

        private string tableName = "AdultSafeguarding";
        private string primaryKeyName = "AdultSafeguardingId";

        public AdultSafeguarding()
        {
            AuthenticateUser();
        }

        public AdultSafeguarding(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAdultSafeguarding(Guid ownerid, Guid responsibleuserid, Guid caseid, string CaseName, Guid personid, Guid adultsafeguardingcategoryofabuseid, Guid adultsafeguardingstatusid, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "caseid_cwname", CaseName);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "adultsafeguardingcategoryofabuseid", adultsafeguardingcategoryofabuseid);
            AddFieldToBusinessDataObject(dataObject, "adultsafeguardingstatusid", adultsafeguardingstatusid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "deprivationofliberty", false);
            AddFieldToBusinessDataObject(dataObject, "discussedwithperson", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAdultSafeguardingByCaseID(Guid CaseId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetAdultSafeguardingByID(Guid AdultSafeguardingId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AdultSafeguardingId", ConditionOperatorType.Equal, AdultSafeguardingId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAdultSafeguarding(Guid AdultSafeguardingID)
        {
            this.DeleteRecord(tableName, AdultSafeguardingID);
        }
    }
}
