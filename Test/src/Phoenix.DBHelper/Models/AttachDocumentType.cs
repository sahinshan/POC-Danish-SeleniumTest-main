using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AttachDocumentType : BaseClass
    {
        public string TableName = "AttachDocumentType";
        public string PrimaryKeyName = "AttachDocumentTypeId";

        public AttachDocumentType()
        {
            AuthenticateUser();
        }

        public AttachDocumentType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAttachDocumentType(Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateAttachDocumentType(Guid AttachDocumentTypeId, Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "AttachDocumentTypeId", AttachDocumentTypeId);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAttachDocumentTypeIdByName(string AttachDocumentTypeName)
        {
            DataQuery query = this.GetDataQueryObject("AttachDocumentType", false, "AttachDocumentTypeId");

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, AttachDocumentTypeName);

            this.AddReturnField(query, "AttachDocumentType", "AttachDocumentTypeId");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "AttachDocumentTypeId");

        }

        public Guid? GetDataRestrictionForAttachDocumentType(Guid AttachDocumentTypeID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.AttachDocumentTypes.Where(c => c.AttachDocumentTypeId == AttachDocumentTypeID).Select(c => c.DataRestrictionId).FirstOrDefault();
            }
        }

    }
}
