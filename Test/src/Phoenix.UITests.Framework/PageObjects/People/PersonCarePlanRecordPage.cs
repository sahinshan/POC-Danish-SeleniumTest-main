using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCarePlanRecordPage : CommonMethods
    {
        public PersonCarePlanRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personcareplanIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplan&')]");
        readonly By copyCarePlanIFrame = By.Id("iframe_CopyCarePlan");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By NeedsTab = By.Id("CWNavGroup_Needs");


        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By Activities_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_Activities']/a");
        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By CaseNotesLink_LeftMenu = By.XPath("//*[@id='CWNavItem_CaseNotes']");
        readonly By PersonCarePlanForm_LeftMenu = By.XPath("//*[@id='CWNavItem_PersonCarePlanForm']");

        readonly By planAgreed_YesRadioButton = By.Id("CWField_planagreed_1");

        readonly By agreedDate_Field = By.Id("CWField_agreementdatetime");
        readonly By agreedTime_Field = By.Id("CWField_agreementdatetime_Time");
        readonly By carePlanAgreedById_LookupButton = By.Id("CWLookupBtn_careplanagreedbyid");
        readonly By carePlanFamilyInvolvedId = By.Id("CWField_careplanfamilyinvolvedid");
        readonly By familyNotInvolvedReasonId_LookupButton = By.Id("CWLookupBtn_familynotinvolvedreasonid");
        readonly By reason_TextBox = By.Id("CWField_reason");
        readonly By startDate_Button = By.Id("CWField_startdate_DatePicker");

        readonly By authorise_Button = By.Id("TI_AuthoriseButton");

        readonly By endDate_Field = By.XPath("//input[@name='CWField_enddate']");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By reviewDate_Field = By.Id("CWField_reviewdate");
        readonly By reviewFrequency_Field = By.Id("CWField_reviewfrequencyid");


        readonly By carePlanFamilyInvolvedId_Field = By.Id("CWField_careplanfamilyinvolvedid"); 
        readonly By familyInvolvedCarePlanId_Field = By.Id("CWFamilyInvolvedInCarePlanId");
        readonly By carePlanFamilyInvolvedErrorMessage = By.XPath("//span[text()='Please fill out this field.']");


        readonly By endReason_LookUP = By.XPath("//button[@id='CWLookupBtn_careplanendreasonid']");

        readonly By SaveAndClose_Button = By.Id("TI_SaveAndCloseButton");

        readonly By NotificationMessage = By.Id("CWNotificationMessage_DataForm");

        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By copyCarePlan_Button = By.Id("TI_CopyCarePlanButton");

        readonly By careCoordinatorId_LookupButton = By.Id("CWLookupBtn_CWCareCoordinatorId");
        readonly By careResponsibleTeamId_LookupButton = By.XPath("//div/button[@id='CWLookupBtn_CWResponsibleTeamId']");
        readonly By carePlanTypeId_LookupButton = By.Id("CWLookupBtn_careplantypeid"); 
        readonly By caseId_LookupButton = By.Id("CWLookupBtn_caseid");

        readonly By copy_Button = By.Id("CWCopy");
        By recordRow(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");

        #region Field Labels

        readonly By carePlanFieldLabel = By.XPath("//li[@id='CWLabelHolder_careplantypeid']/label");

        readonly By planAgreed_Label = By.XPath("//li[@id='CWLabelHolder_planagreed']/label");

        readonly By planAlsoAgreedBy_Label = By.XPath("//label[text()='Plan Also Agreed By']");

        readonly By familyInvolvedInCarePlan_Label = By.Id("CWFamilyInvolvedInCarePlanLabel");

        readonly By agreeWithPerson_Label = By.XPath("//label[text()='Agreed with Person or Legal Representative']");


        #endregion

        #region Fields

        readonly By carePlanField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_careplantypeid_Link']");
        readonly By carePlanLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_careplantypeid']");

        #endregion


        public PersonCarePlanRecordPage WaitForPersonCarePlanRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(pageHeader);

            WaitForElement(carePlanFieldLabel);

            WaitForElement(planAgreed_YesRadioButton);

            WaitForElement(planAgreed_Label);

            WaitForElement(planAlsoAgreedBy_Label);


            return this;
        }
        public PersonCarePlanRecordPage WaitForCopyCarePlanPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElement(copyCarePlanIFrame);
            SwitchToIframe(copyCarePlanIFrame);

            return this;
        }

        public PersonCarePlanRecordPage WaitForPersonCarePlanNeedPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            return this;
        }

        public PersonCarePlanRecordPage WaitForPersonCarePlanFormPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personcareplanIFrame);
            SwitchToIframe(personcareplanIFrame);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            return this;
        }


        public PersonCarePlanRecordPage ClickOnCarePlanNeedRecord(string RecordID)
        {
            Click(recordRow(RecordID));

            return this;
        }

        public PersonCarePlanRecordPage ClickOnCarePlanFormRecord(string RecordID)
        {
            Click(recordRow(RecordID));

            return this;
        }

        public PersonCarePlanRecordPage WaitForRecordToBeClosedAndSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            
            WaitForElement(backButton);

            return this;
        }

        public PersonCarePlanRecordPage TapBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public PersonCarePlanRecordPage TapNeedsSubLink()
        {
            WaitForElementToBeClickable(NeedsTab);
            Click(NeedsTab);

            return this;
        }

        public PersonCarePlanRecordPage TapSaveButton()
        {
            driver.FindElement(saveButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PersonCarePlanRecordPage TapSaveAndCloseButton()
        {
            driver.FindElement(saveAndCloseButton).Click();

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }
        public PersonCarePlanRecordPage SelectYesAgreedWithPersonRadioButton()
        {
            driver.FindElement(planAgreed_YesRadioButton).Click();
            System.Threading.Thread.Sleep(3000);
            return this;
        }
        public PersonCarePlanRecordPage SelectAuthoriseButton()
        {
            driver.FindElement(authorise_Button).Click();

            return this;
        }
        public PersonCarePlanRecordPage InsertEndDate(string EndDateToInsert)
        {
            WaitForElementToBeClickable(endDate_Field);
            SendKeys(endDate_Field, EndDateToInsert + Keys.Enter);
            
            return this;
        }
        public PersonCarePlanRecordPage InsertStartDate(string StartDateToInsert)
        {
            WaitForElementToBeClickable(startDate_Field);
            SendKeys(startDate_Field, StartDateToInsert);
            return this;
        }
        public PersonCarePlanRecordPage InsertReviewDate(string ReviewDateToInsert)
        {
            WaitForElementToBeClickable(reviewDate_Field);
            SendKeys(reviewDate_Field, ReviewDateToInsert);

            return this;
        }

        public PersonCarePlanRecordPage ClickEndReasonLookUp()
        {
            WaitForElement(endReason_LookUP);
            Click(endReason_LookUP);

            return this;
        }
        public PersonCarePlanRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(SaveAndClose_Button);
            Click(SaveAndClose_Button);

            return this;
        }

        public PersonCarePlanRecordPage ClickAdditionalItemsMenuButton()
        {
            this.Click(additionalItemsMenuButton);

            return this;
        }
        public PersonCarePlanRecordPage ClickCopyCarePlan()
        {
            this.Click(copyCarePlan_Button);

            return this;
        }
        public PersonCarePlanRecordPage ClickCarePlanTypeIdLookupButton()
        {
            this.Click(carePlanTypeId_LookupButton);

            return this;
        }
        public PersonCarePlanRecordPage ClickCareCoordinatorIdLookupButton()
        {
            this.Click(careCoordinatorId_LookupButton);

            return this;
        }
        public PersonCarePlanRecordPage ClickCareResponsibleTeamIdLookupButton()
        {
           WaitForElement(careResponsibleTeamId_LookupButton);
           Click(careResponsibleTeamId_LookupButton);

            return this;
        }
        public PersonCarePlanRecordPage ClickCareIdLookUpButton()
        {
            WaitForElementToBeClickable(caseId_LookupButton);
            MoveToElementInPage(caseId_LookupButton);
            Click(caseId_LookupButton);

            return this;
        }
        public PersonCarePlanRecordPage SelectFamilyInvolvedCarePlanId(string SelectByValue)
        {
            WaitForElement(familyInvolvedCarePlanId_Field);
            SelectPicklistElementByValue(familyInvolvedCarePlanId_Field, SelectByValue);

            return this;
        }
        public PersonCarePlanRecordPage TapCopyButton()
        {
            WaitForElement(copy_Button);
            Click(copy_Button);
            return this;
        }
        public PersonCarePlanRecordPage ValidateFamilyInvolvedInCarePlanLabel(string ExpectedText)
        {
            WaitForElement(familyInvolvedInCarePlan_Label);
            ValidateElementTextContainsText(familyInvolvedInCarePlan_Label, ExpectedText);

            return this;
        }
        public PersonCarePlanRecordPage ValidateAgreeWithPersonLabel(string ExpectedText)
        {
            WaitForElement(agreeWithPerson_Label);
            ValidateElementTextContainsText(agreeWithPerson_Label, ExpectedText);

            return this;
        }

        public PersonCarePlanRecordPage ValidateAgreedWithPersonLabel(string ExpectedText)
        {
            WaitForElement(planAgreed_Label);
            ValidateElementTextContainsText(planAgreed_Label, ExpectedText);

            return this;
        }
        public PersonCarePlanRecordPage ValidatePlanAlsoAgreedByLabel(string ExpectedText)
        {
            WaitForElement(planAgreed_Label);
            ValidateElementTextContainsText(planAlsoAgreedBy_Label, ExpectedText);

            return this;
        }
        public PersonCarePlanRecordPage ValidateAgreedDateNonEditable()
        {
            WaitForElement(agreedDate_Field);
            ValidateElementDisabled(agreedDate_Field);

            return this;
        }
        public PersonCarePlanRecordPage ValidateAgreedTimeNonEditable()
        {
            WaitForElement(agreedTime_Field);
            ValidateElementDisabled(agreedTime_Field);

            return this;
        }

        public PersonCarePlanRecordPage ValidateCarePlanAgreedByNonEditable()
        {
            WaitForElement(carePlanAgreedById_LookupButton);
            ValidateElementDisabled(carePlanAgreedById_LookupButton);

            return this;
        }

        public PersonCarePlanRecordPage ValidateCarePlanFamilyInvolvedNonEditable()
        {
            WaitForElement(carePlanFamilyInvolvedId);
            ValidateElementDisabled(carePlanFamilyInvolvedId);

            return this;
        }

        public PersonCarePlanRecordPage ValidateFamilyNotInvolvedReasonNonEditable()
        {
            WaitForElement(familyNotInvolvedReasonId_LookupButton);
            ValidateElementDisabled(familyNotInvolvedReasonId_LookupButton);

            return this;
        }

        public PersonCarePlanRecordPage ValidateReasonTextBoxNonEditable()
        {
            WaitForElement(reason_TextBox);
            ValidateElementDisabled(reason_TextBox);

            return this;
        }
        public PersonCarePlanRecordPage ValidateStartDateNonEditable()
        {
            WaitForElement(startDate_Button);
            ValidateElementDisabled(startDate_Button);

            return this;
        }
        public PersonCarePlanRecordPage ValidateAgreedDateEditable()
        {
            WaitForElement(agreedDate_Field);
            ValidateElementNotDisabled(agreedDate_Field);

            return this;
        }
        public PersonCarePlanRecordPage ValidateAgreedTimeEditable()
        {
            WaitForElement(agreedTime_Field);
            ValidateElementNotDisabled(agreedTime_Field);

            return this;
        }
     
        public PersonCarePlanRecordPage ValidateCarePlanAgreedByEditable()
        {
            WaitForElement(carePlanAgreedById_LookupButton);
            ValidateElementNotDisabled(carePlanAgreedById_LookupButton);

            return this;
        }

        public PersonCarePlanRecordPage ValidateCarePlanFamilyInvolvedEditable()
        {
            WaitForElement(carePlanFamilyInvolvedId);
            ValidateElementNotDisabled(carePlanFamilyInvolvedId);

            return this;
        }

        public PersonCarePlanRecordPage ValidateFamilyNotInvolvedReasonEditable()
        {
            WaitForElement(familyNotInvolvedReasonId_LookupButton);
            ValidateElementNotDisabled(familyNotInvolvedReasonId_LookupButton);

            return this;
        }

        public PersonCarePlanRecordPage ValidateReasonTextBoxEditable()
        {
            WaitForElement(reason_TextBox);
            ValidateElementNotDisabled(reason_TextBox);

            return this;
        }
        public PersonCarePlanRecordPage ValidateStartDateEditable()
        {
            WaitForElement(startDate_Button);
            ValidateElementNotDisabled(startDate_Button);

            return this;
        }

        public PersonCarePlanRecordPage ValidateEndDateField(String text)
        {
            WaitForElement(endDate_Field);
            ValidateElementText(endDate_Field, text);

            return this;
        }

        public PersonCarePlanRecordPage ValidateReviewDateField()
        {
            WaitForElement(reviewDate_Field);
           

            return this;
        }

        public PersonCarePlanRecordPage ValidateReviewFrequencyField(String text)
        {
            WaitForElement(reviewFrequency_Field);
            ValidatePicklistContainsElementByText(reviewFrequency_Field, text);

            return this;
        }
        public PersonCarePlanRecordPage SelectCWCarePlanFamilyInvolvedDropDown(string ElementTextToSelect)
        {
            WaitForElement(carePlanFamilyInvolvedId_Field);
            SelectPicklistElementByText(carePlanFamilyInvolvedId_Field, ElementTextToSelect);
            System.Threading.Thread.Sleep(3000);

            return this;
        }
        public PersonCarePlanRecordPage ValidateFamilyInvolvedInCarePlanErrormessage(string ElementTextToSelect)
        {
            WaitForElement(carePlanFamilyInvolvedErrorMessage);
            ValidateElementText(carePlanFamilyInvolvedErrorMessage, ElementTextToSelect);

            return this;
        }
        public PersonCarePlanRecordPage ValidateNodificationErrormessage(string ElementTextToSelect)
        {
            WaitForElement(NotificationMessage);
            ValidateElementText(NotificationMessage, ElementTextToSelect);

            return this;
        }
        public PersonCarePlanRecordPage ValidateShouldNotExistCarePlanType()
        {
            WaitForElement(carePlanTypeId_LookupButton);
            ValidateElementDoNotExist(carePlanTypeId_LookupButton);

            return this;
        }
        public PersonCarePlanRecordPage ValidateCarePlanTypeVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(carePlanTypeId_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(carePlanTypeId_LookupButton, 3);
            }

            return this;
        }

        public PersonCarePlanRecordPage NavigateToPersonCarePlanCaseNotesArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(Activities_LeftMenu);
            Click(Activities_LeftMenu);

            WaitForElementToBeClickable(CaseNotesLink_LeftMenu);
            Click(CaseNotesLink_LeftMenu);

            return this;
        }

        public PersonCarePlanRecordPage NavigateToPersonCarePlanFormsCarePlanArea()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(PersonCarePlanForm_LeftMenu);
            Click(PersonCarePlanForm_LeftMenu);

            return this;
        }
    }
}
