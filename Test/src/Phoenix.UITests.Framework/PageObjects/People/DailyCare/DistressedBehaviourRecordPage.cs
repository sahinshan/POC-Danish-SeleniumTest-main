using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Security.Policy;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class DistressedBehaviourRecordPage : CommonMethods
    {
        public DistressedBehaviourRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonbehaviourincident&')]");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Distressed Behaviour: ']");

        #endregion

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
		readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly string preferences_Id = "CWField_preferences";
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
        readonly By Consentgivenid = By.XPath("//*[@id='CWField_consentgivenid']");
        readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By Occurred = By.XPath("//*[@id='CWField_occurred']");
        readonly By OccurredDatePicker = By.XPath("//*[@id='CWField_occurred_DatePicker']");
        readonly By Occurred_Time = By.XPath("//*[@id='CWField_occurred_Time']");
        readonly By Occurred_Time_TimePicker = By.XPath("//*[@id='CWField_occurred_Time_TimePicker']");
        readonly By Createdon = By.XPath("//*[@id='CWField_createdon']");
        readonly By CreatedonDatePicker = By.XPath("//*[@id='CWField_createdon_DatePicker']");
        readonly By Createdon_Time = By.XPath("//*[@id='CWField_createdon_Time']");
        readonly By Createdon_Time_TimePicker = By.XPath("//*[@id='CWField_createdon_Time_TimePicker']");
        readonly By Antecedent = By.XPath("//*[@id='CWField_antecedent']");
        readonly By Behaviour = By.XPath("//*[@id='CWField_behaviour']");
        readonly By Consequence = By.XPath("//*[@id='CWField_consequence']");
        readonly By Werethereanytriggers_1 = By.XPath("//*[@id='CWField_werethereanytriggers_1']");
        readonly By Werethereanytriggers_0 = By.XPath("//*[@id='CWField_werethereanytriggers_0']");        
		readonly By WhatwerethetriggersLookupButton = By.XPath("//*[@id='CWLookupBtn_whatwerethetriggers']");
        By WhatWereTheTriggers_SelectedOption(string optionId) => By.XPath("//*[@id='MS_whatwerethetriggers_" + optionId + "']");
        By WhatWereTheTriggers_SelectedOptionLink(string optionId) => By.XPath("//*[@id='MS_whatwerethetriggers_" + optionId + "']/a[@id = '" + optionId + "_Link']");
        //WhatWereTheTriggers_SelectedOptionRemoveButton
        By WhatWereTheTriggers_SelectedOptionRemoveButton(string optionId) => By.XPath("//*[@id='MS_whatwerethetriggers_" + optionId + "']/a[text()='Remove']");
        readonly By triggersifotherTextarea = By.XPath("//*[@id='CWField_triggersifother']");
        readonly By Reviewrequiredbyseniorcolleague_1 = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_1']");
        readonly By Reviewrequiredbyseniorcolleague_0 = By.XPath("//*[@id='CWField_reviewrequiredbyseniorcolleague_0']");
        readonly By Reviewdetails = By.XPath("//*[@id='CWField_reviewdetails']");
		readonly By LocationLookupButton = By.XPath("//*[@id='CWLookupBtn_location']");
        readonly By LocationIfOtherTextareaField = By.Id("CWField_locationifother");
        By location_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_location_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By location_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_location_" + ElementId + "']/a[text()='Remove']");

        readonly By WellbeingidLink = By.XPath("//*[@id='CWField_wellbeingid_Link']");
        readonly By WellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_wellbeingid']");
        readonly By WellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_wellbeingid']");
        readonly By Wellbeing_actiontaken = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By Totaltimespentwithclientminutes = By.XPath("//*[@id='CWField_totaltimespentwithclientminutes']");
        readonly By TotalTimeSpentWithClientMinutesFieldError = By.XPath("//label[@for = 'CWField_totaltimespentwithclientminutes'][@class = 'formerror']/span");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");
        readonly By AssistanceneededidLink = By.XPath("//*[@id='CWField_assistanceneededid_Link']");
        readonly By AssistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_assistanceneededid']");
        readonly By AssistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_assistanceneededid']");
        By StaffRequired_SelectedOption(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']");
        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");
        readonly By AssistanceAmountPicklist = By.Id("CWField_assistanceamountid");
        readonly String CarenoteFieldId = "CWField_carenote";
        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");
        readonly By LinkedadlcategoriesidLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedadlcategoriesid']");
        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        //Fields displayed when Consent Given = No
        readonly By nonconsentDetail = By.Id("CWField_nonconsentdetailid");

        readonly By reasonforabsence = By.Id("CWField_reasonforabsence");

        readonly By reasonconsentdeclined = By.Id("CWField_reasonconsentdeclined");
        readonly By encouragementgiven = By.Id("CWField_encouragementgiven");
        readonly By careprovidedwithoutconsent_YesRadioButton = By.Id("CWField_careprovidedwithoutconsent_1");
        readonly By careprovidedwithoutconsent_NoRadioButton = By.Id("CWField_careprovidedwithoutconsent_0");

        readonly By deferredToDate = By.Id("CWField_deferredtodate");
        readonly By deferredToDate_DatePicker = By.Id("CWField_deferredtodate_DatePicker");
        readonly By deferredToDate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");

        readonly By deferredToTimeOrShift = By.Id("CWField_timeorshiftid");
        readonly By deferredToTimeOrShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");

        readonly By deferredToTime = By.Id("CWField_deferredtotime");
        readonly By deferredtotime_TimePicker = By.Id("CWField_deferredtotime_TimePicker");
        readonly By deferredToTime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");

        readonly By deferredToShift_LookupButton = By.Id("CWLookupBtn_deferredtoshiftid");
        readonly By deferredToShift_LinkField = By.Id("CWField_deferredtoshiftid_Link");
        readonly By deferredToShift_ClearButton = By.Id("CWClearLookup_deferredtoshiftid");
        readonly By deferredToShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoshiftid']/label/span");

        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        #region Section postion

        By SectionNameByPosition(int Position) => By.XPath("//*[@id = 'CWInputForm']//div["+Position+"]//*[@class = 'card-header']/*");

        #endregion

        public DistressedBehaviourRecordPage WaitForPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(cwDialogIFrame);
            SwitchToIframe(cwDialogIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(pageHeader);

            return this;
        }

        //verify pageHeader text
        public DistressedBehaviourRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Distressed Behaviour: " + ExpectedText, pageTitle);

            return this;
        }

        public DistressedBehaviourRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public DistressedBehaviourRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidatePreferencesText(string ExpectedText)
        {
            WaitForElement(Preferences);
            ScrollToElement(Preferences);
            ValidateElementValueByJavascript(preferences_Id, ExpectedText);

            return this;
        }

        //veify Preferences field is disabled or not disabled
        public DistressedBehaviourRecordPage ValidatePreferencesFieldIsDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                ValidateElementDisabled(Preferences);
            else
                ValidateElementNotDisabled(Preferences);

            return this;
        }

        public DistressedBehaviourRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Consentgivenid);
            SelectPicklistElementByText(Consentgivenid, TextToSelect);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistContainsElementByText(Consentgivenid, ExpectedText);

            return this;
        }

        //Verify ConsentGiven field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateConsentGivenFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Consentgivenid);
            else
                WaitForElementNotVisible(Consentgivenid, 3);

            return this;
        }

        public DistressedBehaviourRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateOccurredText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnOccurred(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ClickOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ClickOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public DistressedBehaviourRecordPage SetDateAndTimeOccurred(string DateToInsert, string TimeToInsert)
        {
            WaitForElement(Occurred);
            WaitForElement(Occurred_Time);

            SendKeys(Occurred, DateToInsert + Keys.Tab);
            SendKeys(Occurred_Time, TimeToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCreatedonText(string ExpectedText)
        {
            ValidateElementValue(Createdon, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnCreatedon(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon);
            SendKeys(Createdon, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ClickCreatedonDatePicker()
        {
            WaitForElementToBeClickable(CreatedonDatePicker);
            Click(CreatedonDatePicker);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCreatedon_TimeText(string ExpectedText)
        {
            ValidateElementValue(Createdon_Time, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnCreatedon_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon_Time);
            SendKeys(Createdon_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ClickCreatedon_Time_TimePicker()
        {
            WaitForElementToBeClickable(Createdon_Time_TimePicker);
            Click(Createdon_Time_TimePicker);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateAntecedentText(string ExpectedText)
        {
            ValidateElementText(Antecedent, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnAntecedent(string TextToInsert)
        {
            WaitForElementToBeClickable(Antecedent);
            SendKeys(Antecedent, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateBehaviourText(string ExpectedText)
        {
            ValidateElementText(Behaviour, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnBehaviour(string TextToInsert)
        {
            WaitForElementToBeClickable(Behaviour);
            SendKeys(Behaviour, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateConsequenceText(string ExpectedText)
        {
            ValidateElementText(Consequence, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnConsequence(string TextToInsert)
        {
            WaitForElementToBeClickable(Consequence);
            SendKeys(Consequence, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ClickWerethereanytriggers_YesOption()
        {
            WaitForElementToBeClickable(Werethereanytriggers_1);
            Click(Werethereanytriggers_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWerethereanytriggers_YesOptionChecked()
        {
            WaitForElement(Werethereanytriggers_1);
            ValidateElementChecked(Werethereanytriggers_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWerethereanytriggers_YesOptionNotChecked()
        {
            WaitForElement(Werethereanytriggers_1);
            ValidateElementNotChecked(Werethereanytriggers_1);

            return this;
        }

        public DistressedBehaviourRecordPage ClickWerethereanytriggers_NoOption()
        {
            WaitForElementToBeClickable(Werethereanytriggers_0);
            Click(Werethereanytriggers_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWerethereanytriggers_NoOptionChecked()
        {
            WaitForElement(Werethereanytriggers_0);
            ValidateElementChecked(Werethereanytriggers_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWerethereanytriggers_NoOptionNotChecked()
        {
            WaitForElement(Werethereanytriggers_0);
            ValidateElementNotChecked(Werethereanytriggers_0);

            return this;
        }

        public DistressedBehaviourRecordPage ClickWhatwerethetriggersLookupButton()
        {
            WaitForElementToBeClickable(WhatwerethetriggersLookupButton);
            Click(WhatwerethetriggersLookupButton);

            return this;
        }

        //verify that WhatwerethetriggersLookupButton is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateWhatWereTheTriggersLookupButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WhatwerethetriggersLookupButton);
            else
                WaitForElementNotVisible(WhatwerethetriggersLookupButton, 3);

            return this;
        }

        //insert text on triggersifotherTextarea
        public DistressedBehaviourRecordPage InsertTextOnTriggersIfOtherTextarea(string TextToInsert)
        {
            WaitForElement(triggersifotherTextarea);
            ScrollToElement(triggersifotherTextarea);
            SendKeys(triggersifotherTextarea, TextToInsert + Keys.Tab);

            return this;
        }

        //verify triggersifotherTextarea
        public DistressedBehaviourRecordPage ValidateTriggersIfOtherTextareaText(string ExpectedText)
        {
            WaitForElement(triggersifotherTextarea);
            ScrollToElement(triggersifotherTextarea);
            ValidateElementText(triggersifotherTextarea, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickWhatWereTheTriggers_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(WhatWereTheTriggers_SelectedOptionLink(ElementId));
            Click(WhatWereTheTriggers_SelectedOptionLink(ElementId));

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWhatWereTheTriggers_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(WhatWereTheTriggers_SelectedOptionLink(ElementId));
            ValidateElementText(WhatWereTheTriggers_SelectedOptionLink(ElementId), ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWhatWereTheTriggers_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateWhatWereTheTriggers_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }


        //verify WhatWereTheTriggers_SelectedOption text
        public DistressedBehaviourRecordPage ValidateWhatWereTheTriggers_SelectedOptionText(string ElementId, string ExpectedText)
        {
            ValidateElementTextContainsText(WhatWereTheTriggers_SelectedOption(ElementId), ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWhatWereTheTriggers_SelectedOptionText(Guid ElementId, string ExpectedText)
        {
            return ValidateWhatWereTheTriggers_SelectedOptionText(ElementId.ToString(), ExpectedText);
        }

        public DistressedBehaviourRecordPage ClicWhatWereTheTriggers_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(WhatWereTheTriggers_SelectedOptionRemoveButton(ElementId));
            Click(WhatWereTheTriggers_SelectedOptionRemoveButton(ElementId));

            return this;
        }

        //verify that triggersifotherTextarea is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateTriggersIfOtherTextareaVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(triggersifotherTextarea);
            else
                WaitForElementNotVisible(triggersifotherTextarea, 3);

            return this;
        }

        //verify triggersifothertextarea field maxlength attribute value
        public DistressedBehaviourRecordPage ValidateTriggersIfOtherTextareaFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(triggersifotherTextarea, "maxlength", ExpectedLength);

            return this;
        }

        public DistressedBehaviourRecordPage ClickReviewrequiredbyseniorcolleague_YesOption()
        {
            WaitForElementToBeClickable(Reviewrequiredbyseniorcolleague_1);
            Click(Reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReviewrequiredbyseniorcolleague_YesOptionChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_1);
            ValidateElementChecked(Reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReviewrequiredbyseniorcolleague_YesOptionNotChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_1);
            ValidateElementNotChecked(Reviewrequiredbyseniorcolleague_1);

            return this;
        }

        public DistressedBehaviourRecordPage ClickReviewrequiredbyseniorcolleague_NoOption()
        {
            WaitForElementToBeClickable(Reviewrequiredbyseniorcolleague_0);
            Click(Reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReviewrequiredbyseniorcolleague_NoOptionChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_0);
            ValidateElementChecked(Reviewrequiredbyseniorcolleague_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReviewrequiredbyseniorcolleague_NoOptionNotChecked()
        {
            WaitForElement(Reviewrequiredbyseniorcolleague_0);
            ValidateElementNotChecked(Reviewrequiredbyseniorcolleague_0);

            return this;
        }

        //verify ValidateReviewRequiredBySeniorColleague radio buttons are visible
        public DistressedBehaviourRecordPage ValidateReviewRequiredBySeniorColleagueRadioButtonsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElement(Reviewrequiredbyseniorcolleague_1);
                WaitForElement(Reviewrequiredbyseniorcolleague_0);
            }
            else
            {
                WaitForElementNotVisible(Reviewrequiredbyseniorcolleague_1, 3);
                WaitForElementNotVisible(Reviewrequiredbyseniorcolleague_0, 3);
            }

            return this;
        }


        public DistressedBehaviourRecordPage ValidateReviewdetailsText(string ExpectedText)
        {
            ValidateElementText(Reviewdetails, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertReviewDetails(string TextToInsert)
        {
            WaitForElementToBeClickable(Reviewdetails);
            SendKeys(Reviewdetails, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReviewDetailsIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reviewdetails);
            else
                WaitForElementNotVisible(Reviewdetails, 3);

            return this;
        }

        //Verify ReviewDetails field maxlength attribute value
        public DistressedBehaviourRecordPage ValidateReviewDetailsFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(Reviewdetails, "maxlength", ExpectedLength);

            return this;
        }

        public DistressedBehaviourRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(LocationLookupButton);
            Click(LocationLookupButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(location_SelectedElementLink(ElementId));
            Click(location_SelectedElementLink(ElementId));

            return this;
        }

        public DistressedBehaviourRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(location_SelectedElementLink(ElementId));
            ValidateElementText(location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public DistressedBehaviourRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(location_SelectedElementRemoveButton(ElementId));
            Click(location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        //Insert text on LocationIfOtherTextareaField 
        public DistressedBehaviourRecordPage InsertTextOnLocationIfOtherTextareaField(string TextToInsert)
        {
            WaitForElement(LocationIfOtherTextareaField);
            ScrollToElement(LocationIfOtherTextareaField);
            SendKeys(LocationIfOtherTextareaField, TextToInsert + Keys.Tab);

            return this;
        }

        //verify text in LocationIfOtherTextareaField
        public DistressedBehaviourRecordPage ValidateLocationIfOtherTextareaFieldText(string ExpectedText)
        {
            WaitForElement(LocationIfOtherTextareaField);
            ScrollToElement(LocationIfOtherTextareaField);
            ValidateElementValue(LocationIfOtherTextareaField, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateLocationIfOtherVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOtherTextareaField);
            else
                WaitForElementNotVisible(LocationIfOtherTextareaField, 3);

            return this;
        }

        //verify maxlength attribute value of LocationIfOtherTextareaField
        public DistressedBehaviourRecordPage ValidateLocationIfOtherTextareaFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(LocationIfOtherTextareaField, "maxlength", ExpectedLength);

            return this;
        }


        public DistressedBehaviourRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(WellbeingidLink);
            Click(WellbeingidLink);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(WellbeingidLink);
            ValidateElementText(WellbeingidLink, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(WellbeingidClearButton);
            Click(WellbeingidClearButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(WellbeingidLookupButton);
            Click(WellbeingidLookupButton);

            return this;
        }

        //verify that WellbeingidLookupButton is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateWellbeingLookupButtonIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WellbeingidLookupButton);
            else
                WaitForElementNotVisible(WellbeingidLookupButton, 3);

            return this;
        }

        //verify Wellbeing_actiontaken text
        public DistressedBehaviourRecordPage VerifyActionTaken_HasPainReliefBeenOfferedText(string ExpectedText)
        {
            ValidateElementValue(Wellbeing_actiontaken, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateActionTakenIsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Wellbeing_actiontaken);
            else
                WaitForElementNotVisible(Wellbeing_actiontaken, 3);

            return this;
        }

        //Insert text in Wellbeing_actiontaken field
        public DistressedBehaviourRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(Wellbeing_actiontaken);
            SendKeys(Wellbeing_actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateTotalTimeSpentWithPersonMinutesText(string ExpectedText)
        {
            ValidateElementValue(Totaltimespentwithclientminutes, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnTotalTimesSpentWithPersonMinutes(string TextToInsert)
        {
            WaitForElementToBeClickable(Totaltimespentwithclientminutes);
            SendKeys(Totaltimespentwithclientminutes, TextToInsert + Keys.Tab);

            return this;
        }

        //verify TotalTimeSpentWithPersonMinutes field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateTotalTimeSpentWithPersonMinutesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Totaltimespentwithclientminutes);
            else
                WaitForElementNotVisible(Totaltimespentwithclientminutes, 3);

            return this;
        }

        public DistressedBehaviourRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithClientMinutesFieldError);
            else
                WaitForElementNotVisible(TotalTimeSpentWithClientMinutesFieldError, 3);

            return this;
        }


        public DistressedBehaviourRecordPage VerifyTotalTimeSpentWithPersonMinutesFieldErrorText(string ExpectedText)
        {
            WaitForElement(TotalTimeSpentWithClientMinutesFieldError);
            ValidateElementByTitle(TotalTimeSpentWithClientMinutesFieldError, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateAdditionalnotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnAdditionalnotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        //verify additional notes field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateAdditionalNotesFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Additionalnotes);
            else
                WaitForElementNotVisible(Additionalnotes, 3);

            return this;
        }

        public DistressedBehaviourRecordPage ClickAssistanceneededLink()
        {
            WaitForElementToBeClickable(AssistanceneededidLink);
            Click(AssistanceneededidLink);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateAssistanceneededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(AssistanceneededidLink);
            ValidateElementText(AssistanceneededidLink, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickAssistanceneededClearButton()
        {
            WaitForElementToBeClickable(AssistanceneededidClearButton);
            Click(AssistanceneededidClearButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(AssistanceneededidLookupButton);
            Click(AssistanceneededidLookupButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(StaffrequiredLookupButton);
            Click(StaffrequiredLookupButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCarenoteText(string ExpectedText)
        {
            WaitForElement(Carenote);
            ScrollToElement(Carenote);
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextOnCarenote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ClickLinkedadlcategoriesidLookupButton()
        {
            WaitForElementToBeClickable(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickIsincludeinnexthandover_YesOption()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateIsincludeinnexthandover_YesOptionChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateIsincludeinnexthandover_YesOptionNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public DistressedBehaviourRecordPage ClickIsincludeinnexthandover_NoOption()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateIsincludeinnexthandover_NoOptionChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateIsincludeinnexthandover_NoOptionNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public DistressedBehaviourRecordPage ClickFlagrecordforhandover_YesOption()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateFlagrecordforhandover_YesOptionChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateFlagrecordforhandover_YesOptionNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public DistressedBehaviourRecordPage ClickFlagrecordforhandover_NoOption()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateFlagrecordforhandover_NoOptionChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateFlagrecordforhandover_NoOptionNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        public DistressedBehaviourRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateSelectedNonConsentDetail(string ExpectedText)
        {
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        //verify nonconsentDetail field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateNonConsentDetailFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(nonconsentDetail);
            else
                WaitForElementNotVisible(nonconsentDetail, 3);

            return this;
        }

        public DistressedBehaviourRecordPage SetReasonForAbsence(string TextToInsert)
        {
            WaitForElementVisible(reasonforabsence);
            SendKeys(reasonforabsence, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReasonForAbsence(string ExpectedText)
        {
            ValidateElementValue(reasonforabsence, ExpectedText);

            return this;
        }

        //verify reasonforabsence field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateReasonForAbsenceFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reasonforabsence);
            else
                WaitForElementNotVisible(reasonforabsence, 3);

            return this;
        }

        //verify reasonforabsence field maxlength attribute
        public DistressedBehaviourRecordPage ValidateReasonForAbsenceFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(reasonforabsence, "maxlength", ExpectedLength);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextInReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementVisible(reasonconsentdeclined);
            SendKeys(reasonconsentdeclined, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateReasonConsentDeclined(string ExpectedText)
        {
            ValidateElementValue(reasonconsentdeclined, ExpectedText);

            return this;
        }

        //verify reasonconsentdeclined field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateReasonConsentDeclinedFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(reasonconsentdeclined);
            else
                WaitForElementNotVisible(reasonconsentdeclined, 3);

            return this;
        }

        //verify reasonforconsentdeclined field maxlength attribute
        public DistressedBehaviourRecordPage ValidateReasonConsentDeclinedFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(reasonconsentdeclined, "maxlength", ExpectedLength);

            return this;
        }

        public DistressedBehaviourRecordPage InsertTextInEncouragementGiven(string TextToInsert)
        {
            WaitForElementVisible(encouragementgiven);
            SendKeys(encouragementgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateEncouragementGiven(string ExpectedText)
        {
            ValidateElementValue(encouragementgiven, ExpectedText);

            return this;
        }

        //verify encouragementgiven field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateEncouragementGivenFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(encouragementgiven);
            else
                WaitForElementNotVisible(encouragementgiven, 3);

            return this;
        }

        //verify encouragementgiven field maxlength attribute
        public DistressedBehaviourRecordPage ValidateEncouragementGivenFieldMaxLength(string ExpectedLength)
        {
            ValidateElementAttribute(encouragementgiven, "maxlength", ExpectedLength);

            return this;
        }

        public DistressedBehaviourRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_YesRadioButton);
            Click(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_YesRadioButton);
            ValidateElementChecked(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_YesRadioButton);
            ValidateElementNotChecked(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public DistressedBehaviourRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_NoRadioButton);
            Click(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_NoRadioButton);
            ValidateElementChecked(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_NoRadioButton);
            ValidateElementNotChecked(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        //verify CareProvidedWithoutConsent options are displayed or not displayed
        public DistressedBehaviourRecordPage ValidateCareProvidedWithoutConsentOptionsVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(careprovidedwithoutconsent_YesRadioButton);
                WaitForElementVisible(careprovidedwithoutconsent_NoRadioButton);
            }
            else
            {
                WaitForElementNotVisible(careprovidedwithoutconsent_YesRadioButton, 3);
                WaitForElementNotVisible(careprovidedwithoutconsent_NoRadioButton, 3);
            }

            return this;
        }

        public DistressedBehaviourRecordPage SetDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToDate(string ExpectedText)
        {
            ValidateElementValue(deferredToDate, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToDateErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToDate_ErrorLabel);
            ValidateElementText(deferredToDate_ErrorLabel, ExpectedText);

            return this;
        }

        //Validate deferred to date field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateDeferredToDateFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(deferredToDate);
            }
            else
            {
                WaitForElementNotVisible(deferredToDate, 3);
            }

            return this;
        }

        //click deferred to date datepicker
        public DistressedBehaviourRecordPage ClickDeferredToDate_DatePicker()
        {
            WaitForElementToBeClickable(deferredToDate_DatePicker);
            Click(deferredToDate_DatePicker);

            return this;
        }

        //verify deferred to date datepicker is displayed or not displayed
        public DistressedBehaviourRecordPage VerifyDeferredToDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(deferredToDate_DatePicker);
            }
            else
            {
                WaitForElementNotVisible(deferredToDate_DatePicker, 2);
            }

            return this;
        }

        public DistressedBehaviourRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementVisible(deferredToTimeOrShift);
            SelectPicklistElementByText(deferredToTimeOrShift, TextToSelect);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateSelectedDeferredToTimeOrShift(string ExpectedText)
        {
            ValidatePicklistSelectedText(deferredToTimeOrShift, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToTimeOrShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            ValidateElementText(deferredToTimeOrShift_ErrorLabel, ExpectedText);

            return this;
        }

        //verify deferredToTimeorsift field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateDeferredToTimeOrShiftFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTimeOrShift);
            else
                WaitForElementNotVisible(deferredToTimeOrShift, 3);

            return this;
        }

        public DistressedBehaviourRecordPage SetDeferredToTime(string TextToInsert)
        {
            WaitForElementVisible(deferredToTime);
            SendKeys(deferredToTime, TextToInsert + Keys.Tab);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToTime(string ExpectedText)
        {
            ValidateElementValue(deferredToTime, ExpectedText);

            return this;
        }

        //verify deferredToTime field is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateDeferredToTimeFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToTime);
            else
                WaitForElementNotVisible(deferredToTime, 3);

            return this;
        }

        //click deferredToTime timepicker
        public DistressedBehaviourRecordPage ClickDeferredToTime_TimePicker()
        {
            WaitForElementToBeClickable(deferredtotime_TimePicker);
            Click(deferredtotime_TimePicker);

            return this;
        }

        //verify deferredToTime timepicker is displayed or not displayed
        public DistressedBehaviourRecordPage VerifyDeferredToTime_TimePickerIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
                WaitForElementVisible(deferredtotime_TimePicker);
            else
                WaitForElementNotVisible(deferredtotime_TimePicker, 2);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToTimeErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTime_ErrorLabel);
            ValidateElementText(deferredToTime_ErrorLabel, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(deferredToShift_LookupButton);
            Click(deferredToShift_LookupButton);

            return this;
        }

        //verify deferredToShift_LookupButton is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateDeferredToShiftLookupButtonVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(deferredToShift_LookupButton);
            else
                WaitForElementNotVisible(deferredToShift_LookupButton, 3);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            ValidateElementText(deferredToShift_LinkField, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ClickDeferredToShiftClearButton()
        {
            Click(deferredToShift_ClearButton);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateDeferredToShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToShift_ErrorLabel);
            ValidateElementText(deferredToShift_ErrorLabel, ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(FieldLabel(FieldName));
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }

        //verify SectionNameByPosition
        public DistressedBehaviourRecordPage ValidateSectionName(string ExpectedText, int Position)
        {
            WaitForElement(SectionNameByPosition(Position));            
            ValidateElementText(SectionNameByPosition(Position), ExpectedText);

            return this;
        }

        //verify sectionnamebyposition is displayed or not displayed
        public DistressedBehaviourRecordPage ValidateSectionNameVisible(string ExpectedText, int Position, bool ExpectVisible = true)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(SectionNameByPosition(Position));
                ValidateElementText(SectionNameByPosition(Position), ExpectedText);
            }
            else
            {
                WaitForElementNotVisible(SectionNameByPosition(Position), 3);
            }

            return this;
        }

        public DistressedBehaviourRecordPage ValidateStaffRequiredSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(StaffRequired_SelectedOption(OptionId));
            ValidateElementTextContainsText(StaffRequired_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public DistressedBehaviourRecordPage ValidateStaffRequiredSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateStaffRequiredSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

        public DistressedBehaviourRecordPage SelectAssistanceAmountFromPicklist(string OptionText)
        {
            WaitForElementToBeClickable(AssistanceAmountPicklist);
            SelectPicklistElementByText(AssistanceAmountPicklist, OptionText);

            return this;
        }

        //verify AssistanceAmountPicklist Selected Text
        public DistressedBehaviourRecordPage ValidateAssistanceAmountPicklistSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(AssistanceAmountPicklist, ExpectedText);

            return this;
        }

        //verify AssistanceAmountPicklist is visible or not visible
        public DistressedBehaviourRecordPage ValidateAssistanceAmountPicklistVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmountPicklist);
            else
                WaitForElementNotVisible(AssistanceAmountPicklist, 3);
            return this;
        }

    }
}
