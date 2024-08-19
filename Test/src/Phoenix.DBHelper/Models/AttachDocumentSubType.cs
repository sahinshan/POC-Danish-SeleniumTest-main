using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AttachDocumentSubType : BaseClass
    {

        public string TableName = "AttachDocumentSubType";
        public string PrimaryKeyName = "AttachDocumentSubTypeId";

        public AttachDocumentSubType()
        {
            AuthenticateUser();
        }

        public AttachDocumentSubType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAttachDocumentSubType(Guid OwnerId, string Name, DateTime StartDate, Guid AttachDocumentTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "AttachDocumentTypeId", AttachDocumentTypeId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateAttachDocumentSubType(Guid AttachDocumentSubTypeId, Guid OwnerId, string Name, DateTime StartDate, Guid AttachDocumentTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "AttachDocumentSubTypeId", AttachDocumentSubTypeId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "AttachDocumentTypeId", AttachDocumentTypeId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAttachDocumentSubTypeIdByName(string AttachDocumentSubTypeName)
        {
            DataQuery query = this.GetDataQueryObject("AttachDocumentSubType", false, "AttachDocumentSubTypeId");

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, AttachDocumentSubTypeName);

            this.AddReturnField(query, "AttachDocumentSubType", "AttachDocumentSubTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "AttachDocumentSubTypeId");

        }

    }
}
