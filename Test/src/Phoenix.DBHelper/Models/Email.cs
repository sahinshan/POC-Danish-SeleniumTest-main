using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Email : BaseClass
    {

        public string TableName = "Email";
        public string PrimaryKeyName = "EmailId";


        public Email()
        {
            AuthenticateUser();
        }

        public Email(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateEmail(Guid ownerid, Guid personid, Guid responsibleuserid,
            Guid emailfromlookupid, string emailfromlookupidname, string emailfromlookupidtablename,
            Guid regardingid, string regardingidtablename, string regardingidname,
            string subject, string notes, int statusid, DateTime? duedate = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupid", emailfromlookupid);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupidname", emailfromlookupidname);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupidtablename", emailfromlookupidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", 0);
            AddFieldToBusinessDataObject(dataObject, "issignificantevent", 0);
            AddFieldToBusinessDataObject(dataObject, "iscasenote", 0);
            AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);


            return this.CreateRecord(dataObject);
        }

        public Guid CreateEmail(Guid ownerid, Guid personid, Guid responsibleuserid,
            Guid emailfromlookupid, string emailfromlookupidname, string emailfromlookupidtablename,
            Guid regardingid, string regardingidtablename, string regardingidname,
            string subject, string notes, int statusid, DateTime? duedate,
            Guid? activityreasonid, Guid? activitypriorityid, Guid? activityoutcomeid, Guid? activitycategoryid, Guid? activitysubcategoryid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupid", emailfromlookupid);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupidname", emailfromlookupidname);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupidtablename", emailfromlookupidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(dataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(dataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(dataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(dataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", 0);
            AddFieldToBusinessDataObject(dataObject, "issignificantevent", 0);
            AddFieldToBusinessDataObject(dataObject, "iscasenote", 0);
            AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);


            return this.CreateRecord(dataObject);
        }


        public Guid CreatePersonEmail(Guid ownerid, Guid personid, Guid responsibleuserid, Guid emailfromid, Guid emailtoid, Guid regardingid,
           string regardingidtablename, string regardingidname, string subject, string notes,
           int statusid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "emailfromid", emailfromid);
            AddFieldToBusinessDataObject(dataObject, "emailtoid", emailtoid);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", 0);
            AddFieldToBusinessDataObject(dataObject, "issignificantevent", 0);
            AddFieldToBusinessDataObject(dataObject, "iscasenote", 0);
            AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);


            return this.CreateRecord(dataObject);
        }

        public Guid CreateEmail(Guid ownerid, Guid personid, Guid responsibleuserid,
            Guid emailfromlookupid, string emailfromlookupidname, string emailfromlookupidtablename,
            Guid regardingid, string regardingidtablename, string regardingidname,
            string subject, string notes,
            int statusid, DateTime? duedate,
            Guid? activityreasonid, Guid? activitypriorityid, Guid? activityoutcomeid, Guid? activitycategoryid, Guid? activitysubcategoryid,
            bool IsSignificantEvent, DateTime significanteventdate, Guid significanteventcategoryid, Guid significanteventsubcategoryid,
            bool InformationByThirdParty = false, bool iscasenote = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupid", emailfromlookupid);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupidname", emailfromlookupidname);
            AddFieldToBusinessDataObject(dataObject, "emailfromlookupidtablename", emailfromlookupidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "subject", subject);
            AddFieldToBusinessDataObject(dataObject, "notes", notes);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "duedate", duedate);
            AddFieldToBusinessDataObject(dataObject, "activityreasonid", activityreasonid);
            AddFieldToBusinessDataObject(dataObject, "activitypriorityid", activitypriorityid);
            AddFieldToBusinessDataObject(dataObject, "activityoutcomeid", activityoutcomeid);
            AddFieldToBusinessDataObject(dataObject, "activitycategoryid", activitycategoryid);
            AddFieldToBusinessDataObject(dataObject, "activitysubcategoryid", activitysubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "issignificantevent", IsSignificantEvent);
            AddFieldToBusinessDataObject(dataObject, "significanteventdate", significanteventdate);
            AddFieldToBusinessDataObject(dataObject, "significanteventcategoryid", significanteventcategoryid);
            AddFieldToBusinessDataObject(dataObject, "significanteventsubcategoryid", significanteventsubcategoryid);
            AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", InformationByThirdParty);
            AddFieldToBusinessDataObject(dataObject, "iscasenote", iscasenote);
            AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> CreateMultiplePersonEmails(int TotalRecordsToCreate, Guid ownerid, List<Guid> personids, List<Guid> responsibleusersids, List<string> textList, int statusid)
        {
            var allRecordsToCreate = new List<BusinessData>();
            var rnd = new Random();

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var subject = textList[rnd.Next(0, textList.Count)];
                var notes = textList[rnd.Next(0, textList.Count)];
                var personid = personids[rnd.Next(0, personids.Count)];
                var responsibleuserid = responsibleusersids[rnd.Next(0, responsibleusersids.Count)];

                var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
                AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
                AddFieldToBusinessDataObject(dataObject, "personid", personid);
                AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
                AddFieldToBusinessDataObject(dataObject, "emailfromid", responsibleuserid);
                AddFieldToBusinessDataObject(dataObject, "regardingid", personid);
                AddFieldToBusinessDataObject(dataObject, "regardingidtablename", "person");
                AddFieldToBusinessDataObject(dataObject, "regardingidname", "person name");
                AddFieldToBusinessDataObject(dataObject, "subject", subject);
                AddFieldToBusinessDataObject(dataObject, "notes", notes);
                AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
                AddFieldToBusinessDataObject(dataObject, "informationbythirdparty", 0);
                AddFieldToBusinessDataObject(dataObject, "issignificantevent", 0);
                AddFieldToBusinessDataObject(dataObject, "iscasenote", 0);
                AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
                AddFieldToBusinessDataObject(dataObject, "inactive", 0);

                allRecordsToCreate.Add(dataObject);
            }




            return this.CreateMultipleRecords(allRecordsToCreate);
        }


        public List<Guid> GetEmailByRegardingID(Guid RegardingID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RegardingID", ConditionOperatorType.Equal, RegardingID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetEmailByID(Guid EmailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, EmailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid? GetDataRestrictionForEmail(Guid EmailID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                return entity.Emails.Where(c => c.EmailId == EmailID).Select(x => x.DataRestrictionId).FirstOrDefault();

            }
        }

        public void DeleteEmail(Guid EmailId)
        {
            this.DeleteRecord(TableName, EmailId);
        }


        public List<Guid> GetEmailByPersonID(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



    }
}
