using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceMapping : BaseClass
    {
        public string TableName { get { return "ServiceMapping"; } }
        public string PrimaryKeyName { get { return "ServiceMappingid"; } }

        public ServiceMapping()
        {
            AuthenticateUser();
        }

        public ServiceMapping(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceMapping(Guid ownerid, Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement2Id", ServiceElement2Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceMapping(Guid ownerid, Guid ServiceElement1Id, Guid CareTypeId, bool inactive = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareTypeId", CareTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceMapping(Guid ownerid, Guid OwningBusinessUnitId, Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement2Id", ServiceElement2Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceMapping(Guid ownerid, Guid OwningBusinessUnitId, Guid ServiceElement1Id, Guid CareTypeId, Guid? ServiceElement2Id)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement2Id", ServiceElement2Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareTypeId", CareTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByServiceElement1And2(Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "ServiceElement2Id", ConditionOperatorType.Equal, ServiceElement2Id);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByServiceElement1AndCareType(Guid ServiceElement1Id, Guid CareTypeId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "CareTypeId", ConditionOperatorType.Equal, CareTypeId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeactivateRecord(Guid ServiceMappingid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceMappingid);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", true);

            this.UpdateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid ServiceMappingid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceMappingid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceMapping(Guid ServiceMappingID)
        {
            this.DeleteRecord(TableName, ServiceMappingID);
        }

    }
}