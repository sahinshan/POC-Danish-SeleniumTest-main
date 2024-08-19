using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonCarePlanReview : BaseClass
    {

        public string TableName = "PersonCarePlanReview";
        public string PrimaryKeyName = "PersonCarePlanReviewId";


        public PersonCarePlanReview()
        {
            AuthenticateUser();
        }

        public PersonCarePlanReview(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonCarePlanReview(Guid ownerid, Guid responsibleuserid,
            Guid personid, Guid personcareplanid, DateTime? planneddate, DateTime? actualdate, TimeSpan? actualtime,
            int statusId, string summary, Guid careplanreviewoutcomeid,
            string personview, string carerview, string professionalview,
            bool reviewrequired, DateTime? nextreviewdate, Guid? nextreviewresponsibleuserid,
            List<Guid> careradvocates = null, List<Guid> coworkers = null, List<Guid> otherprofessionals = null)

        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personcareplanid", personcareplanid);
            AddFieldToBusinessDataObject(dataObject, "planneddate", planneddate);
            AddFieldToBusinessDataObject(dataObject, "actualdate", actualdate);
            AddFieldToBusinessDataObject(dataObject, "actualtime", actualtime);
            AddFieldToBusinessDataObject(dataObject, "statusId", statusId);

            AddFieldToBusinessDataObject(dataObject, "summary", summary);
            AddFieldToBusinessDataObject(dataObject, "careplanreviewoutcomeid", careplanreviewoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "personview", personview);
            AddFieldToBusinessDataObject(dataObject, "carerview", carerview);
            AddFieldToBusinessDataObject(dataObject, "professionalview", professionalview);
            AddFieldToBusinessDataObject(dataObject, "reviewrequired", reviewrequired);

            AddFieldToBusinessDataObject(dataObject, "nextreviewdate", nextreviewdate);
            AddFieldToBusinessDataObject(dataObject, "nextreviewresponsibleuserid", nextreviewresponsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            dataObject.MultiSelectBusinessObjectFields["careradvocates"] = new MultiSelectBusinessObjectDataCollection();

            if (careradvocates != null && careradvocates.Count > 0)
            {
                foreach (Guid recordId in careradvocates)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = recordId,
                        ReferenceIdTableName = "person",
                    };
                    dataObject.MultiSelectBusinessObjectFields["careradvocates"].Add(dataRecord);
                }
            }

            dataObject.MultiSelectBusinessObjectFields["coworkers"] = new MultiSelectBusinessObjectDataCollection();

            if (coworkers != null && coworkers.Count > 0)
            {
                foreach (Guid recordId in coworkers)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = recordId,
                        ReferenceIdTableName = "systemuser",
                    };
                    dataObject.MultiSelectBusinessObjectFields["coworkers"].Add(dataRecord);
                }
            }

            dataObject.MultiSelectBusinessObjectFields["otherprofessionals"] = new MultiSelectBusinessObjectDataCollection();

            if (otherprofessionals != null && otherprofessionals.Count > 0)
            {
                foreach (Guid recordId in otherprofessionals)
                {
                    var dataRecord = new MultiSelectBusinessObjectData()
                    {
                        ReferenceId = recordId,
                        ReferenceIdTableName = "professional",
                    };
                    dataObject.MultiSelectBusinessObjectFields["otherprofessionals"].Add(dataRecord);
                }
            }

            return this.CreateRecord(dataObject);
        }



        public List<Guid> GetByPersonId(Guid personid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonCarePlanId(Guid personcareplanid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personcareplanid", ConditionOperatorType.Equal, personcareplanid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid personcareplanreviewid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, personcareplanreviewid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
