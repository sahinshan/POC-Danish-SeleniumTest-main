using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CarePlanTypesPage : CommonMethods
    {
        public CarePlanTypesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_careplantype = By.Id("iframe_careplantype");

        readonly By CarePlanTypesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        
        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");

        readonly By openRecord = By.XPath("//table[@id='CWGrid']/tbody/tr[1]/td[2]");

        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

       



        public CarePlanTypesPage WaitForCarePlanTypesPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_careplantype);
            SwitchToIframe(iframe_careplantype);

            WaitForElement(CarePlanTypesPageHeader);

            WaitForElement(selectAll_Checkbox);

            return this;
        }

        public CarePlanTypesPage ClickNewRecordButton()
        {
            WaitForElement(newRecordButton);
            
            Click(newRecordButton);

            return this;
        }


        public CarePlanTypesPage SearchCarePlanTypesRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);
            WaitForElement(openRecord);
            WaitForElementToBeClickable(openRecord);
            Click(openRecord);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public CarePlanTypesPage OpenCarePlanTypesRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
      
        public CarePlanTypesPage SelectCarePlanTypesRecord()
        {
            WaitForElement(selectAll_Checkbox);
            Click(selectAll_Checkbox);

            return this;
        }


        public CarePlanTypesPage ClickDeleteRecordButton()
        {
            WaitForElement(deleteRecordButton);

            Click(deleteRecordButton);

            return this;
        }





    }
}
