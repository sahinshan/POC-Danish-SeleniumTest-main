using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderContractScheme : BaseClass
    {

        public string TableName = "CareProviderContractScheme";
        public string PrimaryKeyName = "CareProviderContractSchemeId";


        public CareProviderContractScheme()
        {
            AuthenticateUser();
        }

        public CareProviderContractScheme(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public int GetHighestCode()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.AddReturnField(query, TableName, "code");

            query.Orders.Add(new OrderBy("code", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractIntFields(query, "code").FirstOrDefault();
        }

        public List<Guid> GetCareProviderContractSchemeByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCareProviderContractSchemeByID(Guid CareProviderContractSchemeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderContractSchemeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCareProviderContractScheme(String name, DateTime startdate, int code, Guid EstablishmentId, Guid FunderId, Guid ownerid, bool inactive, bool validforexport, bool IsUpdateableOnPersonContractService)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "code", code);
            AddFieldToBusinessDataObject(dataObject, "EstablishmentId", EstablishmentId);
            AddFieldToBusinessDataObject(dataObject, "FunderId", FunderId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "IsUpdateableOnPersonContractService", true);


            return this.CreateRecord(dataObject);
        }

        public Guid CreateCareProviderContractScheme(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string name, DateTime startdate, int code, Guid EstablishmentId, Guid FunderId, bool sundriesapply = false, bool selffunding = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "code", code);
            AddFieldToBusinessDataObject(dataObject, "EstablishmentId", EstablishmentId);
            AddFieldToBusinessDataObject(dataObject, "FunderId", FunderId);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "IsUpdateableOnPersonContractService", true);
            AddFieldToBusinessDataObject(dataObject, "sundriesapply", sundriesapply);
            AddFieldToBusinessDataObject(dataObject, "selffunding", selffunding);


            return this.CreateRecord(dataObject);
        }

        public void DeleteCareProviderContractScheme(Guid CareProviderContractSchemeId)
        {
            this.DeleteRecord(TableName, CareProviderContractSchemeId);
        }

        public void UpdateDefaultAllPersonContractsEnabledForScheduledBookings(Guid CareProviderContractSchemeId, bool defaultallpcenabledforscheduledbookings)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractSchemeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "defaultallpcenabledforscheduledbookings", defaultallpcenabledforscheduledbookings);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSundriesApply(Guid CareProviderContractSchemeId, bool sundriesapply)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractSchemeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "sundriesapply", sundriesapply);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetNotEqualCode(string code = "")
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.NotEqual, code);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCode(int Code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public void UpdateIsUpdateableOnPersonContractService(Guid CareProviderContractSchemeId, bool IsUpdateableOnPersonContractService)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractSchemeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsUpdateableOnPersonContractService", IsUpdateableOnPersonContractService);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAccountCodeApplies(Guid CareProviderContractSchemeId, bool accountcodeapplies)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractSchemeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "accountcodeapplies", accountcodeapplies);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
