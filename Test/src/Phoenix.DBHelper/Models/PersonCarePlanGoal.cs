using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonCarePlanGoal : BaseClass
    {

        public string TableName = "PersonCarePlanGoal";
        public string PrimaryKeyName = "PersonCarePlanGoalId";


        public PersonCarePlanGoal()
        {
            AuthenticateUser();
        }

        public PersonCarePlanGoal(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonCarePlanGoal(Guid ownerid, string title, Guid personid, Guid personcareplanneedid, DateTime startdate, String description, Guid responsibleuserid, Guid careplangoaltypeid, Guid careplanneeddomainid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Title", title);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personcareplanneedid", personcareplanneedid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "iscareplanauthorised", 0);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "careplangoaltypeid", careplangoaltypeid);
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
