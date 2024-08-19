using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DocumentsPage : CommonMethods
    {
        public DocumentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Documents']");
        
        readonly By quickSearchTextBox = By.Id("CWQuickSearch");
        readonly By quickSearchButton = By.Id("CWQuickSearchButton");
        By DocumentRow(string DocumentID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + DocumentID + "']/td[2]");

        public DocumentsPage WaitForDocumentsPageToLoad()
        {
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public DocumentsPage SearchDocumentRecord(string SearchQuery, string DocumentID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);
            WaitForElement(DocumentRow(DocumentID));

            return this;
        }

        public DocumentPage OpenDocumentRecord(string DocumentId)
        {
            WaitForElement(DocumentRow(DocumentId));
            Click(DocumentRow(DocumentId));

            return new DocumentPage(this.driver, this.Wait, this.appURL);
        }
    }
}
