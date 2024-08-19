using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAttendedEducationEstablishment : BaseClass
    {

        public string TableName = "PersonAttendedEducationEstablishment";
        public string PrimaryKeyName = "PersonAttendedEducationEstablishmentId";


        public PersonAttendedEducationEstablishment()
        {
            AuthenticateUser();
        }

        public PersonAttendedEducationEstablishment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonAttendedEducationEstablishment(Guid personid, Guid providerid, DateTime startdate,
            int attendededucationestablishmentstatusid, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "attendededucationestablishmentstatusid", attendededucationestablishmentstatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonAttendedEducationEstablishmentByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonAttendedEducationEstablishmentByID(Guid PersonAttendedEducationEstablishmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonAttendedEducationEstablishmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAttendedEducationEstablishment(Guid PersonAttendedEducationEstablishmentId)
        {
            this.DeleteRecord(TableName, PersonAttendedEducationEstablishmentId);
        }

    }
}
