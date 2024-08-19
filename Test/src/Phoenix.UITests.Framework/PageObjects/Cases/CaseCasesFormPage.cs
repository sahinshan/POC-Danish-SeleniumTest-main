using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// This class represents the Forms(Case) page when accessed via a Case record
    /// ()
    /// </summary>
    public class CaseCasesFormPage : CommonMethods
    {
        public CaseCasesFormPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src, 'editpage.aspx')]");
        readonly By caseFormFrame = By.Id("CWUrlPanel_IFrame");
        readonly By detailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");
        readonly By newButton = By.Id("TI_NewRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By BrokerageEpisodesLeftSubMenuItem = By.XPath("//a[@id='CWNavItem_BrokerageEpisode']");
        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By RelatedItems_LeftMenu = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");


        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Forms (Case)']");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordStatusCell(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[5]");
        By PersonRowCheckBox(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[1]/input");


        //public CaseCasesFormPage WaitForCaseCaseFormPageToLoad()
        //{
        //    Wait.Until(c => c.FindElement(contentIFrame));
        //    driver.SwitchTo().Frame(driver.FindElement(contentIFrame));

        //    Wait.Until(c => c.FindElement(pageHeader));

        //    if (driver.FindElement(pageHeader).Text != "Forms (Case)")
        //        throw new Exception("Page title do not equals: \"Forms (Case)\" ");

        //    return this;
        //}

        public CaseCasesFormPage WaitForCaseCaseFormPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);           

            WaitForElement(caseFormFrame);
            SwitchToIframe(caseFormFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(pageHeader);

            ValidateElementText(pageHeader, "Forms (Case)");

            return this;
        }

       


        /// <summary>
        /// Tap on the New+ button
        /// </summary>
        public CaseFormPage TapNewButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            driver.FindElement(newButton).Click();

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage TapDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            driver.FindElement(DeleteButton).Click();

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage OpenRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifier(RecordID));
            MoveToElementInPage(RecordIdentifier(RecordID));
            this.Click(RecordIdentifier(RecordID));

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseFormPage SelectCaseFormRecord(string caseid)
        {
            WaitForElement(PersonRowCheckBox(caseid));
            Click(PersonRowCheckBox(caseid));

            return new CaseFormPage(this.driver, this.Wait, this.appURL);
        }

        public CaseCasesFormPage ValidateStatusCellText(string RecordID, string ExpectedText)
        {
            this.WaitForElement(RecordStatusCell(RecordID));
            this.ValidateElementText(RecordStatusCell(RecordID), ExpectedText);

            return this;
        }

        public CaseCasesFormPage NavigateToBrokerageEpisodes()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            WaitForElementToBeClickable(RelatedItems_LeftMenu);
            Click(RelatedItems_LeftMenu);

            WaitForElementToBeClickable(BrokerageEpisodesLeftSubMenuItem);
            Click(BrokerageEpisodesLeftSubMenuItem);

            return this;
        }

        public CaseCasesFormPage ClickRefreshButton()
        {            
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }
    }
}
