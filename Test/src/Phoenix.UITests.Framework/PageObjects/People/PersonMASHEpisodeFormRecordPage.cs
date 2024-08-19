using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonMASHEpisodeFormRecordPage : CommonMethods
    {

        public PersonMASHEpisodeFormRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mashepisodeform')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By editAssessmentButton = By.Id("TI_EditAssessmentButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");


        #region Fields

        readonly By Status_Field = By.XPath("//*[@id='CWField_assessmentstatusid']");

        #endregion

        public PersonMASHEpisodeFormRecordPage WaitForPersonMASHEpisodeFormRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }



        public PersonMASHEpisodeFormRecordPage SelectStatus_Field(string TextToSelect)
        {
            SelectPicklistElementByText(Status_Field, TextToSelect);

            return this;
        }
       
        public PersonMASHEpisodeFormRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public PersonMASHEpisodeFormRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonMASHEpisodeFormRecordPage ClickEditAssessmentButton()
        {
            WaitForElementToBeClickable(editAssessmentButton);
            Click(editAssessmentButton);

            return this;
        }
        public PersonMASHEpisodeFormRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

        

    }
}
