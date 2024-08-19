using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class AllegationInvestigatorRecordPage : CommonMethods
    {
        public AllegationInvestigatorRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personAllegationInvestigatorIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=allegationinvestigator&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span");
        readonly By allegation_FieldTitle = By.XPath("//*[@id='CWLabelHolder_allegationid']/label[text()='Allegation']");
        readonly By allegation_Field = By.Id("CWField_allegationid");
        readonly By investigator_FieldTitle = By.XPath("//*[@id='CWLabelHolder_investigatorid']/label");
        readonly By investigator_Field = By.Id("CWField_investigatorid");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By responsibleTeam_Field = By.Id("CWField_ownerid");
        readonly By responsibleUser_FieldTitle = By.XPath("//*[@id='CWLabelHolder_responsibleuserid']/label");
        readonly By responsibleUser_Field = By.Id("CWField_responsibleuserid");
        readonly By dateStarted_FieldTitle = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By dateStarted_Field = By.Id("CWField_startdate");
        readonly By dateEnded_FieldTitle = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By dateEnded_Field = By.Id("CWField_enddate");


        public AllegationInvestigatorRecordPage WaitForAllegationInvestigatorRecordPageToLoad(string TaskTitle)
        {
            driver.SwitchTo().DefaultContent();


            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personAllegationInvestigatorIFrame);
            SwitchToIframe(personAllegationInvestigatorIFrame);

            WaitForElement(pageHeader);

            WaitForElement(general_SectionTitle);
            WaitForElement(allegation_FieldTitle);
            WaitForElement(responsibleTeam_FieldTitle);
            WaitForElement(investigator_FieldTitle);
            WaitForElement(responsibleUser_FieldTitle);
            WaitForElement(dateStarted_FieldTitle);
            WaitForElement(dateEnded_FieldTitle);
            
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Allegation Investigator:\r\n" + TaskTitle);

            return this;
        }
       
        public AllegationInvestigatorRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }
       
        public AllegationInvestigatorRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public AllegationInvestigatorRecordPage InsertDateStarted(string DateToInsert)
        {
            WaitForElement(dateStarted_Field);
            SendKeys(dateStarted_Field, DateToInsert);


            return this;
        }

        public AllegationInvestigatorRecordPage InsertDateEnded(string DateToInsert)
        {
            WaitForElement(dateEnded_Field);
            SendKeys(dateEnded_Field, DateToInsert);


            return this;
        }

        public AllegationInvestigatorRecordPage SelectInvestigator(String OptionToSelect)
        {
            WaitForElementVisible(investigator_Field);
            SelectPicklistElementByValue(investigator_Field, OptionToSelect);

            return this;
        }

    }
}
