using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class TrainingCourseRecordPage : CommonMethods
    {

        public TrainingCourseRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContent_Iframe = By.Id("CWContentIFrame");
        readonly By trainingRequirement_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=trainingrequirement&')]");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']//*[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");

        readonly By courseTitle_Field = By.Id("CWField_title");

        readonly By trainingItem_Lookup = By.Id("CWLookupBtn_coursetitleid");
        readonly By trainingItem_DefaultFieldValue = By.Id("CWField_coursetitleid_cwname");              

        readonly By trainingItem_FieldLinkText = By.Id("CWField_coursetitleid_Link");

        readonly By validFromDate_Field = By.Id("CWField_validfromdate");
        readonly By validToDate_Field = By.Id("CWField_validtodate");
        readonly By durationInDays_Field = By.Id("CWField_trainindduration");
        readonly By courseCapacity_Field = By.Id("CWField_trainingcoursecapacity");

        readonly By Recurrence_Picklist = By.Id("CWField_trainingrecurrenceid");
        readonly By Category_Picklist = By.Id("CWField_categoryid");

        readonly By trainingProvider_Lookup = By.Id("CWLookupBtn_trainingproviderid");

        readonly By responsibleTeam_LookUP = By.Id("CWLookupBtn_ownerid");
        readonly By responsibleTeam_FieldLinkText = By.Id("CWField_ownerid_Link");

        readonly By provider_Field = By.Id("CWLabelHolder_trainingproviderid");
        readonly By provider_InputText = By.Id("CWField_trainingproviderid_cwname");
        readonly By provider_FieldLinkText = By.Id("CWField_trainingproviderid_Link");

        readonly By courseDescription_Picklist = By.Id("CWField_coursedescription");

        readonly By lengthOfCourseMinutes_Picklist = By.Id("CWField_lengthofcourse");


        public TrainingCourseRecordPage WaitForTrainingCourseRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElement(trainingRequirement_Iframe);
            SwitchToIframe(trainingRequirement_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 70);

            WaitForElementVisible(pageHeader);

            WaitForElementVisible(trainingItem_Lookup);

            WaitForElement(validToDate_Field);

            WaitForElement(validFromDate_Field);            

            return this;
        }

        public TrainingCourseRecordPage WaitForRecordToBeSaved()
        {            

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(additionalToolbarElementsButton);            

            return this;
        }

        public TrainingCourseRecordPage ClickTrainingItemLookup()
        {
            WaitForElementToBeClickable(trainingItem_Lookup);
            ScrollToElement(trainingItem_Lookup);
            Click(trainingItem_Lookup);

            return this;
        }

        public TrainingCourseRecordPage ClickResponsibleTeamFieldLookup()
        {
            WaitForElementToBeClickable(responsibleTeam_LookUP);
            ScrollToElement(responsibleTeam_LookUP);
            Click(responsibleTeam_LookUP);

            return this;
        }

        public TrainingCourseRecordPage ClickProviderLookup()
        {
            WaitForElementToBeClickable(trainingProvider_Lookup);
            ScrollToElement(trainingProvider_Lookup);
            Click(trainingProvider_Lookup);

            return this;
        }



        public TrainingCourseRecordPage InsertCourseTitle(string TextToInsert)
        {
            WaitForElement(courseTitle_Field);
            SendKeys(courseTitle_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TrainingCourseRecordPage InsertCourseDescription(string TextToInsert)
        {
            WaitForElement(courseDescription_Picklist);
            SendKeys(courseDescription_Picklist, TextToInsert + Keys.Tab);

            return this;
        }

        public TrainingCourseRecordPage InsertLengthOfCourseMinutes(string TextToInsert)
        {
            WaitForElement(lengthOfCourseMinutes_Picklist);
            SendKeys(lengthOfCourseMinutes_Picklist, TextToInsert + Keys.Tab);

            return this;
        }

        public TrainingCourseRecordPage InsertValidFromDate_Field(string TextToInsert)
        {
            WaitForElement(validFromDate_Field);
            SendKeys(validFromDate_Field, TextToInsert);
            SendKeysWithoutClearing(validFromDate_Field, Keys.Tab);

            return this;
        }

        public TrainingCourseRecordPage InsertValidToDate_Field(string TextToInsert)
        {
            WaitForElement(validToDate_Field);
            SendKeys(validToDate_Field, TextToInsert);
            SendKeysWithoutClearing(validToDate_Field, Keys.Tab);

            return this;
        }

        public TrainingCourseRecordPage InsertDurationInDays_Field(string TextToInsert)
        {
            WaitForElementToBeClickable(durationInDays_Field);
            ScrollToElement(durationInDays_Field);
            SendKeys(durationInDays_Field, TextToInsert);

            return this;
        }

        public TrainingCourseRecordPage InsertCourseCapacity_Field(string TextToInsert)
        {
            WaitForElementToBeClickable(courseCapacity_Field);
            ScrollToElement(courseCapacity_Field);
            SendKeys(courseCapacity_Field, TextToInsert);

            return this;
        }

        public TrainingCourseRecordPage SelectRecurrence(string TextToSelect)
        {
            WaitForElement(Recurrence_Picklist);
            ScrollToElement(Recurrence_Picklist);
            SelectPicklistElementByText(Recurrence_Picklist, TextToSelect);

            return this;
        }
        
        public TrainingCourseRecordPage SelectCategory(string TextToSelect)
        {
            WaitForElement(Category_Picklist);
            ScrollToElement(Category_Picklist);
            SelectPicklistElementByText(Category_Picklist, TextToSelect);

            return this;
        }

        public TrainingCourseRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(responsibleTeam_FieldLinkText);
            ValidateElementByTitle(responsibleTeam_FieldLinkText, ExpectedText);

            return this;
        }

        public TrainingCourseRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);

            return this;
        }

        public TrainingCourseRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public TrainingCourseRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            ScrollToElement(deleteButton);
            Click(deleteButton);

            return this;
        }

        public TrainingCourseRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            ScrollToElement(backButton);
            Click(backButton);            

            return this;

        }

        public TrainingCourseRecordPage ValidateTrainingCourseTitle(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            ScrollToElement(pageHeader);
            string ActualText = GetElementByAttributeValue(pageHeader, "title");
            Assert.IsTrue(ActualText.Contains(ExpectedText));

            return this;
        }

        public TrainingCourseRecordPage ValidateValidFromDateValue(string ExpectedText)
        {
            WaitForElementVisible(validFromDate_Field);
            ScrollToElement(validFromDate_Field);
            ValidateElementValue(validFromDate_Field, ExpectedText);

            return this;
        }

        public TrainingCourseRecordPage ValidateValidToDateValue(string ExpectedText)
        {
            WaitForElementVisible(validToDate_Field);
            ScrollToElement(validToDate_Field);
            ValidateElementValue(validToDate_Field, ExpectedText);

            return this;
        }

        public TrainingCourseRecordPage ValidateDurationInDaysValue(string ExpectedValue)
        {
            WaitForElementVisible(durationInDays_Field);
            ScrollToElement(durationInDays_Field);
            ValidateElementValue(durationInDays_Field, ExpectedValue);

            return this;
        }

        public TrainingCourseRecordPage ValidateCourseCapacityValue(string ExpectedValue)
        {
            WaitForElementVisible(courseCapacity_Field);
            ScrollToElement(courseCapacity_Field);
            ValidateElementValue(courseCapacity_Field, ExpectedValue);

            return this;
        }

        public TrainingCourseRecordPage ValidateRecurrencePicklistSelectedText(string ExpectedText)
        {
            WaitForElementToBeClickable(Recurrence_Picklist);
            ScrollToElement(Recurrence_Picklist);
            ValidatePicklistSelectedText(Recurrence_Picklist, ExpectedText);

            return this;
        }

        public TrainingCourseRecordPage ValidateTrainingItemLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(trainingItem_FieldLinkText);
            ScrollToElement(trainingItem_FieldLinkText);
            ValidateElementByTitle(trainingItem_FieldLinkText, ExpectedText);

            return this;
        }

        public TrainingCourseRecordPage ValidateCategoryPicklistSelectedText(string ExpectedText)
        {
            WaitForElementToBeClickable(Category_Picklist);
            ScrollToElement(Category_Picklist);
            ValidatePicklistSelectedText(Category_Picklist, ExpectedText);

            return this;
        }

        public TrainingCourseRecordPage ValidateProviderFieldSection(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElement(provider_Field);
            bool provider_Field_Visibility = GetElementVisibility(provider_Field);
            bool trainingProvider_Lookup_Visibility = GetElementVisibility(trainingProvider_Lookup);
            bool provider_InputText_Visibility = GetElementVisibility(provider_InputText);

            Assert.AreEqual(ExpectedVisible, provider_Field_Visibility);
            Assert.AreEqual(ExpectedVisible, trainingProvider_Lookup_Visibility);
            Assert.AreEqual(ExpectedVisible, provider_InputText_Visibility);

            return this;
        }

        public TrainingCourseRecordPage ValidateProviderLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(provider_FieldLinkText);
            ValidateElementByTitle(provider_FieldLinkText, ExpectedText);
            return this;
        }
    }
}
