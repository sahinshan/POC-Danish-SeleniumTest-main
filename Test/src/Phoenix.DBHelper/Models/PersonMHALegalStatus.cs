using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonMHALegalStatus : BaseClass
    {

        private string tableName = "PersonMHALegalStatus";
        private string primaryKeyName = "PersonMHALegalStatusId";

        public PersonMHALegalStatus()
        {
            AuthenticateUser();
        }

        public PersonMHALegalStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonMHALegalStatus(Guid ownerid, Guid personid, Guid mhasectionsetupid, Guid administratorid, DateTime actualstartdatetime)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "ownerid_cwname", "Name...");

            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personid_cwname", "Name...");

            AddFieldToBusinessDataObject(dataObject, "mhasectionsetupid", mhasectionsetupid);
            AddFieldToBusinessDataObject(dataObject, "mhasectionsetupid_cwname", "Name...");

            AddFieldToBusinessDataObject(dataObject, "administratorid", administratorid);
            AddFieldToBusinessDataObject(dataObject, "administratorid_cwname", "Name...");
            AddFieldToBusinessDataObject(dataObject, "actualstartdatetime", actualstartdatetime);
            AddFieldToBusinessDataObject(dataObject, "paperworkattached", 0);
            AddFieldToBusinessDataObject(dataObject, "jointmedicalrecommendation", 0);
            AddFieldToBusinessDataObject(dataObject, "firstsection12approved", 0);
            AddFieldToBusinessDataObject(dataObject, "secondsection12approved", 0);
            AddFieldToBusinessDataObject(dataObject, "awolextensionapplied", 0);
            AddFieldToBusinessDataObject(dataObject, "extendduetonearestrelativedisplacement", 0);
            AddFieldToBusinessDataObject(dataObject, "sectionextended", 0);
            AddFieldToBusinessDataObject(dataObject, "rcnotifiedtocompletedeathform", 0);
            AddFieldToBusinessDataObject(dataObject, "isrenewal", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetPersonMHALegalStatusByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPersonMHALegalStatusByID(Guid PersonMHALegalStatusId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonMHALegalStatusId", ConditionOperatorType.Equal, PersonMHALegalStatusId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonMHALegalStatus(Guid PersonMHALegalStatusID)
        {
            this.DeleteRecord(tableName, PersonMHALegalStatusID);
        }



    }
}
