using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents create invoice file dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class CreateInvoiceFilePopup : CommonMethods
    {
        public CreateInvoiceFilePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");               
        readonly By iframe_CreateInvoiceFile = By.Id("iframe_CareProviderFinanceInvoiceFileCreator");
        
        readonly By popupHeader = By.XPath("//*[@id='CWHeaderTitle']");

        #region Labels

        readonly By MailMergeTemplateLookup_Label = By.XPath("//*[@id='TemplatesLookupLabel'][text()='Mail Merge Template']");        
        readonly By MergeLabel_Label = By.XPath("//*[@id='MergeLabel'][text()='Merge']");
        readonly By SelectedRecordsOnCurrentPage_Label = By.XPath("//*[@id='SelectedRecordsLabel'][text()='Selected records on current page']");
        readonly By AllRecordsFromSelectedView_Label = By.XPath("//*[@id='AllRecordsLabel'][text()='All records from selected view']");      
        readonly By SendMailsAutomatically_Label = By.XPath("//*[@id='SendLabel'][text()='Send mails automatically']");

        #endregion

        #region Fields

        readonly By CreateInvoice_MailMergeTemplate_LookupButton = By.Id("CWLookupBtn_TemplatesLookup");
        
        readonly By CreateInvoice_MailMergeTemplate_LinkText = By.Id("TemplatesLookup_Link");

        readonly By SelectedRecordsOnCurrentPage_RadioButton = By.Id("rdSelectedRecords");
        readonly By AllRecordsFromSelectedView_RadioButton = By.Id("rdAllRecords");

        readonly By SendMailsAutomaticallyYes_RadioButton = By.Id("rdSendYes");
        readonly By SendMailsAutomaticallyNo_RadioButton = By.Id("rdSendNo");

        #endregion


        readonly By OKButton = By.Id("CWSave");
        readonly By CancelButton = By.Id("CWClose");



        public CreateInvoiceFilePopup WaitForCreateInvoiceFilePopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CreateInvoiceFile);
            SwitchToIframe(iframe_CreateInvoiceFile);

            WaitForElement(popupHeader);

            WaitForElement(MailMergeTemplateLookup_Label);

            WaitForElement(MergeLabel_Label);
            WaitForElement(SelectedRecordsOnCurrentPage_Label);
            WaitForElement(AllRecordsFromSelectedView_Label);
            
            WaitForElement(SendMailsAutomatically_Label);
            
            return this;
        }

        public CreateInvoiceFilePopup WaitForCreateInvoiceFilePopupToReload()
        {
            WaitForElement(popupHeader);

            WaitForElement(MailMergeTemplateLookup_Label);

            WaitForElement(MergeLabel_Label);
            WaitForElement(SelectedRecordsOnCurrentPage_Label);
            WaitForElement(AllRecordsFromSelectedView_Label);

            WaitForElement(SendMailsAutomatically_Label);

            return this;
        }



        public CreateInvoiceFilePopup ClickMailMergeTemplateLookupButton()
        {
            WaitForElementToBeClickable(CreateInvoice_MailMergeTemplate_LookupButton);
            MoveToElementInPage(CreateInvoice_MailMergeTemplate_LookupButton);
            Click(CreateInvoice_MailMergeTemplate_LookupButton);

            return this;
        }

        public CreateInvoiceFilePopup ClickSelectedRecordsOnCurrentPageRadioButton()
        {
            WaitForElementToBeClickable(SelectedRecordsOnCurrentPage_RadioButton);
            MoveToElementInPage(SelectedRecordsOnCurrentPage_RadioButton);
            Click(SelectedRecordsOnCurrentPage_RadioButton);

            return this;
        }

        //Method to click all records from selected view radio button
        public CreateInvoiceFilePopup ClickAllRecordsFromSelectedViewRadioButton()
        {
            WaitForElementToBeClickable(AllRecordsFromSelectedView_RadioButton);
            MoveToElementInPage(AllRecordsFromSelectedView_RadioButton);
            Click(AllRecordsFromSelectedView_RadioButton);

            return this;
        }

        //Method to click send mails automatically yes radio button
        public CreateInvoiceFilePopup ClickSendMailsAutomaticallyYesRadioButton()
        {
            WaitForElementToBeClickable(SendMailsAutomaticallyYes_RadioButton);
            MoveToElementInPage(SendMailsAutomaticallyYes_RadioButton);
            Click(SendMailsAutomaticallyYes_RadioButton);

            return this;
        }

        //Method to click send mails automatically no radio button
        public CreateInvoiceFilePopup ClickSendMailsAutomaticallyNoRadioButton()
        {
            WaitForElementToBeClickable(SendMailsAutomaticallyNo_RadioButton);
            MoveToElementInPage(SendMailsAutomaticallyNo_RadioButton);
            Click(SendMailsAutomaticallyNo_RadioButton);

            return this;
        }


        public CreateInvoiceFilePopup ClickCancelButton()
        {
            WaitForElementToBeClickable(CancelButton);
            MoveToElementInPage(CancelButton);
            Click(CancelButton);

            return this;
        }

        public CreateInvoiceFilePopup ClickOKButton()
        {
            WaitForElementToBeClickable(OKButton);
            MoveToElementInPage(OKButton);
            Click(OKButton);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            return this;
        }        

    }
}
