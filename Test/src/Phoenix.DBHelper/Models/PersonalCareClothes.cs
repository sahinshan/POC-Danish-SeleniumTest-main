using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonalCareClothes : BaseClass
    {

        public string TableName = "personalcareclothes";
        public string PrimaryKeyName = "personalcareclothesid";

        public PersonalCareClothes()
        {
            AuthenticateUser();
        }

        public PersonalCareClothes(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public void DeleteCareProviderCarePeriod(Guid PersonalCareClothesId)
        {
            this.DeleteRecord(TableName, PersonalCareClothesId);
        }
    }
}
