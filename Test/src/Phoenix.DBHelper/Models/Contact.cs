using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Contact : BaseClass
    {
        public string TableName = "Contact";
        public string PrimaryKeyName = "ContactId";


        public Contact(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateContact(
            Guid ownerid, Guid personid, Guid contacttypeid, Guid contactreasonid, Guid contactpresentingpriorityid, Guid contactstatusid, Guid responsibleuserid,
            Guid regardingid, string regardingidtablename, string regardingidname,
            DateTime contactreceiveddatetime, string presentingneed, int persongroupawareofcontactid, int nextofkinawareofcontactid)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(businessDataObject, "contacttypeid", contacttypeid);
            AddFieldToBusinessDataObject(businessDataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(businessDataObject, "contactpresentingpriorityid", contactpresentingpriorityid);
            AddFieldToBusinessDataObject(businessDataObject, "contactstatusid", contactstatusid);
            AddFieldToBusinessDataObject(businessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(businessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(businessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(businessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(businessDataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(businessDataObject, "presentingneed", presentingneed);
            AddFieldToBusinessDataObject(businessDataObject, "persongroupawareofcontactid", persongroupawareofcontactid);
            AddFieldToBusinessDataObject(businessDataObject, "nextofkinawareofcontactid", nextofkinawareofcontactid);
            AddFieldToBusinessDataObject(businessDataObject, "iscloned", false);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateContact(
            Guid ownerid, Guid personid, Guid contacttypeid, Guid contactreasonid, Guid contactpresentingpriorityid, Guid contactstatusid, Guid responsibleuserid,
            Guid regardingid, string regardingidtablename, string regardingidname,
            DateTime contactreceiveddatetime, string presentingneed, string contactsummary, int persongroupawareofcontactid, int nextofkinawareofcontactid,
            DateTime contactaccepteddatetime, Guid contactoutcomeid)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(businessDataObject, "contacttypeid", contacttypeid);
            AddFieldToBusinessDataObject(businessDataObject, "contactreasonid", contactreasonid);
            AddFieldToBusinessDataObject(businessDataObject, "contactpresentingpriorityid", contactpresentingpriorityid);
            AddFieldToBusinessDataObject(businessDataObject, "contactstatusid", contactstatusid);
            AddFieldToBusinessDataObject(businessDataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(businessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(businessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(businessDataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(businessDataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(businessDataObject, "presentingneed", presentingneed);
            AddFieldToBusinessDataObject(businessDataObject, "persongroupawareofcontactid", persongroupawareofcontactid);
            AddFieldToBusinessDataObject(businessDataObject, "nextofkinawareofcontactid", nextofkinawareofcontactid);
            AddFieldToBusinessDataObject(businessDataObject, "contactsummary", contactsummary);
            AddFieldToBusinessDataObject(businessDataObject, "contactaccepteddatetime", contactaccepteddatetime);
            AddFieldToBusinessDataObject(businessDataObject, "contactoutcomeid", contactoutcomeid);
            AddFieldToBusinessDataObject(businessDataObject, "iscloned", false);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }


        public Dictionary<string, object> GetByID(Guid ContactId, params string[] FieldsToReturn)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, CareWorks.Foundation.Enums.ConditionOperatorType.Equal, ContactId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void Delete(Guid ContactId)
        {
            this.DeleteRecord(TableName, ContactId);
        }


    }
}
