using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class NewCareNeedPopupPage : CommonMethods
    {
        public NewCareNeedPopupPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

       

        #endregion
        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personCarePlanInitialAssessmentRecordIFrame = By.Id("iframe_CWAssessmentDialog");

        readonly By personCarePlanFormRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personcareplanform&')]");
        readonly By NewCarePlanNeedIframe = By.Id("iframe_NewCarePlanNeedPopup");

        readonly By NewCarePlanNeed_AddNeedBtn = By.XPath("//span[contains(text(),'Add Need')]");
        readonly By NewCarePlanNeed_DeleteIcon = By.XPath("//span[contains(text(),'Delete')]/parent::button");
        readonly By NewCarePlanNeed_AddOutComeBtn = By.XPath("//span[contains(text(),'Add Outcome')]");

        readonly By NewCareNeed_NeedField = By.XPath("//input[@name='need']");
        readonly By NewCareNeed_NeedField2 = By.XPath("//div[@id='needTreeWrapperSection']//li[2]//div//input[@name='need']");
        readonly By NewCareNeed_OutComeField = By.XPath("//input[@name='outcome']");
        readonly By NewCareNeed_ActionField = By.XPath("//input[@name='action']");
        readonly By NewCareNeed_DeleteBtn = By.XPath("//button[@onclick='CW.NewCarePlanNeedPopup.DeleteField(this); return false;']");
        readonly By NewCareNeed_AddOutcomeBtn = By.XPath("//span[contains(text(),'Add Outcome')]");
        readonly By NewCareNeed_AddActionBtn = By.XPath("//span[contains(text(),'Add Action')]");
        readonly By NewCarePlanNeedDismissBtn = By.XPath("//span[contains(text(),'Dismiss')]");
        readonly By NewCarePlanNeedOkBtn = By.XPath(" //button/span[contains(text(),'Ok')]");
        readonly By NewCarePlanNeedSaveBtn = By.Id("CWSaveandClosePupup");
        readonly By NewCarePlanNeedCancelBtn = By.Id("CWCancelPopup");
       
        public NewCareNeedPopupPage WaitForNewCareNeedPopupPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personCarePlanFormRecordIFrame);
            SwitchToIframe(personCarePlanFormRecordIFrame);

            WaitForElement(personCarePlanInitialAssessmentRecordIFrame);
            SwitchToIframe(personCarePlanInitialAssessmentRecordIFrame);

            WaitForElement(NewCarePlanNeedIframe);
            SwitchToIframe(NewCarePlanNeedIframe);

            WaitForElementVisible(NewCarePlanNeed_AddNeedBtn);
           
            return this;
        }

        public NewCareNeedPopupPage TapAddNeed()
        {
            Click(NewCarePlanNeed_AddNeedBtn);

            return this;
        }
        public NewCareNeedPopupPage TapDelete()
        {
            Click(NewCarePlanNeed_DeleteIcon);

            return this;
        }

        public NewCareNeedPopupPage TapAddOutcome()
        {
            Click(NewCareNeed_AddOutcomeBtn);

            return this;
        }

        public NewCareNeedPopupPage TapAddAction()
        {
            Click(NewCareNeed_AddActionBtn);

            return this;
        }

        public NewCareNeedPopupPage TapDismiss()
        {
            WaitForElementToBeClickable(NewCarePlanNeedDismissBtn);
            MoveToElementInPage(NewCarePlanNeedDismissBtn);
            Click(NewCarePlanNeedDismissBtn);

            return this;
        }

        public NewCareNeedPopupPage TapOk()
        {
            Click(NewCarePlanNeedOkBtn);

            return this;
        }
        public NewCareNeedPopupPage TapCancel()
        {
            WaitForElement(NewCarePlanNeedCancelBtn);
            WaitForElementToBeClickable(NewCarePlanNeedCancelBtn);
            Click(NewCarePlanNeedCancelBtn);

            return this;
        }

        public NewCareNeedPopupPage TapSave()
        {
            Click(NewCarePlanNeedSaveBtn);

            return this;
        }
        public NewCareNeedPopupPage ValidateNeedield_Visible()
        {
            WaitForElementVisible(NewCareNeed_NeedField);

            return this;
        }

        public NewCareNeedPopupPage ValidateDeleteBtn_Visible()
        {
            WaitForElementVisible(NewCareNeed_DeleteBtn);

            return this;
        }

        public NewCareNeedPopupPage ValidateAddOutcomeBtn_Visible()
        {
            WaitForElementVisible(NewCareNeed_AddOutcomeBtn);

            return this;
        }

        public NewCareNeedPopupPage InsertNeedield_Txt(String TextToInsert)
        {
            SendKeys(NewCareNeed_NeedField, TextToInsert);

            return this;
        }

        public NewCareNeedPopupPage InsertNeedield2_Txt(String TextToInsert)
        {
            SendKeys(NewCareNeed_NeedField2, TextToInsert);

            return this;
        }
        public NewCareNeedPopupPage InsertOutComeField_Txt(String TextToInsert)
        {
            SendKeys(NewCareNeed_OutComeField, TextToInsert);

            return this;
        }
        public NewCareNeedPopupPage InsertActionField_Txt(String TextToInsert)
        {
            SendKeys(NewCareNeed_ActionField, TextToInsert);

            return this;
        }
    }
}
