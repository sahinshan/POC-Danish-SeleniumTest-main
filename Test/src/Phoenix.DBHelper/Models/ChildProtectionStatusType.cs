using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChildProtectionStatusType : BaseClass
    {

        private string tableName = "ChildProtectionStatusType";
        private string primaryKeyName = "ChildProtectionStatusTypeId";

        public ChildProtectionStatusType()
        {
            AuthenticateUser();
        }

        public ChildProtectionStatusType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateChildProtectionStatusType(Guid ownerid, string name, DateTime startdate,
            bool EndDateMandatory = true, bool EndReasonMandatory = true, bool Section47FormRequired = false, bool IsDefault = false)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            AddFieldToBusinessDataObject(dataObject, "EndDateMandatory", EndDateMandatory);
            AddFieldToBusinessDataObject(dataObject, "EndReasonMandatory", EndReasonMandatory);
            AddFieldToBusinessDataObject(dataObject, "Section47FormRequired", Section47FormRequired);
            AddFieldToBusinessDataObject(dataObject, "IsDefault", IsDefault);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }
        public List<Guid> GetChildProtectionStatusTypeByStatusTypeID(Guid ChildProtectionStatusTypeId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionStatusTypeId", ConditionOperatorType.Equal, ChildProtectionStatusTypeId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetChildProtectionStatusTypeByID(Guid ChildProtectionStatusTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionStatusTypeId", ConditionOperatorType.Equal, ChildProtectionStatusTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteChildProtectionStatusTypeID(Guid ChildProtectionStatusTypeId)
        {
            this.DeleteRecord(tableName, ChildProtectionStatusTypeId);
        }

    }
}
