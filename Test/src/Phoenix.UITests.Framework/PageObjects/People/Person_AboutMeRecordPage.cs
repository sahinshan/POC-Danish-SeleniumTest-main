using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class Person_AboutMeRecordPage : CommonMethods
    {
        public Person_AboutMeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");        
        readonly By personAboutMeRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personaboutme&')]");
        readonly By HTMLResoucePanelIFrame = By.Id("CWUrlPanel_IFrame");

        #region About Me tab and options                
        readonly By LoadingImage = By.XPath("//*[@class = 'loader']");
        #endregion

        #region General section
        readonly By generalSectionTitle = By.XPath("//span[text() = 'General']");
        readonly By date_Field = By.Id("AM-select-date");
        readonly By responsibleUser_Field = By.Id("responsibleUser_Link");
        readonly By responsibleUser_Lookup = By.XPath("//input[contains(@onchange, 'responsibleUser')]");
        readonly By responsibleTeam_Field = By.Id("responsibleTeam_Link");
        readonly By responsibleTeam_Lookup = By.XPath("//input[contains(@onchange, 'responsibleTeam')]");
        readonly By supportedToWriteThisBy_Field = By.Id("supportedBy_Link");
        readonly By supportedToWriteThisBy_Lookup = By.XPath("//input[contains(@onchange, 'supportedBy')]");
        readonly By capacityEstablished_YesOption = By.Id("AM-capacity-established-yes");
        readonly By capacityEstablished_NoOption = By.Id("AM-capacity-established-no");
        readonly By consentGrantedForRecordings_YesOption = By.Id("AM-consent-granted-for-recordings-yes");
        readonly By consentGrantedForRecordings_NoOption = By.Id("AM-consent-granted-for-recordings-no");
        #endregion

        #region About Me section
        readonly By aboutMeSection = By.XPath("//*[@id = 'aboutme']");
        readonly By aboutMeSectionTitle = By.XPath("//span[text() = 'About Me']");
        readonly By aboutMeResponse_Field = By.Id("aboutme-response");
        readonly By aboutMeMedia_Upload = By.XPath("//*[@for = 'aboutme-media']/div");
        readonly By aboutMeMediaUploadBtn = By.Id("aboutme-media");

        readonly By aboutMeMediaVideo = By.Id("aboutme-media-video");
        #endregion

        #region What is most important to Me section
        readonly By whatIsMostImportantToMeSection = By.XPath("//*[@id = 'whatismostimportanttome']");
        readonly By whatIsMostImportantToMeSectionTitle = By.XPath("//span[text() = 'What Is Most Important To Me']");
        readonly By whatIsMostImportantToMeResponse_Field = By.Id("whatismostimportanttome-response");
        readonly By whatIsMostImportantToMeMedia_Upload = By.XPath("//*[@for = 'whatismostimportanttome-media']/div");
        readonly By whatIsMostImportantToMeMediaUploadBtn = By.Id("whatismostimportanttome-media");

        readonly By whatIsMostImportantToMeMediaVideo = By.Id("whatismostimportanttome-media-video");
        #endregion

        #region People Who Are Important To Me section
        readonly By peopleWhoAreImportantToMeSection = By.XPath("//*[@id = 'peoplewhoareimportanttome']");
        readonly By peopleWhoAreImportantToMeSectionTitle = By.XPath("//span[text() = 'People Who Are Important To Me section']");
        readonly By peopleWhoAreImportantToMeResponse_Field = By.Id("peoplewhoareimportanttome-response");
        readonly By peopleWhoAreImportantToMeMedia_Upload = By.XPath("//*[@for = 'peoplewhoareimportanttome-media']/div");
        readonly By peopleWhoAreImportantToMeMediaUploadBtn = By.Id("peoplewhoareimportanttome-media");

        readonly By peopleWhoAreImportantToMeMediaVideo = By.Id("peoplewhoareimportanttome-media-video");
        #endregion

        #region How I Communicate And How To Communicate With Me section
        readonly By howICommunicateAndHowToCommunicateWithMeSection = By.XPath("//*[@id = 'howicommunicateandhowtocommunicatewithme']");
        readonly By howICommunicateAndHowToCommunicateWithMeSectionTitle = By.XPath("//span[text() = 'How I Communicate And How To Communicate With Me']");
        readonly By howICommunicateAndHowToCommunicateWithMeResponse_Field = By.Id("howicommunicateandhowtocommunicatewithme-response");
        readonly By howICommunicateAndHowToCommunicateWithMeMedia_Upload = By.XPath("//*[@for = 'howicommunicateandhowtocommunicatewithme-media']/div");
        readonly By howICommunicateAndHowToCommunicateWithMeMediaUploadBtn = By.Id("howicommunicateandhowtocommunicatewithme-media");

        readonly By howICommunicateAndHowToCommunicateWithMeMediaVideo = By.Id("howicommunicateandhowtocommunicatewithme-media-video");
        #endregion

        #region Please Do And Please Do Not section
        readonly By pleasedoandpleasedonotSection = By.XPath("//*[@id = 'pleasedoandpleasedonot']");
        readonly By pleasedoandpleasedonotSectionTitle = By.XPath("//span[text() = 'Please Do And Please Do Not']");
        readonly By pleasedoandpleasedonotResponse_Field = By.Id("pleasedoandpleasedonot-response");
        readonly By pleasedoandpleasedonotMedia_Upload = By.XPath("//*[@for = 'pleasedoandpleasedonot-media']/div");
        readonly By pleasedoandpleasedonotMediaUploadBtn = By.Id("pleasedoandpleasedonot-media");

        readonly By pleasedoandpleasedonotMediaVideo = By.Id("pleasedoandpleasedonot-media-video");
        #endregion

        #region My Wellness section
        readonly By myWellnessSection = By.XPath("//*[@id = 'mywellness']");
        readonly By myWellnessSectionTitle = By.XPath("//span[text() = 'My Wellness']");
        readonly By myWellnessResponse_Field = By.Id("mywellness-response");
        readonly By myWellnessMedia_Upload = By.XPath("//*[@for = 'mywellness-media']/div");
        readonly By myWellnessMediaUploadBtn = By.Id("mywellness-media");

        readonly By myWellnessMediaVideo = By.Id("mywellness-media-video");
        #endregion

        #region How And When To Support Me section
        readonly By howandwhentosupportmeSection = By.XPath("//*[@id = 'howandwhentosupportme']");
        readonly By howandwhentosupportmeSectionTitle = By.XPath("//span[text() = 'How And When To Support Me']");
        readonly By howandwhentosupportmeResponse_Field = By.Id("howandwhentosupportme-response");
        readonly By howandwhentosupportmeMedia_Upload = By.XPath("//*[@for = 'howandwhentosupportme-media']/div");
        readonly By howandwhentosupportmeMediaUploadBtn = By.Id("howandwhentosupportme-media");

        readonly By howandwhentosupportmeMediaVideo = By.Id("howandwhentosupportme-media-video");
        #endregion

        #region Also Worth Knowing About Me section
        readonly By alsoWorthKnowingAboutMeSection = By.XPath("//*[@id = 'alsoworthknowingaboutme']");
        readonly By alsoWorthKnowingAboutMeSectionTitle = By.XPath("//span[text() = 'Also Worth Knowing About Me']");
        readonly By alsoWorthKnowingAboutMeResponse_Field = By.Id("alsoworthknowingaboutme-response");
        readonly By alsoWorthKnowingAboutMeMedia_Upload = By.XPath("//*[@for = 'alsoworthknowingaboutme-media']/div");
        readonly By alsoWorthKnowingAboutMeMediaUploadBtn = By.Id("alsoworthknowingaboutme-media");

        readonly By alsoWorthKnowingAboutMeMediaVideo = By.Id("alsoworthknowingaboutme-media-video");
        #endregion

        #region Physical Characteristics section
        readonly By physicalCharacteristicsSection = By.XPath("//*[@id = 'physicalcharacteristics']");
        readonly By physicalCharacteristicsSectionTitle = By.XPath("//span[text() = 'Physical Characteristics']");
        readonly By physicalCharacteristicsResponse_Field = By.Id("physicalcharacteristics-response");
        readonly By physicalCharacteristicsMedia_Upload = By.XPath("//*[@for = 'physicalcharacteristics-media']/div");
        readonly By physicalCharacteristicsMediaUploadBtn = By.Id("physicalcharacteristics-media");

        readonly By physicalCharacteristicsMediaVideo = By.Id("physicalcharacteristics-media-video");
        #endregion


        public Person_AboutMeRecordPage WaitForPerson_AboutMeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            

            WaitForElement(personAboutMeRecordIFrame);
            SwitchToIframe(personAboutMeRecordIFrame);

            WaitForElement(HTMLResoucePanelIFrame);
            SwitchToIframe(HTMLResoucePanelIFrame);


            return this;
        }

        #region General sections fields actions

        public Person_AboutMeRecordPage ValidateDate(string dateValue)
        {
            WaitForElement(date_Field);
            string actualDateValue = GetElementValueByJavascript("AM-select-date");
            Assert.AreEqual(dateValue, actualDateValue);           

            return this;
        }

        public Person_AboutMeRecordPage ValidatePersonAboutMeRecordPageFieldsAreDisabled()
        {
            WaitForElement(date_Field);
            ValidateElementDisabled(date_Field);
            ValidateElementDisabled(responsibleUser_Lookup);
            ValidateElementDisabled(responsibleTeam_Lookup);
            ValidateElementDisabled(supportedToWriteThisBy_Lookup);            
            ValidateElementDisabled(capacityEstablished_YesOption);
            ValidateElementDisabled(capacityEstablished_NoOption);
            ValidateElementDisabled(consentGrantedForRecordings_YesOption);
            ValidateElementDisabled(consentGrantedForRecordings_NoOption);

            if (GetElementVisibility(aboutMeResponse_Field))
                ValidateElementDisabled(aboutMeResponse_Field);
            if (GetElementVisibility(whatIsMostImportantToMeResponse_Field))
                ValidateElementDisabled(whatIsMostImportantToMeResponse_Field);
            if (GetElementVisibility(peopleWhoAreImportantToMeResponse_Field))
                ValidateElementDisabled(peopleWhoAreImportantToMeResponse_Field);
            if (GetElementVisibility(howICommunicateAndHowToCommunicateWithMeResponse_Field))
                ValidateElementDisabled(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            if (GetElementVisibility(pleasedoandpleasedonotResponse_Field))
                ValidateElementDisabled(pleasedoandpleasedonotResponse_Field);
            if (GetElementVisibility(myWellnessResponse_Field))
                ValidateElementDisabled(myWellnessResponse_Field);
            if (GetElementVisibility(howandwhentosupportmeResponse_Field))
                ValidateElementDisabled(howandwhentosupportmeResponse_Field);
            if (GetElementVisibility(alsoWorthKnowingAboutMeResponse_Field))
                ValidateElementDisabled(alsoWorthKnowingAboutMeResponse_Field);
            if (GetElementVisibility(physicalCharacteristicsResponse_Field))
                ValidateElementDisabled(physicalCharacteristicsResponse_Field);
            return this;
        }

        public Person_AboutMeRecordPage ValidatePersonAboutMeRecordPageMediaFieldsAreDisabled()
        {

            if (GetElementVisibility(aboutMeMediaUploadBtn))
                ValidateElementDisabled(aboutMeMediaUploadBtn);
            if (GetElementVisibility(whatIsMostImportantToMeMediaUploadBtn))
                ValidateElementDisabled(whatIsMostImportantToMeMediaUploadBtn);
            if (GetElementVisibility(peopleWhoAreImportantToMeMediaUploadBtn))
                ValidateElementDisabled(peopleWhoAreImportantToMeMediaUploadBtn);
            if (GetElementVisibility(howICommunicateAndHowToCommunicateWithMeMediaUploadBtn))
                ValidateElementDisabled(howICommunicateAndHowToCommunicateWithMeMediaUploadBtn);
            if (GetElementVisibility(pleasedoandpleasedonotMediaUploadBtn))
                ValidateElementDisabled(pleasedoandpleasedonotMediaUploadBtn);
            if (GetElementVisibility(myWellnessMediaUploadBtn))
                ValidateElementDisabled(myWellnessMediaUploadBtn);
            if (GetElementVisibility(howandwhentosupportmeMediaUploadBtn))
                ValidateElementDisabled(howandwhentosupportmeMediaUploadBtn);
            if (GetElementVisibility(alsoWorthKnowingAboutMeMediaUploadBtn))
                ValidateElementDisabled(alsoWorthKnowingAboutMeMediaUploadBtn);
            if (GetElementVisibility(physicalCharacteristicsMediaUploadBtn))
                ValidateElementDisabled(physicalCharacteristicsMediaUploadBtn);
            return this;
        }

        public Person_AboutMeRecordPage ValidateResponsibleUserField(string expectedText)
        {
            WaitForElement(responsibleUser_Field);
            ValidateElementByTitle(responsibleUser_Field, expectedText);

            return this;
        }

        public Person_AboutMeRecordPage ValidateResponsibleTeamField(string expectedText)
        {
            WaitForElement(responsibleTeam_Field);
            ValidateElementByTitle(responsibleTeam_Field, expectedText);

            return this;
        }

        public Person_AboutMeRecordPage ValidateSupportedToWriteThisByField(string expectedText)
        {
            WaitForElement(supportedToWriteThisBy_Field);
            ValidateElementByTitle(supportedToWriteThisBy_Field, expectedText);

            return this;
        }


        public Person_AboutMeRecordPage ValidateCapacityEstablishedOption(bool status)
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
        public Person_AboutMeRecordPage ValidateConsentGrantedForRecordingOption(bool status)
        {
            WaitForElement(consentGrantedForRecordings_YesOption);
            WaitForElement(consentGrantedForRecordings_NoOption);

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

        #endregion

        #region About Me section fields actions

        public Person_AboutMeRecordPage ValidateAboutMe_MyStory(string expectedText)
        {
            WaitForElement(aboutMeResponse_Field);
            string response = GetElementText(aboutMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidateAboutMeMediaUploadField(bool isPresent)
        {
            WaitForElement(aboutMeMedia_Upload);
            ScrollToElement(aboutMeMedia_Upload);

            if (isPresent)
            {
                WaitForElementVisible(aboutMeMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(aboutMeMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidateAboutMeMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(aboutMeMediaVideo);
                string response = GetElementByAttributeValue(aboutMeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(aboutMeMediaVideo));
            }

            return this;
        }

        #endregion

        #region What Is Most Important To Me fields actions
        public Person_AboutMeRecordPage InsertWhatIsMostImportant_MyStory(string textToInsert)
        {
            WaitForElement(whatIsMostImportantToMeResponse_Field);
            SendKeys(whatIsMostImportantToMeResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidateWhatIsMostImportant_MyStory(string expectedText)
        {
            WaitForElement(whatIsMostImportantToMeResponse_Field);
            string response = GetElementText(whatIsMostImportantToMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }


        public Person_AboutMeRecordPage ValidateWhatIsMostImportantMediaUploadField(bool isPresent)
        {
            WaitForElement(whatIsMostImportantToMeMedia_Upload);
            ScrollToElement(whatIsMostImportantToMeMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(whatIsMostImportantToMeMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(whatIsMostImportantToMeMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidateWhatIsMostImportantMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(whatIsMostImportantToMeMediaVideo);
                string response = GetElementByAttributeValue(whatIsMostImportantToMeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(whatIsMostImportantToMeMediaVideo));
            }

            return this;
        }

        #endregion

        #region People Who Are Important To Me fields actions
        public Person_AboutMeRecordPage InsertPeopleWhoAreImportant_MyStory(string textToInsert)
        {
            WaitForElement(peopleWhoAreImportantToMeResponse_Field);
            SendKeys(peopleWhoAreImportantToMeResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidatePeopleWhoAreImportant_MyStory(string expectedText)
        {
            WaitForElement(peopleWhoAreImportantToMeResponse_Field);
            string response = GetElementText(peopleWhoAreImportantToMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidatePeopleWhoAreImportantMediaUploadField(bool isPresent)
        {
            WaitForElement(peopleWhoAreImportantToMeMedia_Upload);
            ScrollToElement(peopleWhoAreImportantToMeMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(peopleWhoAreImportantToMeMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(peopleWhoAreImportantToMeMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidatePeopleWhoAreImportantMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(peopleWhoAreImportantToMeMediaVideo);
                string response = GetElementByAttributeValue(peopleWhoAreImportantToMeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(peopleWhoAreImportantToMeMediaVideo));
            }

            return this;
        }

        #endregion

        #region How I Communicate And How To Communicate With Me field actions
        public Person_AboutMeRecordPage InsertHowICommunicate_MyStory(string textToInsert)
        {
            WaitForElement(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            SendKeys(howICommunicateAndHowToCommunicateWithMeResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidateHowICommunicate_MyStory(string expectedText)
        {
            WaitForElement(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            string response = GetElementText(howICommunicateAndHowToCommunicateWithMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidateHowICommunicateMediaUploadField(bool isPresent)
        {
            WaitForElement(howICommunicateAndHowToCommunicateWithMeMedia_Upload);
            ScrollToElement(howICommunicateAndHowToCommunicateWithMeMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(howICommunicateAndHowToCommunicateWithMeMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(howICommunicateAndHowToCommunicateWithMeMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidateHowICommunicateMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(howICommunicateAndHowToCommunicateWithMeMediaVideo);
                string response = GetElementByAttributeValue(howICommunicateAndHowToCommunicateWithMeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howICommunicateAndHowToCommunicateWithMeMediaVideo));
            }

            return this;
        }

        #endregion

        #region Please Do And Please Do Not fields actions
        public Person_AboutMeRecordPage InsertPleaseDoAndPleaseDoNot_MyStory(string textToInsert)
        {
            WaitForElement(pleasedoandpleasedonotResponse_Field);
            SendKeys(pleasedoandpleasedonotResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidatePleaseDoAndPleaseDoNot_MyStory(string expectedText)
        {
            WaitForElement(pleasedoandpleasedonotResponse_Field);
            string response = GetElementText(pleasedoandpleasedonotResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidatePleaseDoAndPleaseDoNotUploadField(bool isPresent)
        {
            WaitForElement(pleasedoandpleasedonotMedia_Upload);
            ScrollToElement(pleasedoandpleasedonotMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(pleasedoandpleasedonotMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(pleasedoandpleasedonotMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidatePleaseDoAndPleaseDoNotMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(pleasedoandpleasedonotMediaVideo);
                string response = GetElementByAttributeValue(pleasedoandpleasedonotMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(pleasedoandpleasedonotMediaVideo));
            }

            return this;
        }

        #endregion


        #region My Wellness fields actions
        public Person_AboutMeRecordPage InsertMyWellness_MyStory(string textToInsert)
        {
            WaitForElement(myWellnessResponse_Field);
            SendKeys(myWellnessResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidateMyWellness_MyStory(string expectedText)
        {
            WaitForElement(myWellnessResponse_Field);
            string response = GetElementText(myWellnessResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidateMyWellnessUploadField(bool isPresent)
        {
            WaitForElement(myWellnessMedia_Upload);
            ScrollToElement(myWellnessMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(myWellnessMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(myWellnessMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidateMyWellnessMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(myWellnessMediaVideo);
                string response = GetElementByAttributeValue(myWellnessMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(myWellnessMediaVideo));
            }

            return this;
        }


        #endregion

        #region How And When To Support Me fields actions
        public Person_AboutMeRecordPage InsertHowAndWhenToSupportMe_MyStory(string textToInsert)
        {
            WaitForElement(howandwhentosupportmeResponse_Field);
            SendKeys(howandwhentosupportmeResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidateHowAndWhenToSupportMe_MyStory(string expectedText)
        {
            WaitForElement(howandwhentosupportmeResponse_Field);
            string response = GetElementText(howandwhentosupportmeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidateHowAndWhenToSupportMeUploadField(bool isPresent)
        {
            WaitForElement(howandwhentosupportmeMedia_Upload);
            ScrollToElement(howandwhentosupportmeMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(howandwhentosupportmeMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(howandwhentosupportmeMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidateHowAndWhenToSupportMeMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(howandwhentosupportmeMediaVideo);
                string response = GetElementByAttributeValue(howandwhentosupportmeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(howandwhentosupportmeMediaVideo));
            }

            return this;
        }

        #endregion

        #region Also Worth Knowing About Me fields actions
        public Person_AboutMeRecordPage InsertAlsoWorthKnowingAboutMe_MyStory(string textToInsert)
        {
            WaitForElement(alsoWorthKnowingAboutMeResponse_Field);
            SendKeys(alsoWorthKnowingAboutMeResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidateAlsoWorthKnowingAboutMe_MyStory(string expectedText)
        {
            WaitForElement(alsoWorthKnowingAboutMeResponse_Field);
            string response = GetElementText(alsoWorthKnowingAboutMeResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidateAlsoWorthKnowingAboutMeUploadField(bool isPresent)
        {
            WaitForElement(alsoWorthKnowingAboutMeMedia_Upload);
            ScrollToElement(alsoWorthKnowingAboutMeMedia_Upload);
            if (isPresent)
            {
                WaitForElementVisible(alsoWorthKnowingAboutMeMedia_Upload);
            }
            else
            {
                WaitForElementNotVisible(alsoWorthKnowingAboutMeMedia_Upload, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidateAlsoWorthKnowingAboutMesMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(alsoWorthKnowingAboutMeMediaVideo);
                string response = GetElementByAttributeValue(alsoWorthKnowingAboutMeMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(alsoWorthKnowingAboutMeMediaVideo));
            }

            return this;
        }

        #endregion

        #region Physical Characteristics fields actions
        public Person_AboutMeRecordPage InsertPhysicalCharacteristics_MyStory(string textToInsert)
        {
            WaitForElement(physicalCharacteristicsResponse_Field);
            SendKeys(physicalCharacteristicsResponse_Field, textToInsert);

            return this;
        }

        public Person_AboutMeRecordPage ValidatePhysicalCharacteristics_MyStory(string expectedText)
        {
            WaitForElement(physicalCharacteristicsResponse_Field);
            string response = GetElementText(physicalCharacteristicsResponse_Field);
            Assert.AreEqual(expectedText, response);

            return this;
        }

        public Person_AboutMeRecordPage ValidatePhysicalCharacteristicsUploadField(bool isPresent)
        {
            WaitForElement(physicalCharacteristicsResponse_Field);
            ScrollToElement(physicalCharacteristicsResponse_Field);
            if (isPresent)
            {
                WaitForElementVisible(physicalCharacteristicsResponse_Field);
            }
            else
            {
                WaitForElementNotVisible(physicalCharacteristicsResponse_Field, 3);
            }
            return this;
        }

        public Person_AboutMeRecordPage ValidatePhysicalCharacteristicsMediaPresent(bool isPresent)
        {
            if (isPresent)
            {
                WaitForElement(physicalCharacteristicsMediaVideo);
                string response = GetElementByAttributeValue(physicalCharacteristicsMediaVideo, "src");
                Assert.IsNotNull(response);
                Assert.IsTrue(response.Contains("/viewfile"));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(physicalCharacteristicsMediaVideo));
            }

            return this;
        }

        #endregion



    }
}
