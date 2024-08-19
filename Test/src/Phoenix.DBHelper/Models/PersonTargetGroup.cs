using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonTargetGroup : BaseClass
    {

        public string TableName = "PersonTargetGroup";
        public string PrimaryKeyName = "PersonTargetGroupId";

        public PersonTargetGroup()
        {
            AuthenticateUser();
        }

        public PersonTargetGroup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreatePersonTargetGroup(Guid ownerid, Guid OwningBusinessUnitId, string Name, int Code, DateTime StartDate, DateTime? EndDate, bool Inactive = false, bool ValidForExport = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "Code", Code);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", ValidForExport);

            return CreateRecord(buisinessDataObject);
        }


    }
}
