using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class ChargeApportionmentRecordPage : CommonMethods
    {
        public ChargeApportionmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderchargeapportionment')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By BackButton = By.Id("BackButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By ValidateChargeApportionmentButton = By.Id("TI_ValidateChargeApportionment");
        readonly By ToolbarMenuButton = By.Id("CWToolbarMenu");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditSubMenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By DetailsTab = By.XPath("//*[@id='CWNavGroup_EditForm']/a");
        readonly By ApportionmentDetailsTab = By.XPath("//*[@id='CWNavGroup_ApportionmentDetails']/a");

        readonly By NotificationArea = By.XPath("//*[@id='CWNotificationHolder_DataForm']");

        #region Genereal

        readonly By GeneralSectionTitle = By.XPath("//*[@id='CWSection_General']//span[text()='General']");

        readonly By Person_LabelField = By.XPath("//*[@id='CWLabelHolder_personid']/label[text()='Person']");
        readonly By Person_MandatoryField = By.XPath("//*[@id='CWLabelHolder_personid']/label/span[@class='mandatory']");
        readonly By Person_LinkText = By.Id("CWField_personid_Link");
        readonly By Person_LookupButton = By.Id("CWLookupBtn_personid");

        readonly By ServiceType_LabelField = By.XPath("//*[@id='CWLabelHolder_servicetypeid']/label[text()='Service Type']");
        readonly By ServiceType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_servicetypeid']/label/span[@class='mandatory']");
        readonly By ServiceType_PickList = By.Id("CWField_servicetypeid");

        readonly By StartDate_LabelField = By.XPath("//*[@id='CWLabelHolder_startdate']/label[text()='Start Date']");
        readonly By StartDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class='mandatory']");
        readonly By StartDate_Field = By.Id("CWField_startdate");
        readonly By StartDate_DatePickerIcon = By.Id("CWField_startdate_DatePicker");

        readonly By ApportionmentType_LabelField = By.XPath("//*[@id='CWLabelHolder_aportionmenttypeid']/label[text()='Apportionment Type']");
        readonly By ApportionmentType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_aportionmenttypeid']/label/span[@class='mandatory']");
        readonly By ApportionmentType_PickList = By.Id("CWField_aportionmenttypeid");

        readonly By ResponsibleTeam_LabelField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By ResponsibleTeam_MandatoryField = By.XPath("//*[@id='CWLabelHolder_ownerid']/label/span[@class='mandatory']");
        readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        
        readonly By PersonContract_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractid']/label[text()='Person Contract']");
        readonly By PersonContract_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractid']/label/span[@class='mandatory']");
        readonly By PersonContract_LinkText = By.Id("CWField_careproviderpersoncontractid_Link");
        readonly By PersonContract_LookupButton = By.Id("CWLookupBtn_careproviderpersoncontractid");
        readonly By PersonContract_RemoveButton = By.Id("CWClearLookup_careproviderpersoncontractid");

        readonly By PersonContractService_LabelField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractserviceid']/label[text()='Person Contract Service']");
        readonly By PersonContractService_MandatoryField = By.XPath("//*[@id='CWLabelHolder_careproviderpersoncontractserviceid']/label/span[@class='mandatory']");
        readonly By PersonContractService_LinkText = By.Id("CWField_careproviderpersoncontractserviceid_Link");
        readonly By PersonContractService_LookupButton = By.Id("CWLookupBtn_careproviderpersoncontractserviceid");
        readonly By PersonContractService_RemoveButton = By.Id("CWClearLookup_careproviderpersoncontractserviceid");

        readonly By EndDate_LabelField = By.XPath("//*[@id='CWLabelHolder_enddate']/label[text()='End Date']");
        readonly By EndDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_enddate']/label/span[@class='mandatory']");
        readonly By EndDate_Field = By.Id("CWField_enddate");
        readonly By EndDate_DatePickerIcon = By.Id("CWField_enddate_DatePicker");

        readonly By Validated_LabelField = By.XPath("//*[@id='CWLabelHolder_validated']/label[text()='Validated?']");
        readonly By Validated_MandatoryField = By.XPath("//*[@id='CWLabelHolder_validated']/label/span[@class='mandatory']");
        readonly By Validated_YesOption = By.Id("CWField_validated_1");
        readonly By Validated_NoOption = By.Id("CWField_validated_0");

        #endregion


        public ChargeApportionmentRecordPage WaitForChargeApportionmentRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);
            WaitForElementVisible(SaveButton);

            return this;
        }

        public ChargeApportionmentRecordPage WaitForChargeApportionmentRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(pageHeader);
            WaitForElementVisible(BackButton);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateAllFieldsOfGeneralSection()
        {
            WaitForElementVisible(GeneralSectionTitle);
            ScrollToElement(GeneralSectionTitle);

            WaitForElementVisible(Person_LabelField);
            WaitForElementVisible(ServiceType_LabelField);
            WaitForElementVisible(ServiceType_PickList);

            WaitForElementVisible(StartDate_LabelField);
            WaitForElementVisible(StartDate_Field);

            WaitForElementVisible(ApportionmentType_LabelField);
            WaitForElementVisible(ApportionmentType_PickList);

            WaitForElementVisible(ResponsibleTeam_LabelField);
            WaitForElementVisible(ResponsibleTeam_LinkText);
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            WaitForElementVisible(EndDate_LabelField);
            WaitForElementVisible(EndDate_Field);

            WaitForElementVisible(Validated_LabelField);
            WaitForElementVisible(Validated_YesOption);
            WaitForElementVisible(Validated_NoOption);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ValidateElementText(pageHeader, "Charge Apportionment:\r\n" + ExpectedText);

            return this;
        }

        public ChargeApportionmentRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(auditSubMenuLink);
            Click(auditSubMenuLink);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateApportionmentDetailsTabIsVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ApportionmentDetailsTab);
            else
                WaitForElementNotVisible(ApportionmentDetailsTab, 5);

            return this;
        }

        public ChargeApportionmentRecordPage NavigateToApportionmentDetailsTab()
        {
            WaitForElementToBeClickable(ApportionmentDetailsTab);
            Click(ApportionmentDetailsTab);

            return this;
        }

        public ChargeApportionmentRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public ChargeApportionmentRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickValidateChargeApportionmentButton()
        {
            WaitForElementToBeClickable(ValidateChargeApportionmentButton);
            Click(ValidateChargeApportionmentButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            ScrollToElement(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(DeleteRecordButton);
            ScrollToElement(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickPersonContractLookupButton()
        {
            WaitForElementToBeClickable(PersonContract_LookupButton);
            ScrollToElement(PersonContract_LookupButton);
            Click(PersonContract_LookupButton);

            return this;
        }

        public ChargeApportionmentRecordPage ClickPersonContractServiceLookupButton()
        {
            WaitForElementToBeClickable(PersonContractService_LookupButton);
            ScrollToElement(PersonContractService_LookupButton);
            Click(PersonContractService_LookupButton);

            return this;
        }

        public ChargeApportionmentRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(SaveButton);
            WaitForElementVisible(SaveAndCloseButton);
            WaitForElementVisible(AssignRecordButton);

            return this;
        }

        public ChargeApportionmentRecordPage InsertStartDate(string StartDate)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, StartDate + Keys.Tab);

            return this;
        }

        public ChargeApportionmentRecordPage InsertEndDate(string EndDate)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, EndDate + Keys.Tab);

            return this;
        }

        public ChargeApportionmentRecordPage SelectServiceType(string TextToSelect)
        {
            WaitForElementToBeClickable(ServiceType_PickList);
            ScrollToElement(ServiceType_PickList);
            SelectPicklistElementByText(ServiceType_PickList, TextToSelect);

            return this;
        }

        public ChargeApportionmentRecordPage SelectApportionmentType(string TextToSelect)
        {
            WaitForElementToBeClickable(ApportionmentType_PickList);
            ScrollToElement(ApportionmentType_PickList);
            SelectPicklistElementByText(ApportionmentType_PickList, TextToSelect);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Person_LabelField);
            ScrollToElement(Person_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Person_MandatoryField);
            else
                WaitForElementNotVisible(Person_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateServiceTypeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ServiceType_LabelField);
            ScrollToElement(ServiceType_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ServiceType_MandatoryField);
            else
                WaitForElementNotVisible(ServiceType_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateStartDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(StartDate_LabelField);
            ScrollToElement(StartDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(StartDate_MandatoryField);
            else
                WaitForElementNotVisible(StartDate_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateEndDateMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(EndDate_LabelField);
            ScrollToElement(EndDate_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(EndDate_MandatoryField);
            else
                WaitForElementNotVisible(EndDate_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateApportionmentTypeMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ApportionmentType_LabelField);
            ScrollToElement(ApportionmentType_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ApportionmentType_MandatoryField);
            else
                WaitForElementNotVisible(ApportionmentType_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateResponsibleTeamMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(ResponsibleTeam_LabelField);
            ScrollToElement(ResponsibleTeam_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_MandatoryField);
            else
                WaitForElementNotVisible(ResponsibleTeam_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(PersonContract_LabelField);
            ScrollToElement(PersonContract_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(PersonContract_MandatoryField);
            else
                WaitForElementNotVisible(PersonContract_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractServiceMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(PersonContractService_LabelField);
            ScrollToElement(PersonContractService_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(PersonContractService_MandatoryField);
            else
                WaitForElementNotVisible(PersonContractService_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateValidatedMandatoryFieldVisibility(bool ExpectVisible)
        {
            WaitForElementVisible(Validated_LabelField);
            ScrollToElement(Validated_LabelField);

            if (ExpectVisible)
                WaitForElementVisible(Validated_MandatoryField);
            else
                WaitForElementNotVisible(Validated_MandatoryField, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateSelectedServiceTypePickListValue(string expectedText)
        {
            WaitForElementVisible(ServiceType_PickList);
            ScrollToElement(ServiceType_PickList);
            ValidatePicklistSelectedText(ServiceType_PickList, expectedText);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateSelectedApportionmentTypePickListValue(string expectedText)
        {
            WaitForElementVisible(ApportionmentType_PickList);
            ScrollToElement(ApportionmentType_PickList);
            ValidatePicklistSelectedText(ApportionmentType_PickList, expectedText);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            WaitForElementToBeClickable(StartDate_Field);
            ScrollToElement(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedValue);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateEndDateFieldValue(string ExpectedValue)
        {
            WaitForElementToBeClickable(EndDate_Field);
            ScrollToElement(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedValue);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(Person_LinkText);
            ScrollToElement(Person_LinkText);
            ValidateElementText(Person_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkText);
            ScrollToElement(ResponsibleTeam_LinkText);
            ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonContract_LinkText);
            ScrollToElement(PersonContract_LinkText);
            ValidateElementText(PersonContract_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractServiceLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonContractService_LinkText);
            ScrollToElement(PersonContractService_LinkText);
            ValidateElementText(PersonContractService_LinkText, ExpectedText);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractFieldIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(PersonContract_LabelField);
                WaitForElementVisible(PersonContract_LinkText);
                WaitForElementVisible(PersonContract_LookupButton);
                WaitForElementVisible(PersonContract_RemoveButton);
            }
            else
            {
                WaitForElementNotVisible(PersonContract_LabelField, 3);
                WaitForElementNotVisible(PersonContract_LinkText, 3);
                WaitForElementNotVisible(PersonContract_LookupButton, 3);
                WaitForElementNotVisible(PersonContract_RemoveButton, 3);
            }

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractServiceFieldIsVisible(bool IsVisible)
        {
            if (IsVisible)
            {
                WaitForElementVisible(PersonContractService_LabelField);
                WaitForElementVisible(PersonContractService_LinkText);
                WaitForElementVisible(PersonContractService_LookupButton);
                WaitForElementVisible(PersonContractService_RemoveButton);
            }
            else
            {
                WaitForElementNotVisible(PersonContractService_LabelField, 3);
                WaitForElementNotVisible(PersonContractService_LinkText, 3);
                WaitForElementNotVisible(PersonContractService_LookupButton, 3);
                WaitForElementNotVisible(PersonContractService_RemoveButton, 3);
            }

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonLookupButtonIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(Person_LookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(Person_LookupButton);
            else
                ValidateElementNotDisabled(Person_LookupButton);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateServiceTypePickListIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(ServiceType_PickList);

            if (ExpectDisabled)
                ValidateElementDisabled(ServiceType_PickList);
            else
                ValidateElementNotDisabled(ServiceType_PickList);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateStartDateFieldIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(StartDate_Field);

            if (ExpectDisabled)
                ValidateElementDisabled(StartDate_Field);
            else
                ValidateElementNotDisabled(StartDate_Field);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateApportionmentTypePickListIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(ApportionmentType_PickList);

            if (ExpectDisabled)
                ValidateElementDisabled(ApportionmentType_PickList);
            else
                ValidateElementNotDisabled(ApportionmentType_PickList);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateResponsibleTeamLookupButtonIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(ResponsibleTeam_LookupButton);
            else
                ValidateElementNotDisabled(ResponsibleTeam_LookupButton);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractLookupButtonIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(PersonContract_LookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(PersonContract_LookupButton);
            else
                ValidateElementNotDisabled(PersonContract_LookupButton);

            return this;
        }

        public ChargeApportionmentRecordPage ValidatePersonContractServiceLookupButtonIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(PersonContractService_LookupButton);

            if (ExpectDisabled)
                ValidateElementDisabled(PersonContractService_LookupButton);
            else
                ValidateElementNotDisabled(PersonContractService_LookupButton);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateEndDateFieldIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(EndDate_Field);

            if (ExpectDisabled)
                ValidateElementDisabled(EndDate_Field);
            else
                ValidateElementNotDisabled(EndDate_Field);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateValidatedOptionsIsDisabled(bool ExpectDisabled)
        {
            WaitForElementVisible(Validated_YesOption);
            WaitForElementVisible(Validated_NoOption);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(Validated_YesOption);
                ValidateElementDisabled(Validated_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(Validated_YesOption);
                ValidateElementNotDisabled(Validated_NoOption);
            }

            return this;
        }

        public ChargeApportionmentRecordPage ValidateApportionmentTypePickListValues(string expected)
        {
            WaitForElementVisible(ApportionmentType_PickList);
            ValidatePicklistContainsElementByText(ApportionmentType_PickList, expected);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateValidateChargeApportionmentButtonIsVisible(bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ValidateChargeApportionmentButton);
            else
                WaitForElementNotVisible(ValidateChargeApportionmentButton, 3);

            return this;
        }

        public ChargeApportionmentRecordPage ValidateValidatedOptionsIsSelected(bool YesOption)
        {
            WaitForElementVisible(Validated_YesOption);
            WaitForElementVisible(Validated_NoOption);

            if (YesOption)
            {
                ValidateElementChecked(Validated_YesOption);
                ValidateElementNotChecked(Validated_NoOption);
            }
            else
            {
                ValidateElementChecked(Validated_NoOption);
                ValidateElementNotChecked(Validated_YesOption);
            }

            return this;
        }

    }
}
