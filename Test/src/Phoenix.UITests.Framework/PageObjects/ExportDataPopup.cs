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
    public class ExportDataPopup : CommonMethods
    {
        public ExportDataPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_ExcelExport = By.XPath("//*[@id='iframe_ExcelExport']");


        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/header/h1");

        readonly By RecordsToExport_FieldLabel = By.XPath("//label[@for='CWExportType'][text()='Records to Export']");
        readonly By ExportFormat_FieldLabel = By.XPath("//label[@for='CWExportFormat'][text()='Export Format']");
        readonly By ExportInDataMapFormat_FieldLabel = By.XPath("//label[@for='CWDataMapFormat'][text()='Export in data map format?']");

        readonly By RecordsToExport_Field = By.XPath("//*[@id='CWExportType']");
        readonly By ExportFormat_Field = By.XPath("//*[@id='CWExportFormat']");
        readonly By ExportInDataMapFormat_Field = By.XPath("//*[@id='CWDataMapFormat']");

        readonly By ExportButton = By.Id("CWSaveButton");
        readonly By CloseButton = By.Id("CWCloseButton");

        


        public ExportDataPopup WaitForExportDataPopupToLoad()
        {
            WaitForElement(iframe_ExcelExport);
            SwitchToIframe(iframe_ExcelExport);

            WaitForElement(popupHeader);

            WaitForElement(RecordsToExport_FieldLabel);
            WaitForElement(ExportFormat_FieldLabel);
            WaitForElement(ExportInDataMapFormat_FieldLabel);

            WaitForElement(RecordsToExport_Field);
            WaitForElement(ExportFormat_Field);
            WaitForElement(ExportInDataMapFormat_Field);
            
            WaitForElement(ExportButton);
            WaitForElement(CloseButton);

            return this;
        }

        public ExportDataPopup SelectRecordsToExport(string TextToSelect)
        {
            SelectPicklistElementByText(RecordsToExport_Field, TextToSelect);

            return this;
        }

        public ExportDataPopup SelectExportFormat(string TextToSelect)
        {
            SelectPicklistElementByText(ExportFormat_Field, TextToSelect);

            return this;
        }

        public ExportDataPopup ClickExportButton()
        {
            ScrollToElement(ExportButton);
            WaitForElementToBeClickable(ExportButton);
            Click(ExportButton);

            return this;
        }

        public ExportDataPopup ClickExportInDataMapFormat()
        {
            Click(ExportInDataMapFormat_Field);

            return this;
        }

        public ExportDataPopup TapCloseButton()
        {
            Click(CloseButton);

            return this;
        }

        

    }
}
