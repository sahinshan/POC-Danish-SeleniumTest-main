using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
   
{
   public class PersonAlertAndHazardReviewPage : CommonMethods
    {

        public PersonAlertAndHazardReviewPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personalertandhazard&')]");
        readonly By alertAndHazardsReviewFrame = By.Id("CWUrlPanel_IFrame");


        readonly By newRecordButton = By.XPath("//*[@id='TI_NewRecordButton']");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordRow(string recordID) => By.XPath("//tbody/tr['" + recordID + "']");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");



        public PersonAlertAndHazardReviewPage WaitForPersonAlertAndHazardReviewPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(alertAndHazardsReviewFrame);
            this.SwitchToIframe(alertAndHazardsReviewFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);



            return this;
        }

        public PersonAlertAndHazardReviewPage ClickNewRecordButton()
        {
           
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);
            return this;
        }

        public PersonAlertAndHazardReviewPage SelectPersonAlertAndHazardReviewRecord(string RecordId)
        {
            WaitForElement(recordCheckbox(RecordId));
            Click(recordCheckbox(RecordId));

            return this;
        }

        public PersonAlertAndHazardReviewPage OpenPersonAlertAndHazardReviewRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public PersonAlertAndHazardReviewPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            MoveToElementInPage(deleteRecordButton);
            Click(deleteRecordButton);
            return this;
        }
        public PersonAlertAndHazardReviewPage ClickBackButton()
        {

            WaitForElementVisible(back_Button);
            Click(back_Button);

            return this;
        }
    }
}
