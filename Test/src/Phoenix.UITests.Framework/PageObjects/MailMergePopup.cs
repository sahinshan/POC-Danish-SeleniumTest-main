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
    public class MailMergePopup : CommonMethods
    {
        public MailMergePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By iframe_MailMerge = By.Id("iframe_MailMerge");

        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/div/h1[text()='Mail Merge']");


        #region Labels

        readonly By MailMergeTemplate_Label = By.XPath("//*[@id='TemplatesLabel'][text()='Mail Merge Template']");
        
        readonly By MergeLabel_Label = By.XPath("//*[@id='MergeLabel'][text()='Merge']");
        readonly By SelectedRecordsOnCurrentPage_Label = By.XPath("//*[@id='SelectedRecordsLabel'][text()='Selected records on current page']");
        readonly By AllRecordsFromSelectedView_Label = By.XPath("//*[@id='AllRecordsLabel'][text()='All records from selected view']");
        
        readonly By CreateActivity_Label = By.XPath("//*[@id='CreateActivityLabel'][text()='Create Activity']");

        readonly By CreateAs_Label = By.XPath("//*[@id='CreateAsLabel'][text()='Create As']");
        readonly By CompletedActivity_Label = By.XPath("//*[@id='ActivityStatusCloseLabel'][text()='Completed Activity']");
        readonly By OpenActivity_Label = By.XPath("//*[@id='ActivityStatusOpenLabel'][text()='Open Activity']");
        
        readonly By ResponsibleTeam_Label = By.XPath("//*[@id='CWResponsibleTeamIdLabel'][text()='Responsible Team']");
        readonly By ActivitySubject_Label = By.XPath("//*[@id='ActivitySubjectLabel'][text()='Activity Subject']");

        #endregion

        #region Fields

        readonly By MailMergeTemplate_Picklist = By.Id("TemplatesSelect");

        readonly By SelectedRecordsOnCurrentPage_RadioButton = By.Id("rdSelectedRecords");
        readonly By AllRecordsFromSelectedView_RadioButton = By.Id("rdAllRecords");

        readonly By CreateActivityYes_RadioButton = By.Id("rdActivityYes");
        readonly By CreateActivityNo_RadioButton = By.Id("rdActivityNo");

        readonly By CompletedActivity_RadioButton = By.Id("rdActivityStatusClose");
        readonly By OpenActivity_RadioButton = By.Id("rdActivityStatusOpen");

        readonly By ResponsibleTeam_Field = By.Id("CWResponsibleTeamId_Link");
        readonly By ResponsibleTeam_RemoveButton = By.Id("CWClearLookup_CWResponsibleTeamId");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_CWResponsibleTeamId");

        readonly By ActivitySubject_Field = By.Id("txtSubject");

        #endregion


        readonly By OKButton = By.Id("OKButton");
        readonly By CancelButton = By.Id("CancelButton");


        /// <summary>
        /// This variable represents the element that holds the pdf file in a popup window when a user prints a mail merge document
        /// </summary>
        readonly By pdfEmbededElement = By.XPath("//html/body/embed[@type='application/pdf']");
        




        public MailMergePopup WaitForMailMergePopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(iframe_MailMerge);
            SwitchToIframe(iframe_MailMerge);

            WaitForElement(popupHeader);

            WaitForElement(MailMergeTemplate_Label);

            WaitForElement(MergeLabel_Label);
            WaitForElement(SelectedRecordsOnCurrentPage_Label);
            WaitForElement(AllRecordsFromSelectedView_Label);
            
            WaitForElement(CreateActivity_Label);
            
            WaitForElement(CreateAs_Label);
            WaitForElement(CompletedActivity_Label);
            WaitForElement(OpenActivity_Label);
            
            WaitForElement(ActivitySubject_Label);

            return this;
        }

        public MailMergePopup WaitForMailMergePopupToReload()
        {
            WaitForElement(popupHeader);

            WaitForElement(MailMergeTemplate_Label);

            WaitForElement(MergeLabel_Label);
            WaitForElement(SelectedRecordsOnCurrentPage_Label);
            WaitForElement(AllRecordsFromSelectedView_Label);

            WaitForElement(CreateActivity_Label);

            WaitForElement(CreateAs_Label);
            WaitForElement(CompletedActivity_Label);
            WaitForElement(OpenActivity_Label);

            WaitForElement(ActivitySubject_Label);

            return this;
        }



        public MailMergePopup SelectMailMergeTemplateByText(string TextToSelect)
        {
            SelectPicklistElementByText(MailMergeTemplate_Picklist, TextToSelect);

            return this;
        }

        public MailMergePopup TapSelectedRecordsOnCurrentPageRadioButton()
        {
            Click(SelectedRecordsOnCurrentPage_RadioButton);

            return this;
        }

        public MailMergePopup TapAllRecordsFromSelectedViewRadioButton()
        {
            Click(AllRecordsFromSelectedView_RadioButton);

            return this;
        }

        public MailMergePopup TapCreateActivityYesRadioButton()
        {
            Click(CreateActivityYes_RadioButton);

            return this;
        }

        public MailMergePopup TapCreateActivityNoRadioButton()
        {
            Click(CreateActivityNo_RadioButton);

            return this;
        }

        public MailMergePopup TapCompletedActivityRadioButton()
        {
            Click(CompletedActivity_RadioButton);

            return this;
        }

        public MailMergePopup TapOpenActivityRadioButton()
        {
            Click(OpenActivity_RadioButton);

            return this;
        }

        public MailMergePopup TapResponsibleTeamLookupButton()
        {
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public MailMergePopup InsertActivitySubject(string TextToInsert)
        {
            SendKeys(ActivitySubject_Field, TextToInsert);

            return this;
        }


        /// <summary>
        /// this method will validate that a popup window was open with a print pdf file.
        /// This method will try to locate the popup, switch the driver focus to the popup and will search for the element "//html/body/embed[@type='application/pdf']"  in the popup
        /// </summary>
        /// <returns></returns>
        public MailMergePopup ValidatePDFPopupIsOpen()
        {
            string currentWindow = GetCurrentWindowIdentifier();
            string popupWindow = GetAllWindowIdentifier().Where(c => c != currentWindow).FirstOrDefault();

            SwitchToWindow(popupWindow);

            WaitForElement(pdfEmbededElement);

            return this;
        }


        public MailMergePopup ClickCancelButton()
        {
            Click(CancelButton);

            return this;
        }

        public MailMergePopup ClickOKButton()
        {
            WaitForElement(OKButton);
            Click(OKButton);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            return this;
        }

        

    }
}
