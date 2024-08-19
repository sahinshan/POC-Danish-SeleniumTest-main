using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class PersonDiagnosis : BaseClass
    {
        public string TableName { get { return "PersonDiagnosis"; } }
        public string PrimaryKeyName { get { return "PersonDiagnosisid"; } }


        public PersonDiagnosis()
        {
            AuthenticateUser();
        }

        public PersonDiagnosis(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid Personid)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, Personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreatePersonDiagnosis(Guid OwnerId, string title, Guid personid, int primaryorsecondaryid, int provisionalorconfirmedid, DateTime startdate, Guid diagnosisid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "primaryorsecondaryid", primaryorsecondaryid);
            AddFieldToBusinessDataObject(dataObject, "provisionalorconfirmedid", provisionalorconfirmedid);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "diagnosisid", diagnosisid);


            return this.CreateRecord(dataObject);
        }

        public Dictionary<string, object> GetByID(Guid PersonDiagnosisId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonDiagnosisId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
        public List<Guid> GetByName(string PersonDiagnosisName)
        {
            var query = new DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", ConditionOperatorType.Equal, PersonDiagnosisName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((string)c.FieldCollection[PrimaryKeyName].ToString())).ToList();
            else
                return new List<Guid>();
        }
        public void DeletePersonDiagnosis(Guid PersonDiagnosisid)
        {
            this.DeleteRecord(TableName, PersonDiagnosisid);
        }
    }
}
