using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings
{
    public class AboutMeSetupRecordPage : CommonMethods
    {
        public AboutMeSetupRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");        
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=aboutmesetup&')]");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By backButton = By.XPath("//button[@title = 'Back']");
        readonly By LoadingImage = By.XPath("//*[@class = 'loader']");

        readonly By MenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/button");

        readonly By relatedItemsDetailsElementExpanded = By.XPath("//span[text()='Related Items']/parent::div/parent::summary/parent::details[@open]");
        readonly By relatedItemsLeftSubMenu = By.XPath("//details/summary/div/span[text()='Related Items']");

        readonly By detailsTab = By.XPath("//li//a[text() = 'Details']");

        readonly By audit_MenuLeftSubMenu = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By aboutMeGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingabout']/label/span");
        readonly By whatIsMostImportantToMeGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingwhat']/label/span");
        readonly By peopleWhoAreImportantToMeGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingpeople']/label/span");
        readonly By howICommunicateGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturinghow']/label/span");
        readonly By pleaseDoAndPleaseDoNotGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingpleasedoand']/label/span");
        readonly By myWellnessGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingmywellness']/label/span");
        readonly By howAndWhenToSupportMeGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturinghowandwhento']/label/span");
        readonly By alsoWorthKnowingAboutMeGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingalsoworthkno']/label/span");
        readonly By physicalCharacteristicsGuidelines_ErrorLabel = By.XPath("//*[@id='CWControlHolder_guidelinestoconsiderwhencapturingguidancetoco']/label/span");

        #region General section
        readonly By responsibleTeam_label = By.XPath("//li[@id = 'CWLabelHolder_ownerid']/label");
        readonly By responsibleTeam_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_ownerid']/label/span[@class = 'mandatory']");
        readonly By enableMediaContent_label = By.XPath("//li[@id = 'CWLabelHolder_enablemediacontent']/label");
        readonly By enableMediaContent_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_enablemediacontent']/label/span[@class = 'mandatory']");
        readonly By status_label = By.XPath("//li[@id = 'CWLabelHolder_statusid']/label");
        readonly By status_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_statusid']/label/span[@class = 'mandatory']");
        readonly By hideAboutMeSection_label = By.XPath("//li[@id = 'CWLabelHolder_hideaboutmesection']/label");
        readonly By hideAboutMeSection_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hideaboutmesection']/label/span[@class = 'mandatory']");
        readonly By aboutMeGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingabout']/label");
        readonly By aboutMeGuideline_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingabout']/label/span[@class = 'mandatory']");
        readonly By hideWhatIsMostImportant_label = By.XPath("//li[@id = 'CWLabelHolder_hidewhatismostimportanttomesection']/label");
        readonly By hideWhatIsMostImportant_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidewhatismostimportanttomesection']/label/span[@class = 'mandatory']");
        readonly By whatIsMostImportantGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingwhat']/label");
        readonly By whatIsMostImportantGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingwhat']/label/span[@class = 'mandatory']");
        readonly By hidePeopleWhoAreImportant_label = By.XPath("//li[@id = 'CWLabelHolder_hidepeoplewhoareimportanttomesect']/label");
        readonly By hidePeopleWhoAreImportant_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidepeoplewhoareimportanttomesect']/label/span[@class = 'mandatory']");
        readonly By peopleWhoAreImportantGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingpeople']/label");
        readonly By peopleWhoAreImportantGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingpeople']/label/span[@class = 'mandatory']");
        readonly By hideHowICommunicate_label = By.XPath("//li[@id = 'CWLabelHolder_hidehowicommunicateandhowtocom']/label");
        readonly By hideHowICommunicate_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidehowicommunicateandhowtocom']/label/span[@class = 'mandatory']");
        readonly By howICommunicateGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturinghow']/label");
        readonly By howICommunicatetGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturinghow']/label/span[@class = 'mandatory']");
        readonly By hidePleaseDoAndPleaseDoNot_label = By.XPath("//li[@id = 'CWLabelHolder_hidepleasedoandpleasedonotsection']/label");
        readonly By hidePleaseDoAndPleaseDoNot_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidepleasedoandpleasedonotsection']/label/span[@class = 'mandatory']");
        readonly By pleaseDoAndPleaseDoNotGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingpleasedoand']/label");
        readonly By pleaseDoAndPleaseDoNotGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingpleasedoand']/label/span[@class = 'mandatory']");
        readonly By hideMyWellness_label = By.XPath("//li[@id = 'CWLabelHolder_hidemywellnesssection']/label");
        readonly By hideMyWellness_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidemywellnesssection']/label/span[@class = 'mandatory']");
        readonly By myWellnessGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingmywellness']/label");
        readonly By myWellnessGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingmywellness']/label/span[@class = 'mandatory']");
        readonly By hideHowAndWhenToSupport_label = By.XPath("//li[@id = 'CWLabelHolder_hidehowandwhentosupportmesection']/label");
        readonly By hideHowAndWhenToSupport_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidehowandwhentosupportmesection']/label/span[@class = 'mandatory']");
        readonly By howAndWhenToSupportGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturinghowandwhento']/label");
        readonly By howAndWhenToSupportGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturinghowandwhento']/label/span[@class = 'mandatory']");
        readonly By hideAlsoWorthKnowingAbout_label = By.XPath("//li[@id = 'CWLabelHolder_hidealsoworthknowingaboutmesection']/label");
        readonly By hideAlsoWorthKnowingAbout_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidealsoworthknowingaboutmesection']/label/span[@class = 'mandatory']");
        readonly By alsoWorthKnowingAboutGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingalsoworthkno']/label");
        readonly By alsoWorthKnowingAboutGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingalsoworthkno']/label/span[@class = 'mandatory']");
        readonly By hidePhysicalCharacteristics_label = By.XPath("//li[@id = 'CWLabelHolder_hidephysicalcharacteristicssection']/label");
        readonly By hidePhysicalCharacteristics_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_hidephysicalcharacteristicssection']/label/span[@class = 'mandatory']");
        readonly By physicalCharacteristicsGuidelines_label = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingguidancetoco']/label");
        readonly By physicalCharacteristicsGuidelines_MandatoryField = By.XPath("//li[@id = 'CWLabelHolder_guidelinestoconsiderwhencapturingguidancetoco']/label/span[@class = 'mandatory']");

        readonly By responsibleTeam_Field = By.XPath("//*[@id = 'CWField_ownerid_Link']");
        readonly By responsibleUser_Lookup = By.XPath("//button[@id = 'CWLookupBtn_ownerid']");//input[@id = 'CWField_ownerid_cwname'] //button[@title = 'Lookup Responsible Team']

        readonly By enableMediaContent_YesOption = By.Id("CWField_enablemediacontent_1");
        readonly By enableMediaContent_NoOption = By.Id("CWField_enablemediacontent_0");
        readonly By status_Picklist = By.Id("CWField_statusid");

        readonly By hideAboutMe_YesOption = By.Id("CWField_hideaboutmesection_1");
        readonly By hideAboutMe_NoOption = By.Id("CWField_hideaboutmesection_0");
        readonly By aboutMeTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingabout']");

        readonly By hideWhatIsMostImportant_YesOption = By.Id("CWField_hidewhatismostimportanttomesection_1");
        readonly By hideWhatIsMostImportant_NoOption = By.Id("CWField_hidewhatismostimportanttomesection_0");
        readonly By whatIsMostImportantTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingwhat']");

        readonly By hidePeopleWhoAreImportant_YesOption = By.Id("CWField_hidepeoplewhoareimportanttomesect_1");
        readonly By hidePeopleWhoAreImportant_NoOption = By.Id("CWField_hidepeoplewhoareimportanttomesect_0");
        readonly By peopleWhoAreImportantTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingpeople']");

        readonly By hideHowICommunicate_YesOption = By.Id("CWField_hidehowicommunicateandhowtocom_1");
        readonly By hideHowICommunicate_NoOption = By.Id("CWField_hidehowicommunicateandhowtocom_0");
        readonly By howICommunicateTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturinghow']");

        readonly By hidePleaseDoAndPleaseDoNot_YesOption = By.Id("CWField_hidepleasedoandpleasedonotsection_1");
        readonly By hidePleaseDoAndPleaseDoNot_NoOption = By.Id("CWField_hidepleasedoandpleasedonotsection_0");
        readonly By pleaseDoAndPleaseDoNotTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingpleasedoand']");

        readonly By hideMyWellness_YesOption = By.Id("CWField_hidemywellnesssection_1");
        readonly By hideMyWellness_NoOption = By.Id("CWField_hidemywellnesssection_0");
        readonly By myWellnessTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingmywellness']");

        readonly By hideHowAndWhenToSupport_YesOption = By.Id("CWField_hidehowandwhentosupportmesection_1");
        readonly By hideHowAndWhenToSupport_NoOption = By.Id("CWField_hidehowandwhentosupportmesection_0");
        readonly By howAndWhenToSupportTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturinghowandwhento']");

        readonly By hideAlsoWorthKnowingAbout_YesOption = By.Id("CWField_hidealsoworthknowingaboutmesection_1");
        readonly By hideAlsoWorthKnowingAbout_NoOption = By.Id("CWField_hidealsoworthknowingaboutmesection_0");
        readonly By alsoWorthKnowingAboutTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingalsoworthkno']");

        readonly By hidePhysicalCharacteristics_YesOption = By.Id("CWField_hidephysicalcharacteristicssection_1");
        readonly By hidePhysicalCharacteristics_NoOption = By.Id("CWField_hidephysicalcharacteristicssection_0");
        readonly By physicalCharacteristicsTextarea = By.XPath("//textarea[@id = 'CWField_guidelinestoconsiderwhencapturingguidancetoco']");

        readonly By recordActiveStatusYesNo = By.XPath("//label[text() = 'Active']/following-sibling::span");

        #endregion

        public AboutMeSetupRecordPage WaitForAboutMeSetupRecordPageToLoad()
        {
            SwitchToDefaultFrame();
            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);
            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);
            
            return this;
        }

        public AboutMeSetupRecordPage NavigateToAuditPage()
        {
            WaitForElementToBeClickable(MenuButton);
            Click(MenuButton);

            if (!CheckIfElementExists(relatedItemsDetailsElementExpanded)) //if the element is not expanded, then click on it
            {
                WaitForElementToBeClickable(relatedItemsLeftSubMenu);
                Click(relatedItemsLeftSubMenu);
            }

            WaitForElementToBeClickable(audit_MenuLeftSubMenu);
            Click(audit_MenuLeftSubMenu);

            return this;
        }

        public AboutMeSetupRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);
            Thread.Sleep(500);
            return this;

        }

        public AboutMeSetupRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);
            return this;

        }

        public AboutMeSetupRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);
            return this;
        }

        public AboutMeSetupRecordPage ClickDetailsTab()
        {
            WaitForElementToBeClickable(detailsTab);
            ScrollToElement(detailsTab);
            Click(detailsTab);

            return this;
        }

        public AboutMeSetupRecordPage ValidateMessageAreaText(string ExpectedText)
        {            
            ValidateElementText(notificationMessage, ExpectedText);
            Assert.AreEqual(ExpectedText, GetElementText(notificationMessage));

            return this;
        }

        public AboutMeSetupRecordPage ValidatePageHeader(string expectedText)
        {
            WaitForElement(pageHeader);
            ScrollToElement(pageHeader);
            string pageHeaderValue = GetElementByAttributeValue(pageHeader, "title");
            Assert.IsTrue(pageHeaderValue.Contains(expectedText), "Expected: " + expectedText + " || Actual: " + pageHeaderValue);
            return this;
        }


        public AboutMeSetupRecordPage ValidateResponsibleTeamMandatoryField()
        {
            WaitForElement(responsibleTeam_MandatoryField);
            WaitForElement(responsibleTeam_Field);
            WaitForElement(responsibleUser_Lookup);
            Assert.IsTrue(GetElementVisibility(responsibleTeam_MandatoryField));
            Assert.IsTrue(GetElementVisibility(responsibleTeam_Field));
            Assert.IsTrue(GetElementVisibility(responsibleUser_Lookup));
            return this;
        }

        public AboutMeSetupRecordPage ValidateEnableMediaContentMandatoryField()
        {            
            Assert.IsTrue(GetElementVisibility(enableMediaContent_MandatoryField));
            Assert.IsTrue(GetElementVisibility(enableMediaContent_YesOption));
            Assert.IsTrue(GetElementVisibility(enableMediaContent_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateStatusMandatoryField()
        {            
            Assert.IsTrue(GetElementVisibility(status_MandatoryField));
            Assert.IsTrue(GetElementVisibility(status_Picklist));
            return this;
        }

        public AboutMeSetupRecordPage ValidateSelectedStatusField(string expectedOption)
        {
            ValidatePicklistSelectedText(status_Picklist, expectedOption);
            return this;
        }

        public AboutMeSetupRecordPage ValidateStatusFieldOptionNotPresent(string optionNotPresent)
        {
            ValidatePicklistDoesNotContainsElementByText(status_Picklist, optionNotPresent);
            return this;
        }

        public AboutMeSetupRecordPage SelectAboutMeSetupStatus(string optionToSelect)
        {
            WaitForElement(status_Picklist);
            ScrollToElement(status_Picklist);
            SelectPicklistElementByText(status_Picklist, optionToSelect);
            return this;
        }

        #region About Me section
        public AboutMeSetupRecordPage ValidateHideAboutMeSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hideAboutMeSection_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hideAboutMe_YesOption));
            Assert.IsTrue(GetElementVisibility(hideAboutMe_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateAboutMeGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(aboutMeGuideline_MandatoryField);
                WaitForElement(aboutMeTextarea);
                Assert.IsTrue(GetElementVisibility(aboutMeGuideline_MandatoryField));
                Assert.IsTrue(GetElementVisibility(aboutMeTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(aboutMeGuideline_MandatoryField));
                Assert.IsFalse(GetElementVisibility(aboutMeTextarea));

            }
            return this;
        }

        #endregion

        #region What is most important to Me section
        public AboutMeSetupRecordPage ValidateHideWhatIsMostImportantToMeSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hideWhatIsMostImportant_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hideWhatIsMostImportant_YesOption));
            Assert.IsTrue(GetElementVisibility(hideWhatIsMostImportant_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateWhatIsMostImportantGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(whatIsMostImportantGuidelines_MandatoryField);
                WaitForElement(whatIsMostImportantTextarea);
                Assert.IsTrue(GetElementVisibility(whatIsMostImportantGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(whatIsMostImportantTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantTextarea));

            }
            return this;
        }
        #endregion

        #region People Who Are Important To Me section
        public AboutMeSetupRecordPage ValidateHidePeopleWhoAreImportantToMeSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hidePeopleWhoAreImportant_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hidePeopleWhoAreImportant_YesOption));
            Assert.IsTrue(GetElementVisibility(hidePeopleWhoAreImportant_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidatePeopleWhoAreImportantToMeGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(peopleWhoAreImportantGuidelines_MandatoryField);
                WaitForElement(peopleWhoAreImportantTextarea);
                Assert.IsTrue(GetElementVisibility(peopleWhoAreImportantGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(peopleWhoAreImportantTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantTextarea));

            }
            return this;
        }
        #endregion

        #region How I Communicate And How To Communicate With Me section
        public AboutMeSetupRecordPage ValidateHideHowICommunicateAndHowToCommunicateWithMeSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hideHowICommunicate_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hideHowICommunicate_YesOption));
            Assert.IsTrue(GetElementVisibility(hideHowICommunicate_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateHowICommunicateAndHowToCommunicateWithMeGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(howICommunicatetGuidelines_MandatoryField);
                WaitForElement(howICommunicateTextarea);
                Assert.IsTrue(GetElementVisibility(howICommunicatetGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(howICommunicateTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howICommunicatetGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(howICommunicateTextarea));

            }
            return this;
        }
        #endregion

        #region Please Do And Please Do Not section
        public AboutMeSetupRecordPage ValidateHidePleaseDoAndPleaseDoNotSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hidePleaseDoAndPleaseDoNot_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hidePleaseDoAndPleaseDoNot_YesOption));
            Assert.IsTrue(GetElementVisibility(hidePleaseDoAndPleaseDoNot_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidatePleaseDoAndPleaseDoNotSectionGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(pleaseDoAndPleaseDoNotGuidelines_MandatoryField);
                WaitForElement(pleaseDoAndPleaseDoNotTextarea);
                Assert.IsTrue(GetElementVisibility(pleaseDoAndPleaseDoNotGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(pleaseDoAndPleaseDoNotTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(pleaseDoAndPleaseDoNotGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(pleaseDoAndPleaseDoNotTextarea));

            }
            return this;
        }
        #endregion

        #region My Wellness section
        public AboutMeSetupRecordPage ValidateHideMyWellnessSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hideMyWellness_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hideMyWellness_YesOption));
            Assert.IsTrue(GetElementVisibility(hideMyWellness_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateMyWellnessSectionGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(myWellnessGuidelines_MandatoryField);
                WaitForElement(myWellnessTextarea);
                Assert.IsTrue(GetElementVisibility(myWellnessGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(myWellnessTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(myWellnessGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(myWellnessTextarea));

            }
            return this;
        }
        #endregion

        #region How And When To Support Me section
        public AboutMeSetupRecordPage ValidateHideHowAndWhenToSupportMeSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hideHowAndWhenToSupport_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hideHowAndWhenToSupport_YesOption));
            Assert.IsTrue(GetElementVisibility(hideHowAndWhenToSupport_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateHowAndWhenToSupportMeSectionGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(howAndWhenToSupportGuidelines_MandatoryField);
                WaitForElement(howAndWhenToSupportTextarea);
                Assert.IsTrue(GetElementVisibility(howAndWhenToSupportGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(howAndWhenToSupportTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howAndWhenToSupportGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(howAndWhenToSupportTextarea));

            }
            return this;
        }
        #endregion

        #region Also Worth Knowing About Me section
        public AboutMeSetupRecordPage ValidateHideAlsoWorthKnowingAboutSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hideAlsoWorthKnowingAbout_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hideAlsoWorthKnowingAbout_YesOption));
            Assert.IsTrue(GetElementVisibility(hideAlsoWorthKnowingAbout_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidateAlsoWorthKnowingAboutSectionGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(alsoWorthKnowingAboutGuidelines_MandatoryField);
                WaitForElement(alsoWorthKnowingAboutTextarea);
                Assert.IsTrue(GetElementVisibility(alsoWorthKnowingAboutGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(alsoWorthKnowingAboutTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutTextarea));

            }
            return this;
        }
        #endregion

        #region Physical Characteristics section
        public AboutMeSetupRecordPage ValidateHidePhysicalCharacteristicsSectionMandatoryField()
        {
            Assert.IsTrue(GetElementVisibility(hidePhysicalCharacteristics_MandatoryField));
            Assert.IsTrue(GetElementVisibility(hidePhysicalCharacteristics_YesOption));
            Assert.IsTrue(GetElementVisibility(hidePhysicalCharacteristics_NoOption));
            return this;
        }

        public AboutMeSetupRecordPage ValidatePhysicalCharacteristicsSectionGuidelinesMandatoryField(bool expectedState)
        {
            if (expectedState)
            {
                WaitForElement(physicalCharacteristicsGuidelines_MandatoryField);
                WaitForElement(physicalCharacteristicsTextarea);
                Assert.IsTrue(GetElementVisibility(physicalCharacteristicsGuidelines_MandatoryField));
                Assert.IsTrue(GetElementVisibility(physicalCharacteristicsTextarea));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsGuidelines_MandatoryField));
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsTextarea));

            }
            return this;
        }
        #endregion

        #region Field populating and validating methods

        public AboutMeSetupRecordPage ValidateAboutMeGuidelinesFieldErrorLabelText(string ExpectedText)
        {         
            ValidateElementByTitle(aboutMeGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidateWhatIsMostImportantToMeGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(whatIsMostImportantToMeGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidatePeopleWhoAreImportantToMeGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(peopleWhoAreImportantToMeGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidateHowICommunicateGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(howICommunicateGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidatePleaseDoAndPleaseDoNotGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(pleaseDoAndPleaseDoNotGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidateMyWellnessGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(myWellnessGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidateHowAndWhenToSupportGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(howAndWhenToSupportMeGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidateAlsoWorthKnowingAboutMeGuidelinesFieldErrorLabelText(string ExpectedText)
        {           
            ValidateElementByTitle(alsoWorthKnowingAboutMeGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage ValidatePhysicalCharacteristicsGuidelinesFieldErrorLabelText(string ExpectedText)
        {            
            ValidateElementByTitle(physicalCharacteristicsGuidelines_ErrorLabel, ExpectedText);
            return this;
        }

        public AboutMeSetupRecordPage InsertAboutMeGuidelines(string textToInsert)
        {
            WaitForElement(aboutMeTextarea);
            SendKeys(aboutMeTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertWhatIsMostImportantToMeGuidelines(string textToInsert)
        {
            WaitForElement(whatIsMostImportantTextarea);
            SendKeys(whatIsMostImportantTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertPeopleWhoAreImportantToMeGuidelines(string textToInsert)
        {
            WaitForElement(peopleWhoAreImportantTextarea);
            SendKeys(peopleWhoAreImportantTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertHowICommunicateAndHowToCommunicateWithMeGuidelines(string textToInsert)
        {
            WaitForElement(howICommunicateTextarea);
            SendKeys(howICommunicateTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertPleaseDoAndPleaseDoNotGuidelines(string textToInsert)
        {
            WaitForElement(pleaseDoAndPleaseDoNotTextarea);
            SendKeys(pleaseDoAndPleaseDoNotTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertMyWellnessGuidelines(string textToInsert)
        {
            WaitForElement(myWellnessTextarea);
            SendKeys(myWellnessTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertHowAndWhenToSupportMeGuidelines(string textToInsert)
        {
            WaitForElement(howAndWhenToSupportTextarea);
            SendKeys(howAndWhenToSupportTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertAlsoWorthKnowingAboutMeGuidelines(string textToInsert)
        {
            WaitForElement(alsoWorthKnowingAboutTextarea);
            SendKeys(alsoWorthKnowingAboutTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage InsertPhysicalCharacteristicsGuidelines(string textToInsert)
        {
            WaitForElement(physicalCharacteristicsTextarea);
            SendKeys(physicalCharacteristicsTextarea, textToInsert);
            return this;
        }

        public AboutMeSetupRecordPage ValidateAboutMeGuidelinesField(string ExpectedText)
        {
            WaitForElement(aboutMeTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingabout"));
            return this;
        }

        public AboutMeSetupRecordPage ValidateWhatIsMostImportantToMeGuidelinesField(string ExpectedText)
        {
            WaitForElement(whatIsMostImportantTextarea);            
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingwhat"));
            return this;
        }

        public AboutMeSetupRecordPage ValidatePeopleWhoAreImportantToMeGuidelinesField(string ExpectedText)
        {
            WaitForElement(peopleWhoAreImportantTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingpeople"));
            return this;
        }

        public AboutMeSetupRecordPage ValidateHowICommunicateGuidelinesField(string ExpectedText)
        {
            WaitForElement(howICommunicateTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturinghow"));
            return this;
        }

        public AboutMeSetupRecordPage ValidatePleaseDoAndPleaseDoNotGuidelinesField(string ExpectedText)
        {
            WaitForElement(pleaseDoAndPleaseDoNotTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingpleasedoand"));
            return this;
        }

        public AboutMeSetupRecordPage ValidateMyWellnessGuidelinesField(string ExpectedText)
        {
            WaitForElement(myWellnessTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingmywellness"));
            return this;
        }

        public AboutMeSetupRecordPage ValidateHowAndWhenToSupportGuidelinesField(string ExpectedText)
        {
            WaitForElement(howAndWhenToSupportTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturinghowandwhento"));
            return this;
        }

        public AboutMeSetupRecordPage ValidateAlsoWorthKnowingAboutMeGuidelinesField(string ExpectedText)
        {
            WaitForElement(alsoWorthKnowingAboutTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingalsoworthkno"));
            return this;
        }

        public AboutMeSetupRecordPage ValidatePhysicalCharacteristicsGuidelinesField(string ExpectedText)
        {
            WaitForElement(physicalCharacteristicsTextarea);
            Assert.AreEqual(ExpectedText, GetElementValueByJavascript("CWField_guidelinestoconsiderwhencapturingguidancetoco"));
            return this;
        }

        public AboutMeSetupRecordPage SelectHideAboutMeSectionOption(bool option)
        {
            WaitForElement(hideAboutMe_YesOption);
            WaitForElement(hideAboutMe_NoOption);
            if (option)
            {
                Click(hideAboutMe_YesOption);
            }
            else
            {
                Click(hideAboutMe_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHideAboutMeSectionSelectedOption(bool option)
        {
            WaitForElement(hideAboutMe_YesOption);
            WaitForElement(hideAboutMe_NoOption);
            if (option)
            {
                ValidateElementChecked(hideAboutMe_YesOption);
                ValidateElementNotChecked(hideAboutMe_NoOption);
            }
            else
            {
                ValidateElementChecked(hideAboutMe_NoOption);
                ValidateElementNotChecked(hideAboutMe_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHideWhatIsMostImportantToMeSectionOption(bool option)
        {
            WaitForElement(hideWhatIsMostImportant_YesOption);
            WaitForElement(hideWhatIsMostImportant_NoOption);
            if (option)
            {
                Click(hideWhatIsMostImportant_YesOption);
            }
            else
            {
                Click(hideWhatIsMostImportant_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHideWhatIsMostImportantToMeSectionSelectedOption(bool option)
        {
            WaitForElement(hidePeopleWhoAreImportant_YesOption);
            WaitForElement(hidePeopleWhoAreImportant_NoOption);
            if (option)
            {
                ValidateElementChecked(hidePeopleWhoAreImportant_YesOption);
                ValidateElementNotChecked(hidePeopleWhoAreImportant_NoOption);
            }
            else
            {
                ValidateElementChecked(hidePeopleWhoAreImportant_NoOption);
                ValidateElementNotChecked(hidePeopleWhoAreImportant_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHidePeopleWhoAreImportantToMeSectionOption(bool option)
        {
            WaitForElement(hidePeopleWhoAreImportant_YesOption);
            WaitForElement(hidePeopleWhoAreImportant_NoOption);
            if (option)
            {
                Click(hidePeopleWhoAreImportant_YesOption);
            }
            else
            {
                Click(hidePeopleWhoAreImportant_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHidePeopleWhoAreImportantToMeSectionSelectedOption(bool option)
        {
            WaitForElement(hidePeopleWhoAreImportant_YesOption);
            WaitForElement(hidePeopleWhoAreImportant_NoOption);
            if (option)
            {
                ValidateElementChecked(hidePeopleWhoAreImportant_YesOption);
                ValidateElementNotChecked(hidePeopleWhoAreImportant_NoOption);
            }
            else
            {
                ValidateElementChecked(hidePeopleWhoAreImportant_NoOption);
                ValidateElementNotChecked(hidePeopleWhoAreImportant_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHideHowICommunicateAndHowToCommunicateSectionOption(bool option)
        {
            WaitForElement(hideHowICommunicate_YesOption);
            WaitForElement(hideHowICommunicate_NoOption);
            if (option)
            {
                Click(hideHowICommunicate_YesOption);
            }
            else
            {
                Click(hideHowICommunicate_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHideHowICommunicateAndHowToCommunicateSectionSelectedOption(bool option)
        {
            WaitForElement(hideHowICommunicate_YesOption);
            WaitForElement(hideHowICommunicate_NoOption);
            if (option)
            {
                ValidateElementChecked(hideHowICommunicate_YesOption);
                ValidateElementNotChecked(hideHowICommunicate_NoOption);
            }
            else
            {
                ValidateElementChecked(hideHowICommunicate_NoOption);
                ValidateElementNotChecked(hideHowICommunicate_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHidePleaseDoAndPleaseDoNotSectionOption(bool option)
        {
            WaitForElement(hidePleaseDoAndPleaseDoNot_YesOption);
            WaitForElement(hidePleaseDoAndPleaseDoNot_NoOption);
            if (option)
            {
                Click(hidePleaseDoAndPleaseDoNot_YesOption);
            }
            else
            {
                Click(hidePleaseDoAndPleaseDoNot_NoOption);

            }
            return this;
        }
        public AboutMeSetupRecordPage ValidateHidePleaseDoAndPleaseDoNotSectionSelectedOption(bool option)
        {
            WaitForElement(hidePleaseDoAndPleaseDoNot_YesOption);
            WaitForElement(hidePleaseDoAndPleaseDoNot_NoOption);
            if (option)
            {
                ValidateElementChecked(hidePleaseDoAndPleaseDoNot_YesOption);
                ValidateElementNotChecked(hidePleaseDoAndPleaseDoNot_NoOption);
            }
            else
            {
                ValidateElementChecked(hidePleaseDoAndPleaseDoNot_NoOption);
                ValidateElementNotChecked(hidePleaseDoAndPleaseDoNot_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHideMyWellnessSectionOption(bool option)
        {
            WaitForElement(hideMyWellness_YesOption);
            WaitForElement(hideMyWellness_NoOption);
            if (option)
            {
                Click(hideMyWellness_YesOption);
            }
            else
            {
                Click(hideMyWellness_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHideMyWellnessSectionSelectedOption(bool option)
        {
            WaitForElement(hideMyWellness_YesOption);
            WaitForElement(hideMyWellness_NoOption);
            if (option)
            {
                ValidateElementChecked(hideMyWellness_YesOption);
                ValidateElementNotChecked(hideMyWellness_NoOption);
            }
            else
            {
                ValidateElementChecked(hideMyWellness_NoOption);
                ValidateElementNotChecked(hideMyWellness_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHideHowAndWhenToSupportMeOption(bool option)
        {
            WaitForElement(hideHowAndWhenToSupport_YesOption);
            WaitForElement(hideHowAndWhenToSupport_NoOption);
            if (option)
            {
                Click(hideHowAndWhenToSupport_YesOption);
            }
            else
            {
                Click(hideHowAndWhenToSupport_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHideHowAndWhenToSupportMeSectionSelectedOption(bool option)
        {
            WaitForElement(hideHowAndWhenToSupport_YesOption);
            WaitForElement(hideHowAndWhenToSupport_NoOption);
            if (option)
            {
                ValidateElementChecked(hideHowAndWhenToSupport_YesOption);
                ValidateElementNotChecked(hideHowAndWhenToSupport_NoOption);
            }
            else
            {
                ValidateElementChecked(hideHowAndWhenToSupport_NoOption);
                ValidateElementNotChecked(hideHowAndWhenToSupport_YesOption);

            }
            return this;
        }


        public AboutMeSetupRecordPage SelectHideAlsoWorthKnowingAboutMeSectionOption(bool option)
        {
            WaitForElement(hideAlsoWorthKnowingAbout_YesOption);
            WaitForElement(hideAlsoWorthKnowingAbout_NoOption);
            if (option)
            {
                Click(hideAlsoWorthKnowingAbout_YesOption);
            }
            else
            {
                Click(hideAlsoWorthKnowingAbout_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHideAlsoWorthKnowingAboutMeSectionSelectedOption(bool option)
        {
            WaitForElement(hideAlsoWorthKnowingAbout_YesOption);
            WaitForElement(hideAlsoWorthKnowingAbout_NoOption);
            if (option)
            {
                ValidateElementChecked(hideAlsoWorthKnowingAbout_YesOption);
                ValidateElementNotChecked(hideAlsoWorthKnowingAbout_NoOption);
            }
            else
            {
                ValidateElementChecked(hideAlsoWorthKnowingAbout_NoOption);
                ValidateElementNotChecked(hideAlsoWorthKnowingAbout_YesOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage SelectHidePhysicalCharacteristicsSectionOption(bool option)
        {
            WaitForElement(hidePhysicalCharacteristics_YesOption);
            WaitForElement(hidePhysicalCharacteristics_NoOption);
            if (option)
            {
                Click(hidePhysicalCharacteristics_YesOption);
            }
            else
            {
                Click(hidePhysicalCharacteristics_NoOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateHidePhysicalCharacteristicsSectionSelectedOption(bool option)
        {
            WaitForElement(hidePhysicalCharacteristics_YesOption);
            WaitForElement(hidePhysicalCharacteristics_NoOption);
            if (option)
            {
                ValidateElementChecked(hidePhysicalCharacteristics_YesOption);
                ValidateElementNotChecked(hidePhysicalCharacteristics_NoOption);
            }
            else
            {
                ValidateElementChecked(hidePhysicalCharacteristics_NoOption);
                ValidateElementNotChecked(hidePhysicalCharacteristics_YesOption);

            }
            return this;
        }

        public AboutMeSetupRecordPage ValidateResponsibleTeamFieldLookup()
        {
            WaitForElement(responsibleUser_Lookup);
            ValidateElementDisabled(responsibleUser_Lookup);
            return this;
        }

        public AboutMeSetupRecordPage ValidateFieldsAreDisabled()
        {
            WaitForElement(responsibleUser_Lookup);
            WaitForElement(enableMediaContent_YesOption);
            WaitForElement(enableMediaContent_NoOption);
            WaitForElement(status_Picklist);
            WaitForElement(aboutMeTextarea);

            ValidateElementDisabled(responsibleUser_Lookup);
            ValidateElementDisabled(enableMediaContent_YesOption);
            ValidateElementDisabled(enableMediaContent_NoOption);
            ValidateElementDisabled(status_Picklist);
            ValidateElementDisabled(hideAboutMe_YesOption);
            ValidateElementDisabled(hideAboutMe_NoOption);
            ValidateElementDisabled(aboutMeTextarea);
            ValidateElementDisabled(hideWhatIsMostImportant_YesOption);
            ValidateElementDisabled(hideWhatIsMostImportant_NoOption);
            ValidateElementDisabled(whatIsMostImportantTextarea);
            ValidateElementDisabled(hidePeopleWhoAreImportant_YesOption);
            ValidateElementDisabled(hidePeopleWhoAreImportant_NoOption);
            ValidateElementDisabled(peopleWhoAreImportantTextarea);
            ValidateElementDisabled(hideHowICommunicate_YesOption);
            ValidateElementDisabled(hideHowICommunicate_NoOption);
            ValidateElementDisabled(howICommunicateTextarea);
            ValidateElementDisabled(hidePleaseDoAndPleaseDoNot_YesOption);
            ValidateElementDisabled(hidePleaseDoAndPleaseDoNot_NoOption);
            ValidateElementDisabled(pleaseDoAndPleaseDoNotTextarea);
            ValidateElementDisabled(hideMyWellness_YesOption);
            ValidateElementDisabled(hideMyWellness_NoOption);
            ValidateElementDisabled(myWellnessTextarea);
            ValidateElementDisabled(hideHowAndWhenToSupport_YesOption);
            ValidateElementDisabled(hideHowAndWhenToSupport_NoOption);
            ValidateElementDisabled(howAndWhenToSupportTextarea);
            ValidateElementDisabled(hideAlsoWorthKnowingAbout_YesOption);
            ValidateElementDisabled(hideAlsoWorthKnowingAbout_NoOption);
            ValidateElementDisabled(alsoWorthKnowingAboutTextarea);
            ValidateElementDisabled(hidePhysicalCharacteristics_YesOption);
            ValidateElementDisabled(hidePhysicalCharacteristics_NoOption);
            ValidateElementDisabled(physicalCharacteristicsTextarea);
            return this;
        }

        public AboutMeSetupRecordPage ValidateRecordActiveStatus(string expectedActiveStatus)
        {
            WaitForElement(recordActiveStatusYesNo);
            Assert.AreEqual(expectedActiveStatus, GetElementText(recordActiveStatusYesNo));
            return this;
        }

        #endregion
    }
}
