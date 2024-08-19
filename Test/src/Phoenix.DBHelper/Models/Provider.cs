using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Provider : BaseClass
    {
        public string TableName = "Provider";
        public string PrimaryKeyName = "ProviderId";

        public Provider()
        {
            AuthenticateUser();
        }

        public Provider(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetProviderByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetProviderByID(Guid ProviderId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ProviderId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateProviderMainPhone(Guid ProviderID, string mainphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Provider", "ProviderId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderID", ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mainphone", mainphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateavailableOnProviderPortal(Guid ProviderID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Provider", "ProviderId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderID", ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "availableonproviderportal", 1);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateProviderOtherPhone(Guid ProviderID, string otherphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Provider", "ProviderId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderID", ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "otherphone", otherphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateProviderEmail(Guid ProviderID, string Email)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Provider", "ProviderId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderID", ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", Email);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRTTTreatmentFunction(Guid ProviderID, Guid rtttreatmentfunctionid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rtttreatmentfunctionid", rtttreatmentfunctionid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStartDate(Guid ProviderID, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Provider", "ProviderId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderID", ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinanceDetails(Guid ProviderID, string registeredno, bool cpsuspenddebtorinvoices, string vatregistrationnumber, int preferreddocumentsdeliverymethodid, string debtornumber1,
            Guid? cpsuspenddebtorinvoicesreasonid, string financereferencecode, Guid? financecodeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ProviderID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "registeredno", registeredno);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoices", cpsuspenddebtorinvoices);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "vatregistrationnumber", vatregistrationnumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "preferreddocumentsdeliverymethodid", preferreddocumentsdeliverymethodid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "debtornumber1", debtornumber1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoicesreasonid", cpsuspenddebtorinvoicesreasonid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "financereferencecode", financereferencecode);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "financecodeid", financecodeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreateProvider(string name, Guid ownerid, int providertypeid = 3, string email = "")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providertypeid", providertypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", email);

            return this.CreateRecord(buisinessDataObject);

        }

        public Guid CreateProvider(Guid providerId, string name, Guid ownerid, int providertypeid = 3, string email = "")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providertypeid", providertypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", email);


            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateProvider(string name, Guid ownerid, int providertypeid, bool enablescheduling)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providertypeid", providertypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enablescheduling", enablescheduling);

            return this.CreateRecord(buisinessDataObject);

        }

        public Guid CreateProvider(string name, Guid ownerid, int providertypeid, bool enablescheduling,
            DateTime addressstartdate, int addresstypeid, string propertyname, string propertyNo, string street,
            string district, string town, string county, string postcode)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providertypeid", providertypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enablescheduling", enablescheduling);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressstartdate", addressstartdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", addresstypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "propertyname", propertyname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline1", propertyNo);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline2", street);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline3", district);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline4", town);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline5", county);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "postcode", postcode);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "preferreddocumentsdeliverymethodid", 2); //Letter

            return this.CreateRecord(buisinessDataObject);

        }

        public void UpdateCreditorNumberField(Guid ProviderID, string creditornumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Provider", "ProviderId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderID", ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "creditornumber", creditornumber);

            this.UpdateRecord(buisinessDataObject);

        }

        public void UpdateEnableScheduling(Guid ProviderID, bool enablescheduling)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enablescheduling", enablescheduling);

            this.UpdateRecord(buisinessDataObject);

        }

        public void DeleteProvider(Guid ProviderId)
        {
            this.DeleteRecord(TableName, ProviderId);
        }

        public List<Guid> GetAllProviderSortByNameAscendingOrder()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "enablescheduling", ConditionOperatorType.Equal, true);
            query.Orders.Add(new OrderBy("name", SortOrder.Ascending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateParentProvider(Guid ProviderID, Guid parentproviderid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "parentproviderid", parentproviderid);

            this.UpdateRecord(buisinessDataObject);
        }

        //update debtornumber1 field
        public void UpdateDebtorNumber(Guid ProviderID, string debtornumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ProviderID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "debtornumber1", debtornumber);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
