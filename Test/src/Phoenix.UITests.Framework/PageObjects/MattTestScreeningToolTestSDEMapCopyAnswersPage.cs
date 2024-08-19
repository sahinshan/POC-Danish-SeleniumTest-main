using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class MattTestScreeningToolTestSDEMapCopyAnswersPage : CommonMethods
    {
        public MattTestScreeningToolTestSDEMapCopyAnswersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By documentIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=caseform&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By iframe_CWAssessmentDialog = By.Id("iframe_CWAssessmentDialog");



        readonly By selectAllCheckBox = By.Id("SDEMapsSelector_SelectAll");

        By recordCheckBox(string IdentifierFromID) => By.XPath("//input[@identifierfromid='" + IdentifierFromID + "']");
        
        By FromInfoLine1(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[2]/div[1]");
        By FromInfoLine2(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[2]/div[2]");
        By FromInfoLine3(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[2]/div[3]");
        By FromInfoLine4(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[2]/div[4]");


        By ToInfoLine3(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[3]/div[3]");
        By ToInfoLine4(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[3]/div[4]");


        By AnswerLine4(int RowPosition) => By.XPath("//*[@id='CWGridHolder']/div[" + RowPosition + "]/div[4]/div[4]");



        By AnswerCheckbox(string DocumentQuestionIdentifierId) => By.XPath("//*[@identifierfromid='" + DocumentQuestionIdentifierId + "']");


        readonly By DoNotShowAgainCheckBox = By.Id("SDEMapsSelector_NotShowAgain");
        readonly By SaveButton = By.Id("CWSDEMapsSelection_Save");
        readonly By DontCopyButton = By.Id("CWSDEMapsSelection_NotCopy");



        public MattTestScreeningToolTestSDEMapCopyAnswersPage WaitForMattTestScreeningToolTestSDEMapCopyAnswersPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(documentIFrame);
            SwitchToIframe(documentIFrame);

            WaitForElement(iframe_CWAssessmentDialog);
            SwitchToIframe(iframe_CWAssessmentDialog);

            WaitForElement(selectAllCheckBox);
            WaitForElement(DoNotShowAgainCheckBox);
            WaitForElement(SaveButton);
            WaitForElement(DontCopyButton);


            return this;

        }


        public MattTestScreeningToolTestSDEMapCopyAnswersPage TapSelectAllCheckBox()
        {
            Click(selectAllCheckBox);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage TapDoNotShowAgainCheckBox()
        {
            Click(DoNotShowAgainCheckBox);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage TapSaveButton()
        {
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage TapDontCopyButton()
        {
            Click(DontCopyButton);

            return this;
        }




        public MattTestScreeningToolTestSDEMapCopyAnswersPage TapWFMultipleChoiceCheckbox(string DocumentQuestionIdentifierId)
        {
            Click(AnswerCheckbox(DocumentQuestionIdentifierId));

            return this;
        }



        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateFromInfoLine1Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine1(RowPosition), ExpectedText);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateFromInfoLine2Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine2(RowPosition), ExpectedText);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateFromInfoLine3Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine3(RowPosition), ExpectedText);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateFromInfoLine4Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine4(RowPosition), ExpectedText);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateToInfoLine3Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(ToInfoLine3(RowPosition), ExpectedText);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateToInfoLine4Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(ToInfoLine4(RowPosition), ExpectedText);

            return this;
        }
        public MattTestScreeningToolTestSDEMapCopyAnswersPage ValidateAnswerLine4Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(AnswerLine4(RowPosition), ExpectedText);

            return this;
        }     
        
    }
}
