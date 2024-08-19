using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class ProductLanguage : BaseClass
    {

        public string TableName = "ProductLanguage";
        public string PrimaryKeyName = "ProductLanguageId";

        public ProductLanguage()
        {
            AuthenticateUser();
        }

        public ProductLanguage(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetProductLanguageIdByName(string ProductLanguageName)
        {
            var allProductLanguages = this.RetrieveProductLanguages();

            return allProductLanguages.Where(c => c.Key == ProductLanguageName).Select(c => c.Value).ToList();
        }


        public Guid CreateProductLanguage(string Name, string culturename, string currencysymbol, int lcid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "culturename", culturename);
            AddFieldToBusinessDataObject(dataObject, "currencysymbol", currencysymbol);
            AddFieldToBusinessDataObject(dataObject, "lcid", lcid);
            AddFieldToBusinessDataObject(dataObject, "TwelveHourFormat", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }
    }
}
