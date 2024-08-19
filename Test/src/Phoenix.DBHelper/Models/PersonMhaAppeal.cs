using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonMhaAppeal : BaseClass
    {

        private string tableName = "PersonMhaAppeal";
        private string primaryKeyName = "PersonMhaAppealId";

        public PersonMhaAppeal()
        {
            AuthenticateUser();
        }

        public PersonMhaAppeal(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonMhaAppeal(Guid ownerid, Guid personid, int appealtypeid, DateTime appealrequesteddate, Guid? mharightsandrequestsid, bool inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "appealtypeid", appealtypeid);
            AddFieldToBusinessDataObject(dataObject, "appealrequesteddate", appealrequesteddate);
            AddFieldToBusinessDataObject(dataObject, "mharightsandrequestsid", mharightsandrequestsid);

            AddFieldToBusinessDataObject(dataObject, "inactive", inactive);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetPersonMhaAppealByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetPersonMhaAppealByPersonIDAndAppealTypeId(Guid personid, int appealtypeid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.BaseClassAddTableCondition(query, "appealtypeid", ConditionOperatorType.Equal, appealtypeid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPersonMhaAppealByID(Guid PersonMhaAppealId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonMhaAppealId", ConditionOperatorType.Equal, PersonMhaAppealId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonMhaAppeal(Guid PersonMhaAppealId)
        {
            this.DeleteRecord(tableName, PersonMhaAppealId);
        }



    }
}
