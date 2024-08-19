using CareWorks.Foundation.Enums;
using CareWorks.Foundation.SystemEntities;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AdoptionLink : BaseClass
    {

        public string TableName = "AdoptionLink";
        public string PrimaryKeyName = "AdoptionLinkId";


        public AdoptionLink()
        {
            AuthenticateUser();
        }

        public AdoptionLink(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAdoptionLink(Guid OwnerId, Guid ResponsibleUserId, Guid PreAdoptionPersonId, Guid PersonId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PreAdoptionPersonId", PreAdoptionPersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonID(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPreAdoptionPersonId(Guid PreAdoptionPersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PreAdoptionPersonId", ConditionOperatorType.Equal, PreAdoptionPersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetAdoptionLinkByID(Guid AdoptionLinkId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AdoptionLinkId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAdoptionLink(Guid AdoptionLinkId)
        {
            this.DeleteRecord(TableName, AdoptionLinkId);
        }


    }
}
