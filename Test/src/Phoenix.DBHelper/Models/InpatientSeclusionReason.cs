using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientSeclusionReason : BaseClass
    {

        private string tableName = "InpatientSeclusionReason";
        private string primaryKeyName = "InpatientSeclusionReasonId";

        public InpatientSeclusionReason()
        {
            AuthenticateUser();
        }

        public InpatientSeclusionReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Guid CreateInpatientSeclusionsReason(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startDate", startDate);


            return this.CreateRecord(buisinessDataObject);

        }


        public Dictionary<string, object> GetInpatientSeclusionReasonByID(Guid InpatientSeclusionReasonId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, InpatientSeclusionReasonId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
