using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Applicant : BaseClass
    {
        public string TableName { get { return "Applicant"; } }
        public string PrimaryKeyName { get { return "Applicantid"; } }


        public Applicant()
        {
            AuthenticateUser();
        }

        public Applicant(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateApplicant(string firstname, string lastname, Guid ownerid)
        {
            int diff = (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Monday)) % 7;
            var startOfWeek = DateTime.Now.AddDays(-1 * diff).Date;

            return CreateApplicant(firstname, lastname, ownerid, startOfWeek, startOfWeek);
        }

        public Guid CreateApplicant(string firstname, string lastname, Guid ownerid, DateTime satransportweek1cyclestartdate, DateTime SAWeek1CycleStartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "firstname", firstname);
            AddFieldToBusinessDataObject(dataObject, "lastname", lastname);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "satransportweek1cyclestartdate", satransportweek1cyclestartdate);
            AddFieldToBusinessDataObject(dataObject, "SAWeek1CycleStartDate", SAWeek1CycleStartDate);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByFirstName(string firstname)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "firstname", ConditionOperatorType.Equal, firstname);

            AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByLastName(string lastname)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            BaseClassAddTableCondition(query, "lastname", ConditionOperatorType.Equal, lastname);
            AddReturnField(query, TableName, PrimaryKeyName);
            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByComponentID(Guid ComponentId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            BaseClassAddTableCondition(query, "ComponentId", ConditionOperatorType.Equal, ComponentId);

            AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetApplicantByID(Guid ApplicantId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("Applicant", false, "ApplicantId");
            AddReturnFields(query, TableName, FieldsToReturn);

            BaseClassAddTableCondition(query, "ApplicantId", ConditionOperatorType.Equal, ApplicantId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteApplicant(Guid ApplicantId)
        {
            DeleteRecord(TableName, ApplicantId);
        }

        public void UpdatePronoun(Guid ApplicantId, Guid pronounsid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ApplicantId);

            AddFieldToBusinessDataObject(buisinessDataObject, "pronounsid", pronounsid);

            UpdateRecord(buisinessDataObject);
        }

        public void UpdateAvailableFrom(Guid ApplicantId, DateTime availablefrom)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ApplicantId);

            AddFieldToBusinessDataObject(buisinessDataObject, "availablefrom", availablefrom);

            UpdateRecord(buisinessDataObject);
        }
    }
}
