using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPAllowanceSetup : BaseClass
    {

        private string tableName = "CPAllowanceSetup";
        private string primaryKeyName = "CPAllowanceSetupId";

        public CPAllowanceSetup()
        {
            AuthenticateUser();
        }

        public CPAllowanceSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByAllowanceType(Guid cpallowancetypeid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cpallowancetypeid", ConditionOperatorType.Equal, cpallowancetypeid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPAllowanceSetupByID(Guid CPAllowanceSetupId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPAllowanceSetupId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPAllowanceSetup(
            Guid cpallowancetypeid, int payeetypeid, bool allowglcodeupdating, DateTime startdate, bool enddatemandatory, bool taxableallowance,
            int ratetypeid, decimal rate, Guid carerrateunitid, bool fixedrate,
            int ruletypeid, int minimumdays, int cessationage, int adjusteddays, int advanceweeks,
            bool reclaimable, bool reclaimfunction, Guid ownerid, Guid carerbatchgroupingid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            //General

            AddFieldToBusinessDataObject(buisinessDataObject, "cpallowancetypeid", cpallowancetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "payeetypeid", payeetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowglcodeupdating", allowglcodeupdating);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddatemandatory", enddatemandatory);
            AddFieldToBusinessDataObject(buisinessDataObject, "taxableallowance", taxableallowance);

            //Rate Information
            AddFieldToBusinessDataObject(buisinessDataObject, "ratetypeid", ratetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "carerrateunitid", carerrateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "fixedrate", fixedrate);

            //Payment Information
            AddFieldToBusinessDataObject(buisinessDataObject, "ruletypeid", ruletypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "minimumdays", minimumdays);
            AddFieldToBusinessDataObject(buisinessDataObject, "cessationage", cessationage);
            AddFieldToBusinessDataObject(buisinessDataObject, "adjusteddays", adjusteddays);
            AddFieldToBusinessDataObject(buisinessDataObject, "advanceweeks", advanceweeks);

            //Respite
            AddFieldToBusinessDataObject(buisinessDataObject, "respite", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "respitesuspension", false);

            //Payment Criteria
            AddFieldToBusinessDataObject(buisinessDataObject, "reclaimable", reclaimable);
            AddFieldToBusinessDataObject(buisinessDataObject, "reclaimfunction", reclaimfunction);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "carerbatchgroupingid", carerbatchgroupingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "usedincalculations", false);



            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

    }
}
