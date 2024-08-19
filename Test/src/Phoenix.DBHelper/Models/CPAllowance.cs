using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPAllowance : BaseClass
    {

        private string tableName = "CPAllowance";
        private string primaryKeyName = "CPAllowanceId";

        public CPAllowance()
        {
            AuthenticateUser();
        }

        public CPAllowance(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPAllowance(Guid ProviderId, Guid PersonId,
            Guid serviceprovisionid, Guid allowancetypeid, Guid ownerid,
            DateTime serviceprovisionstartdate, DateTime? serviceprovisionenddate, DateTime startdate, TimeSpan starttime, DateTime? enddate, TimeSpan? endtime,
            int payeetypeid, int units, decimal rate, bool topay,
            int ratetypeid, Guid carerrateunitid, int? ruletypeid,
            int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderId", ProviderId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);

            //General
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionid", serviceprovisionid);
            AddFieldToBusinessDataObject(buisinessDataObject, "allowancetypeid", allowancetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            //Dates
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionstartdate", serviceprovisionstartdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovisionenddate", serviceprovisionenddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);

            //Payment Information
            AddFieldToBusinessDataObject(buisinessDataObject, "payeetypeid", payeetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "units", units);
            AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "topay", topay);

            //Setup Information
            AddFieldToBusinessDataObject(buisinessDataObject, "ratetypeid", ratetypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "carerrateunitid", carerrateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ruletypeid", ruletypeid);

            //Status Details
            AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);



            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }


        public void UpdateStatus(Guid PersonID, int statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByServiceProvisionId(Guid serviceprovisionid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "serviceprovisionid", ConditionOperatorType.Equal, serviceprovisionid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPAllowanceByID(Guid CPAllowanceId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPAllowanceId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
