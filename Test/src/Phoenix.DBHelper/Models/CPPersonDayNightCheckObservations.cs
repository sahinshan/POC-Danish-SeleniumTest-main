using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonDayNightCheckObservations : BaseClass
    {
        public string TableName
        {
            get
            {
                return "CPPersonDayNightCheck";
            }
        }
        public string PrimaryKeyName
        {
            get
            {
                return "cppersondaynightcheckobservationsid";
            }
        }


        public CPPersonDayNightCheckObservations()
        {
            AuthenticateUser();
        }

        public CPPersonDayNightCheckObservations(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetByName(string name)
        {
            var query = new CareDirector.Sdk.Query.DataQuery(TableName, true, PrimaryKeyName);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, name);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => (Guid.Parse(c.FieldCollection[PrimaryKeyName].ToString()))).ToList();
            else
                return new List<Guid>();
        }


    }
}
