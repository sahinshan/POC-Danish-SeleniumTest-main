using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemUser : BaseClass
    {

        public string TableName = "SystemUser";
        public string PrimaryKeyName = "SystemUserId";


        public SystemUser(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSystemUser(
            string username, string firstname, string lastname, string fullname, string password, string workemail, string secureemailaddress, string timezoneid,
            DateTime? startdate, int? addresstypeid,
            Guid languageid, Guid authenticationproviderid, Guid owningbusinessunitid, Guid defaultteamid, bool ismanager = false, int employeetypeid = 4, int? maximumworkinghours = null, DateTime? availablefrom = null, int persongenderid = 1)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            try
            {
                if (!availablefrom.HasValue)
                    availablefrom = DateTime.Now.Date;

                AddFieldToBusinessDataObject(buisinessDataObject, "username", username);
                AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
                AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);
                AddFieldToBusinessDataObject(buisinessDataObject, "fullname", fullname);
                AddFieldToBusinessDataObject(buisinessDataObject, "password", password);
                AddFieldToBusinessDataObject(buisinessDataObject, "workemail", workemail);
                AddFieldToBusinessDataObject(buisinessDataObject, "secureemailaddress", secureemailaddress);
                AddFieldToBusinessDataObject(buisinessDataObject, "timezoneid", timezoneid);
                AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
                AddFieldToBusinessDataObject(buisinessDataObject, "languageid", languageid);
                AddFieldToBusinessDataObject(buisinessDataObject, "authenticationproviderid", authenticationproviderid);
                AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
                AddFieldToBusinessDataObject(buisinessDataObject, "defaultteamid", defaultteamid);
                AddFieldToBusinessDataObject(buisinessDataObject, "recordsperpageid", 100);
                AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", addresstypeid);
                AddFieldToBusinessDataObject(buisinessDataObject, "canworkoffline", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "failedpasswordattemptcount", 0);
                AddFieldToBusinessDataObject(buisinessDataObject, "isaccountlocked", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "isserviceaccount", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "ismanager", ismanager);
                AddFieldToBusinessDataObject(buisinessDataObject, "disableformautosave", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "cancompletednar", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthorisednar", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "islegitimaterelationshipenabled", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthoriseform", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthoriseonbehalfofothers", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "pushappointments", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "workinmultipleteams", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "updatesysuseraddress", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "employeetypeid", employeetypeid);
                AddFieldToBusinessDataObject(buisinessDataObject, "maximumworkinghours", maximumworkinghours);
                AddFieldToBusinessDataObject(buisinessDataObject, "availablefrom", availablefrom);
                AddFieldToBusinessDataObject(buisinessDataObject, "persongenderid", persongenderid);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return this.CreateRecord(buisinessDataObject);
        }


        public Guid CreateSystemUser(
            string username, string firstname, string lastname, string fullname, string password, string workemail, string secureemailaddress, string timezoneid,
            DateTime? startdate, int? addresstypeid,
            Guid languageid, Guid authenticationproviderid, Guid owningbusinessunitid, Guid defaultteamid, DateTime? LastPasswordChangedDate,
            bool workinmultipleteams = false, bool ismanager = true, DateTime? availablefrom = null, int? employeetypeid = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            try
            {
                if (!availablefrom.HasValue)
                    availablefrom = DateTime.Now.Date;

                AddFieldToBusinessDataObject(buisinessDataObject, "username", username);
                AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
                AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);
                AddFieldToBusinessDataObject(buisinessDataObject, "fullname", fullname);
                AddFieldToBusinessDataObject(buisinessDataObject, "password", password);
                AddFieldToBusinessDataObject(buisinessDataObject, "workemail", workemail);
                AddFieldToBusinessDataObject(buisinessDataObject, "secureemailaddress", secureemailaddress);
                AddFieldToBusinessDataObject(buisinessDataObject, "timezoneid", timezoneid);
                AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
                AddFieldToBusinessDataObject(buisinessDataObject, "languageid", languageid);
                AddFieldToBusinessDataObject(buisinessDataObject, "authenticationproviderid", authenticationproviderid);
                AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
                AddFieldToBusinessDataObject(buisinessDataObject, "defaultteamid", defaultteamid);
                AddFieldToBusinessDataObject(buisinessDataObject, "recordsperpageid", 100);
                AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", addresstypeid);
                AddFieldToBusinessDataObject(buisinessDataObject, "canworkoffline", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "failedpasswordattemptcount", 0);
                AddFieldToBusinessDataObject(buisinessDataObject, "isaccountlocked", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "isserviceaccount", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "ismanager", ismanager);
                AddFieldToBusinessDataObject(buisinessDataObject, "disableformautosave", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "cancompletednar", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthorisednar", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "islegitimaterelationshipenabled", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthoriseform", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthoriseonbehalfofothers", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "pushappointments", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "workinmultipleteams", workinmultipleteams);
                AddFieldToBusinessDataObject(buisinessDataObject, "updatesysuseraddress", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "LastPasswordChangedDate", LastPasswordChangedDate);
                AddFieldToBusinessDataObject(buisinessDataObject, "availablefrom", availablefrom);

                if (employeetypeid.HasValue)
                    AddFieldToBusinessDataObject(buisinessDataObject, "employeetypeid", employeetypeid.Value);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreateSystemUser(Guid SystemUserId,
           string username, string firstname, string lastname, string fullname, string password, string workemail, string secureemailaddress, string timezoneid,
           DateTime? startdate, int? addresstypeid,
           Guid languageid, Guid authenticationproviderid, Guid owningbusinessunitid, Guid defaultteamid, DateTime? LastPasswordChangedDate, bool ismanager = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            try
            {
                AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);

                AddFieldToBusinessDataObject(buisinessDataObject, "username", username);
                AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
                AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);
                AddFieldToBusinessDataObject(buisinessDataObject, "fullname", fullname);
                AddFieldToBusinessDataObject(buisinessDataObject, "password", password);
                AddFieldToBusinessDataObject(buisinessDataObject, "workemail", workemail);
                AddFieldToBusinessDataObject(buisinessDataObject, "secureemailaddress", secureemailaddress);
                AddFieldToBusinessDataObject(buisinessDataObject, "timezoneid", timezoneid);
                AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
                AddFieldToBusinessDataObject(buisinessDataObject, "languageid", languageid);
                AddFieldToBusinessDataObject(buisinessDataObject, "authenticationproviderid", authenticationproviderid);
                AddFieldToBusinessDataObject(buisinessDataObject, "owningbusinessunitid", owningbusinessunitid);
                AddFieldToBusinessDataObject(buisinessDataObject, "defaultteamid", defaultteamid);
                AddFieldToBusinessDataObject(buisinessDataObject, "recordsperpageid", 100);
                AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", addresstypeid);
                AddFieldToBusinessDataObject(buisinessDataObject, "canworkoffline", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "failedpasswordattemptcount", 0);
                AddFieldToBusinessDataObject(buisinessDataObject, "isaccountlocked", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "isserviceaccount", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "ismanager", ismanager);
                AddFieldToBusinessDataObject(buisinessDataObject, "disableformautosave", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "cancompletednar", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthorisednar", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "islegitimaterelationshipenabled", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthoriseform", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "canauthoriseonbehalfofothers", true);
                AddFieldToBusinessDataObject(buisinessDataObject, "pushappointments", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "workinmultipleteams", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "updatesysuseraddress", false);
                AddFieldToBusinessDataObject(buisinessDataObject, "LastPasswordChangedDate", LastPasswordChangedDate);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetSystemUserByUserName(string UserName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserName", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, UserName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetSystemUserByUserNameContains(string UserName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserName", CareWorks.Foundation.Enums.ConditionOperatorType.Contains, UserName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByDefaultTeamId(Guid TeamID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "defaultteamid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, TeamID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByDefaultTeamId(string username, Guid TeamID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "username", CareWorks.Foundation.Enums.ConditionOperatorType.StartsWith, username);
            this.BaseClassAddTableCondition(query, "defaultteamid", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, TeamID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetSystemUserByUserNamePrefix(string UserName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserName", CareWorks.Foundation.Enums.ConditionOperatorType.Like, UserName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteSystemUser(Guid SystemUserId)
        {
            this.DeleteRecord(TableName, SystemUserId);
        }

        public void UpdatePassword(Guid SystemUserId, string password)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "password", password);

            this.UpdateRecord(buisinessDataObject);
        }


        public void UpdateJobRoleType(Guid SystemUserId, Guid jobroletypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "jobroletypeid", jobroletypeid);

            this.UpdateRecord(buisinessDataObject);
        }
        public void UpdateSAWeek1CycleStartDate(Guid SystemUserId, DateTime SAWeek1CycleStartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SAWeek1CycleStartDate", SAWeek1CycleStartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSATransportWeek1CycleStartDate(Guid SystemUserId, DateTime SATransportWeek1CycleStartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SATransportWeek1CycleStartDate", SATransportWeek1CycleStartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateLastPasswordChangedDate(Guid SystemUserId, DateTime LastPasswordChangedDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);
            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "LastPasswordChangedDate", LastPasswordChangedDate);
            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateLinkedAddress(Guid SystemUserId, Guid? linkedaddressid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "linkedaddressid", linkedaddressid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDefaultTeam(Guid SystemUserId, Guid defaultteamid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "defaultteamid", defaultteamid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCommentLegacyField(Guid SystemUserId, string comment)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "comment", comment);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateProfessionType(Guid SystemUserId, Guid professiontypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "professiontypeid", professiontypeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAddressFields(Guid SystemUserId, DateTime startdate, int addresstypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", addresstypeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateIsAccountLocked(Guid SystemUserId, bool isaccountlocked)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "isaccountlocked", isaccountlocked);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFailedPasswordAttemptCount(Guid SystemUserId, int? failedpasswordattemptcount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "failedpasswordattemptcount", failedpasswordattemptcount);

            this.UpdateRecord(buisinessDataObject);
        }



        public void ClearAddressFields(Guid SystemUserId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "propertyname", null);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline1", null); //Property No
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline2", null); //Street
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline3", null); //Village / District
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline4", null); //Town / City
            AddFieldToBusinessDataObject(buisinessDataObject, "postcode", null);
            AddFieldToBusinessDataObject(buisinessDataObject, "addressline5", null); //County
            AddFieldToBusinessDataObject(buisinessDataObject, "country", null);
            AddFieldToBusinessDataObject(buisinessDataObject, "addresstypeid", null);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", null);
            AddFieldToBusinessDataObject(buisinessDataObject, "LinkedAddressId", null);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteSystemUser_AdoNetDirectConnection(Guid SystemUserId)
        {
            Dictionary<string, object> dataToReturn = new Dictionary<string, object>();

            string connectionString = @"Data Source=cduktest01;Initial Catalog=CareProviders_CD;User ID=automationtestuser;Password=Passw0rd_!";

            string queryString = "delete from SystemUser where SystemUserId = @UserID";


            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@UserID", SystemUserId);

                try
                {
                    connection.Open();
                    var count = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public Dictionary<string, object> GetSystemUserBySystemUserID(Guid SystemUserId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetLinkedAddressByUserName(string UserName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "UserName", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, UserName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateEmployeeTypeId(Guid SystemUserId, int EmployeeTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "employeetypeid", EmployeeTypeId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSystemUserTimezone(Guid SystemUserId, string TimeZoneId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "TimeZoneId", TimeZoneId);

            this.UpdateRecord(buisinessDataObject);

        }

        public void UpdateisManagerFld(Guid SystemUserId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ismanager", true);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePronoun(Guid SystemUserID, Guid pronounsid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "pronounsid", pronounsid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStatedGender(Guid SystemUserId, int persongenderid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "persongenderid", persongenderid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateMaximumWorkingHours(Guid SystemUserId, int maximumworkinghours)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemUserId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "maximumworkinghours", maximumworkinghours);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
