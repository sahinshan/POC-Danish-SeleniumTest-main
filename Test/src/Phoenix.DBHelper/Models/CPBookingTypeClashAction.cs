using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingTypeClashAction : BaseClass
    {

        private string tableName = "CPBookingTypeClashAction";
        private string primaryKeyName = "CPBookingTypeClashActionId";

        public CPBookingTypeClashAction()
        {
            AuthenticateUser();
        }

        public CPBookingTypeClashAction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCPBookingTypeId(Guid CPBookingTypeId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBookingTypeId", ConditionOperatorType.Equal, CPBookingTypeId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByCPBookingTypeId(Guid CPBookingTypeId, int bookingtypeclassid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBookingTypeId", ConditionOperatorType.Equal, CPBookingTypeId);
            this.BaseClassAddTableCondition(query, "bookingtypeclassid", ConditionOperatorType.Equal, bookingtypeclassid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public void UpdateDoubleBookingActionId(Guid CPBookingTypeClashActionId, int doublebookingactionid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPBookingTypeClashActionId);

            AddFieldToBusinessDataObject(buisinessDataObject, "doublebookingactionid", doublebookingactionid);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByCPBookingTypeIdAndContainsName(Guid CPBookingTypeId, string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBookingTypeId", ConditionOperatorType.Equal, CPBookingTypeId);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPBookingTypeClashActionByID(Guid CPBookingTypeClashActionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPBookingTypeClashActionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPBookingTypeClashAction(Guid CPBookingTypeId, int BookingTypeClassId, int DefaultDoubleBookingActionId, int DoubleBookingActionId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "CPBookingTypeId", CPBookingTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingTypeClassId", BookingTypeClassId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultDoubleBookingActionId", DefaultDoubleBookingActionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DoubleBookingActionId", DoubleBookingActionId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

    }
}
