using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SignificantEventSubCategoryPersonChronology : BaseClass
    {

        public string TableName = "SignificantEventSubCategoryPersonChronology";
        public string PrimaryKeyName = "SignificantEventSubCategoryPersonChronologyId";


        public SignificantEventSubCategoryPersonChronology()
        {
            AuthenticateUser();
        }

        public SignificantEventSubCategoryPersonChronology(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetSignificantEventSubCategoryPersonChronologyByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSignificantEventSubCategoryPersonChronologyByID(Guid SignificantEventSubCategoryPersonChronologyId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SignificantEventSubCategoryPersonChronologyId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }




        public Guid CreateSignificantEventSubCategoryPersonChronology(Guid SignificantEventSubCategoryId, Guid PersonChronologyId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SignificantEventSubCategoryId", SignificantEventSubCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonChronologyId", PersonChronologyId);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteSignificantEventSubCategoryPersonChronology(Guid SignificantEventSubCategoryPersonChronologyId)
        {
            this.DeleteRecord(TableName, SignificantEventSubCategoryPersonChronologyId);
        }
    }
}
