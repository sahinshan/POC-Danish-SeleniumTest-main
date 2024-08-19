using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class ContactReason : BaseClass
    {

        public string TableName = "contactreason";
        public string PrimaryKeyName = "contactreasonid";

        public ContactReason()
        {
            AuthenticateUser();
        }

        public ContactReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContactReason(Guid OwnerId, string Name, DateTime StartDate, int BusinessTypeId, bool SupportMultipleCases)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "BusinessTypeId", BusinessTypeId);
            AddFieldToBusinessDataObject(dataObject, "SupportMultipleCases", SupportMultipleCases);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateContactReason(Guid OwnerId, string Name, DateTime StartDate, int BusinessTypeId, int rttadmissiontypeid, bool SupportMultipleCases)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "BusinessTypeId", BusinessTypeId);
            AddFieldToBusinessDataObject(dataObject, "rttadmissiontypeid", rttadmissiontypeid);
            AddFieldToBusinessDataObject(dataObject, "SupportMultipleCases", SupportMultipleCases);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateContactReason(Guid contactreasonid, Guid OwnerId, string Name, DateTime StartDate, int BusinessTypeId, int rttadmissiontypeid, bool SupportMultipleCases)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "BusinessTypeId", BusinessTypeId);
            AddFieldToBusinessDataObject(dataObject, "rttadmissiontypeid", rttadmissiontypeid);
            AddFieldToBusinessDataObject(dataObject, "SupportMultipleCases", SupportMultipleCases);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }



        //public List<Guid> GetByName(string InpatientAdmissionSourceName)
        //{
        //DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
        //query.Filter.AddCondition(TableName, "name", ConditionOperatorType.Equal, InpatientAdmissionSourceName);

        //this.AddReturnField(query, TableName, PrimaryKeyName);

        // return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        //}




        public List<Guid> GetAllActiveRecords()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, false);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByName(string contactreasonName)
        {
            //    DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            //    this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Contains, Name);
            //    this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, false);

            //    this.AddReturnField(query, TableName, PrimaryKeyName);

            //    return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("contactreason", true, "contactreasonId");
            query.PrimaryKeyName = "contactreasonId";

            query.Filter.AddCondition("contactreason", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, contactreasonName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((c.FieldCollection["contactreasonId"]).ToString())).ToList();
            else
                return new List<Guid>();
        }



        public Dictionary<string, object> GetByID(Guid CaseStatusId, params string[] FieldsToReturn)
        {

            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseStatusId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
