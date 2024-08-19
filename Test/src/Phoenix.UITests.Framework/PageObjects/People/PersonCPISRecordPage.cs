using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonCPISRecordPage : CommonMethods
    {

        public PersonCPISRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpis&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By assignButton = By.Id("TI_AssignRecordButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");



        #region Labels

        readonly By batchID_Label = By.XPath("//*[@id='CWLabelHolder_batchid']/label");

        readonly By recordType_Label = By.XPath("//*[@id='CWLabelHolder_recordtypeid']/label");
        readonly By person_Label = By.XPath("//*[@id='CWLabelHolder_personid']/label");
        readonly By responsibleTeam_Label = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By status_Label = By.XPath("//*[@id='CWLabelHolder_statusid']/label");

        readonly By startdate_Label = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By deleteDate_Label = By.XPath("//*[@id='CWLabelHolder_deletedate']/label");
        readonly By endDate_Label = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By motherOfUnbornChild_Label = By.XPath("//*[@id='CWLabelHolder_motherid']/label");


        readonly By message_Label = By.XPath("//*[@id='CWLabelHolder_message']/label");

        #endregion

        #region fields

        readonly By batchID_Field = By.XPath("//*[@id='CWField_batchid']");

        readonly By recordType_Field = By.XPath("//*[@id='CWField_recordtypeid']");
        readonly By person_Field = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By responsibleTeam_Field = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By status_Field = By.XPath("//*[@id='CWField_statusid']");

        readonly By startdate_Field = By.XPath("//*[@id='CWField_startdate']");
        readonly By deleteDate_Field = By.XPath("//*[@id='CWField_deletedate']");
        readonly By endDate_Field = By.XPath("//*[@id='CWField_enddate']");
        readonly By motherOfUnbornChild_Field = By.XPath("//*[@id='CWField_motherid_Link']");


        readonly By message_Field = By.XPath("//*[@id='CWField_message']");

        #endregion



        public PersonCPISRecordPage WaitForPersonCPISRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);
            this.WaitForElement(assignButton);
            this.WaitForElement(deleteButton);

            this.WaitForElement(batchID_Label);

            this.WaitForElement(recordType_Label);
            this.WaitForElement(person_Label);
            this.WaitForElement(responsibleTeam_Label);
            this.WaitForElement(status_Label);

            this.WaitForElement(startdate_Label);
            this.WaitForElement(deleteDate_Label);
            this.WaitForElement(endDate_Label);
            this.WaitForElement(motherOfUnbornChild_Label);

            this.WaitForElement(message_Label);

            return this;
        }



        public PersonCPISRecordPage ValidateBatchIDFieldText(string Expectedtext)
        {
            ValidateElementText(batchID_Field, Expectedtext);

            return this;
        }


        public PersonCPISRecordPage ValidateRecordTypeFieldText(string Expectedtext)
        {
            ValidatePicklistSelectedText(recordType_Field, Expectedtext);

            return this;
        }

        public PersonCPISRecordPage ValidatePersonFieldText(string Expectedtext)
        {
            ValidateElementText(person_Field, Expectedtext);

            return this;
        }

        public PersonCPISRecordPage ValidateResponsibleTeamFieldText(string Expectedtext)
        {
            ValidateElementText(responsibleTeam_Field, Expectedtext);

            return this;
        }

        public PersonCPISRecordPage ValidateStatusFieldText(string Expectedtext)
        {
            ValidatePicklistSelectedText(status_Field, Expectedtext);

            return this;
        }


        public PersonCPISRecordPage ValidateStarDateFieldText(string Expectedtext)
        {
            ValidateElementValue(startdate_Field, Expectedtext);

            return this;
        }

        public PersonCPISRecordPage ValidateDeleteDateFieldText(string Expectedtext)
        {
            ValidateElementValue(deleteDate_Field, Expectedtext);

            return this;
        }

        public PersonCPISRecordPage ValidateEndDateFieldText(string Expectedtext)
        {
            ValidateElementValue(endDate_Field, Expectedtext);

            return this;
        }

        public PersonCPISRecordPage ValidateMotherOfUnbornChildFieldText(string Expectedtext)
        {
            if (string.IsNullOrEmpty(Expectedtext))
            {
                ValidateElementText(motherOfUnbornChild_Field, Expectedtext);
            }
            else
            {
                ValidateElementText(motherOfUnbornChild_Field, Expectedtext);
            }

            return this;
        }


        public PersonCPISRecordPage ValidateMessageFieldText(string Expectedtext)
        {
            ValidateElementText(message_Field, Expectedtext);

            return this;
        }



        public PersonCPISRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PersonCPISRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            return this;
        }

        public PersonCPISRecordPage ClickSaveAndCloseButtonAndWaitForNoRefreshPannel()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }




    }
}
