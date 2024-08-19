using CareDirector.Sdk.Enums;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AvailabilityTypes : BaseClass
    {

        public string TableName = "AvailabilityTypes";
        public string PrimaryKeyName = "AvailabilityTypesId";


        public AvailabilityTypes(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAvailabilityType(string name, int precedence, bool validForExport, Guid ownerId,
            int diaryBookingsValidityId, int regularBookingsValidityId, bool countsTowardsPeriodWorked)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "Name", name);
            AddFieldToBusinessDataObject(businessDataObject, "Precedence", precedence);
            AddFieldToBusinessDataObject(businessDataObject, "ValidForExport", validForExport);
            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "DiaryBookingsValidityId", diaryBookingsValidityId);
            AddFieldToBusinessDataObject(businessDataObject, "RegularBookingsValidityId", regularBookingsValidityId);
            AddFieldToBusinessDataObject(businessDataObject, "CountsTowardsPeriodWorked", countsTowardsPeriodWorked);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);


            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetAvailabilityTypeBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAvailabilityTypeByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByAvailabilityTypeID(Guid AvailabilityTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AvailabilityTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllAvailabilityTypes()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetAvailabilityTypeWithHighestPrecedence()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.AddReturnField(query, TableName, "precedence");

            query.Orders.Add(new CareDirector.Sdk.Query.OrderBy("precedence", SortOrder.Descending, TableName));

            query.RecordsPerPage = 1;
            query.PageNumber = 1;

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAvailabilityType(Guid AvailabilityTypeId)
        {
            this.DeleteRecord(TableName, AvailabilityTypeId);
        }

        public void UpdateDiaryBookingsValidityId(Guid AvailabilityTypesId, int DiaryBookingsValidityId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, AvailabilityTypesId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "diarybookingsvalidityid", DataType.Integer, BusinessObjectFieldType.Unknown, false, DiaryBookingsValidityId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
