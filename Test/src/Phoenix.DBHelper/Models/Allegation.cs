using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Allegation : BaseClass
    {

        private string tableName = "Allegation";
        private string primaryKeyName = "AllegationId";

        public Allegation()
        {
            AuthenticateUser();
        }

        public Allegation(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAllegation(Guid adultsafeguardingid, Guid ownerid,
            Guid allegedvictimid, string allegedvictimidtablename, string allegedvictimidname,
            Guid allegedabuserid, string allegedabuseridtablename, string allegedabuseridname,
            Guid personid, Guid allegationcategoryid, DateTime allegationdate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "adultsafeguardingid", adultsafeguardingid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "allegedvictimid", allegedvictimid);
            AddFieldToBusinessDataObject(dataObject, "allegedvictimidtablename", allegedvictimidtablename);
            AddFieldToBusinessDataObject(dataObject, "allegedvictimidname", allegedvictimidname);
            AddFieldToBusinessDataObject(dataObject, "allegedabuserid", allegedabuserid);
            AddFieldToBusinessDataObject(dataObject, "allegedabuseridtablename", allegedabuseridtablename);
            AddFieldToBusinessDataObject(dataObject, "allegedabuseridname", allegedabuseridname);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "allegationcategoryid", allegationcategoryid);
            AddFieldToBusinessDataObject(dataObject, "allegationdate", allegationdate);
            AddFieldToBusinessDataObject(dataObject, "partoflargerinvestigation", false);
            AddFieldToBusinessDataObject(dataObject, "shouldpolicenotified", false);
            AddFieldToBusinessDataObject(dataObject, "policenotified", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAllegationByAdultSafeguardingID(Guid adultsafeguardingid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "adultsafeguardingid", ConditionOperatorType.Equal, adultsafeguardingid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetAllegationByID(Guid AllegationId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, AllegationId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAllegation(Guid AllegationID)
        {
            this.DeleteRecord(tableName, AllegationID);
        }

        public List<Guid> GetAllegationIdByVictim(Guid AllegedVictimId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AllegedVictimId", ConditionOperatorType.Equal, AllegedVictimId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetAllegationIdByAbuser(Guid AllegedAbuserId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AllegedAbuserId", ConditionOperatorType.Equal, AllegedAbuserId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Guid CreateAllegationVictim(Guid allegedvictimid, Guid allegedabuserid, DateTime allegationdate, Guid allegationcategoryid, Guid ownerid, Guid personid, string allegedvictimidtablename, string allegedvictimidname,
             string allegedabuseridtablename, string allegedabuseridname)

        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);


            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "allegedvictimid", allegedvictimid);
            AddFieldToBusinessDataObject(dataObject, "allegedvictimidtablename", allegedvictimidtablename);
            AddFieldToBusinessDataObject(dataObject, "allegedvictimidname", allegedvictimidname);
            AddFieldToBusinessDataObject(dataObject, "allegedabuserid", allegedabuserid);
            AddFieldToBusinessDataObject(dataObject, "allegedabuseridtablename", allegedabuseridtablename);
            AddFieldToBusinessDataObject(dataObject, "allegedabuseridname", allegedabuseridname);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "allegationcategoryid", allegationcategoryid);
            AddFieldToBusinessDataObject(dataObject, "allegationdate", allegationdate);
            AddFieldToBusinessDataObject(dataObject, "partoflargerinvestigation", false);
            AddFieldToBusinessDataObject(dataObject, "shouldpolicenotified", false);
            AddFieldToBusinessDataObject(dataObject, "policenotified", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        //public Guid CreateAllegationVictim(Guid allegedvictimid, Guid allegedabuserid, DateTime allegationdate, Guid allegationcategoryid, Guid ownerid, Guid personid, string allegedvictimidtablename, string allegedvictimidname,
        //    string allegedabuseridtablename, string allegedabuseridname)
        //{
        //    var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

        //    AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
        //    AddFieldToBusinessDataObject(dataObject, "allegedvictimid", allegedvictimid);
        //    AddFieldToBusinessDataObject(dataObject, "allegedvictimidtablename", allegedvictimidtablename);
        //    AddFieldToBusinessDataObject(dataObject, "allegedvictimidname", allegedvictimidname);
        //    AddFieldToBusinessDataObject(dataObject, "allegedabuserid", allegedabuserid);
        //    AddFieldToBusinessDataObject(dataObject, "allegedabuseridtablename", allegedabuseridtablename);
        //    AddFieldToBusinessDataObject(dataObject, "allegedabuseridname", allegedabuseridname);
        //    AddFieldToBusinessDataObject(dataObject, "personid", personid);
        //    AddFieldToBusinessDataObject(dataObject, "allegationcategoryid", allegationcategoryid);
        //    AddFieldToBusinessDataObject(dataObject, "allegationdate", allegationdate);
        //    AddFieldToBusinessDataObject(dataObject, "partoflargerinvestigation", false);
        //    AddFieldToBusinessDataObject(dataObject, "shouldpolicenotified", false);
        //    AddFieldToBusinessDataObject(dataObject, "policenotified", false);
        //    AddFieldToBusinessDataObject(dataObject, "inactive", false);

        //    return this.CreateRecord(dataObject);
        //}


    }
}
