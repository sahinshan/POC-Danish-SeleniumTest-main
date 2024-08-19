using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonalCareBodyArea : BaseClass
    {

        public string TableName = "personalcarebodyarea";
        public string PrimaryKeyName = "personalcarebodyareaid";

        public PersonalCareBodyArea()
        {
            AuthenticateUser();
        }

        public PersonalCareBodyArea(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public void DeleteCareProviderCarePeriod(Guid PersonalCareBodyAreaId)
        {
            this.DeleteRecord(TableName, PersonalCareBodyAreaId);
        }
    }
}
