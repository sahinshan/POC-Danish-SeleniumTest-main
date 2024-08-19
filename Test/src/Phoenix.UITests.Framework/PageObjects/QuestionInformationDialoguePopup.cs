using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// This class represents the Section information popup.
    /// this popup is displayed when a user open an assessment in edit mode, taps on a section (or sub section) menu button and tap on the "Section Information" link
    /// </summary>
    public class QuestionInformationDialoguePopup : CommonMethods
    {
        public QuestionInformationDialoguePopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWInformationDialogWindow = By.Id("iframe_CWInformationDialogWindow");

        readonly By popupTitle = By.XPath("//*[@id='DataTitle']");


        #region Labels

        readonly By CreatedByLabel = By.XPath("//*[@id='CWCreatedBy'][text()='Created By:']");
        readonly By CreatedOnLabel = By.XPath("//*[@id='CWCreatedOn'][text()='Created On:']");
        readonly By ModifiedByLabel = By.XPath("//*[@id='CWModifiedBy'][text()='Modified By:']");
        readonly By ModifiedOnLabel = By.XPath("//*[@id='CWModifiedOn'][text()='Modified On:']");
        readonly By IdLabel = By.XPath("//*[@id='CWId'][text()='Id:']");
        readonly By OwnerLabel = By.XPath("//*[@id='CWOwner'][text()='Owner:']");
        readonly By ExcludeFromPrintLabel = By.XPath("//*[@id='CWExcludeFromPrint'][text()='Exclude From Print']");

        #endregion

        #region Fields

        By CreatedByFieldValue(string ExpectedText) => By.XPath("//*[@id='CreatedByLabel'][text()='" + ExpectedText + "']");
        By CreatedOnFieldValue(string ExpectedText) => By.XPath("//*[@id='CreatedOnLabel'][text()='" + ExpectedText + "']");
        By ModifiedByFieldValue(string ExpectedText) => By.XPath("//*[@id='ModifiedByLabel'][text()='" + ExpectedText + "']");
        By ModifiedOnFieldValue(string ExpectedText) => By.XPath("//*[@id='ModifiedOnLabel'][text()='" + ExpectedText + "']");
        By IdentifierFieldValue(string ExpectedText) => By.XPath("//*[@id='IdLabel'][text()='" + ExpectedText + "']");
        By OwnerFieldValue(string ExpectedText) => By.XPath("//*[@id='OwnerLabel'][text()='" + ExpectedText + "']");

        readonly By ExcludeFromPrintCheckbox = By.XPath("//*[@id='ExcludeFromPrintCheck']");

        #endregion

        #region Buttons

        readonly By SaveButton = By.XPath("//*[@id='btnSave'][@value='Save']");
        readonly By CloseButon = By.XPath("//*[@id='CWCloseButton'][@value='Close']");

        #endregion




        public QuestionInformationDialoguePopup WaitForQuestionInformationDialoguePopupToLoad(string ExpectedPopupTitle)
        {
            WaitForElement(iframe_CWInformationDialogWindow);
            SwitchToIframe(iframe_CWInformationDialogWindow);

            WaitForElement(popupTitle);
            ValidateElementText(popupTitle, ExpectedPopupTitle);

            WaitForElement(CreatedByLabel);
            WaitForElement(CreatedOnLabel);
            WaitForElement(ModifiedByLabel);
            WaitForElement(ModifiedOnLabel);
            WaitForElement(IdLabel);
            WaitForElement(OwnerLabel);
            WaitForElement(ExcludeFromPrintLabel);

            WaitForElement(ExcludeFromPrintCheckbox);
            
            WaitForElement(SaveButton);
            WaitForElement(CloseButon);

            return this;
        }




        public QuestionInformationDialoguePopup TapExcludeFromPrintCheckbox()
        {
            Click(ExcludeFromPrintCheckbox);

            return this;
        }

        public QuestionInformationDialoguePopup TapSaveButton()
        {
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public QuestionInformationDialoguePopup TapCloseButton()
        {
            Click(CloseButon);

            return this;
        }


        public QuestionInformationDialoguePopup ValidateExcludeFromPrintCheckboxNotChecked()
        {
            ValidateElementNotChecked(ExcludeFromPrintCheckbox);

            return this;
        }

        public QuestionInformationDialoguePopup ValidateExcludeFromPrintCheckboxChecked()
        {
            ValidateElementChecked(ExcludeFromPrintCheckbox);

            return this;
        }

        public QuestionInformationDialoguePopup ValidateCreatedByInformation(string ExpectedText)
        {
            WaitForElement(CreatedByFieldValue(ExpectedText));

            return this;
        }

        public QuestionInformationDialoguePopup ValidateCreatedOnInformation(string ExpectedText)
        {
            WaitForElement(CreatedOnFieldValue(ExpectedText));

            return this;
        }

        public QuestionInformationDialoguePopup ValidateModifiedByInformation(string ExpectedText)
        {
            WaitForElement(ModifiedByFieldValue(ExpectedText));

            return this;
        }

        public QuestionInformationDialoguePopup ValidateModifiedOnInformation(string ExpectedText)
        {
            WaitForElement(ModifiedOnFieldValue(ExpectedText));

            return this;
        }

        public QuestionInformationDialoguePopup ValidateIdentifierInformation(string ExpectedText)
        {
            WaitForElement(IdentifierFieldValue(ExpectedText));

            return this;
        }

        public QuestionInformationDialoguePopup ValidateOwnerInformation(string ExpectedText)
        {
            WaitForElement(OwnerFieldValue(ExpectedText));

            return this;
        }
    }
}
