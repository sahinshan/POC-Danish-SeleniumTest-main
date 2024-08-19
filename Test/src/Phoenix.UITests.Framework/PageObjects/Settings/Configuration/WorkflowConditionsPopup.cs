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
    public class WorkflowConditionsPopup : CommonMethods
    {
        public WorkflowConditionsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='CWGroupConditionHeader']/h1");
        readonly By recordTypeName = By.XPath("//*/div[1]/span[@class='conditionBuilderTitle']");


        #region Fields

        readonly By AddRuleButton = By.XPath("//*[text()='         Add Rule       ']");
        
        By filterPicklist(string PicklistPosition) => By.XPath("//*[contains(@name,'rule_" + PicklistPosition + "_filter')]");
        By operatorPicklist(string PicklistPosition) => By.XPath("//*[contains(@name,'rule_" + PicklistPosition + "_operator')]");
        By targetTypePicklist(string PicklistPosition) => By.XPath("//*[contains(@id,'CWTargetType')][contains(@id,'rule_" + PicklistPosition + "_value')]");
        By targetFieldPicklist(string PicklistPosition) => By.XPath("//*[contains(@id,'CWTargetField')][contains(@id,'rule_" + PicklistPosition + "_value')]");
        By targetFieldInput(string InputPosition) => By.XPath("//*[@type='text'][contains(@id,'rule_" + InputPosition + "_value')]");


        readonly By RelatedBusinessObject_Picklist = By.XPath("//*[@class='card']/*/*/*/select[@class='relatedBOSelector form-control']");
        readonly By RelatedBusinessObject_AddRuleButton = By.XPath("//*[@title='Add Related Business Object']");


        By RelatedBusinessObject_ConditionTitle(int ConditionPosition) => By.XPath("//*[@class='relatedBOContainer']/div[" + ConditionPosition + "]/div/span");



        readonly By saveButton = By.XPath("//*[@id='CWGroupCondition_btnSave']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='CWGroupCondition_btnSaveAndClose']");
        readonly By cancelButton = By.XPath("//*[@id='CWGroupCondition_btnClose']");

        #endregion



        public WorkflowConditionsPopup WaitForWorkflowConditionsPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(recordTypeName);

            WaitForElement(AddRuleButton);

            WaitForElement(RelatedBusinessObject_AddRuleButton);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(saveAndCloseButton);
            
            return this;
        }


        public WorkflowConditionsPopup SelectFilter(string PicklistPosition, string TextToSelect)
        {
            WaitForElementToBeClickable(filterPicklist(PicklistPosition));
            SelectPicklistElementByText(filterPicklist(PicklistPosition), TextToSelect);

            return this;
        }
        public WorkflowConditionsPopup SelectOperator(string PicklistPosition, string TextToSelect)
        {
            WaitForElementToBeClickable(operatorPicklist(PicklistPosition));
            SelectPicklistElementByText(operatorPicklist(PicklistPosition), TextToSelect);

            return this;
        }
        public WorkflowConditionsPopup SelectTargetType(string PicklistPosition, string TextToSelect)
        {
            WaitForElementToBeClickable(targetTypePicklist(PicklistPosition));
            SelectPicklistElementByText(targetTypePicklist(PicklistPosition), TextToSelect);

            return this;
        }
        public WorkflowConditionsPopup SelectTargetField(string PicklistPosition, string TextToSelect)
        {
            WaitForElementToBeClickable(targetFieldPicklist(PicklistPosition));
            SelectPicklistElementByText(targetFieldPicklist(PicklistPosition), TextToSelect);

            return this;
        }
        public WorkflowConditionsPopup InsertTargetInput(string InputPosition, string TextToInsert)
        {
            WaitForElementToBeClickable(targetFieldInput(InputPosition));
            SendKeys(targetFieldInput(InputPosition), TextToInsert);

            return this;
        }



        public WorkflowConditionsPopup ValidateRelatedBusinessObjectsContainsElement(string ElementTextToFind)
        {
            ValidatePicklistContainsElementByText(RelatedBusinessObject_Picklist, ElementTextToFind);

            return this;
        }
        public WorkflowConditionsPopup ValidateRelatedBusinessObject_ConditionTitleText(int ConditionPosition, string ExpectedText)
        {
            WaitForElementToBeClickable(RelatedBusinessObject_ConditionTitle(ConditionPosition));
            ValidateElementText(RelatedBusinessObject_ConditionTitle(ConditionPosition), ExpectedText);

            return this;
        }


        public WorkflowConditionsPopup ClickSaveButton()
        {
            Click(saveButton);

            return this;
        }
        public WorkflowConditionsPopup ClickSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            return this;
        }
        public WorkflowConditionsPopup ClickCancelButton()
        {
            Click(cancelButton);

            return this;
        }

    }
}
