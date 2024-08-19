using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SignificantEventCategoryPersonChronology : BaseClass
    {

        public string TableName = "SignificantEventCategoryPersonChronology";
        public string PrimaryKeyName = "SignificantEventCategoryPersonChronologyId";


        public SignificantEventCategoryPersonChronology()
        {
            AuthenticateUser();
        }

        public SignificantEventCategoryPersonChronology(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetSignificantEventCategoryPersonChronologyByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSignificantEventCategoryPersonChronologyByID(Guid SignificantEventCategoryPersonChronologyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SignificantEventCategoryPersonChronologyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }




        public Guid CreateSignificantEventCategoryPersonChronology(Guid SignificantEventCategoryId, Guid PersonChronologyId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SignificantEventCategoryId", SignificantEventCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonChronologyId", PersonChronologyId);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteSignificantEventCategoryPersonChronology(Guid SignificantEventCategoryPersonChronologyId)
        {
            this.DeleteRecord(TableName, SignificantEventCategoryPersonChronologyId);
        }
    }
}
