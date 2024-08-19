using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PlacementRoomType : BaseClass
    {
        public string TableName = "PlacementRoomType";
        public string PrimaryKeyName = "PlacementRoomTypeId";

        public PlacementRoomType()
        {
            AuthenticateUser();
        }

        public PlacementRoomType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPlacementRoomTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPlacementRoomTypeByID(Guid PlacementRoomTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PlacementRoomTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreatePlacementRoomType(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);


            return this.CreateRecord(dataObject);
        }

        public void DeletePlacementRoomType(Guid PlacementRoomTypeId)
        {
            this.DeleteRecord(TableName, PlacementRoomTypeId);
        }

    }
}
