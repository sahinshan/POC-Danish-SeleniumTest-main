using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    //[TestClass]
    public class MobileApp_Scripts : FunctionalTest
    {
        private Guid _defaultLoginUserID;
        private string username;

        //[TestInitialize()]
        public void MobileApp_SetupTest()
        {
            try
            {
                #region Authentication Data

                username = ConfigurationManager.AppSettings["Username"];
                var password = ConfigurationManager.AppSettings["Password"];
                var DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                if (DataEncoded.Equals("true"))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(username);
                    username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                    base64EncodedBytes = System.Convert.FromBase64String(password);
                    password = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                }

                _defaultLoginUserID = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(_defaultLoginUserID, DateTime.Now.Date);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }


        //[TestMethod]
        public void MobileApp_Scripts_UITestMethod01()
        {
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Default Team

            var _defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Internal

            var _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Language

            var _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_defaultTeamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Salaried")[0];

            #endregion

            #region Care plan need domain

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").First();

            #endregion

            #region Alert And Hazard Type

            var _alertAndHazardsType = commonMethodsDB.CreateAlertAndHazardType(_defaultTeamId, "Dangerous Dog", new DateTime(2010, 1, 1));

            #endregion

            #region Allergy Type

            var _allergyTypeId = dbHelper.allergyType.GetByName("Food").First();

            #endregion

            var numberOfBUs = 10;
            var numberOfTeams = 10;
            var totalTeams = 1;

            for (int a = 1; a <= numberOfBUs; a++)
            {
                #region Business Unit

                var _businessUnitId = commonMethodsDB.CreateBusinessUnit("Mobile BU " + a);

                #endregion

                for (int b = 0; b < numberOfTeams; b++)
                {
                    #region Team

                    var _teamName = "Mobile Team " + totalTeams;
                    var _teamId = commonMethodsDB.CreateTeam(_teamName, null, _businessUnitId, Guid.NewGuid().ToString(), "mobileteam" + totalTeams + "@careworkstempmail.com", _teamName, "020 123456");
                    totalTeams++;

                    #endregion

                    for (int c = 0; c < 5; c++)
                    {
                        var currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

                        #region Person

                        var _personFirstName = GetRandomFirstName();
                        var _personLastName = GetRandomLastName();
                        var _personFullName = _personFirstName + " " + _personLastName;
                        var _personID = commonMethodsDB.CreatePersonRecord(_personFirstName, _personLastName, _ethnicityId, _teamId);

                        #endregion

                        #region Provider

                        var providerName = "Provider " + currentTimeString;
                        var addressType = 10; //Home
                        var providerId = commonMethodsDB.CreateProvider(providerName, _teamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

                        var funderProviderName = "Funder Provider " + currentTimeString;
                        var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _teamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

                        #endregion

                        #region Care Provider Contract Scheme

                        var contractScheme1Name = "CPCS_" + currentTimeString;
                        var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
                        var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_teamId, _defaultLoginUserID, _businessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

                        #endregion

                        #region Person Contract

                        var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_teamId, "title", _personID, _defaultLoginUserID, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday.AddDays(-7), null, true);

                        #endregion

                        #region Person Alert And Hazards

                        var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _teamId, _teamName, _personID, _personFullName, _alertAndHazardsType, "Dangerous Dog", null, null, thisWeekMonday, null, 4);

                        #endregion

                        #region Person Allergy

                        var _personAllergyId = dbHelper.personAllergy.CreatePersonAllergy(_teamId, false, _personID, _personFullName, _allergyTypeId, "Food", "details ...", thisWeekMonday, null, "Food allergy ....", 1);

                        #endregion

                        #region System User

                        var _userName = "mobileuser" + currentTimeString;
                        var _systemUserId = commonMethodsDB.CreateSystemUserRecord(_userName, "Mobile User", currentTimeString, "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                        #endregion

                        #region System User Employment Contract

                        var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _staffRoleTypeid, _teamId, _employmentContractTypeId);

                        #endregion

                        #region Careplan

                        var _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanNeedDomainId, _teamId, 1, _businessUnitId, DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), "expected outcome", _personID, "currentsituation", _systemUserId, 1);

                        #endregion
                    }
                }
            }
        }

        //[TestMethod]
        public void MobileApp_Scripts_UITestMethod02()
        {
            var thisWeekMonday = commonMethodsHelper.GetThisWeekFirstMonday();

            #region Default Team

            var _defaultTeamId = dbHelper.team.GetFirstTeams(1, 1, true).First();
            var _defaultTeamName = (string)(dbHelper.team.GetTeamByID(_defaultTeamId, "name")["name"]);
            var _defaultBusinessUnitId = (Guid)(dbHelper.team.GetTeamByID(_defaultTeamId, "owningbusinessunitid")["owningbusinessunitid"]);

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_defaultTeamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Internal

            var _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Language

            var _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Care provider staff role type

            var _staffRoleTypeid = commonMethodsDB.CreateCareProviderStaffRoleType(_defaultTeamId, "Cook", "", "", new DateTime(2000, 1, 1), "");

            #endregion

            #region Employment Contract Type

            var _employmentContractTypeId = dbHelper.employmentContractType.GetByName("Salaried")[0];

            #endregion

            #region Care plan need domain

            var _carePlanNeedDomainId = dbHelper.personCarePlanNeedDomain.GetByName("Acute").First();

            #endregion

            #region Alert And Hazard Type

            var _alertAndHazardsType = commonMethodsDB.CreateAlertAndHazardType(_defaultTeamId, "Dangerous Dog", new DateTime(2010, 1, 1));

            #endregion

            #region Allergy Type

            var _allergyTypeId = dbHelper.allergyType.GetByName("Food").First();

            #endregion

            #region System User

            var _systemUserId = commonMethodsDB.CreateSystemUserRecord("store_test_user", "store", "test user", "Passw0rd_!", _defaultBusinessUnitId, _defaultTeamId, _languageId, _authenticationproviderid);

            #endregion

            #region System User Employment Contract

            var _systemUserEmploymentContractId = dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(_systemUserId, new DateTime(2022, 1, 1), _staffRoleTypeid, _defaultTeamId, _employmentContractTypeId);

            #endregion

            for (int c = 0; c < 20; c++)
            {
                var currentTimeString = DateTime.Now.ToString("yyyyMMddHHmmss");

                #region Person

                var _personFirstName = GetRandomFirstName();
                var _personLastName = GetRandomLastName();
                var _personFullName = _personFirstName + " " + _personLastName;
                var _personID = commonMethodsDB.CreatePersonRecord(_personFirstName, _personLastName, _ethnicityId, _defaultTeamId);

                #endregion

                #region Provider

                var providerName = "Provider " + currentTimeString;
                var addressType = 10; //Home
                var providerId = commonMethodsDB.CreateProvider(providerName, _defaultTeamId, 13, true, new DateTime(2020, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Residential Establishment

                var funderProviderName = "Funder Provider " + currentTimeString;
                var funderProviderID = commonMethodsDB.CreateProvider(funderProviderName, _defaultTeamId, 10, false, new DateTime(2023, 1, 1), addressType, "pna", "pno", "st", "dst", "tw", "co", "CR0 3RL"); //Local Authority

                #endregion

                #region Care Provider Contract Scheme

                var contractScheme1Name = "CPCS_" + currentTimeString;
                var contractScheme1Code = dbHelper.careProviderContractScheme.GetHighestCode() + 1;
                var careProviderContractScheme1Id = commonMethodsDB.CreateCareProviderContractScheme(_defaultTeamId, _systemUserId, _defaultBusinessUnitId, contractScheme1Name, new DateTime(2020, 1, 1), contractScheme1Code, providerId, funderProviderID);

                #endregion

                #region Person Contract

                var _personcontractId = dbHelper.careProviderPersonContract.CreateCareProviderPersonContract(_defaultTeamId, "title", _personID, _systemUserId, providerId, careProviderContractScheme1Id, funderProviderID, thisWeekMonday.AddDays(-7), null, true);

                #endregion

                #region Person Alert And Hazards

                var personAlertAndHazards = dbHelper.personAlertAndHazard.CreatePersonAlertAndHazard(1, "Person Alert and Hazards 001", _defaultTeamId, _defaultTeamName, _personID, _personFullName, _alertAndHazardsType, "Dangerous Dog", null, null, thisWeekMonday, null, 4);

                #endregion

                #region Person Allergy

                var _personAllergyId = dbHelper.personAllergy.CreatePersonAllergy(_defaultTeamId, false, _personID, _personFullName, _allergyTypeId, "Food", "details ...", thisWeekMonday, null, "Food allergy ....", 1);

                #endregion

                #region Careplan

                var _personCarePlanID = dbHelper.personCarePlan.CreatePersonCarePlan(_carePlanNeedDomainId, _defaultTeamId, 1, _defaultBusinessUnitId, DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), "expected outcome", _personID, "currentsituation", _systemUserId, 1);

                #endregion
            }


        }

        private string GetRandomFirstName()
        {
            // List of sample first names
            List<string> firstNames = new List<string>
            {
                "John", "Jane", "Michael", "Emily", "Chris", "Anna", "James", "Jessica", "David", "Laura",
                "Robert", "Mary", "William", "Patricia", "Charles", "Linda", "Joseph", "Barbara", "Thomas", "Susan",
                "Daniel", "Margaret", "Matthew", "Sarah", "Anthony", "Karen", "Mark", "Nancy", "Donald", "Lisa",
                "Paul", "Betty", "Steven", "Sandra", "Andrew", "Ashley", "Kenneth", "Dorothy", "Joshua", "Kimberly",
                "Kevin", "Donna", "Brian", "Carol", "George", "Michelle", "Edward", "Amanda", "Ronald", "Melissa"
            };

            // Create a random number generator
            Random random = new Random();

            // Generate random name
            return firstNames[random.Next(firstNames.Count)];
        }

        private string GetRandomLastName()
        {
            // List of sample first names
            List<string> lastNames = new List<string>
            {
                "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Hernandez",
                "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin", "Lee",
                "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker",
                "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green",
                "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez"
            };

            // Create a random number generator
            Random random = new Random();

            // Generate random name
            return lastNames[random.Next(lastNames.Count)];
        }

    }
}
