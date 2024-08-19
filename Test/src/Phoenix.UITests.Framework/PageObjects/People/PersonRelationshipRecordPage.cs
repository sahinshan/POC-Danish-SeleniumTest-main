using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRelationshipRecordPage : CommonMethods
    {
        public PersonRelationshipRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personrelationship')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By familyMember_Field = By.Id("CWField_familymemberid");
        readonly By personRelationshipType = By.Id("CWField_personrelationshiptypeid_Link");
        readonly By relatedPersonRelationshipType = By.Id("CWField_relatedpersonrelationshiptypeid_Link");
        readonly By isBirthParent_Field = By.Id("CWField_isbirthparentid");
        readonly By endDate_Field = By.Id("CWField_enddate");


        public PersonRelationshipRecordPage WaitForPersonRelationshipRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            return this;
        }

        public PersonRelationshipRecordPage SelectFamilyMembers(String OptionToSelect)
        {
            WaitForElementVisible(familyMember_Field);
            SelectPicklistElementByValue(familyMember_Field, OptionToSelect);

            return this;
        }


        public PersonRelationshipRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }


        public PersonRelationshipRecordPage ValidatePersonRelationshipType(String ExpectedText)
        {
            WaitForElementVisible(personRelationshipType);
            MoveToElementInPage(personRelationshipType);
            ValidateElementText(personRelationshipType, ExpectedText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelatedPersonRelationshipType(String ExpectedText)
        {
            WaitForElementVisible(relatedPersonRelationshipType);
            MoveToElementInPage(relatedPersonRelationshipType);
            ValidateElementText(relatedPersonRelationshipType, ExpectedText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateIsBirthParent(String ExpectedText)
        {
            WaitForElementVisible(isBirthParent_Field);
            MoveToElementInPage(isBirthParent_Field);
            ValidatePicklistSelectedText(isBirthParent_Field, ExpectedText);
           

            return this;
        }
        public PersonRelationshipRecordPage ValidateRelatedPersonRelationshipEndDate(String ExpectedText)
        {
            ScrollToElement(endDate_Field);
            WaitForElementVisible(endDate_Field);
            ValidateElementValue(endDate_Field, ExpectedText);

            return this;
        }
    }
}


       