using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace Phoenix.UITests.Settings.Configuration
{
    [TestClass]
    public class RTTTreatmentFunctions_UITestCases : FunctionalTest
    {
        #region properties

        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _systemUserName;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #region RTT Treatment Function Code

        private Guid _rttTreatmentFunctionCode_100_Id;
        private Guid _rttTreatmentFunctionCode_101_Id;
        private Guid _rttTreatmentFunctionCode_110_Id;
        private Guid _rttTreatmentFunctionCode_120_Id;
        private Guid _rttTreatmentFunctionCode_130_Id;
        private Guid _rttTreatmentFunctionCode_140_Id;
        private Guid _rttTreatmentFunctionCode_150_Id;
        private Guid _rttTreatmentFunctionCode_160_Id;
        private Guid _rttTreatmentFunctionCode_170_Id;

        private Guid _rttTreatmentFunctionCode_300_Id;
        private Guid _rttTreatmentFunctionCode_301_Id;
        private Guid _rttTreatmentFunctionCode_320_Id;
        private Guid _rttTreatmentFunctionCode_330_Id;
        private Guid _rttTreatmentFunctionCode_340_Id;

        private Guid _rttTreatmentFunctionCode_400_Id;
        private Guid _rttTreatmentFunctionCode_410_Id;
        private Guid _rttTreatmentFunctionCode_430_Id;

        private Guid _rttTreatmentFunctionCode_502_Id;
        private Guid _rttTreatmentFunctionCode_656_Id;

        private Guid _rttTreatmentFunctionCode_700_Id;
        private Guid _rttTreatmentFunctionCode_710_Id;
        private Guid _rttTreatmentFunctionCode_711_Id;
        private Guid _rttTreatmentFunctionCode_712_Id;
        private Guid _rttTreatmentFunctionCode_713_Id;
        private Guid _rttTreatmentFunctionCode_715_Id;
        private Guid _rttTreatmentFunctionCode_720_Id;
        private Guid _rttTreatmentFunctionCode_721_Id;
        private Guid _rttTreatmentFunctionCode_722_Id;
        private Guid _rttTreatmentFunctionCode_723_Id;
        private Guid _rttTreatmentFunctionCode_724_Id;
        private Guid _rttTreatmentFunctionCode_725_Id;
        private Guid _rttTreatmentFunctionCode_726_Id;
        private Guid _rttTreatmentFunctionCode_727_Id;
        private Guid _rttTreatmentFunctionCode_730_Id;

        private Guid _rttTreatmentFunctionCode_X02_Id;
        private Guid _rttTreatmentFunctionCode_X03_Id;
        private Guid _rttTreatmentFunctionCode_X04_Id;
        private Guid _rttTreatmentFunctionCode_X05_Id;
        private Guid _rttTreatmentFunctionCode_X06_Id;

        #endregion

        #endregion

        [TestInitialize()]
        public void TestMethod_Setup()
        {
            #region Internal

            _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

            #endregion

            #region Default User

            string username = ConfigurationManager.AppSettings["Username"];
            string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

            commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

            #endregion

            #region Language

            _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

            #endregion Language

            #region Business Unit

            _businessUnitId = commonMethodsDB.CreateBusinessUnit("RTT Treatment Function BU");

            #endregion

            #region Team

            _teamId = commonMethodsDB.CreateTeam("RTT Treatment Function T1", null, _businessUnitId, "907678", "RTTTreatmentFunctionT1@careworkstempmail.com", "RTT Treatment Function", "020 123456");

            #endregion

            #region System User

            _systemUserName = "RTTTreatmentFunctionsUser1";
            commonMethodsDB.CreateSystemUserRecord(_systemUserName, "RTTTreatmentFunctions", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion
        }

        public void GetRTTTreatmentFunctionCode()
        {
            _rttTreatmentFunctionCode_100_Id = dbHelper.rttTreatmentFunctionCode.GetByName("General Surgery Service")[0];
            _rttTreatmentFunctionCode_101_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Urology Service")[0];
            _rttTreatmentFunctionCode_110_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Trauma & Orthopaedics Service")[0];
            _rttTreatmentFunctionCode_120_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Ear, Nose & Throat Service")[0];
            _rttTreatmentFunctionCode_130_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Ophthalmology Service")[0];
            _rttTreatmentFunctionCode_140_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Oral Surgery Service")[0];
            _rttTreatmentFunctionCode_150_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Neurosurgery Service")[0];
            _rttTreatmentFunctionCode_160_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Plastic Surgery Service")[0];
            _rttTreatmentFunctionCode_170_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Cardiothoracic Surgery Service")[0];

            _rttTreatmentFunctionCode_300_Id = dbHelper.rttTreatmentFunctionCode.GetByName("General Internal Medicine Service")[0];
            _rttTreatmentFunctionCode_301_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Gastroenterology Service")[0];
            _rttTreatmentFunctionCode_320_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Cardiology Service")[0];
            _rttTreatmentFunctionCode_330_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Dermatology Service")[0];
            _rttTreatmentFunctionCode_340_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Respiratory Medicine Service")[0];

            _rttTreatmentFunctionCode_400_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Neurology Service")[0];
            _rttTreatmentFunctionCode_410_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Rheumatology Service")[0];
            _rttTreatmentFunctionCode_430_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Elderly Medicine Service")[0];

            _rttTreatmentFunctionCode_502_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Gynaecology Service")[0];

            _rttTreatmentFunctionCode_656_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Clinical Psychology Service")[0];

            _rttTreatmentFunctionCode_700_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Learning Disability Service")[0];
            _rttTreatmentFunctionCode_710_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Adult Mental Health Service")[0];
            _rttTreatmentFunctionCode_711_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Child and Adolescent Psychiatry Service")[0];
            _rttTreatmentFunctionCode_712_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Forensic Psychiatry Service")[0];
            _rttTreatmentFunctionCode_713_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Medical Psychotherapy Service")[0];
            _rttTreatmentFunctionCode_715_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Old Age Psychiatry Service")[0];
            _rttTreatmentFunctionCode_720_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Eating Disorders Service")[0];
            _rttTreatmentFunctionCode_721_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Addiction Service")[0];
            _rttTreatmentFunctionCode_722_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Liaison Psychiatry Service")[0];
            _rttTreatmentFunctionCode_723_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Psychiatric Intensive Care Service")[0];
            _rttTreatmentFunctionCode_724_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Perinatal Mental Health Service")[0];
            _rttTreatmentFunctionCode_725_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Mental Health Recovery and Rehabilitation Service")[0];
            _rttTreatmentFunctionCode_726_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Mental Health Dual Diagnosis Service")[0];
            _rttTreatmentFunctionCode_727_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Dementia Assessment Service")[0];
            _rttTreatmentFunctionCode_730_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Neuropsychiatry Service")[0];

            _rttTreatmentFunctionCode_X02_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Other - Medical Services (All other TREATMENT FUNCTIONS in the Medical Services group not reported individually)")[0];
            _rttTreatmentFunctionCode_X03_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Other - Mental Health Services (All other TREATMENT FUNCTIONS in the Mental Health group not reported individually) This included the following functions")[0];
            _rttTreatmentFunctionCode_X04_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Other - Paediatric Services (All other TREATMENT FUNCTIONS in the Paediatric group not reported individually)")[0];
            _rttTreatmentFunctionCode_X05_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Other - Surgical Services (All other TREATMENT FUNCTIONS in the Surgical group not reported individually)")[0];
            _rttTreatmentFunctionCode_X06_Id = dbHelper.rttTreatmentFunctionCode.GetByName("Other - Other Services (All other TREATMENT FUNCTIONS in the Other group not reported individually) For more information on Treatment Function Code definitions")[0];

        }

        #region https://advancedcsg.atlassian.net/browse/ACC-2551

        [TestProperty("JiraIssueID", "ACC-2590")]
        [Description("Step(s) 1 to 6 & 8 to 13 from the original test method CDV6-21717")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void RTTTreatmentFunctions_UITestMethod001()
        {
            #region Step 1 & 2

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("RTT Treatment Functions")
                .ClickReferenceDataMainHeader("Referral to Treatment (RTT)")
                .ClickReferenceDataElement("RTT Treatment Functions");

            rttTreatmentFunctionsPage
                .WaitForRTTTreatmentFunctionsPageToLoad()
                .ClickNewRecordButton();

            rttTreatmentFunctionRecordPage
                .WaitForRTTTreatmentFunctionRecordPageToLoad()
                .ValidateAllFieldsVisible()

                .ValidateNameMandatoryFieldVisibility(true)
                .ValidateNameFieldMaximumLimitText("450")

                .ValidateTreatmentFunctionCodeMandatoryFieldVisibility(true)
                .ValidateRollupTreatmentFunctionCodeMandatoryFieldVisibility(false)
                .ValidateCodeFieldIsNumeric()
                .ValidateCodeMandatoryFieldVisibility(false)

                .ValidateStartDateMandatoryFieldVisibility(true)
                .ValidateStartDateDatePickerIsVisibile()

                .ValidateEndDateMandatoryFieldVisibility(false)
                .ValidateEndDateDatePickerIsVisibile();

            #endregion

            #region Step 3

            rttTreatmentFunctionRecordPage
                .ClickSaveButton()
                .ValidateNotificationAreaText("Some data is not correct. Please review the data in the Form.")
                .ValidateNameFieldErrorLabelText("Please fill out this field.")
                .ValidateTreatmentFunctionCodeFieldErrorLabelText("Please fill out this field.");

            #endregion

            #region Step 4

            var _rttTreatmentFunctionName = "RTT_Function_" + _currentDateString;

            rttTreatmentFunctionRecordPage
                .InsertName(_rttTreatmentFunctionName)
                .InsertTreatmentFunctionCode("2")
                .InsertCode("Test")
                .ValidateCodeFieldErrorLabelText("Please enter a value between -2147483648 and 2147483647.");

            #endregion

            #region Step 5

            rttTreatmentFunctionRecordPage
                .InsertCode("2")
                .InsertStartDate(DateTime.Now.Date.AddDays(-2).ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(DateTime.Now.Date.AddDays(-4).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("End Date can not be prior to start date")
                .TapOKButton();

            #endregion

            #region Step 6

            rttTreatmentFunctionRecordPage
                .InsertEndDate("")
                .ClickSaveButton()
                .WaitForRecordToBeSaved()
                .ValidateRTTTreatmentFunctionRecordTitle(_rttTreatmentFunctionName);

            #endregion

            #region Step 8

            rttTreatmentFunctionRecordPage
                .ClickBackButton();

            System.Threading.Thread.Sleep(1000);

            rttTreatmentFunctionsPage
                .WaitForRTTTreatmentFunctionsPageToLoad()
                .ValidateHeaderCellText(2, "Name")
                .ValidateHeaderCellText(3, "Code")
                .ValidateHeaderCellText(4, "Treatment Function Code")
                .ValidateHeaderCellText(5, "Rollup Treatment Function Code")
                .ValidateHeaderCellText(6, "Start Date")
                .ValidateHeaderCellText(7, "End Date")
                .ValidateHeaderCellText(8, "Valid For Export");

            #endregion

            #region Step 9 to 13

            rttTreatmentFunctionsPage
                .InsertSearchQuery(_rttTreatmentFunctionName)
                .TapSearchButton();

            Guid _rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.GetByName(_rttTreatmentFunctionName)[0];
            var _rttTreatmentFunctionCodeActiveRecordId = commonMethodsDB.CreateRTTTreatmentFunctionCode(_teamId, "RTT_Function_Active_Record", new DateTime(2020, 1, 1));

            rttTreatmentFunctionsPage
                .ValidateRecordCellContent(_rttTreatmentFunctionCodeId.ToString(), 2, _rttTreatmentFunctionName)
                .OpenRecord(_rttTreatmentFunctionCodeId.ToString());

            rttTreatmentFunctionRecordPage
                .WaitForRTTTreatmentFunctionRecordPageToLoad()
                .InsertEndDate(DateTime.Now.Date.AddDays(-1).ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            rttTreatmentFunctionsPage
                .WaitForRTTTreatmentFunctionsPageToLoad()
                .SelectSystemView("Inactive Records")
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCodeId.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCodeActiveRecordId.ToString(), false)

                .SelectSystemView("Active Records")
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCodeId.ToString(), false)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCodeActiveRecordId.ToString(), true);

            dbHelper.rttTreatmentFunctionCode.DeleteRTTTreatmentFunctionCodeRecord(_rttTreatmentFunctionCodeId);

            #endregion

        }

        [TestProperty("JiraIssueID", "ACC-2591")]
        [Description("Step(s) 7 & 14 from the original test method CDV6-21717")]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        [TestMethod]
        public void RTTTreatmentFunctions_UITestMethod002()
        {
            GetRTTTreatmentFunctionCode();

            #region Step 7

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("RTT Treatment Functions")
                .ClickReferenceDataMainHeader("Referral to Treatment (RTT)")
                .ClickReferenceDataElement("RTT Treatment Functions");

            rttTreatmentFunctionsPage
                .WaitForRTTTreatmentFunctionsPageToLoad()
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_100_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_101_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_110_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_120_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_130_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_140_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_150_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_160_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_170_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_300_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_301_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_320_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_330_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_340_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_400_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_410_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_430_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_502_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_656_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_700_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_710_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_711_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_712_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_713_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_715_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_720_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_721_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_722_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_723_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_724_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_725_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_726_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_727_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_730_Id.ToString(), true)

                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_X02_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_X03_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_X04_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_X05_Id.ToString(), true)
                .ValidateRecordPresentOrNot(_rttTreatmentFunctionCode_X06_Id.ToString(), true);

            #endregion

            #region Step 14

            DateTime startDate = new DateTime(2020, 1, 1);
            var _rttFunctionName = "RTT_Function_" + _currentDateString;
            var _rttTreatmentFunctionCodeId = dbHelper.rttTreatmentFunctionCode.CreateRTTTreatmentFunctionCode(_teamId, _rttFunctionName, startDate, 100, "100Code");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("RTT Treatment Functions")
                .SelectFilter("1", "Treatment Function Code")
                .SelectOperator("1", "Equals")
                .InsertRuleValueText("1", "100")

                .ClickAddRuleButton(1)
                .SelectFilter("2", "Rollup Treatment Function Code")
                .SelectOperator("2", "Contains Data")

                .ClickAddRuleButton(1)
                .SelectFilter("3", "Start Date")
                .SelectOperator("3", "Equals")
                .InsertRuleValueText("3", startDate.ToString("dd'/'MM'/'yyyy"))

                .ClickAddRuleButton(1)
                .SelectFilter("4", "Inactive")
                .SelectOperator("4", "Equals")
                .SelectPicklistRuleValue("4", "No")

                .ClickSearchButton()
                .WaitForResultsPageToLoad()
                .ValidateSearchResultRecordPresent(_rttTreatmentFunctionCodeId.ToString());

            dbHelper.rttTreatmentFunctionCode.DeleteRTTTreatmentFunctionCodeRecord(_rttTreatmentFunctionCodeId);

            #endregion

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}
