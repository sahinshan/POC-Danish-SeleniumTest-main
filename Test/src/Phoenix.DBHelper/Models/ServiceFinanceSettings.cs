using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceFinanceSettings : BaseClass
    {
        public string TableName { get { return "ServiceFinanceSettings"; } }
        public string PrimaryKeyName { get { return "ServiceFinanceSettingsid"; } }

        public ServiceFinanceSettings()
        {
            AuthenticateUser();
        }

        public ServiceFinanceSettings(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceFinanceSettings(Guid OwnerId, Guid serviceprovidedid, Guid paymenttypecodeid, Guid vatcodeid, Guid providerbatchgroupingid,
            DateTime startdate, DateTime? enddate, int adjusteddays)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "paymenttypecodeid", paymenttypecodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatcodeid", vatcodeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerbatchgroupingid", providerbatchgroupingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "adjusteddays", adjusteddays);

            AddFieldToBusinessDataObject(buisinessDataObject, "usedinbatchsetup", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "endreasonrulesapply", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "vatapplycharging", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "AggregateOnInvoice", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndReasonRulesApply", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid ServiceFinanceSettingsid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceFinanceSettingsid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
