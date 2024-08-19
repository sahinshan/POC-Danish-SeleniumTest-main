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
    public class PersonAllergy : BaseClass
    {

        public string TableName = "PersonAllergy";
        public string PrimaryKeyName = "PersonAllergyId";
        

        public PersonAllergy(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonAllergy(Guid ownerid, bool inactive, Guid personid, string PersonName, Guid allergytypeid, string AllergytypeName, string allergendetails, DateTime startdate, DateTime enddate, string description, int allergicreactionlevelid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", PersonName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergytypeid", allergytypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergytypeid_cwname", AllergytypeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergendetails", allergendetails);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "description", description);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergicreactionlevelid", allergicreactionlevelid);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonAllergyByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAllergyByID(Guid PersonAllergyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAllergyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAllergy(Guid PersonAllergyId)
        {
            this.DeleteRecord(TableName, PersonAllergyId);
        }
    }
}
