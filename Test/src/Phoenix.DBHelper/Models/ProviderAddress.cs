using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProviderAddress : BaseClass
    {

        public string TableName = "ProviderAddress";
        public string PrimaryKeyName = "ProviderAddressId";


        public ProviderAddress()
        {
            AuthenticateUser();
        }

        public ProviderAddress(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetProviderAddressByProviderID(Guid ProviderId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ProviderId", ConditionOperatorType.Equal, ProviderId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetProviderAddressByID(Guid ProviderAddressId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ProviderAddressId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public Guid CreateProviderAddress(Guid personID, Guid significanteventcategoryid, DateTime eventdate, Guid ownerid, Guid significanteventsubcategoryid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personID", personID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventcategoryid", significanteventcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "significanteventsubcategoryid", significanteventsubcategoryid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "eventdate", eventdate);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteProviderAddress(Guid ProviderAddressId)
        {
            this.DeleteRecord(TableName, ProviderAddressId);
        }
    }
}
