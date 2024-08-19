using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class GLCode : BaseClass
    {

        public string TableName = "GLCode";
        public string PrimaryKeyName = "GLCodeId";

        public GLCode()
        {
            AuthenticateUser();
        }

        public GLCode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateGLCode(Guid OwnerId, Guid GLCodeLocationId, string Description, string ExpenditureCode, string IncomeCode, bool ExemptFromCharging = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "GLCodeLocationId", GLCodeLocationId);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "ExpenditureCode", ExpenditureCode);
            AddFieldToBusinessDataObject(dataObject, "IncomeCode", IncomeCode);
            AddFieldToBusinessDataObject(dataObject, "ExemptFromCharging", ExemptFromCharging);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByDescription(string Description)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Description", ConditionOperatorType.Equal, Description);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public void DeleteGLCodeRecord(Guid glcodeid)
        {
            this.DeleteRecord(TableName, glcodeid);
        }


    }
}
