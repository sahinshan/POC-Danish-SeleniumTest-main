using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonPostAdoptionLinkRecordPage : CommonMethods
    {
        public PersonPostAdoptionLinkRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }


        private IAlert alert;

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=adoptionlink')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By saveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By restrictAccessButton = By.Id("TI_RestrictAccessButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button");


        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");


        #region Fields

        readonly By PreAdoptionPerson_FieldHeader = By.Id("CWLabelHolder_preadoptionpersonid");
        readonly By PreAdoptionPerson_LinkField = By.Id("CWField_preadoptionpersonid_Link");
        readonly By PreAdoptionPerson_FieldLookUpButton = By.Id("CWLookupBtn_preadoptionpersonid");
        readonly By PreAdoptionPerson_FieldRemoveButton = By.Id("CWClearLookup_preadoptionpersonid");
        readonly By PreAdoptionPerson_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_preadoptionpersonid']/label/span");

        readonly By PostAdoptionPerson_FieldHeader = By.Id("CWLabelHolder_personid");
        readonly By PostAdoptionPerson_LinkField = By.Id("CWField_personid_Link");
        readonly By PostAdoptionPerson_FieldLookUpButton = By.Id("CWLookupBtn_personid");
        readonly By PostAdoptionPerson_FieldRemoveButton = By.Id("CWClearLookup_personid");
        readonly By PostAdoptionPerson_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_personid']/label/span");

        readonly By ResponsibleTeam_FieldHeader = By.Id("CWLabelHolder_ownerid");
        readonly By ResponsibleTeam_LinkField = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_FieldLookUpButton = By.Id("CWLookupBtn_ownerid");
        readonly By ResponsibleTeam_FieldRemoveButton = By.Id("CWClearLookup_ownerid");
        readonly By ResponsibleTeam_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");

        readonly By ResponsibleUser_FieldHeader = By.Id("CWLabelHolder_responsibleuserid");
        readonly By ResponsibleUser_LinkField = By.Id("CWField_responsibleuserid_Link");
        readonly By ResponsibleUser_FieldLookUpButton = By.Id("CWLookupBtn_responsibleuserid");
        readonly By ResponsibleUser_FieldRemoveButton = By.Id("CWClearLookup_responsibleuserid");
        readonly By ResponsibleUser_FieldMessageArea = By.XPath("//*[@id='CWControlHolder_responsibleuserid']/label/span");

        #endregion



        public PersonPostAdoptionLinkRecordPage WaitForPersonPostAdoptionLinkRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(PreAdoptionPerson_FieldHeader);
            WaitForElement(PostAdoptionPerson_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);

            return this;
        }

        public PersonPostAdoptionLinkRecordPage WaitForPersonPostAdoptionLinkRecordPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDataFormDialog);
            SwitchToIframe(iframe_CWDataFormDialog);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(pageHeader);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(PreAdoptionPerson_FieldHeader);
            WaitForElement(PostAdoptionPerson_FieldHeader);
            WaitForElement(ResponsibleTeam_FieldHeader);
            WaitForElement(ResponsibleUser_FieldHeader);

            return this;
        }


        public PersonPostAdoptionLinkRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }




        public PersonPostAdoptionLinkRecordPage ValidatePreAdoptionPersonLinkText(String ExpectedText)
        {
            ValidateElementText(PreAdoptionPerson_LinkField, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidatePostAdoptionPersonLinkText(String ExpectedText)
        {
            ValidateElementText(PostAdoptionPerson_LinkField, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidateResponsibleTeamLinkText(String ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidateResponsibleUserLinkText(String ExpectedText)
        {
            ValidateElementText(ResponsibleUser_LinkField, ExpectedText);

            return this;
        }




        public PersonPostAdoptionLinkRecordPage ValidateMessageAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(notificationMessage);
            }
            else
            {
                WaitForElementNotVisible(notificationMessage, 3);
            }

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidatePreAdoptionPersonErrorAreaVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(PreAdoptionPerson_FieldMessageArea);
            }
            else
            {
                WaitForElementNotVisible(PreAdoptionPerson_FieldMessageArea, 3);
            }

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidatePostAdoptionPersonErrorAreaVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(PostAdoptionPerson_FieldMessageArea);
            }
            else
            {
                WaitForElementNotVisible(PostAdoptionPerson_FieldMessageArea, 3);
            }

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidateResponsibleTeamErrorAreaVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldMessageArea);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldMessageArea, 3);
            }

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidateResponsibleUserErrorAreaVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleUser_FieldMessageArea);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleUser_FieldMessageArea, 3);
            }

            return this;
        }



        public PersonPostAdoptionLinkRecordPage ValidateMessageAreaText(String ExpectedText)
        {
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidatePreAdoptionPersonErrorAreaText(String ExpectedText)
        {
            ValidateElementText(PreAdoptionPerson_FieldMessageArea, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidatePostAdoptionPersonErrorAreaText(String ExpectedText)
        {
            ValidateElementText(PostAdoptionPerson_FieldMessageArea, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidateResponsibleTeamErrorAreaText(String ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_FieldMessageArea, ExpectedText);

            return this;
        }
        public PersonPostAdoptionLinkRecordPage ValidateResponsibleUserErrorAreaText(String ExpectedText)
        {
            ValidateElementText(ResponsibleUser_FieldMessageArea, ExpectedText);

            return this;
        }



        public PersonPostAdoptionLinkRecordPage ClickPreAdoptionPersonLookUpButton()
        {
            WaitForElementToBeClickable(PreAdoptionPerson_FieldLookUpButton);
            Click(PreAdoptionPerson_FieldLookUpButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickPreAdoptionPersonRemoveButton()
        {
            WaitForElementToBeClickable(PreAdoptionPerson_FieldRemoveButton);
            Click(PreAdoptionPerson_FieldRemoveButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickPostAdoptionPersonLookUpButton()
        {
            WaitForElementToBeClickable(PostAdoptionPerson_FieldLookUpButton);
            Click(PostAdoptionPerson_FieldLookUpButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickPostAdoptionPersonRemoveButton()
        {
            WaitForElementToBeClickable(PostAdoptionPerson_FieldRemoveButton);
            Click(PostAdoptionPerson_FieldRemoveButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickResponsibleTeamLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_FieldLookUpButton);
            Click(ResponsibleTeam_FieldLookUpButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_FieldRemoveButton);
            Click(ResponsibleTeam_FieldRemoveButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickResponsibleUserLookUpButton()
        {
            WaitForElementToBeClickable(ResponsibleUser_FieldLookUpButton);
            Click(ResponsibleUser_FieldLookUpButton);
            return this;
        }
        public PersonPostAdoptionLinkRecordPage ClickResponsibleUserRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleUser_FieldRemoveButton);
            Click(ResponsibleUser_FieldRemoveButton);
            return this;
        }

    }
}
