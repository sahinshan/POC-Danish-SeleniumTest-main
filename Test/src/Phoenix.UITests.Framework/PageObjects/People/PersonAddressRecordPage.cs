using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.People;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class PersonAddressRecordPage : CommonMethods
	{

        public PersonAddressRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personaddress&')]");


        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By AssignRecordButton = By.XPath("//*[@id='TI_AssignRecordButton']");
		readonly By ErrorCorrection = By.XPath("//*[@id='TI_ErrorCorrection']");
		readonly By RestrictAccessButton = By.XPath("//*[@id='TI_RestrictAccessButton']");
		readonly By DeleteRecordButton = By.XPath("//*[@id='TI_DeleteRecordButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");

        readonly By PersonidLink = By.XPath("//*[@id='CWField_personid_Link']");
		readonly By PersonidLookupButton = By.XPath("//*[@id='CWLookupBtn_personid']");
		readonly By Addresstypeid = By.XPath("//*[@id='CWField_addresstypeid']");
		readonly By Propertyname = By.XPath("//*[@id='CWField_propertyname']");
		readonly By Addressline1 = By.XPath("//*[@id='CWField_addressline1']");
		readonly By Addressline2 = By.XPath("//*[@id='CWField_addressline2']");
		readonly By Addressline3 = By.XPath("//*[@id='CWField_addressline3']");
		readonly By Addressline4 = By.XPath("//*[@id='CWField_addressline4']");
		readonly By Addressline5 = By.XPath("//*[@id='CWField_addressline5']");
		readonly By Postcode = By.XPath("//*[@id='CWField_postcode']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By AddresspropertytypeidLink = By.XPath("//*[@id='CWField_addresspropertytypeid_Link']");
		readonly By AddresspropertytypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_addresspropertytypeid']");
		readonly By PlacementtypeidLink = By.XPath("//*[@id='CWField_placementtypeid_Link']");
		readonly By PlacementtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_placementtypeid']");
		readonly By Uprn = By.XPath("//*[@id='CWField_uprn']");
		readonly By AddressboroughidLink = By.XPath("//*[@id='CWField_addressboroughid_Link']");
		readonly By AddressboroughidLookupButton = By.XPath("//*[@id='CWLookupBtn_addressboroughid']");
		readonly By AddresswardidLink = By.XPath("//*[@id='CWField_addresswardid_Link']");
		readonly By AddresswardidLookupButton = By.XPath("//*[@id='CWLookupBtn_addresswardid']");
		readonly By Country = By.XPath("//*[@id='CWField_country']");
		readonly By Accommodationstatusid = By.XPath("//*[@id='CWField_accommodationstatusid']");
		readonly By AccommodationtypeidLink = By.XPath("//*[@id='CWField_accommodationtypeid_Link']");
		readonly By AccommodationtypeidLookupButton = By.XPath("//*[@id='CWLookupBtn_accommodationtypeid']");
		readonly By Telephone1 = By.XPath("//*[@id='CWField_telephone1']");
		readonly By Telephone2 = By.XPath("//*[@id='CWField_telephone2']");
		readonly By Telephone3 = By.XPath("//*[@id='CWField_telephone3']");
		readonly By Startdate = By.XPath("//*[@id='CWField_startdate']");
		readonly By StartdateDatePicker = By.XPath("//*[@id='CWField_startdate_DatePicker']");
		readonly By Comments = By.XPath("//*[@id='CWField_comments']");
		readonly By Enddate = By.XPath("//*[@id='CWField_enddate']");
		readonly By EnddateDatePicker = By.XPath("//*[@id='CWField_enddate_DatePicker']");


        public PersonAddressRecordPage WaitForPersonAddressRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

			WaitForElementVisible(BackButton);

            return this;
        }

        public PersonAddressRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public PersonAddressRecordPage ClickAssignRecordButton()
		{
			WaitForElementToBeClickable(AssignRecordButton);
			Click(AssignRecordButton);

			return this;
		}

		public PersonAddressRecordPage ClickErrorCorrection()
		{
			WaitForElementToBeClickable(ErrorCorrection);
			Click(ErrorCorrection);

			return this;
		}

		public PersonAddressRecordPage ClickRestrictAccessButton()
		{
			WaitForElementToBeClickable(RestrictAccessButton);
			Click(RestrictAccessButton);

			return this;
		}

		public PersonAddressRecordPage ClickDeleteRecordButton()
		{
			WaitForElementToBeClickable(DeleteRecordButton);
			Click(DeleteRecordButton);

			return this;
		}

        public PersonAddressRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public PersonAddressRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public PersonAddressRecordPage ClickPersonLink()
		{
			WaitForElementToBeClickable(PersonidLink);
			Click(PersonidLink);

			return this;
		}

		public PersonAddressRecordPage ValidatePersonLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PersonidLink);
			ValidateElementText(PersonidLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickPersonLookupButton()
		{
			WaitForElementToBeClickable(PersonidLookupButton);
			Click(PersonidLookupButton);

			return this;
		}

		public PersonAddressRecordPage SelectAddresstype(string TextToSelect)
		{
			WaitForElementToBeClickable(Addresstypeid);
			SelectPicklistElementByText(Addresstypeid, TextToSelect);

			return this;
		}

		public PersonAddressRecordPage ValidateAddresstypeSelectedText(string ExpectedText)
		{
			WaitForElementVisible(Addresstypeid);
            ValidatePicklistSelectedText(Addresstypeid, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ValidatePropertyNameText(string ExpectedText)
        {
            WaitForElementVisible(Propertyname);
            ValidateElementValue(Propertyname, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnPropertName(string TextToInsert)
		{
			WaitForElementToBeClickable(Propertyname);
			SendKeys(Propertyname, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidatePropertNoText(string ExpectedText)
        {
            WaitForElementVisible(Addressline1);
            ValidateElementValue(Addressline1, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnPropertNo(string TextToInsert)
		{
			WaitForElementToBeClickable(Addressline1);
			SendKeys(Addressline1, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateStreetText(string ExpectedText)
        {
            WaitForElementVisible(Addressline2);
            ValidateElementValue(Addressline2, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnStreet(string TextToInsert)
		{
			WaitForElementToBeClickable(Addressline2);
			SendKeys(Addressline2, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateVlgDistrictText(string ExpectedText)
        {
            WaitForElementVisible(Addressline3);
            ValidateElementValue(Addressline3, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnVlgDistrict(string TextToInsert)
		{
			WaitForElementToBeClickable(Addressline3);
			SendKeys(Addressline3, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateTownCityText(string ExpectedText)
        {
            WaitForElementVisible(Addressline4);
            ValidateElementValue(Addressline4, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnTownCity(string TextToInsert)
		{
			WaitForElementToBeClickable(Addressline4);
			SendKeys(Addressline4, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateCountyText(string ExpectedText)
        {
            WaitForElementVisible(Addressline5);
            ValidateElementValue(Addressline5, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnCounty(string TextToInsert)
		{
			WaitForElementToBeClickable(Addressline5);
			SendKeys(Addressline5, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidatePostcodeText(string ExpectedText)
        {
            WaitForElementVisible(Postcode);
            ValidateElementValue(Postcode, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnPostcode(string TextToInsert)
		{
			WaitForElementToBeClickable(Postcode);
			SendKeys(Postcode, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ClickResponsibleTeamLink()
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public PersonAddressRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElementToBeClickable(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		public PersonAddressRecordPage ClickPropertyTypeLink()
		{
			WaitForElementToBeClickable(AddresspropertytypeidLink);
			Click(AddresspropertytypeidLink);

			return this;
		}

		public PersonAddressRecordPage ValidatePropertyTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AddresspropertytypeidLink);
			ValidateElementText(AddresspropertytypeidLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickPropertyTypeLookupButton()
		{
			WaitForElementToBeClickable(AddresspropertytypeidLookupButton);
			Click(AddresspropertytypeidLookupButton);

			return this;
		}

		public PersonAddressRecordPage ClickPlacementTypeLink()
		{
			WaitForElementToBeClickable(PlacementtypeidLink);
			Click(PlacementtypeidLink);

			return this;
		}

		public PersonAddressRecordPage ValidatePlacementTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(PlacementtypeidLink);
			ValidateElementText(PlacementtypeidLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickPlacementTypeLookupButton()
		{
			WaitForElementToBeClickable(PlacementtypeidLookupButton);
			Click(PlacementtypeidLookupButton);

			return this;
		}

		public PersonAddressRecordPage ValidateUprnText(string ExpectedText)
        {
            WaitForElementVisible(Uprn);
            ValidateElementValue(Uprn, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnUprn(string TextToInsert)
		{
			WaitForElementToBeClickable(Uprn);
			SendKeys(Uprn, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ClickBoroughLink()
		{
			WaitForElementToBeClickable(AddressboroughidLink);
			Click(AddressboroughidLink);

			return this;
		}

		public PersonAddressRecordPage ValidateBoroughLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AddressboroughidLink);
			ValidateElementText(AddressboroughidLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickBoroughLookupButton()
		{
			WaitForElementToBeClickable(AddressboroughidLookupButton);
			Click(AddressboroughidLookupButton);

			return this;
		}

		public PersonAddressRecordPage ClickWardLink()
		{
			WaitForElementToBeClickable(AddresswardidLink);
			Click(AddresswardidLink);

			return this;
		}

		public PersonAddressRecordPage ValidateWardLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AddresswardidLink);
			ValidateElementText(AddresswardidLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickWardLookupButton()
		{
			WaitForElementToBeClickable(AddresswardidLookupButton);
			Click(AddresswardidLookupButton);

			return this;
		}

		public PersonAddressRecordPage ValidateCountryText(string ExpectedText)
		{
			ValidateElementValue(Country, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnCountry(string TextToInsert)
		{
			WaitForElementToBeClickable(Country);
			SendKeys(Country, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage SelectAccommodationStatus(string TextToSelect)
		{
			WaitForElementToBeClickable(Accommodationstatusid);
			SelectPicklistElementByText(Accommodationstatusid, TextToSelect);

			return this;
		}

		public PersonAddressRecordPage ValidateAccommodationStatusSelectedText(string ExpectedText)
        {
            WaitForElementVisible(Accommodationstatusid);
            ValidateElementText(Accommodationstatusid, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickAccommodationTypeLink()
		{
			WaitForElementToBeClickable(AccommodationtypeidLink);
			Click(AccommodationtypeidLink);

			return this;
		}

		public PersonAddressRecordPage ValidateAccommodationTypeLinkText(string ExpectedText)
		{
			WaitForElementToBeClickable(AccommodationtypeidLink);
			ValidateElementText(AccommodationtypeidLink, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage ClickAccommodationTypeLookupButton()
		{
			WaitForElementToBeClickable(AccommodationtypeidLookupButton);
			Click(AccommodationtypeidLookupButton);

			return this;
		}

		public PersonAddressRecordPage ValidateTelephone1Text(string ExpectedText)
        {
            WaitForElementVisible(Telephone1);
            ValidateElementValue(Telephone1, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnTelephone1(string TextToInsert)
		{
			WaitForElementToBeClickable(Telephone1);
			SendKeys(Telephone1, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateTelephone2Text(string ExpectedText)
        {
            WaitForElementVisible(Telephone2);
            ValidateElementValue(Telephone2, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnTelephone2(string TextToInsert)
		{
			WaitForElementToBeClickable(Telephone2);
			SendKeys(Telephone2, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateTelephone3Text(string ExpectedText)
        {
            WaitForElementVisible(Telephone3);
            ValidateElementValue(Telephone3, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnTelephone3(string TextToInsert)
		{
			WaitForElementToBeClickable(Telephone3);
			SendKeys(Telephone3, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateStartDateText(string ExpectedText)
        {
            WaitForElementVisible(Startdate);
            ValidateElementValue(Startdate, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnStartDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Startdate);
			SendKeys(Startdate, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ClickStartDateDatePicker()
		{
			WaitForElementToBeClickable(StartdateDatePicker);
			Click(StartdateDatePicker);

			return this;
		}

		public PersonAddressRecordPage ValidateCommentsText(string ExpectedText)
        {
            WaitForElementVisible(Comments);
            ValidateElementText(Comments, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnComments(string TextToInsert)
		{
			WaitForElementToBeClickable(Comments);
			SendKeys(Comments, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ValidateEndDateText(string ExpectedText)
        {
            WaitForElementVisible(Enddate);
            ValidateElementValue(Enddate, ExpectedText);

			return this;
		}

		public PersonAddressRecordPage InsertTextOnEndDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Enddate);
			SendKeys(Enddate, TextToInsert);
			
			return this;
		}

		public PersonAddressRecordPage ClickEndDateDatePicker()
		{
			WaitForElementToBeClickable(EnddateDatePicker);
			Click(EnddateDatePicker);

			return this;
		}

	}
}
