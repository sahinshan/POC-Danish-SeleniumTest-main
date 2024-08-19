using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAllergy : BaseClass
    {

        public string TableName = "PersonAllergy";
        public string PrimaryKeyName = "PersonAllergyId";


        public PersonAllergy()
        {
            AuthenticateUser();
        }

        public PersonAllergy(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonAllergy(Guid ownerid, bool inactive, Guid personid, string PersonName, Guid allergytypeid, string AllergytypeName, string allergendetails, DateTime startdate, DateTime? enddate, string description, int allergicreactionlevelid)
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
            if (enddate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "description", description);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergicreactionlevelid", allergicreactionlevelid);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonAllergy(Guid ownerid, bool inactive, Guid personid, string PersonName, Guid allergytypeid, string AllergytypeName, DateTime startdate, DateTime? enddate, string description, int allergicreactionlevelid, string SnomedCTId, string snomedctidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", PersonName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergytypeid", allergytypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergytypeid_cwname", AllergytypeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "description", description);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "allergicreactionlevelid", allergicreactionlevelid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SnomedCTId", SnomedCTId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "snomedctidname", snomedctidname);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonAllergyByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAllergyByID(Guid PersonAllergyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAllergyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAllergy(Guid PersonAllergyId)
        {
            this.DeleteRecord(TableName, PersonAllergyId);
        }
    }
}
