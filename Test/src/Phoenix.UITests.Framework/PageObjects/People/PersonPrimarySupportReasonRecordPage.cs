using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPrimarySupportReasonRecordPage : CommonMethods
    {

        public PersonPrimarySupportReasonRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");       
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personprimarysupportreason')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");       


        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By cloneButton = By.Id("TI_CloneButton");
        readonly By shareRecordButton = By.Id("TI_ShareRecordButton");
       


        #region Fields
        readonly By primarySupportReason_LookupButton = By.Id("CWField_primarysupportreasontypeid_cwname");              
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");
        readonly By primaryReason_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[1]");
        readonly By startDate_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[2]");
        readonly By endDate_FieldErrorArea = By.XPath("(//span[@title='Please fill out this field.'])[3]");
        readonly By primaryRelatedReason_LookupButton = By.Id("CWLookupBtn_primarysupportreasontypeid");
        readonly By ownerID_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By menuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");
        readonly By auditButton = By.Id("CWNavItem_AuditHistory");
        readonly By personid_Field = By.Id("CWLookupBtn_personid");
        

        #endregion

        public PersonPrimarySupportReasonRecordPage WaitForPersonPrimarySupportReasonRecordPageToLoad(string ExpectedTitle = "New")
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);
            ValidateElementTextContainsText(pageHeader, "Person Primary Support Reason:\r\n" + ExpectedTitle);

            this.WaitForElement(saveButton);            
            this.WaitForElement(saveAndCloseButton);

            return this;
        }

       

        public PersonPrimarySupportReasonRecordPage WaitForPersonPrimarySupportReasonRecordPageToLoadFromAdvanceSearch()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);            
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);



            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);
            ValidateElementText(pageHeader, "Person Primary Support Reason:\r\nNew");


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }


        public PersonPrimarySupportReasonRecordPage WaitForPersonPrimarySupportReasonRecordModificationPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);
           // ValidateElementText(pageHeader, "Person Primary Support Reason: New");


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }

        public PersonPrimarySupportReasonRecordPage ClickPrimarySupportReasonLookupButton()
        {
            Click(primaryRelatedReason_LookupButton);       


            return this;
        }

        public PersonPrimarySupportReasonRecordPage ClickOwnerIDLookupButton()
        {
            Click(ownerID_LookupButton);


            return this;
        }

        public PersonPrimarySupportReasonRecordPage ClickPersonID_LookupButton()
        {
            Click(personid_Field);


            return this;
        }
        public PersonPrimarySupportReasonRecordPage ClickMenuButton()
        {
            Click(menuButton);

            return this;

        }
        public PersonPrimarySupportReasonRecordPage ClickAuditButton()
        {
            Click(auditButton);

            return this;

        }
        public PersonPrimarySupportReasonRecordPage ClickShareRecordButton()
        {
            Click(shareRecordButton);

            return this;

        }


        public PersonPrimarySupportReasonRecordPage InsertStartDate(string StartDateToInsert)
        {
            SendKeys(startDate_Field, StartDateToInsert);        

            return this;
        }
        public PersonPrimarySupportReasonRecordPage InsertEndDate(string EndDateToInsert)
        {
           
            SendKeys(endDate_Field, EndDateToInsert);

            return this;
        }

        public PersonPrimarySupportReasonRecordPage ValidateNotificationMessage(string ExpectedMessage)
        {
            WaitForElementVisible(notificationMessageArea);

            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }


        public PersonPrimarySupportReasonRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public PersonPrimarySupportReasonRecordPage ValidatePrimarySupportFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(primaryReason_FieldErrorArea, ExpectedMessage);

            return this;
        }
        public PersonPrimarySupportReasonRecordPage ValidatestartDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(startDate_FieldErrorArea, ExpectedMessage);

            return this;
        }
        public PersonPrimarySupportReasonRecordPage ValidateEndDateFieldErrorMessage(string ExpectedMessage)
        {
            ValidateElementText(endDate_FieldErrorArea, ExpectedMessage);

            return this;
        }


        public PersonPrimarySupportReasonRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

       
        public PersonPrimarySupportReasonRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }
        

    }
}
