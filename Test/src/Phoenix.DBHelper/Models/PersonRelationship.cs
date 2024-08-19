using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonRelationship : BaseClass
    {

        public string TableName = "PersonRelationship";
        public string PrimaryKeyName = "PersonRelationshipId";


        public PersonRelationship()
        {
            AuthenticateUser();
        }

        public PersonRelationship(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonRelationship(Guid ownerid,
            Guid personid, string personName,
            Guid personrelationshiptypeid, string personrelationshiptypeName,
            Guid relatedpersonid, string relatedpersonName,
            Guid relatedpersonrelationshiptypeid, string relatedpersonrelationshiptypeName,
            DateTime startdate, string description, int insidehouseholdid, int emergencycontactid, int familymemberid, int keyholderid, int advocateid, int mhanearestrelativeid,
            int isbirthparentid, int primarycarerid, int powerofattorneyid, int secondarycaregiverid, int financialrepresentativeid, int hasparentalresponsibilityid, int externalprimarycaseworkerid, int pchrid,
            int nextofkinid, int legalguardianid, int youngcarerid, bool inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid_cwname", personName);

            AddFieldToBusinessDataObject(buisinessDataObject, "personrelationshiptypeid", personrelationshiptypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personrelationshiptypeid_cwname", personrelationshiptypeName);

            AddFieldToBusinessDataObject(buisinessDataObject, "relatedpersonid", relatedpersonid);
            AddFieldToBusinessDataObject(buisinessDataObject, "relatedpersonid_cwname", relatedpersonName);

            AddFieldToBusinessDataObject(buisinessDataObject, "relatedpersonrelationshiptypeid", relatedpersonrelationshiptypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "relatedpersonrelationshiptypeid_cwname", relatedpersonrelationshiptypeName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "description", description);
            AddFieldToBusinessDataObject(buisinessDataObject, "insidehouseholdid", insidehouseholdid);
            AddFieldToBusinessDataObject(buisinessDataObject, "emergencycontactid", emergencycontactid);
            AddFieldToBusinessDataObject(buisinessDataObject, "familymemberid", familymemberid);
            AddFieldToBusinessDataObject(buisinessDataObject, "keyholderid", keyholderid);
            AddFieldToBusinessDataObject(buisinessDataObject, "advocateid", advocateid);
            AddFieldToBusinessDataObject(buisinessDataObject, "mhanearestrelativeid", mhanearestrelativeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "isbirthparentid", isbirthparentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "primarycarerid", primarycarerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "powerofattorneyid", powerofattorneyid);
            AddFieldToBusinessDataObject(buisinessDataObject, "secondarycaregiverid", secondarycaregiverid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialrepresentativeid", financialrepresentativeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "hasparentalresponsibilityid", hasparentalresponsibilityid);
            AddFieldToBusinessDataObject(buisinessDataObject, "externalprimarycaseworkerid", externalprimarycaseworkerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "pchrid", pchrid);
            AddFieldToBusinessDataObject(buisinessDataObject, "nextofkinid", nextofkinid);
            AddFieldToBusinessDataObject(buisinessDataObject, "legalguardianid", legalguardianid);
            AddFieldToBusinessDataObject(buisinessDataObject, "youngcarerid", youngcarerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonRelationshipByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonRelationshipByID(Guid PersonRelationshipId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonRelationshipId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonRelationship(Guid PersonRelationshipId)
        {
            this.DeleteRecord(TableName, PersonRelationshipId);
        }
    }
}
