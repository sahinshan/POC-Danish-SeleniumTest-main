using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SDEMapListPopup : CommonMethods
    {
        public SDEMapListPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWSDEMapsListDialog = By.Id("iframe_CWSDEMapsListDialog");
        
        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/h1");



        #region From This Document Area

        readonly By iframe_MapsFrom = By.Id("MapsFrom");

        readonly By fromThisDocument_Header = By.XPath("//*[@id='CWHeader']/h2[text()='Answers Mapped ']/strong[text()='from this Document']");

        By fromThisDocument_MappingCheckbox(string MappingId) => By.XPath("//*[@id='CHK_" + MappingId + "']");
        
        readonly By fromThisDocument_Pagination_FirstPageButton = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]");
        readonly By fromThisDocument_Pagination_PreviousPageButton = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]");
        readonly By fromThisDocument_Pagination_PageNumber = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        readonly By fromThisDocument_Pagination_NextPageButton = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]");

        readonly By fromThisDocument_PaginationINformation = By.XPath("//*[@id='CWPagingFooter']/span");

        #endregion


        #region to this Documents Area

        readonly By iframe_MapsTo = By.Id("MapsTo");

        readonly By toThisDocument_Header = By.XPath("//*[@id='CWHeader']/h2[text()='Answers Mapped ']/strong[text()='from Other Documents']");

        By toThisDocument_MappingCheckbox(string MappingId) => By.XPath("//*[@id='CHK_" + MappingId + "']");

        readonly By toThisDocument_Pagination_FirstPageButton = By.XPath("//*[@id='CWPagingFooter']/ul/li[1]");
        readonly By toThisDocument_Pagination_PreviousPageButton = By.XPath("//*[@id='CWPagingFooter']/ul/li[2]");
        readonly By toThisDocument_Pagination_PageNumber = By.XPath("//*[@id='CWPagingFooter']/ul/li[3]/a");
        readonly By toThisDocument_Pagination_NextPageButton = By.XPath("//*[@id='CWPagingFooter']/ul/li[4]");

        readonly By toThisDocument_PaginationINformation = By.XPath("//*[@id='CWPagingFooter']/span");

        #endregion








        public SDEMapListPopup WaitForSDEMapListPopupToLoad()
        {
            WaitForElement(iframe_CWSDEMapsListDialog);
            SwitchToIframe(iframe_CWSDEMapsListDialog);

            WaitForElement(popupHeader);

            return this;
        }



        public SDEMapListPopup SwitchToMapsFromIframe()
        {
            WaitForElement(iframe_MapsFrom);
            SwitchToIframe(iframe_MapsFrom);

            WaitForElement(fromThisDocument_Header);
            
            WaitForElement(fromThisDocument_Pagination_FirstPageButton);
            WaitForElement(fromThisDocument_Pagination_PreviousPageButton);
            WaitForElement(fromThisDocument_Pagination_PageNumber);
            WaitForElement(fromThisDocument_Pagination_NextPageButton);

            return this;
        }

        public SDEMapListPopup ValidateFromSDEMapRecordVisibility(string MappingId, bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementToBeClickable(fromThisDocument_MappingCheckbox(MappingId));
            else
                WaitForElementNotVisible(fromThisDocument_MappingCheckbox(MappingId), 7);

            return this;
        }

        public SDEMapListPopup ClickFromSDEMapNextPageButton()
        {
            Click(fromThisDocument_Pagination_NextPageButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SDEMapListPopup ValidateFromSDEMapCurrentPageNumber(string ExpectedText)
        {
            ValidateElementText(fromThisDocument_Pagination_PageNumber, ExpectedText);

            return this;
        }

        public SDEMapListPopup ValidateFromSDEMapPaginationInformation(string ExpectedText)
        {
            ValidateElementText(fromThisDocument_PaginationINformation, ExpectedText);

            return this;
        }





        public SDEMapListPopup SwitchToMapsToIframe()
        {
            WaitForElement(iframe_MapsTo);
            SwitchToIframe(iframe_MapsTo);

            WaitForElement(toThisDocument_Header);

            WaitForElement(toThisDocument_Pagination_FirstPageButton);
            WaitForElement(toThisDocument_Pagination_PreviousPageButton);
            WaitForElement(toThisDocument_Pagination_PageNumber);
            WaitForElement(toThisDocument_Pagination_NextPageButton);

            return this;
        }

        public SDEMapListPopup ValidateToSDEMapRecordVisibility(string MappingId, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementToBeClickable(toThisDocument_MappingCheckbox(MappingId));
            else
                WaitForElementNotVisible(toThisDocument_MappingCheckbox(MappingId), 7);

            return this;
        }

        public SDEMapListPopup ClickToSDEMapNextPageButton()
        {
            Click(toThisDocument_Pagination_NextPageButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public SDEMapListPopup ValidateToSDEMapCurrentPageNumber(string ExpectedText)
        {
            ValidateElementText(toThisDocument_Pagination_PageNumber, ExpectedText);

            return this;
        }

        public SDEMapListPopup ValidateToSDEMapPaginationInformation(string ExpectedText)
        {
            ValidateElementText(toThisDocument_PaginationINformation, ExpectedText);

            return this;
        }


    }
}
