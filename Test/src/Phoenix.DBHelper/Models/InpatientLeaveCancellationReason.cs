using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientLeaveCancellationReason : BaseClass
    {
        public string TableName { get { return "InpatientLeaveCancellationReason"; } }
        public string PrimaryKeyName { get { return "InpatientLeaveCancellationReasonid"; } }


        public InpatientLeaveCancellationReason()
        {
            AuthenticateUser();
        }

        public InpatientLeaveCancellationReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid Personid)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, Personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid InpatientLeaveCancellationReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientLeaveCancellationReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetInpatientLeaveCancellationReasonByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateInpatientLeaveCancellationReason(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startDate", startDate);

            return this.CreateRecord(buisinessDataObject);

        }

        public Guid CreateInpatientLeaveCancellationReason(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate, bool validfordischarge)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startDate", startDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "validfordischarge", validfordischarge);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteInpatientLeaveCancellationReason(Guid InpatientLeaveCancellationReasonid)
        {
            this.DeleteRecord(TableName, InpatientLeaveCancellationReasonid);
        }
    }
}
