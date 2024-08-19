using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonalBudgetType : BaseClass
    {

        public string TableName = "PersonalBudgetType";
        public string PrimaryKeyName = "PersonalBudgetTypeId";

        public PersonalBudgetType()
        {
            AuthenticateUser();
        }

        public PersonalBudgetType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string PersonalBudgetTypeName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, PersonalBudgetTypeName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreatePersonalBudgetType(string PersonalBudgetTypeName, DateTime startdate, int code, Guid ownerid, Guid OwningBusinessUnitId, DateTime? enddate, bool oneoff = false, bool authorisable = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonalBudgetTypeName", PersonalBudgetTypeName);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "oneoff", oneoff);
            AddFieldToBusinessDataObject(buisinessDataObject, "authorisable", authorisable);

            return CreateRecord(buisinessDataObject);
        }


    }
}
