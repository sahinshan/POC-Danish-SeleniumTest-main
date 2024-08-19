using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonContract : BaseClass
    {

        public string TableName = "CareProviderPersonContract";
        public string PrimaryKeyName = "CareProviderPersonContractId";

        public CareProviderPersonContract()
        {
            AuthenticateUser();
        }

        public CareProviderPersonContract(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBypersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateCareProviderPersonContract(Guid ownerid, string title, Guid personid, Guid systemuserid, Guid providerestablishmentid, Guid careprovidercontractschemeid, Guid providerfunderid, DateTime startdate, DateTime? enddatetime = null, bool pcisenabledforscheduledbookings = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerestablishmentid", providerestablishmentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractschemeid", careprovidercontractschemeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "providerfunderid", providerfunderid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddatetime", enddatetime);
            AddFieldToBusinessDataObject(buisinessDataObject, "pcisenabledforscheduledbookings", pcisenabledforscheduledbookings);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderPersonContractNumber", 256);

            return CreateRecord(buisinessDataObject);
        }

        public void UpdatePcIsEnabledForScheduleBooking(Guid CareProviderPersonContractId, bool pcisenabledforscheduledbookings)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderPersonContractId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "pcisenabledforscheduledbookings", DataType.Boolean, BusinessObjectFieldType.Unknown, false, pcisenabledforscheduledbookings);


            this.UpdateRecord(buisinessDataObject);
        }

        //update startdate
        public void UpdateStartDate(Guid CareProviderPersonContractId, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderPersonContractId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", DataType.DateTime, BusinessObjectFieldType.Unknown, false, startdate);

            this.UpdateRecord(buisinessDataObject);
        }

        //update enddatetime
        public void UpdateEndDateTime(Guid CareProviderPersonContractId, DateTime? enddatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderPersonContractId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddatetime", DataType.DateTime, BusinessObjectFieldType.Unknown, false, enddatetime);

            this.UpdateRecord(buisinessDataObject);
        }


        public Dictionary<string, object> GetByID(Guid CareProviderPersonContractId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderPersonContractId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
