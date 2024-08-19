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
    public class WebsitePageWidgetSettingsPopup : CommonMethods
    {
        public WebsitePageWidgetSettingsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        

        readonly By popupHeader = By.XPath("//*[@id='widgetSettings']/div/div/div/h5[text()='Widget Settings']");



        readonly By WidgetType_Label = By.XPath("//*[@id='widgetSettings']/div/div/div/div/div/label[text()='Widget Type']");
        readonly By RecordType_Label = By.XPath("//*[@id='widgetSettings']/div/div/div/div/div/label[text()='Record Type']");
        readonly By Form_Label = By.XPath("//*[@id='widgetSettings']/div/div/div/div/div/label[text()='Form']");
        readonly By View_Label = By.XPath("//*[@id='CWDataView']/div/label[text()='View']");
        readonly By Widget_Label = By.XPath("//*[@id='CWWebsiteWidget']/div/label[text()='Widget']");
        readonly By WidgetTitle_Label = By.XPath("//*[@id='widgetSettings']/div/div/div/div/div/label[text()='Widget Title']");
        readonly By HTMLFile_Label = By.XPath("//*[@id='CWHtmlFile']/div/label[text()='HTML File']");
        readonly By StylesheetFile_Label = By.XPath("//*[@id='CWStylesheet']/div/label[text()='Stylesheet File']");
        readonly By ScriptFile_Label = By.XPath("//*[@id='CWScript']/div/label[text()='Script File']");

        readonly By WidgetType_Picklist = By.XPath("//*[@id='widgetType']");
        readonly By RecordType_Picklist = By.XPath("//*[@id='widgetBusinessObjectId']");
        readonly By Form_Picklist = By.XPath("//*[@id='widgetDataFormId']");
        readonly By FormSaveAction_Picklist = By.XPath("//*[@id='widgetDataFormSaveActionId']");
        readonly By LocalizedString_Picklist = By.XPath("//*[@id='widgetLocalizedStringId']");
        readonly By View_Picklist = By.XPath("//*[@id='widgetDataViewId']");
        readonly By Widget_Picklist = By.XPath("//*[@id='WebsiteWidgetId']");
        readonly By WidgetTitle_Field = By.XPath("//*[@id='widgetTitle']");
        readonly By HTMLFile_Picklist = By.XPath("//*[@id='HtmlFileId']");
        readonly By StylesheetFile_Picklist = By.XPath("//*[@id='StylesheetFileId']");
        readonly By ScriptFile_Picklist = By.XPath("//*[@id='ScriptFileId']");
        readonly By Id_Field = By.XPath("//*[@id='widgetId']");

        readonly By SaveSettingsButton = By.Id("btnSaveWidgetSettings");
        readonly By CancelButton = By.XPath("//*[@id='widgetSettings']//button[text()='Cancel']");

        


        public WebsitePageWidgetSettingsPopup WaitForWebsitePageWidgetSettingsPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(WidgetType_Label);
            WaitForElement(SaveSettingsButton);
            WaitForElement(CancelButton);

            return this;
        }


        public WebsitePageWidgetSettingsPopup SelectWidgetType(string ElementToSelect)
        {
            WaitForElementToBeClickable(WidgetType_Picklist);
            SelectPicklistElementByText(WidgetType_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectRecordType(string ElementToSelect)
        {
            WaitForElementToBeClickable(RecordType_Picklist);
            SelectPicklistElementByText(RecordType_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectForm(string ElementToSelect)
        {
            WaitForElementToBeClickable(Form_Picklist);
            SelectPicklistElementByText(Form_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectFormSaveAction(string ElementToSelect)
        {
            WaitForElementToBeClickable(FormSaveAction_Picklist);
            SelectPicklistElementByText(FormSaveAction_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectLocalizedString(string ElementToSelect)
        {
            WaitForElementToBeClickable(LocalizedString_Picklist);
            SelectPicklistElementByText(LocalizedString_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectView(string ElementToSelect)
        {
            WaitForElementToBeClickable(View_Picklist);
            SelectPicklistElementByText(View_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectWidget(string ElementToSelect)
        {
            WaitForElementToBeClickable(Widget_Picklist);
            SelectPicklistElementByText(Widget_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup InsertWidgetTitle(string TextToInsert)
        {
            WaitForElementToBeClickable(WidgetTitle_Field);
            SelectPicklistElementByText(WidgetTitle_Field, TextToInsert);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectHTMLFile(string ElementToSelect)
        {
            WaitForElementToBeClickable(HTMLFile_Picklist);
            SelectPicklistElementByText(HTMLFile_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectStylesheetFile(string ElementToSelect)
        {
            WaitForElementToBeClickable(StylesheetFile_Picklist);
            SelectPicklistElementByText(StylesheetFile_Picklist, ElementToSelect);

            return this;
        }
        public WebsitePageWidgetSettingsPopup SelectScriptFile(string ElementToSelect)
        {
            WaitForElementToBeClickable(ScriptFile_Picklist);
            SelectPicklistElementByText(ScriptFile_Picklist, ElementToSelect);

            return this;
        }


        public WebsitePageWidgetSettingsPopup InsertId(string TextToInsert)
        {
            WaitForElementToBeClickable(Id_Field);
            SendKeys(Id_Field, TextToInsert);

            return this;
        }



        public WebsitePageWidgetSettingsPopup ValidateWidgetTypeSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(WidgetType_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateRecordTypeSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(RecordType_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateFormSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(Form_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateViewSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(View_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateWidgetSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(Widget_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateWidgetTitleFieldText(string ExpectedText)
        {
            ValidateElementText(WidgetTitle_Field, ExpectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateHTMLFileSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(HTMLFile_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateStylesheetFileSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(StylesheetFile_Picklist, ExpectedSelectedText);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateScriptFileSelectedValue(string ExpectedSelectedText)
        {
            ValidatePicklistSelectedText(ScriptFile_Picklist, ExpectedSelectedText);

            return this;
        }




        public WebsitePageWidgetSettingsPopup ValidateWidgetTypeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WidgetType_Picklist);
            else
                WaitForElementNotVisible(WidgetType_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateRecordTypeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(RecordType_Picklist);
            else
                WaitForElementNotVisible(RecordType_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateFormFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Form_Picklist);
            else
                WaitForElementNotVisible(Form_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateWidgetTitleFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WidgetTitle_Field);
            else
                WaitForElementNotVisible(WidgetTitle_Field, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateViewFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(View_Picklist);
            else
                WaitForElementNotVisible(View_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateWidgetFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Widget_Picklist);
            else
                WaitForElementNotVisible(Widget_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateHTMLFileFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(HTMLFile_Picklist);
            else
                WaitForElementNotVisible(HTMLFile_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateStylesheetFileFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(StylesheetFile_Picklist);
            else
                WaitForElementNotVisible(StylesheetFile_Picklist, 3);

            return this;
        }
        public WebsitePageWidgetSettingsPopup ValidateScriptFileFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ScriptFile_Picklist);
            else
                WaitForElementNotVisible(ScriptFile_Picklist, 3);

            return this;
        }


        public WebsitePageWidgetSettingsPopup TapCancelButton()
        {
            Click(CancelButton);

            return this;
        }
        public WebsitePageWidgetSettingsPopup TapSaveSettingsButton()
        {
            Click(SaveSettingsButton);

            return this;
        }



    }
}
