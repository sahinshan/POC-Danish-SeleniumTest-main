using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WorkflowSendEmailPropertiesPage : CommonMethods
    {

        public WorkflowSendEmailPropertiesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=workflow&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By iframe_WorkflowActionProperties = By.Id("iframe_WorkflowActionProperties");

        

        readonly By pageHeader = By.XPath("//*[@id='CWHeader']");

        readonly By backButton = By.XPath("//*[@id='WFPToolbar']/div/div/button");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        

        readonly By BusinessObjectPicklist_CustomFieldsTool = By.Id("BOSelect");
        readonly By BusinessObjectFieldsPicklist_CustomFieldsTool = By.Id("BOFieldSelect");
        readonly By SelectButton_CustomFieldsTool = By.Id("AddButton");
        readonly By FieldsSelectPicklist_CustomFieldsTool = By.Id("FieldsSelect");
        readonly By AddButton_CustomFieldsTool = By.Id("SetButton");



        readonly By RegardingField_LocalValueSet = By.XPath("//*[@id='CWWFHolder_regardingid']/li");
        readonly By ResponsibleUserField_LocalValueSet = By.XPath("//*[@id='CWWFHolder_responsibleuserid']/li");
        readonly By DueDateField_LocalValueSet = By.XPath("//*[@id='CWWFHolder_duedate']/li");
        readonly By Category_LocalValueSet = By.XPath("//*[@id='CWWFHolder_activitycategoryid']/li");


        readonly By CategoryFieldFormControlArea = By.XPath("//a[@id='CWField_activitycategoryid_Link']");

        



        public WorkflowSendEmailPropertiesPage WaitForWorkflowSendEmailPropertiesPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(CWHTMLResourcePanel_IFrame);
            this.SwitchToIframe(CWHTMLResourcePanel_IFrame);

            this.WaitForElement(iframe_WorkflowActionProperties);
            this.SwitchToIframe(iframe_WorkflowActionProperties);

            this.WaitForElement(pageHeader);

            this.WaitForElement(saveAndCloseButton);

            return this;
        }

        public WorkflowSendEmailPropertiesPage ValidateRegardingFieldLocalValueText(string ExpectedText)
        {
            ValidateElementText(RegardingField_LocalValueSet, ExpectedText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage ValidateResponsibleUserFieldLocalValueText(string ExpectedText)
        {
            ValidateElementText(ResponsibleUserField_LocalValueSet, ExpectedText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage ValidateDueDateFieldLocalValueText(string ExpectedText)
        {
            ValidateElementText(DueDateField_LocalValueSet, ExpectedText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage ValidateCategoryFieldLocalValueText(string ExpectedText)
        {
            ValidateElementText(Category_LocalValueSet, ExpectedText);

            return this;
        }



        public WorkflowSendEmailPropertiesPage ValidateBusinessObjectPicklistContainsElement(string ExpectedElementText)
        {
            WaitForElement(BusinessObjectPicklist_CustomFieldsTool);
            ValidatePicklistContainsElementByText(BusinessObjectPicklist_CustomFieldsTool, ExpectedElementText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage SelectBusinessObjectPicklistElement(string ElementText)
        {
            WaitForElementToBeClickable(BusinessObjectPicklist_CustomFieldsTool);
            SelectPicklistElementByText(BusinessObjectPicklist_CustomFieldsTool, ElementText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage SelectBusinessObjectFieldsPicklistElement(string ElementText)
        {
            WaitForElementToBeClickable(BusinessObjectFieldsPicklist_CustomFieldsTool);
            SelectPicklistElementByText(BusinessObjectFieldsPicklist_CustomFieldsTool, ElementText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage ClickSelectButton()
        {
            WaitForElementToBeClickable(SelectButton_CustomFieldsTool);
            Click(SelectButton_CustomFieldsTool);

            return this;
        }
        public WorkflowSendEmailPropertiesPage ValidateFieldsSelectPicklistContainsElement(string ExpectedElementText)
        {
            WaitForElementToBeClickable(FieldsSelectPicklist_CustomFieldsTool);
            ValidatePicklistContainsElementByText(FieldsSelectPicklist_CustomFieldsTool, ExpectedElementText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage SelectFieldToAdd(string ElementText)
        {
            WaitForElementToBeClickable(FieldsSelectPicklist_CustomFieldsTool);
            SelectPicklistElementByText(FieldsSelectPicklist_CustomFieldsTool, ElementText);

            return this;
        }
        public WorkflowSendEmailPropertiesPage ClickAddButton()
        {
            WaitForElementToBeClickable(AddButton_CustomFieldsTool);
            Click(AddButton_CustomFieldsTool);

            return this;
        }



        public WorkflowSendEmailPropertiesPage ClickCategoryFieldFormControlArea()
        {
            WaitForElementToBeClickable(CategoryFieldFormControlArea);
            Click(CategoryFieldFormControlArea);

            return this;
        }

        public WorkflowSendEmailPropertiesPage ClickSaveAndCloseButton()
        {
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public WorkflowSendEmailPropertiesPage ClickBackButton()
        {
            Click(backButton);

            return this;
        }




    }
}
