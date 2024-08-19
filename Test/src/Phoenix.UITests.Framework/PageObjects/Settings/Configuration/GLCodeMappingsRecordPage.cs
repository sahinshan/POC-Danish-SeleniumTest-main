using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class GLCodeMappingsRecordPage : CommonMethods
    {
        public GLCodeMappingsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=glcodemapping')]");

        readonly By ServiceMappingRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By ServiceMappingRecordTitle = By.XPath("//*[@id='CWToolbar']/div/h1/span[@class='record-title']");

        #region option toolbar

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By shareRecordButton = By.Id("TI_ShareRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By activateButton = By.Id("TI_ActivateButton");

        #endregion

        #region Details area

        readonly By ServiceElement1_FieldLabel = By.XPath("//*[@id='CWLabelHolder_serviceelement1id']/label");
        readonly By ServiceElement1_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_serviceelement1id']");
        readonly By ServiceElement1_LinkField = By.XPath("//*[@id='CWField_serviceelement1id_Link']");

        readonly By PositionNumber_FieldLabel = By.XPath("//*[@id='CWLabelHolder_positionnumber']/label");
        readonly By PositionNumber_InputField = By.Id("CWField_positionnumber");

        readonly By Level1_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level1id']/label");
        readonly By Level1_LookupButtonField = By.Id("CWLookupBtn_level1id");
        readonly By Level1_RemoveButtonField = By.Id("CWClearLookup_level1id");
        readonly By Level1_LinkField = By.Id("CWField_level1id_Link");

        readonly By Level2_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level2id']/label");
        readonly By Level2_LookupButtonField = By.Id("CWLookupBtn_level2id");
        readonly By Level2_RemoveButtonField = By.Id("CWClearLookup_level2id");
        readonly By Level2_LinkField = By.Id("CWField_level2id_Link");

        readonly By Level3_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level3id']/label");
        readonly By Level3_LookupButtonField = By.Id("CWLookupBtn_level3id");
        readonly By Level3_RemoveButtonField = By.Id("CWClearLookup_level3id");
        readonly By Level3_LinkField = By.Id("CWField_level3id_Link");

        readonly By Level4_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level4id']/label");
        readonly By Level4_LookupButtonField = By.Id("CWLookupBtn_level4id");
        readonly By Level4_RemoveButtonField = By.Id("CWClearLookup_level4id");
        readonly By Level4_LinkField = By.Id("CWField_level4id_Link");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_LookupButtonField = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButtonField = By.XPath("//*[@id='CWClearLookup_ownerid']");
        readonly By ResponsibleTeam_LinkField = By.XPath("//*[@id='CWField_ownerid_Link']");

        #endregion

        #region Constant

        readonly By Level1Constant_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level1constant']/label");
        readonly By Level1Constant_InputField = By.Id("CWField_level1constant");

        readonly By Level2Constant_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level2constant']/label");
        readonly By Level2Constant_InputField = By.Id("CWField_level2constant");

        readonly By Level3Constant_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level3constant']/label");
        readonly By Level3Constant_InputField = By.Id("CWField_level3constant");

        readonly By Level4Constant_FieldLabel = By.XPath("//*[@id='CWLabelHolder_level4constant']/label");
        readonly By Level4Constant_InputField = By.Id("CWField_level4constant");

        #endregion

        #region Rule Details
        readonly By RuleDetails_Section = By.Id("CWSection_RuleDetails");
        readonly By IfPosition_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ifposition']/label");
        readonly By IfPosition_InputField = By.Id("CWField_ifposition");

        readonly By IsGLCodeLocation_FieldLabel = By.XPath("//*[@id='CWLabelHolder_isglcodelocationid']/label");
        readonly By IsGLCodeLocation_LookupButtonField = By.Id("CWLookupBtn_isglcodelocationid");
        readonly By IsGLCodeLocation_RemoveButtonField = By.Id("CWClearLookup_isglcodelocationid");
        readonly By IsGLCodeLocation_LinkField = By.Id("CWField_isglcodelocationid_Link");

        readonly By Method_FieldLabel = By.XPath("//*[@id='CWLabelHolder_methodid']/label");
        readonly By Method_SelectField = By.Id("CWField_methodid");

        readonly By ThenUseGLCodeLocation_FieldLookup = By.Id("CWLookupBtn_usedglcodelocationid");
        readonly By ThenUseGLCodeLocation_LinkField = By.Id("CWField_usedglcodelocationid_Link");

        readonly By ThenUsePosition_Field = By.Id("CWField_usedposition");

        #endregion

        public GLCodeMappingsRecordPage WaitForGLCodeMappingsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(ServiceMappingRecordPageHeader);

            return this;
        }

        public GLCodeMappingsRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            MoveToElementInPage(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public GLCodeMappingsRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            MoveToElementInPage(saveButton);
            Click(saveButton);

            return this;
        }

        public GLCodeMappingsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public GLCodeMappingsRecordPage ClickDeleteButton(bool Toolbar = false)
        {
            if (Toolbar)
            {
                WaitForElementToBeClickable(additionalToolbarElementsButton);
                Click(additionalToolbarElementsButton);
            }

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }

        public GLCodeMappingsRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(shareRecordButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateDefaultFieldsVisible()
        {
            WaitForElementVisible(ServiceElement1_FieldLabel);
            WaitForElementVisible(ServiceElement1_LookupButtonField);

            WaitForElementVisible(PositionNumber_FieldLabel);
            WaitForElementVisible(PositionNumber_InputField);

            WaitForElementVisible(Level1_FieldLabel);
            WaitForElementVisible(Level1_LookupButtonField);

            WaitForElementVisible(ResponsibleTeam_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickServiceElement1LookupButton()
        {
            WaitForElementToBeClickable(ServiceElement1_LookupButtonField);
            Click(ServiceElement1_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel1LookupButton()
        {
            WaitForElementToBeClickable(Level1_LookupButtonField);
            Click(Level1_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel2LookupButtonIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level2_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(Level2_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(Level2_LookupButtonField, 5);
                Assert.IsFalse(GetElementVisibility(Level2_LookupButtonField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel3LookupButtonIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level3_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(Level3_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(Level3_LookupButtonField, 5);
                Assert.IsFalse(GetElementVisibility(Level3_LookupButtonField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel4LookupButtonIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level4_LookupButtonField);
                Assert.IsTrue(GetElementVisibility(Level4_LookupButtonField));
            }
            else
            {
                WaitForElementNotVisible(Level4_LookupButtonField, 5);
                Assert.IsFalse(GetElementVisibility(Level4_LookupButtonField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel1ConstantFieldIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level1Constant_InputField);
                Assert.IsTrue(GetElementVisibility(Level1Constant_InputField));
            }
            else
            {
                WaitForElementNotVisible(Level1Constant_InputField, 5);
                Assert.IsFalse(GetElementVisibility(Level1Constant_InputField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel2ConstantFieldIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level2Constant_InputField);
                Assert.IsTrue(GetElementVisibility(Level2Constant_InputField));
            }
            else
            {
                WaitForElementNotVisible(Level2Constant_InputField, 5);
                Assert.IsFalse(GetElementVisibility(Level2Constant_InputField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel3ConstantFieldIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level3Constant_InputField);
                Assert.IsTrue(GetElementVisibility(Level3Constant_InputField));
            }
            else
            {
                WaitForElementNotVisible(Level3Constant_InputField, 5);
                Assert.IsFalse(GetElementVisibility(Level3Constant_InputField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel4ConstantFieldIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(Level4Constant_InputField);
                Assert.IsTrue(GetElementVisibility(Level4Constant_InputField));
            }
            else
            {
                WaitForElementNotVisible(Level4Constant_InputField, 5);
                Assert.IsFalse(GetElementVisibility(Level4Constant_InputField));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel2LookupButton()
        {
            WaitForElementToBeClickable(Level2_LookupButtonField);
            Click(Level2_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel3LookupButton()
        {
            WaitForElementToBeClickable(Level3_LookupButtonField);
            Click(Level3_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel4LookupButton()
        {
            WaitForElementToBeClickable(Level4_LookupButtonField);
            Click(Level4_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButtonField);
            Click(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButtonField);
            Click(ResponsibleTeam_RemoveButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage InsertLevel1ConstantText(string TextToInsert)
        {
            WaitForElementVisible(Level1Constant_FieldLabel);
            WaitForElementToBeClickable(Level1Constant_InputField);
            MoveToElementInPage(Level1Constant_InputField);
            SendKeys(Level1Constant_InputField, TextToInsert);

            return this;
        }

        public GLCodeMappingsRecordPage InsertLevel2ConstantText(string TextToInsert)
        {
            WaitForElementToBeClickable(Level2Constant_InputField);
            MoveToElementInPage(Level2Constant_InputField);
            SendKeys(Level2Constant_InputField, TextToInsert);

            return this;
        }

        public GLCodeMappingsRecordPage InsertLevel3ConstantText(string TextToInsert)
        {
            WaitForElementToBeClickable(Level3Constant_InputField);
            MoveToElementInPage(Level3Constant_InputField);
            SendKeys(Level3Constant_InputField, TextToInsert);

            return this;
        }

        public GLCodeMappingsRecordPage InsertLevel4ConstantText(string TextToInsert)
        {
            WaitForElementToBeClickable(Level4Constant_InputField);
            MoveToElementInPage(Level4Constant_InputField);
            SendKeys(Level4Constant_InputField, TextToInsert);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel1RemoveButton()
        {
            WaitForElementToBeClickable(Level1_RemoveButtonField);
            MoveToElementInPage(Level1_RemoveButtonField);
            Click(Level1_RemoveButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel2RemoveButton()
        {
            WaitForElementToBeClickable(Level2_RemoveButtonField);
            MoveToElementInPage(Level2_RemoveButtonField);
            Click(Level2_RemoveButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel3RemoveButton()
        {
            WaitForElementToBeClickable(Level3_RemoveButtonField);
            MoveToElementInPage(Level3_RemoveButtonField);
            Click(Level3_RemoveButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ClickLevel4RemoveButton()
        {
            WaitForElementToBeClickable(Level4_RemoveButtonField);
            MoveToElementInPage(Level4_RemoveButtonField);
            Click(Level4_RemoveButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage SelectRuleDetailsMethod(String OptionToSelect)
        {
            MoveToElementInPage(RuleDetails_Section);
            WaitForElementToBeClickable(Method_SelectField);
            SelectPicklistElementByText(Method_SelectField, OptionToSelect);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateRuleDetailsFieldIsDisplayed(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(RuleDetails_Section);
                MoveToElementInPage(RuleDetails_Section);
            }
            else
            {
                WaitForElementNotVisible(RuleDetails_Section, 10);
            }

            Assert.AreEqual(ExpectVisible, GetElementVisibility(IfPosition_FieldLabel));
            Assert.AreEqual(ExpectVisible, GetElementVisibility(IfPosition_InputField));

            Assert.AreEqual(ExpectVisible, GetElementVisibility(IsGLCodeLocation_FieldLabel));
            Assert.AreEqual(ExpectVisible, GetElementVisibility(IsGLCodeLocation_LookupButtonField));

            Assert.AreEqual(ExpectVisible, GetElementVisibility(Method_FieldLabel));
            Assert.AreEqual(ExpectVisible, GetElementVisibility(Method_SelectField));

            return this;
        }

        public GLCodeMappingsRecordPage ValidateThenUseGLCodeLocationFieldIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(ThenUseGLCodeLocation_FieldLookup);
                Assert.IsTrue(GetElementVisibility(ThenUseGLCodeLocation_FieldLookup));
            }
            else
            {
                WaitForElementNotVisible(ThenUseGLCodeLocation_FieldLookup, 5);
                Assert.IsFalse(GetElementVisibility(ThenUseGLCodeLocation_FieldLookup));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateThenUsePositionFieldIsVisible(bool ExpectedStatus)
        {
            if (ExpectedStatus)
            {
                WaitForElementToBeClickable(ThenUsePosition_Field);
                Assert.IsTrue(GetElementVisibility(ThenUsePosition_Field));
            }
            else
            {
                WaitForElementNotVisible(ThenUsePosition_Field, 5);
                Assert.IsFalse(GetElementVisibility(ThenUsePosition_Field));
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateThenUseGLCodeLocationFieldText(string ExpectedText)
        {
            MoveToElementInPage(ThenUseGLCodeLocation_LinkField);
            WaitForElementToBeClickable(ThenUseGLCodeLocation_LinkField);
            ValidateElementByTitle(ThenUseGLCodeLocation_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateThenUsePositionFieldText(string ExpectedText)
        {
            MoveToElementInPage(ThenUsePosition_Field);
            WaitForElementToBeClickable(ThenUsePosition_Field);
            ValidateElementValue(ThenUsePosition_Field, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateServiceElement1LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ServiceElement1_LinkField);
            WaitForElementToBeClickable(ServiceElement1_LinkField);
            ValidateElementText(ServiceElement1_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel1LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(Level1_LinkField);
            WaitForElementToBeClickable(Level1_LinkField);
            ValidateElementText(Level1_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel2LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(Level2_LinkField);
            WaitForElementToBeClickable(Level2_LinkField);
            ValidateElementText(Level2_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel3LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(Level3_LinkField);
            WaitForElementToBeClickable(Level3_LinkField);
            ValidateElementText(Level3_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel4LinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(Level4_LinkField);
            WaitForElementToBeClickable(Level4_LinkField);
            ValidateElementText(Level4_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            MoveToElementInPage(ResponsibleTeam_LinkField);
            WaitForElementToBeClickable(ResponsibleTeam_LinkField);
            ValidateElementText(ResponsibleTeam_LinkField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidatePositionNumberFieldValue(string ExpectedText)
        {
            MoveToElementInPage(PositionNumber_InputField);
            ValidateElementValue(PositionNumber_InputField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateServiceElement1LinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceElement1_LinkField);
            else
                WaitForElementNotVisible(ServiceElement1_LinkField, 3);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateServiceElement1LookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ServiceElement1_LookupButtonField);
            else
                WaitForElementNotVisible(ServiceElement1_LookupButtonField, 3);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel1LinkFieldVisibility(bool ExpectVisible)
        {
            Assert.AreEqual(ExpectVisible, GetElementVisibility(Level1_LinkField));

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel1Visibility(bool ExpectVisible)
        {
            Assert.AreEqual(ExpectVisible, GetElementVisibility(Level1_FieldLabel));
            Assert.AreEqual(ExpectVisible, GetElementVisibility(Level1_LookupButtonField));

            return this;
        }

        public GLCodeMappingsRecordPage ValidateServiceElement1LookupIsDisabled()
        {
            MoveToElementInPage(ServiceElement1_LookupButtonField);
            ValidateElementDisabled(ServiceElement1_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel2LinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Level2_LinkField);
            else
                WaitForElementNotVisible(Level2_LinkField, 3);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel2Visibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Level2_FieldLabel);
                WaitForElementVisible(Level2_LookupButtonField);
            }
            else
            {
                WaitForElementNotVisible(Level2_FieldLabel, 3);
                WaitForElementNotVisible(Level2_LookupButtonField, 3);
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel3LinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Level3_LinkField);
            else
                WaitForElementNotVisible(Level3_LinkField, 3);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel3Visibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Level3_FieldLabel);
                WaitForElementVisible(Level3_LookupButtonField);
            }
            else
            {
                WaitForElementNotVisible(Level3_FieldLabel, 3);
                WaitForElementNotVisible(Level3_LookupButtonField, 3);
            }


            return this;
        }

        public GLCodeMappingsRecordPage ValidateResponsibleTeamLinkFieldVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_LinkField);
            else
                WaitForElementNotVisible(ResponsibleTeam_LinkField, 3);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateResponsibleTeamVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldLabel);
                WaitForElementVisible(ResponsibleTeam_LookupButtonField);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldLabel, 3);
                WaitForElementNotVisible(ResponsibleTeam_LookupButtonField, 3);
            }

            return this;
        }

        public GLCodeMappingsRecordPage ValidateResponsibleTeamRemoveButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeam_RemoveButtonField);
            else
                WaitForElementNotVisible(ResponsibleTeam_RemoveButtonField, 3);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel1ConstantFieldText(string ExpectedText)
        {
            WaitForElementVisible(Level1Constant_FieldLabel);
            WaitForElementToBeClickable(Level1Constant_InputField);
            MoveToElementInPage(Level1Constant_InputField);
            ValidateElementValue(Level1Constant_InputField, ExpectedText);

            return this;
        }


        public GLCodeMappingsRecordPage ValidatePositionNumberFieldIsDisabled()
        {
            MoveToElementInPage(PositionNumber_InputField);
            ValidateElementDisabled(PositionNumber_InputField);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel2ConstantText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level2Constant_InputField);
            MoveToElementInPage(Level2Constant_InputField);
            ValidateElementValue(Level2Constant_InputField, ExpectedText);

            return this;
        }


        public GLCodeMappingsRecordPage ValidateResponsibleTeamLookupIsDisabled()
        {
            MoveToElementInPage(ResponsibleTeam_LookupButtonField);
            ValidateElementDisabled(ResponsibleTeam_LookupButtonField);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel3ConstantText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level3Constant_InputField);
            MoveToElementInPage(Level3Constant_InputField);
            ValidateElementValue(Level3Constant_InputField, ExpectedText);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateGLCodeMappingRecordTitle(string Title)
        {
            WaitForElementVisible(ServiceMappingRecordTitle);
            ValidateElementText(ServiceMappingRecordTitle, Title);

            return this;
        }

        public GLCodeMappingsRecordPage ValidateLevel4ConstantText(string ExpectedText)
        {
            WaitForElementToBeClickable(Level4Constant_InputField);
            MoveToElementInPage(Level4Constant_InputField);
            ValidateElementValue(Level4Constant_InputField, ExpectedText);

            return this;
        }

    }
}