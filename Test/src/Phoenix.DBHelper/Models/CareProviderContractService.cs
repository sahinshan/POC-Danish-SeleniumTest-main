using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderContractService : BaseClass
    {

        public string TableName = "CareProviderContractService";
        public string PrimaryKeyName = "CareProviderContractServiceId";


        public CareProviderContractService()
        {
            AuthenticateUser();
        }

        public CareProviderContractService(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetCareProviderContractServiceByserviceId(Guid CareProviderServiceId)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderServiceId", ConditionOperatorType.Equal, CareProviderServiceId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByEstablishmentId(Guid establishmentproviderid)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "establishmentproviderid", ConditionOperatorType.Equal, establishmentproviderid);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCareProviderContractServiceByID(Guid CareProviderContractServiceId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderContractServiceId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCareProviderContractService(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string title, Guid establishmentproviderid, Guid funderproviderid, Guid careprovidercontractschemeid, Guid careproviderserviceid, Guid careprovidervatcodeid, Guid careproviderbatchgroupingid, Guid careproviderrateunitid)
        {
            return CreateCareProviderContractService(ownerid, responsibleuserid, owningbusinessunitid, title, establishmentproviderid, funderproviderid, careprovidercontractschemeid, careproviderserviceid, careprovidervatcodeid, careproviderbatchgroupingid, careproviderrateunitid, 1, 2, false, false, false);
        }

        public Guid CreateCareProviderContractService(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string title, Guid establishmentproviderid,
            Guid funderproviderid, Guid careprovidercontractschemeid, Guid careproviderserviceid, Guid careprovidervatcodeid, Guid careproviderbatchgroupingid, Guid careproviderrateunitid,
            int contractservicetypeid, int contractserviceadjusteddaysid, bool isroomsapply, bool isnegotiatedratescanapply, bool ispermitrateoverride = false, bool chargescheduledcareonactuals = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "establishmentproviderid", establishmentproviderid);
            AddFieldToBusinessDataObject(dataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "funderproviderid", funderproviderid);

            AddFieldToBusinessDataObject(dataObject, "careproviderserviceid", careproviderserviceid);

            AddFieldToBusinessDataObject(dataObject, "careproviderrateunitid", careproviderrateunitid);
            AddFieldToBusinessDataObject(dataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid);
            AddFieldToBusinessDataObject(dataObject, "careprovidervatcodeid", careprovidervatcodeid);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInFinanceInvoiceBatch", false);

            AddFieldToBusinessDataObject(dataObject, "contractservicetypeid", contractservicetypeid);
            AddFieldToBusinessDataObject(dataObject, "ContractServiceAdjustedDaysId", contractserviceadjusteddaysid);
            AddFieldToBusinessDataObject(dataObject, "isroomsapply", isroomsapply);
            AddFieldToBusinessDataObject(dataObject, "isnegotiatedratescanapply", isnegotiatedratescanapply);
            AddFieldToBusinessDataObject(dataObject, "ispermitrateoverride", ispermitrateoverride);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInFinance", false);



            AddFieldToBusinessDataObject(dataObject, "chargescheduledcareonactuals", chargescheduledcareonactuals);
            AddFieldToBusinessDataObject(dataObject, "isscheduleservice", false);
            AddFieldToBusinessDataObject(dataObject, "IsNetIncome", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateCareProviderContractService(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string title, Guid establishmentproviderid,
            Guid funderproviderid, Guid careprovidercontractschemeid, Guid careproviderserviceid, Guid? careproviderservicedetailid, Guid careprovidervatcodeid,
            Guid careproviderbatchgroupingid, Guid careproviderrateunitid,
            int contractservicetypeid, int contractserviceadjusteddaysid, bool isroomsapply, bool isnegotiatedratescanapply, bool ispermitrateoverride = false, bool chargescheduledcareonactuals = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "establishmentproviderid", establishmentproviderid);
            AddFieldToBusinessDataObject(dataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "funderproviderid", funderproviderid);

            AddFieldToBusinessDataObject(dataObject, "careproviderserviceid", careproviderserviceid);
            if (careproviderservicedetailid.HasValue)
                AddFieldToBusinessDataObject(dataObject, "careproviderservicedetailid", careproviderservicedetailid.Value);

            AddFieldToBusinessDataObject(dataObject, "careproviderrateunitid", careproviderrateunitid);
            AddFieldToBusinessDataObject(dataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid);
            AddFieldToBusinessDataObject(dataObject, "careprovidervatcodeid", careprovidervatcodeid);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInFinanceInvoiceBatch", false);

            AddFieldToBusinessDataObject(dataObject, "contractservicetypeid", contractservicetypeid);
            AddFieldToBusinessDataObject(dataObject, "ContractServiceAdjustedDaysId", contractserviceadjusteddaysid);
            AddFieldToBusinessDataObject(dataObject, "isroomsapply", isroomsapply);
            AddFieldToBusinessDataObject(dataObject, "isnegotiatedratescanapply", isnegotiatedratescanapply);
            AddFieldToBusinessDataObject(dataObject, "ispermitrateoverride", ispermitrateoverride);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInFinance", false);



            AddFieldToBusinessDataObject(dataObject, "chargescheduledcareonactuals", chargescheduledcareonactuals);
            AddFieldToBusinessDataObject(dataObject, "isscheduleservice", false);
            AddFieldToBusinessDataObject(dataObject, "IsNetIncome", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateCareProviderContractService(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string title, Guid establishmentproviderid,
            Guid funderproviderid, Guid careprovidercontractschemeid, Guid careproviderserviceid, Guid? careproviderservicedetailid, Guid? cpbookingtypeid, Guid careprovidervatcodeid,
            Guid careproviderbatchgroupingid, Guid careproviderrateunitid,
            int contractservicetypeid, int contractserviceadjusteddaysid, bool isroomsapply, bool isnegotiatedratescanapply, bool ispermitrateoverride = false, bool chargescheduledcareonactuals = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "establishmentproviderid", establishmentproviderid);
            AddFieldToBusinessDataObject(dataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "funderproviderid", funderproviderid);

            AddFieldToBusinessDataObject(dataObject, "careproviderserviceid", careproviderserviceid);
            if (careproviderservicedetailid.HasValue)
                AddFieldToBusinessDataObject(dataObject, "careproviderservicedetailid", careproviderservicedetailid.Value);
            if (cpbookingtypeid.HasValue)
                AddFieldToBusinessDataObject(dataObject, "cpbookingtypeid", cpbookingtypeid.Value);

            AddFieldToBusinessDataObject(dataObject, "careproviderrateunitid", careproviderrateunitid);
            AddFieldToBusinessDataObject(dataObject, "careproviderbatchgroupingid", careproviderbatchgroupingid);
            AddFieldToBusinessDataObject(dataObject, "careprovidervatcodeid", careprovidervatcodeid);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInFinanceInvoiceBatch", false);

            AddFieldToBusinessDataObject(dataObject, "contractservicetypeid", contractservicetypeid);
            AddFieldToBusinessDataObject(dataObject, "ContractServiceAdjustedDaysId", contractserviceadjusteddaysid);
            AddFieldToBusinessDataObject(dataObject, "isroomsapply", isroomsapply);
            AddFieldToBusinessDataObject(dataObject, "isnegotiatedratescanapply", isnegotiatedratescanapply);
            AddFieldToBusinessDataObject(dataObject, "IsUsedInFinance", false);



            AddFieldToBusinessDataObject(dataObject, "chargescheduledcareonactuals", chargescheduledcareonactuals);
            AddFieldToBusinessDataObject(dataObject, "isscheduleservice", false);
            AddFieldToBusinessDataObject(dataObject, "IsNetIncome", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);
            AddFieldToBusinessDataObject(dataObject, "ispermitrateoverride", ispermitrateoverride);

            return this.CreateRecord(dataObject);
        }



        public void UpdateIsUsedInFinance(Guid CareProviderContractServiceId, bool IsUsedInFinance)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsUsedInFinance", IsUsedInFinance);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid CareProviderContractServiceId, bool inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCareProviderServiceDetailId(Guid CareProviderContractServiceId, Guid careproviderservicedetailid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderservicedetailid", careproviderservicedetailid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderContractService(Guid CareProviderContractServiceId)
        {
            this.DeleteRecord(TableName, CareProviderContractServiceId);
        }

        //Method to update isnetcome field
        public void UpdateIsNetIncome(Guid CareProviderContractServiceId, bool IsNetIncome)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsNetIncome", IsNetIncome);

            this.UpdateRecord(buisinessDataObject);
        }

        //method to updated incomecareprovidercontractserviceid
        public void UpdateIncomeCareProviderContractService(Guid CareProviderContractServiceId, Guid incomecareprovidercontractserviceid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "incomecareprovidercontractserviceid", incomecareprovidercontractserviceid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDateToCommenceBlockCharging(Guid CareProviderContractServiceId, DateTime datetocommenceblockcharging)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "datetocommenceblockcharging", datetocommenceblockcharging);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
