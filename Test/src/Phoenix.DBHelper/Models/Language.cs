using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Language : BaseClass
    {

        public string TableName = "Language";
        public string PrimaryKeyName = "LanguageId";


        public Language()
        {
            AuthenticateUser();
        }

        public Language(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetLanguageIdByName(string LanguageName)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("Language", true, "LanguageId");
            query.PrimaryKeyName = "LanguageId";

            query.Filter.AddCondition("Language", "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, LanguageName);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((c.FieldCollection["LanguageId"]).ToString())).ToList();
            else
                return new List<Guid>();
        }


        public Guid CreateLanguage(string Name, Guid ownerid, string Code, string govcode, DateTime startdate, DateTime? enddate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "ReferenceDataOwner", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateLanguage(Guid PrimaryKeyId, string Name, Guid ownerid, string Code, string govcode, DateTime startdate, DateTime? enddate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, PrimaryKeyId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(dataObject, "ReferenceDataOwner", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public Dictionary<string, object> GetByID(Guid LanguageId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, LanguageId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }


    }
}
