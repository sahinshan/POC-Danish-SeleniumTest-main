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
    public class PersonDNAR : BaseClass
    {

        public string TableName = "PersonDNAR";
        public string PrimaryKeyName = "PersonDNARId";

        public PersonDNAR(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetPersonDNARIdForPerson(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);
           

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

            
        }

        public List<Guid> GetPersonDNARIdForPerson(Guid PersonID,Boolean CancelDecision)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);
            this.AddTableCondition(query, "CancelDecision", ConditionOperatorType.Equal, CancelDecision);


            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public Dictionary<string, object> GetPersonDNARByID(Guid PersonDNARId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonDNARId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonDNAR(Guid PersonDNARID)
        {
            var response = DataProvider.Delete("PersonDNAR", PersonDNARID);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

        }

        public Guid CreatePersonDNARRecord(
            Guid ownerId, String title,
             bool Inactive, Guid PersonId, String additionalinformation, DateTime completeddate, Guid DNARcompletedbyid, DateTime dnardatetime, int DNARAgeStatusId = 1, bool interestharmcprbenefit = false, bool cprrefused = false,
             bool naturaldeath = false, bool other = false, int discusseddecisionid = 1, int DNARreviewdecisionid = 3, bool canceldecision = false,
             int adultcprdeceisionid = 1)
        {
            var dataObject = GetBusinessDataBaseObject("PersonDNAR", "PersonDNARId");

            dataObject.FieldCollection.Add("OwnerId", ownerId);
            dataObject.FieldCollection.Add("title", title);
            dataObject.FieldCollection.Add("Inactive", false);
            dataObject.FieldCollection.Add("PersonId", PersonId);
            dataObject.FieldCollection.Add("additionalinformation", additionalinformation);
            dataObject.FieldCollection.Add("completeddate", completeddate);
            dataObject.FieldCollection.Add("DNARcompletedbyid", DNARcompletedbyid);

            dataObject.FieldCollection.Add("dnardatetime", dnardatetime);



            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(dataObject);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            return response.Id.Value;
        }

        public Guid CreatePersonDNARRecord(
            Guid ownerId, String title,
            bool Inactive, Guid PersonId, String additionalinformation, DateTime cancelleddate, DateTime completeddate, bool CancelDecision, Guid cancelledbyid, Guid DNARcompletedbyid, DateTime dnardatetime, int DNARAgeStatusId = 1, bool interestharmcprbenefit = false, bool cprrefused = false,
            bool naturaldeath = false, bool other = false, int discusseddecisionid = 1, int DNARreviewdecisionid = 3,
            int adultcprdeceisionid = 1)
        {
            var dataObject = GetBusinessDataBaseObject("PersonDNAR", "PersonDNARId");

            dataObject.FieldCollection.Add("OwnerId", ownerId);
            dataObject.FieldCollection.Add("title", title);
            dataObject.FieldCollection.Add("Inactive", false);
            dataObject.FieldCollection.Add("PersonId", PersonId);
            dataObject.FieldCollection.Add("additionalinformation", additionalinformation);
            dataObject.FieldCollection.Add("cancelleddate", cancelleddate);
            dataObject.FieldCollection.Add("completeddate", completeddate);
            dataObject.FieldCollection.Add("CancelDecision", CancelDecision);
            dataObject.FieldCollection.Add("cancelledbyid", cancelledbyid);

            dataObject.FieldCollection.Add("DNARcompletedbyid", DNARcompletedbyid);

            dataObject.FieldCollection.Add("dnardatetime", dnardatetime);



            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(dataObject);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            return response.Id.Value;
        }

        public void UpdateCancelDecision(Guid PersonDNARId, bool CancelDecision)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonDNARId);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CancelDecision", 1);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
