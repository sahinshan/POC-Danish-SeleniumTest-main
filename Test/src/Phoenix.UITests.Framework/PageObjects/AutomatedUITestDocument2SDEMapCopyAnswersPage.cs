using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class AutomatedUITestDocument2SDEMapCopyAnswersPage : CommonMethods
    {
        public AutomatedUITestDocument2SDEMapCopyAnswersPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
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





        readonly By LocationRow1AnswerValue = By.XPath("//*[@id='CWGridHolder']/div[2]/div[4]/div[4]");
        readonly By LocationRow2AnswerValue = By.XPath("//*[@id='CWGridHolder']/div[3]/div[4]/div[4]");
        readonly By TestDecRow1AnswerValue = By.XPath("//*[@id='CWGridHolder']/div[4]/div[4]/div[4]");
        readonly By TestDecRow2AnswerValue = By.XPath("//*[@id='CWGridHolder']/div[5]/div[4]/div[4]");
        readonly By WFBooleanAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[6]/div[4]/div[4]");
        readonly By WFDateAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[7]/div[4]/div[4]");
        readonly By WFDecimalAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[8]/div[4]/div[4]");
        readonly By WFLookupAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[9]/div[4]/div[4]");
        readonly By WFMultipleChoiceAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[10]/div[4]/div[4]");
        readonly By WFMultipleResponseAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[11]/div[4]/div[4]");
        readonly By WFNumericAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[12]/div[4]/div[4]");
        readonly By WFParagraphAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[13]/div[4]/div[4]");
        readonly By WFPickListAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[14]/div[4]/div[4]");
        readonly By WFShortAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[15]/div[4]/div[4]");
        readonly By WFTimeAnswerValue = By.XPath("//*[@id='CWGridHolder']/div[16]/div[4]/div[4]");


        readonly By WFMultipleChoiceCheckbox = By.XPath("//*[@identifierfromid='979a5f7b-8500-ea11-a2c7-005056926fe4']");
        readonly By WFDecimalAnswerCheckbox = By.XPath("//*[@identifierfromid='11c98281-8500-ea11-a2c7-005056926fe4']");
        readonly By WFMultipleResponseCheckbox = By.XPath("//*[@identifierfromid='b79edb8b-8500-ea11-a2c7-005056926fe4']");
        readonly By WFNumericCheckbox = By.XPath("//*[@identifierfromid='cdb46792-8500-ea11-a2c7-005056926fe4']");
        readonly By WFLookupCheckbox = By.XPath("//*[@identifierfromid='ddb46792-8500-ea11-a2c7-005056926fe4']");
        readonly By WFDateCheckbox = By.XPath("//*[@identifierfromid='8d3ded9a-8500-ea11-a2c7-005056926fe4']");
        readonly By WFShortAnswerCheckbox = By.XPath("//*[@identifierfromid='e2e56ea2-8500-ea11-a2c7-005056926fe4']");
        readonly By WFParagraphCheckbox = By.XPath("//*[@identifierfromid='2f4747a9-8500-ea11-a2c7-005056926fe4']");
        readonly By WFPicklistCheckbox = By.XPath("//*[@identifierfromid='5598ceaf-8500-ea11-a2c7-005056926fe4']");
        readonly By WFBooleanCheckbox = By.XPath("//*[@identifierfromid='d7d529c2-8500-ea11-a2c7-005056926fe4']");
        readonly By WFTimeCheckbox = By.XPath("//*[@identifierfromid='8dee77c8-8500-ea11-a2c7-005056926fe4']");
        readonly By WFLocationRow1Checkbox = By.XPath("//*[@identifierfromid='77e1c1d8-8500-ea11-a2c7-005056926fe4']");
        readonly By WFTestDecRow1Checkbox = By.XPath("//*[@identifierfromid='7be1c1d8-8500-ea11-a2c7-005056926fe4']");
        readonly By WFLocationRow2Checkbox = By.XPath("//*[@identifierfromid='7fe1c1d8-8500-ea11-a2c7-005056926fe4']");
        readonly By WFTestDecRow2Checkbox = By.XPath("//*[@identifierfromid='83e1c1d8-8500-ea11-a2c7-005056926fe4']");


        readonly By DoNotShowAgainCheckBox = By.Id("SDEMapsSelector_NotShowAgain");
        readonly By SaveButton = By.Id("CWSDEMapsSelection_Save");
        readonly By DontCopyButton = By.Id("CWSDEMapsSelection_NotCopy");



        public AutomatedUITestDocument2SDEMapCopyAnswersPage WaitForAutomatedUITestDocument2SDEMapCopyAnswersPageToLoad()
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


        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapSelectAllCheckBox()
        {
            Click(selectAllCheckBox);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapDoNotShowAgainCheckBox()
        {
            Click(DoNotShowAgainCheckBox);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapSaveButton()
        {
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapDontCopyButton()
        {
            Click(DontCopyButton);

            return this;
        }


        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFMultipleChoiceCheckbox()
        {
            Click(WFMultipleChoiceCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFDecimalCheckbox()
        {
            Click(WFDecimalAnswerCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFMultipleResponseCheckbox()
        {
            Click(WFMultipleResponseCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFNumericCheckbox()
        {
            Click(WFNumericCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFLookupCheckbox()
        {
            Click(WFLookupCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFDateCheckbox()
        {
            Click(WFDateCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFShortAnswerCheckbox()
        {
            Click(WFShortAnswerCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFParagraphCheckbox()
        {
            Click(WFParagraphCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFPicklistCheckbox()
        {
            Click(WFPicklistCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFBooleanCheckbox()
        {
            Click(WFBooleanCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFTimeCheckbox()
        {
            Click(WFTimeCheckbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFLocationRow1Checkbox()
        {
            Click(WFLocationRow1Checkbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFTestDecRow1Checkbox()
        {
            Click(WFTestDecRow1Checkbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFLocationRow2Checkbox()
        {
            Click(WFLocationRow2Checkbox);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage TapWFTestDecRow2Checkbox()
        {
            Click(WFTestDecRow2Checkbox);

            return this;
        }




        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFMultipleChoiceAnswer(string ExpectedAnswer) 
        {
            ValidateElementText(WFMultipleChoiceAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFDecimalAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFDecimalAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFMultipleResponseAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFMultipleResponseAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFNumericAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFNumericAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFLookupAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFLookupAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFDateAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFDateAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFShortAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFShortAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFParagraphAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFParagraphAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFPicklistAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFPickListAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFBooleanAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFBooleanAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFTimeAnswer(string ExpectedAnswer)
        {
            ValidateElementText(WFTimeAnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFLocationRow1Answer(string ExpectedAnswer)
        {
            ValidateElementText(LocationRow1AnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFTestDecRow1Answer(string ExpectedAnswer)
        {
            ValidateElementText(TestDecRow1AnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFLocationRow2Answer(string ExpectedAnswer)
        {
            ValidateElementText(LocationRow2AnswerValue, ExpectedAnswer);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateWFTestDecRow2Answer(string ExpectedAnswer)
        {
            ValidateElementText(TestDecRow2AnswerValue, ExpectedAnswer);

            return this;
        }






        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateFromInfoLine1Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine1(RowPosition), ExpectedText);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateFromInfoLine2Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine2(RowPosition), ExpectedText);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateFromInfoLine3Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine3(RowPosition), ExpectedText);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateFromInfoLine4Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(FromInfoLine4(RowPosition), ExpectedText);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateToInfoLine3Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(ToInfoLine3(RowPosition), ExpectedText);

            return this;
        }
        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateToInfoLine4Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(ToInfoLine4(RowPosition), ExpectedText);

            return this;
        }

        public AutomatedUITestDocument2SDEMapCopyAnswersPage ValidateAnswerLine4Text(int RowPosition, string ExpectedText)
        {
            ValidateElementText(AnswerLine4(RowPosition), ExpectedText);

            return this;
        }
        
    }
}
