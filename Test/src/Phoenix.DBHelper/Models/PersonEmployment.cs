using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonEmployment : BaseClass
    {

        private string tableName = "PersonEmployment";
        private string primaryKeyName = "PersonEmploymentId";

        public PersonEmployment()
        {
            AuthenticateUser();
        }

        public PersonEmployment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonEmployment(string Title, string Employer, Guid PersonID, Guid Ownerid, Guid EmploymentWeeklyHoursWorkedId, Guid EmploymentStatusId, Guid? EmploymentTypeId, Guid? EmploymentReasonLeftId, DateTime StartDate, DateTime EndDate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("Employer", Employer);

            dataObject.FieldCollection.Add("Inactive", false);

            dataObject.FieldCollection.Add("PersonID", PersonID);
            dataObject.FieldCollection.Add("Ownerid", Ownerid);

            dataObject.FieldCollection.Add("EmploymentWeeklyHoursWorkedId", EmploymentWeeklyHoursWorkedId);
            dataObject.FieldCollection.Add("EmploymentStatusId", EmploymentStatusId);
            dataObject.FieldCollection.Add("EmploymentTypeId", EmploymentTypeId);
            dataObject.FieldCollection.Add("EmploymentReasonLeftId", EmploymentReasonLeftId);

            dataObject.FieldCollection.Add("StartDate", StartDate);
            dataObject.FieldCollection.Add("EndDate", EndDate);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetPersonEmploymentForPersonRecord(Guid PersonID, string Employer)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonID", ConditionOperatorType.Equal, PersonID);
            this.BaseClassAddTableCondition(query, "Employer", ConditionOperatorType.Equal, Employer);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPersonEmploymentByID(Guid PersonEmploymentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, PersonEmploymentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
