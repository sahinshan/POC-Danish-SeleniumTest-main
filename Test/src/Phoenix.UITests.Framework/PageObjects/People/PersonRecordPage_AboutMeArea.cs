
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordPage_AboutMeArea : CommonMethods
    {
        public PersonRecordPage_AboutMeArea(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By personAboutMeTabIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By aboutMeAreaIFrame = By.XPath("//*[@id='CWFrame']");
        readonly By widgetIframe = By.XPath("//iframe[contains(@src,'HTML_about_me.html?type=person')]");

        readonly By errorMessageText = By.XPath("//*[@class='cd-snackbar-container']");
        readonly By errorMessageCloseButton = By.XPath("//*[@class='cd-snackbar-container']/*/button");


        #region About Me tab and options
        readonly By aboutMeSectionTab = By.XPath("//li[contains(@id,'CWTab_')]/a[@title = 'About Me']");
        readonly By aboutMePageHeader = By.XPath("//h1[text() = 'About Me']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndArchiveButton = By.Id("TI_SaveAndArchiveButton");
        readonly By LoadingImage = By.XPath("//*[@class = 'loader']");
        readonly By errorMessage = By.XPath("//div[@id = 'AM-content-holder']/div");
        #endregion

        #region General section
        readonly By generalSectionTitle = By.XPath("//span[text() = 'General']");

        readonly By date_CalendarIcon = By.XPath("//*[@id='AM-select-date-date-picker']");
        readonly By date_Field = By.Id("AM-select-date");
        readonly By date_ErrorLabel = By.XPath("//*[@id='AM-date-anchor']/label[@class='formerror']/span");

        //readonly By responsibleUser_Field = By.Id("responsibleUser_Link");
        //readonly By responsibleUser_Lookup = By.Id("CWLookupBtn_responsibleUser");
        //readonly By responsibleUser_RemoveButton = By.Id("CWClearLookup_responsibleUser");
        //readonly By responsibleUser_ErrorLabel = By.XPath("//*[@id='responsibleUser_Container']/label[@class='formerror']/span");

        readonly By responsibleTeam_Field = By.Id("responsibleTeam_Link");
        readonly By responsibleTeam_Lookup = By.Id("CWLookupBtn_responsibleTeam");
        readonly By responsibleTeam_RemoveButton = By.Id("CWClearLookup_responsibleTeam");
        readonly By responsibleTeam_ErrorLabel = By.XPath("//*[@id='responsibleTeam_Container']/label[@class='formerror']/span");

        readonly By supportedToWriteThisBy_Field = By.Id("supportedBy_Link");
        readonly By supportedToWriteThisBy_Lookup = By.Id("CWLookupBtn_supportedBy");
        readonly By supportedToWriteThisBy_RemoveButton = By.Id("CWClearLookup_supportedBy");
        readonly By supportedToWriteThisBy_ErrorLabel = By.XPath("//*[@id='supportedBy_Container']/label[@class='formerror']/span");

        readonly By capacityEstablished_YesOption = By.Id("AM-capacity-established-yes");
        readonly By capacityEstablished_NoOption = By.Id("AM-capacity-established-no");
        readonly By consentGrantedForRecordingsLabel = By.XPath("//label[@for = 'AM-consent-granted-for-recordings']");
        readonly By consentGrantedForRecordings_YesOption = By.Id("AM-consent-granted-for-recordings-yes");
        readonly By consentGrantedForRecordings_NoOption = By.Id("AM-consent-granted-for-recordings-no");
        readonly By CollapseExpandButton = By.XPath("//*[@id='AM-content-header']/div/div/div[contains(@class, 'AM-collapse')]");
        #endregion

        #region About Me section
        readonly By aboutMeSection = By.XPath("//*[@id = 'aboutme']");
        readonly By aboutMeSectionTitle = By.XPath("//span[text() = 'About Me']");
        readonly By aboutMeInformationLabel = By.XPath("//label[@for = 'aboutme']");
        readonly By aboutMeInformationField = By.XPath("//label[@for = 'aboutme']/../div/div/div[@class = 'markdown']");
        readonly By aboutMeResponseLabel = By.XPath("//label[@for = 'aboutme-response']");
        readonly By aboutMeMediaLabel = By.XPath("//label[@for = 'aboutme-media']");
        readonly By aboutMeResponse_Field = By.Id("aboutme-response");
        readonly By aboutMeMedia_Upload = By.XPath("//*[@for = 'aboutme-media']/div");
        readonly By aboutMeMediaUploadBtn = By.Id("aboutme-media");
        readonly By aboutMeMediaLabelOverlay = By.XPath("//div[@class='media-ui']/label[@for='aboutme-media']");

        readonly By aboutMeMediaVideo = By.Id("aboutme-media-video");
        readonly By aboutMeMediaDeleteButton = By.Id("aboutme-media-remove");
        #endregion

        #region What is most important to Me section
        readonly By whatIsMostImportantToMeSection = By.XPath("//*[@id = 'whatismostimportanttome']");
        readonly By whatIsMostImportantToMeSectionTitle = By.XPath("//span[text() = 'What Is Most Important To Me']");
        readonly By whatIsMostImportantToMeInformationLabel = By.XPath("//label[@for = 'whatismostimportanttome']");
        readonly By whatIsMostImportantToMeInformationField = By.XPath("//label[@for = 'whatismostimportanttome']/../div/div/div[@class = 'markdown']");
        readonly By whatIsMostImportantToMeResponseLabel = By.XPath("//label[@for = 'whatismostimportanttome-response']");
        readonly By whatIsMostImportantToMeMediaLabel = By.XPath("//label[@for = 'whatismostimportanttome-media']");
        readonly By whatIsMostImportantToMeResponse_Field = By.Id("whatismostimportanttome-response");
        readonly By whatIsMostImportantToMeMedia_Upload = By.XPath("//*[@for = 'whatismostimportanttome-media']/div");
        readonly By whatIsMostImportantToMeMediaUploadBtn = By.Id("whatismostimportanttome-media");

        readonly By whatIsMostImportantToMeMediaVideo = By.Id("whatismostimportanttome-media-video");
        readonly By whatIsMostImportantToMeMediaDeleteButton = By.Id("whatismostimportanttome-media-remove");
        #endregion

        #region People Who Are Important To Me section
        readonly By peopleWhoAreImportantToMeSection = By.XPath("//*[@id = 'peoplewhoareimportanttome']");
        readonly By peopleWhoAreImportantToMeSectionTitle = By.XPath("//span[text() = 'People Who Are Important To Me section']");
        readonly By peopleWhoAreImportantToMeInformationLabel = By.XPath("//label[@for = 'peoplewhoareimportanttome']");
        readonly By peopleWhoAreImportantToMeInformationField = By.XPath("//label[@for = 'peoplewhoareimportanttome']/../div/div/div[@class = 'markdown']");
        readonly By peopleWhoAreImportantToMeResponseLabel = By.XPath("//label[@for = 'peoplewhoareimportanttome-response']");
        readonly By peopleWhoAreImportantToMeMediaLabel = By.XPath("//label[@for = 'peoplewhoareimportanttome-media']");
        readonly By peopleWhoAreImportantToMeResponse_Field = By.Id("peoplewhoareimportanttome-response");
        readonly By peopleWhoAreImportantToMeMedia_Upload = By.XPath("//*[@for = 'peoplewhoareimportanttome-media']/div");
        readonly By peopleWhoAreImportantToMeMediaUploadBtn = By.Id("peoplewhoareimportanttome-media");

        readonly By peopleWhoAreImportantToMeMediaVideo = By.Id("peoplewhoareimportanttome-media-video");
        #endregion

        #region How I Communicate And How To Communicate With Me section
        readonly By howICommunicateAndHowToCommunicateWithMeSection = By.XPath("//*[@id = 'howicommunicateandhowtocommunicatewithme']");
        readonly By howICommunicateAndHowToCommunicateWithMeSectionTitle = By.XPath("//span[text() = 'How I Communicate And How To Communicate With Me']");
        readonly By howICommunicateAndHowToCommunicateWithMeInformationLabel = By.XPath("//label[@for = 'howicommunicateandhowtocommunicatewithme']");
        readonly By howICommunicateAndHowToCommunicateWithMeInformationField = By.XPath("//label[@for = 'howicommunicateandhowtocommunicatewithme']/../div/div/div[@class = 'markdown']");
        readonly By howICommunicateAndHowToCommunicateWithMeResponseLabel = By.XPath("//label[@for = 'howicommunicateandhowtocommunicatewithme-response']");
        readonly By howICommunicateAndHowToCommunicateWithMeMediaLabel = By.XPath("//label[@for = 'howicommunicateandhowtocommunicatewithme-media']");
        readonly By howICommunicateAndHowToCommunicateWithMeResponse_Field = By.Id("howicommunicateandhowtocommunicatewithme-response");
        readonly By howICommunicateAndHowToCommunicateWithMeMedia_Upload = By.XPath("//*[@for = 'howicommunicateandhowtocommunicatewithme-media']/div");
        readonly By howICommunicateAndHowToCommunicateWithMeMediaUploadBtn = By.Id("howicommunicateandhowtocommunicatewithme-media");

        readonly By howICommunicateAndHowToCommunicateWithMeMediaVideo = By.Id("howicommunicateandhowtocommunicatewithme-media-video");
        #endregion

        #region Please Do And Please Do Not section
        readonly By pleasedoandpleasedonotSection = By.XPath("//*[@id = 'pleasedoandpleasedonot']");
        readonly By pleasedoandpleasedonotSectionTitle = By.XPath("//span[text() = 'Please Do And Please Do Not']");
        readonly By pleasedoandpleasedonotInformationLabel = By.XPath("//label[@for = 'pleasedoandpleasedonot']");
        readonly By pleasedoandpleasedonotInformationField = By.XPath("//label[@for = 'pleasedoandpleasedonot']/../div/div/div[@class = 'markdown']");
        readonly By pleasedoandpleasedonotResponseLabel = By.XPath("//label[@for = 'pleasedoandpleasedonot-response']");
        readonly By pleasedoandpleasedonotMediaLabel = By.XPath("//label[@for = 'pleasedoandpleasedonot-media']");
        readonly By pleasedoandpleasedonotResponse_Field = By.Id("pleasedoandpleasedonot-response");
        readonly By pleasedoandpleasedonotMedia_Upload = By.XPath("//*[@for = 'pleasedoandpleasedonot-media']/div");
        readonly By pleasedoandpleasedonotMediaUploadBtn = By.Id("pleasedoandpleasedonot-media");

        readonly By pleasedoandpleasedonotMediaVideo = By.Id("pleasedoandpleasedonot-media-video");
        #endregion

        #region My Wellness section
        readonly By myWellnessSection = By.XPath("//*[@id = 'mywellness']");
        readonly By myWellnessSectionTitle = By.XPath("//span[text() = 'My Wellness']");
        readonly By myWellnessInformationLabel = By.XPath("//label[@for = 'mywellness']");
        readonly By myWellnessInformationField = By.XPath("//label[@for = 'mywellness']/../div/div/div[@class = 'markdown']");
        readonly By myWellnessResponseLabel = By.XPath("//label[@for = 'mywellness-response']");
        readonly By myWellnessMediaLabel = By.XPath("//label[@for = 'mywellness-media']");
        readonly By myWellnessResponse_Field = By.Id("mywellness-response");
        readonly By myWellnessMedia_Upload = By.XPath("//*[@for = 'mywellness-media']/div");
        readonly By myWellnessMediaUploadBtn = By.Id("mywellness-media");

        readonly By myWellnessMediaVideo = By.Id("mywellness-media-video");
        #endregion

        #region How And When To Support Me section
        readonly By howandwhentosupportmeSection = By.XPath("//*[@id = 'howandwhentosupportme']");
        readonly By howandwhentosupportmeSectionTitle = By.XPath("//span[text() = 'How And When To Support Me']");
        readonly By howandwhentosupportmeInformationLabel = By.XPath("//label[@for = 'howandwhentosupportme']");
        readonly By howandwhentosupportmeInformationField = By.XPath("//label[@for = 'howandwhentosupportme']/../div/div/div[@class = 'markdown']");
        readonly By howandwhentosupportmeResponseLabel = By.XPath("//label[@for = 'howandwhentosupportme-response']");
        readonly By howandwhentosupportmeMediaLabel = By.XPath("//label[@for = 'howandwhentosupportme-media']");
        readonly By howandwhentosupportmeResponse_Field = By.Id("howandwhentosupportme-response");
        readonly By howandwhentosupportmeMedia_Upload = By.XPath("//*[@for = 'howandwhentosupportme-media']/div");
        readonly By howandwhentosupportmeMediaUploadBtn = By.Id("howandwhentosupportme-media");

        readonly By howandwhentosupportmeMediaVideo = By.Id("howandwhentosupportme-media-video");
        #endregion

        #region Also Worth Knowing About Me section
        readonly By alsoWorthKnowingAboutMeSection = By.XPath("//*[@id = 'alsoworthknowingaboutme']");
        readonly By alsoWorthKnowingAboutMeSectionTitle = By.XPath("//span[text() = 'Also Worth Knowing About Me']");
        readonly By alsoWorthKnowingAboutMeInformationLabel = By.XPath("//label[@for = 'alsoworthknowingaboutme']");
        readonly By alsoWorthKnowingAboutMeInformationField = By.XPath("//label[@for = 'alsoworthknowingaboutme']/../div/div/div[@class = 'markdown']");
        readonly By alsoWorthKnowingAboutMeResponseLabel = By.XPath("//label[@for = 'alsoworthknowingaboutme-response']");
        readonly By alsoWorthKnowingAboutMeMediaLabel = By.XPath("//label[@for = 'alsoworthknowingaboutme-media']");
        readonly By alsoWorthKnowingAboutMeResponse_Field = By.Id("alsoworthknowingaboutme-response");
        readonly By alsoWorthKnowingAboutMeMedia_Upload = By.XPath("//*[@for = 'alsoworthknowingaboutme-media']/div");
        readonly By alsoWorthKnowingAboutMeMediaUploadBtn = By.Id("alsoworthknowingaboutme-media");

        readonly By alsoWorthKnowingAboutMeMediaVideo = By.Id("alsoworthknowingaboutme-media-video");
        #endregion

        #region Physical Characteristics section
        readonly By physicalCharacteristicsSection = By.XPath("//*[@id = 'physicalcharacteristics']");
        readonly By physicalCharacteristicsSectionTitle = By.XPath("//span[text() = 'Physical Characteristics']");
        readonly By physicalCharacteristicsInformationLabel = By.XPath("//label[@for = 'physicalcharacteristics']");
        readonly By physicalCharacteristicsInformationField = By.XPath("//label[@for = 'physicalcharacteristics']/../div/div/div[@class = 'markdown']");
        readonly By physicalCharacteristicsResponseLabel = By.XPath("//label[@for = 'physicalcharacteristics-response']");
        readonly By physicalCharacteristicsMediaLabel = By.XPath("//label[@for = 'physicalcharacteristics-media']");
        readonly By physicalCharacteristicsResponse_Field = By.Id("physicalcharacteristics-response");
        readonly By physicalCharacteristicsMedia_Upload = By.XPath("//*[@for = 'physicalcharacteristics-media']/div");
        readonly By physicalCharacteristicsMediaUploadBtn = By.Id("physicalcharacteristics-media");

        readonly By physicalCharacteristicsMediaVideo = By.Id("physicalcharacteristics-media-video");
        #endregion

        readonly By headinglevel1 = By.XPath("//label[@for = 'aboutme']/../div/descendant::h1");
        readonly By headinglevel2 = By.XPath("//label[@for = 'whatismostimportanttome']/../div/descendant::h2");
        readonly By paragraphText = By.XPath("//label[@for = 'peoplewhoareimportanttome']/../div/descendant::p");
        readonly By boldText = By.XPath("//label[@for = 'howicommunicateandhowtocommunicatewithme']/../div/descendant::strong");
        readonly By italicizedText = By.XPath("//label[@for = 'pleasedoandpleasedonot']/../div/descendant::em");
        readonly By orderedListText = By.XPath("//label[@for = 'mywellness']/../div/descendant::ol/li");
        readonly By unorderedListText = By.XPath("//label[@for = 'howandwhentosupportme']/../div/descendant::ul/li");
        readonly By lineBreak = By.XPath("//label[@for = 'alsoworthknowingaboutme']/../div/descendant::p");

        public PersonRecordPage_AboutMeArea WaitForPersonRecordPage_AboutMeAreaToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementNotVisible(LoadingImage, 60);

            WaitForElement(personAboutMeTabIFrame);
            SwitchToIframe(personAboutMeTabIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(aboutMeSectionTab);

            WaitForElement(aboutMeAreaIFrame);
            SwitchToIframe(aboutMeAreaIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(aboutMePageHeader);
            WaitForElement(saveButton);
            WaitForElement(saveAndArchiveButton);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateGeneralErrorMessageVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(errorMessageText);
                WaitForElementVisible(errorMessageCloseButton);
            }
            else
            {

                WaitForElementNotVisible(errorMessageText, 3);
                WaitForElementNotVisible(errorMessageCloseButton, 3);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateGeneralErrorMessageText(string ExpectedText)
        {

            ValidateElementTextContainsText(errorMessageText, ExpectedText);

            return this;
        }

        #region General sections fields actions

        public PersonRecordPage_AboutMeArea ClickGeneralSectionCollapseExpandButton()
        {
            WaitForElementToBeClickable(CollapseExpandButton);
            Click(CollapseExpandButton);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateGeneralAreaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(date_Field);
                //WaitForElementVisible(responsibleUser_Lookup);
                WaitForElementVisible(responsibleTeam_Lookup);

                WaitForElementVisible(supportedToWriteThisBy_Lookup);
                WaitForElementVisible(capacityEstablished_YesOption);
                WaitForElementVisible(capacityEstablished_NoOption);
                WaitForElementVisible(consentGrantedForRecordings_YesOption);
                WaitForElementVisible(consentGrantedForRecordings_NoOption);
            }
            else
            {
                WaitForElementNotVisible(date_Field, 3);
                //WaitForElementNotVisible(responsibleUser_Lookup, 3);
                WaitForElementNotVisible(responsibleTeam_Lookup, 3);

                WaitForElementNotVisible(supportedToWriteThisBy_Lookup, 3);
                WaitForElementNotVisible(capacityEstablished_YesOption, 3);
                WaitForElementNotVisible(capacityEstablished_NoOption, 3);
                WaitForElementNotVisible(consentGrantedForRecordings_YesOption, 3);
                WaitForElementNotVisible(consentGrantedForRecordings_NoOption, 3);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ClickOnDateCalendarIcon()
        {
            WaitForElement(date_CalendarIcon);

            Click(date_CalendarIcon);

            return this;
        }
        public PersonRecordPage_AboutMeArea InsertDate()
        {
            WaitForElement(date_Field);

            SendKeys(date_Field, DateTime.Now.ToString("dd'/'MM'/'yyyy"));
            SendKeysWithoutClearing(date_Field, Keys.Enter);


            return this;
        }

        public PersonRecordPage_AboutMeArea InsertDate(string dateValue)
        {
            WaitForElement(date_Field);
            Click(date_Field);
            ClearInputElementViaJavascript("AM-select-date");
            SendKeys(date_Field, dateValue + Keys.Tab);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateDate(string dateValue)
        {
            WaitForElement(date_Field);
            string actualDateValue = GetElementValueByJavascript("AM-select-date");
            Assert.AreEqual(dateValue, actualDateValue);

            return this;
        }

        public string GetDate()
        {
            WaitForElement(date_Field);
            string actualDateValue = GetElementValueByJavascript("AM-select-date");

            return actualDateValue;
        }

        public PersonRecordPage_AboutMeArea ClickResponsibleTeamLookupButton()
        {
            WaitForElement(responsibleTeam_Lookup);
            ScrollToElement(responsibleTeam_Lookup);
            Click(responsibleTeam_Lookup);

            return this;
        }

        public PersonRecordPage_AboutMeArea ClickResponsibleTeamRemoveButton()
        {
            WaitForElement(responsibleTeam_RemoveButton);
            Click(responsibleTeam_RemoveButton);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateResponsibleTeamField(string expectedText)
        {
            WaitForElement(responsibleTeam_Field);
            ValidateElementText(responsibleTeam_Field, expectedText);

            return this;
        }

        public string GetResponsibleTeam()
        {
            WaitForElement(responsibleTeam_Field);
            string actualValue = GetElementByAttributeValue(responsibleTeam_Field, "title");


            return actualValue;
        }

        public PersonRecordPage_AboutMeArea ClickSupportedToWriteThisByLookupButton()
        {
            WaitForElement(supportedToWriteThisBy_Lookup);
            ScrollToElement(supportedToWriteThisBy_Lookup);
            Click(supportedToWriteThisBy_Lookup);

            return this;
        }
        public PersonRecordPage_AboutMeArea ClickSupportedToWriteThisByRemoveButton()
        {
            WaitForElement(supportedToWriteThisBy_RemoveButton);
            ScrollToElement(supportedToWriteThisBy_RemoveButton);
            Click(supportedToWriteThisBy_RemoveButton);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateSupportedToWriteThisByField(string expectedText)
        {
            WaitForElement(supportedToWriteThisBy_Field);
            ValidateElementText(supportedToWriteThisBy_Field, expectedText);

            return this;
        }

        public string GetSupportedToWriteThisBy()
        {
            WaitForElement(supportedToWriteThisBy_Field);
            string actualValue = GetElementByAttributeValue(supportedToWriteThisBy_Field, "title");


            return actualValue;
        }

        public PersonRecordPage_AboutMeArea SelectCapacityEstablisedOption(bool state)
        {
            WaitForElementToBeClickable(capacityEstablished_YesOption);
            WaitForElementToBeClickable(capacityEstablished_NoOption);

            if (state.Equals(true))
            {
                Click(capacityEstablished_YesOption);
            }
            else
            {
                Click(capacityEstablished_NoOption);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateCapacityEstablishedOption(bool status)
        {
            WaitForElementVisible(capacityEstablished_YesOption);
            WaitForElementVisible(capacityEstablished_NoOption);

            if (status.Equals(true))
            {
                ValidateElementChecked(capacityEstablished_YesOption);
                ValidateElementNotChecked(capacityEstablished_NoOption);
            }
            else
            {
                ValidateElementNotChecked(capacityEstablished_YesOption);
                ValidateElementChecked(capacityEstablished_NoOption);
            }

            return this;
        }

        public string GetCapacityEstablishedSelectedOption()
        {
            WaitForElement(capacityEstablished_YesOption);
            WaitForElement(capacityEstablished_NoOption);
            string actualSelectedVal = null;

            if (GetElementByAttributeValue(capacityEstablished_YesOption, "checked")?.Any() == null && GetElementByAttributeValue(capacityEstablished_NoOption, "checked").Equals("true"))
            {
                actualSelectedVal = "No";
            }
            else
            if (GetElementByAttributeValue(capacityEstablished_NoOption, "checked")?.Any() == null && GetElementByAttributeValue(capacityEstablished_YesOption, "checked").Equals("true"))
            {
                actualSelectedVal = "Yes";
            }
            return actualSelectedVal;
        }

        public PersonRecordPage_AboutMeArea SelectConsentGrantedForRecordingOption(bool state)
        {
            WaitForElement(consentGrantedForRecordings_YesOption);
            WaitForElement(consentGrantedForRecordings_NoOption);

            if (state.Equals(true))
            {
                Click(consentGrantedForRecordings_YesOption);
            }
            else
            {
                Click(consentGrantedForRecordings_NoOption);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateConsentGrantedForRecordingOption(bool status)
        {
            WaitForElementVisible(consentGrantedForRecordings_YesOption);
            WaitForElementVisible(consentGrantedForRecordings_NoOption);

            if (status.Equals(true))
            {
                ValidateElementChecked(consentGrantedForRecordings_YesOption);
                ValidateElementNotChecked(consentGrantedForRecordings_NoOption);
            }
            else
            {
                ValidateElementNotChecked(consentGrantedForRecordings_YesOption);
                ValidateElementChecked(consentGrantedForRecordings_NoOption);
            }

            return this;
        }

        public string GetConsentGrantedSelectedOption()
        {
            WaitForElement(consentGrantedForRecordings_YesOption);
            WaitForElement(consentGrantedForRecordings_NoOption);
            string actualSelectedVal = null;
            if (GetElementByAttributeValue(consentGrantedForRecordings_YesOption, "checked")?.Any() == null && GetElementByAttributeValue(consentGrantedForRecordings_NoOption, "checked").Equals("true"))
            {
                actualSelectedVal = "No";
            }
            else
            if (GetElementByAttributeValue(consentGrantedForRecordings_YesOption, "checked")?.Any() == null && GetElementByAttributeValue(consentGrantedForRecordings_NoOption, "checked").Equals("true"))
            {
                actualSelectedVal = "Yes";
            }
            return actualSelectedVal;
        }

        public PersonRecordPage_AboutMeArea ValidateConsentGrantedForRecordingFieldPresent(bool status)
        {
            if (status.Equals(true))
            {
                WaitForElementVisible(consentGrantedForRecordingsLabel);
                WaitForElementVisible(consentGrantedForRecordings_YesOption);
                WaitForElementVisible(consentGrantedForRecordings_NoOption);
                Assert.IsTrue(GetElementVisibility(consentGrantedForRecordingsLabel));
                ValidateElementNotChecked(consentGrantedForRecordings_YesOption);
                ValidateElementChecked(consentGrantedForRecordings_NoOption);
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(consentGrantedForRecordingsLabel));
                Assert.IsFalse(GetElementVisibility(consentGrantedForRecordings_YesOption));
                Assert.IsFalse(GetElementVisibility(consentGrantedForRecordings_NoOption));

            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateDateFieldDisabled(bool ExpectDisabled)
        {
            WaitForElement(date_Field);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(date_Field);
            }
            else
            {
                ValidateElementNotDisabled(date_Field);
            }

            return this;
        }

        //public PersonRecordPage_AboutMeArea ValidateResponsibleUserFieldDisabled(bool ExpectDisabled)
        //{
        //    WaitForElement(responsibleUser_Lookup);

        //    if (ExpectDisabled)
        //    {
        //        ValidateElementDisabled(responsibleUser_Lookup);
        //    }
        //    else
        //    {
        //        ValidateElementNotDisabled(responsibleUser_Lookup);
        //    }

        //    return this;
        //}

        public PersonRecordPage_AboutMeArea ValidateResponsibleTeamFieldDisabled(bool ExpectDisabled)
        {
            WaitForElement(responsibleTeam_Lookup);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(responsibleTeam_Lookup);
            }
            else
            {
                ValidateElementNotDisabled(responsibleTeam_Lookup);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateSupportedToWriteThisByFieldDisabled(bool ExpectDisabled)
        {
            WaitForElement(supportedToWriteThisBy_Lookup);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(supportedToWriteThisBy_Lookup);
            }
            else
            {
                ValidateElementNotDisabled(supportedToWriteThisBy_Lookup);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateCapacityEstablishedFieldDisabled(bool ExpectDisabled)
        {
            WaitForElement(capacityEstablished_YesOption);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(capacityEstablished_YesOption);
                ValidateElementDisabled(capacityEstablished_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(capacityEstablished_YesOption);
                ValidateElementNotDisabled(capacityEstablished_NoOption);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateConsentGrantedForRecordingsFieldDisabled(bool ExpectDisabled)
        {
            WaitForElement(consentGrantedForRecordings_YesOption);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(consentGrantedForRecordings_YesOption);
                ValidateElementDisabled(consentGrantedForRecordings_NoOption);
            }
            else
            {
                ValidateElementNotDisabled(consentGrantedForRecordings_YesOption);
                ValidateElementNotDisabled(consentGrantedForRecordings_NoOption);
            }

            return this;
        }


        public PersonRecordPage_AboutMeArea ValidateDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(date_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(date_ErrorLabel, 3);
            }

            return this;
        }
        //public PersonRecordPage_AboutMeArea ValidateResponsibleUserErrorLabelVisibility(bool ExpectVisible)
        //{
        //    if (ExpectVisible)
        //    {
        //        WaitForElementVisible(responsibleUser_ErrorLabel);
        //    }
        //    else
        //    {
        //        WaitForElementNotVisible(responsibleUser_ErrorLabel, 3);
        //    }

        //    return this;
        //}
        public PersonRecordPage_AboutMeArea ValidateResponsibleTeamErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(responsibleTeam_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(responsibleTeam_ErrorLabel, 3);
            }

            return this;
        }
        public PersonRecordPage_AboutMeArea ValidateSupportedToWriteThisByErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(supportedToWriteThisBy_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(supportedToWriteThisBy_ErrorLabel, 3);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(date_ErrorLabel, ExpectedText);

            return this;
        }
        //public PersonRecordPage_AboutMeArea ValidateResponsibleUserErrorLabelText(string ExpectedText)
        //{
        //    ValidateElementText(responsibleUser_ErrorLabel, ExpectedText);

        //    return this;
        //}
        public PersonRecordPage_AboutMeArea ValidateResponsibleTeamErrorLabelText(string ExpectedText)
        {
            ValidateElementText(responsibleTeam_ErrorLabel, ExpectedText);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateSupportedToWriteThisByErrorLabelText(string ExpectedText)
        {
            ValidateElementText(supportedToWriteThisBy_ErrorLabel, ExpectedText);

            return this;
        }

        #endregion

        #region About Me section fields actions
        public PersonRecordPage_AboutMeArea InsertAboutMe_MyStory(string textToInsert)
        {
            WaitForElement(aboutMeResponse_Field);
            SendKeys(aboutMeResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMe_MyStory(string expectedText)
        {
            WaitForElement(aboutMeResponse_Field);
            string response = GetElementText(aboutMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMe_Information(string expectedText)
        {
            WaitForElement(aboutMeInformationField);

            Assert.AreEqual(expectedText, GetElementText(aboutMeInformationField));
            return this;
        }

        public PersonRecordPage_AboutMeArea UploadAboutMeMedia(string mediaPath)
        {
            WaitForElement(aboutMeMediaUploadBtn);            
            SetElementVisibilityStyleToVisibleByElementId("aboutme-media");
            SendKeys(aboutMeMediaUploadBtn, mediaPath);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeMediaReadOnly(bool ExpectReadonly)
        {
            WaitForElement(aboutMeMediaLabelOverlay);
            var readonlyValue = this.GetElementByAttributeValue(aboutMeMediaLabelOverlay, "readonly");

            if (ExpectReadonly)
            {
                if (string.IsNullOrEmpty(readonlyValue))
                {
                    throw new Exception("readonly property is not set");
                }
                if (!readonlyValue.Equals("true"))
                {
                    throw new Exception("readonly property is not set");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(readonlyValue) && readonlyValue.Equals("true"))
                {
                    throw new Exception("readonly property is set");
                }
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeMediaUploadField(bool isPresent)
        {

            if (isPresent)
            {
                WaitForElement(aboutMeMedia_Upload);
                ScrollToElement(aboutMeMedia_Upload);
                Assert.IsTrue(GetElementVisibility(aboutMeMedia_Upload));
            }
            else
            {
                WaitForElementNotVisible(aboutMeMedia_Upload, 3);
                Assert.IsFalse(GetElementVisibility(aboutMeMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeMediaPresent(bool isPresent)
        {
            string response = "";

            if (isPresent)
            {
                WaitForElement(aboutMeMediaVideo);
                ScrollToElement(aboutMeMediaVideo);
                response = GetElementByAttributeValue(aboutMeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(aboutMeMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(aboutMeInformationLabel);
            WaitForElement(aboutMeResponseLabel);
            WaitForElement(aboutMeMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(aboutMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(aboutMeResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(aboutMeMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(aboutMeInformationLabel);
            WaitForElement(aboutMeResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(aboutMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(aboutMeResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(aboutMeInformationField);
                Assert.IsTrue(GetElementVisibility(aboutMeInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(aboutMeInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAboutMeResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(aboutMeResponse_Field);
                Assert.IsTrue(GetElementVisibility(aboutMeResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(aboutMeResponse_Field));
            }
            return this;
        }


        public PersonRecordPage_AboutMeArea ClickAboutMeMediaDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 80);
            WaitForElementToBeClickable(aboutMeMediaDeleteButton);
            Click(aboutMeMediaDeleteButton);

            return this;
        }

        #endregion

        #region What Is Most Important To Me fields actions
        public PersonRecordPage_AboutMeArea InsertWhatIsMostImportant_MyStory(string textToInsert)
        {
            WaitForElement(whatIsMostImportantToMeResponse_Field);
            SendKeys(whatIsMostImportantToMeResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportant_MyStory(string expectedText)
        {
            WaitForElement(whatIsMostImportantToMeResponse_Field);
            string response = GetElementText(whatIsMostImportantToMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }


        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantMediaUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(whatIsMostImportantToMeMedia_Upload);
                ScrollToElement(whatIsMostImportantToMeMedia_Upload);
                Assert.IsTrue(GetElementVisibility(whatIsMostImportantToMeMedia_Upload));

            }
            else
            {
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantToMeMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(whatIsMostImportantToMeMediaVideo);
                string response = GetElementByAttributeValue(whatIsMostImportantToMeMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantToMeMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantToMeSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(whatIsMostImportantToMeInformationLabel);
            WaitForElement(whatIsMostImportantToMeResponseLabel);
            WaitForElement(whatIsMostImportantToMeMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(whatIsMostImportantToMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(whatIsMostImportantToMeResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(whatIsMostImportantToMeMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantToMeSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(whatIsMostImportantToMeInformationLabel);
            WaitForElement(whatIsMostImportantToMeResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(whatIsMostImportantToMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(whatIsMostImportantToMeResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantToMeGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(whatIsMostImportantToMeInformationField);
                Assert.IsTrue(GetElementVisibility(whatIsMostImportantToMeInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantToMeInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantToMeResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(whatIsMostImportantToMeResponse_Field);
                Assert.IsTrue(GetElementVisibility(whatIsMostImportantToMeResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantToMeResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateWhatIsMostImportantToMe_Information(string expectedText)
        {
            WaitForElement(whatIsMostImportantToMeInformationField);

            Assert.AreEqual(expectedText, GetElementText(whatIsMostImportantToMeInformationField));

            return this;
        }

        public PersonRecordPage_AboutMeArea UploadWhatIsMostImportantToMeMedia(string mediaPath)
        {
            WaitForElement(whatIsMostImportantToMeMediaUploadBtn);
            SetElementVisibilityStyleToVisibleByElementId("whatismostimportanttome-media");
            SendKeys(whatIsMostImportantToMeMediaUploadBtn, mediaPath);

            return this;
        }

        public PersonRecordPage_AboutMeArea ClickWhatIsMostImportantToMeMediaDeleteButton()
        {
            WaitForElementToBeClickable(whatIsMostImportantToMeMediaDeleteButton);
            ScrollToElement(whatIsMostImportantToMeMediaDeleteButton);
            Click(whatIsMostImportantToMeMediaDeleteButton);

            return this;
        }
        #endregion

        #region People Who Are Important To Me fields actions
        public PersonRecordPage_AboutMeArea InsertPeopleWhoAreImportant_MyStory(string textToInsert)
        {
            WaitForElement(peopleWhoAreImportantToMeResponse_Field);
            SendKeys(peopleWhoAreImportantToMeResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportant_MyStory(string expectedText)
        {
            WaitForElement(peopleWhoAreImportantToMeResponse_Field);
            string response = GetElementText(peopleWhoAreImportantToMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantMediaUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(peopleWhoAreImportantToMeMedia_Upload);
                ScrollToElement(peopleWhoAreImportantToMeMedia_Upload);
                Assert.IsTrue(GetElementVisibility(peopleWhoAreImportantToMeMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantToMeMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(peopleWhoAreImportantToMeMediaVideo);
                string response = GetElementByAttributeValue(peopleWhoAreImportantToMeMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantToMeMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantToMeSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(peopleWhoAreImportantToMeInformationLabel);
            WaitForElement(peopleWhoAreImportantToMeResponseLabel);
            WaitForElement(peopleWhoAreImportantToMeMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(peopleWhoAreImportantToMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(peopleWhoAreImportantToMeResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(peopleWhoAreImportantToMeMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantToMeSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(peopleWhoAreImportantToMeInformationLabel);
            WaitForElement(peopleWhoAreImportantToMeResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(peopleWhoAreImportantToMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(peopleWhoAreImportantToMeResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantToMeGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(peopleWhoAreImportantToMeInformationField);
                Assert.IsTrue(GetElementVisibility(peopleWhoAreImportantToMeInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantToMeInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantToMeResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(peopleWhoAreImportantToMeResponse_Field);
                Assert.IsTrue(GetElementVisibility(peopleWhoAreImportantToMeResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantToMeResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePeopleWhoAreImportantToMe_Information(string expectedText)
        {
            WaitForElement(peopleWhoAreImportantToMeInformationField);

            Assert.AreEqual(expectedText, GetElementText(peopleWhoAreImportantToMeInformationField));
            return this;
        }

        #endregion

        #region How I Communicate And How To Communicate With Me field actions
        public PersonRecordPage_AboutMeArea InsertHowICommunicate_MyStory(string textToInsert)
        {
            WaitForElement(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            SendKeys(howICommunicateAndHowToCommunicateWithMeResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicate_MyStory(string expectedText)
        {
            WaitForElement(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            string response = GetElementText(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateMediaUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(howICommunicateAndHowToCommunicateWithMeMedia_Upload);
                ScrollToElement(howICommunicateAndHowToCommunicateWithMeMedia_Upload);
                Assert.IsTrue(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(howICommunicateAndHowToCommunicateWithMeMediaVideo);
                string response = GetElementByAttributeValue(howICommunicateAndHowToCommunicateWithMeMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateAndHowToCommunicateWithMeSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(howICommunicateAndHowToCommunicateWithMeInformationLabel);
            WaitForElement(howICommunicateAndHowToCommunicateWithMeResponseLabel);
            WaitForElement(howICommunicateAndHowToCommunicateWithMeMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(howICommunicateAndHowToCommunicateWithMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(howICommunicateAndHowToCommunicateWithMeResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(howICommunicateAndHowToCommunicateWithMeMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateAndHowToCommunicateWithMeSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(howICommunicateAndHowToCommunicateWithMeInformationLabel);
            WaitForElement(howICommunicateAndHowToCommunicateWithMeResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(howICommunicateAndHowToCommunicateWithMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(howICommunicateAndHowToCommunicateWithMeResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateAndHowToCommunicateWithMeGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(howICommunicateAndHowToCommunicateWithMeInformationField);
                Assert.IsTrue(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateAndHowToCommunicateWithMeResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(howICommunicateAndHowToCommunicateWithMeResponse_Field);
                Assert.IsTrue(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowICommunicateAndHowToCommunicateWithMe_Information(string expectedText)
        {
            WaitForElement(howICommunicateAndHowToCommunicateWithMeInformationField);

            Assert.AreEqual(expectedText, GetElementText(howICommunicateAndHowToCommunicateWithMeInformationField));
            return this;
        }
        #endregion

        #region Please Do And Please Do Not fields actions
        public PersonRecordPage_AboutMeArea InsertPleaseDoAndPleaseDoNot_MyStory(string textToInsert)
        {
            WaitForElement(pleasedoandpleasedonotResponse_Field);
            SendKeys(pleasedoandpleasedonotResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNot_MyStory(string expectedText)
        {
            WaitForElement(pleasedoandpleasedonotResponse_Field);
            string response = GetElementText(pleasedoandpleasedonotResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNotUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(pleasedoandpleasedonotMedia_Upload);
                ScrollToElement(pleasedoandpleasedonotMedia_Upload);
                Assert.IsTrue(GetElementVisibility(pleasedoandpleasedonotMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(pleasedoandpleasedonotMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNotMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(pleasedoandpleasedonotMediaVideo);
                string response = GetElementByAttributeValue(pleasedoandpleasedonotMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(pleasedoandpleasedonotMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNoSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(pleasedoandpleasedonotInformationLabel);
            WaitForElement(pleasedoandpleasedonotResponseLabel);
            WaitForElement(pleasedoandpleasedonotMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(pleasedoandpleasedonotInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(pleasedoandpleasedonotResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(pleasedoandpleasedonotMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNoSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(pleasedoandpleasedonotInformationLabel);
            WaitForElement(pleasedoandpleasedonotResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(pleasedoandpleasedonotInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(pleasedoandpleasedonotResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNotGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(pleasedoandpleasedonotInformationField);
                Assert.IsTrue(GetElementVisibility(pleasedoandpleasedonotInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(pleasedoandpleasedonotInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNotResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(pleasedoandpleasedonotResponse_Field);
                Assert.IsTrue(GetElementVisibility(pleasedoandpleasedonotResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(pleasedoandpleasedonotResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePleaseDoAndPleaseDoNot_Information(string expectedText)
        {
            WaitForElement(pleasedoandpleasedonotInformationField);

            Assert.AreEqual(expectedText, GetElementText(pleasedoandpleasedonotInformationField));
            return this;
        }
        #endregion


        #region My Wellness fields actions
        public PersonRecordPage_AboutMeArea InsertMyWellness_MyStory(string textToInsert)
        {
            WaitForElement(myWellnessResponse_Field);
            SendKeys(myWellnessResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateMyWellness_MyStory(string expectedText)
        {
            WaitForElement(myWellnessResponse_Field);
            string response = GetElementText(myWellnessResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateMyWellnessUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(myWellnessMedia_Upload);
                ScrollToElement(myWellnessMedia_Upload);
                Assert.IsTrue(GetElementVisibility(myWellnessMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(myWellnessMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateMyWellnessMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(myWellnessMediaVideo);
                string response = GetElementByAttributeValue(myWellnessMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(myWellnessMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateMyWellnessSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(myWellnessInformationLabel);
            WaitForElement(myWellnessResponseLabel);
            WaitForElement(myWellnessMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(myWellnessInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(myWellnessResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(myWellnessMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateMyWellnessSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(myWellnessInformationLabel);
            WaitForElement(myWellnessResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(myWellnessInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(myWellnessResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateMyWellnessGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(myWellnessInformationField);
                Assert.IsTrue(GetElementVisibility(myWellnessInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(myWellnessInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateMyWellnessResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(myWellnessResponse_Field);
                Assert.IsTrue(GetElementVisibility(myWellnessResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(myWellnessResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateMyWellness_Information(string expectedText)
        {
            WaitForElement(myWellnessInformationField);

            Assert.AreEqual(expectedText, GetElementText(myWellnessInformationField));
            return this;
        }
        #endregion

        #region How And When To Support Me fields actions
        public PersonRecordPage_AboutMeArea InsertHowAndWhenToSupportMe_MyStory(string textToInsert)
        {
            WaitForElement(howandwhentosupportmeResponse_Field);
            SendKeys(howandwhentosupportmeResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMe_MyStory(string expectedText)
        {
            WaitForElement(howandwhentosupportmeResponse_Field);
            string response = GetElementText(howandwhentosupportmeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMeUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(howandwhentosupportmeMedia_Upload);
                ScrollToElement(howandwhentosupportmeMedia_Upload);
                Assert.IsTrue(GetElementVisibility(howandwhentosupportmeMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howandwhentosupportmeMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMeMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(howandwhentosupportmeMediaVideo);
                string response = GetElementByAttributeValue(howandwhentosupportmeMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howandwhentosupportmeMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMeSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(howandwhentosupportmeInformationLabel);
            WaitForElement(howandwhentosupportmeResponseLabel);
            WaitForElement(howandwhentosupportmeMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(howandwhentosupportmeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(howandwhentosupportmeResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(howandwhentosupportmeMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMeSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(howandwhentosupportmeInformationLabel);
            WaitForElement(howandwhentosupportmeResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(howandwhentosupportmeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(howandwhentosupportmeResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMeGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(howandwhentosupportmeInformationField);
                Assert.IsTrue(GetElementVisibility(howandwhentosupportmeInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howandwhentosupportmeInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMeResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(howandwhentosupportmeResponse_Field);
                Assert.IsTrue(GetElementVisibility(howandwhentosupportmeResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howandwhentosupportmeResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHowAndWhenToSupportMe_Information(string expectedText)
        {
            WaitForElement(howandwhentosupportmeInformationField);

            Assert.AreEqual(expectedText, GetElementText(howandwhentosupportmeInformationField));
            return this;
        }
        #endregion

        #region Also Worth Knowing About Me fields actions
        public PersonRecordPage_AboutMeArea InsertAlsoWorthKnowingAboutMe_MyStory(string textToInsert)
        {
            WaitForElement(alsoWorthKnowingAboutMeResponse_Field);
            SendKeys(alsoWorthKnowingAboutMeResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMe_MyStory(string expectedText)
        {
            WaitForElement(alsoWorthKnowingAboutMeResponse_Field);
            string response = GetElementText(alsoWorthKnowingAboutMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMeUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(alsoWorthKnowingAboutMeMedia_Upload);
                ScrollToElement(alsoWorthKnowingAboutMeMedia_Upload);
                Assert.IsTrue(GetElementVisibility(alsoWorthKnowingAboutMeMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutMeMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMeMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(alsoWorthKnowingAboutMeMediaVideo);
                string response = GetElementByAttributeValue(alsoWorthKnowingAboutMeMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutMeMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMeSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(alsoWorthKnowingAboutMeInformationLabel);
            WaitForElement(alsoWorthKnowingAboutMeResponseLabel);
            WaitForElement(alsoWorthKnowingAboutMeMediaLabel);

            Assert.AreEqual(informationLabelName, GetElementText(alsoWorthKnowingAboutMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(alsoWorthKnowingAboutMeResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(alsoWorthKnowingAboutMeMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMeSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(alsoWorthKnowingAboutMeInformationLabel);
            WaitForElement(alsoWorthKnowingAboutMeResponseLabel);

            Assert.AreEqual(informationLabelName, GetElementText(alsoWorthKnowingAboutMeInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(alsoWorthKnowingAboutMeResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMeGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(alsoWorthKnowingAboutMeInformationField);
                Assert.IsTrue(GetElementVisibility(alsoWorthKnowingAboutMeInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutMeInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMeResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(alsoWorthKnowingAboutMeResponse_Field);
                Assert.IsTrue(GetElementVisibility(alsoWorthKnowingAboutMeResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutMeResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateAlsoWorthKnowingAboutMe_Information(string expectedText)
        {
            WaitForElement(alsoWorthKnowingAboutMeInformationField);

            Assert.AreEqual(expectedText, GetElementText(alsoWorthKnowingAboutMeInformationField));
            return this;
        }
        #endregion

        #region Physical Characteristics fields actions
        public PersonRecordPage_AboutMeArea InsertPhysicalCharacteristics_MyStory(string textToInsert)
        {
            WaitForElement(physicalCharacteristicsResponse_Field);
            SendKeys(physicalCharacteristicsResponse_Field, textToInsert);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristics_MyStory(string expectedText)
        {
            WaitForElement(physicalCharacteristicsResponse_Field);
            string response = GetElementText(physicalCharacteristicsResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristicsUploadField(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(physicalCharacteristicsResponse_Field);
                ScrollToElement(physicalCharacteristicsResponse_Field);
                Assert.IsTrue(GetElementVisibility(physicalCharacteristicsMedia_Upload));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsMedia_Upload));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristicsMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(physicalCharacteristicsMediaVideo);
                string response = GetElementByAttributeValue(physicalCharacteristicsMediaVideo, "src");
                Assert.IsNotNull(response);
                //Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsMediaVideo));
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristicsSectionColumns(string informationLabelName, string textAreaLabelName, string mediaLabelName)
        {

            WaitForElement(physicalCharacteristicsInformationLabel);
            WaitForElement(physicalCharacteristicsResponseLabel);
            WaitForElement(physicalCharacteristicsMediaLabel);
            ScrollToElement(physicalCharacteristicsInformationLabel);

            Assert.AreEqual(informationLabelName, GetElementText(physicalCharacteristicsInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(physicalCharacteristicsResponseLabel));
            Assert.AreEqual(mediaLabelName, GetElementText(physicalCharacteristicsMediaLabel));


            return this;

        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristicsSectionColumns(string informationLabelName, string textAreaLabelName)
        {

            WaitForElement(physicalCharacteristicsInformationLabel);
            WaitForElement(physicalCharacteristicsResponseLabel);
            ScrollToElement(physicalCharacteristicsInformationLabel);

            Assert.AreEqual(informationLabelName, GetElementText(physicalCharacteristicsInformationLabel));
            Assert.AreEqual(textAreaLabelName, GetElementText(physicalCharacteristicsResponseLabel));

            return this;

        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristicsGuidelinesFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(physicalCharacteristicsInformationField);
                Assert.IsTrue(GetElementVisibility(physicalCharacteristicsInformationField));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsInformationField));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristicsResponseFieldDisplayed(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(physicalCharacteristicsResponse_Field);
                Assert.IsTrue(GetElementVisibility(physicalCharacteristicsResponse_Field));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsResponse_Field));
            }
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidatePhysicalCharacteristics_Information(string expectedText)
        {
            WaitForElement(physicalCharacteristicsInformationField);

            Assert.AreEqual(expectedText, GetElementText(physicalCharacteristicsInformationField));
            return this;
        }
        #endregion

        #region Options tab actions

        public PersonRecordPage_AboutMeArea ClickSaveButton(int timer = 30)
        {

            WaitForElement(saveButton);
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            //WaitForElementNotVisible(LoadingImage, timer);

            if (GetElementVisibility(LoadingImage))
            {
                WaitForElementNotVisible(LoadingImage, timer);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ClickSaveAndArchiveButton()
        {
            WaitForElement(saveAndArchiveButton);
            Click(saveAndArchiveButton);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateNoAboutMeSetupRecordErrorMessage(string expectedMessage)
        {
            WaitForElement(errorMessage);
            string actualErrorMessage = GetElementText(errorMessage);
            Assert.AreEqual(expectedMessage, actualErrorMessage);

            return this;
        }



        public PersonRecordPage_AboutMeArea ValidateSaveButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(saveButton);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(saveButton);
            }
            else
            {
                ValidateElementNotDisabled(saveButton);
            }

            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateSaveAndCloseButtonDisabled(bool ExpectDisabled)
        {
            WaitForElement(saveAndArchiveButton);

            if (ExpectDisabled)
            {
                ValidateElementDisabled(saveAndArchiveButton);
            }
            else
            {
                ValidateElementNotDisabled(saveAndArchiveButton);
            }

            return this;
        }

        #endregion

        public PersonRecordPage_AboutMeArea ValidateHeadingLevel1(string expectedText)
        {
            WaitForElement(headinglevel1);
            Assert.AreEqual(expectedText, GetElementText(headinglevel1));
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateHeadingLevel2(string expectedText)
        {
            WaitForElement(headinglevel2);
            Assert.AreEqual(expectedText, GetElementText(headinglevel2));
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateParagraphText(string expectedText)
        {
            WaitForElement(paragraphText);
            Assert.AreEqual(expectedText, GetElementText(paragraphText));
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateBoldText(string expectedText)
        {
            WaitForElement(boldText);
            Assert.AreEqual(expectedText, GetElementText(boldText));
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateItalicizedText(string expectedText)
        {
            WaitForElement(italicizedText);
            Assert.AreEqual(expectedText, GetElementText(italicizedText));
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateOrderedListElements(int elementCount)
        {
            WaitForElement(orderedListText);
            int actualElementCount = GetCountOfElements(orderedListText);
            Assert.AreEqual(elementCount, actualElementCount);
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateUnorderedListElements(int elementCount)
        {
            WaitForElement(unorderedListText);
            int actualElementCount = GetCountOfElements(unorderedListText);
            Assert.AreEqual(elementCount, actualElementCount);
            return this;
        }

        public PersonRecordPage_AboutMeArea ValidateLinebreakText(string expectedText)
        {
            WaitForElement(lineBreak);
            string actualText = GetElementText(lineBreak);
            Console.WriteLine("Expected Text: " + expectedText);
            Console.WriteLine("Actual Inner Text: " + actualText);
            Assert.IsTrue(actualText.Contains(expectedText));
            return this;
        }
    }
}
