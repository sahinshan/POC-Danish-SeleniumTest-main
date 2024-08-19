    using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace CareDirectorApp.TestFramework
{
    public class PersonDisabilityImpairments : BaseClass
    {

        public string TableName = "PersonDisabilityImpairments";
        public string PrimaryKeyName = "PersonDisabilityImpairmentsId";
        

        public PersonDisabilityImpairments(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonDisabilityImpairments(
            Guid ownerid, Guid personid, Guid? disabilitytypeid, Guid? impairmenttypeid, string registereddisabilitynumber, 
            DateTime diagnosisdate, DateTime notifieddate, DateTime startdate, DateTime? enddate, DateTime cvireceiveddate, 
            DateTime onsetdate, DateTime reviewdate, int disabilityimpairmenttypeid, int disabilityseverityid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);

            if(disabilitytypeid.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "disabilitytypeid", disabilitytypeid.Value);

            if(impairmenttypeid.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "impairmenttypeid", impairmenttypeid.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "registereddisabilitynumber", registereddisabilitynumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "diagnosisdate", diagnosisdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notifieddate", notifieddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            
            if(enddate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "cvireceiveddate", cvireceiveddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "onsetdate", onsetdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "reviewdate", reviewdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "disabilityimpairmenttypeid", disabilityimpairmenttypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "disabilityseverityid", disabilityseverityid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonDisabilityImpairmentsByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonDisabilityImpairmentsByID(Guid PersonDisabilityImpairmentsId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonDisabilityImpairmentsId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonDisabilityImpairments(Guid PersonDisabilityImpairmentsId)
        {
            this.DeleteRecord(TableName, PersonDisabilityImpairmentsId);
        }
    }
}
