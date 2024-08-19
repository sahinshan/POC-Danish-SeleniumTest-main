using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class WebSiteSitemapRecordDesignerPage : CommonMethods
    {

        public WebSiteSitemapRecordDesignerPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=websitesitemap&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By backButton = By.XPath("//*/button[@title='Back']");



        #region Sitemap Container

        By nodeElement_Link(string NodePosition) => By.XPath("//*[@id='WebsiteSitemapContainer']/ul/li[" + NodePosition + "]/a");
        By childNodeElement_Link(string NodePosition, string ChildPosition) => By.XPath("//*[@id='WebsiteSitemapContainer']/ul/li[" + NodePosition + "]/ul/li[" + ChildPosition + "]/a");



        readonly By newNode_Button = By.XPath("//*[@id='WebsiteSitemapContainer']/ul/li/button");

        By newChildNode_Button(string NodePosition) => By.XPath("//*[@id='WebsiteSitemapContainer']/ul/li[" + NodePosition + "]/ul/li/button");


        #endregion

        #region Details Container

        readonly By DetailsContainerBackButton = By.XPath("//*[@id='DetailsContainer']/*/*/*/button[@title='Back']");
        readonly By DetailsContainerSaveButton = By.XPath("//div[@id='DetailsContainer']/*/*/*/*/*[@id='TI_SaveButton']");
        readonly By DetailsContainerDeleteButton = By.XPath("//div[@id='DetailsContainer']/*/*/*/*/*[@id='TI_DeleteRecordButton']");
        readonly By DetailsContainerHeader = By.XPath("//*[@id='DetailsContainer']/*/*/h1");

        readonly By type_FieldLabel = By.XPath("//*[@id='CWLabelHolder_typeid']/label");
        readonly By title_FieldLabel = By.XPath("//*[@id='CWLabelHolder_title']/label");
        readonly By page_FieldLabel = By.XPath("//*[@id='CWLabelHolder_pageid']/label");
        readonly By link_FieldLabel = By.XPath("//*[@id='CWLabelHolder_link']/label");
        readonly By displayOrder_FieldLabel = By.XPath("//*[@id='CWLabelHolder_displayorder']/label");

        readonly By name_Field = By.XPath("//*[@id='CWField_name']");
        readonly By type_Field = By.XPath("//*[@id='CWField_typeid']");
        readonly By type_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_typeid']/label/span");
        readonly By title_Field = By.XPath("//*[@id='CWField_title']");
        readonly By title_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_title']/label/span");
        readonly By page_Field = By.XPath("//*[@id='CWField_pageid']");
        readonly By page_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_pageid']/label/span");
        readonly By link_Field = By.XPath("//*[@id='CWField_link']");
        readonly By link_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_link']/label/span");
        readonly By displayOrder_Field = By.XPath("//*[@id='CWField_displayorder']");
        readonly By displayOrder_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_displayorder']/label/span");


        #endregion




        public WebSiteSitemapRecordDesignerPage WaitForWebSiteSitemapRecordDesignerPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(CWHTMLResourcePanel_IFrame);
            this.SwitchToIframe(CWHTMLResourcePanel_IFrame);

            this.WaitForElement(pageHeader);

            this.WaitForElement(backButton);


            return this;
        }

        public WebSiteSitemapRecordDesignerPage WaitForDetailsContainerToLoad()
        {
            this.WaitForElement(DetailsContainerBackButton);
            this.WaitForElement(DetailsContainerSaveButton);
            this.WaitForElement(DetailsContainerHeader);

            this.WaitForElement(type_FieldLabel);
            this.WaitForElement(title_FieldLabel);
            this.WaitForElement(title_FieldLabel);
            this.WaitForElement(displayOrder_FieldLabel);

            this.WaitForElement(type_Field);
            this.WaitForElement(title_Field);
            this.WaitForElement(title_Field);
            this.WaitForElement(displayOrder_Field);


            return this;
        }




        public WebSiteSitemapRecordDesignerPage ValidateNodeLinkText(string NodePosition, string ExpectedText)
        {
            WaitForElement(nodeElement_Link(NodePosition));
            ValidateElementText(nodeElement_Link(NodePosition), ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateChildNodeLinkText(string NodePosition, string ChildNodePosition, string ExpectedText)
        {
            WaitForElement(childNodeElement_Link(NodePosition, ChildNodePosition));
            ValidateElementText(childNodeElement_Link(NodePosition, ChildNodePosition), ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ClickNodeLink(string NodePosition)
        {
            WaitForElement(nodeElement_Link(NodePosition));
            Click(nodeElement_Link(NodePosition));

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ClickChildNodeLink(string NodePosition, string ChildNodePosition)
        {
            WaitForElement(childNodeElement_Link(NodePosition, ChildNodePosition));
            Click(childNodeElement_Link(NodePosition, ChildNodePosition));

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ClickNewNodeButton()
        {
            WaitForElement(newNode_Button);
            Click(newNode_Button);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ClickNewChildNodeButton(string NodePosition)
        {
            WaitForElement(newChildNode_Button(NodePosition));
            Click(newChildNode_Button(NodePosition));

            return this;
        }


        public WebSiteSitemapRecordDesignerPage ValidateNewNodeButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElement(newNode_Button);
            else
                WaitForElementNotVisible(newNode_Button, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateNewChildNodeButtonVisibility(bool ExpectVisible, string NodePosition)
        {
            if (ExpectVisible)
                WaitForElement(newChildNode_Button(NodePosition));
            else
                WaitForElementNotVisible(newChildNode_Button(NodePosition), 3);

            return this;

        }
        public WebSiteSitemapRecordDesignerPage ValidateNodeLinkVisibility(bool ExpectVisible, string NodePosition)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(nodeElement_Link(NodePosition));
                WaitForElementVisible(nodeElement_Link(NodePosition));
            }
            else
                WaitForElementNotVisible(nodeElement_Link(NodePosition), 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateChildNodeLinkVisibility(bool ExpectVisible, string NodePosition, string ChildNodePosition)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(childNodeElement_Link(NodePosition, ChildNodePosition));
                WaitForElementVisible(childNodeElement_Link(NodePosition, ChildNodePosition));
            }
            else
                WaitForElementNotVisible(childNodeElement_Link(NodePosition, ChildNodePosition), 3);

            return this;

        }



        public WebSiteSitemapRecordDesignerPage ValidateTypeSelectedValue(string ExpectedText)
        {
            ValidatePicklistSelectedText(type_Field, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateTitleText(string ExpectedText)
        {
            ValidateElementValue(title_Field, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidatePageSelectedValue(string ExpectedText)
        {
            ValidatePicklistSelectedText(page_Field, ExpectedText);

            return this;
        }

        public WebSiteSitemapRecordDesignerPage ValidatePageTextPresent(bool ExpectTextPresent, string ExpectedText)
        {
            if (ExpectTextPresent)
                ValidatePicklistContainsElementByText(page_Field, ExpectedText);
            else
                ValidatePicklistDoesNotContainsElementByText(page_Field, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateLinkText(string ExpectedText)
        {
            ValidateElementValue(link_Field, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateDisplayOrderText(string ExpectedText)
        {
            ValidateElementValue(displayOrder_Field, ExpectedText);

            return this;
        }



        public WebSiteSitemapRecordDesignerPage SelectType(string ValueToSelect)
        {
            SelectPicklistElementByText(type_Field, ValueToSelect);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage InsertName(string TextToInsert)
        {
            SendKeys(name_Field, TextToInsert);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage InsertTitle(string TextToInsert)
        {
            SendKeys(title_Field, TextToInsert);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage SelectPage(string ValueToSelect)
        {
            SelectPicklistElementByText(page_Field, ValueToSelect);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage InsertLink(string TextToInsert)
        {
            SendKeys(link_Field, TextToInsert);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage InsertDisplayOrderText(string TextToInsert)
        {
            SendKeys(displayOrder_Field, TextToInsert);

            return this;
        }



        public WebSiteSitemapRecordDesignerPage ValidateTypeFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(type_Field);
                WaitForElementVisible(type_Field);
            }
            else
                WaitForElementNotVisible(type_Field, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateTitleFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(title_Field);
                WaitForElementVisible(title_Field);
            }
            else
                WaitForElementNotVisible(title_Field, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidatePageFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(page_Field);
                WaitForElementVisible(page_Field);
            }
            else
                WaitForElementNotVisible(page_Field, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(link_Field);
                WaitForElementVisible(link_Field);
            }
            else
                WaitForElementNotVisible(link_Field, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateDisplayOrderFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                MoveToElementInPage(displayOrder_Field);
                WaitForElementVisible(displayOrder_Field);
            }
            else
                WaitForElementNotVisible(displayOrder_Field, 3);

            return this;
        }




        public WebSiteSitemapRecordDesignerPage ValidateTypeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(type_FieldErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateTitleErrorLabelText(string ExpectedText)
        {
            ValidateElementText(title_FieldErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidatePageErrorLabelText(string ExpectedText)
        {
            ValidateElementText(page_FieldErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateLinkErrorLabelText(string ExpectedText)
        {
            ValidateElementText(link_FieldErrorLabel, ExpectedText);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateDisplayOrderErrorLabelText(string ExpectedText)
        {
            ValidateElementText(displayOrder_FieldErrorLabel, ExpectedText);

            return this;
        }



        public WebSiteSitemapRecordDesignerPage ValidateTypeFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(type_FieldErrorLabel);
            else
                WaitForElementNotVisible(type_FieldErrorLabel, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateTitleFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(title_FieldErrorLabel);
            else
                WaitForElementNotVisible(title_FieldErrorLabel, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidatePageFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(page_FieldErrorLabel);
            else
                WaitForElementNotVisible(page_FieldErrorLabel, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateLinkFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(link_FieldErrorLabel);
            else
                WaitForElementNotVisible(link_FieldErrorLabel, 3);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ValidateDisplayOrderFieldErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(displayOrder_FieldErrorLabel);
            else
                WaitForElementNotVisible(displayOrder_FieldErrorLabel, 3);

            return this;
        }


        public WebSiteSitemapRecordDesignerPage ClickBackButton()
        {
            this.WaitForElement(backButton);
            this.Click(backButton);

            return this;
        }

        public WebSiteSitemapRecordDesignerPage ClickDetailsContainerAreaBackButton()
        {
            this.WaitForElement(DetailsContainerBackButton);
            this.Click(DetailsContainerBackButton);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ClickDetailsContainerSaveButton()
        {
            this.WaitForElement(DetailsContainerSaveButton);
            this.Click(DetailsContainerSaveButton);

            return this;
        }
        public WebSiteSitemapRecordDesignerPage ClickDetailsContainerDeleteButton()
        {
            this.WaitForElement(DetailsContainerDeleteButton);
            this.Click(DetailsContainerDeleteButton);

            return this;
        }
    }
}
