using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonContractServiceEndReason : BaseClass
    {

        public string TableName = "CareProviderPersonContractServiceEndReason";
        public string PrimaryKeyName = "CareProviderPersonContractServiceEndReasonId";

        public CareProviderPersonContractServiceEndReason()
        {
            AuthenticateUser();
        }

        public CareProviderPersonContractServiceEndReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string CareProviderPersonContractServiceEndReasonName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, CareProviderPersonContractServiceEndReasonName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCode(int Code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderPersonContractServiceEndReason(Guid ownerid, string Name, DateTime startdate, int code)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);


            return CreateRecord(buisinessDataObject);
        }

        public void UpdateInactiveStatus(Guid CareProviderPersonContractServiceEndReasonId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceEndReasonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
