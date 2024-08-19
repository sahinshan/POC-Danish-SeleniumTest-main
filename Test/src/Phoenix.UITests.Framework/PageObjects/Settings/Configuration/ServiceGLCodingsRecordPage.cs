using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceGLCodingsRecordPage : CommonMethods
    {
        public ServiceGLCodingsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialog_ServiceGLCodeingFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceglcoding')]");

      

        readonly By ServiceGLCodingsRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By shareRecord_Button = By.Id("TI_ShareRecordButton");
        readonly By assignRecord_Button = By.Id("TI_AssignRecordButton");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By serviceGLCodings_Tab = By.Id("CWNavGroup_ServiceGLCodings");
        readonly By backButton = By.Id("BackButton");

        readonly By serviceElement2_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_serviceelement2id']//span[@class = 'mandatory']");
        readonly By careType_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_caretypeid']//span[@class = 'mandatory']");
        readonly By clientCategory_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_clientcategoryid']//span[@class = 'mandatory']");

        readonly By serviceElement1_FieldLinkText = By.Id("CWField_serviceelement1id_Link");
        readonly By serviceElement2_DefaultField = By.Id("CWField_serviceelement2id_cwname");
        readonly By serviceElement2_FieldLinkText = By.Id("CWField_serviceelement2id_Link");
        readonly By careType_FieldLinkText = By.Id("CWField_caretypeid_Link");
        readonly By careType_DefaultField = By.Id("CWField_caretypeid_cwname");
        readonly By clientCategory_DefaultField = By.Id("CWField_clientcategoryid_cwname");
        readonly By clientCategory_FieldLinkText = By.Id("CWField_clientcategoryid_Link");
        readonly By serviceGLCode_DefaultField = By.Id("CWField_serviceglcodeid_cwname");
        readonly By serviceGLCode_FieldLinkText = By.Id("CWField_serviceglcodeid_Link");
        readonly By team_DefaultField = By.Id("CWField_teamid_cwname");
        readonly By team_FieldLinkText = By.Id("CWField_teamid_Link");
        readonly By teamGLCode_DefaultField = By.Id("CWField_teamglcodeid_cwname");
        readonly By teamGLCode_FieldLinkText = By.Id("CWField_teamglcodeid_Link");

        readonly By serviceElement1_LookUpButton = By.Id("CWLookupBtn_serviceelement1id");
        readonly By serviceElement2_LookUpButton = By.Id("CWLookupBtn_serviceelement2id");
        readonly By careType_LookupButton = By.Id("CWLookupBtn_caretypeid");
        readonly By clientCategory_LookUpButton = By.Id("CWLookupBtn_clientcategoryid");
        readonly By serviceGLCode_LookUpButton = By.Id("CWLookupBtn_serviceglcodeid");
        readonly By team_LookUpButton = By.Id("CWLookupBtn_teamid");
        readonly By teamGlCode_LookUpButton = By.Id("CWLookupBtn_teamglcodeid");
        readonly By responsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");

        readonly By se2OrCareTypeAll_NoOption = By.Id("CWField_se2all_0");
        readonly By se2OrCareTypeAll_YesOption = By.Id("CWField_se2all_1");

        readonly By clientCategoryAll_NoOption = By.Id("CWField_ccall_0");
        readonly By clientCategoryAll_YesOption = By.Id("CWField_ccall_1");

        By ValidRateUnitOption_Field(string Identifier) => By.XPath("//*[@id='MS_validrateunits_" + Identifier + "']");
        By ValidRateUnitOption_RemoveButton(string Identifier) => By.XPath("//*[@id='MS_validrateunits_" + Identifier + "']/a");


        readonly By DefaultRateUnit_LinkField = By.XPath("//*[@id='CWField_rateunitid_Link']");

        

        public ServiceGLCodingsRecordPage WaitForServiceGLCodingsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(cwDialog_ServiceGLCodeingFrame);
            SwitchToIframe(cwDialog_ServiceGLCodeingFrame);


            WaitForElement(ServiceGLCodingsRecordPageHeader);

           

            return this;
        }

        public ServiceGLCodingsRecordPage NavigateToServiceGLCodingsTab()
        {
            WaitForElementVisible(serviceGLCodings_Tab);
            Click(serviceGLCodings_Tab);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickSaveButton()
        {
            WaitForElementVisible(save_Button);
            Click(save_Button);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            MoveToElementInPage(backButton);
            Click(backButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateIconsAfterSave()
        {
            WaitForElementVisible(shareRecord_Button);
            WaitForElementVisible(assignRecord_Button);
            

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateRecordTitle(string PageTitle)
        {


            ValidateElementTextContainsText(pageHeader, "Service GL Coding:\r\n" + PageTitle);

            return this;
        }


        public ServiceGLCodingsRecordPage ClickServiceElement2LookUpButton()
        {
            WaitForElementToBeClickable(serviceElement2_LookUpButton);
            Click(serviceElement2_LookUpButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickTeamLookUpButton()
        {
            WaitForElementToBeClickable(team_LookUpButton);
            MoveToElementInPage(teamGlCode_LookUpButton);
            Click(team_LookUpButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickTeamGLCodeLookUpButton()
        {
            MoveToElementInPage(teamGlCode_LookUpButton);
            WaitForElementToBeClickable(teamGlCode_LookUpButton);
            Click(teamGlCode_LookUpButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickClientCategoryLookUpButton()
        {
            WaitForElementToBeClickable(clientCategory_LookUpButton);
            MoveToElementInPage(clientCategory_LookUpButton);
            Click(clientCategory_LookUpButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickCareTypeLookUpButton()
        {
            WaitForElementToBeClickable(careType_LookupButton);
            MoveToElementInPage(careType_LookupButton);
            Click(careType_LookupButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickServiceGlCodeLookUpButton()
        {
            WaitForElement(serviceGLCode_LookUpButton);
            Click(serviceGLCode_LookUpButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickServiceElement2OrCareTypeAllYesOption()
        {
            WaitForElementToBeClickable(se2OrCareTypeAll_YesOption);
            Click(se2OrCareTypeAll_YesOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickServiceElement2OrCareTypeAllNoOption()
        {
            WaitForElementToBeClickable(se2OrCareTypeAll_NoOption);
            Click(se2OrCareTypeAll_NoOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickClientCategoryAllYesOption()
        {
            WaitForElementToBeClickable(clientCategoryAll_YesOption);
            Click(clientCategoryAll_YesOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ClickClientCategoryAllNoOption()
        {
            WaitForElementToBeClickable(clientCategoryAll_NoOption);
            Click(clientCategoryAll_NoOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2OrCareTypeAllYesOptionChecked()
        {
            ValidateElementChecked(se2OrCareTypeAll_YesOption);
            ValidateElementNotChecked(se2OrCareTypeAll_NoOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2OrCareTypeAllNoOptionChecked()
        {
            ValidateElementChecked(se2OrCareTypeAll_NoOption);
            ValidateElementNotChecked(se2OrCareTypeAll_YesOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryAllYesOptionChecked()
        {
            ValidateElementChecked(clientCategoryAll_YesOption);
            ValidateElementNotChecked(clientCategoryAll_NoOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryAllNoOptionChecked()
        {
            ValidateElementChecked(clientCategoryAll_NoOption);
            ValidateElementNotChecked(clientCategoryAll_YesOption);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeLookupFieldDisabled()
        {
            WaitForElement(careType_LookupButton);
            ValidateElementDisabled(careType_LookupButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryLookupFieldDisabled()
        {
            WaitForElement(clientCategory_LookUpButton);
            ValidateElementDisabled(clientCategory_LookUpButton);

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2MandatoryFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceElement2_MandatoryField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceElement2_MandatoryField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2FieldLinkText(string ExpectedText)
        {
            WaitForElement(serviceElement2_FieldLinkText);
            MoveToElementInPage(serviceElement2_FieldLinkText);
            Assert.AreEqual(ExpectedText, GetElementText(serviceElement2_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeMandatoryFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(careType_MandatoryField);
            }           
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(careType_MandatoryField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeFieldLinkText(string ExpectedText)
        {
            WaitForElement(careType_FieldLinkText);
            MoveToElementInPage(careType_FieldLinkText);
            Assert.AreEqual(ExpectedText, GetElementText(careType_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryMandatoryFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(clientCategory_MandatoryField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(clientCategory_MandatoryField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement1FieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceElement1_FieldLinkText);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceElement1_FieldLinkText));
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement1LookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceElement1_LookUpButton);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceElement1_LookUpButton));
            return this;            
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement1LookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(serviceElement1_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(serviceElement1_LookUpButton);
            }
            
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2LookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(serviceElement2_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(serviceElement2_LookUpButton);
            }
            
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(careType_LookupButton);
            }
            else
            {
                ValidateElementNotDisabled(careType_LookupButton);
            }

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(clientCategory_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(clientCategory_LookUpButton);
            }

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceGLCodeLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(serviceGLCode_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(serviceGLCode_LookUpButton);
            }
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(team_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(team_LookUpButton);
            }
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamGLCodeLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(teamGlCode_LookUpButton);
            }
            else
            {
                ValidateElementNotDisabled(teamGlCode_LookUpButton);
            }
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2OrCareTypeAllButtonsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(se2OrCareTypeAll_YesOption);
                ValidateElementDisabled(se2OrCareTypeAll_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(se2OrCareTypeAll_YesOption);
                ValidateElementNotDisabled(se2OrCareTypeAll_NoOption);
            }
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryAllButtonsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(clientCategoryAll_YesOption);
                ValidateElementDisabled(clientCategoryAll_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(clientCategoryAll_YesOption);
                ValidateElementNotDisabled(clientCategoryAll_NoOption);
            }
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateResponsibleTeamLookupButtonDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementDisabled(responsibleTeam_LookupButton);
            }
            else
            {
                ValidateElementNotDisabled(responsibleTeam_LookupButton);
            }
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2DefaultFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceElement2_DefaultField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceElement2_DefaultField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeDefaultFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(careType_DefaultField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(careType_DefaultField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(careType_FieldLinkText);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(careType_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2FieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceElement2_FieldLinkText);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceElement2_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2LookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceElement2_LookUpButton);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceElement2_LookUpButton));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateCareTypeLookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(careType_LookupButton);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(careType_LookupButton));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryDefaultFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(clientCategory_DefaultField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(clientCategory_DefaultField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryFieldTextVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(clientCategory_FieldLinkText);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(clientCategory_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryLookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(clientCategory_LookUpButton);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(clientCategory_LookUpButton));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryFieldLinkText(string ExpectedText)
        {
            WaitForElement(clientCategory_FieldLinkText);
            MoveToElementInPage(clientCategory_FieldLinkText);
            Assert.AreEqual(ExpectedText, GetElementText(clientCategory_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceGLCodeDefaultFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceGLCode_DefaultField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceGLCode_DefaultField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceGLCodeFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceGLCode_FieldLinkText);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceGLCode_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceGLCodeLookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(serviceGLCode_LookUpButton);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(serviceGLCode_LookUpButton));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceGLCodeFieldLinkText(string ExpectedText)
        {
            WaitForElement(serviceGLCode_FieldLinkText);
            MoveToElementInPage(serviceGLCode_FieldLinkText);
            Assert.AreEqual(ExpectedText, GetElementText(serviceGLCode_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamDefaultFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(team_DefaultField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(team_DefaultField));
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(team_FieldLinkText);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(team_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamLookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(team_LookUpButton);
            }
            else
            {
                WaitForElementNotVisible(team_LookUpButton, 3);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(team_LookUpButton));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamGLCodeDefaultFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(teamGLCode_DefaultField);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(teamGLCode_DefaultField));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamGLCodeLinkFieldVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(teamGLCode_FieldLinkText);
            }        
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(teamGLCode_FieldLinkText));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateTeamGLCodeLookupButtonVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(teamGlCode_LookUpButton);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(teamGlCode_LookUpButton));

            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2OrCareTypeAllYesOptionVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(se2OrCareTypeAll_YesOption);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(se2OrCareTypeAll_YesOption));            
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateServiceElement2OrCareTypeAllNoOptionVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(se2OrCareTypeAll_NoOption);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(se2OrCareTypeAll_NoOption));
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryAllYesOptionVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(clientCategoryAll_YesOption);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(clientCategoryAll_YesOption));
            return this;
        }

        public ServiceGLCodingsRecordPage ValidateClientCategoryAllNoOptionVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(clientCategoryAll_NoOption);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(clientCategoryAll_NoOption));
            return this;
        }
    }
}
