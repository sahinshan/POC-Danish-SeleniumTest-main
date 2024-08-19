using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonChronologyRecordPrintPopup : CommonMethods
    {
        public PersonChronologyRecordPrintPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By popupHeader = By.XPath("//*[@id='CWPopupTitle'][text()='Print']");
        readonly By GroupBy_Picklist = By.Id("CWGroupBy");
        readonly By PrintButton = By.Id("btnPrint");


        public PersonChronologyRecordPrintPopup WaitForPersonChronologyRecordPrintPopupToLoad()
        {
            WaitForElement(popupHeader);
            WaitForElement(GroupBy_Picklist);
            WaitForElement(PrintButton);

            return this;
        }

        public PersonChronologyRecordPrintPopup SelectGroupByByText(string TextToSelect)
        {
            SelectPicklistElementByText(GroupBy_Picklist, TextToSelect);

            return this;
        }

        public PersonChronologyRecordPrintPopup TapPrintButton()
        {
            Click(PrintButton);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            return this;
        }



    }
}
