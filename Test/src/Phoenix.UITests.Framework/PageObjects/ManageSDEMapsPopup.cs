using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ManageSDEMapsPopup : CommonMethods
    {
        public ManageSDEMapsPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupIframe = By.Id("iframe_CWSDEMapsDialog");
        readonly By popupHeader = By.Id("CWSDEDialogTitle");
        readonly By CreateMapPopupHeader = By.Id("CWSDEMapsEditDialogHeader");


        readonly By createMapFromThisQuestionButton = By.Id("CWSDEMapsBtnFromThisQuestion");
        readonly By createMapToThisQuestionButton = By.Id("CWSDEMapsBtnToThisQuestion");
        
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_RenderSDEMaps");

        readonly By whenDoYouWantToRunThisMappingLabel = By.Id("CWSDEMapsEdit_Mode_LB");
        readonly By twoWayMapLabel = By.Id("LB_CWSDEMapsEdit_TwoWay");
        readonly By documentCategoryLabel = By.Id("LB_CWSDEMapsEdit_DocumentCategory");
        readonly By documentLabel = By.Id("CWSDEMapsEdit_DocumentLabel");
        readonly By sectionLabel = By.Id("LB_CWSDEMapsEdit_Section");
        readonly By questionLabel = By.Id("LB_CWSDEMapsEdit_Questions");
        readonly By isConditionalLabel = By.Id("LB_CWSDEMapsEdit_Conditional");

        readonly By whenDoYouWantToRunThisMappingField = By.Id("CWSDEMapsEdit_Mode");
        readonly By twoWayMapField = By.Id("CWSDEMapsEdit_TwoWay");
        readonly By documentCategory = By.Id("CWSDEMapsEdit_DocumentCategory");
        readonly By documentLookupButton = By.Id("CWLookupBtn_CWSDEMapsEdit_Document");
        readonly By sectionField = By.Id("CWSDEMapsEdit_Section");
        readonly By isConditionalField = By.Id("CWSDEMapsEdit_Conditional");

        By questionCheckBox(string QuestionName) => By.XPath("//input[@type='checkbox'][@title='" + QuestionName  + "']");


        #region From This Question Section

        readonly By fromThisQuestionSectionTitle = By.XPath("//*[@id='SectionFromHeader']/h2[text()='From This Question']");

        By SDEMapFromThisQuestionLine1(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemfrom')]/div/div/div/div[1]/p");
        By SDEMapFromThisQuestionLine2(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemfrom')]/div/div/div/div[2]/p");
        By SDEMapFromThisQuestionLine3(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemfrom')]/div/div/div/div[3]/p");
        By SDEMapFromThisQuestionLine4(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemfrom')]/div/div/div/div[4]/p");
        By SDEMapFromThisQuestionLine5(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemfrom')]/div/div/div/div[5]/p");

        #endregion

        #region To This Question Section

        readonly By toThisQuestionSectionTitle = By.XPath("//*[@id='SectionToHeader']/h2[text()='To This Question']");

        By SDEMapToThisQuestionLine1(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemto')]/div/div/div/div[1]/p");
        By SDEMapToThisQuestionLine2(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemto')]/div/div/div/div[2]/p");
        By SDEMapToThisQuestionLine3(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "'][contains(@class,'itemto')]/div/div/div/div[3]/p");

        #endregion

        #region Manage SDE Map buttons

        By executeThisMappingWhenNewAssessmentIsCreatedButton(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "']/div/div/div[3]/div/div[1]/button");
        By nonConditionalMappingButton(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "']/div/div/div[3]/div/div[2]/button");
        By deleteSDEMapButton(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "']/div/div/div[3]/div/div[3]/button");
        By editSDEMapButton(string SDEMapID) => By.XPath("//div[@cwid='SDEMap_" + SDEMapID + "']/div/div/div[3]/div/div[4]/button");

        #endregion


        readonly By savebutton = By.Id("CWSDEMapsEdit_Save");
        readonly By closeButton = By.Id("CWSDEMapsEdit_Close");


        public ManageSDEMapsPopup WaitForManageSDEMapsPopupToLoad()
        {
            WaitForElement(popupIframe);
            SwitchToIframe(popupIframe);

            WaitForElement(popupHeader);
            
            WaitForElement(createMapFromThisQuestionButton);
            WaitForElement(createMapToThisQuestionButton);

            WaitForElementNotVisible(whenDoYouWantToRunThisMappingLabel, 3);
            WaitForElementNotVisible(twoWayMapLabel, 3);
            WaitForElementNotVisible(documentLabel, 3);
            WaitForElementNotVisible(sectionLabel, 3);
            WaitForElementNotVisible(questionLabel, 3);
            WaitForElementNotVisible(isConditionalLabel, 3);
            WaitForElementNotVisible(whenDoYouWantToRunThisMappingField, 3);
            WaitForElementNotVisible(twoWayMapField, 3);
            WaitForElementNotVisible(documentLookupButton, 3);
            WaitForElementNotVisible(sectionField, 3);
            WaitForElementNotVisible(isConditionalField, 3);

            return this;
        }

        public ManageSDEMapsPopup WaitForCreateSDEMapControlsToLoad()
        {
            WaitForElementVisible(whenDoYouWantToRunThisMappingLabel);
            WaitForElementVisible(twoWayMapLabel);
            WaitForElementVisible(documentCategoryLabel);
            WaitForElementVisible(documentLabel);
            WaitForElementVisible(sectionLabel);
            WaitForElementVisible(questionLabel);
            WaitForElementVisible(isConditionalLabel);
            
            WaitForElementVisible(whenDoYouWantToRunThisMappingField);
            WaitForElementVisible(twoWayMapField);
            WaitForElementVisible(documentCategory);
            WaitForElementVisible(documentLookupButton);
            WaitForElementVisible(sectionField);
            WaitForElementVisible(isConditionalField);

            return this;
        }

        public ManageSDEMapsPopup WaitForCreateSDEMapControlsToLoadInEditMode()
        {
            WaitForElementVisible(whenDoYouWantToRunThisMappingLabel);
            WaitForElementVisible(isConditionalLabel);

            WaitForElementNotVisible(twoWayMapLabel, 3);
            WaitForElementNotVisible(documentCategoryLabel, 3);
            WaitForElementNotVisible(documentLabel, 3);
            WaitForElementNotVisible(sectionLabel, 3);
            WaitForElementNotVisible(questionLabel, 3);
            

            WaitForElementVisible(whenDoYouWantToRunThisMappingField);
            WaitForElementVisible(isConditionalField);

            WaitForElementNotVisible(twoWayMapField, 3);
            WaitForElementNotVisible(documentCategory, 3);
            WaitForElementNotVisible(documentLookupButton, 3);
            WaitForElementNotVisible(sectionField, 3);

            return this;
        }

        public ManageSDEMapsPopup WaitForManageSDEMapsPopupToReload()
        {
            WaitForElement(popupHeader);

            WaitForElement(createMapFromThisQuestionButton);
            WaitForElement(createMapToThisQuestionButton);

            WaitForElementVisible(whenDoYouWantToRunThisMappingLabel);
            WaitForElementVisible(isConditionalLabel);

            return this;
        }

        /// <summary>
        /// this icon can be displayed in several situations, including when a SDE Map is deleted (after the user confirms the deletion on the alert popup)
        /// </summary>
        /// <param name="SDEMapID"></param>
        /// <returns></returns>
        public ManageSDEMapsPopup WaitForRefreshIconToBeClose()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ManageSDEMapsPopup ValidateNotificationMessagePresent(string ExpectedMessage)
        {
            WaitForElementVisible(notificationMessageArea);
            ValidateElementText(notificationMessageArea, ExpectedMessage);

            return this;
        }

        public ManageSDEMapsPopup ValidateHeaderText(string ExpectedText)
        {
            ValidateElementText(popupHeader, ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateCreatemapPopupHeaderText(string ExpectedText)
        {
            ValidateElementText(CreateMapPopupHeader, ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateFromThisQuestionSectionTitle()
        {
            WaitForElement(fromThisQuestionSectionTitle);

            return this;
        }

        public ManageSDEMapsPopup ValidateFromThisQuestionMappingLine1(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapFromThisQuestionLine1(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateFromThisQuestionMappingLine2(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapFromThisQuestionLine2(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateFromThisQuestionMappingLine3(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapFromThisQuestionLine3(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateFromThisQuestionMappingLine4(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapFromThisQuestionLine4(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateFromThisQuestionMappingLine5(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapFromThisQuestionLine5(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateToThisQuestionSectionTitle()
        {
            WaitForElement(toThisQuestionSectionTitle);

            return this;
        }

        public ManageSDEMapsPopup ValidateToThisQuestionMappingLine1(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapToThisQuestionLine1(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateToThisQuestionMappingLine2(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapToThisQuestionLine2(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateToThisQuestionMappingLine3(string SDEMapID, string ExpectedText)
        {
            ValidateElementText(SDEMapToThisQuestionLine3(SDEMapID), ExpectedText);

            return this;
        }

        public ManageSDEMapsPopup ValidateExecuteThisMappingWhenNewAssessmentIsCreatedButtonVisisble(string SDEMapID)
        {
            WaitForElementVisible(executeThisMappingWhenNewAssessmentIsCreatedButton(SDEMapID));

            return this;
        }
        
        public ManageSDEMapsPopup ValidateNonConditionalMappingButtonVisisble(string SDEMapID)
        {
            WaitForElementVisible(nonConditionalMappingButton(SDEMapID));

            return this;
        }
        
        public ManageSDEMapsPopup ValidateDeleteSDEMapButtonVisisble(string SDEMapID)
        {
            WaitForElementVisible(deleteSDEMapButton(SDEMapID));

            return this;
        }
        
        public ManageSDEMapsPopup ValidateEditSDEMapButtonVisisble(string SDEMapID)
        {
            WaitForElementVisible(editSDEMapButton(SDEMapID));

            return this;
        }

        public ManageSDEMapsPopup ClickEditSDEMapButton(string SDEMapID)
        {
            Click(editSDEMapButton(SDEMapID));

            return this;
        }

        public ManageSDEMapsPopup SelectWhenDoYouWantToRunThisMappingByText(string TextToSelect)
        {
            SelectPicklistElementByText(whenDoYouWantToRunThisMappingField, TextToSelect);

            return this;
        }

        public ManageSDEMapsPopup SelectTwoWayMapByText(string TextToSelect)
        {
            SelectPicklistElementByText(twoWayMapField, TextToSelect);

            return this;
        }

        public ManageSDEMapsPopup SelectDocumentCategory(string TextToSelect)
        {
            SelectPicklistElementByText(documentCategory, TextToSelect);

            return this;
        }

        public ManageSDEMapsPopup ClickDocumentLookupButton()
        {
            Click(documentLookupButton);

            return this;
        }

        public ManageSDEMapsPopup SelectSectionByText(string TextToSelect)
        {
            ScrollToElementViaJavascript(sectionField);
            SelectPicklistElementByText(sectionField, TextToSelect);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ManageSDEMapsPopup SelectIsConditionalText(string TextToSelect)
        {
            SelectPicklistElementByText(isConditionalField, TextToSelect);

            return this;
        }

        public ManageSDEMapsPopup SelectQuestion(string QuestionName)
        {
            ScrollToElementViaJavascript(questionCheckBox(QuestionName));
            Click(questionCheckBox(QuestionName));

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ManageSDEMapsPopup TapCreateMapFromThisQuestionButton()
        {
            Click(createMapFromThisQuestionButton);

            return this;
        }

        public ManageSDEMapsPopup TapCreateMapToThisQuestionButton()
        {
            Click(createMapToThisQuestionButton);

            return this;
        }

        public ManageSDEMapsPopup TapSDEMapDeleteButton(string SDEMapID)
        {
            Click(deleteSDEMapButton(SDEMapID));

            return this;
        }

        public ManageSDEMapsPopup TapSaveButton()
        {
            Click(savebutton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ManageSDEMapsPopup TapCloseButton()
        {
            Click(closeButton);

            return this;
        }
    }
}
