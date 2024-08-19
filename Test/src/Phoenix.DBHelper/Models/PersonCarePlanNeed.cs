using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonCarePlanNeed : BaseClass
    {

        public string TableName = "PersonCarePlanNeed";
        public string PrimaryKeyName = "PersonCarePlanNeedId";


        public PersonCarePlanNeed()
        {
            AuthenticateUser();
        }

        public PersonCarePlanNeed(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonCarePlanNeed(Guid ownerid, string title, Guid personid, Guid personcareplanid, DateTime dateidentified, Guid careplanneeddomainid, String description)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Title", title);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personcareplanid", personcareplanid);
            AddFieldToBusinessDataObject(dataObject, "startdate", dateidentified);
            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "neednumber", 1);

            return this.CreateRecord(dataObject);
        }

        public Guid CreatePersonCarePlanNeed(Guid ownerid, string title, Guid personid, Guid personcareplanid, DateTime dateidentified, Guid careplanneeddomainid, String description, Guid documentanswerid)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Title", title);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personcareplanid", personcareplanid);
            AddFieldToBusinessDataObject(dataObject, "startdate", dateidentified);
            AddFieldToBusinessDataObject(dataObject, "careplanneeddomainid", careplanneeddomainid);
            AddFieldToBusinessDataObject(dataObject, "description", description);
            AddFieldToBusinessDataObject(dataObject, "neednumber", 1);
            AddFieldToBusinessDataObject(dataObject, "documentanswerid", documentanswerid);

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
