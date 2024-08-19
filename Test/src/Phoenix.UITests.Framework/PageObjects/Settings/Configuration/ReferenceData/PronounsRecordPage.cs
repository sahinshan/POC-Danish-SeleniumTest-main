using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PronounsRecordPage : CommonMethods
    {
        public PronounsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        //readonly By iframe_pronouns = By.Id("iframe_pronouns");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=pronouns')]");
     
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");


        readonly By PronounsRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        #region General area

        readonly By Name_FieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");

        readonly By Code_FieldLabel = By.XPath("//*[@id='CWLabelHolder_code']/label");
        readonly By Code_Field = By.XPath("//*[@id='CWField_code']");

        readonly By GovCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_govcode']/label");
        readonly By GovCode_Field = By.XPath("//*[@id='CWField_govcode']");

        readonly By Inactive_FieldLabel = By.XPath("//*[@id='CWLabelHolder_inactive']/label");
        readonly By Inactive_YesRadioButton = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By Inactive_NoRadioButton = By.XPath("//*[@id='CWField_inactive_0']");


        readonly By StartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");

        readonly By EndDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");

        readonly By ValidForExport_FieldLabel = By.XPath("//*[@id='CWLabelHolder_validforexport']/label");
        readonly By ValidForExport_YesRadioButton = By.XPath("//*[@id='CWField_validforexport_1']");
        readonly By ValidForExport_NoRadioButton = By.XPath("//*[@id='CWField_validforexport_0']");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_FieldLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");

        #endregion



        public PronounsRecordPage WaitForPronounsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);


            //WaitForElement(iframe_pronouns);
            //SwitchToIframe(iframe_pronouns);


            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(PronounsRecordPageHeader);

            WaitForElementVisible(Name_FieldLabel);
            WaitForElementVisible(Name_Field);
            WaitForElementVisible(StartDate_FieldLabel);
            WaitForElementVisible(StartDate_Field);

            return this;
        }


        public PronounsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PronounsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }
        public PronounsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public PronounsRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            MoveToElementInPage(deleteButton);
            Click(deleteButton);

            return this;
        }


        public PronounsRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public PronounsRecordPage ValidateNewRecordFieldsVisible()
        {
            WaitForElementVisible(Name_FieldLabel);
            WaitForElementVisible(Name_Field);
            WaitForElementVisible(Code_FieldLabel);
            WaitForElementVisible(Code_Field);
            WaitForElementVisible(GovCode_FieldLabel);
            WaitForElementVisible(GovCode_Field);
            WaitForElementVisible(Inactive_FieldLabel);
            WaitForElementVisible(Inactive_YesRadioButton);
            WaitForElementVisible(Inactive_NoRadioButton);
           
            WaitForElementVisible(StartDate_FieldLabel);
            WaitForElementVisible(StartDate_Field);
            WaitForElementVisible(EndDate_FieldLabel);
            WaitForElementVisible(EndDate_Field);
            WaitForElementVisible(ValidForExport_FieldLabel);
            WaitForElementVisible(ValidForExport_YesRadioButton);
            WaitForElementVisible(ValidForExport_NoRadioButton);
            WaitForElementVisible(ResponsibleTeam_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            return this;
        }

        public PronounsRecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public PronounsRecordPage InsertCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Code_Field);
            SendKeys(Code_Field, TextToInsert);

            return this;
        }
        public PronounsRecordPage InsertGovCode(string TextToInsert)
        {
            WaitForElementToBeClickable(GovCode_Field);
            SendKeys(GovCode_Field, TextToInsert);

            return this;
        }
        public PronounsRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }
        public PronounsRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }

        public PronounsRecordPage ClickInactiveYesOption()
        {
            WaitForElementToBeClickable(Inactive_YesRadioButton);
            Click(Inactive_YesRadioButton);

            return this;
        }
        public PronounsRecordPage ClickInactiveNoOption()
        {
            WaitForElementToBeClickable(Inactive_NoRadioButton);
            Click(Inactive_NoRadioButton);

            return this;
        }

        public PronounsRecordPage ClickValidForExportYesOption()
        {
            WaitForElementToBeClickable(ValidForExport_YesRadioButton);
            Click(ValidForExport_YesRadioButton);

            return this;
        }
        public PronounsRecordPage ClickValidForExportNoOption()
        {
            WaitForElementToBeClickable(ValidForExport_NoRadioButton);
            Click(ValidForExport_NoRadioButton);

            return this;
        }

        public PronounsRecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Name_Field);
            WaitForElementToBeClickable(Name_Field);
            ValidateElementValue(Name_Field, ExpectedValue);

            return this;
        }

        public PronounsRecordPage ValidateCodeFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(Code_Field);
            WaitForElementToBeClickable(Code_Field);
            ValidateElementValue(Code_Field, ExpectedValue);

            return this;
        }

        public PronounsRecordPage ValidateGovCodeFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(GovCode_Field);
            WaitForElementToBeClickable(GovCode_Field);
            ValidateElementValue(GovCode_Field, ExpectedValue);

            return this;
        }

        public PronounsRecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(StartDate_Field);
            WaitForElementToBeClickable(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedValue);

            return this;
        }

        public PronounsRecordPage ValidateEndDateFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(EndDate_Field);
            WaitForElementToBeClickable(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedValue);

            return this;
        }

        public PronounsRecordPage ValidateResponsibleTeamFieldValue(string ExpectedValue)
        {
            MoveToElementInPage(ResponsibleTeam_FieldLink);
            WaitForElementToBeClickable(ResponsibleTeam_FieldLink);
            ValidateElementByTitle(ResponsibleTeam_FieldLink, ExpectedValue);

            return this;
        }

        public PronounsRecordPage ValidateInactiveYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(Inactive_YesRadioButton);
                ValidateElementNotChecked(Inactive_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Inactive_YesRadioButton);
                ValidateElementChecked(Inactive_NoRadioButton);
            }

            return this;
        }

        public PronounsRecordPage ValidateValidForExportYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ValidForExport_YesRadioButton);
                ValidateElementNotChecked(ValidForExport_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ValidForExport_YesRadioButton);
                ValidateElementChecked(ValidForExport_NoRadioButton);
            }

            return this;
        }       

        public PronounsRecordPage ValidateNameFieldIsDisabled()
        {
            WaitForElementVisible(Name_Field);
            MoveToElementInPage(Name_Field);
            ValidateElementDisabled(Name_Field);

            return this;
        }

        public PronounsRecordPage ValidateCodeFieldIsDisabled()
        {
            WaitForElementVisible(Code_Field);
            MoveToElementInPage(Code_Field);
            ValidateElementDisabled(Code_Field);

            return this;
        }

        public PronounsRecordPage ValidateGovCodeFieldIsDisabled()
        {
            WaitForElementVisible(GovCode_Field);
            MoveToElementInPage(GovCode_Field);
            ValidateElementDisabled(GovCode_Field);

            return this;
        }

        public PronounsRecordPage ValidateStartDateFieldIsDisabled()
        {
            WaitForElementVisible(StartDate_Field);
            MoveToElementInPage(StartDate_Field);
            ValidateElementDisabled(StartDate_Field);

            return this;
        }

        public PronounsRecordPage ValidateEndDateFieldIsDisabled()
        {
            WaitForElementVisible(EndDate_Field);
            MoveToElementInPage(EndDate_Field);
            ValidateElementDisabled(EndDate_Field);

            return this;
        }

        public PronounsRecordPage ValidateInactiveFieldOptionsDisabled()
        {
            WaitForElementVisible(Inactive_YesRadioButton);
            MoveToElementInPage(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_NoRadioButton);

            return this;
        }          
        
    }
}