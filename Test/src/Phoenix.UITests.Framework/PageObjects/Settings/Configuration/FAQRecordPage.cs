    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FAQRecordPage : CommonMethods
    {
        public FAQRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SummaryDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=faq&')]"); 


        
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By DetailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By ApplicationsSection = By.XPath("//li[@id='CWNavGroup_Applications']/a[@title='Applications']");

        #endregion

        #region FieldTitles

        readonly By title_FieldTitle = By.XPath("//*[@id='CWLabelHolder_title']/label");
        readonly By language_FieldTitle = By.XPath("//*[@id='CWLabelHolder_languageid']/label");
        readonly By status_FieldTitle = By.XPath("//*[@id='CWLabelHolder_statusid']/label");
        readonly By keywords_FieldTitle = By.XPath("//*[@id='CWLabelHolder_keywords']/label");
        readonly By category_FieldTitle = By.XPath("//*[@id='CWLabelHolder_faqcategoryid']/label");
        readonly By ResponsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By upvotes_FieldTitle = By.XPath("//*[@id='CWLabelHolder_numberofupvotes']/label");
        readonly By downvotes_FieldTitle = By.XPath("//*[@id='CWLabelHolder_numberofdownvotes']/label");

        #endregion

        #region Fields

        readonly By title_Field = By.Id("CWField_title");
        readonly By language_LinkField = By.Id("CWField_languageid_Link");
        readonly By language_RemoveButton = By.Id("CWClearLookup_languageid");
        readonly By language_LookupButton = By.Id("CWLookupBtn_languageid");
        readonly By status_Picklist = By.Id("CWField_statusid");
        readonly By keywords_Field = By.Id("CWField_keywords");
        readonly By category_LinkField = By.Id("CWField_faqcategoryid_Link");
        readonly By category_removeButton = By.Id("CWClearLookup_faqcategoryid");
        readonly By category_LookupButton = By.Id("CWLookupBtn_faqcategoryid");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By upvotes_Field = By.Id("CWField_numberofupvotes");
        readonly By downvotes_Field = By.Id("CWField_numberofdownvotes");

        #endregion



        public FAQRecordPage WaitForFAQRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SummaryDashboardRecordIFrame);
            SwitchToIframe(SummaryDashboardRecordIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            
            WaitForElement(DetailsSection);

            WaitForElement(title_FieldTitle);
            WaitForElement(language_FieldTitle);
            WaitForElement(status_FieldTitle);
            WaitForElement(keywords_FieldTitle);
            WaitForElement(category_FieldTitle);
            WaitForElement(ResponsibleTeam_FieldTitle);
            WaitForElement(upvotes_FieldTitle);
            WaitForElement(downvotes_FieldTitle);

            return this;
        }




        public FAQRecordPage InsertName(string ValueToInsert)
        {
            SendKeys(title_Field, ValueToInsert);

            return this;
        }

        public FAQRecordPage InsertKeywords(string ValueToInsert)
        {
            SendKeys(keywords_Field, ValueToInsert);

            return this;
        }

        public FAQRecordPage TapLanguageLookupButton()
        {
            Click(language_LookupButton);

            return this;
        }

        public FAQRecordPage TapLanguageRemoveButton()
        {
            Click(language_RemoveButton);

            return this;
        }

        public FAQRecordPage SelectStatus(string TextToSelect)
        {
            SelectPicklistElementByText(status_Picklist, TextToSelect);

            return this;
        }

        public FAQRecordPage TapCategoryLookupButton()
        {
            Click(category_LookupButton);

            return this;
        }

        public FAQRecordPage TapCategoryRemoveButton()
        {
            Click(category_removeButton);

            return this;
        }

        public FAQRecordPage TapResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public FAQRecordPage TapResponsibleTeamRemoveButton()
        {
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }


        public FAQRecordPage ValidateNameValue(string ExpectedValue)
        {
            ValidateElementValue(title_Field, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateLanguageValue(string ExpectedValue)
        {
            ValidateElementText(language_LinkField, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateStatusValue(string ExpectedValue)
        {
            ValidatePicklistSelectedText(status_Picklist, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateKeywordsValue(string ExpectedValue)
        {
            ValidateElementValue(keywords_Field, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateCategoryValue(string ExpectedValue)
        {
            ValidateElementText(category_LinkField, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateResponsibleTeamValue(string ExpectedValue)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateUpvotesValue(string ExpectedValue)
        {
            ValidateElementValue(upvotes_Field, ExpectedValue);

            return this;
        }

        public FAQRecordPage ValidateDownVotesValue(string ExpectedValue)
        {
            ValidateElementValue(downvotes_Field, ExpectedValue);

            return this;
        }



        public FAQRecordPage TapDetailsTab()
        {
            Click(DetailsSection);

            return this;
        }

        public FAQRecordPage TapApplicationsTab()
        {
            Click(ApplicationsSection);

            return this;
        }


        public FAQRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FAQRecordPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            //WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FAQRecordPage TapDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
    }
}
