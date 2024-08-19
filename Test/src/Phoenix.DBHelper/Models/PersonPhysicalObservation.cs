using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonPhysicalObservation : BaseClass
    {

        public string TableName = "PersonPhysicalObservation";
        public string PrimaryKeyName = "PersonPhysicalObservationId";


        public PersonPhysicalObservation()
        {
            AuthenticateUser();
        }

        public PersonPhysicalObservation(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonPhysicalObservation(Guid ownerid,
            Guid personid, DateTime DateTimeTaken, string ReviewPlanningActionsTakenRequired, DateTime ReviewDateTime, Guid dataformid, bool repeatobservationsrequired = true, bool inactive = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);

            AddFieldToBusinessDataObject(buisinessDataObject, "DateTimeTaken", DateTimeTaken);
            AddFieldToBusinessDataObject(buisinessDataObject, "ReviewPlanningActionsTakenRequired", ReviewPlanningActionsTakenRequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "ReviewDateTime", ReviewDateTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "dataformid", dataformid);
            AddFieldToBusinessDataObject(buisinessDataObject, "repeatobservationsrequired", repeatobservationsrequired);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonPhysicalObservationByPersonID(Guid PersonID, bool repeatobservationsrequired = false)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);
            this.BaseClassAddTableCondition(query, "repeatobservationsrequired", ConditionOperatorType.Equal, repeatobservationsrequired);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public void DeletePersonPhysicalObservation(Guid PersonPhysicalObservationId)
        {
            this.DeleteRecord(TableName, PersonPhysicalObservationId);
        }
    }
}
