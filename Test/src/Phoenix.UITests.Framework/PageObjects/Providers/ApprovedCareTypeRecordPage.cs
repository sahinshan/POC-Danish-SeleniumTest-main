using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Providers
{
    public class ApprovedCareTypeRecordPage : CommonMethods
    {

        public ApprovedCareTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=approvedcaretype&')]");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By activateButton = By.Id("TI_ActivateButton");
        
        readonly By toolbarMenuButton = By.Id("CWToolbarMenu");
        readonly By toolbarMenuDeleteButton = By.XPath("//a[@title = 'Delete']");

        readonly By approvedCareTypeRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[contains(@title, 'Approved Care Type: ')]");

        readonly By providerLookupButton = By.Id("CWLookupBtn_providerid");
        readonly By serviceElement1LookupButton = By.Id("CWLookupBtn_serviceelement1id");
        readonly By careTypeLookupButton = By.Id("CWLookupBtn_caretypeid");

        readonly By serviceElement1FieldLink = By.Id("CWField_serviceelement1id_Link");
        readonly By careTypeFieldLink = By.Id("CWField_caretypeid_Link");

        readonly By maleGenderAccommodated_YesOption = By.Id("CWField_malegenderaccommodated_1");
        readonly By maleGenderAccommodated_NoOption = By.Id("CWField_malegenderaccommodated_0");

        readonly By femaleGenderAccommodated_YesOption = By.Id("CWField_femalegenderaccommodated_1");
        readonly By femaleGenderAccommodated_NoOption = By.Id("CWField_femalegenderaccommodated_0");

        readonly By capacityField = By.Id("CWField_capacity");
        readonly By capacityIncludingSiblingBedsField = By.Id("CWField_capacityincludingsiblingbeds");

        readonly By startDateField = By.Id("CWField_startdate");
        readonly By endDateField = By.Id("CWField_enddate");

        readonly By maleAgeFromField = By.Id("CWField_maleagefrom");
        readonly By maleAgeToField = By.Id("CWField_maleageto");
        readonly By femaleAgeFromField = By.Id("CWField_femaleagefrom");
        readonly By femaleAgeToField = By.Id("CWField_femaleageto");

        readonly By approvalStatusPicklist = By.Id("CWField_approvalstatusid");

        readonly By serviceProvidedIdNumber = By.Id("CWControlHolder_serviceprovidednumber");

        readonly By inactive_YesOption = By.Id("CWField_inactive_1");
        readonly By inactive_NoOption = By.Id("CWField_inactive_0");

        public ApprovedCareTypeRecordPage WaitForApprovedCareTypeRecordPageToLoad(bool active)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            if (active)
            {
                WaitForElement(saveButton);
                WaitForElement(saveAndCloseButton);
            }
            WaitForElement(providerLookupButton);
            WaitForElement(serviceElement1LookupButton);
            WaitForElement(maleGenderAccommodated_YesOption);
            WaitForElement(maleGenderAccommodated_NoOption);
            WaitForElement(femaleGenderAccommodated_YesOption);
            WaitForElement(femaleGenderAccommodated_NoOption);
            WaitForElement(approvalStatusPicklist);           

            return this;
        }

        public ApprovedCareTypeRecordPage WaitForApprovedCareTypeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            
            WaitForElement(providerLookupButton);
            WaitForElement(serviceElement1LookupButton);
            WaitForElement(capacityField);
            WaitForElement(approvalStatusPicklist);

            return this;
        }

        public ApprovedCareTypeRecordPage SelectServiceElement1LookupButton()
        {
            MoveToElementInPage(serviceElement1LookupButton);
            WaitForElementToBeClickable(serviceElement1LookupButton);
            Click(serviceElement1LookupButton);
            return this;
        }

        public ApprovedCareTypeRecordPage SelectCareTypeLookupButton()
        {
            MoveToElementInPage(careTypeLookupButton);
            WaitForElementToBeClickable(careTypeLookupButton);
            Click(careTypeLookupButton);
            return this;
        }

        public ApprovedCareTypeRecordPage SelectApprovalStatus(string optionToSelect)
        {
            WaitForElement(approvalStatusPicklist);
            MoveToElementInPage(approvalStatusPicklist);
            SelectPicklistElementByText(approvalStatusPicklist, optionToSelect);

            return this;
        }

        public ApprovedCareTypeRecordPage InsertCapacity(string TextToEnter)
        {            
            MoveToElementInPage(capacityField);
            WaitForElementToBeClickable(capacityField);
            SendKeys(capacityField, TextToEnter);

            return this;
        }

        public ApprovedCareTypeRecordPage InsertCapacityIncludingSiblingBeds(string TextToEnter)
        {
            MoveToElementInPage(capacityIncludingSiblingBedsField);
            WaitForElementToBeClickable(capacityIncludingSiblingBedsField);
            SendKeys(capacityIncludingSiblingBedsField, TextToEnter);

            return this;
        }

        public ApprovedCareTypeRecordPage InsertStartDate(string TextToEnter)
        {
            MoveToElementInPage(startDateField);
            WaitForElementToBeClickable(startDateField);
            SendKeys(startDateField, TextToEnter);

            return this;
        }

        public ApprovedCareTypeRecordPage InsertEndDate(string TextToEnter)
        {
            MoveToElementInPage(endDateField);
            WaitForElementToBeClickable(endDateField);
            SendKeys(endDateField, TextToEnter);

            return this;
        }

        public ApprovedCareTypeRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ApprovedCareTypeRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            WaitForElementNotVisible(By.Id("CWRefreshPanel"), 5);

            return this;
        }

        public ApprovedCareTypeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            WaitForElementNotVisible(By.Id("CWRefreshPanel"), 5);

            return this;
        }

        public ApprovedCareTypeRecordPage ClickDeleteButton()
        {
            WaitForElement(toolbarMenuButton);
            Click(toolbarMenuButton);
            WaitForElement(toolbarMenuDeleteButton);
            Click(toolbarMenuDeleteButton);            
            return this;
        }

        public ApprovedCareTypeRecordPage ClickActivateButton()
        {
            WaitForElement(activateButton);
            MoveToElementInPage(activateButton);
            Click(activateButton);
            return this;
        }


        public ApprovedCareTypeRecordPage InactiveStatus(string optionToSelect)
        {
            if (optionToSelect.Equals("Yes"))
            {
                WaitForElement(inactive_YesOption);
                Click(inactive_YesOption);
            }
            else
            {
                WaitForElement(inactive_NoOption);
                Click(inactive_NoOption);
            }

            return this;
        }

        public ApprovedCareTypeRecordPage ValidateInactiveStatus(string optionToSelect)
        {
            WaitForElement(inactive_YesOption);
            WaitForElement(inactive_NoOption);
            
            if (optionToSelect.Equals("Yes"))
            {
                ValidateElementChecked(inactive_YesOption);
                ValidateElementNotChecked(inactive_NoOption);
                ValidateElementDisabled(inactive_YesOption);
                ValidateElementDisabled(inactive_NoOption);
            }
            else
            {
                ValidateElementChecked(inactive_NoOption);
                ValidateElementNotChecked(inactive_YesOption);
                ValidateElementNotDisabled(inactive_YesOption);
                ValidateElementNotDisabled(inactive_NoOption);
            }

            return this;
        }

        public ApprovedCareTypeRecordPage WaitForRecordPageTitleToLoad(string PageTitle)
        {
            WaitForApprovedCareTypeRecordPageToLoad();

            ValidateElementTextContainsText(approvedCareTypeRecordPageHeader, "Service Provided:\r\n" + PageTitle);

            return this;
        }

        public ApprovedCareTypeRecordPage ValidateRecordIsInactive()
        {
            WaitForElementVisible(activateButton);
            Assert.IsTrue(GetElementVisibility(activateButton));
            ValidateElementChecked(inactive_YesOption);
            ValidateElementNotChecked(inactive_NoOption);
            ValidateElementDisabled(inactive_YesOption);
            ValidateElementDisabled(inactive_NoOption);


            return this;
        }

    }
}
