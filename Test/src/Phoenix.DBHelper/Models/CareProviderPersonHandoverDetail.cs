using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonHandoverDetail : BaseClass
    {

        public string TableName = "CareProviderPersonHandoverDetail";
        public string PrimaryKeyName = "CareProviderPersonHandoverDetailId";

        public CareProviderPersonHandoverDetail()
        {
            AuthenticateUser();
        }

        public CareProviderPersonHandoverDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateCareProviderPersonHandoverDetail(Guid recordid,string recordidname,string  recordidtablename,string handovercomments,Guid ownerid,Boolean handoveracknowledged)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

          
            AddFieldToBusinessDataObject(buisinessDataObject, "recordid", recordid);
            AddFieldToBusinessDataObject(buisinessDataObject, "recordidname", recordidname);

            AddFieldToBusinessDataObject(buisinessDataObject, "recordidtablename", recordidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "handovercomments", handovercomments);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "handoveracknowledged", handoveracknowledged);
           

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByrecordid(Guid recordid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "recordid", ConditionOperatorType.Equal, recordid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Dictionary<string, object> GetHandoverRecordDetailsByID(Guid CareProviderPersonHandoverDetailId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderPersonHandoverDetailId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }
    }
}
