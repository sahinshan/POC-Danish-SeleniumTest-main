using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class JobRoleType : BaseClass
    {

        public string TableName = "JobRoleType".ToLower();
        public string PrimaryKeyName = "JobRoleTypeId".ToLower();

        public JobRoleType()
        {
            AuthenticateUser();
        }

        public JobRoleType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateJobRoleType(Guid OwnerId, string Name, DateTime StartDate, int isconsultantid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "isconsultantid", isconsultantid);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateIsConsultantId(Guid JobRoleTypeId, bool IsConsultantId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, JobRoleTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsConsultantId", IsConsultantId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
