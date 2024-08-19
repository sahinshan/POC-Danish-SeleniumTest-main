using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonalCareOther : BaseClass
    {

        public string TableName = "personalcareother";
        public string PrimaryKeyName = "personalcareotherid";

        public PersonalCareOther()
        {
            AuthenticateUser();
        }

        public PersonalCareOther(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);
            
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public void DeleteCareProviderCarePeriod(Guid PersonalCareOtherId)
        {
            this.DeleteRecord(TableName, PersonalCareOtherId);
        }
    }
}
