using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonCarePlanIntervention : BaseClass
    {

        public string TableName = "PersonCarePlanIntervention";
        public string PrimaryKeyName = "PersonCarePlanInterventionId";


        public PersonCarePlanIntervention()
        {
            AuthenticateUser();
        }

        public PersonCarePlanIntervention(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonCarePlaIntervention(Guid ownerid, string title, Guid personid, Guid personcareplangoalid, DateTime plannedstartdate, String description, Guid responsibleuserid, Guid careplaninterventiontypetypeid, Guid careplanneeddomainid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Title", title);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personcareplangoalid", personcareplangoalid);
            AddFieldToBusinessDataObject(dataObject, "plannedstartdate", plannedstartdate);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "iscareplanauthorised", 0);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "careplaninterventiontypetypeid", careplaninterventiontypetypeid);
            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);

            return this.CreateRecord(dataObject);
        }



        public List<Guid> GetByPersonId(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }




    }
}
