using System;

namespace Phoenix.DBHelper.Models
{
    public class MashEpisode : BaseClass
    {
        public MashEpisode()
        {
            AuthenticateUser();
        }

        public MashEpisode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public string TableName { get { return "MashEpisode"; } }
        public string PrimaryKeyName { get { return "MashEpisodeId"; } }

        public Guid CreateMashEpisode(Guid regardingid, string regardingidtablename, string regardingidname, Guid PersonId, DateTime contactreceiveddatetime, Guid mashstatusid, Guid ownerid, Guid responsibleuserid, int persongroupawareofmashid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);

            AddFieldToBusinessDataObject(dataObject, "contactreceiveddatetime", contactreceiveddatetime);
            AddFieldToBusinessDataObject(dataObject, "mashstatusid", mashstatusid);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "responsibleuserid", responsibleuserid);
            AddFieldToBusinessDataObject(dataObject, "persongroupawareofmashid", persongroupawareofmashid);

            AddFieldToBusinessDataObject(dataObject, "ActiveCaseOnCreation", false);
            AddFieldToBusinessDataObject(dataObject, "ActiveGroupCaseOnCreation", false);
            AddFieldToBusinessDataObject(dataObject, "PersonHasActiveRelationshipOnCreation", false);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }


        public void UpdateDateTimeContactReceived(Guid MashEpisodeID, DateTime contactreceiveddatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, MashEpisodeID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contactreceiveddatetime", contactreceiveddatetime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCaseRecordedDatetime(Guid MashEpisodeID, DateTime? caserecordeddatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, MashEpisodeID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caserecordeddatetime", caserecordeddatetime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDateTimeInitialMASHRating(Guid MashEpisodeID, DateTime contactmashratingdatetime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, MashEpisodeID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "contactmashratingdatetime", contactmashratingdatetime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAdultMentalHealth(Guid MashEpisodeID, int adultmentalhealthid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, MashEpisodeID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "adultmentalhealthid", adultmentalhealthid);

            this.UpdateRecord(buisinessDataObject);
        }


    }
}
