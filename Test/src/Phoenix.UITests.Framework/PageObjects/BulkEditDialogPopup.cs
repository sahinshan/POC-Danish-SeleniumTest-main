using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class BulkEditDialogPopup : CommonMethods
    {
        public BulkEditDialogPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWBulkEditDialog = By.Id("iframe_CWBulkEditDialog");



        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/h1[text()='Update Multiple Records']");
        By popupSubHeader(string NumberOfRecordsSelected) => By.XPath("//*[@id='CWHeader']/small[text()='Number of selected records: " + NumberOfRecordsSelected + "']");


        By fieldCheckBox(string BusinessObjectFieldName) => By.Id("CWFormCheck_" + BusinessObjectFieldName);
        By fieldTitle(string BusinessObjectFieldName) => By.XPath("//*[@id='CWLabelHolder_" + BusinessObjectFieldName + "']/label");
        
        
        By yesRadioButtonField(string BusinessObjectFieldName) => By.Id("CWField_" + BusinessObjectFieldName + "_1");
        By noRadioButtonField(string BusinessObjectFieldName) => By.Id("CWField_" + BusinessObjectFieldName + "_0");
        By lookupFieldButton(string BusinessObjectFieldName) => By.Id("CWLookupBtn_" + BusinessObjectFieldName);
        By inputField(string BusinessObjectFieldName) => By.XPath("//input[@id='CWField_" + BusinessObjectFieldName + "']");
        By selectField(string BusinessObjectFieldName) => By.XPath("//select[@id='CWField_" + BusinessObjectFieldName + "']");

        readonly By TimePicker = By.XPath("//button[@id = 'CWField_runon_Time_TimePicker']");

        By errorMessage(string BusinessObjectFieldName) => By.XPath("//*[@id='CWControlHolder_" + BusinessObjectFieldName + "']/label/span");


        readonly By anyUpdateCheckBox = By.XPath("//*[contains(@id,'CWFormCheck_')]");


        //Audit Details Area
        readonly By auditReasonLookupButton = By.XPath("//*[@id='CWLookupBtn_CWAuditReasonId']");



        readonly By updateButton = By.Id("CWUpdateButton");
        readonly By closeButton = By.Id("CWCloseButton");




        public BulkEditDialogPopup WaitForBulkEditDialogPopupToLoad(string NumberOfRecordsSelected, bool IsCWContentIFrame = true)
        {
            if (IsCWContentIFrame)
            {
                SwitchToDefaultFrame();

                WaitForElement(CWContentIFrame);
                SwitchToIframe(CWContentIFrame);
            }

            WaitForElement(iframe_CWBulkEditDialog);
            SwitchToIframe(iframe_CWBulkEditDialog);

            WaitForElement(popupHeader);
            WaitForElement(popupSubHeader(NumberOfRecordsSelected));
            WaitForElement(closeButton);

            return this;
        }

        public BulkEditDialogPopup WaitForBulkEditDialogPopupToReload(string NumberOfRecordsSelected)
        {
            WaitForElement(popupHeader);
            WaitForElement(popupSubHeader(NumberOfRecordsSelected));
            WaitForElement(closeButton);

            return this;
        }

        public BulkEditDialogPopup ValidateFieldTitle(string BusinessObjectFieldName, string ExpectedName)
        {
            WaitForElement(fieldTitle(BusinessObjectFieldName));

            ValidateElementText(fieldTitle(BusinessObjectFieldName), ExpectedName);

            return this;
        }

        public BulkEditDialogPopup ValidateUpdateCheckBoxIsVisible(string BusinessObjectFieldName)
        {
            WaitForElementVisible(fieldCheckBox(BusinessObjectFieldName));
            WaitForElementToBeClickable(fieldCheckBox(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup ValidateInputFieldIsVisible(string BusinessObjectFieldName)
        {
            WaitForElementVisible(inputField(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup ValidateLookupButtonIsVisible(string BusinessObjectFieldName)
        {
            WaitForElementVisible(lookupFieldButton(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup ValidateRadioButtonOptionsIsVisible(string BusinessObjectFieldName)
        {
            WaitForElementVisible(yesRadioButtonField(BusinessObjectFieldName));
            WaitForElementVisible(noRadioButtonField(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup ValidateErrorMessage(string BusinessObjectFieldName, string ExpectedMessage)
        {
            WaitForElement(errorMessage(BusinessObjectFieldName));

            ValidateElementText(errorMessage(BusinessObjectFieldName), ExpectedMessage);

            return this;
        }

        public BulkEditDialogPopup ClickUpdateCheckBox(string BusinessObjectFieldName)
        {
            WaitForElement(fieldCheckBox(BusinessObjectFieldName));
            Click(fieldCheckBox(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup ClickYesRadioButtonField(string BusinessObjectFieldName)
        {
            WaitForElement(yesRadioButtonField(BusinessObjectFieldName));
            Click(yesRadioButtonField(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup ClickNoRadioButtonField(string BusinessObjectFieldName)
        {
            WaitForElement(noRadioButtonField(BusinessObjectFieldName));
            Click(noRadioButtonField(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup InsertValueInInputField(string BusinessObjectFieldName, string TextToInsert)
        {
            WaitForElement(inputField(BusinessObjectFieldName));
            SendKeys(inputField(BusinessObjectFieldName), TextToInsert);

            return this;
        }

        public BulkEditDialogPopup ClickOnFieldLookupButton(string BusinessObjectFieldName)
        {
            WaitForElement(lookupFieldButton(BusinessObjectFieldName));
            Click(lookupFieldButton(BusinessObjectFieldName));

            return this;
        }

        public BulkEditDialogPopup SelectValueInPicklistField(string BusinessObjectFieldName, string PicklistTextToSelect)
        {
            WaitForElement(selectField(BusinessObjectFieldName));
            SelectPicklistElementByText(selectField(BusinessObjectFieldName), PicklistTextToSelect);

            return this;
        }

        public BulkEditDialogPopup ValidateNoUpdateCheckboxVisible()
        {
            ValidateElementDoNotExist(anyUpdateCheckBox);

            return this;
        }

        public BulkEditDialogPopup ClickCloseButton()
        {
            Click(closeButton);

            return this;
        }

        public BulkEditDialogPopup ClickUpdateButton()
        {
            WaitForElement(updateButton);
            Click(updateButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public BulkEditDialogPopup ClickAuditReasonLookupButton()
        {
            WaitForElement(auditReasonLookupButton);
            ScrollToElement(auditReasonLookupButton);
            Click(auditReasonLookupButton);

            return this;
        }

        //click TimePicker button
        public BulkEditDialogPopup ClickTimePickerButton()
        {
            WaitForElementVisible(TimePicker);
            ScrollToElement(TimePicker);
            Click(TimePicker);

            return this;
        }

    }
}
