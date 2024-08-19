using Microsoft.VisualStudio.TestTools.UnitTesting;
using Phoenix.UITests.Framework.FileSystem;
using Phoenix.DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareCloud.AdvanceFinancials.UITests
{
    public class CommonMethodsDB
    {
        private DatabaseHelper dbHelper;
        internal TestContext testContext;
        internal FileIOHelper fileIOHelper;

        public CommonMethodsDB(DatabaseHelper DBHelper)
        {
            dbHelper = DBHelper;
        }

        public CommonMethodsDB(DatabaseHelper _dbHelper, TestContext _testContext)
        {
            dbHelper = _dbHelper;
            testContext = _testContext;
        }

        public CommonMethodsDB(DatabaseHelper _dbHelper, FileIOHelper _fileIOHelper, TestContext _testContext)
        {
            dbHelper = _dbHelper;
            fileIOHelper = _fileIOHelper;
            testContext = _testContext;
        }

        public string UpdateSystemUserLastPasswordChange(string username, string dataEncoded)
        {
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);
            return username;
        }

        public Guid CreateBusinessUnit(string Name)
        {
            try
            {
                if (!dbHelper.businessUnit.GetByName(Name).Any())
                    dbHelper.businessUnit.CreateBusinessUnit(Name);
                return dbHelper.businessUnit.GetByName(Name)[0];
            }
            catch (Exception ex)
            {
                //we get a strange error where, for BUs, the create method throws an exception, but the record is actually created
                return dbHelper.businessUnit.GetByName(Name).FirstOrDefault();
            }
        }

        public Guid CreateProductLanguage(string LanguageName, string culturename, string currencysymbol, int lcid)
        {
            if (!dbHelper.productLanguage.GetProductLanguageIdByName(LanguageName).Any())
                dbHelper.productLanguage.CreateProductLanguage(LanguageName, culturename, currencysymbol, lcid);

            return dbHelper.productLanguage.GetProductLanguageIdByName(LanguageName)[0];
        }

        public Guid CreateTeam(string Name, Guid? TeamManagerId, Guid OwningBusinessUnitId, string Code, string EmailAddress, string Description, string PhoneNumber)
        {
            if (!dbHelper.team.GetTeamIdByName(Name).Any())
                dbHelper.team.CreateTeam(Name, TeamManagerId, OwningBusinessUnitId, Code, EmailAddress, Description, PhoneNumber);

            return dbHelper.team.GetTeamIdByName(Name)[0];

        }

        public Guid CreateTeam(Guid TeamId, string Name, Guid? TeamManagerId, Guid OwningBusinessUnitId, string Code, string EmailAddress, string Description, string PhoneNumber)
        {
            if (!dbHelper.team.GetTeamIdByName(Name).Any())
                dbHelper.team.CreateTeam(TeamId, Name, TeamManagerId, OwningBusinessUnitId, Code, EmailAddress, Description, PhoneNumber);

            return dbHelper.team.GetTeamIdByName(Name)[0];

        }

        public Guid CreateSystemUserRecord(string username, string firstName, string lastName, string password, Guid businessUnitId, Guid teamId, Guid _languageId, Guid _authenticationproviderid)
        {
            #region System User

            Guid _userId = Guid.Empty;

            if (!dbHelper.systemUser.GetSystemUserByUserName(username).Any())
            {
                _userId = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, firstName + " " + lastName, password, username + "@somemail.com", username + "@otheremail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamId, DateTime.Now.Date, false, true, null, 4);
                dbHelper.systemUser.UpdateEmployeeTypeId(_userId, 4);

                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var secureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                CreateUserSecurityProfile(_userId, systemAdministratorSecurityProfileId);
                CreateUserSecurityProfile(_userId, secureFieldsSecurityProfileId);
            }

            if (_userId == Guid.Empty)
                _userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();

            TimeZone localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_userId, localZone.StandardName);
            dbHelper.systemUser.UpdateLastPasswordChangedDate(_userId, DateTime.UtcNow);

            return _userId;

            #endregion
        }

        public Guid CreateUserSecurityProfile(Guid userId, Guid securityProfileId)
        {
            #region System User Security Profile

            if (!dbHelper.userSecurityProfile.GetByUserIDAndProfileId(userId, securityProfileId).Any())
                return dbHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);

            return dbHelper.userSecurityProfile.GetByUserIDAndProfileId(userId, securityProfileId).First();

            #endregion
        }

        public Guid CreateProvider(string name, Guid ownerid, int providertypeid, bool enablescheduling)
        {
            if (!dbHelper.provider.GetProviderByName(name).Any())
                dbHelper.provider.CreateProvider(name, ownerid, providertypeid, enablescheduling);
            return dbHelper.provider.GetProviderByName(name)[0];
        }

        public Guid CreateEthnicity(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.ethnicity.GetEthnicityIdByName(Name).Any())
                dbHelper.ethnicity.CreateEthnicity(OwnerId, Name, StartDate);
            return dbHelper.ethnicity.GetEthnicityIdByName(Name)[0];
        }

        public Guid CreatePersonRecord(string FirstName, string LastName, Guid EthnicityId, Guid TeamID, DateTime DOB)
        {
            #region Person
            Guid _personID = Guid.Empty;

            if (!dbHelper.person.GetByFirstAndLastName(FirstName, LastName).Any())
            {
                _personID = dbHelper.person.CreatePersonRecord("", FirstName, "", LastName, "", DOB, EthnicityId, TeamID, 7, 2);
                System.Threading.Thread.Sleep(2000);
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstAndLastName(FirstName, LastName).FirstOrDefault();
            }
            return _personID;

            #endregion
        }

        public Guid CreateCareProviderService(Guid ownerid, string Name, DateTime startdate, int code, DateTime? enddate = null, bool IsScheduledService = false, bool validforexport = false, bool inactive = false)
        {
            if (!dbHelper.careProviderService.GetByName(Name).Any())
                dbHelper.careProviderService.CreateCareProviderService(ownerid, Name, startdate, code, enddate, IsScheduledService, validforexport, inactive);
            return dbHelper.careProviderService.GetByName(Name)[0];
        }

        public Guid CreateCareProviderServiceDetail(Guid ownerid, string Name, int? code, int? govcode, DateTime startdate, DateTime? enddate = null, bool inactive = false, bool validforexport = false)
        {
            if (!dbHelper.careProviderServiceDetail.GetByName(Name).Any())
                return dbHelper.careProviderServiceDetail.CreateCareProviderServiceDetail(ownerid, Name, code, govcode, startdate, enddate, inactive, validforexport);
            return dbHelper.careProviderServiceDetail.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateCareProviderServiceMapping(Guid ServiceId, Guid ownerid, Guid? ServiceDetailId, Guid? BookingTypeId, Guid? FinanceCodeId, string NoteText, bool inactive = false)
        {
            if (!dbHelper.careProviderServiceMapping.GetByServiceAndServiceDetailAndBookingType(ServiceId, ServiceDetailId, BookingTypeId).Any())
                return dbHelper.careProviderServiceMapping.CreateCareProviderServiceMapping(ServiceId, ownerid, ServiceDetailId, BookingTypeId, FinanceCodeId, NoteText, inactive);
            return dbHelper.careProviderServiceMapping.GetByServiceAndServiceDetailAndBookingType(ServiceId, ServiceDetailId, BookingTypeId).FirstOrDefault();
        }

        public Guid CreateCareProviderExtractName(Guid OwnerId, string Name, int? Code, string GovCode = null, DateTime? StartDate = null, DateTime? EndDate = null, bool Inactive = false, bool ValidForExport = false)
        {
            if (!dbHelper.careProviderExtractName.GetByName(Name).Any())
                return dbHelper.careProviderExtractName.CreateCareProviderExtractName(OwnerId, Name, Code, GovCode, StartDate, EndDate, Inactive, ValidForExport);
            return dbHelper.careProviderExtractName.GetByName(Name)[0];
        }

        public Guid CreateCareProviderFinanceExtractBatchSetup(Guid ownerid,
            DateTime startdate, TimeSpan starttime, DateTime? enddate, Guid careproviderextractnameid, int extractfrequencyid,
            Guid careproviderextracttypeid, Guid mailmergeid, Guid? EmailSenderId = null, string EmailSenderIdTableName = "", string EmailSenderIdName = "", bool extractonmonday = false, bool extractontuesday = false, bool extractonwednesday = false, bool extractonthursday = false,
            bool extractonfriday = false, bool extractonsaturday = false, bool extractonsunday = false,
            bool excludezerotransactions = true, bool excludezerotransactionsfrominvoice = true, bool generateandsendinvoicesautomatically = false,
            int CPFinanceExtractBatchPaymentTermsId = 1, int PaymentTermUnits = 1, bool showroomnumber = false, bool showinvoicetext = true, bool showweeklybreakdown = true,
            int cpfinanceextractbatchpaymentdetailid = 1, string vatglcode = "", int invoicetransactionsgroupingid = 2, string extractreference = "")
        {
            if (!dbHelper.careProviderFinanceExtractBatchSetup.GetByCareProviderExtractNameId(careproviderextractnameid).Any())
                return dbHelper.careProviderFinanceExtractBatchSetup.CreateCareProviderFinanceExtractBatchSetup(ownerid,
                    startdate, starttime, enddate, careproviderextractnameid, extractfrequencyid, careproviderextracttypeid, mailmergeid,
                    EmailSenderId, EmailSenderIdTableName, EmailSenderIdName,
                    extractonmonday, extractontuesday, extractonwednesday, extractonthursday, extractonfriday, extractonsaturday, extractonsunday,
                    excludezerotransactions, excludezerotransactionsfrominvoice, generateandsendinvoicesautomatically,
                    CPFinanceExtractBatchPaymentTermsId, PaymentTermUnits, showroomnumber, showinvoicetext, showweeklybreakdown,
                    cpfinanceextractbatchpaymentdetailid, vatglcode, invoicetransactionsgroupingid, extractreference);

            return dbHelper.careProviderFinanceExtractBatchSetup.GetByCareProviderExtractNameId(careproviderextractnameid).FirstOrDefault();
        }

        public Guid CreateCareProviderContractScheme(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string name, DateTime startdate, int code, Guid EstablishmentId, Guid FunderId)
        {
            if (!dbHelper.careProviderContractScheme.GetCareProviderContractSchemeByName(name).Any())
                return dbHelper.careProviderContractScheme.CreateCareProviderContractScheme(ownerid, responsibleuserid, owningbusinessunitid, name, startdate, code, EstablishmentId, FunderId);
            return dbHelper.careProviderContractScheme.GetCareProviderContractSchemeByName(name)[0];
        }
    }
}
