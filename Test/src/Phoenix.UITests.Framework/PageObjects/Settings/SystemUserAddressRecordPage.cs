
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SystemUserAddressRecordPage : CommonMethods
    {
        public SystemUserAddressRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuseraddress&')]");
        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By save_Button = By.Id("TI_SaveButton");
        readonly By saveAndClose_Button = By.Id("TI_SaveAndCloseButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/*/*/button[@title='Back'][@onclick='CW.DataForm.Close(); return false;']");
        readonly By delete_Button = By.Id("TI_DeleteRecordButton");
        readonly By menu_Button = By.Id("CWNavGroup_Menu");
        readonly By audit_Button = By.Id("CWNavItem_AuditHistory");


        #region Fields

        readonly By systemUser_Field = By.Id("CWField_systemuserid");
        readonly By systemUser_LinkField = By.Id("CWField_systemuserid_Link");
        readonly By systemUser_LoopUpButton = By.Id("CWLookupBtn_systemuserid");
       
        readonly By propertyname_Field = By.Id("CWField_propertyname");
        readonly By propertyno_Field = By.Id("CWField_addressline1");
        readonly By street_Field = By.Id("CWField_addressline2");
        readonly By villageDistrict_Field = By.Id("CWField_addressline3");
        readonly By townCity_Field = By.Id("CWField_addressline4");
        readonly By postcode_Field = By.Id("CWField_postcode");

        readonly By county_Field = By.Id("CWField_addressline5");
        readonly By country_Field = By.Id("CWField_country");
        readonly By addressType_Field = By.Id("CWField_addresstypeid");
        readonly By startDate_Field = By.Id("CWField_startdate");
        readonly By endDate_Field = By.Id("CWField_enddate");

        readonly By addressSearch_Button = By.Id("CWFieldButton_AddressSearch");


        readonly By addressType_MandatoryField = By.XPath("//*[@id='CWLabelHolder_addresstypeid']/label/span[@class='mandatory']");
        readonly By startDate_MandatoryField = By.XPath("//*[@id='CWLabelHolder_startdate']/label/span[@class ='mandatory']");

        #endregion

        #region FieldLabel
        readonly By propertyname_FieldLabel = By.Id("CWLabelHolder_propertyname");
        readonly By propertyno_FieldLabel = By.Id("CWLabelHolder_addressline1");
        readonly By street_FieldLabel = By.Id("CWLabelHolder_addressline2");
        readonly By villageDistrict_FieldLabel = By.Id("CWLabelHolder_addressline3");
        readonly By townCity_FieldLabel = By.Id("CWLabelHolder_addressline4");
        readonly By postcode_FieldLabel = By.Id("CWLabelHolder_postcode");

        readonly By county_FieldLabel = By.Id("CWLabelHolder_addressline5");
        readonly By country_FieldLabel = By.Id("CWLabelHolder_country");
        readonly By addressType_FieldLabel = By.Id("CWLabelHolder_addresstypeid");
        readonly By startDate_FieldLabel = By.Id("CWLabelHolder_startdate");
        readonly By endDate_FieldLabel = By.Id("CWLabelHolder_enddate");

        readonly By addressSearch_ButtonLabel = By.Id("CWControlHolder_AddressSearch");

        #endregion



        public SystemUserAddressRecordPage WaitForSystemUserAddressRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);
           
            return this;
        }        

        public SystemUserAddressRecordPage ValidateLeftSideFields()
        {
            ValidateElementEnabled(propertyname_Field);
            ValidateElementEnabled(propertyno_Field);
            ValidateElementEnabled(street_Field);
            ValidateElementEnabled(villageDistrict_Field);
            ValidateElementEnabled(townCity_Field);
            ValidateElementEnabled(postcode_Field);
            //ValidateElementEnabled(addressSearch_Button);

            return this;
        }
        public SystemUserAddressRecordPage ValidateRightSideFields()
        {
            ValidateElementEnabled(county_Field);
            ValidateElementEnabled(country_Field);
            ValidateElementEnabled(addressType_Field);
            ValidateElementEnabled(startDate_Field);
            ValidateElementEnabled(endDate_Field);
           
            return this;
        }

        public SystemUserAddressRecordPage ValidateSystemUserAddressRecordPageTitle(string PageTitle)
        {


            ValidateElementTextContainsText(pageHeader, PageTitle);
            return this;
        }

        public SystemUserAddressRecordPage WaitForSystemUserAddressRecordPageTitleToLoad(string PageTitle)
        {
            WaitForSystemUserAddressRecordPageToLoad();

            WaitForElementToContainText(pageHeader, "System User Address:\r\n" + PageTitle);

            return this;
        }

        public SystemUserAddressRecordPage ValidateAddressType_MandatoryField()
        {
            ValidateElementEnabled(addressType_MandatoryField);


            return this;
        }

        public SystemUserAddressRecordPage ValidateStartDateMandatoryFields()
        {
            ValidateElementEnabled(startDate_MandatoryField);


            return this;
        }

        public SystemUserAddressRecordPage ValidateStartDateText(string ExpectedText)
        {
            WaitForElementVisible(startDate_Field);
            ValidateElementValue(startDate_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage ValidateAddressType_Options(string TextToFind)
        {
            ValidatePicklistContainsElementByText(addressType_Field, "Home");
            ValidatePicklistContainsElementByText(addressType_Field, "Work");
      


            return this;
        }

        public SystemUserAddressRecordPage ValidateTownCityText(string ExpectedText)
        {
            WaitForElementVisible(townCity_Field);
            ValidateElementValue(townCity_Field, ExpectedText);
           


            return this;
        }


        public SystemUserAddressRecordPage ValidatePropertyNameText(string ExpectedText)
        {
            WaitForElementVisible(propertyname_Field);
            ValidateElementValue(propertyname_Field, ExpectedText);



            return this;
        }

        public SystemUserAddressRecordPage ValidatePropertyNoText(string ExpectedText)
        {
            WaitForElementVisible(propertyno_Field);
            ValidateElementValue(propertyno_Field, ExpectedText);



            return this;
        }

        public SystemUserAddressRecordPage ValidateCountyText(string ExpectedText)
        {
            WaitForElementVisible(county_Field);
            ValidateElementValue(county_Field, ExpectedText);



            return this;
        }

        public SystemUserAddressRecordPage ValidatePostCodeText(string ExpectedText)
        {
            WaitForElementVisible(postcode_Field);
            ValidateElementValue(postcode_Field, ExpectedText);



            return this;
        }

        public SystemUserAddressRecordPage ValidateCountryText(string ExpectedText)
        {
            WaitForElementVisible(country_Field);
            ValidateElementValue(country_Field, ExpectedText);



            return this;
        }

        public SystemUserAddressRecordPage ValidateVillageDistrictText(string ExpectedText)
        {
            WaitForElementVisible(villageDistrict_Field);
            ValidateElementValue(villageDistrict_Field, ExpectedText);



            return this;
        }

        public SystemUserAddressRecordPage ValidateStreetText(string ExpectedText)
        {
            WaitForElementVisible(street_Field);
            ValidateElementValue(street_Field, ExpectedText);



            return this;
        }


        public SystemUserAddressRecordPage ValidateAddressTypeText(string ExpectedText)
        {
            WaitForElementVisible(addressType_Field);
            ValidateElementValue(addressType_Field, ExpectedText);



            return this;
        }


        public SystemUserAddressRecordPage ValidateToolTipTextForSystemUser(string ExpectedText)
        {
            

            ValidateElementToolTip(systemUser_Field, ExpectedText);
           
            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForPropertyName(string ExpectedText)
        {

            ValidateElementToolTip(propertyname_Field, ExpectedText);
           
            return this;
        }


        public SystemUserAddressRecordPage ValidateToolTipTextForPropertyNo(string ExpectedText)
        {

            ValidateElementToolTip(propertyno_Field, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForStreet(string ExpectedText)
        {

            ValidateElementToolTip(street_Field, ExpectedText);

            return this;
        }


        public SystemUserAddressRecordPage ValidateToolTipTextForVillageDistrict(string ExpectedText)
        {

            ValidateElementToolTip(villageDistrict_Field, ExpectedText);

            return this;
        }
        public SystemUserAddressRecordPage ValidateToolTipTextForTownCity(string ExpectedText)
        {

            ValidateElementToolTip(townCity_Field, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForPostCode(string ExpectedText)
        {

            ValidateElementToolTip(postcode_Field, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForCounty(string ExpectedText)
        {

            ValidateElementToolTip(county_Field, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForCountry(string ExpectedText)
        {

            ValidateElementToolTip(country_Field, ExpectedText);

            return this;
        }


        public SystemUserAddressRecordPage ValidateToolTipTextForAddressType(string ExpectedText)
        {

            ValidateElementToolTip(addressType_Field, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForStartDate(string ExpectedText)
        {

            ValidateElementToolTip(startDate_Field, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateToolTipTextForEndDate(string ExpectedText)
        {

            ValidateElementToolTip(endDate_Field, ExpectedText);

            return this;
        }


        public SystemUserAddressRecordPage SelectAddressType_Options(string TextToSelect)
        {
            WaitForElement(addressType_Field);
            SelectPicklistElementByText(addressType_Field, TextToSelect);

            return this;
        }


        public SystemUserAddressRecordPage ValidateSystemUser_LinkField(string ExpectedText)
        {
            WaitForElement(systemUser_LinkField);
            ValidateElementTextContainsText(systemUser_LinkField, ExpectedText);

            return this;
        }

        public SystemUserAddressRecordPage ValidateSystemUser_Editable(bool ExpectedText)
        {
            if (ExpectedText)
            {

                WaitForElementToBeClickable(systemUser_LinkField);
                Click(systemUser_Field);

            }
            else
            {
                WaitForElement(systemUser_LinkField, 5);
                
            }
            return this;
        }

        public SystemUserAddressRecordPage ClickSystemUserLoopUpButton()
        {
            WaitForElement(systemUser_LoopUpButton);
            Click(systemUser_LoopUpButton);

            return this;
        }


        public SystemUserAddressRecordPage InsertStartDate(string DateToInsert)
        {
            WaitForElement(startDate_Field);
            SendKeys(startDate_Field, DateToInsert);


            return this;
        }

        public SystemUserAddressRecordPage TabToNextElement()
        {
            WaitForElementVisible(startDate_Field);
            SendKeysWithoutClearing(startDate_Field, Keys.Tab);

            return this;
        }

        public SystemUserAddressRecordPage InsertPropertyName(string ExpectedText)
        {
            WaitForElement(propertyname_Field);
            SendKeys(propertyname_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertPropertyNo(string ExpectedText)
        {
            WaitForElement(propertyno_Field);
            SendKeys(propertyno_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertStreet(string ExpectedText)
        {
            WaitForElement(street_Field);
            SendKeys(street_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertVillageDistrict(string ExpectedText)
        {
            WaitForElement(villageDistrict_Field);
            SendKeys(villageDistrict_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertTownCity(string ExpectedText)
        {
            WaitForElement(townCity_Field);
            SendKeys(townCity_Field, ExpectedText);


            return this;
        }


        public SystemUserAddressRecordPage InsertPostCode(string ExpectedText)
        {
            WaitForElement(postcode_Field);
            SendKeys(postcode_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertCounty(string ExpectedText)
        {
            WaitForElement(county_Field);
            SendKeys(county_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertCountry(string ExpectedText)
        {
            WaitForElement(country_Field);
            SendKeys(country_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage InsertEndDate(string InsertDate)
        {
            WaitForElement(endDate_Field);
            SendKeys(endDate_Field, InsertDate);


            return this;
        }

        public SystemUserAddressRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(save_Button);
            MoveToElementInPage(save_Button);
            Click(save_Button);
            return this;
        }

        public SystemUserAddressRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndClose_Button);
            Click(saveAndClose_Button);
            return this;
        }

        public SystemUserAddressRecordPage ValidateEndDate(string ExpectedText)
        {
            WaitForElementVisible(endDate_Field);
            ValidateElementValue(endDate_Field, ExpectedText);


            return this;
        }

        public SystemUserAddressRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(back_Button);
            Click(back_Button);
            return this;
        }

        public SystemUserAddressRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(delete_Button);
            Click(delete_Button);
            return this;
        }

        public SystemUserAddressRecordPage NavigateToMenuSubPage_Aduit()
        {


            WaitForElement(menu_Button);
            Click(menu_Button);

            WaitForElement(audit_Button);
            Click(audit_Button);

            return this;
        }

        public SystemUserAddressRecordPage ClickAddressSearchButton()
        {
            WaitForElementToBeClickable(addressSearch_Button);
            Click(addressSearch_Button);
            return this;
        }

    }
}
