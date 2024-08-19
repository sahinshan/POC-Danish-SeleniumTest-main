using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonHealthDetailRecordPage : CommonMethods
    {
        public PersonHealthDetailRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personhealthdetail')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");
        readonly By person_FieldTitle = By.XPath("//*[@id='CWLabelHolder_personid']/label");
        readonly By responsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By healthIssue_FieldTitle = By.XPath("//*[@id='CWLabelHolder_healthissuetypeid']/label");
        readonly By diagnosed_FieldTitle = By.XPath("//*[@id='CWLabelHolder_diagnosedid']/label");
        readonly By startDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By diagnosedDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_diagnoseddate']/label");
        readonly By endDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By notes_FieldTitle = By.XPath("//*[@id='CWLabelHolder_notes']/label");

        readonly By healthIssue_LookUpButton = By.Id("CWLookupBtn_healthissuetypeid");
        readonly By healthIssue_Field = By.Id("CWField_healthissuetypeid_cwname");
        readonly By diagnosed_Field = By.Id("CWField_diagnosedid");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By diagnosedDate_Field = By.Id("CWField_diagnoseddate");
        readonly By endDate_Field = By.Id("CWField_enddate");
        readonly By notes_Field = By.Id("CWField_notes");


        readonly By psychosisInformation_FieldTitle = By.XPath("//*[@id='CWSection_PsychosisInformation']/fieldset/div[1]/span");
        readonly By prodromePsychosisDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_prodromepsychosisdate']/label");
        readonly By emergentpsychosisDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_emergentpsychosisdate']/label");
        readonly By manifestpsychosisDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_manifestpsychosisdate']/label");
        readonly By firstPrescriptionDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_firstprescriptiondate']/label");
        readonly By psychosisFirstTreatmentStartDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_psychosisfirsttreatmentstartdate']/label");

        
        readonly By prodromePsychosisDate_Field = By.Id("CWLabelHolder_prodromepsychosisdate");
        readonly By emergentPsychosisDate_Field = By.Id("CWLabelHolder_emergentpsychosisdate");
        readonly By manifestPsychosisDate_Field = By.Id("CWLabelHolder_manifestpsychosisdate");
        readonly By firstPrescriptionDate_Field = By.Id("CWLabelHolder_firstprescriptiondate");
        readonly By psychosisFirstTreatmentStartDate_Field = By.Id("CWLabelHolder_psychosisfirsttreatmentstartdate");


        public PersonHealthDetailRecordPage WaitForPersonHealthDetailRecordPageToLoad(string TaskTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);
            this.WaitForElement(person_FieldTitle);
            this.WaitForElement(responsibleTeam_FieldTitle);
            this.WaitForElement(healthIssue_FieldTitle);
            this.WaitForElement(diagnosed_FieldTitle);
            this.WaitForElement(startDate_FieldTitle);
            this.WaitForElement(diagnosedDate_FieldTitle);
            this.WaitForElement(endDate_FieldTitle);
            this.WaitForElement(notes_FieldTitle);
          

            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            ValidateElementTextContainsText(pageHeader, "Person Health Detail:\r\n" + TaskTitle);

           

            return this;
        }

        public PersonHealthDetailRecordPage ClickHealthIssueLookupButton()
        {
            WaitForElementToBeClickable(healthIssue_LookUpButton);
            Click(healthIssue_LookUpButton);

            return this;
        }
        public PersonHealthDetailRecordPage SelectDiagnosed(String OptionToSelect)
        {
            WaitForElementVisible(diagnosed_Field);
            SelectPicklistElementByValue(diagnosed_Field, OptionToSelect);

            return this;
        }
        public PersonHealthDetailRecordPage InsertStartDate(string SearchQuery)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, SearchQuery);


            return this;
        }

        public PersonHealthDetailRecordPage InsertDiagnosedDate(string SearchQuery)
        {
            WaitForElement(diagnosedDate_Field);
            SendKeys(diagnosedDate_Field, SearchQuery);


            return this;
        }

        public PersonHealthDetailRecordPage InsertEndDate(string SearchQuery)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, SearchQuery);


            return this;
        }
        public PersonHealthDetailRecordPage InsertNotes(String TextToInsert)
        {
            WaitForElement(notes_Field);
            SendKeys(notes_Field, TextToInsert);
            return this;
        }

        public PersonHealthDetailRecordPage InsertProdromePsychosisDate(string SearchQuery)
        {
            WaitForElement(prodromePsychosisDate_Field);
            SendKeys(prodromePsychosisDate_Field, SearchQuery);


            return this;
        }
        public PersonHealthDetailRecordPage InsertEmergentPsychosisDate(string SearchQuery)
        {
            WaitForElement(emergentPsychosisDate_Field);
            SendKeys(emergentPsychosisDate_Field, SearchQuery);


            return this;
        }
        public PersonHealthDetailRecordPage InsertManifestPsychosisDate(string SearchQuery)
        {
            WaitForElement(manifestPsychosisDate_Field);
            SendKeys(manifestPsychosisDate_Field, SearchQuery);


            return this;
        }

        public PersonHealthDetailRecordPage InsertFirstPrescriptionDate(string SearchQuery)
        {
            WaitForElement(firstPrescriptionDate_Field);
            SendKeys(firstPrescriptionDate_Field, SearchQuery);


            return this;
        }

        public PersonHealthDetailRecordPage InsertPsychosisFirstTreatmentStartDate(string SearchQuery)
        {
            WaitForElement(psychosisFirstTreatmentStartDate_Field);
            SendKeys(psychosisFirstTreatmentStartDate_Field, SearchQuery);


            return this;
        }

        public PersonHealthDetailRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public PersonHealthDetailRecordPage ValidateProdromePsychosisDateFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(prodromePsychosisDate_Field);
            }
            else
            {
                WaitForElementNotVisible(prodromePsychosisDate_Field, 5);
            }
            return this;
        }
        public PersonHealthDetailRecordPage ValidateEmergentPsychosisDateFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(emergentPsychosisDate_Field);
            }
            else
            {
                WaitForElementNotVisible(emergentPsychosisDate_Field, 5);
            }
            return this;
        }
        public PersonHealthDetailRecordPage ValidateManifestPsychosisDateFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(manifestPsychosisDate_Field);
            }
            else
            {
                WaitForElementNotVisible(manifestPsychosisDate_Field, 5);
            }
            return this;
        }

        public PersonHealthDetailRecordPage ValidateFirstPrescriptionDateFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(firstPrescriptionDate_Field);
            }
            else
            {
                WaitForElementNotVisible(firstPrescriptionDate_Field, 5);
            }
            return this;
        }
        
            public PersonHealthDetailRecordPage ValidatePsychosisFirstTreatmentStartDateFieldVisible(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(psychosisFirstTreatmentStartDate_Field);
            }
            else
            {
                WaitForElementNotVisible(psychosisFirstTreatmentStartDate_Field, 5);
            }
            return this;
        }

        public PersonHealthDetailRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonHealthDetailRecordPage ValidateHealthIssueType(bool ExpectedText,String HealthIssueType)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(healthIssue_Field);
                ValidateElementText(healthIssue_Field, HealthIssueType);
            }
            else
            {
                WaitForElementNotVisible(healthIssue_Field, 5);
            }
            return this;
        }
        public PersonHealthDetailRecordPage ValidateStartDate(String ExpectedText)
        {
            WaitForElementVisible(startDate_Field);
            ValidateElementTextContainsText(startDate_Field, ExpectedText);
            return this;
        }

        public PersonHealthDetailRecordPage ValidateEndDate(String ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            ValidateElementTextContainsText(endDate_Field, ExpectedText);
            return this;
        }

        public PersonHealthDetailRecordPage ClickDeleteButton()
        {
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

    }
}
