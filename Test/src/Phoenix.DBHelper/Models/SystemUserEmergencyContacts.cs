using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Phoenix.DBHelper.Models
{
    public class SystemUserEmergencyContacts : BaseClass
    {

        public string TableName = "SystemUserEmergencyContacts";
        public string PrimaryKeyName = "SystemUserEmergencyContactsId";


        public SystemUserEmergencyContacts(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUserEmergencyContacts(Guid ownerId,
            Guid SystemUserId, Guid TitleId, string firstname, string lastname,
            DateTime startdate, bool nextofkin, string contacttelephoneprimary)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "TitleId", TitleId);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
            AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "nextofkin", nextofkin);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttelephoneprimary", contacttelephoneprimary);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateSystemUser_EmergencyContacts(Guid ownerId, Guid systemuserid, Guid owningbusinessunitid, string firstname, Guid titleid, string lastname, string contacttelephoneprimary, string contacttelephoneother1,
            string contacttelephoneother2, string contacttelephoneother3, Int32 nextofkin, DateTime startdate, DateTime? enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
            AddFieldToBusinessDataObject(buisinessDataObject, "systemuserid", systemuserid);
            AddFieldToBusinessDataObject(buisinessDataObject, "titleid", titleid);
            AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttelephoneprimary", contacttelephoneprimary);
            AddFieldToBusinessDataObject(buisinessDataObject, "nextofkin", nextofkin);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttelephoneother1", contacttelephoneother1);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttelephoneother2", contacttelephoneother2);
            AddFieldToBusinessDataObject(buisinessDataObject, "contacttelephoneother3", contacttelephoneother3);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetBySystemUserId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid SystemUserEmergencyContactsId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserEmergencyContactsId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteSystemUserEmergencyContacts(Guid SystemUserEmergencyContactsId)
        {
            this.DeleteRecord(TableName, SystemUserEmergencyContactsId);
        }

        public void DeleteSystemUserEmergencyContacts_AdoNetDirectConnection(Guid SystemUserEmergencyContactsId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from SystemUserEmergencyContacts where SystemUserEmergencyContactsId = @SystemUserEmergencyContactsId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@SystemUserEmergencyContactsId", SystemUserEmergencyContactsId);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
