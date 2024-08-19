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
    public class WorkflowActionPopup : CommonMethods
    {
        public WorkflowActionPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='CWActionHeader']/h1");


        #region Fields
        
        readonly By ActionFieldLabel = By.XPath("//*[@id='divAction_content']/label");
        readonly By CustomTitleFieldLabel = By.XPath("//*[@id='customActionNameContent']/div/label");

        readonly By ActionPicklist = By.XPath("//*[@id='cboRuleAction']");
        readonly By CustomTitleCheckbox = By.XPath("//*[@id='CBCustomTitle']");
        readonly By CustomTitleTextbox = By.XPath("//*[@id='CustomTitle']");

        readonly By SetPropertiesButton = By.XPath("//*[@id='cboPropertiesSelection']");

        readonly By saveButton = By.XPath("//*[@id='CWAction_btnSave']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='CWAction_btnSaveAndClose']");
        readonly By cancelButton = By.XPath("//*[@id='CWAction_btnClose']");

        #endregion



        public WorkflowActionPopup WaitForWorkflowActionPopupToLoad()
        {
            WaitForElement(popupHeader);

            WaitForElement(ActionFieldLabel);
            WaitForElement(CustomTitleFieldLabel);

            WaitForElement(ActionPicklist);
            WaitForElement(CustomTitleCheckbox);
            WaitForElement(CustomTitleTextbox);
            
            WaitForElement(SetPropertiesButton);

            //WaitForElement(saveButton);
            //WaitForElement(saveAndCloseButton);
            //WaitForElement(saveAndCloseButton);
            
            return this;
        }


        public WorkflowActionPopup ValidateActionSelectedValue(string ExpectedText)
        {
            ValidatePicklistSelectedText(ActionPicklist, ExpectedText);

            return this;
        }
        public WorkflowActionPopup ValidateCustomTitleText(string ExpectedText)
        {
            ValidateElementValueByJavascript("CustomTitle", ExpectedText);

            return this;
        }
        public WorkflowActionPopup ValidateCustomTitleCheckboxChecked(bool ExpectedChecked)
        {
            ValidateElementCheckedByJavascript("CBCustomTitle", ExpectedChecked);

            return this;
        }


        public WorkflowActionPopup ValidateActionPicklistDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(ActionPicklist);
            else
                ValidateElementNotDisabled(ActionPicklist);

            return this;
        }
        public WorkflowActionPopup ValidateCustomTitleCheckboxDisabled(bool ExpectDisabled)
        {
            if(ExpectDisabled)
                ValidateElementDisabled(CustomTitleCheckbox);
            else
                ValidateElementNotDisabled(CustomTitleCheckbox);

            return this;
        }
        public WorkflowActionPopup ValidateCustomTitleTextboxDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(CustomTitleTextbox);
            else
                ValidateElementNotDisabled(CustomTitleTextbox);

            return this;
        }



        public WorkflowActionPopup SelectAction(string TextToSelect)
        {
            WaitForElementToBeClickable(ActionPicklist);
            SelectPicklistElementByText(ActionPicklist, TextToSelect);

            return this;
        }

        public WorkflowActionPopup InsertCustomTitle(string TextToSelect)
        {
            WaitForElement(CustomTitleTextbox);
            WaitForElementVisible(CustomTitleTextbox);
            WaitForElementToBeClickable(CustomTitleTextbox);
            SendKeys(CustomTitleTextbox, TextToSelect);

            return this;
        }

        public WorkflowActionPopup ClickSetPropertiesButton()
        {
            WaitForElementToBeClickable(SetPropertiesButton);
            Click(SetPropertiesButton);

            return this;
        }
        public WorkflowActionPopup ClickCustomTitleCheckbox()
        {
            Click(CustomTitleCheckbox);

            return this;
        }
        public WorkflowActionPopup ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }
        public WorkflowActionPopup ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }
        public WorkflowActionPopup ClickCancelButton()
        {
            Click(cancelButton);

            return this;
        }

    }
}
