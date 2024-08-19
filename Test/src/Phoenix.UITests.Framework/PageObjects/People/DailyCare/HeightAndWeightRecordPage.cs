using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
	public class HeightAndWeightRecordPage : CommonMethods
	{

        public HeightAndWeightRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personheightandweight&')]");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Height & Weight: ']");

        #endregion

        #region Options toolbar

        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");

        #endregion

        #region Section menu
        readonly By SectionMenuButton_PreviousMUSTScore = By.XPath("//*[@id='section-menu-button-PreviousMUSTScore']");
		readonly By SectionMenuButton_Height = By.XPath("//*[@id='section-menu-button-Height']");
		readonly By SectionMenuButton_Weight = By.XPath("//*[@id='section-menu-button-Weight']");
		readonly By SectionMenuButton_Length = By.XPath("//*[@id='section-menu-button-Length']");
		readonly By SectionMenuButton_WeightLoss = By.XPath("//*[@id='section-menu-button-WeightLoss']");
		readonly By SectionMenuButton_MUSTMeasurements = By.XPath("//*[@id='section-menu-button-MUSTMeasurements']");
		readonly By SectionMenuButton_MUSTScore = By.XPath("//*[@id='section-menu-button-MUSTScore']");
		readonly By SectionMenuButton_ManagementAndReview = By.XPath("//*[@id='section-menu-button-ManagementAndReview']");
		readonly By SectionMenuButton_AdditionalInformation = By.XPath("//*[@id='section-menu-button-AdditionalInformation']");
		readonly By SectionMenuButton_CareNote = By.XPath("//*[@id='section-menu-button-CareNote']");
		readonly By SectionMenuButton_CareNeeds = By.XPath("//*[@id='section-menu-button-CareNeeds']");
		readonly By SectionMenuButton_Handover = By.XPath("//*[@id='section-menu-button-Handover']");
		readonly By SectionMenuToggleLink = By.XPath("//*[@id='section-menu-toggle-link']");

        #endregion

        #region General
        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
		readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly string preferences_Id = "CWField_preferences";
        readonly By Preferences = By.XPath("//*[@id='CWField_preferences']");
		readonly By Careconsentgivenid = By.XPath("//*[@id='CWField_careconsentgivenid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Datetimetaken = By.XPath("//*[@id='CWField_datetimetaken']");
		readonly By DatetimetakenDatePicker = By.XPath("//*[@id='CWField_datetimetaken_DatePicker']");
		readonly By Datetimetaken_Time = By.XPath("//*[@id='CWField_datetimetaken_Time']");
		readonly By Datetimetaken_Time_TimePicker = By.XPath("//*[@id='CWField_datetimetaken_Time_TimePicker']");

        //Fields displayed when Consent Given = No
        readonly By nonconsentDetail = By.Id("CWField_carenonconsentid");

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
        readonly By deferredToTime_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtotime']/label/span");

        readonly By deferredToShift_LookupButton = By.Id("CWLookupBtn_deferredtoselectedshiftid");
        readonly By deferredToShift_LinkField = By.Id("CWField_deferredtoselectedshiftid_Link");
        readonly By deferredToShift_ClearButton = By.Id("CWClearLookup_deferredtoselectedshiftid");
        readonly By deferredToShift_ErrorLabel = By.XPath("//*[@id='CWControlHolder_deferredtoselectedshiftid']/label/span");

        #endregion

        #region Previous Must score 
        readonly By Musttotalscoreprevious = By.XPath("//*[@id='CWField_musttotalscoreprevious']");
		readonly By Riskprevious = By.XPath("//*[@id='CWField_riskprevious']");
        #endregion

        #region Height
        readonly By Estimatedheight_YesRadioButton = By.XPath("//*[@id='CWField_estimatedheight_1']");
		readonly By Estimatedheight_NoRadioButton = By.XPath("//*[@id='CWField_estimatedheight_0']");
		readonly By Heightmetres = By.XPath("//*[@id='CWField_heightmetres']");
		readonly By Heightfeet = By.XPath("//*[@id='CWField_heightfeet']");
		readonly By Heightinches = By.XPath("//*[@id='CWField_heightinches']");

        #endregion

        #region Weight
        readonly By Estimatedweight_YesRadioButton = By.XPath("//*[@id='CWField_estimatedweight_1']");
		readonly By Estimatedweight_NoRadioButton = By.XPath("//*[@id='CWField_estimatedweight_0']");
		readonly By Weightkilos = By.XPath("//*[@id='CWField_weightkilos']");
		readonly By Weightstones = By.XPath("//*[@id='CWField_weightstones']");
		readonly By Weightpounds = By.XPath("//*[@id='CWField_weightpounds']");
		readonly By Weightounces = By.XPath("//*[@id='CWField_weightounces']");
		readonly By Hasamputation_YesRadioButton = By.XPath("//*[@id='CWField_hasamputation_1']");
		readonly By Hasamputation_NoRadioButton = By.XPath("//*[@id='CWField_hasamputation_0']");
		readonly By Muacmeasurementcm = By.XPath("//*[@id='CWField_muacmeasurementcm']");
        #endregion

        #region Length
        readonly By Lengthfeet = By.XPath("//*[@id='CWField_lengthfeet']");
		readonly By Lengthinches = By.XPath("//*[@id='CWField_lengthinches']");
		readonly By Lengthcentimetres = By.XPath("//*[@id='CWField_lengthcentimetres']");
		readonly By Headcircumference = By.XPath("//*[@id='CWField_headcircumference']");

        #endregion

        #region Weight loss

        readonly By Weightlossstones = By.XPath("//*[@id='CWField_weightlossstones']");
		readonly By Weightlosspounds = By.XPath("//*[@id='CWField_weightlosspounds']");
		readonly string WeightLossKilosId = "CWField_weightlosskilos";
        readonly By Weightlosskilos = By.XPath("//*[@id='CWField_weightlosskilos']");
		readonly string WeightlosspercentId = "CWField_weightlosspercent";
        readonly By Weightlosspercent = By.XPath("//*[@id='CWField_weightlosspercent']");

		#endregion

        #region Other information
		readonly By Ageattimetaken = By.XPath("//*[@id='CWField_ageattimetaken']");
		readonly By Estimatedbmi_YesRadioButton = By.XPath("//*[@id='CWField_estimatedbmi_1']");
		readonly By Estimatedbmi_NoRadioButton = By.XPath("//*[@id='CWField_estimatedbmi_0']");
		readonly By Bmiresult = By.XPath("//*[@id='CWField_bmiresult']");
		readonly By Bmiscore = By.XPath("//*[@id='CWField_bmiscore']");
        #endregion

		#region MUST Measurements 
        readonly By Bmimustscore = By.XPath("//*[@id='CWField_bmimustscore']");
		readonly By Acutediseaseeffect_YesRadioButton = By.XPath("//*[@id='CWField_acutediseaseeffect_1']");
		readonly By Acutediseaseeffect_NoRadioButton = By.XPath("//*[@id='CWField_acutediseaseeffect_0']");
		readonly By Weightlossmustscore = By.XPath("//*[@id='CWField_weightlossmustscore']");
		readonly By Acutediseasemustscore = By.XPath("//*[@id='CWField_acutediseasemustscore']");
		readonly By Musttotalscore = By.XPath("//*[@id='CWField_musttotalscore']");
		readonly By Risk = By.XPath("//*[@id='CWField_risk']");
        #endregion

        #region Management and Review
		 
        readonly By Suggestedscreeningid = By.XPath("//*[@id='CWField_suggestedscreeningid']");
		readonly By Nextscreeningdate = By.XPath("//*[@id='CWField_nextscreeningdate']");
		readonly By NextscreeningdateDatePicker = By.XPath("//*[@id='CWField_nextscreeningdate_DatePicker']");
		readonly By Nextscreeningdate_Time = By.XPath("//*[@id='CWField_nextscreeningdate_Time']");
		readonly By Nextscreeningdate_Time_TimePicker = By.XPath("//*[@id='CWField_nextscreeningdate_Time_TimePicker']");
		readonly By Monitorfoodandfluid_YesRadioButton = By.XPath("//*[@id='CWField_monitorfoodandfluid_1']");
		readonly By Monitorfoodandfluid_NoRadioButton = By.XPath("//*[@id='CWField_monitorfoodandfluid_0']");
		readonly By Additionalcomments = By.XPath("//*[@id='CWField_additionalcomments']");

        #endregion

        #region Additional Information
        By carephysicallocation_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_carephysicallocationid_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By carephysicallocation_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_carephysicallocationid_" + ElementId + "']/a[text()='Remove']");
        readonly By CarephysicallocationidLookupButton = By.XPath("//*[@id='CWLookupBtn_carephysicallocationid']");
		readonly By LocationIfOtherTextareaField = By.Id("CWField_locationifother");
        readonly By CarewellbeingidLink = By.XPath("//*[@id='CWField_carewellbeingid_Link']");
        readonly By CarewellbeingidClearButton = By.XPath("//*[@id='CWClearLookup_carewellbeingid']");
        readonly By CarewellbeingidLookupButton = By.XPath("//*[@id='CWLookupBtn_carewellbeingid']");
		readonly By actiontakenTextareaField = By.XPath("//*[@id='CWField_actiontaken']");
        readonly By Totaltimespentwithclientminutes = By.XPath("//*[@id='CWField_totaltimespentwithclientminutes']");
		readonly By Additionalnotes = By.XPath("//*[@id='CWField_additionalnotes']");
        By Equipment_SelectedElementLink(string ElementId) => By.XPath("//*[@id='MS_equipment_" + ElementId + "']/a[@id='" + ElementId + "_Link']");
        By Equipment_SelectedElementRemoveButton(string ElementId) => By.XPath("//*[@id='MS_equipment_" + ElementId + "']/a[text()='Remove']");
        readonly By EquipmentLookupButton = By.XPath("//*[@id='CWLookupBtn_equipment']");
		readonly By EquipmentIfOtherTextareaField = By.Id("CWField_equipmentifother");
        readonly By CareassistanceneededidLink = By.XPath("//*[@id='CWField_careassistanceneededid_Link']");
        readonly By CareassistanceneededidClearButton = By.XPath("//*[@id='CWClearLookup_careassistanceneededid']");
        readonly By CareassistanceneededidLookupButton = By.XPath("//*[@id='CWLookupBtn_careassistanceneededid']");
        By StaffRequired_SelectedOption(string optionId) => By.XPath("//*[@id='MS_staffrequired_" + optionId + "']");
        readonly By StaffrequiredLookupButton = By.XPath("//*[@id='CWLookupBtn_staffrequired']");
		readonly By AssistanceAmountPicklist = By.Id("CWField_careassistancelevelid");

		#endregion

		#region Care note, Care need, Handover
		readonly String CarenoteFieldId = "CWField_carenote";
        readonly By Carenote = By.XPath("//*[@id='CWField_carenote']");
		readonly By CareplanneeddomainidLookupButton = By.XPath("//*[@id='CWLookupBtn_careplanneeddomainid']");
		readonly By Isincludeinnexthandover_YesRadioButton = By.XPath("//*[@id='CWField_isincludeinnexthandover_1']");
		readonly By Isincludeinnexthandover_NoRadioButton = By.XPath("//*[@id='CWField_isincludeinnexthandover_0']");
		readonly By Flagrecordforhandover_YesRadioButton = By.XPath("//*[@id='CWField_flagrecordforhandover_1']");
		readonly By Flagrecordforhandover_NoRadioButton = By.XPath("//*[@id='CWField_flagrecordforhandover_0']");

        #endregion

        #region Field Labels

        By FieldLabel(string FieldName) => By.XPath("//*[starts-with(@id, 'CWLabelHolder_')]/*[text() = '" + FieldName + "']");
        By MandatoryField_Label(string FieldName) => By.XPath("//label[text()='" + FieldName + "']/span[@class='mandatory']");

        #endregion

        #region Section postion

        By HeightAndWeightSectionNameByPosition(int Position) => By.XPath("//*[@id = 'CWTab_HeightAndWeight']//div[@class = 'col']/div["+Position+"]//*[@class = 'card-header']");		

        #endregion

        public HeightAndWeightRecordPage WaitForPageToLoad()
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
        public HeightAndWeightRecordPage VerifyPageHeaderText(string ExpectedText)
        {
            WaitForElementVisible(pageHeader);
            string pageTitle = GetElementByAttributeValue(pageHeader, "title");
            Assert.AreEqual("Height & Weight: " + ExpectedText, pageTitle);

            return this;
        }

        public HeightAndWeightRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public HeightAndWeightRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			return this;
		}

		public HeightAndWeightRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_PreviousMUSTScore()
		{
			WaitForElementToBeClickable(SectionMenuButton_PreviousMUSTScore);
			Click(SectionMenuButton_PreviousMUSTScore);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_Height()
		{
			WaitForElementToBeClickable(SectionMenuButton_Height);
			Click(SectionMenuButton_Height);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_Weight()
		{
			WaitForElementToBeClickable(SectionMenuButton_Weight);
			Click(SectionMenuButton_Weight);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_Length()
		{
			WaitForElementToBeClickable(SectionMenuButton_Length);
			Click(SectionMenuButton_Length);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_WeightLoss()
		{
			WaitForElementToBeClickable(SectionMenuButton_WeightLoss);
			Click(SectionMenuButton_WeightLoss);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_MUSTMeasurements()
		{
			WaitForElementToBeClickable(SectionMenuButton_MUSTMeasurements);
			Click(SectionMenuButton_MUSTMeasurements);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_MUSTScore()
		{
			WaitForElementToBeClickable(SectionMenuButton_MUSTScore);
			Click(SectionMenuButton_MUSTScore);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_ManagementAndReview()
		{
			WaitForElementToBeClickable(SectionMenuButton_ManagementAndReview);
			Click(SectionMenuButton_ManagementAndReview);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_AdditionalInformation()
		{
			WaitForElementToBeClickable(SectionMenuButton_AdditionalInformation);
			Click(SectionMenuButton_AdditionalInformation);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_CareNote()
		{
			WaitForElementToBeClickable(SectionMenuButton_CareNote);
			Click(SectionMenuButton_CareNote);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_CareNeeds()
		{
			WaitForElementToBeClickable(SectionMenuButton_CareNeeds);
			Click(SectionMenuButton_CareNeeds);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuButton_Handover()
		{
			WaitForElementToBeClickable(SectionMenuButton_Handover);
			Click(SectionMenuButton_Handover);

			return this;
		}

		public HeightAndWeightRecordPage ClickSectionMenuToggleLink()
		{
			WaitForElementToBeClickable(SectionMenuToggleLink);
			Click(SectionMenuToggleLink);

			return this;
		}

		public HeightAndWeightRecordPage ValidateSectionMenuToggleLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(SectionMenuToggleLink);
			ValidateElementText(SectionMenuToggleLink, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage ClickPersonidLink()
		{
			WaitForElementToBeClickable(PersonidLink);
			Click(PersonidLink);

			return this;
		}

		public HeightAndWeightRecordPage ValidatePersonidLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonidLink);
			ValidateElementText(PersonidLink, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage ClickPersonidLookupButton()
		{
			WaitForElementToBeClickable(PersonidLookupButton);
			Click(PersonidLookupButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidatePreferencesText(string ExpectedText)
		{
			WaitForElement(Preferences);
			ScrollToElement(Preferences);
			ValidateElementValueByJavascript(preferences_Id, ExpectedText);

			return this;
		}

        //verify preferences textare field is displayed or not displayed
        public HeightAndWeightRecordPage VerifyPreferencesTextAreaFieldIsDisplayed(bool ExpectedDisplayed)
        {
            if (ExpectedDisplayed)
            {
                WaitForElementVisible(Preferences);
            }
            else
            {
                WaitForElementNotVisible(Preferences, 2);
            }

            return this;
        }

        //verify preferences textare field is disabled or not disabled
        public HeightAndWeightRecordPage VerifyPreferencesTextAreaFieldIsDisabled(bool ExpectedDisabled)
        {
            if (ExpectedDisabled)
            {
                ValidateElementAttribute(Preferences, "disabled", "true");
            }
            else
            {
                ValidateElementAttribute(Preferences, "disabled", "false");
            }

            return this;
        }


        public HeightAndWeightRecordPage InsertTextOnPreferences(string TextToInsert)
		{
			WaitForElementToBeClickable(Preferences);
			SendKeys(Preferences, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage SelectConsentGiven(string TextToSelect)
		{
			WaitForElementToBeClickable(Careconsentgivenid);
			SelectPicklistElementByText(Careconsentgivenid, TextToSelect);

			return this;
		}

		public HeightAndWeightRecordPage ValidateConsentGivenSelectedText(string ExpectedText)
		{
			ValidatePicklistContainsElementByText(Careconsentgivenid, ExpectedText);

			return this;
		}

        public HeightAndWeightRecordPage SelectNonConsentDetail(string TextToSelect)
        {
            WaitForElementVisible(nonconsentDetail);
            SelectPicklistElementByText(nonconsentDetail, TextToSelect);

            return this;
        }

        public HeightAndWeightRecordPage ValidateSelectedNonConsentDetail(string ExpectedText)
        {
            ValidatePicklistSelectedText(nonconsentDetail, ExpectedText);

            return this;
        }

        //verify nonconsentDetail field is displayed or not displayed
		public HeightAndWeightRecordPage ValidateNonConsentDetailFieldVisible(bool ExpectVisible)
		{
            if (ExpectVisible)
                WaitForElementVisible(nonconsentDetail);
            else
                WaitForElementNotVisible(nonconsentDetail, 3);

            return this;
        }

        public HeightAndWeightRecordPage SetReasonForAbsence(string TextToInsert)
        {
            WaitForElementVisible(reasonforabsence);
            SendKeys(reasonforabsence, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public HeightAndWeightRecordPage ValidateReasonForAbsence(string ExpectedText)
        {
            ValidateElementValue(reasonforabsence, ExpectedText);

            return this;
        }

        //verify reasonforabsence field is displayed or not displayed
		public HeightAndWeightRecordPage ValidateReasonForAbsenceFieldVisible(bool ExpectVisible)
		{
            if (ExpectVisible)
                WaitForElementVisible(reasonforabsence);
            else
                WaitForElementNotVisible(reasonforabsence, 3);

            return this;
        }

        public HeightAndWeightRecordPage InsertTextInReasonConsentDeclined(string TextToInsert)
        {
            WaitForElementVisible(reasonconsentdeclined);
            SendKeys(reasonconsentdeclined, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public HeightAndWeightRecordPage ValidateReasonConsentDeclined(string ExpectedText)
        {
            ValidateElementValue(reasonconsentdeclined, ExpectedText);

            return this;
        }

        //verify reasonconsentdeclined field is displayed or not displayed
		public HeightAndWeightRecordPage ValidateReasonConsentDeclinedFieldVisible(bool ExpectVisible)
		{
            if (ExpectVisible)
                WaitForElementVisible(reasonconsentdeclined);
            else
                WaitForElementNotVisible(reasonconsentdeclined, 3);

            return this;
        }

        public HeightAndWeightRecordPage InsertTextInEncouragementGiven(string TextToInsert)
        {
            WaitForElementVisible(encouragementgiven);
            SendKeys(encouragementgiven, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public HeightAndWeightRecordPage ValidateEncouragementGiven(string ExpectedText)
        {
            ValidateElementValue(encouragementgiven, ExpectedText);

            return this;
        }

		//verify encouragementgiven field is displayed or not displayed
		public HeightAndWeightRecordPage ValidateEncouragementGivenFieldVisible(bool ExpectVisible)
		{
			if (ExpectVisible)
                WaitForElementVisible(encouragementgiven);
            else
                WaitForElementNotVisible(encouragementgiven, 3);

            return this;
		}

        public HeightAndWeightRecordPage ClickCareProvidedWithoutConsent_YesRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_YesRadioButton);
            Click(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public HeightAndWeightRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_YesRadioButton);
            ValidateElementChecked(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public HeightAndWeightRecordPage ValidateCareProvidedWithoutConsent_YesRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_YesRadioButton);
            ValidateElementNotChecked(careprovidedwithoutconsent_YesRadioButton);

            return this;
        }

        public HeightAndWeightRecordPage ClickCareProvidedWithoutConsent_NoRadioButton()
        {
            WaitForElementToBeClickable(careprovidedwithoutconsent_NoRadioButton);
            Click(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        public HeightAndWeightRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonChecked()
        {
            WaitForElement(careprovidedwithoutconsent_NoRadioButton);
            ValidateElementChecked(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        public HeightAndWeightRecordPage ValidateCareProvidedWithoutConsent_NoRadioButtonNotChecked()
        {
            WaitForElement(careprovidedwithoutconsent_NoRadioButton);
            ValidateElementNotChecked(careprovidedwithoutconsent_NoRadioButton);

            return this;
        }

        //verify CareProvidedWithoutConsent options are displayed or not displayed
		public HeightAndWeightRecordPage ValidateCareProvidedWithoutConsentOptionsVisible(bool ExpectVisible)
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

        public HeightAndWeightRecordPage SetDeferredToDate(string TextToInsert)
        {
            WaitForElementVisible(deferredToDate);
            SendKeys(deferredToDate, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public HeightAndWeightRecordPage ValidateDeferredToDate(string ExpectedText)
        {
            ValidateElementValue(deferredToDate, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ValidateDeferredToDateErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToDate_ErrorLabel);
            ValidateElementText(deferredToDate_ErrorLabel, ExpectedText);

            return this;
        }

        //Validate deferred to date field is displayed or not displayed
        public HeightAndWeightRecordPage ValidateDeferredToDateFieldVisible(bool ExpectVisible)
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
        public HeightAndWeightRecordPage ClickDeferredToDate_DatePicker()
        {
            WaitForElementToBeClickable(deferredToDate_DatePicker);
            Click(deferredToDate_DatePicker);

            return this;
        }

        //verify deferred to date datepicker is displayed or not displayed
        public HeightAndWeightRecordPage VerifyDeferredToDate_DatePickerIsDisplayed(bool ExpectedDisplayed)
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

        public HeightAndWeightRecordPage SelectDeferredToTimeOrShift(string TextToSelect)
        {
            WaitForElementVisible(deferredToTimeOrShift);
            SelectPicklistElementByText(deferredToTimeOrShift, TextToSelect);

            return this;
        }

        public HeightAndWeightRecordPage ValidateSelectedDeferredToTimeOrShift(string ExpectedText)
        {
            ValidatePicklistSelectedText(deferredToTimeOrShift, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ValidateDeferredToTimeOrShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTimeOrShift_ErrorLabel);
            ValidateElementText(deferredToTimeOrShift_ErrorLabel, ExpectedText);

            return this;
        }

        //verify deferredToTimeorsift field is displayed or not displayed
		public HeightAndWeightRecordPage ValidateDeferredToTimeOrShiftFieldVisible(bool ExpectVisible)
		{
            if (ExpectVisible)
                WaitForElementVisible(deferredToTimeOrShift);
            else
                WaitForElementNotVisible(deferredToTimeOrShift, 3);

            return this;
        }

        public HeightAndWeightRecordPage SetDeferredToTime(string TextToInsert)
        {
            WaitForElementVisible(deferredToTime);
            SendKeys(deferredToTime, TextToInsert + OpenQA.Selenium.Keys.Tab);

            return this;
        }

        public HeightAndWeightRecordPage ValidateDeferredToTime(string ExpectedText)
        {
            ValidateElementValue(deferredToTime, ExpectedText);

            return this;
        }

        //verify deferredToTime field is displayed or not displayed
		public HeightAndWeightRecordPage ValidateDeferredToTimeFieldVisible(bool ExpectVisible)
		{
			if (ExpectVisible)
                WaitForElementVisible(deferredToTime);
            else
                WaitForElementNotVisible(deferredToTime, 3);

            return this;
		}

        public HeightAndWeightRecordPage ValidateDeferredToTimeErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToTime_ErrorLabel);
            ValidateElementText(deferredToTime_ErrorLabel, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ClickDeferredToShiftLookupButton()
        {
            WaitForElementToBeClickable(deferredToShift_LookupButton);
            Click(deferredToShift_LookupButton);

            return this;
        }

        //verify deferredToShift_LookupButton is displayed or not displayed
		public HeightAndWeightRecordPage ValidateDeferredToShiftLookupButtonVisible(bool ExpectVisible)
		{
			if (ExpectVisible)
                WaitForElementVisible(deferredToShift_LookupButton);
            else
                WaitForElementNotVisible(deferredToShift_LookupButton, 3);

            return this;
		}

        public HeightAndWeightRecordPage ValidateDeferredToShiftLinkText(string ExpectedText)
        {
            ValidateElementText(deferredToShift_LinkField, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ClickDeferredToShiftClearButton()
        {
            Click(deferredToShift_ClearButton);

            return this;
        }

        public HeightAndWeightRecordPage ValidateDeferredToShiftErrorLabel(string ExpectedText)
        {
            WaitForElementVisible(deferredToShift_ErrorLabel);
            ValidateElementText(deferredToShift_ErrorLabel, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public HeightAndWeightRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateDateAndTimeOccurred_DateText(string ExpectedText)
		{
			ValidateElementValue(Datetimetaken, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnDateAndTimeOccurred_DateField(string TextToInsert)
		{
			WaitForElementToBeClickable(Datetimetaken);
			SendKeys(Datetimetaken, TextToInsert + Keys.Tab);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickDateAndTimeOccurred_DatePicker()
		{
			WaitForElementToBeClickable(DatetimetakenDatePicker);
			Click(DatetimetakenDatePicker);

			return this;
		}

		public HeightAndWeightRecordPage ValidateDateAndTimeOccurred_TimeText(string ExpectedText)
		{
			ValidateElementValue(Datetimetaken_Time, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnDateAndTimeOccurred_TimeField(string TextToInsert)
		{
			WaitForElementToBeClickable(Datetimetaken_Time);
			SendKeys(Datetimetaken_Time, TextToInsert + Keys.Tab);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickDateAndTimeOccurred_TimePicker()
		{
			WaitForElement(Datetimetaken_Time);
			ScrollToElement(Datetimetaken_Time);
			Click(Datetimetaken_Time);

			WaitForElement(Datetimetaken_Time_TimePicker);
			ScrollToElement(Datetimetaken_Time_TimePicker);
			Click(Datetimetaken_Time_TimePicker);

			return this;
		}

		public HeightAndWeightRecordPage ValidateMusttotalscorepreviousText(string ExpectedText)
		{
			ValidateElementValue(Musttotalscoreprevious, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnMusttotalscoreprevious(string TextToInsert)
		{
			WaitForElementToBeClickable(Musttotalscoreprevious);
			SendKeys(Musttotalscoreprevious, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateRiskpreviousText(string ExpectedText)
		{
			ValidateElementValue(Riskprevious, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnRiskprevious(string TextToInsert)
		{
			WaitForElementToBeClickable(Riskprevious);
			SendKeys(Riskprevious, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickEstimatedheight_YesRadioButton()
		{
			WaitForElementToBeClickable(Estimatedheight_YesRadioButton);
			Click(Estimatedheight_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedheight_YesRadioButtonChecked()
		{
			WaitForElement(Estimatedheight_YesRadioButton);
			ScrollToElement(Estimatedheight_YesRadioButton);
			ValidateElementChecked(Estimatedheight_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedheight_YesRadioButtonNotChecked()
		{
			WaitForElement(Estimatedheight_YesRadioButton);
			ValidateElementNotChecked(Estimatedheight_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickEstimatedheight_NoRadioButton()
		{
			WaitForElementToBeClickable(Estimatedheight_NoRadioButton);
			Click(Estimatedheight_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedheight_NoRadioButtonChecked()
		{
			WaitForElement(Estimatedheight_NoRadioButton);
			ValidateElementChecked(Estimatedheight_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedheight_NoRadioButtonNotChecked()
		{
			WaitForElement(Estimatedheight_NoRadioButton);
			ValidateElementNotChecked(Estimatedheight_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedValueOptionsVisible(bool ExpectedVisible)
		{
			if (ExpectedVisible)
			{
				WaitForElementVisible(Estimatedheight_YesRadioButton);
				WaitForElementVisible(Estimatedheight_NoRadioButton);
			}
			else
			{
				WaitForElementNotVisible(Estimatedheight_YesRadioButton, 3);
                WaitForElementNotVisible(Estimatedheight_NoRadioButton, 3);
			}

			return this;
		}

		public HeightAndWeightRecordPage ValidateHeightmetresText(string ExpectedText)
		{
			ValidateElementValue(Heightmetres, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnHeightmetres(string TextToInsert)
		{
			WaitForElementToBeClickable(Heightmetres);
			SendKeys(Heightmetres, TextToInsert + Keys.Tab);
			
			return this;
		}

		//verify Heightmetres is visible or not visible
		public HeightAndWeightRecordPage ValidateHeightInMetresFieldIsVisible(bool ExpectedVisible)
		{
            if (ExpectedVisible)
                WaitForElementVisible(Heightmetres);
            else
                WaitForElementNotVisible(Heightmetres, 3);

            return this;
        }

		public HeightAndWeightRecordPage ValidateHeightfeetText(string ExpectedText)
		{
			ValidateElementValue(Heightfeet, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnHeightfeet(string TextToInsert)
		{
			WaitForElementToBeClickable(Heightfeet);
			SendKeys(Heightfeet, TextToInsert);
			
			return this;
		}

        //verify Heightfeet field is visible or not visible
		public HeightAndWeightRecordPage ValidateHeightInFeetFieldIsVisible(bool ExpectedVisible)
		{
            if (ExpectedVisible)
                WaitForElementVisible(Heightfeet);
            else
                WaitForElementNotVisible(Heightfeet, 3);

            return this;
        }

        public HeightAndWeightRecordPage ValidateHeightinchesText(string ExpectedText)
		{
			ValidateElementValue(Heightinches, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnHeightinches(string TextToInsert)
		{
			WaitForElementToBeClickable(Heightinches);
			SendKeys(Heightinches, TextToInsert);
			
			return this;
		}

		//verify HeightInInchesField is visible or not visible
		public HeightAndWeightRecordPage ValidateHeightInInchesFieldIsVisible(bool ExpectedVisible)
		{
            if (ExpectedVisible)
                WaitForElementVisible(Heightinches);
            else
                WaitForElementNotVisible(Heightinches, 3);

            return this;
        }

		public HeightAndWeightRecordPage ClickEstimatedweight_YesRadioButton()
		{
			WaitForElementToBeClickable(Estimatedweight_YesRadioButton);
			Click(Estimatedweight_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedweight_YesRadioButtonChecked()
		{
			WaitForElement(Estimatedweight_YesRadioButton);
			ValidateElementChecked(Estimatedweight_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedweight_YesRadioButtonNotChecked()
		{
			WaitForElement(Estimatedweight_YesRadioButton);
			ValidateElementNotChecked(Estimatedweight_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickEstimatedweight_NoRadioButton()
		{
			WaitForElementToBeClickable(Estimatedweight_NoRadioButton);
			Click(Estimatedweight_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedweight_NoRadioButtonChecked()
		{
			WaitForElement(Estimatedweight_NoRadioButton);
			ValidateElementChecked(Estimatedweight_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedweight_NoRadioButtonNotChecked()
		{
			WaitForElement(Estimatedweight_NoRadioButton);
			ValidateElementNotChecked(Estimatedweight_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightInKilogramsText(string ExpectedText)
		{
			ScrollToElement(Weightkilos);
			ValidateElementValue(Weightkilos, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightInKilograms(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightkilos);
			SendKeys(Weightkilos, TextToInsert + Keys.Tab);
			
			return this;
		}

        //verify Weightkilos field is disabled or not disabled
		public HeightAndWeightRecordPage ValidateWeightInKilogramsFieldDisabled(bool ExpectDisabled)
		{
			ScrollToElement(Weightkilos);
			if (ExpectDisabled)
                ValidateElementDisabled(Weightkilos);
            else
                ValidateElementNotDisabled(Weightkilos);

            return this;
		}

        public HeightAndWeightRecordPage ValidateWeightstonesText(string ExpectedText)
		{
			ValidateElementValue(Weightstones, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightstones(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightstones);
			SendKeys(Weightstones, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightpoundsText(string ExpectedText)
		{
			ValidateElementValue(Weightpounds, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightpounds(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightpounds);
			SendKeys(Weightpounds, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightouncesText(string ExpectedText)
		{
			ValidateElementValue(Weightounces, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightounces(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightounces);
			SendKeys(Weightounces, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickHasamputation_YesRadioButton()
		{
			WaitForElementToBeClickable(Hasamputation_YesRadioButton);
			Click(Hasamputation_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateHasamputation_YesRadioButtonChecked()
		{
			WaitForElement(Hasamputation_YesRadioButton);
			ValidateElementChecked(Hasamputation_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateHasamputation_YesRadioButtonNotChecked()
		{
			WaitForElement(Hasamputation_YesRadioButton);
			ValidateElementNotChecked(Hasamputation_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickHasamputation_NoRadioButton()
		{
			WaitForElementToBeClickable(Hasamputation_NoRadioButton);
			Click(Hasamputation_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateHasamputation_NoRadioButtonChecked()
		{
			WaitForElement(Hasamputation_NoRadioButton);
			ValidateElementChecked(Hasamputation_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateHasamputation_NoRadioButtonNotChecked()
		{
			WaitForElement(Hasamputation_NoRadioButton);
			ValidateElementNotChecked(Hasamputation_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateMuacmeasurementcmText(string ExpectedText)
		{
			ScrollToElement(Muacmeasurementcm);
			ValidateElementValue(Muacmeasurementcm, ExpectedText);

			return this;
		}

        //verify Muacmeasurementcm field is disabled or not disabled
		public HeightAndWeightRecordPage ValidateMuacmeasurementcmFieldDisabled(bool ExpectDisabled)
		{
			if (ExpectDisabled)
                ValidateElementDisabled(Muacmeasurementcm);
            else
                ValidateElementNotDisabled(Muacmeasurementcm);

            return this;
        }

        public HeightAndWeightRecordPage InsertTextOnMuacmeasurementcm(string TextToInsert)
		{
			ScrollToElement(Muacmeasurementcm);
			WaitForElementToBeClickable(Muacmeasurementcm);
			SendKeys(Muacmeasurementcm, TextToInsert + Keys.Tab);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateLengthfeetText(string ExpectedText)
		{
			ValidateElementValue(Lengthfeet, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnLengthfeet(string TextToInsert)
		{
			WaitForElementToBeClickable(Lengthfeet);
			SendKeys(Lengthfeet, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateLengthinchesText(string ExpectedText)
		{
			ValidateElementValue(Lengthinches, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnLengthinches(string TextToInsert)
		{
			WaitForElementToBeClickable(Lengthinches);
			SendKeys(Lengthinches, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateLengthcentimetresText(string ExpectedText)
		{
			ValidateElementValue(Lengthcentimetres, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnLengthcentimetres(string TextToInsert)
		{
			WaitForElementToBeClickable(Lengthcentimetres);
			SendKeys(Lengthcentimetres, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateHeadcircumferenceText(string ExpectedText)
		{
			ValidateElementValue(Headcircumference, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnHeadcircumference(string TextToInsert)
		{
			WaitForElementToBeClickable(Headcircumference);
			SendKeys(Headcircumference, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightlossstonesText(string ExpectedText)
		{
			ValidateElementValue(Weightlossstones, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightlossstones(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightlossstones);
			SendKeys(Weightlossstones, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightlosspoundsText(string ExpectedText)
		{
			ValidateElementValue(Weightlosspounds, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightlosspounds(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightlosspounds);
			SendKeys(Weightlosspounds, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightLossInKilogramsText(string ExpectedText)
		{
			WaitForElement(Weightlosskilos);
			ScrollToElement(Weightlosskilos);
			ValidateElementValueByJavascript(WeightLossKilosId, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightlosskilos(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightlosskilos);
			SendKeys(Weightlosskilos, TextToInsert + Keys.Tab);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightLossPercentText(string ExpectedText)
		{
			WaitForElement(Weightlosspercent);
			ScrollToElement(Weightlosspercent);
			ValidateElementValueByJavascript(WeightlosspercentId, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightlosspercent(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightlosspercent);
			SendKeys(Weightlosspercent, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateAgeattimetakenText(string ExpectedText)
		{
			ValidateElementValue(Ageattimetaken, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnAgeattimetaken(string TextToInsert)
		{
			WaitForElementToBeClickable(Ageattimetaken);
			SendKeys(Ageattimetaken, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickEstimatedbmi_YesRadioButton()
		{
			WaitForElementToBeClickable(Estimatedbmi_YesRadioButton);
			Click(Estimatedbmi_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedbmi_YesRadioButtonChecked()
		{
			WaitForElement(Estimatedbmi_YesRadioButton);
			ValidateElementChecked(Estimatedbmi_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedbmi_YesRadioButtonNotChecked()
		{
			WaitForElement(Estimatedbmi_YesRadioButton);
			ValidateElementNotChecked(Estimatedbmi_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickEstimatedbmi_NoRadioButton()
		{
			WaitForElementToBeClickable(Estimatedbmi_NoRadioButton);
			Click(Estimatedbmi_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedbmi_NoRadioButtonChecked()
		{
			WaitForElement(Estimatedbmi_NoRadioButton);
			ValidateElementChecked(Estimatedbmi_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateEstimatedbmi_NoRadioButtonNotChecked()
		{
			WaitForElement(Estimatedbmi_NoRadioButton);
			ValidateElementNotChecked(Estimatedbmi_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateBmiresultText(string ExpectedText)
		{
			ValidateElementValue(Bmiresult, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnBmiresult(string TextToInsert)
		{
			WaitForElementToBeClickable(Bmiresult);
			SendKeys(Bmiresult, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateBmiscoreText(string ExpectedText)
		{
			ScrollToElement(Bmiscore);
			ValidateElementValue(Bmiscore, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnBmiscore(string TextToInsert)
		{
			WaitForElementToBeClickable(Bmiscore);
			SendKeys(Bmiscore, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateBmimustscoreText(string ExpectedText)
		{
			ValidateElementValue(Bmimustscore, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnBmimustscore(string TextToInsert)
		{
			WaitForElementToBeClickable(Bmimustscore);
			SendKeys(Bmimustscore, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickAcutediseaseeffect_YesRadioButton()
		{
			WaitForElementToBeClickable(Acutediseaseeffect_YesRadioButton);
			Click(Acutediseaseeffect_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateAcutediseaseeffect_YesRadioButtonChecked()
		{
			WaitForElement(Acutediseaseeffect_YesRadioButton);
			ValidateElementChecked(Acutediseaseeffect_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateAcutediseaseeffect_YesRadioButtonNotChecked()
		{
			WaitForElement(Acutediseaseeffect_YesRadioButton);
			ValidateElementNotChecked(Acutediseaseeffect_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickAcutediseaseeffect_NoRadioButton()
		{
			WaitForElementToBeClickable(Acutediseaseeffect_NoRadioButton);
			Click(Acutediseaseeffect_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateAcutediseaseeffect_NoRadioButtonChecked()
		{
			WaitForElement(Acutediseaseeffect_NoRadioButton);
			ValidateElementChecked(Acutediseaseeffect_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateAcutediseaseeffect_NoRadioButtonNotChecked()
		{
			WaitForElement(Acutediseaseeffect_NoRadioButton);
			ValidateElementNotChecked(Acutediseaseeffect_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateWeightlossmustscoreText(string ExpectedText)
		{
			ValidateElementValue(Weightlossmustscore, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnWeightlossmustscore(string TextToInsert)
		{
			WaitForElementToBeClickable(Weightlossmustscore);
			SendKeys(Weightlossmustscore, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateAcutediseasemustscoreText(string ExpectedText)
		{
			ValidateElementValue(Acutediseasemustscore, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnAcutediseasemustscore(string TextToInsert)
		{
			WaitForElementToBeClickable(Acutediseasemustscore);
			SendKeys(Acutediseasemustscore, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateMusttotalscoreText(string ExpectedText)
		{
			ValidateElementValue(Musttotalscore, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnMusttotalscore(string TextToInsert)
		{
			WaitForElementToBeClickable(Musttotalscore);
			SendKeys(Musttotalscore, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateRiskText(string ExpectedText)
		{
			ValidateElementValue(Risk, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnRisk(string TextToInsert)
		{
			WaitForElementToBeClickable(Risk);
			SendKeys(Risk, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage SelectSuggestedscreeningid(string TextToSelect)
		{
			WaitForElementToBeClickable(Suggestedscreeningid);
			SelectPicklistElementByText(Suggestedscreeningid, TextToSelect);

			return this;
		}

		public HeightAndWeightRecordPage ValidateSuggestedscreeningidSelectedText(string ExpectedText)
		{
			ValidatePicklistSelectedText(Suggestedscreeningid, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage ValidateNextscreeningdateText(string ExpectedText)
		{
			ValidateElementValue(Nextscreeningdate, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnNextscreeningdate(string TextToInsert)
		{
			WaitForElementToBeClickable(Nextscreeningdate);
			SendKeys(Nextscreeningdate, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickNextscreeningdateDatePicker()
		{
			WaitForElementToBeClickable(NextscreeningdateDatePicker);
			Click(NextscreeningdateDatePicker);

			return this;
		}

		public HeightAndWeightRecordPage ValidateNextscreeningdate_TimeText(string ExpectedText)
		{
			ValidateElementValue(Nextscreeningdate_Time, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnNextscreeningdate_Time(string TextToInsert)
		{
			WaitForElementToBeClickable(Nextscreeningdate_Time);
			SendKeys(Nextscreeningdate_Time, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickNextscreeningdate_Time_TimePicker()
		{
			WaitForElementToBeClickable(Nextscreeningdate_Time_TimePicker);
			Click(Nextscreeningdate_Time_TimePicker);

			return this;
		}

		public HeightAndWeightRecordPage ClickMonitorfoodandfluid_YesRadioButton()
		{
			WaitForElementToBeClickable(Monitorfoodandfluid_YesRadioButton);
			Click(Monitorfoodandfluid_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateMonitorfoodandfluid_YesRadioButtonChecked()
		{
			WaitForElement(Monitorfoodandfluid_YesRadioButton);
			ValidateElementChecked(Monitorfoodandfluid_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateMonitorfoodandfluid_YesRadioButtonNotChecked()
		{
			WaitForElement(Monitorfoodandfluid_YesRadioButton);
			ValidateElementNotChecked(Monitorfoodandfluid_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickMonitorfoodandfluid_NoRadioButton()
		{
			WaitForElementToBeClickable(Monitorfoodandfluid_NoRadioButton);
			Click(Monitorfoodandfluid_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateMonitorfoodandfluid_NoRadioButtonChecked()
		{
			WaitForElement(Monitorfoodandfluid_NoRadioButton);
			ValidateElementChecked(Monitorfoodandfluid_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateMonitorfoodandfluid_NoRadioButtonNotChecked()
		{
			WaitForElement(Monitorfoodandfluid_NoRadioButton);
			ValidateElementNotChecked(Monitorfoodandfluid_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateAdditionalcommentsText(string ExpectedText)
		{
			ValidateElementText(Additionalcomments, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnAdditionalcomments(string TextToInsert)
		{
			WaitForElementToBeClickable(Additionalcomments);
			SendKeys(Additionalcomments, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickLocationLookupButton()
		{
			WaitForElementToBeClickable(CarephysicallocationidLookupButton);
			Click(CarephysicallocationidLookupButton);

			return this;
		}

        //verify LocationIfOtherTextareaField is displayed or not displayed
		public HeightAndWeightRecordPage ValidateLocationIfOtherTextareaFieldVisible(bool ExpectVisible)
		{
			if (ExpectVisible)
				WaitForElementVisible(LocationIfOtherTextareaField);
			else
				WaitForElementNotVisible(LocationIfOtherTextareaField, 3);

			return this;
		}

		//Insert text on LocationIfOtherTextareaField
		public HeightAndWeightRecordPage InsertTextOnLocationIfOtherTextareaField(string TextToInsert)
		{
			WaitForElement(LocationIfOtherTextareaField);
			SendKeys(LocationIfOtherTextareaField, TextToInsert + Keys.Tab);

			return this;
		}

        //verify LocationIfOtherTextareaField

		public HeightAndWeightRecordPage ValidateLocationIfOtherTextareaField(string ExpectedText)
		{
			ValidateElementValue(LocationIfOtherTextareaField, ExpectedText);

			return this;
		}

        public HeightAndWeightRecordPage ClickLocation_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementLink(ElementId));
            Click(carephysicallocation_SelectedElementLink(ElementId));

            return this;
        }

        public HeightAndWeightRecordPage ValidateLocation_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementLink(ElementId));
            ValidateElementText(carephysicallocation_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ValidateLocation_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateLocation_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public HeightAndWeightRecordPage ClickLocation_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(carephysicallocation_SelectedElementRemoveButton(ElementId));
            Click(carephysicallocation_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public HeightAndWeightRecordPage ClickWellbeingLink()
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            Click(CarewellbeingidLink);

            return this;
        }

        public HeightAndWeightRecordPage ValidateWellbeingLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CarewellbeingidLink);
            ValidateElementText(CarewellbeingidLink, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ClickWellbeingClearButton()
        {
            WaitForElementToBeClickable(CarewellbeingidClearButton);
            Click(CarewellbeingidClearButton);

            return this;
        }

        public HeightAndWeightRecordPage ClickWellbeingLookupButton()
		{
			WaitForElementToBeClickable(CarewellbeingidLookupButton);
			Click(CarewellbeingidLookupButton);

			return this;
		}

        //Insert text on actiontakenTextareaField
		public HeightAndWeightRecordPage InsertTextOnActionTakenTextareaField(string TextToInsert)
		{
			WaitForElement(actiontakenTextareaField);
			SendKeys(actiontakenTextareaField, TextToInsert + Keys.Tab);

			return this;
		}

		//Verify ActionTakenTextareaField
		public HeightAndWeightRecordPage ValidateActionTakenTextareaField(string ExpectedText)
		{
			ValidateElementText(actiontakenTextareaField, ExpectedText);
			return this;
		}

        public HeightAndWeightRecordPage ValidateTotalTimeSpentWithPersonMinutesText(string ExpectedText)
		{
			ValidateElementValue(Totaltimespentwithclientminutes, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnTotalTimeSpentWithPersonMinutes(string TextToInsert)
		{
			WaitForElementToBeClickable(Totaltimespentwithclientminutes);
			SendKeys(Totaltimespentwithclientminutes, TextToInsert);
			
			return this;
		}

        //verify totaltimespentwithclientminutes is visible or not visible
		public HeightAndWeightRecordPage ValidateTotalTimeSpentWithPersonMinutesFieldVisible(bool ExpectVisible)
		{
			if(ExpectVisible)
				WaitForElementVisible(Totaltimespentwithclientminutes);
            else
                WaitForElementNotVisible(Totaltimespentwithclientminutes, 3);

            return this;
		}

        public HeightAndWeightRecordPage ValidateAdditionalNotesText(string ExpectedText)
		{
			ValidateElementText(Additionalnotes, ExpectedText);

			return this;
		}

		public HeightAndWeightRecordPage InsertTextOnAdditionalNotes(string TextToInsert)
		{
			WaitForElementToBeClickable(Additionalnotes);
			SendKeys(Additionalnotes, TextToInsert + Keys.Tab);
			
			return this;
		}

        //verify Additionalnotes is visible or not visible
		public HeightAndWeightRecordPage ValidateAdditionalNotesFieldVisible(bool ExpectVisible)
		{
			if(ExpectVisible)
                WaitForElementVisible(Additionalnotes);
            else
                WaitForElementNotVisible(Additionalnotes, 3);

            return this;
        }

        public HeightAndWeightRecordPage ClickEquipmentLookupButton()
		{
			WaitForElementToBeClickable(EquipmentLookupButton);
			Click(EquipmentLookupButton);

			return this;
		}

		//verify EquipmentIfOtherTextareaField is displayed or not displayed
		public HeightAndWeightRecordPage ValidateEquipmentIfOtherTextareaFieldVisible(bool ExpectVisible)
		{
            if (ExpectVisible)
                WaitForElementVisible(EquipmentIfOtherTextareaField);
            else
                WaitForElementNotVisible(EquipmentIfOtherTextareaField, 3);

            return this;
        }

		//Insert text on EquipmentIfOtherTextareaField
		public HeightAndWeightRecordPage InsertTextOnEquipmentIfOtherTextareaField(string TextToInsert)
		{
			WaitForElement(EquipmentIfOtherTextareaField);
			SendKeys(EquipmentIfOtherTextareaField, TextToInsert + Keys.Tab);
			return this;
		}

		//Verify EquipmentIfOtherTextareaField
		public HeightAndWeightRecordPage ValidateEquipmentIfOtherTextareaField(string ExpectedText)
		{
			ValidateElementValue(EquipmentIfOtherTextareaField, ExpectedText);

			return this;
		}

        public HeightAndWeightRecordPage ClickEquipment_SelectedElementLink(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            Click(Equipment_SelectedElementLink(ElementId));

            return this;
        }

        public HeightAndWeightRecordPage ValidateEquipment_SelectedElementLinkText(string ElementId, string ExpectedText)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementLink(ElementId));
            ValidateElementText(Equipment_SelectedElementLink(ElementId), ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ValidateEquipment_SelectedElementLinkText(Guid ElementId, string ExpectedText)
        {
            return ValidateEquipment_SelectedElementLinkText(ElementId.ToString(), ExpectedText);
        }

        public HeightAndWeightRecordPage ClickEquipment_SelectedElementRemoveButton(string ElementId)
        {
            WaitForElementToBeClickable(Equipment_SelectedElementRemoveButton(ElementId));
            Click(Equipment_SelectedElementRemoveButton(ElementId));

            return this;
        }

        public HeightAndWeightRecordPage ClickAssistanceNeededLink()
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            Click(CareassistanceneededidLink);

            return this;
        }

        public HeightAndWeightRecordPage ValidateAssistanceNeededLinkText(string ExpectedText)
        {
            WaitForElementToBeClickable(CareassistanceneededidLink);
            ValidateElementText(CareassistanceneededidLink, ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ClickAssistanceNeededClearButton()
        {
            WaitForElementToBeClickable(CareassistanceneededidClearButton);
            Click(CareassistanceneededidClearButton);

            return this;
        }

        public HeightAndWeightRecordPage ClickAssistanceNeededLookupButton()
		{
			WaitForElementToBeClickable(CareassistanceneededidLookupButton);
			Click(CareassistanceneededidLookupButton);

			return this;
		}

        //Select value from CareAssistanceLevelPicklist
		public HeightAndWeightRecordPage SelectAssistanceAmountFromPicklist(string OptionText)
		{
			WaitForElementToBeClickable(AssistanceAmountPicklist);
            SelectPicklistElementByText(AssistanceAmountPicklist, OptionText);

            return this;
        }

		//verify AssistanceAmountPicklist Selected value
		public HeightAndWeightRecordPage ValidateAssistanceAmountPicklistSelectedValue(string ExpectedValue)
		{
            ValidatePicklistSelectedText(AssistanceAmountPicklist, ExpectedValue);

            return this;
        }

		//verify AssistanceAmountPicklist is visible or not visible
		public HeightAndWeightRecordPage ValidateAssistanceAmountPicklistVisible(bool ExpectVisible)
		{
			if (ExpectVisible)
                WaitForElementVisible(AssistanceAmountPicklist);
            else
                WaitForElementNotVisible(AssistanceAmountPicklist, 3);
			return this;
		}

        public HeightAndWeightRecordPage ClickStaffrequiredLookupButton()
		{
			WaitForElementToBeClickable(StaffrequiredLookupButton);
			Click(StaffrequiredLookupButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateCarenoteText(string ExpectedText)
		{
            WaitForElement(Carenote);
            ScrollToElement(Carenote);
            var fieldValue = GetElementValueByJavascript(CarenoteFieldId);
            Assert.AreEqual(ExpectedText, fieldValue);

            return this;
		}

        //verify Carenote is visible or not visible
		public HeightAndWeightRecordPage ValidateCarenoteIsVisible(bool ExpectVisible)
		{
			if (ExpectVisible)
                WaitForElementVisible(Carenote);
            else
                WaitForElementNotVisible(Carenote, 3);

            return this;
		}

        public HeightAndWeightRecordPage InsertTextOnCarenote(string TextToInsert)
		{
			WaitForElementToBeClickable(Carenote);
			SendKeys(Carenote, TextToInsert);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickCareplanneeddomainidLookupButton()
		{
			WaitForElementToBeClickable(CareplanneeddomainidLookupButton);
			Click(CareplanneeddomainidLookupButton);

			return this;
		}

		public HeightAndWeightRecordPage ClickIsincludeinnexthandover_YesRadioButton()
		{
			WaitForElementToBeClickable(Isincludeinnexthandover_YesRadioButton);
			Click(Isincludeinnexthandover_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateIsincludeinnexthandover_YesRadioButtonChecked()
		{
			WaitForElement(Isincludeinnexthandover_YesRadioButton);
			ValidateElementChecked(Isincludeinnexthandover_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateIsincludeinnexthandover_YesRadioButtonNotChecked()
		{
			WaitForElement(Isincludeinnexthandover_YesRadioButton);
			ValidateElementNotChecked(Isincludeinnexthandover_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickIsincludeinnexthandover_NoRadioButton()
		{
			WaitForElementToBeClickable(Isincludeinnexthandover_NoRadioButton);
			Click(Isincludeinnexthandover_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateIsincludeinnexthandover_NoRadioButtonChecked()
		{
			WaitForElement(Isincludeinnexthandover_NoRadioButton);
			ValidateElementChecked(Isincludeinnexthandover_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateIsincludeinnexthandover_NoRadioButtonNotChecked()
		{
			WaitForElement(Isincludeinnexthandover_NoRadioButton);
			ValidateElementNotChecked(Isincludeinnexthandover_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickFlagrecordforhandover_YesRadioButton()
		{
			WaitForElementToBeClickable(Flagrecordforhandover_YesRadioButton);
			Click(Flagrecordforhandover_YesRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateFlagrecordforhandover_YesRadioButtonChecked()
		{
			WaitForElement(Flagrecordforhandover_YesRadioButton);
			ValidateElementChecked(Flagrecordforhandover_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateFlagrecordforhandover_YesRadioButtonNotChecked()
		{
			WaitForElement(Flagrecordforhandover_YesRadioButton);
			ValidateElementNotChecked(Flagrecordforhandover_YesRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ClickFlagrecordforhandover_NoRadioButton()
		{
			WaitForElementToBeClickable(Flagrecordforhandover_NoRadioButton);
			Click(Flagrecordforhandover_NoRadioButton);

			return this;
		}

		public HeightAndWeightRecordPage ValidateFlagrecordforhandover_NoRadioButtonChecked()
		{
			WaitForElement(Flagrecordforhandover_NoRadioButton);
			ValidateElementChecked(Flagrecordforhandover_NoRadioButton);
			
			return this;
		}

		public HeightAndWeightRecordPage ValidateFlagrecordforhandover_NoRadioButtonNotChecked()
		{
			WaitForElement(Flagrecordforhandover_NoRadioButton);
			ValidateElementNotChecked(Flagrecordforhandover_NoRadioButton);
			
			return this;
		}

        public HeightAndWeightRecordPage ValidateFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(FieldLabel(FieldName));
            else
                WaitForElementNotVisible(FieldLabel(FieldName), 3);

            return this;
        }

        public HeightAndWeightRecordPage ValidateMandatoryFieldIsVisible(string FieldName, bool ExpectVisible = true)
        {
            if (ExpectVisible)
                WaitForElementVisible(MandatoryField_Label(FieldName));
            else
                WaitForElementNotVisible(MandatoryField_Label(FieldName), 3);

            return this;
        }

        //verify SectionNameByPosition
        public HeightAndWeightRecordPage ValidateSectionName(string ExpectedText, int Position)
		{
			WaitForElement(HeightAndWeightSectionNameByPosition(Position));
			ScrollToElement(HeightAndWeightSectionNameByPosition(Position));
            ValidateElementText(HeightAndWeightSectionNameByPosition(Position), ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ValidateStaffRequiredSelectedOptionText(string OptionId, string ExpectedText)
        {
            WaitForElement(StaffRequired_SelectedOption(OptionId));
            ValidateElementTextContainsText(StaffRequired_SelectedOption(OptionId), ExpectedText);

            return this;
        }

        public HeightAndWeightRecordPage ValidateStaffRequiredSelectedOptionText(Guid OptionId, string ExpectedText)
        {
            return ValidateStaffRequiredSelectedOptionText(OptionId.ToString(), ExpectedText);
        }

    }
}
