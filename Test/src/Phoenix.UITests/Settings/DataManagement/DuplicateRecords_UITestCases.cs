using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Settings.DataManagement
{
    [TestClass]
    public class DuplicateRecords_UITestCases : FunctionalTest
    {

        private Guid MergeRecords_ScheduleJobID = new Guid("52736669-28cb-e911-a2c9-0050569231cf"); //Merge Records

        #region issue https://advancedcsg.atlassian.net/browse/CDV6-9095

        [Description("Testing Feature to pick subordinate fields to be taken to master record in merge - " +
            "Access the Duplicate Records window - Open a Duplicate Record with 2 duplicates (a master and a subordinate with different data) - Select the 2 duplicates records - Click on the Merge Duplicate button - " +
            "Wait for the Merge Record dialog to be displayed - Select the Master record as the main record - " +
            "Select the Master record as the main record - Select Address Type, Adult Safeguarding Flag, Age, Age Group, Allergies Not Recorded, Allow Email to be copied from the subordinate record - " +
            "Click on the merge button - Execute the Merge scheduled job - Validate that the select data was copied from the subordinate record to the master record")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24562")]
        public void DuplicateRecords_UITestMethod01()
        {
            var duplicateDetectionRule = new Guid("e31e376f-a7ab-ea11-a2cd-005056926fe4"); //First Name, Gender, DOB

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid Ethnicity_WhiteIrish = dbHelper.ethnicity.GetEthnicityIdByName("White Irish")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AddressTypeId_Home = 6;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Home, AccommodationStatusId, LivesAloneTypeId, GenderId);



            //get person Numbers
            var personNumberMaster = (int)dbHelper.person.GetPersonById(_personIDMaster, "personnumber")["personnumber"];
            var personNumberSubordinate = (int)dbHelper.person.GetPersonById(_personIDSubordinate, "personnumber")["personnumber"];

            //update the subordinate record
            dbHelper.person.UpdateAdultSafeguarding(_personIDSubordinate, true);
            dbHelper.person.UpdateAge(_personIDSubordinate, 33);
            dbHelper.person.UpdateAgeGroupId(_personIDSubordinate, 5); //25-35
            dbHelper.person.UpdateAllergiesNotRecorded(_personIDSubordinate, false);
            dbHelper.person.UpdateAllowEmail(_personIDSubordinate, true);
            dbHelper.person.UpdateAllowPhone(_personIDSubordinate, true);
            dbHelper.person.UpdateAllowSMS(_personIDSubordinate, true);
            dbHelper.person.UpdateRetainInformationConcern(_personIDSubordinate, true);
            dbHelper.person.UpdateDeceased(_personIDSubordinate, true);
            dbHelper.person.UpdateDateOfBirth(_personIDSubordinate, DateTime.Now.AddYears(-33).Date);
            dbHelper.person.UpdateEthnicityId(_personIDSubordinate, Ethnicity_WhiteIrish);
            dbHelper.person.UpdateInterpreterRequired(_personIDSubordinate, true);
            dbHelper.person.UpdateIsExternalPerson(_personIDSubordinate, true);
            dbHelper.person.UpdateIsLookedAfterChild(_personIDSubordinate, true);
            dbHelper.person.UpdateKnownAllergies(_personIDSubordinate, true);
            dbHelper.person.UpdateNHSNumber(_personIDSubordinate, "987 654 3210");
            dbHelper.person.UpdateNHSNumberStatusId(_personIDSubordinate, 1); //Number present and verified
            dbHelper.person.UpdateNoKnownAllergies(_personIDSubordinate, true);
            dbHelper.person.UpdatePDSIsDeferred(_personIDSubordinate, true);
            dbHelper.person.UpdatePDSNHSNoSuperseded(_personIDSubordinate, true);
            dbHelper.person.UpdateRecordedInError(_personIDSubordinate, true);
            dbHelper.person.UpdateRelatedAdultSafeguardingFlag(_personIDSubordinate, true);
            dbHelper.person.UpdateRepresentAlertOrHazard(_personIDSubordinate, true);
            dbHelper.person.UpdateChildProtectionFlag(_personIDSubordinate, true);
            dbHelper.person.UpdateSuppressStatementInvoices(_personIDSubordinate, true);

            //update the Master record
            dbHelper.person.UpdateAdultSafeguarding(_personIDMaster, false);
            dbHelper.person.UpdateAllergiesNotRecorded(_personIDMaster, true);
            dbHelper.person.UpdateAllowEmail(_personIDMaster, false);
            dbHelper.person.UpdateAllowPhone(_personIDMaster, false);
            dbHelper.person.UpdateAllowSMS(_personIDMaster, false);
            dbHelper.person.UpdateRetainInformationConcern(_personIDMaster, false);
            dbHelper.person.UpdateDeceased(_personIDMaster, false);
            dbHelper.person.UpdateInterpreterRequired(_personIDMaster, false);
            dbHelper.person.UpdateIsExternalPerson(_personIDMaster, false);
            dbHelper.person.UpdateIsLookedAfterChild(_personIDMaster, false);
            dbHelper.person.UpdateKnownAllergies(_personIDMaster, false);
            dbHelper.person.UpdateNHSNumber(_personIDMaster, "987 654 3222");
            dbHelper.person.UpdateNHSNumberStatusId(_personIDMaster, 2); //Number present but not traced
            dbHelper.person.UpdateNoKnownAllergies(_personIDMaster, false);
            dbHelper.person.UpdatePDSIsDeferred(_personIDMaster, false);
            dbHelper.person.UpdatePDSNHSNoSuperseded(_personIDMaster, false);
            dbHelper.person.UpdateRecordedInError(_personIDMaster, false);
            dbHelper.person.UpdateRelatedAdultSafeguardingFlag(_personIDMaster, false);
            dbHelper.person.UpdateRepresentAlertOrHazard(_personIDMaster, false);
            dbHelper.person.UpdateChildProtectionFlag(_personIDMaster, false);
            dbHelper.person.UpdateSuppressStatementInvoices(_personIDMaster, false);


            //create DuplicateRecord
            var duplicateRecordId = dbHelper.duplicateRecord.CreateDuplicateRecord(OwnerID, FirstName + " Master", duplicateDetectionRule, 2, _personIDMaster, "person", FirstName + " Master");

            //Create SubordinateDuplicate
            var subordinateDuplicateID1 = dbHelper.subordinateDuplicate.CreateProfessionalSubordinateDuplicate(OwnerID, FirstName + " Master", duplicateRecordId, _personIDMaster, "person", FirstName + " Master", personNumberMaster.ToString(), false);
            var subordinateDuplicateID2 = dbHelper.subordinateDuplicate.CreateProfessionalSubordinateDuplicate(OwnerID, FirstName + " Subordinate", duplicateRecordId, _personIDSubordinate, "person", FirstName + " Subordinate", personNumberSubordinate.ToString(), false);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickDuplicateRecordsButton();

            duplicateRecordsPage
                .WaitForDuplicateRecordsPageToLoad()
                .InsertSearchQuery(FirstName)
                .ClickSearchButton()
                .CliickOnRecord(duplicateRecordId.ToString());

            duplicateRecordRecordPage
                .WaitForDuplicateRecordRecordPageToLoad()

                .WaitForSubordinateDuplicatesAreaToLoad()
                .ClickSubordinateDuplicates_RecordCheckBox(subordinateDuplicateID1.ToString())
                .ClickSubordinateDuplicates_RecordCheckBox(subordinateDuplicateID2.ToString())
                .ClickSubordinateDuplicates_MergeDuplicateButton();

            duplicateRecordMergeDialog
                .WaitForDuplicateRecordMergeDialogToLoad()
                .SelectMainRecord(subordinateDuplicateID1.ToString())
                .Clickadultsafeguarding_Checkbox(subordinateDuplicateID2.ToString())
                .Clickage_Checkbox(subordinateDuplicateID2.ToString())
                .Clickagegroup_Checkbox(subordinateDuplicateID2.ToString())
                .Clickallergiesnotrecorded_Checkbox(subordinateDuplicateID2.ToString())
                .Clickallowemail_Checkbox(subordinateDuplicateID2.ToString())
                .ClickMergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Merge request submitted successfully.").TapOKButton();

            duplicateRecordRecordPage
                .WaitForDuplicateRecordRecordPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);


            //get the master record person fields to verify if they were updated
            var fields = dbHelper.person.GetPersonById(_personIDMaster, "adultsafeguardingflag", "age", "agegroupid", "allergiesnotrecorded", "allowemail",

                "allowphone", "allowsms", "retaininformationconcern", "deceased", "dateofbirth", "ethnicityid", "interpreterrequired", "isexternalperson",
                "islookedafterchild", "knownallergies", "nhsnumber", "nhsnumberstatusid", "noknownallergies", "pdsisdeferred", "pdsnhsnosuperseded", "recordedinerror", "relatedadultsafeguardingflag", "representalertorhazard", "childprotectionflag", "suppressstatementinvoices");

            Assert.AreEqual(true, fields["adultsafeguardingflag"]);
            Assert.AreEqual(33, fields["age"]);
            Assert.AreEqual(5, fields["agegroupid"]);
            Assert.AreEqual(false, fields["allergiesnotrecorded"]);
            Assert.AreEqual(true, fields["allowemail"]);

            Assert.AreEqual(false, fields["allowphone"]);
            Assert.AreEqual(false, fields["allowsms"]);
            Assert.AreEqual(false, fields["retaininformationconcern"]);
            Assert.AreEqual(false, fields["deceased"]);
            Assert.AreEqual(DateOfBirth, fields["dateofbirth"]);
            Assert.AreEqual(Ethnicity, fields["ethnicityid"]);
            Assert.AreEqual(false, fields["interpreterrequired"]);
            Assert.AreEqual(false, fields["isexternalperson"]);
            //Assert.AreEqual("987 654 3222", fields["nhsnumber"]); //change of behaviour on how the NHS Numbers is displayed
            Assert.AreEqual(2, fields["nhsnumberstatusid"]);
            Assert.AreEqual(false, fields["noknownallergies"]);
            Assert.AreEqual(false, fields["pdsisdeferred"]);
            Assert.AreEqual(false, fields["pdsnhsnosuperseded"]);
            Assert.AreEqual(false, fields["recordedinerror"]);
            Assert.AreEqual(false, fields["suppressstatementinvoices"]);

        }


        [Description("Testing Feature to pick subordinate fields to be taken to master record in merge - " +
            "Access the Duplicate Records window - Open a Duplicate Record with 2 duplicates (a master and a subordinate with different data) - Select the 2 duplicates records - Click on the Merge Duplicate button - " +
            "Wait for the Merge Record dialog to be displayed - " +
            "Select Allow Phone, Allow SMS, Concern with ability to retain information, Deceased, DOB, Ethnicity and Interpreter Required to be copied from the subordinate record - " +
            "Click on the merge button - Execute the Merge scheduled job - Validate that the select data was copied from the subordinate record to the master record")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24563")]
        public void DuplicateRecords_UITestMethod02()
        {
            var duplicateDetectionRule = new Guid("e31e376f-a7ab-ea11-a2cd-005056926fe4"); //First Name, Gender, DOB

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid Ethnicity_WhiteIrish = dbHelper.ethnicity.GetEthnicityIdByName("White Irish")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AddressTypeId_Home = 6;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Home, AccommodationStatusId, LivesAloneTypeId, GenderId);



            //get person Numbers
            var personNumberMaster = (int)dbHelper.person.GetPersonById(_personIDMaster, "personnumber")["personnumber"];
            var personNumberSubordinate = (int)dbHelper.person.GetPersonById(_personIDSubordinate, "personnumber")["personnumber"];

            //update the subordinate record
            dbHelper.person.UpdateAdultSafeguarding(_personIDSubordinate, true);
            dbHelper.person.UpdateAge(_personIDSubordinate, 33);
            dbHelper.person.UpdateAgeGroupId(_personIDSubordinate, 5); //25-35
            dbHelper.person.UpdateAllergiesNotRecorded(_personIDSubordinate, false);
            dbHelper.person.UpdateAllowEmail(_personIDSubordinate, true);
            dbHelper.person.UpdateAllowPhone(_personIDSubordinate, true);
            dbHelper.person.UpdateAllowSMS(_personIDSubordinate, true);
            dbHelper.person.UpdateRetainInformationConcern(_personIDSubordinate, true);
            dbHelper.person.UpdateDeceased(_personIDSubordinate, true);
            dbHelper.person.UpdateDateOfBirth(_personIDSubordinate, DateTime.Now.AddYears(-33).Date);
            dbHelper.person.UpdateEthnicityId(_personIDSubordinate, Ethnicity_WhiteIrish);
            dbHelper.person.UpdateInterpreterRequired(_personIDSubordinate, true);
            dbHelper.person.UpdateIsExternalPerson(_personIDSubordinate, true);
            dbHelper.person.UpdateIsLookedAfterChild(_personIDSubordinate, true);
            dbHelper.person.UpdateKnownAllergies(_personIDSubordinate, true);
            dbHelper.person.UpdateNHSNumber(_personIDSubordinate, "987 654 3210");
            dbHelper.person.UpdateNHSNumberStatusId(_personIDSubordinate, 1); //Number present and verified
            dbHelper.person.UpdateNoKnownAllergies(_personIDSubordinate, true);
            dbHelper.person.UpdatePDSIsDeferred(_personIDSubordinate, true);
            dbHelper.person.UpdatePDSNHSNoSuperseded(_personIDSubordinate, true);
            dbHelper.person.UpdateRecordedInError(_personIDSubordinate, true);
            dbHelper.person.UpdateRelatedAdultSafeguardingFlag(_personIDSubordinate, true);
            dbHelper.person.UpdateRepresentAlertOrHazard(_personIDSubordinate, true);
            dbHelper.person.UpdateChildProtectionFlag(_personIDSubordinate, true);
            dbHelper.person.UpdateSuppressStatementInvoices(_personIDSubordinate, true);

            //update the Master record
            dbHelper.person.UpdateAdultSafeguarding(_personIDMaster, false);
            dbHelper.person.UpdateAllergiesNotRecorded(_personIDMaster, true);
            dbHelper.person.UpdateAllowEmail(_personIDMaster, false);
            dbHelper.person.UpdateAllowPhone(_personIDMaster, false);
            dbHelper.person.UpdateAllowSMS(_personIDMaster, false);
            dbHelper.person.UpdateRetainInformationConcern(_personIDMaster, false);
            dbHelper.person.UpdateDeceased(_personIDMaster, false);
            dbHelper.person.UpdateInterpreterRequired(_personIDMaster, false);
            dbHelper.person.UpdateIsExternalPerson(_personIDMaster, false);
            dbHelper.person.UpdateIsLookedAfterChild(_personIDMaster, false);
            dbHelper.person.UpdateKnownAllergies(_personIDMaster, false);
            dbHelper.person.UpdateNHSNumber(_personIDMaster, "987 654 3222");
            dbHelper.person.UpdateNHSNumberStatusId(_personIDMaster, 2); //Number present but not traced
            dbHelper.person.UpdateNoKnownAllergies(_personIDMaster, false);
            dbHelper.person.UpdatePDSIsDeferred(_personIDMaster, false);
            dbHelper.person.UpdatePDSNHSNoSuperseded(_personIDMaster, false);
            dbHelper.person.UpdateRecordedInError(_personIDMaster, false);
            dbHelper.person.UpdateRelatedAdultSafeguardingFlag(_personIDMaster, false);
            dbHelper.person.UpdateRepresentAlertOrHazard(_personIDMaster, false);
            dbHelper.person.UpdateChildProtectionFlag(_personIDMaster, false);
            dbHelper.person.UpdateSuppressStatementInvoices(_personIDMaster, false);


            //create DuplicateRecord
            var duplicateRecordId = dbHelper.duplicateRecord.CreateDuplicateRecord(OwnerID, FirstName + " Master", duplicateDetectionRule, 2, _personIDMaster, "person", FirstName + " Master");

            //Create SubordinateDuplicate
            var subordinateDuplicateID1 = dbHelper.subordinateDuplicate.CreateProfessionalSubordinateDuplicate(OwnerID, FirstName + " Master", duplicateRecordId, _personIDMaster, "person", FirstName + " Master", personNumberMaster.ToString(), false);
            var subordinateDuplicateID2 = dbHelper.subordinateDuplicate.CreateProfessionalSubordinateDuplicate(OwnerID, FirstName + " Subordinate", duplicateRecordId, _personIDSubordinate, "person", FirstName + " Subordinate", personNumberSubordinate.ToString(), false);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickDuplicateRecordsButton();

            duplicateRecordsPage
                .WaitForDuplicateRecordsPageToLoad()
                .InsertSearchQuery(FirstName)
                .ClickSearchButton()
                .CliickOnRecord(duplicateRecordId.ToString());

            duplicateRecordRecordPage
                .WaitForDuplicateRecordRecordPageToLoad()

                .WaitForSubordinateDuplicatesAreaToLoad()
                .ClickSubordinateDuplicates_RecordCheckBox(subordinateDuplicateID1.ToString())
                .ClickSubordinateDuplicates_RecordCheckBox(subordinateDuplicateID2.ToString())
                .ClickSubordinateDuplicates_MergeDuplicateButton();

            duplicateRecordMergeDialog
                .WaitForDuplicateRecordMergeDialogToLoad()
                .SelectMainRecord(subordinateDuplicateID1.ToString())

                .Clickallowphone_Checkbox(subordinateDuplicateID2.ToString())
                .Clickallowsms_Checkbox(subordinateDuplicateID2.ToString())
                .Clickretaininformationconcern_Checkbox(subordinateDuplicateID2.ToString())
                .Clickdeceased_Checkbox(subordinateDuplicateID2.ToString())
                .Clickdateofbirth_Checkbox(subordinateDuplicateID2.ToString())
                .Clickethnicity_Checkbox(subordinateDuplicateID2.ToString())
                .Clickinterpreterrequired_Checkbox(subordinateDuplicateID2.ToString())

                .ClickMergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Merge request submitted successfully.").TapOKButton();

            duplicateRecordRecordPage
                .WaitForDuplicateRecordRecordPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);


            //get the master record person fields to verify if they were updated
            var fields = dbHelper.person.GetPersonById(_personIDMaster, "adultsafeguardingflag", "age", "agegroupid", "allergiesnotrecorded", "allowemail",

                "allowphone", "allowsms", "retaininformationconcern", "deceased", "dateofbirth", "ethnicityid", "interpreterrequired", "isexternalperson",
                "islookedafterchild", "knownallergies", "nhsnumber", "nhsnumberstatusid", "noknownallergies", "pdsisdeferred", "pdsnhsnosuperseded", "recordedinerror", "relatedadultsafeguardingflag", "representalertorhazard", "childprotectionflag", "suppressstatementinvoices");

            Assert.AreEqual(false, fields["allowemail"]);

            Assert.AreEqual(true, fields["allowphone"]);
            Assert.AreEqual(true, fields["allowsms"]);
            Assert.AreEqual(true, fields["retaininformationconcern"]);
            Assert.AreEqual(true, fields["deceased"]);
            Assert.AreEqual(DateTime.Now.AddYears(-33).Date, fields["dateofbirth"]);
            Assert.AreEqual(Ethnicity_WhiteIrish, fields["ethnicityid"]);
            Assert.AreEqual(true, fields["interpreterrequired"]);

            Assert.AreEqual(false, fields["isexternalperson"]);
            //Assert.AreEqual("987 654 3222", fields["nhsnumber"]); //change of behaviour on how the NHS Numbers is displayed
            Assert.AreEqual(2, fields["nhsnumberstatusid"]);
            Assert.AreEqual(false, fields["noknownallergies"]);
            Assert.AreEqual(false, fields["pdsisdeferred"]);
            Assert.AreEqual(false, fields["pdsnhsnosuperseded"]);
            Assert.AreEqual(false, fields["recordedinerror"]);
            Assert.AreEqual(false, fields["suppressstatementinvoices"]);

        }

        [Description("Testing Feature to pick subordinate fields to be taken to master record in merge - " +
            "Access the Duplicate Records window - Open a Duplicate Record with 2 duplicates (a master and a subordinate with different data) - Select the 2 duplicates records - Click on the Merge Duplicate button - " +
            "Wait for the Merge Record dialog to be displayed - Select the Master record as the main record - " +
            "do not select any field from the subordinate record - " +
            "Click on the merge button - Execute the Merge scheduled job - Validate that the master record maintains all data after the merge")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24564")]
        public void DuplicateRecords_UITestMethod003()
        {
            var duplicateDetectionRule = new Guid("e31e376f-a7ab-ea11-a2cd-005056926fe4"); //First Name, Gender, DOB

            string FirstName = "Datam" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DateTime DateOfBirth = new DateTime(1999, 1, 1);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid Ethnicity_WhiteIrish = dbHelper.ethnicity.GetEthnicityIdByName("White Irish")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AddressTypeId_Home = 6;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            //Create Master person record
            Guid _personIDMaster = dbHelper.person.CreatePersonRecord("", FirstName, "", "Master", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Create Subordinate person record
            Guid _personIDSubordinate = dbHelper.person.CreatePersonRecord("", FirstName, "", "Subordinate", "", DateOfBirth, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Home, AccommodationStatusId, LivesAloneTypeId, GenderId);



            //get person Numbers
            var personNumberMaster = (int)dbHelper.person.GetPersonById(_personIDMaster, "personnumber")["personnumber"];
            var personNumberSubordinate = (int)dbHelper.person.GetPersonById(_personIDSubordinate, "personnumber")["personnumber"];

            //update the subordinate record
            dbHelper.person.UpdateAdultSafeguarding(_personIDSubordinate, true);
            dbHelper.person.UpdateAge(_personIDSubordinate, 33);
            dbHelper.person.UpdateAgeGroupId(_personIDSubordinate, 5); //25-35
            dbHelper.person.UpdateAllergiesNotRecorded(_personIDSubordinate, false);
            dbHelper.person.UpdateAllowEmail(_personIDSubordinate, true);
            dbHelper.person.UpdateAllowPhone(_personIDSubordinate, true);
            dbHelper.person.UpdateAllowSMS(_personIDSubordinate, true);
            dbHelper.person.UpdateRetainInformationConcern(_personIDSubordinate, true);
            dbHelper.person.UpdateDeceased(_personIDSubordinate, true);
            dbHelper.person.UpdateDateOfBirth(_personIDSubordinate, DateTime.Now.AddYears(-33).Date);
            dbHelper.person.UpdateEthnicityId(_personIDSubordinate, Ethnicity_WhiteIrish);
            dbHelper.person.UpdateInterpreterRequired(_personIDSubordinate, true);
            dbHelper.person.UpdateIsExternalPerson(_personIDSubordinate, true);
            dbHelper.person.UpdateIsLookedAfterChild(_personIDSubordinate, true);
            dbHelper.person.UpdateKnownAllergies(_personIDSubordinate, true);
            dbHelper.person.UpdateNHSNumber(_personIDSubordinate, "987 654 3210");
            dbHelper.person.UpdateNHSNumberStatusId(_personIDSubordinate, 1); //Number present and verified
            dbHelper.person.UpdateNoKnownAllergies(_personIDSubordinate, true);
            dbHelper.person.UpdatePDSIsDeferred(_personIDSubordinate, true);
            dbHelper.person.UpdatePDSNHSNoSuperseded(_personIDSubordinate, true);
            dbHelper.person.UpdateRecordedInError(_personIDSubordinate, true);
            dbHelper.person.UpdateRelatedAdultSafeguardingFlag(_personIDSubordinate, true);
            dbHelper.person.UpdateRepresentAlertOrHazard(_personIDSubordinate, true);
            dbHelper.person.UpdateChildProtectionFlag(_personIDSubordinate, true);
            dbHelper.person.UpdateSuppressStatementInvoices(_personIDSubordinate, true);

            //update the Master record
            dbHelper.person.UpdateAdultSafeguarding(_personIDMaster, false);
            dbHelper.person.UpdateAllergiesNotRecorded(_personIDMaster, true);
            dbHelper.person.UpdateAllowEmail(_personIDMaster, false);
            dbHelper.person.UpdateAllowPhone(_personIDMaster, false);
            dbHelper.person.UpdateAllowSMS(_personIDMaster, false);
            dbHelper.person.UpdateRetainInformationConcern(_personIDMaster, false);
            dbHelper.person.UpdateDeceased(_personIDMaster, false);
            dbHelper.person.UpdateInterpreterRequired(_personIDMaster, false);
            dbHelper.person.UpdateIsExternalPerson(_personIDMaster, false);
            dbHelper.person.UpdateIsLookedAfterChild(_personIDMaster, false);
            dbHelper.person.UpdateKnownAllergies(_personIDMaster, false);
            dbHelper.person.UpdateNHSNumber(_personIDMaster, "987 654 3222");
            dbHelper.person.UpdateNHSNumberStatusId(_personIDMaster, 2); //Number present but not traced
            dbHelper.person.UpdateNoKnownAllergies(_personIDMaster, false);
            dbHelper.person.UpdatePDSIsDeferred(_personIDMaster, false);
            dbHelper.person.UpdatePDSNHSNoSuperseded(_personIDMaster, false);
            dbHelper.person.UpdateRecordedInError(_personIDMaster, false);
            dbHelper.person.UpdateRelatedAdultSafeguardingFlag(_personIDMaster, false);
            dbHelper.person.UpdateRepresentAlertOrHazard(_personIDMaster, false);
            dbHelper.person.UpdateChildProtectionFlag(_personIDMaster, false);
            dbHelper.person.UpdateSuppressStatementInvoices(_personIDMaster, false);


            //create DuplicateRecord
            var duplicateRecordId = dbHelper.duplicateRecord.CreateDuplicateRecord(OwnerID, FirstName + " Master", duplicateDetectionRule, 2, _personIDMaster, "person", FirstName + " Master");

            //Create SubordinateDuplicate
            var subordinateDuplicateID1 = dbHelper.subordinateDuplicate.CreateProfessionalSubordinateDuplicate(OwnerID, FirstName + " Master", duplicateRecordId, _personIDMaster, "person", FirstName + " Master", personNumberMaster.ToString(), false);
            var subordinateDuplicateID2 = dbHelper.subordinateDuplicate.CreateProfessionalSubordinateDuplicate(OwnerID, FirstName + " Subordinate", duplicateRecordId, _personIDSubordinate, "person", FirstName + " Subordinate", personNumberSubordinate.ToString(), false);


            loginPage
                .GoToLoginPage()
                .Login("SecurityTestUserAdmin", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToDataManagementSection();

            dataManagementPage
                .WaitForDataManagementPageToLoad()
                .ClickDuplicateDetectionAreaButton()
                .ClickDuplicateRecordsButton();

            duplicateRecordsPage
                .WaitForDuplicateRecordsPageToLoad()
                .InsertSearchQuery(FirstName)
                .ClickSearchButton()
                .CliickOnRecord(duplicateRecordId.ToString());

            duplicateRecordRecordPage
                .WaitForDuplicateRecordRecordPageToLoad()

                .WaitForSubordinateDuplicatesAreaToLoad()
                .ClickSubordinateDuplicates_RecordCheckBox(subordinateDuplicateID1.ToString())
                .ClickSubordinateDuplicates_RecordCheckBox(subordinateDuplicateID2.ToString())
                .ClickSubordinateDuplicates_MergeDuplicateButton();

            duplicateRecordMergeDialog
                .WaitForDuplicateRecordMergeDialogToLoad()
                .SelectMainRecord(subordinateDuplicateID1.ToString())
                .ClickMergeButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Merge request submitted successfully.").TapOKButton();

            duplicateRecordRecordPage
                .WaitForDuplicateRecordRecordPageToLoad();


            //authenticate against the v6 Web API
            this.WebAPIHelper.Security.Authenticate();

            //execute the "Merge Records" schedule job and wait for the Idle status
            this.WebAPIHelper.ScheduleJob.Execute(MergeRecords_ScheduleJobID.ToString(), this.WebAPIHelper.Security.AuthenticationCookie);
            dbHelper = new DBHelper.DatabaseHelper();
            this.dbHelper.scheduledJob.WaitForScheduledJobIdleState(MergeRecords_ScheduleJobID);


            //get the master record person fields to verify if they were updated
            var fields = dbHelper.person.GetPersonById(_personIDMaster, "adultsafeguardingflag", "age", "agegroupid", "allergiesnotrecorded", "allowemail",

                "allowphone", "allowsms", "retaininformationconcern", "deceased", "dateofbirth", "ethnicityid", "interpreterrequired", "isexternalperson",
                "islookedafterchild", "knownallergies", "nhsnumber", "nhsnumberstatusid", "noknownallergies", "pdsisdeferred", "pdsnhsnosuperseded", "recordedinerror", "relatedadultsafeguardingflag", "representalertorhazard", "childprotectionflag", "suppressstatementinvoices");

            Assert.AreEqual(true, fields["adultsafeguardingflag"]);
            Assert.AreEqual(false, fields["allergiesnotrecorded"]);
            Assert.AreEqual(false, fields["allowemail"]);
            Assert.AreEqual(false, fields["allowphone"]);
            Assert.AreEqual(false, fields["allowsms"]);
            Assert.AreEqual(false, fields["retaininformationconcern"]);
            Assert.AreEqual(false, fields["deceased"]);
            Assert.AreEqual(DateOfBirth, fields["dateofbirth"]);
            Assert.AreEqual(Ethnicity, fields["ethnicityid"]);
            Assert.AreEqual(false, fields["interpreterrequired"]);
            Assert.AreEqual(false, fields["isexternalperson"]);
            //Assert.AreEqual("987 654 3222", fields["nhsnumber"]); //Change of behaviour for the nhs number functionality
            Assert.AreEqual(2, fields["nhsnumberstatusid"]);
            Assert.AreEqual(false, fields["noknownallergies"]);
            Assert.AreEqual(false, fields["pdsisdeferred"]);
            Assert.AreEqual(false, fields["pdsnhsnosuperseded"]);
            Assert.AreEqual(false, fields["recordedinerror"]);
            Assert.AreEqual(false, fields["suppressstatementinvoices"]);

        }

        #endregion
    }


}
