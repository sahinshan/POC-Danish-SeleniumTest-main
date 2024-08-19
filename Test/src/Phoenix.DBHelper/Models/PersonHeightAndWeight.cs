using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonHeightAndWeight : BaseClass
    {
        public string TableName = "PersonHeightAndWeight";
        public string PrimaryKeyName = "PersonHeightAndWeightId";


        public PersonHeightAndWeight()
        {
            AuthenticateUser();
        }

        public PersonHeightAndWeight(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonHeightAndWeight(Guid ownerid, Guid? responsibleuserid,
            Guid personid, DateTime datetimetaken, int ageattimetaken,
            bool? estimatedweight, decimal weightkilos,
            bool? estimatedheight, decimal heightmetres,
            decimal? weightlosskilos, int? weightlossstones, int? weightlosspounds, decimal? weightlosspercent,
            int? suggestedscreeningid = null, bool caserequired = false, bool careplanneeded = false, bool monitorfoodandfluid = false,
            bool estimatedbmi = false, DateTime? nextscreeningdate = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);

            AddFieldToBusinessDataObject(dataObject, "datetimetaken", datetimetaken);
            AddFieldToBusinessDataObject(dataObject, "ageattimetaken", ageattimetaken);
            AddFieldToBusinessDataObject(dataObject, "estimatedweight", estimatedweight);
            AddFieldToBusinessDataObject(dataObject, "weightkilos", weightkilos);
            AddFieldToBusinessDataObject(dataObject, "estimatedheight", estimatedheight);
            AddFieldToBusinessDataObject(dataObject, "heightmetres", heightmetres);
            AddFieldToBusinessDataObject(dataObject, "weightlosskilos", weightlosskilos);
            AddFieldToBusinessDataObject(dataObject, "weightlossstones", weightlossstones);
            AddFieldToBusinessDataObject(dataObject, "weightlosspounds", weightlosspounds);
            AddFieldToBusinessDataObject(dataObject, "weightlosspercent", weightlosspercent);
            AddFieldToBusinessDataObject(dataObject, "suggestedscreeningid", suggestedscreeningid);
            AddFieldToBusinessDataObject(dataObject, "caserequired", caserequired);
            AddFieldToBusinessDataObject(dataObject, "careplanneeded", careplanneeded);
            AddFieldToBusinessDataObject(dataObject, "monitorfoodandfluid", monitorfoodandfluid);
            AddFieldToBusinessDataObject(dataObject, "estimatedbmi", estimatedbmi);
            AddFieldToBusinessDataObject(dataObject, "nextscreeningdate", nextscreeningdate);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public Guid CreatePersonHeightAndWeight(Guid ownerid,
            Guid personid, string preferences, DateTime datetimetaken,
            int careconsentgivenid,
            bool? estimatedweight, decimal weightkilos, int? weightstones, int? weightpounds, int? weightounces,
            bool? estimatedheight, decimal heightmetres, int? heightfeet, int? heightinches,
            decimal? weightlosskilos, int? weightlossstones, int? weightlosspounds, decimal? weightlosspercent,
            int? ageattimetaken, bool? estimatedbmi, string bmiresult, string bmiscore,
            int? bmimustscore, int? weightlossmustscore, bool? acutediseaseeffect, int? acutediseasemustscore,
            int? musttotalscore, string risk,
            Dictionary<Guid, string> RequiredCarephysicallocationId, Dictionary<Guid, string> RequiredEquipment, 
            Guid carewellbeingid,
            Guid? careassistanceneededid, Dictionary<Guid, string> systemuserinfo, int totaltimespentwithclientminutes,
            string additionalnotes = "",
            bool isincludeinnexthandover = false, bool flagrecordforhandover = false,
            int? suggestedscreeningid = null, Guid? careplanneeddomainid = null, bool monitorfoodandfluid = false,
            DateTime? nextscreeningdate = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "preferences", preferences);
            AddFieldToBusinessDataObject(dataObject, "datetimetaken", datetimetaken);
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);

            AddFieldToBusinessDataObject(dataObject, "estimatedweight", estimatedweight);
            AddFieldToBusinessDataObject(dataObject, "weightkilos", weightkilos);
            if (weightstones.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightstones", weightstones);
            if (weightpounds.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightpounds", weightpounds);
            if (weightounces.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightounces", weightounces);
            AddFieldToBusinessDataObject(dataObject, "estimatedheight", estimatedheight);
            AddFieldToBusinessDataObject(dataObject, "heightmetres", heightmetres);
            if (heightfeet.HasValue)
                AddFieldToBusinessDataObject(dataObject, "heightfeet", heightfeet);
            if (heightinches.HasValue)
                AddFieldToBusinessDataObject(dataObject, "heightinches", heightinches);
            AddFieldToBusinessDataObject(dataObject, "weightlosskilos", weightlosskilos);
            if(weightlossstones.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightlossstones", weightlossstones);
            if(weightlosspounds.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightlosspounds", weightlosspounds);
            if (weightlosspercent.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightlosspercent", weightlosspercent);
            if (ageattimetaken.HasValue)
                AddFieldToBusinessDataObject(dataObject, "ageattimetaken", ageattimetaken);
            if (estimatedbmi.HasValue)
                AddFieldToBusinessDataObject(dataObject, "estimatedbmi", estimatedbmi);
            if (!bmiresult.Equals(""))
                AddFieldToBusinessDataObject(dataObject, "bmiresult", bmiresult);
            if (!bmiscore.Equals(""))
                AddFieldToBusinessDataObject(dataObject, "bmiscore", bmiscore);
            if (bmimustscore.HasValue)
                AddFieldToBusinessDataObject(dataObject, "bmimustscore", bmimustscore);
            if (weightlossmustscore.HasValue)
                AddFieldToBusinessDataObject(dataObject, "weightlossmustscore", weightlossmustscore);
            if (acutediseaseeffect.HasValue)
                AddFieldToBusinessDataObject(dataObject, "acutediseaseeffect", acutediseaseeffect);
            if (acutediseasemustscore.HasValue)
                AddFieldToBusinessDataObject(dataObject, "acutediseasemustscore", acutediseasemustscore);
            if (musttotalscore.HasValue)
                AddFieldToBusinessDataObject(dataObject, "musttotalscore", musttotalscore);
            if (!risk.Equals(""))
                AddFieldToBusinessDataObject(dataObject, "risk", risk);

            dataObject.MultiSelectBusinessObjectFields["carephysicallocationid"] = new MultiSelectBusinessObjectDataCollection();

            if (RequiredCarephysicallocationId != null && RequiredCarephysicallocationId.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> CarephysicallocationId in RequiredCarephysicallocationId)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = CarephysicallocationId.Key,
                        ReferenceIdTableName = "carephysicallocation",
                        ReferenceName = CarephysicallocationId.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["carephysicallocationid"].Add(dataRecord);
                }
            }

            dataObject.MultiSelectBusinessObjectFields["equipment"] = new MultiSelectBusinessObjectDataCollection();

            if (RequiredEquipment != null && RequiredEquipment.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> careequipmentid in RequiredEquipment)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = careequipmentid.Key,
                        ReferenceIdTableName = "careequipment",
                        ReferenceName = careequipmentid.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["equipment"].Add(dataRecord);
                }
            }

            AddFieldToBusinessDataObject(dataObject, "careassistanceneededid", careassistanceneededid);
            AddFieldToBusinessDataObject(dataObject, "carewellbeingid", carewellbeingid);

            dataObject.MultiSelectBusinessObjectFields["staffrequired"] = new MultiSelectBusinessObjectDataCollection();

            if (systemuserinfo != null && systemuserinfo.Count > 0)
            {
                foreach (KeyValuePair<Guid, string> systemuserid in systemuserinfo)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = systemuserid.Key,
                        ReferenceIdTableName = "systemuser",
                        ReferenceName = systemuserid.Value,
                    };
                    dataObject.MultiSelectBusinessObjectFields["staffrequired"].Add(dataRecord);
                }
            }

            AddFieldToBusinessDataObject(dataObject, "totaltimespentwithclientminutes", totaltimespentwithclientminutes);
            AddFieldToBusinessDataObject(dataObject, "additionalnotes", additionalnotes);

            AddFieldToBusinessDataObject(dataObject, "isincludeinnexthandover", isincludeinnexthandover);
            AddFieldToBusinessDataObject(dataObject, "flagrecordforhandover", flagrecordforhandover);

            AddFieldToBusinessDataObject(dataObject, "suggestedscreeningid", suggestedscreeningid);
            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);
            AddFieldToBusinessDataObject(dataObject, "monitorfoodandfluid", monitorfoodandfluid);
            AddFieldToBusinessDataObject(dataObject, "nextscreeningdate", nextscreeningdate);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public Guid CreatePersonHeightAndWeight(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, string reasonforabsence)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "carenonconsentid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "reasonforabsence", reasonforabsence);




            return this.CreateRecord(dataObject);
        }

        //When Care Consent = No, Non-consent Detail = Deferred
        public Guid CreatePersonHeightAndWeight(Guid personid, Guid ownerid, Guid owningbusinessunitid, DateTime occurred, int careconsentgivenid, int carenonconsentid, DateTime deferredtodate, int timeorshiftid, TimeSpan? deferredtotime, Guid? deferredtoshiftid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(dataObject, "occurred", occurred);
            AddFieldToBusinessDataObject(dataObject, "careconsentgivenid", careconsentgivenid);
            AddFieldToBusinessDataObject(dataObject, "carenonconsentid", carenonconsentid);
            AddFieldToBusinessDataObject(dataObject, "deferredtodate", deferredtodate);
            AddFieldToBusinessDataObject(dataObject, "timeorshiftid", timeorshiftid);
            AddFieldToBusinessDataObject(dataObject, "deferredtotime", deferredtotime);
            AddFieldToBusinessDataObject(dataObject, "deferredtoshiftid", deferredtoshiftid);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByPersonId(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByPersonHeightAndWeightId(Guid PersonHeightAndWeightId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonHeightAndWeightId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid PersonHeightAndWeightId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonHeightAndWeightId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonHeightAndWeight(Guid PersonHeightAndWeightId)
        {
            this.DeleteRecord(TableName, PersonHeightAndWeightId);
        }


    }
}
