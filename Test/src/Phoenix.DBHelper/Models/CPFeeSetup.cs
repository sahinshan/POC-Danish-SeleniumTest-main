using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPFeeSetup : BaseClass
    {
        public string TableName = "CPFeeSetup";
        public string PrimaryKeyName = "CPFeeSetupId";

        public CPFeeSetup()
        {
            AuthenticateUser();
        }

        public CPFeeSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPFeeSetup(
            Guid cpfeetypeid, Guid ownerid, DateTime startdate, bool enddatemandatory, bool allowglupdating, bool taxablefee,
            int ratetypeid, Guid cpcarerrateunitid, decimal rate, bool fixedrate,
            int ruletypeid, int adjusteddays, int advanceweeks,
            bool reclaimable, bool reclaimfunction, Guid carerbatchgroupingid, bool usedincalculations
            )
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //General
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpfeetypeid", cpfeetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddatemandatory", enddatemandatory);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allowglupdating", allowglupdating);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "taxablefee", taxablefee);

            //Rate Information
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ratetypeid", ratetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpcarerrateunitid", cpcarerrateunitid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "fixedrate", fixedrate);

            //Payment Information
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ruletypeid", ruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "adjusteddays", adjusteddays);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "advanceweeks", advanceweeks);


            //Payment Criteria
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reclaimable", reclaimable);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reclaimfunction", reclaimfunction);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "carerbatchgroupingid", carerbatchgroupingid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "usedincalculations", usedincalculations);


            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);


            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByFeeType(Guid cpfeetypeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "cpfeetypeid", ConditionOperatorType.Equal, cpfeetypeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPFeeSetupId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPFeeSetupId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCPFeeSetup(Guid CPFeeSetupId)
        {
            this.DeleteRecord(TableName, CPFeeSetupId);
        }

    }
}
