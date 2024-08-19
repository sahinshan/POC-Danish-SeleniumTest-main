using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServicePackage : BaseClass
    {
        public string TableName { get { return "ServicePackage"; } }
        public string PrimaryKeyName { get { return "ServicePackageId"; } }

        public ServicePackage()
        {
            AuthenticateUser();
        }

        public ServicePackage(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServicePackage(Guid OwnerId, Guid ResponsibleUserId, Guid PersonId, Guid ServicePackageTypeId, DateTime StartDate, DateTime? EndDate = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "responsibleuserid", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "servicepackagetypeid", ServicePackageTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", EndDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonId(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);
            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServicePackageId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServicePackageId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
