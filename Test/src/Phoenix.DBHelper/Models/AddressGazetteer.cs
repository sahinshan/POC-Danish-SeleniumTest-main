using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class AddressGazetteer : BaseClass
    {
        public string TableName = "AddressGazetteer";
        public string PrimaryKeyName = "AddressGazetteerId";

        public AddressGazetteer()
        {
            AuthenticateUser();
        }

        public AddressGazetteer(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateAddressGazetteer(string UPRN, string PropertyNumber, string PropertyName, string Street, string Village, string Town,
            string County, string Country, string Language, string Postcode, string Latitude, string Longitude, string XCoordinate, string YCoordinate,
            Guid? AddressBoroughId, Guid? AddressWardId
)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "UPRN", UPRN);
            AddFieldToBusinessDataObject(dataObject, "AddressLine1", PropertyNumber);
            AddFieldToBusinessDataObject(dataObject, "PropertyName", PropertyName);
            AddFieldToBusinessDataObject(dataObject, "AddressLine2", Street);
            AddFieldToBusinessDataObject(dataObject, "AddressLine3", Village);
            AddFieldToBusinessDataObject(dataObject, "AddressLine4", Town);
            AddFieldToBusinessDataObject(dataObject, "AddressLine5", County);
            AddFieldToBusinessDataObject(dataObject, "Country", Country);
            AddFieldToBusinessDataObject(dataObject, "Language", Language);
            AddFieldToBusinessDataObject(dataObject, "Postcode", Postcode);
            AddFieldToBusinessDataObject(dataObject, "Latitude", Latitude);
            AddFieldToBusinessDataObject(dataObject, "Longitude", Longitude);
            AddFieldToBusinessDataObject(dataObject, "XCoordinate", XCoordinate);
            AddFieldToBusinessDataObject(dataObject, "YCoordinate", YCoordinate);
            AddFieldToBusinessDataObject(dataObject, "AddressBoroughId", AddressBoroughId);
            AddFieldToBusinessDataObject(dataObject, "AddressWardId", AddressWardId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAddressGazetteerIdByUPRN(string UPRN)
        {
            CareDirector.Sdk.Query.DataQuery query = new CareDirector.Sdk.Query.DataQuery("AddressGazetteer", true, "AddressGazetteerId");
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, "UPRN", ConditionOperatorType.Equal, UPRN);

            CareDirector.Sdk.ServiceResponse.BusinessDataCollectionResponse response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
                return response.BusinessDataCollection.Select(c => Guid.Parse((c.FieldCollection[PrimaryKeyName]).ToString())).ToList();
            else
                return new List<Guid>();
        }
    }
}
