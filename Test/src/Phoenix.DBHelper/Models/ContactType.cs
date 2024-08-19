using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ContactType : BaseClass
    {
        public string TableName { get { return "ContactType"; } }
        public string PrimaryKeyName { get { return "ContactTypeid"; } }


        public ContactType()
        {
            AuthenticateUser();
        }

        public ContactType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContactType(Guid ownerid, string Name, DateTime StartDate, bool ValidForInitialContact)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForInitialContact", ValidForInitialContact);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateContactType(Guid ContactTypeid, Guid ownerid, string Name, DateTime StartDate, bool ValidForInitialContact)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ContactTypeid", ContactTypeid);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForInitialContact", ValidForInitialContact);

            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetByName(string Name)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ContactTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ContactTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteContactType(Guid ContactTypeid)
        {
            this.DeleteRecord(TableName, ContactTypeid);
        }
    }
}
