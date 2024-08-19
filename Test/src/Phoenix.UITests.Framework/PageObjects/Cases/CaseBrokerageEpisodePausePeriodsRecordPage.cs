using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class CaseBrokerageEpisodePausePeriodsRecordPage : CommonMethods
    {
        public CaseBrokerageEpisodePausePeriodsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


  

        readonly By contentIFrame = By.Id("CWContentIFrame");        
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageepisodepauseperiod')]");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back']");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");


        #region Fields

        readonly By brokerageepisodeid_Field = By.Id("CWField_brokerageepisodeid");
        readonly By pausedate_Field = By.Id("CWField_pausedatetime");
        readonly By pausedatetime_Field = By.Id("CWField_pausedatetime_Time");
        readonly By pauseReasonid_Field = By.Id("CWField_brokerageepisodepausereasonid");
        readonly By otherPauseReason_Field = By.Id("CWField_otherpausereason");
        readonly By ReasponsibleTeam_Field = By.Id("CWField_ownerid");
        readonly By status_Field = By.Id("CWField_statusid");
        readonly By restartDate_Field = By.Id("CWField_restartdatetime");
        readonly By restarttime_Field = By.Id("CWField_restartdatetime_Time");

        readonly By brokerageepisodeid_FieldHeader = By.Id("CWLabelHolder_brokerageepisodeid");
        readonly By pausedatetime_FieldHeader = By.Id("CWLabelHolder_pausedatetime");
        readonly By pauseReasonid_FieldHeader = By.Id("CWLabelHolder_brokerageepisodepausereasonid");
        readonly By otherPauseReason_FieldHeader = By.Id("CWLabelHolder_otherpausereason");
        readonly By ReasponsibleTeam_FieldHeader = By.Id("CWLabelHolder_ownerid");
        readonly By status_FieldHeader = By.Id("CWLabelHolder_statusid");
        readonly By restartDateTime_FieldHeader = By.Id("CWLabelHolder_restartdatetime");




        #endregion

       


        public CaseBrokerageEpisodePausePeriodsRecordPage ClickDeleteButton()
        {
            WaitForElementVisible(DeleteButton);
            Click(DeleteButton);
            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ClickBackButton()
        {

            WaitForElementVisible(Back_Button);
            Click(Back_Button);

            return this;
        }

      

        public CaseBrokerageEpisodePausePeriodsRecordPage WaitForCaseBrokerageEpisodePausePeriodsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(brokerageepisodeid_FieldHeader);
            WaitForElement(pausedatetime_FieldHeader);
            WaitForElement(pauseReasonid_FieldHeader);
            WaitForElement(otherPauseReason_FieldHeader);
            WaitForElement(ReasponsibleTeam_FieldHeader);
            WaitForElement(status_FieldHeader);
            WaitForElement(restartDateTime_FieldHeader);



            return this;
        }

       
        public CaseBrokerageEpisodePausePeriodsRecordPage WaitForCaseBrokerageEpisodePausePeriodsRecordPageToLoad(string PageTitle)
        {
            WaitForCaseBrokerageEpisodePausePeriodsRecordPageToLoad();

            ValidateElementTextContainsText(pageHeader, "Brokerage Episode Pause Period:\r\n" + PageTitle);

            return this;
        }

       

        public CaseBrokerageEpisodePausePeriodsRecordPage ValidateBrokerageEpisodeidFieldText(string ExpectedText)
        {
            ValidateElementText(brokerageepisodeid_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ValidatePauseDateField(string ExpectedText)
        {
            ValidateElementText(pausedate_Field, ExpectedText);

            return this;
        }

        public CaseBrokerageEpisodePausePeriodsRecordPage ValidatePauseTimeField(string ExpectedText)
        {
            ValidateElementText(pausedatetime_Field, ExpectedText);

            return this;
        }

        public CaseBrokerageEpisodePausePeriodsRecordPage ValidateRestartDateField(string ExpectedText)
        {
            ValidateElementText(restartDate_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ValidateRestartTimeField(string ExpectedText)
        {
            ValidateElementText(restarttime_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ValidatePauseReasonIdField(string ExpectedText)
        {
            ValidateElementText(pauseReasonid_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ValidateOtherReasonFieldText(string ExpectedText)
        {
            ValidateElementText(otherPauseReason_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ValidateResponsibleTeamFieldText(string ExpectedText)
        {
            ValidateElementText(ReasponsibleTeam_Field, ExpectedText);

            return this;
        }
        public CaseBrokerageEpisodePausePeriodsRecordPage ValidateStatusFieldText(string ExpectedText)
        {
            ValidateElementText(status_Field, ExpectedText);

            return this;
        }
       

    }
}
