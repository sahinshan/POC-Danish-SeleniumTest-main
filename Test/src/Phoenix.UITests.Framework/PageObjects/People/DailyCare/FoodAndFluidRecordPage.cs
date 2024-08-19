using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FoodAndFluidRecordPage : CommonMethods
    {
        public FoodAndFluidRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonfoodandfluid&')]");

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
        readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Food And Fluid: ']");

        readonly By TopPageNotification = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        #region General section

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
        readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
        readonly By Careconsentgivenid = By.XPath("//*[@id='CWField_careconsentgivenid']");
        readonly By Careconsentgivenid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careconsentgivenid']/label/span");
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

        readonly By Carenonconsentid = By.XPath("//*[@id='CWField_carenonconsentid']");
        readonly By Carenonconsentid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carenonconsentid']/label/span");

        readonly By Reasonforabsence = By.XPath("//*[@id='CWField_reasonforabsence']");
        readonly By Reasonforabsence_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reasonforabsence']/label/span");

        readonly By Reasonconsentdeclined = By.XPath("//*[@id='CWField_reasonconsentdeclined']");
        readonly By Reasonconsentdeclined_ErrorLabel = By.XPath("//*[@id='CWControlHolder_reasonconsentdeclined']/label/span");
        readonly By Encouragementgiven = By.XPath("//*[@id='CWField_encouragementgiven']");
        readonly By Encouragementgiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_encouragementgiven']/label/span");
        readonly By Careprovidedwithoutconsent_1 = By.XPath("//*[@id='CWField_careprovidedwithoutconsent_1']");
        readonly By Careprovidedwithoutconsent_0 = By.XPath("//*[@id='CWField_careprovidedwithoutconsent_0']");

        readonly By Deferredtodate = By.XPath("//*[@id='CWField_deferredtodate']");
        readonly By Deferredtodate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtodate']/label/span");
        readonly By Timeorshiftid = By.XPath("//*[@id='CWField_timeorshiftid']");
        readonly By Timeorshiftid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timeorshiftid']/label/span");
        readonly By Deferredtotime = By.XPath("//*[@id='CWField_deferredtotime']");
        readonly By Deferredtotime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");
        readonly By DeferredtoselectedshiftidLink = By.XPath("//*[@id='CWField_deferredtoselectedshiftid_Link']");
        readonly By DeferredtoselectedshiftidLookupButton = By.XPath("//*[@id='CWLookupBtn_deferredtoselectedshiftid']");
        readonly By Deferredtoselectedshiftid_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoselectedshiftid']/label/span");

        #endregion

        #region Details section

        readonly By SwallowAssessmentRatingTextAreaField = By.XPath("//*[@id='CWField_swallowassessmentrating']");
        readonly By MealTypeLink = By.XPath("//*[@id='CWField_mealtypeid_Link']");
        readonly By MealTypeClearButton = By.XPath("//*[@id='CWClearLookup_mealtypeid']");
        readonly By MealTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_mealtypeid']");
        readonly By MealTypeOther = By.XPath("//*[@id='CWField_mealtypeother']");

        readonly By MealTypeRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_mealtypeid']/label/span");
        readonly By MealTypeOther_ErrorLabel = By.XPath("//*[@id='CWControlHolder_mealtypeother']/label/span");

        #endregion

        #region Food section

        readonly By TypeOfFood = By.XPath("//*[@id='CWField_typeoffood']");
        readonly By AmountOfFoodOfferedLink = By.XPath("//*[@id='CWField_amountoffoodofferedid_Link']");
        readonly By AmountOfFoodOfferedClearButton = By.XPath("//*[@id='CWClearLookup_amountoffoodofferedid']");
        readonly By AmountOfFoodOfferedLookupButton = By.XPath("//*[@id='CWLookupBtn_amountoffoodofferedid']");
        readonly By AmountOfFoodOther = By.XPath("//*[@id='CWField_amountoffoodother']");
        readonly By AmountOfFoodEatenLink = By.XPath("//*[@id='CWField_amountoffoodeatenid_Link']");
        readonly By AmountOfFoodEatenClearButton = By.XPath("//*[@id='CWClearLookup_amountoffoodeatenid']");
        readonly By AmountOfFoodEatenLookupButton = By.XPath("//*[@id='CWLookupBtn_amountoffoodeatenid']");
        //AmountOfFoodOffered Required Error Label
        readonly By AmountOfFoodOffered_ErrorLabel = By.XPath("//*[@id='CWControlHolder_amountoffoodofferedid']/label/span");
        //AmountOfFoodOther Required Error Label
        readonly By AmountOfFoodOther_ErrorLabel = By.XPath("//*[@id='CWControlHolder_amountoffoodother']/label/span");
        //AmountOfFoodEaten Required Error Label
        readonly By AmountOfFoodEaten_ErrorLabel = By.XPath("//*[@id='CWControlHolder_amountoffoodeatenid']/label/span");

        #endregion

        #region Fluid section

        readonly By TypeoffluididLink = By.XPath("//*[@id='CWField_typeoffluidid_Link']");
        readonly By TypeoffluididClearButton = By.XPath("//*[@id='CWClearLookup_typeoffluidid']");
        readonly By TypeoffluididLookupButton = By.XPath("//*[@id='CWLookupBtn_typeoffluidid']");
        readonly By Typeoffluidother = By.XPath("//*[@id='CWField_typeoffluidother']");
        readonly By AmountoffluidofferedidLink = By.XPath("//*[@id='CWField_amountoffluidofferedid_Link']");
        readonly By AmountoffluidofferedidClearButton = By.XPath("//*[@id='CWClearLookup_amountoffluidofferedid']");
        readonly By AmountoffluidofferedidLookupButton = By.XPath("//*[@id='CWLookupBtn_amountoffluidofferedid']");
        readonly By Amountoffluidother = By.XPath("//*[@id='CWField_amountoffluidother']");
        readonly By Amountoffluiddrank = By.XPath("//*[@id='CWField_amountoffluiddrank']");
        //Typeoffluidother Required Error Label
        readonly By TypeofFluidOther_ErrorLabel = By.XPath("//*[@id='CWControlHolder_typeoffluidother']/label/span");
        //Amountoffluidofferedid Required Error Label
        readonly By AmountOfFluidOffered_ErrorLabel = By.XPath("//*[@id='CWControlHolder_amountoffluidofferedid']/label/span");
        //amountoffluidother Required Error Label
        readonly By AmountOfFluidOther_ErrorLabel = By.XPath("//*[@id='CWControlHolder_amountoffluidother']/label/span");
        //Amountoffluiddrank Required Error Label
        readonly By AmountOfFluidDrank_ErrorLabel = By.XPath("//*[@id='CWControlHolder_amountoffluiddrank']/label/span");

        #endregion

        #region Non-Oral section

        readonly By NonoralfluiddeliveryidLink = By.XPath("//*[@id='CWField_nonoralfluiddeliveryid_Link']");
        readonly By NonoralfluiddeliveryidClearButton = By.XPath("//*[@id='CWClearLookup_nonoralfluiddeliveryid']");
        readonly By NonoralfluiddeliveryidLookupButton = By.XPath("//*[@id='CWLookupBtn_nonoralfluiddeliveryid']");
        readonly By WhatWasGivenTextField = By.XPath("//*[@id='CWField_typeofnonoralfluid']");
        readonly By Expirydate = By.XPath("//*[@id='CWField_expirydate']");
        readonly By ExpirydateDatePicker = By.XPath("//*[@id='CWField_expirydate_DatePicker']");
        readonly By Fluidamountgiven = By.XPath("//*[@id='CWField_fluidamountgiven']");
        readonly By Fluidrate = By.XPath("//*[@id='CWField_fluidrate']");
        readonly By FluidgivenbysystemuseridLink = By.XPath("//*[@id='CWField_fluidgivenbysystemuserid_Link']");
        readonly By FluidgivenbysystemuseridClearButton = By.XPath("//*[@id='CWClearLookup_fluidgivenbysystemuserid']");
        readonly By FluidgivenbysystemuseridLookupButton = By.XPath("//*[@id='CWLookupBtn_fluidgivenbysystemuserid']");
        //Fluidamountgiven Required Error Label
        readonly By FluidAmountGiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_fluidamountgiven']/label/span");
        //Fluidrate Required Error Label
        readonly By FluidRateRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_fluidrate']/label/span");
        //What was given Required Error Label
        readonly By WhatWasGiven_ErrorLabel = By.XPath("//*[@id='CWControlHolder_typeofnonoralfluid']/label/span");
        //Expirydate Required Error Label
        readonly By ExpiryDate_ErrorLabel = By.XPath("//*[@id='CWControlHolder_expirydate']/label/span");
        #endregion

        #region Additional Information section

        By Location_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocations_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Location_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocations_" + ElementId + "']/a[text()='Remove']");
        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocations']");
        readonly By Location_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carephysicallocations']/label/span");

        readonly By locationifother = By.XPath("//*[@id='CWField_locationifother']");
        readonly By LocationIfOther_ErrorLabel = By.XPath("//*[@id='CWControlHolder_locationifother']/label/span");
        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By Wellbeing_ErrorLabel = By.XPath("//*[@id='CWControlHolder_carewellbeingid']/label/span");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");
        readonly By actiontaken = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By ActionTaken_ErrorLabel = By.XPath("//*[@id='CWControlHolder_actiontaken']/label/span");
        readonly By Timespentwithclient = By.XPath("//*[@id='CWField_timespentwithclient']");
        readonly By TotalTimeSpentWithPerson_ErrorLabel = By.XPath("//*[@id='CWControlHolder_timespentwithclient']/label/span");
        readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");
        By Equipment_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_equipments_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Equipment_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_equipments_" + ElementId + "']/a[text()='Remove']");
        readonly By EquipmentsLookupButton = By.XPath("//*[@id='CWLookupBtn_equipments']");
        readonly By EquipmentIfOther = By.XPath("//*[@id='CWField_equipmentifother']");
        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_assistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_assistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_assistanceneededid']");
        readonly By AssistanceNeeded_ErrorLabel = By.XPath("//*[@id='CWControlHolder_assistanceneededid']/label/span");
        readonly By careassistancelevelid = By.XPath("//*[@id='CWField_careassistancelevelid']");
        readonly By AssistanceAmount_ErrorLabel = By.XPath("//*[@id='CWControlHolder_careassistancelevelid']/label/span");

        By StaffRequired_SelectedElementLinkBeforeSave(string ElementId, string ElementText) => By.XPath("//*[@id='MS_staffrequired_" + ElementId + "'][text()='" + ElementText + "']");
        By StaffRequired_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_staffrequired_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By StaffRequired_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_staffrequired_" + ElementId + "']/a[text()='Remove']");
        readonly By staffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");

        //Equipment Required Error Label
        readonly By EquipmentRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_equipments']/label/span");
        //Equipment If Other Required Error Label
        readonly By EquipmentIfOtherRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_equipmentifother']/label/span");
        //Staff Required Required Error Label
        readonly By StaffRequiredRequired_ErrorLabel = By.XPath("//*[@id='CWControlHolder_staffrequired']/label/span");

        #endregion

        #region Care Note

        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");

        #endregion

        #region Care Needs

        readonly By LinkedadlcategoriesidLookupButton = By.XPath("//*[@id='CWLookupBtn_linkedactivitiesofdailyliving']");
        By LinkedAdl_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_linkedactivitiesofdailyliving_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By LinkedAdl_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_linkedactivitiesofdailyliving_" + ElementId + "']/a[text()='Remove']");

        #endregion

        #region Handover

        readonly By Isincludeinnexthandover_1 = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
        readonly By Isincludeinnexthandover_0 = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
        readonly By Flagrecordforhandover_1 = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
        readonly By Flagrecordforhandover_0 = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion

        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion


        public FoodAndFluidRecordPage WaitForPageToLoad()
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


        public FoodAndFluidRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            Click(BackButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            Click(AssignRecordButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }


        public FoodAndFluidRecordPage ValidateTopPageNotificationText(string ExpectedText)
        {
            ValidateElementText(TopPageNotification, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTopPageNotificationVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TopPageNotification);
            else
                WaitForElementNotVisible(TopPageNotification, 3);

            return this;
        }


        public FoodAndFluidRecordPage ClickPersonLink()
        {
            WaitForElementToBeClickable(PersonidLink);
            Click(PersonidLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidatePersonLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(PersonidLink);
            ValidateElementText(PersonidLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickPersonLookupButton()
        {
            WaitForElementToBeClickable(PersonidLookupButton);
            Click(PersonidLookupButton);

            return this;
        }

        //verify Person lookup button is visible
        public FoodAndFluidRecordPage ValidatePersonLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(PersonidLookupButton);
            else
                WaitForElementNotVisible(PersonidLookupButton, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidatePreferencesText(string ExpectedText)
        {
            WaitForElement(Preferences);
            var elementValue = this.GetElementValueByJavascript("CWField_preferences");
            Assert.AreEqual(ExpectedText, elementValue);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnPreferences(string TextToInsert)
        {
            WaitForElementToBeClickable(Preferences);
            SendKeys(Preferences, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementToBeClickable(Carenonconsentid);
            SelectPicklistElementByText(Carenonconsentid, TextToSelect);

            return this;
        }

        public FoodAndFluidRecordPage ValidateNonConsentDetailSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Carenonconsentid, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateNonConsentDetailErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Carenonconsentid_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateNonConsentDetailErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Carenonconsentid_ErrorLabel);
            else
                WaitForElementNotVisible(Carenonconsentid_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateReasonForAbsenceText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnReasonForAbsence(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonforabsence);
            SendKeys(Reasonforabsence, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateReasonForAbsenceErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonforabsence_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateReasonForAbsenceErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonforabsence_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonforabsence_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateReasonConsentDeclinedText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementToBeClickable(Reasonconsentdeclined);
            SendKeys(Reasonconsentdeclined, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateReasonConsentDeclinedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Reasonconsentdeclined_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateReasonConsentDeclinedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Reasonconsentdeclined_ErrorLabel);
            else
                WaitForElementNotVisible(Reasonconsentdeclined_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEncouragementGivenText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnEncouragementGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Encouragementgiven);
            SendKeys(Encouragementgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEncouragementGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Encouragementgiven_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEncouragementGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Encouragementgiven_ErrorLabel);
            else
                WaitForElementNotVisible(Encouragementgiven_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_1);
            Click(Careprovidedwithoutconsent_1);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_1);
            ValidateElementNotChecked(Careprovidedwithoutconsent_1);

            return this;
        }

        public FoodAndFluidRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(Careprovidedwithoutconsent_0);
            Click(Careprovidedwithoutconsent_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(Careprovidedwithoutconsent_0);
            ValidateElementNotChecked(Careprovidedwithoutconsent_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToDateText(string ExpectedText)
        {
            ValidateElementValue(Deferredtodate, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertDeferredToDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtodate);
            SendKeys(Deferredtodate, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtodate_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtodate_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtodate_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementToBeClickable(Timeorshiftid);
            SelectPicklistElementByText(Timeorshiftid, TextToSelect);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToTimeOrShiftSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Timeorshiftid, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToTimeOrShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Timeorshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToTimeOrShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timeorshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Timeorshiftid_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToTimeText(string ExpectedText)
        {
            ValidateElementValue(Deferredtotime, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertDeferredToTime(string TextToInsert)
        {
            WaitForElementToBeClickable(Deferredtotime);
            SendKeys(Deferredtotime, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToTimeErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtotime_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToTimeErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtotime_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtotime_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickDeferredToShiftLink()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            Click(DeferredtoselectedshiftidLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLink);
            ValidateElementText(DeferredtoselectedshiftidLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(DeferredtoselectedshiftidLookupButton);
            Click(DeferredtoselectedshiftidLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToShiftErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Deferredtoselectedshiftid_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDeferredToShiftErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Deferredtoselectedshiftid_ErrorLabel);
            else
                WaitForElementNotVisible(Deferredtoselectedshiftid_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage SelectConsentGiven(string TextToSelect)
        {
            WaitForElementToBeClickable(Careconsentgivenid);
            SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

            return this;
        }

        public FoodAndFluidRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(Careconsentgivenid, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateConsentGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Careconsentgivenid_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateConsentGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if(ExpectVisible)
                WaitForElementVisible(Careconsentgivenid_ErrorLabel);
            else
                WaitForElementNotVisible(Careconsentgivenid_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickResponsibleTeamLink()
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            Click(ResponsibleTeamLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeamLink);
            ValidateElementText(ResponsibleTeamLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeamLookupButton);
            Click(ResponsibleTeamLookupButton);

            return this;
        }

        //verify Responsible Team lookup button is visible
        public FoodAndFluidRecordPage ValidateResponsibleTeamLookupButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ResponsibleTeamLookupButton);
            else
                WaitForElementNotVisible(ResponsibleTeamLookupButton, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDateAndTimeOccurred_DateText(string ExpectedText)
        {
            ValidateElementValue(Occurred, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnDateAndTimeOccurred_Date(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred);
            SendKeys(Occurred, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickDateAndTimeOccurredDatePicker()
        {
            WaitForElementToBeClickable(OccurredDatePicker);
            Click(OccurredDatePicker);

            return this;
        }

        public FoodAndFluidRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
        {
            ValidateElementValue(Occurred_Time, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnDateAndTimeOccurred_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Occurred_Time);
            SendKeys(Occurred_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickDateAndTimeOccurred_Time_TimePicker()
        {
            WaitForElementToBeClickable(Occurred_Time_TimePicker);
            Click(Occurred_Time_TimePicker);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCreatedOnText(string ExpectedText)
        {
            ValidateElementValue(Createdon, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnCreatedOn(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon);
            SendKeys(Createdon, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickCreatedOnDatePicker()
        {
            WaitForElementToBeClickable(CreatedonDatePicker);
            Click(CreatedonDatePicker);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCreatedOn_TimeText(string ExpectedText)
        {
            ValidateElementValue(Createdon_Time, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnCreatedOn_Time(string TextToInsert)
        {
            WaitForElementToBeClickable(Createdon_Time);
            SendKeys(Createdon_Time, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickCreatedOn_Time_TimePicker()
        {
            WaitForElementToBeClickable(Createdon_Time_TimePicker);
            Click(Createdon_Time_TimePicker);

            return this;
        }

        public FoodAndFluidRecordPage ValidateSwallowAssessmentRatingText(string ExpectedText)
        {
            WaitForElement(SwallowAssessmentRatingTextAreaField);
            ScrollToElement(SwallowAssessmentRatingTextAreaField);
            var ActualText = GetElementValueByJavascript("CWField_swallowassessmentrating");
            Assert.AreEqual(ExpectedText, ActualText);

            return this;
        }

        //verify SwallowAssessmentRatingTextAreaField is visible
        public FoodAndFluidRecordPage ValidateSwallowAssessmentRatingVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(SwallowAssessmentRatingTextAreaField);
            else
                WaitForElementNotVisible(SwallowAssessmentRatingTextAreaField, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickMealtypeLink()
        {
            WaitForElementToBeClickable(MealTypeLink);
            Click(MealTypeLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateMealTypeLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(MealTypeLink);
            ValidateElementText(MealTypeLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickMealTypeClearButton()
        {
            WaitForElementToBeClickable(MealTypeClearButton);
            Click(MealTypeClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickMealTypeLookupButton()
        {
            WaitForElement(MealTypeLookupButton);
            ScrollToElement(MealTypeLookupButton);
            Click(MealTypeLookupButton);

            return this;
        }

        //Inser text in MealTypeOther text field
        public FoodAndFluidRecordPage InsertTextOnMealTypeOther(string TextToInsert)
        {
            WaitForElementToBeClickable(MealTypeOther);
            SendKeys(MealTypeOther, TextToInsert + Keys.Tab);

            return this;
        }

        //verify text in MealTypeOther field
        public FoodAndFluidRecordPage ValidateMealTypeOtherText(string ExpectedText)
        {
            ValidateElementText(MealTypeOther, ExpectedText);

            return this;
        }

        //verify mealtypeother field is visible
        public FoodAndFluidRecordPage ValidateMealTypeOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MealTypeOther);
            else
                WaitForElementNotVisible(MealTypeOther, 3);

            return this;
        }

        //verify MealTypeRequired_ErrorLabel text
        public FoodAndFluidRecordPage ValidateMealTypeRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(MealTypeRequired_ErrorLabel, ExpectedText);

            return this;
        }

        //verify MealTypeRequired_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateMealTypeRequiredErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MealTypeRequired_ErrorLabel);
            else
                WaitForElementNotVisible(MealTypeRequired_ErrorLabel, 3);

            return this;
        }

        //verify MealTypeOther_ErrorLabel text
        public FoodAndFluidRecordPage ValidateMealTypeOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(MealTypeOther_ErrorLabel, ExpectedText);

            return this;
        }

        //verify MealTypeOther_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateMealTypeOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(MealTypeOther_ErrorLabel);
            else
                WaitForElementNotVisible(MealTypeOther_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTypeOfFoodText(string ExpectedText)
        {
            WaitForElement(TypeOfFood);
            ScrollToElement(TypeOfFood);
            ValidateElementText(TypeOfFood, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnTypeOfFood(string TextToInsert)
        {
            WaitForElementToBeClickable(TypeOfFood);
            SendKeys(TypeOfFood, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFoodOfferedLink()
        {
            WaitForElementToBeClickable(AmountOfFoodOfferedLink);
            Click(AmountOfFoodOfferedLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAmountOfFoodOfferedLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(AmountOfFoodOfferedLink);
            ValidateElementText(AmountOfFoodOfferedLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFoodOfferedClearButton()
        {
            WaitForElementToBeClickable(AmountOfFoodOfferedClearButton);
            Click(AmountOfFoodOfferedClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFoodOfferedLookupButton()
        {
            WaitForElementToBeClickable(AmountOfFoodOfferedLookupButton);
            Click(AmountOfFoodOfferedLookupButton);

            return this;
        }

        //Insert text in AmountOfFoodOther text field
        public FoodAndFluidRecordPage InsertTextOnAmountOfFoodOther(string TextToInsert)
        {
            WaitForElementToBeClickable(AmountOfFoodOther);
            SendKeys(AmountOfFoodOther, TextToInsert + Keys.Tab);

            return this;
        }

        //verify text in AmountOfFoodOther field
        public FoodAndFluidRecordPage ValidateAmountOfFoodOtherText(string ExpectedText)
        {
            ValidateElementText(AmountOfFoodOther, ExpectedText);

            return this;
        }

        //verify AmountOfFoodOther field is visible
        public FoodAndFluidRecordPage ValidateAmountOfFoodOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFoodOther);
            else
                WaitForElementNotVisible(AmountOfFoodOther, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFoodEatenLink()
        {
            WaitForElementToBeClickable(AmountOfFoodEatenLink);
            Click(AmountOfFoodEatenLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAmountOfFoodEatenLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(AmountOfFoodEatenLink);
            ValidateElementText(AmountOfFoodEatenLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFoodEatenClearButton()
        {
            WaitForElementToBeClickable(AmountOfFoodEatenClearButton);
            Click(AmountOfFoodEatenClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFoodEatenLookupButton()
        {
            WaitForElementToBeClickable(AmountOfFoodEatenLookupButton);
            Click(AmountOfFoodEatenLookupButton);

            return this;
        }

        //verify AmountOfFoodOffered_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountOfFoodOfferedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AmountOfFoodOffered_ErrorLabel, ExpectedText);

            return this;
        }

        //verify AmountOfFoodOffered_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateAmountOfFoodOfferedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFoodOffered_ErrorLabel);
            else
                WaitForElementNotVisible(AmountOfFoodOffered_ErrorLabel, 3);

            return this;
        }

        //verify AmountOfFoodOther_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountOfFoodOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AmountOfFoodOther_ErrorLabel, ExpectedText);

            return this;
        }

        //verify AmountOfFoodOther_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateAmountOfFoodOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFoodOther_ErrorLabel);
            else
                WaitForElementNotVisible(AmountOfFoodOther_ErrorLabel, 3);

            return this;
        }

        //verify AmountOfFoodEaten_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountOfFoodEatenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AmountOfFoodEaten_ErrorLabel, ExpectedText);

            return this;
        }

        //verify AmountOfFoodEaten_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateAmountOfFoodEatenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFoodEaten_ErrorLabel);
            else
                WaitForElementNotVisible(AmountOfFoodEaten_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickTypeOfFluidLink()
        {
            WaitForElementToBeClickable(TypeoffluididLink);
            Click(TypeoffluididLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTypeOfFluidLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(TypeoffluididLink);
            ValidateElementText(TypeoffluididLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickTypeOfFluidClearButton()
        {
            WaitForElementToBeClickable(TypeoffluididClearButton);
            Click(TypeoffluididClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickTypeOfFluidLookupButton()
        {
            WaitForElementToBeClickable(TypeoffluididLookupButton);
            Click(TypeoffluididLookupButton);

            return this;
        }

        //Insert text in TypeOfFluidOther text field
        public FoodAndFluidRecordPage InsertTextOnTypeOfFluidOther(string TextToInsert)
        {
            WaitForElementToBeClickable(Typeoffluidother);
            SendKeys(Typeoffluidother, TextToInsert + Keys.Tab);

            return this;
        }

        //verify text in TypeOfFluidOther field
        public FoodAndFluidRecordPage ValidateTypeOfFluidOtherText(string ExpectedText)
        {
            ValidateElementText(Typeoffluidother, ExpectedText);

            return this;
        }

        //verify TypeOfFluidOther field is visible
        public FoodAndFluidRecordPage ValidateTypeOfFluidOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Typeoffluidother);
            else
                WaitForElementNotVisible(Typeoffluidother, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFluidOfferedLink()
        {
            WaitForElementToBeClickable(AmountoffluidofferedidLink);
            Click(AmountoffluidofferedidLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAmountOfFluidOfferedLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(AmountoffluidofferedidLink);
            ValidateElementText(AmountoffluidofferedidLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFluidOfferedClearButton()
        {
            WaitForElementToBeClickable(AmountoffluidofferedidClearButton);
            Click(AmountoffluidofferedidClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickAmountOfFluidOfferedLookupButton()
        {
            WaitForElementToBeClickable(AmountoffluidofferedidLookupButton);
            Click(AmountoffluidofferedidLookupButton);

            return this;
        }

        //Insert text in AmountOfFluidOther text field
        public FoodAndFluidRecordPage InsertTextOnAmountOfFluidOther(string TextToInsert)
        {
            WaitForElementToBeClickable(Amountoffluidother);
            SendKeys(Amountoffluidother, TextToInsert + Keys.Tab); 

            return this;
        }

        //verify text in AmountOfFluidOther field
        public FoodAndFluidRecordPage ValidateAmountOfFluidOtherText(string ExpectedText)
        {
            ValidateElementText(Amountoffluidother, ExpectedText);

            return this;
        }

        //verify AmountOfFluidOther field is visible
        public FoodAndFluidRecordPage ValidateAmountOfFluidOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Amountoffluidother);
            else
                WaitForElementNotVisible(Amountoffluidother, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAmountOfFluidDrankText(string ExpectedText)
        {
            ValidateElementValue(Amountoffluiddrank, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnAmountOfFluidDrank(string TextToInsert)
        {
            WaitForElementToBeClickable(Amountoffluiddrank);
            SendKeys(Amountoffluiddrank, TextToInsert + Keys.Tab);

            return this;
        }

        //Verify TypeofFluidOther_ErrorLabel text
        public FoodAndFluidRecordPage ValidateTypeofFluidOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TypeofFluidOther_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify TypeofFluidOther_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateTypeofFluidOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TypeofFluidOther_ErrorLabel);
            else
                WaitForElementNotVisible(TypeofFluidOther_ErrorLabel, 3);

            return this;
        }

        //Verify AmountOfFluidOffered_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountOfFluidOfferedErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AmountOfFluidOffered_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify AmountOfFluidOffered_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateAmountOfFluidOfferedErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFluidOffered_ErrorLabel);
            else
                WaitForElementNotVisible(AmountOfFluidOffered_ErrorLabel, 3);

            return this;
        }

        //Verify amountoffluidother field is visible
        public FoodAndFluidRecordPage ValidateAmountOfFluidOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFluidOther_ErrorLabel);
            else
                WaitForElementNotVisible(AmountOfFluidOther_ErrorLabel, 3);

            return this;
        }

        //Verify AmountOfFluidOther_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountOfFluidOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AmountOfFluidOther_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify AmountOfFluidDrank_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountOfFluidDrankErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AmountOfFluidDrank_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify AmountOfFluidDrank_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateAmountOfFluidDrankErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AmountOfFluidDrank_ErrorLabel);
            else
                WaitForElementNotVisible(AmountOfFluidDrank_ErrorLabel, 3);

            return this;
        }


        public FoodAndFluidRecordPage ClickNonOralFluidDeliveryLink()
        {
            WaitForElementToBeClickable(NonoralfluiddeliveryidLink);
            Click(NonoralfluiddeliveryidLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateNonOralFluidDeliveryLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(NonoralfluiddeliveryidLink);
            ValidateElementText(NonoralfluiddeliveryidLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickNonOralFluidDeliveryClearButton()
        {
            WaitForElementToBeClickable(NonoralfluiddeliveryidClearButton);
            Click(NonoralfluiddeliveryidClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickNonOralFluidDeliveryLookupButton()
        {
            WaitForElementToBeClickable(NonoralfluiddeliveryidLookupButton);
            Click(NonoralfluiddeliveryidLookupButton);

            return this;
        }

        //ValidateTypeofnonoralfluidText
        public FoodAndFluidRecordPage ValidateWhatWasGivenText(string ExpectedText)
        {
            ValidateElementText(WhatWasGivenTextField, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnWhatWasGivenTextField(string TextToInsert)
        {
            WaitForElementToBeClickable(WhatWasGivenTextField);
            SendKeys(WhatWasGivenTextField, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateExpirydateText(string ExpectedText)
        {
            ValidateElementValue(Expirydate, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnExpirydate(string TextToInsert)
        {
            WaitForElementToBeClickable(Expirydate);
            SendKeys(Expirydate, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickExpirydateDatePicker()
        {
            WaitForElementToBeClickable(ExpirydateDatePicker);
            Click(ExpirydateDatePicker);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFluidAmountGivenText(string ExpectedText)
        {
            ValidateElementValue(Fluidamountgiven, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnFluidAmountGiven(string TextToInsert)
        {
            WaitForElementToBeClickable(Fluidamountgiven);
            SendKeys(Fluidamountgiven, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFluidRateText(string ExpectedText)
        {
            ValidateElementValue(Fluidrate, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnFluidRate(string TextToInsert)
        {
            WaitForElementToBeClickable(Fluidrate);
            SendKeys(Fluidrate, TextToInsert + Keys.Tab);

            return this;
        }

        //Verify FluidAmountGiven_ErrorLabel is visible
        public FoodAndFluidRecordPage ValidateAmountGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(FluidAmountGiven_ErrorLabel);
            else
                WaitForElementNotVisible(FluidAmountGiven_ErrorLabel, 3);

            return this;
        }

        //Verify FluidAmountGiven_ErrorLabel text
        public FoodAndFluidRecordPage ValidateAmountGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(FluidAmountGiven_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify WhatWasGiven_ErrorLabel is visible
        public FoodAndFluidRecordPage ValidateWhatWasGivenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(WhatWasGiven_ErrorLabel);
            else
                WaitForElementNotVisible(WhatWasGiven_ErrorLabel, 3);

            return this;
        }

        //Verify WhatWasGiven_ErrorLabel text
        public FoodAndFluidRecordPage ValidateWhatWasGivenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(WhatWasGiven_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify FluidRate_ErrorLabel is visible
        public FoodAndFluidRecordPage ValidateFluidRateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(FluidRateRequired_ErrorLabel);
            else
                WaitForElementNotVisible(FluidRateRequired_ErrorLabel, 3);

            return this;
        }

        //Verify FluidRate_ErrorLabel text
        public FoodAndFluidRecordPage ValidateFluidRateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(FluidRateRequired_ErrorLabel, ExpectedText);

            return this;
        }

        //Verify ExpiredDate_ErrorLabel is visible
        public FoodAndFluidRecordPage ValidateExpiryDateErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ExpiryDate_ErrorLabel);
            else
                WaitForElementNotVisible(ExpiryDate_ErrorLabel, 3);

            return this;
        }

        //Verify ExpiryDate_ErrorLabel text
        public FoodAndFluidRecordPage ValidateExpiryDateErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ExpiryDate_ErrorLabel, ExpectedText);

            return this;
        }


        public FoodAndFluidRecordPage ClickFluidGivenBySystemUserLink()
        {
            WaitForElementToBeClickable(FluidgivenbysystemuseridLink);
            Click(FluidgivenbysystemuseridLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFluidGivenBySystemUserLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(FluidgivenbysystemuseridLink);
            ValidateElementText(FluidgivenbysystemuseridLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickFluidGivenBySystemUserClearButton()
        {
            WaitForElementToBeClickable(FluidgivenbysystemuseridClearButton);
            Click(FluidgivenbysystemuseridClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickFluidGivenBySystemUserLookupButton()
        {
            WaitForElementToBeClickable(FluidgivenbysystemuseridLookupButton);
            Click(FluidgivenbysystemuseridLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            Click(Location_SelectedElementLink(ElementId));

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Location_SelectedElementLink(ElementId));
            ValidateElementText(Location_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public FoodAndFluidRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Location_SelectedElementRemoveButton(ElementId));
            Click(Location_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public FoodAndFluidRecordPage ClickLocationLookupButton()
        {
            WaitForElementToBeClickable(CarephysicallocationidLookupButton);
            Click(CarephysicallocationidLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocationIfOtherText(string ExpectedText)
        {
            ValidateElementText(locationifother, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnLocationIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(locationifother);
            SendKeys(locationifother, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocationIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(locationifother);
            else
                WaitForElementNotVisible(locationifother, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickEquipmentsLookupButton()
        {
            WaitForElementToBeClickable(EquipmentsLookupButton);
            Click(EquipmentsLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickEquipment_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            Click(Equipment_SelectedElementLink(ElementId));

            return this;
        }

        public FoodAndFluidRecordPage ValidateEquipment_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            ValidateElementText(Equipment_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEquipment_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateEquipment_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public FoodAndFluidRecordPage ClickEquipment_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementRemoveButton(ElementId));
            Click(Equipment_SelectedElementRemoveButton(ElementId));

            return this;
        }

        //verify EquipmentRequired_ErrorLabel visibility
        public FoodAndFluidRecordPage ValidateEquipmentRequiredErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EquipmentRequired_ErrorLabel);
            else
                WaitForElementNotVisible(EquipmentRequired_ErrorLabel, 3);

            return this;
        }

        //verify EquipmentRequired_ErrorLabel text
        public FoodAndFluidRecordPage ValidateEquipmentRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EquipmentRequired_ErrorLabel, ExpectedText);

            return this;
        }

        //verify EquipmentIfOther field is visible
        public FoodAndFluidRecordPage ValidateEquipmentIfOtherVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EquipmentIfOther);
            else
                WaitForElementNotVisible(EquipmentIfOther, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEquipmentIfOtherText(string ExpectedText)
        {
            ValidateElementText(EquipmentIfOther, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnEquipmentIfOther(string TextToInsert)
        {
            WaitForElementToBeClickable(EquipmentIfOther);
            SendKeys(EquipmentIfOther, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEquipmentIfOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(EquipmentIfOtherRequired_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateEquipmentIfOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(EquipmentIfOtherRequired_ErrorLabel);
            else
                WaitForElementNotVisible(EquipmentIfOtherRequired_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickWellbeingLookupButton()
        {
            WaitForElementToBeClickable(CarewellbeingidLookupButton);
            Click(CarewellbeingidLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage ValidateActionTakenText(string ExpectedText)
        {
            ValidateElementText(actiontaken, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnActionTaken(string TextToInsert)
        {
            WaitForElementToBeClickable(actiontaken);
            SendKeys(actiontaken, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateActionTakenVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(actiontaken);
            else
                WaitForElementNotVisible(actiontaken, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTotalTimeSpentWithPersonText(string ExpectedText)
        {
            ValidateElementValue(Timespentwithclient, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnTotalTimeSpentWithPerson(string TextToInsert)
        {
            WaitForElementToBeClickable(Timespentwithclient);
            SendKeys(Timespentwithclient, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTotalTimeSpentWithPersonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Timespentwithclient);
            else
                WaitForElementNotVisible(Timespentwithclient, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAdditionalNotesText(string ExpectedText)
        {
            ValidateElementText(Additionalnotes, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
        {
            WaitForElementToBeClickable(Additionalnotes);
            SendKeys(Additionalnotes, TextToInsert + Keys.Tab);

            return this;
        }

        public FoodAndFluidRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public FoodAndFluidRecordPage ClickAssistanceNeededLookupButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidLookupButton);
            Click(CareassistanceneededidLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage SelectAssistanceAmount(string TextToSelect)
        {
            WaitForElementToBeClickable(careassistancelevelid);
            SelectPicklistElementByText(careassistancelevelid, TextToSelect);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceAmountSelectedText(string ExpectedText)
        {
            ValidatePicklistSelectedText(careassistancelevelid, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceAmountVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(careassistancelevelid);
            else
                WaitForElementNotVisible(careassistancelevelid, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocationErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Location_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocationErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Location_ErrorLabel);
            else
                WaitForElementNotVisible(Location_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocationIfOtherErrorLabelText(string ExpectedText)
        {
            ValidateElementText(LocationIfOther_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLocationIfOtherErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(LocationIfOther_ErrorLabel);
            else
                WaitForElementNotVisible(LocationIfOther_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateWellbeingErrorLabelText(string ExpectedText)
        {
            ValidateElementText(Wellbeing_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateWellbeingErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(Wellbeing_ErrorLabel);
            else
                WaitForElementNotVisible(Wellbeing_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateActionTakenErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ActionTaken_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateActionTakenErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ActionTaken_ErrorLabel);
            else
                WaitForElementNotVisible(ActionTaken_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTotalTimeSpentWithPersonErrorLabelText(string ExpectedText)
        {
            ValidateElementText(TotalTimeSpentWithPerson_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateTotalTimeSpentWithPersonErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(TotalTimeSpentWithPerson_ErrorLabel);
            else
                WaitForElementNotVisible(TotalTimeSpentWithPerson_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceNeededErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceNeeded_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceNeededErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceNeeded_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceNeeded_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceAmountErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AssistanceAmount_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateAssistanceAmountErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(AssistanceAmount_ErrorLabel);
            else
                WaitForElementNotVisible(AssistanceAmount_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ClickStaffRequired_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            Click(StaffRequired_SelectedElementLink(ElementId));

            return this;
        }

        public FoodAndFluidRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLinkBeforeSave(ElementId, ExpectedText));

            return this;
        }

        public FoodAndFluidRecordPage ValidateStaffRequired_SelectedElementLinkTextBeforeSave(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkTextBeforeSave(ElementId.ToString(), ExpectedText);
        }

        public FoodAndFluidRecordPage ValidateStaffRequired_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementLink(ElementId));
            ValidateElementText(StaffRequired_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateStaffRequired_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateStaffRequired_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public FoodAndFluidRecordPage ClickStaffRequired_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(StaffRequired_SelectedElementRemoveButton(ElementId));
            Click(StaffRequired_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public FoodAndFluidRecordPage ClickStaffRequiredLookupButton()
        {
            WaitForElementToBeClickable(staffrequiredLookupButton);
            Click(staffrequiredLookupButton);

            return this;
        }

        public FoodAndFluidRecordPage ValidateStaffRequiredErrorLabelText(string ExpectedText)
        {
            ValidateElementText(StaffRequiredRequired_ErrorLabel, ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateStaffRequiredErrorLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(StaffRequiredRequired_ErrorLabel);
            else
                WaitForElementNotVisible(StaffRequiredRequired_ErrorLabel, 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateCareNoteText(string ExpectedText)
        {
            var elementText = this.GetElementValueByJavascript("CWField_carenote");
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        public FoodAndFluidRecordPage InsertTextOnCareNote(string TextToInsert)
        {
            WaitForElementToBeClickable(Carenote);
            SendKeys(Carenote, TextToInsert + Keys.Tab);
            return this;
        }

        public FoodAndFluidRecordPage ClickLinkedActivitiesOfDailyLivingLookupButton()
        {
            WaitForElement(LinkedadlcategoriesidLookupButton);
            ScrollToElement(LinkedadlcategoriesidLookupButton);
            Click(LinkedadlcategoriesidLookupButton);

            return this;
        }


        public FoodAndFluidRecordPage ValidateLinkedAdlCategories_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(LinkedAdl_SelectedElementLink(ElementId));
            ValidateElementText(LinkedAdl_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public FoodAndFluidRecordPage ValidateLinkedAdlCategories_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLinkedAdlCategories_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public FoodAndFluidRecordPage ClickLinkedAdlCategories_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(LinkedAdl_SelectedElementRemoveButton(ElementId));
            Click(LinkedAdl_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public FoodAndFluidRecordPage ClickIncludeInNextHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_1);
            Click(Isincludeinnexthandover_1);

            return this;
        }

        public FoodAndFluidRecordPage ValidateIncludeInNextHandover_YesRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementChecked(Isincludeinnexthandover_1);

            return this;
        }

        public FoodAndFluidRecordPage ValidateIncludeInNextHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_1);
            ValidateElementNotChecked(Isincludeinnexthandover_1);

            return this;
        }

        public FoodAndFluidRecordPage ClickIncludeInNextHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Isincludeinnexthandover_0);
            Click(Isincludeinnexthandover_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateIncludeInNextHandover_NoRadioButtonChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementChecked(Isincludeinnexthandover_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateIncludeInNextHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Isincludeinnexthandover_0);
            ValidateElementNotChecked(Isincludeinnexthandover_0);

            return this;
        }

        public FoodAndFluidRecordPage ClickFlagRecordForHandover_YesRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_1);
            Click(Flagrecordforhandover_1);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFlagRecordForHandover_YesRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementChecked(Flagrecordforhandover_1);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFlagRecordForHandover_YesRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_1);
            ValidateElementNotChecked(Flagrecordforhandover_1);

            return this;
        }

        public FoodAndFluidRecordPage ClickFlagRecordForHandover_NoRadioButton()
        {
            WaitForElementToBeClickable(Flagrecordforhandover_0);
            Click(Flagrecordforhandover_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFlagRecordForHandover_NoRadioButtonChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementChecked(Flagrecordforhandover_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFlagRecordForHandover_NoRadioButtonNotChecked()
        {
            WaitForElement(Flagrecordforhandover_0);
            ValidateElementNotChecked(Flagrecordforhandover_0);

            return this;
        }

        public FoodAndFluidRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(FieldLabel(FieldName));
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public FoodAndFluidRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }
    }
}

