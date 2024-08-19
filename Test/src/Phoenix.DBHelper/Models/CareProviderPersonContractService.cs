using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonContractService : BaseClass
    {

        public string TableName = "CareProviderPersonContractService";
        public string PrimaryKeyName = "CareProviderPersonContractServiceId";

        public CareProviderPersonContractService()
        {
            AuthenticateUser();
        }

        public CareProviderPersonContractService(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateCareProviderPersonContractService(Guid careproviderpersoncontractid, Guid ownerid,
            Guid careprovidercontractschemeid, Guid careproviderserviceid, Guid careprovidercontractserviceid,
            DateTime startdatetime, int personcontractservicestatusid,
            int frequencyinweeks, Guid careproviderrateunitid, bool? israterequired = false, bool? cpsuspenddebtorinvoices = false, bool? isupdatefinancecode = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersoncontractid", careproviderpersoncontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderserviceid", careproviderserviceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractserviceid", careprovidercontractserviceid);

            AddFieldToBusinessDataObject(buisinessDataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "personcontractservicestatusid", personcontractservicestatusid);

            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", frequencyinweeks);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderrateunitid", careproviderrateunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "israterequired", israterequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoices", cpsuspenddebtorinvoices);
            AddFieldToBusinessDataObject(buisinessDataObject, "isupdatefinancecode", isupdatefinancecode);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCareProviderPersonContractService(Guid careproviderpersoncontractid, Guid ownerid,
            Guid careprovidercontractschemeid, Guid careproviderserviceid, Guid CareProviderServiceDetailId, Guid? careprovidercontractserviceid,
            DateTime startdatetime, int personcontractservicestatusid,
            int frequencyinweeks, Guid careproviderrateunitid, bool? israterequired = false, bool? cpsuspenddebtorinvoices = false, bool? isupdatefinancecode = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersoncontractid", careproviderpersoncontractid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderserviceid", careproviderserviceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderServiceDetailId", CareProviderServiceDetailId);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractserviceid", careprovidercontractserviceid);

            AddFieldToBusinessDataObject(buisinessDataObject, "startdatetime", startdatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "personcontractservicestatusid", personcontractservicestatusid);

            AddFieldToBusinessDataObject(buisinessDataObject, "frequencyinweeks", frequencyinweeks);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderrateunitid", careproviderrateunitid);

            AddFieldToBusinessDataObject(buisinessDataObject, "israterequired", israterequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoices", cpsuspenddebtorinvoices);
            AddFieldToBusinessDataObject(buisinessDataObject, "isupdatefinancecode", isupdatefinancecode);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonContractId(Guid careproviderpersoncontractid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderpersoncontractid", ConditionOperatorType.Equal, careproviderpersoncontractid);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderPersonContractServiceId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderPersonContractServiceId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByAccountCode(int accountcode)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "accountcode", ConditionOperatorType.Equal, accountcode);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public void UpdateAccountCode(Guid CareProviderPersonContractServiceId, string accountcode)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "accountcode", accountcode);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEndDateTime(Guid CareProviderPersonContractServiceId, DateTime? enddatetime, Guid? careproviderpersoncontractserviceendreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddatetime", enddatetime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "careproviderpersoncontractserviceendreasonid", careproviderpersoncontractserviceendreasonid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatus(Guid CareProviderPersonContractServiceId, int personcontractservicestatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personcontractservicestatusid", personcontractservicestatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOverrideRateAndRateVerified(Guid CareProviderPersonContractServiceId, bool isoverriderate, bool israteverified)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "isoverriderate", isoverriderate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "israteverified", israteverified);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSuspendDebtorInvoicesAndReason(Guid CareProviderPersonContractServiceId, bool cpsuspenddebtorinvoices, Guid? cpsuspenddebtorinvoicesreasonid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoices", cpsuspenddebtorinvoices);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoicesreasonid", cpsuspenddebtorinvoicesreasonid);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetAllCareProviderService()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetByCareProviderPersonContractIdAndSchemeId(Guid CareProviderPersonContractId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderPersonContractId", ConditionOperatorType.Equal, CareProviderPersonContractId);

            /*if (CareProviderContractSchemeId.HasValue)
                this.BaseClassAddTableCondition(query, "CareProviderContractSchemeId", ConditionOperatorType.Equal, CareProviderContractSchemeId.Value);
*/
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }
    }
}
