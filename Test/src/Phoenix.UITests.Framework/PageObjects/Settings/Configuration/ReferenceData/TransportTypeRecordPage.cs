using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class TransportTypeRecordPage : CommonMethods
    {
        public TransportTypeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        //readonly By iframe_transporttype = By.Id("iframe_transporttype");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=transporttype')]");



        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By additionalToolbarElementsButton = By.XPath("//div[@id='CWToolbar']/div/div/div[@id='CWToolbarMenu']/button");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");


        readonly By TransportTypeRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");


        #region General

        readonly By Name_FieldLabel = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By Name_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_name']/label/span");
        readonly By Name_Field = By.XPath("//*[@id='CWField_name']");

        readonly By Code_FieldLabel = By.XPath("//*[@id='CWLabelHolder_code']/label");
        readonly By Code_Field = By.XPath("//*[@id='CWField_code']");

        readonly By GovCode_FieldLabel = By.XPath("//*[@id='CWLabelHolder_govcode']/label");
        readonly By GovCode_Field = By.XPath("//*[@id='CWField_govcode']");

        readonly By Inactive_FieldLabel = By.XPath("//*[@id='CWLabelHolder_inactive']/label");
        readonly By Inactive_YesRadioButton = By.XPath("//*[@id='CWField_inactive_1']");
        readonly By Inactive_NoRadioButton = By.XPath("//*[@id='CWField_inactive_0']");

        readonly By TravelTimeCalculation_FieldLabel = By.XPath("//*[@id='CWLabelHolder_traveltimecalculationid']/label");
        readonly By TravelTimeCalculation_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_traveltimecalculationid']/label/span");
        readonly By TravelTimeCalculation_Field = By.XPath("//*[@id='CWField_traveltimecalculationid']");

        readonly By Speed_FieldLabel = By.XPath("//*[@id='CWLabelHolder_speed']/label");
        readonly By Speed_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_speed']/label/span");
        readonly By Speed_Field = By.XPath("//*[@id='CWField_speed']");

        readonly By Icon_FieldLabel = By.XPath("//*[@id='CWLabelHolder_transporttypeiconid']/label");
        readonly By Icon_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_transporttypeiconid']/label/span");
        readonly By Icon_Field = By.XPath("//*[@id='CWField_transporttypeiconid']");

        readonly By StartDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By StartDate_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_startdate']/label/span");
        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");

        readonly By EndDate_FieldLabel = By.XPath("//*[@id='CWLabelHolder_enddate']/label");
        readonly By EndDate_Field = By.XPath("//*[@id='CWField_enddate']");

        readonly By ValidForExport_FieldLabel = By.XPath("//*[@id='CWLabelHolder_validforexport']/label");
        readonly By ValidForExport_YesRadioButton = By.XPath("//*[@id='CWField_validforexport_1']");
        readonly By ValidForExport_NoRadioButton = By.XPath("//*[@id='CWField_validforexport_0']");

        readonly By ResponsibleTeam_FieldLabel = By.XPath("//*[@id='CWLabelHolder_ownerid']/label");
        readonly By ResponsibleTeam_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_ownerid']/label/span");
        readonly By ResponsibleTeam_FieldLink = By.XPath("//*[@id='CWField_ownerid_Link']");
        readonly By ResponsibleTeam_LookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
        readonly By ResponsibleTeam_RemoveButton = By.XPath("//*[@id='CWClearLookup_ownerid']");

        #endregion

        #region Auto Allocation

        readonly By MileageExpenseCostsApplyToThisTransportType_FieldLabel = By.XPath("//*[@id='CWLabelHolder_mileageexpensecostsapplytothistransporttype']/label");
        readonly By MileageExpenseCostsApplyToThisTransportType_YesRadioButton = By.XPath("//*[@id='CWField_mileageexpensecostsapplytothistransporttype_1']");
        readonly By MileageExpenseCostsApplyToThisTransportType_NoRadioButton = By.XPath("//*[@id='CWField_mileageexpensecostsapplytothistransporttype_0']");

        #endregion

        #region Auto Allocation: Journey Between Bookings

        readonly By JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldLabel = By.XPath("//*[@id='CWLabelHolder_thresholdjourneybetweenbookingstimeid']/label");
        readonly By JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_thresholdjourneybetweenbookingstimeid']/label/span");
        readonly By JourneyBetweenBookings_TotalJourneyTimeExceeds_Field = By.XPath("//*[@id='CWField_thresholdjourneybetweenbookingstimeid']");

        readonly By JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldLabel = By.XPath("//*[@id='CWLabelHolder_thresholdjourneybetweenbookingsdistanceid']/label");
        readonly By JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_thresholdjourneybetweenbookingsdistanceid']/label/span");
        readonly By JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field = By.XPath("//*[@id='CWField_thresholdjourneybetweenbookingsdistanceid']");

        readonly By JourneyBetweenBookings_AverageJourneyTime_FieldLabel = By.XPath("//*[@id='CWLabelHolder_averagejourneytimebetweenbookingsid']/label");
        readonly By JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_averagejourneytimebetweenbookingsid']/label/span");
        readonly By JourneyBetweenBookings_AverageJourneyTime_Field = By.XPath("//*[@id='CWField_averagejourneytimebetweenbookingsid']");

        #endregion

        #region Auto Allocation: Journey To / From Home

        readonly By JourneyToFromHome_TotalJourneyTimeExceeds_FieldLabel = By.XPath("//*[@id='CWLabelHolder_thresholdjourneytofromhometimeid']/label");
        readonly By JourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_thresholdjourneytofromhometimeid']/label/span");
        readonly By JourneyToFromHome_TotalJourneyTimeExceeds_Field = By.XPath("//*[@id='CWField_thresholdjourneytofromhometimeid']");

        readonly By JourneyToFromHome_TotalJourneyDistanceExceeds_FieldLabel = By.XPath("//*[@id='CWLabelHolder_thresholdjourneytofromhomedistanceid']/label");
        readonly By JourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_thresholdjourneytofromhomedistanceid']/label/span");
        readonly By JourneyToFromHome_TotalJourneyDistanceExceeds_Field = By.XPath("//*[@id='CWField_thresholdjourneytofromhomedistanceid']");

        readonly By JourneyToFromHome_AverageJourneyTime_FieldLabel = By.XPath("//*[@id='CWLabelHolder_averagejourneytimetofromhomeid']/label");
        readonly By JourneyToFromHome_AverageJourneyTime_FieldErrorLabel = By.XPath("//*[@id='CWControlHolder_averagejourneytimetofromhomeid']/label/span");
        readonly By JourneyToFromHome_AverageJourneyTime_Field = By.XPath("//*[@id='CWField_averagejourneytimetofromhomeid']");

        #endregion


        public TransportTypeRecordPage WaitForTransportTypeRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            //WaitForElement(iframe_transporttype);
            //SwitchToIframe(iframe_transporttype);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(TransportTypeRecordPageHeader);

            WaitForElementVisible(Name_FieldLabel);
            WaitForElementVisible(Name_Field);
            WaitForElementVisible(Code_FieldLabel);
            WaitForElementVisible(Code_Field);

            return this;
        }


        public TransportTypeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            ScrollToElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }

        public TransportTypeRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            ScrollToElement(saveButton);
            Click(saveButton);

            return this;
        }

        public TransportTypeRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public TransportTypeRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(deleteButton);
            ScrollToElement(deleteButton);
            Click(deleteButton);

            return this;
        }


        public TransportTypeRecordPage WaitForRecordToBeSaved()
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);

            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);
            WaitForElementVisible(deleteButton);
            WaitForElementVisible(assignRecordButton);
            WaitForElementVisible(additionalToolbarElementsButton);

            return this;
        }



        public TransportTypeRecordPage ValidateNewRecordFieldsVisible()
        {
            WaitForElementVisible(Name_FieldLabel);
            WaitForElementVisible(Name_Field);
            WaitForElementVisible(Code_FieldLabel);
            WaitForElementVisible(Code_Field);
            WaitForElementVisible(GovCode_FieldLabel);
            WaitForElementVisible(GovCode_Field);
            WaitForElementVisible(Inactive_FieldLabel);
            WaitForElementVisible(Inactive_YesRadioButton);
            WaitForElementVisible(Inactive_NoRadioButton);
            WaitForElementVisible(TravelTimeCalculation_FieldLabel);
            WaitForElementVisible(TravelTimeCalculation_Field);
            WaitForElementVisible(Speed_FieldLabel);
            WaitForElementVisible(Speed_Field);

            WaitForElementVisible(Icon_FieldLabel);
            WaitForElementVisible(Icon_Field);
            WaitForElementVisible(StartDate_FieldLabel);
            WaitForElementVisible(StartDate_Field);
            WaitForElementVisible(EndDate_FieldLabel);
            WaitForElementVisible(EndDate_Field);
            WaitForElementVisible(ValidForExport_FieldLabel);
            WaitForElementVisible(ValidForExport_YesRadioButton);
            WaitForElementVisible(ValidForExport_NoRadioButton);
            WaitForElementVisible(ResponsibleTeam_FieldLabel);
            WaitForElementVisible(ResponsibleTeam_LookupButton);

            WaitForElementVisible(MileageExpenseCostsApplyToThisTransportType_FieldLabel);
            WaitForElementVisible(MileageExpenseCostsApplyToThisTransportType_YesRadioButton);
            WaitForElementVisible(MileageExpenseCostsApplyToThisTransportType_NoRadioButton);

            return this;
        }

        public TransportTypeRecordPage ValidateAutoAllocationJourneyBetweenBookingsSectionVisible()
        {
            WaitForElementVisible(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldLabel);
            WaitForElementVisible(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field);
            WaitForElementVisible(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldLabel);
            WaitForElementVisible(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field);
            WaitForElementVisible(JourneyBetweenBookings_AverageJourneyTime_FieldLabel);
            WaitForElementVisible(JourneyBetweenBookings_AverageJourneyTime_Field);

            return this;
        }

        public TransportTypeRecordPage ValidateAutoAllocationJourneyToFromHomeVisible()
        {
            WaitForElementVisible(JourneyToFromHome_TotalJourneyTimeExceeds_FieldLabel);
            WaitForElementVisible(JourneyToFromHome_TotalJourneyTimeExceeds_Field);
            WaitForElementVisible(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldLabel);
            WaitForElementVisible(JourneyToFromHome_TotalJourneyDistanceExceeds_Field);
            WaitForElementVisible(JourneyToFromHome_AverageJourneyTime_FieldLabel);
            WaitForElementVisible(JourneyToFromHome_AverageJourneyTime_Field);

            return this;
        }



        public TransportTypeRecordPage ClickResponsibleTeamLookupButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_LookupButton);
            Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public TransportTypeRecordPage ClickResponsibleTeamRemoveButton()
        {
            WaitForElementToBeClickable(ResponsibleTeam_RemoveButton);
            Click(ResponsibleTeam_RemoveButton);

            return this;
        }



        public TransportTypeRecordPage InsertName(string TextToInsert)
        {
            WaitForElementToBeClickable(Name_Field);
            SendKeys(Name_Field, TextToInsert);

            return this;
        }

        public TransportTypeRecordPage InsertCode(string TextToInsert)
        {
            WaitForElementToBeClickable(Code_Field);
            SendKeys(Code_Field, TextToInsert);

            return this;
        }

        public TransportTypeRecordPage InsertGovCode(string TextToInsert)
        {
            WaitForElementToBeClickable(GovCode_Field);
            SendKeys(GovCode_Field, TextToInsert);

            return this;
        }

        public TransportTypeRecordPage SelectTravelTimeCalculation(string TextToSelect)
        {
            WaitForElementToBeClickable(TravelTimeCalculation_Field);
            SelectPicklistElementByText(TravelTimeCalculation_Field, TextToSelect);

            return this;
        }

        public TransportTypeRecordPage InsertSpeed(string TextToInsert)
        {
            WaitForElementToBeClickable(Speed_Field);
            SendKeys(Speed_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TransportTypeRecordPage SelectIcon(string TextToSelect)
        {
            WaitForElementToBeClickable(Icon_Field);
            SelectPicklistElementByText(Icon_Field, TextToSelect);

            return this;
        }

        public TransportTypeRecordPage InsertStartDate(string TextToInsert)
        {
            WaitForElementToBeClickable(StartDate_Field);
            SendKeys(StartDate_Field, TextToInsert);

            return this;
        }

        public TransportTypeRecordPage InsertEndDate(string TextToInsert)
        {
            WaitForElementToBeClickable(EndDate_Field);
            SendKeys(EndDate_Field, TextToInsert);

            return this;
        }

        public TransportTypeRecordPage InsertJourneyBetweenBookings_TotalJourneyTimeExceeds(string TextToInsert)
        {
            WaitForElementToBeClickable(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field);
            SendKeys(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TransportTypeRecordPage InsertJourneyBetweenBookings_TotalJourneyDistanceExceeds(string TextToInsert)
        {
            WaitForElementToBeClickable(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field);
            SendKeys(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TransportTypeRecordPage InsertJourneyBetweenBookings_AverageJourneyTime(string TextToInsert)
        {
            WaitForElementToBeClickable(JourneyBetweenBookings_AverageJourneyTime_Field);
            SendKeys(JourneyBetweenBookings_AverageJourneyTime_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TransportTypeRecordPage InsertJourneyToFromHome_TotalJourneyTimeExceeds(string TextToInsert)
        {
            WaitForElementToBeClickable(JourneyToFromHome_TotalJourneyTimeExceeds_Field);
            SendKeys(JourneyToFromHome_TotalJourneyTimeExceeds_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TransportTypeRecordPage InsertJourneyToFromHome_TotalJourneyDistanceExceeds(string TextToInsert)
        {
            WaitForElementToBeClickable(JourneyToFromHome_TotalJourneyDistanceExceeds_Field);
            SendKeys(JourneyToFromHome_TotalJourneyDistanceExceeds_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public TransportTypeRecordPage InsertJourneyToFromHome_AverageJourneyTime(string TextToInsert)
        {
            WaitForElementToBeClickable(JourneyToFromHome_AverageJourneyTime_Field);
            SendKeys(JourneyToFromHome_AverageJourneyTime_Field, TextToInsert + Keys.Tab);

            return this;
        }



        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field);
            }
            else
            {
                ValidateElementNotDisabled(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field);
            }
            else
            {
                ValidateElementNotDisabled(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(JourneyToFromHome_TotalJourneyTimeExceeds_Field);
            }
            else
            {
                ValidateElementNotDisabled(JourneyToFromHome_TotalJourneyTimeExceeds_Field);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(JourneyToFromHome_TotalJourneyDistanceExceeds_Field);
            }
            else
            {
                ValidateElementNotDisabled(JourneyToFromHome_TotalJourneyDistanceExceeds_Field);
            }

            return this;
        }



        public TransportTypeRecordPage ClickInactiveYesOption()
        {
            WaitForElementToBeClickable(Inactive_YesRadioButton);
            Click(Inactive_YesRadioButton);

            return this;
        }

        public TransportTypeRecordPage ClickInactiveNoOption()
        {
            WaitForElementToBeClickable(Inactive_NoRadioButton);
            Click(Inactive_NoRadioButton);

            return this;
        }

        public TransportTypeRecordPage ClickValidForExportYesOption()
        {
            WaitForElementToBeClickable(ValidForExport_YesRadioButton);
            Click(ValidForExport_YesRadioButton);

            return this;
        }

        public TransportTypeRecordPage ClickValidForExportNoOption()
        {
            WaitForElementToBeClickable(ValidForExport_NoRadioButton);
            Click(ValidForExport_NoRadioButton);

            return this;
        }



        public TransportTypeRecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            ScrollToElement(Name_Field);
            WaitForElement(Name_Field);
            ValidateElementValue(Name_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateCodeFieldValue(string ExpectedValue)
        {
            ScrollToElement(Code_Field);
            WaitForElement(Code_Field);
            ValidateElementValue(Code_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateGovCodeFieldValue(string ExpectedValue)
        {
            ScrollToElement(GovCode_Field);
            WaitForElement(GovCode_Field);
            ValidateElementValue(GovCode_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateTravelTimeCalculationSelectedText(string ExpectedText)
        {
            WaitForElement(TravelTimeCalculation_Field);
            ValidatePicklistSelectedText(TravelTimeCalculation_Field, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateIconSelectedText(string ExpectedText)
        {
            WaitForElement(Icon_Field);
            ValidatePicklistSelectedText(Icon_Field, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateSpeedFieldValue(string ExpectedValue)
        {
            ScrollToElement(Speed_Field);
            WaitForElement(Speed_Field);
            ValidateElementValue(Speed_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            ScrollToElement(StartDate_Field);
            WaitForElement(StartDate_Field);
            ValidateElementValue(StartDate_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateEndDateFieldValue(string ExpectedValue)
        {
            ScrollToElement(EndDate_Field);
            WaitForElement(EndDate_Field);
            ValidateElementValue(EndDate_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateResponsibleTeamLinkFieldText(string ExpectedText)
        {
            ScrollToElement(ResponsibleTeam_FieldLink);
            WaitForElement(ResponsibleTeam_FieldLink);
            ValidateElementText(ResponsibleTeam_FieldLink, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldValue(string ExpectedValue)
        {
            WaitForElement(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field);
            ValidateElementValue(JourneyBetweenBookings_TotalJourneyTimeExceeds_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldValue(string ExpectedValue)
        {
            WaitForElement(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field);
            ValidateElementValue(JourneyBetweenBookings_TotalJourneyDistanceExceeds_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_AverageJourneyTime_FieldValue(string ExpectedValue)
        {
            WaitForElement(JourneyBetweenBookings_AverageJourneyTime_Field);
            ValidateElementValue(JourneyBetweenBookings_AverageJourneyTime_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldValue(string ExpectedValue)
        {
            WaitForElement(JourneyToFromHome_TotalJourneyTimeExceeds_Field);
            ValidateElementValue(JourneyToFromHome_TotalJourneyTimeExceeds_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldValue(string ExpectedValue)
        {
            WaitForElement(JourneyToFromHome_TotalJourneyDistanceExceeds_Field);
            ValidateElementValue(JourneyToFromHome_TotalJourneyDistanceExceeds_Field, ExpectedValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_AverageJourneyTime_FieldValue(string ExpectedValue)
        {
            WaitForElement(JourneyToFromHome_AverageJourneyTime_Field);
            ValidateElementValue(JourneyToFromHome_AverageJourneyTime_Field, ExpectedValue);

            return this;
        }



        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldLabelAttribute(string AtributeName, string ExpectedAtributeValue)
        {
            ValidateElementAttribute(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldLabel, AtributeName, ExpectedAtributeValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldLabelAttribute(string AtributeName, string ExpectedAtributeValue)
        {
            ValidateElementAttribute(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldLabel, AtributeName, ExpectedAtributeValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldLabelAttribute(string AtributeName, string ExpectedAtributeValue)
        {
            ValidateElementAttribute(JourneyToFromHome_TotalJourneyTimeExceeds_FieldLabel, AtributeName, ExpectedAtributeValue);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldLabelAttribute(string AtributeName, string ExpectedAtributeValue)
        {
            ValidateElementAttribute(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldLabel, AtributeName, ExpectedAtributeValue);

            return this;
        }



        public TransportTypeRecordPage ValidateInactiveYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(Inactive_YesRadioButton);
                ValidateElementNotChecked(Inactive_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(Inactive_YesRadioButton);
                ValidateElementChecked(Inactive_NoRadioButton);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateValidForExportYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(ValidForExport_YesRadioButton);
                ValidateElementNotChecked(ValidForExport_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(ValidForExport_YesRadioButton);
                ValidateElementChecked(ValidForExport_NoRadioButton);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateMileageExpenseCostsApplyToThisTransportTypeYesOptionChecked(bool ExpectYesOptionChecked)
        {
            if (ExpectYesOptionChecked)
            {
                ValidateElementChecked(MileageExpenseCostsApplyToThisTransportType_YesRadioButton);
                ValidateElementNotChecked(MileageExpenseCostsApplyToThisTransportType_NoRadioButton);
            }
            else
            {
                ValidateElementNotChecked(MileageExpenseCostsApplyToThisTransportType_YesRadioButton);
                ValidateElementChecked(MileageExpenseCostsApplyToThisTransportType_NoRadioButton);
            }

            return this;
        }



        public TransportTypeRecordPage ValidateNameFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Name_FieldErrorLabel);
            ScrollToElement(Name_FieldErrorLabel);
            ValidateElementText(Name_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateTravelTimeCalculationFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(TravelTimeCalculation_FieldErrorLabel);
            ScrollToElement(TravelTimeCalculation_FieldErrorLabel);
            ValidateElementText(TravelTimeCalculation_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateSpeedFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Speed_FieldErrorLabel);
            ScrollToElement(Speed_FieldErrorLabel);
            ValidateElementText(Speed_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateStartDateFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(StartDate_FieldErrorLabel);
            ScrollToElement(StartDate_FieldErrorLabel);
            ValidateElementText(StartDate_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateIconFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(Icon_FieldErrorLabel);
            ScrollToElement(Icon_FieldErrorLabel);
            ValidateElementText(Icon_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateResponsibleTeamFieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(ResponsibleTeam_FieldErrorLabel);
            ScrollToElement(ResponsibleTeam_FieldErrorLabel);
            ValidateElementText(ResponsibleTeam_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabel);
            ScrollToElement(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabel);
            ValidateElementText(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabel);
            ScrollToElement(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabel);
            ValidateElementText(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_AverageJourneyTime_FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel);
            ScrollToElement(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel);
            ValidateElementText(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(JourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabel);
            ScrollToElement(JourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabel);
            ValidateElementText(JourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabel);
            ScrollToElement(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabel);
            ValidateElementText(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabel, ExpectedText);

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_AverageJourneyTime_FieldErrorLabelText(string ExpectedText)
        {
            WaitForElement(JourneyToFromHome_AverageJourneyTime_FieldErrorLabel);
            ScrollToElement(JourneyToFromHome_AverageJourneyTime_FieldErrorLabel);
            ValidateElementText(JourneyToFromHome_AverageJourneyTime_FieldErrorLabel, ExpectedText);

            return this;
        }



        public TransportTypeRecordPage ValidateNameFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(Name_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Name_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateTravelTimeCalculationFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(TravelTimeCalculation_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(TravelTimeCalculation_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateSpeedFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(Speed_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Speed_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateStartDateFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(StartDate_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(StartDate_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateIconFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(Icon_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(Icon_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateResponsibleTeamFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyBetweenBookings_TotalJourneyTimeExceeds_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyBetweenBookings_TotalJourneyDistanceExceeds_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_AverageJourneyTime_FieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyBetweenBookings_AverageJourneyTimeFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyBetweenBookings_AverageJourneyTime_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyToFromHome_TotalJourneyTimeExceeds_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyToFromHome_TotalJourneyDistanceExceeds_FieldErrorLabel, 3);
            }

            return this;
        }

        public TransportTypeRecordPage ValidateJourneyToFromHome_AverageJourneyTimeFieldErrorLabelVisible(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(JourneyToFromHome_AverageJourneyTime_FieldErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(JourneyToFromHome_AverageJourneyTime_FieldErrorLabel, 3);
            }

            return this;
        }





        public TransportTypeRecordPage ValidateNameFieldIsDisabled()
        {
            WaitForElementVisible(Name_Field);
            ScrollToElement(Name_Field);
            ValidateElementDisabled(Name_Field);

            return this;
        }

        public TransportTypeRecordPage ValidateCodeFieldIsDisabled()
        {
            WaitForElementVisible(Code_Field);
            ScrollToElement(Code_Field);
            ValidateElementDisabled(Code_Field);

            return this;
        }

        public TransportTypeRecordPage ValidateGovCodeFieldIsDisabled()
        {
            WaitForElementVisible(GovCode_Field);
            ScrollToElement(GovCode_Field);
            ValidateElementDisabled(GovCode_Field);

            return this;
        }

        public TransportTypeRecordPage ValidateTravelTimeCalculationFieldIsDisabled()
        {
            WaitForElementVisible(TravelTimeCalculation_Field);
            ScrollToElement(TravelTimeCalculation_Field);
            ValidateElementDisabled(TravelTimeCalculation_Field);

            return this;
        }

        public TransportTypeRecordPage ValidateSpeedFieldIsDisabled()
        {
            WaitForElementVisible(Speed_Field);
            ScrollToElement(Speed_Field);
            ValidateElementDisabled(Speed_Field);

            return this;
        }

        public TransportTypeRecordPage ValidateInactiveFieldOptionsDisabled()
        {
            WaitForElementVisible(Inactive_YesRadioButton);
            ScrollToElement(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_YesRadioButton);
            ValidateElementDisabled(Inactive_NoRadioButton);

            return this;
        }

    }
}