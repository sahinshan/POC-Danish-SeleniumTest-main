using Microsoft.Office.Interop.Word;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class UserChartPopup : CommonMethods
    {
        public UserChartPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWChartConfig = By.Id("iframe_CWChartConfig");

        readonly By popupHeader = By.Id("CWHeaderText");

        readonly By saveButton = By.Id("CWSaveButton");
        readonly By closeButton = By.Id("CWCloseButton");

        #region Field Titles

        readonly By Name_FieldLabel = By.XPath("//label[@for='CWName']");
        readonly By Type_FieldLabel = By.XPath("//label[@for='CWChartType']");
        readonly By RecordType_FieldLabel = By.XPath("//label[@for='CWRecordType']");
        readonly By DataView_FieldLabel = By.XPath("//label[@for='CWDataView']");
        readonly By SeriesColor_FieldLabel = By.XPath("//label[@for='CWSeriesColor']");
        readonly By SeriesColors_FieldLabel = By.XPath("//label[@for='CWSeriesColors']");
        readonly By Series_FieldLabel = By.XPath("//h5[text()='Series']");
        readonly By Categories_FieldLabel = By.XPath("//h5[text()='Categories']");
        readonly By Legend_FieldLabel = By.XPath("//h5[text()='Legend']");
        readonly By Admin_FieldLabel = By.XPath("//h5[text()='Admin']");


        #endregion

        #region Fields

        readonly By Name_Field = By.Id("CWName");
        readonly By Type_Field = By.Id("CWChartType");
        readonly By RecordType_Field = By.Id("CWRecordType");
        readonly By DataView_Field = By.Id("CWDataView");
        readonly By SeriesColor_Field = By.Id("CWSeriesColor");
        readonly By SeriesColors_Field = By.Id("CWSeriesColors");
        
        By Series_Field(int SeriesID) => By.Id("CWSeries_" + SeriesID);
        By SeriesResultType_Field(int SeriesID) => By.Id("CWSeries_" + SeriesID + "_ResultType");
        By SeriesLabel_Field(int SeriesID) => By.Id("CWSeries_" + SeriesID + "_Label");
        By Series_DeleteButton(int SeriesID) => By.XPath("//*[@id='CWSeriesPanel_" + SeriesID + "']/*/*/button[text()='Delete']");

        readonly By AddSeries_Button = By.Id("CWAddSeries");


        By Category_Field(int CategoryID) => By.Id("CWCategory_" + CategoryID);
        By CategoryGroupType_Field(int CategoryID) => By.Id("CWCategory_" + CategoryID + "_GroupType");
        By Category_DeleteButton(int CategoryID) => By.XPath("//*[@id='CWCategoryPanel_" + CategoryID + "']/*/*/button");

        readonly By AddCategory_Button = By.Id("CWAddCategory");

        readonly By ShowLegend_CheckBox = By.Id("CWShowLegend");
        
        readonly By LegendPosition_Field = By.Id("CWLegendPosition");

        readonly By ValidForExport_CheckBox = By.Id("CWValidForExport");
        readonly By Inactive_CheckBox = By.Id("CWInactive");


        #endregion

        #region Chart Area

        readonly By chartArea = By.Id("CWChart");

        readonly By chartBar = By.XPath("//*[@class='apexcharts-bar-area']");

        By chartBarByValue(int Val) => By.XPath("//*[@class='apexcharts-bar-area'][@val='" + Val + "']");

        By chartBarByID(string id) => By.XPath("//*[@id='" + id + "']");

        By chartBarByIDAndValue(string id, int Val) => By.XPath("//*[@id='" + id + "'][@val='" + Val + "']");

        By chartBarByJAndIndexAndValue(int j, int index, int val) => By.XPath("//*[@j='"+ j + "'][@index='"+ index + "'][@val='"+ val + "']");

        By donutBarByIDAndAngle(string id, string angle) => By.XPath("//*[@id='" + id + "'][@*[name()='data:angle'] = '" + angle + "']");

        By donutBarByJAndIndexAndAngle(int j, int index, string angle) => By.XPath("//*[@j='" + j + "'][@index='" + index + "'][@*[name()='data:angle'] = '" + angle + "']");


        #endregion



        public UserChartPopup WaitForUserChartPopupToLoad()
        {
            System.Threading.Thread.Sleep(3000);

            WaitForElement(iframe_CWChartConfig);
            SwitchToIframe(iframe_CWChartConfig);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(popupHeader);

            WaitForElement(Name_FieldLabel);
            WaitForElement(Type_FieldLabel);
            WaitForElement(RecordType_FieldLabel);
            WaitForElement(DataView_FieldLabel);
            WaitForElement(SeriesColor_FieldLabel);
            WaitForElement(SeriesColors_FieldLabel);
            WaitForElement(Series_FieldLabel);
            WaitForElement(Categories_FieldLabel);
            WaitForElement(Legend_FieldLabel);
            WaitForElement(Admin_FieldLabel);
            
            WaitForElement(chartArea);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public UserChartPopup InsertName(string TextToInsert)
        {
            SendKeys(Name_Field, TextToInsert);

            return this;
        }
        public UserChartPopup SelectType(string TextToSelect)
        {
            SelectPicklistElementByText(Type_Field, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public UserChartPopup SelectRecordType(string TextToSelect)
        {
            SelectPicklistElementByText(RecordType_Field, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }
        public UserChartPopup SelectDataView(string TextToSelect)
        {
            SelectPicklistElementByText(DataView_Field, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public UserChartPopup SelectSeries(int SeriesPosition, string TextToSelect)
        {
            SelectPicklistElementByText(Series_Field(SeriesPosition), TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public UserChartPopup SelectSeriesResultType(int SeriesPosition, string TextToSelect)
        {
            SelectPicklistElementByText(SeriesResultType_Field(SeriesPosition), TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public UserChartPopup SelectCategory(int CategoryPosition, string TextToSelect)
        {
            SelectPicklistElementByText(Category_Field(CategoryPosition), TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }
        public UserChartPopup SelectCategoryGroupType(int GroupPosition, string TextToSelect)
        {
            SelectPicklistElementByText(CategoryGroupType_Field(GroupPosition), TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }


        public UserChartPopup TapAddSeries()
        {
            Click(AddSeries_Button);

            return this;
        }
        public UserChartPopup TapDeleteSeries(int SeriesPosition)
        {
            Click(Series_DeleteButton(SeriesPosition));

            return this;
        }
        public UserChartPopup TapDeleteCategory(int CategoryPosition)
        {
            Click(Category_DeleteButton(CategoryPosition));

            return this;
        }
        public UserChartPopup TapAddCategory()
        {
            Click(AddCategory_Button);

            return this;
        }
        public UserChartPopup TapShowLegendCheckBox()
        {
            Click(ShowLegend_CheckBox);

            return this;
        }
        public UserChartPopup TapValidForExportCheckBox()
        {
            Click(ValidForExport_CheckBox);

            return this;
        }
        public UserChartPopup TapInactiveCheckBox()
        {
            Click(Inactive_CheckBox);

            return this;
        }
        public UserChartPopup TapSavebutton()
        {
            Click(saveButton);

            return this;
        }
        public UserChartPopup TapClosebutton()
        {
            Click(closeButton);

            return this;
        }


        public UserChartPopup ValidatePopupHeaderTitle(string ExpectedText)
        {
            ValidateElementText(popupHeader, ExpectedText);

            return this;
        }
        public UserChartPopup ValidateName(string ExpectedText)
        {
            ValidateElementValueByJavascript("CWName", ExpectedText);
            //ValidateElementText(Name_Field, ExpectedText);

            return this;
        }
        public UserChartPopup ValidateType(string ExpectedText)
        {
            ValidatePicklistSelectedText(Type_Field, ExpectedText);

            return this;
        }
        public UserChartPopup ValidateRecordType(string ExpectedText)
        {
            ValidatePicklistSelectedText(RecordType_Field, ExpectedText);

            return this;
        }
        public UserChartPopup ValidateDataView(string ExpectedText)
        {
            ValidatePicklistSelectedText(DataView_Field, ExpectedText);

            return this;
        }
        public UserChartPopup ValidateSeries(int SeriesPosition, string ExpectedText)
        {
            ValidatePicklistSelectedText(Series_Field(SeriesPosition), ExpectedText);

            return this;
        }
        public UserChartPopup ValidateSeriesResultType(int SeriesPosition, string ExpectedText)
        {
            ValidatePicklistSelectedText(SeriesResultType_Field(SeriesPosition), ExpectedText);

            return this;
        }
        public UserChartPopup ValidateSeriesLabel(int SeriesPosition, string ExpectedText)
        {
            ValidateElementValueByJavascript("CWSeries_" + SeriesPosition + "_Label", ExpectedText);
            //ValidateElementText(SeriesLabel_Field(SeriesPosition), ExpectedText);

            return this;
        }
        public UserChartPopup ValidateCategory(int CategoryPosition, string ExpectedText)
        {
            ValidatePicklistSelectedText(Category_Field(CategoryPosition), ExpectedText);

            return this;
        }
        public UserChartPopup ValidateCategoryGroupType(int CategoryGroupTypePosition, string ExpectedText)
        {
            ValidatePicklistSelectedText(CategoryGroupType_Field(CategoryGroupTypePosition), ExpectedText);

            return this;
        }


        public UserChartPopup ValidateChartBarVisible(int BarValue)
        {
            WaitForElementVisible(chartBarByValue(BarValue));

            return this;
        }
        public UserChartPopup ValidateChartBarVisible(string id)
        {
            WaitForElementVisible(chartBarByID(id));

            return this;
        }
        public UserChartPopup ValidateChartBarVisible(string id, int Val)
        {
            WaitForElementVisible(chartBarByIDAndValue(id, Val));

            return this;
        }

        public UserChartPopup ValidateChartBarVisible(int j, int index, int val)
        {
            WaitForElementVisible(chartBarByJAndIndexAndValue(j, index, val));

            return this;
        }

        public UserChartPopup ValidateDonutBarVisible(string id, string angle)
        {
            WaitForElementVisible(donutBarByIDAndAngle(id, angle));

            return this;
        }

        public UserChartPopup ValidateDonutBarVisible(int j, int index, string angle)
        {
            WaitForElementVisible(donutBarByJAndIndexAndAngle(j, index, angle));

            return this;
        }
    }
}
