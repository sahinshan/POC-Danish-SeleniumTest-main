
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class PersonPage : CommonMethods
    {

        #region Top Area

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("SaveAndCloseButton");
        readonly Func<AppQuery, AppQuery> _navigationButton = e => e.Marked("person_MapNavigation");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("MainStackLayout").Descendant().Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string PersonName) => e => e.Text("PERSON: " + PersonName);

        #endregion

        #region Top Banner

        readonly Func<AppQuery, AppQuery> _personNameAndId_TopBanner = e => e.Marked("text_heading");
        readonly Func<AppQuery, AppQuery> _bornLabel_TopBanner = e => e.Marked("label_Born:");
        readonly Func<AppQuery, AppQuery> _bornText_TopBanner = e => e.Marked("text_Born:");
        readonly Func<AppQuery, AppQuery> _genderLabel_TopBanner = e => e.Marked("label_Gender:");
        readonly Func<AppQuery, AppQuery> _genderText_TopBanner = e => e.Marked("text_Gender:");
        readonly Func<AppQuery, AppQuery> _nhsLabel_TopBanner = e => e.Marked("label_NHS No:");
        readonly Func<AppQuery, AppQuery> _nhsText_TopBanner = e => e.Marked("text_NHS No:");
        readonly Func<AppQuery, AppQuery> _toogleIcon_TopBanner = e => e.Marked("toggleIcon");
        readonly Func<AppQuery, AppQuery> _preferredNameLabel_TopBanner = e => e.Marked("label_Preferred Name:");
        readonly Func<AppQuery, AppQuery> _preferredNameText_TopBanner = e => e.Marked("text_Preferred Name:");

        readonly Func<AppQuery, AppQuery> _primaryAddressLabel_TopBanner = e => e.Marked("label_Address (Home)");
        readonly Func<AppQuery, AppQuery> _primaryAddressText_TopBanner = e => e.Marked("text_Address (Home)");
        readonly Func<AppQuery, AppQuery> _phoneAndEmailLabel_TopBanner = e => e.Marked("label_Phone and Email");
        readonly Func<AppQuery, AppQuery> _homeLabel_TopBanner = e => e.Marked("label_Home:");
        readonly Func<AppQuery, AppQuery> _homeText_TopBanner = e => e.Marked("text_Home:");
        readonly Func<AppQuery, AppQuery> _workLabel_TopBanner = e => e.Marked("label_Work:");
        readonly Func<AppQuery, AppQuery> _workText_TopBanner = e => e.Marked("text_Work:");
        readonly Func<AppQuery, AppQuery> _mobileLabel_TopBanner = e => e.Marked("label_Mobile:");
        readonly Func<AppQuery, AppQuery> _mobileText_TopBanner = e => e.Marked("text_Mobile:");
        readonly Func<AppQuery, AppQuery> _emailLabel_TopBanner = e => e.Marked("label_Email:");
        readonly Func<AppQuery, AppQuery> _emailText_TopBanner = e => e.Marked("text_Email:");

        #endregion

        #region Fields
        readonly Func<AppQuery, AppQuery> _idLabel = e => e.Text("Id");
        readonly Func<AppQuery, AppQuery> _titleLabel = e => e.Text("Title");
        readonly Func<AppQuery, AppQuery> _firstNameLabel = e => e.Text("First Name");
        readonly Func<AppQuery, AppQuery> _middleNameLabel = e => e.Text("Middle Name");
        readonly Func<AppQuery, AppQuery> _lastNameLabel = e => e.Text("Last Name");
        readonly Func<AppQuery, AppQuery> _preferredNameLabel = e => e.Text("Preferred Name");
        readonly Func<AppQuery, AppQuery> _statedGenderLabel = e => e.Text("Stated Gender");
        readonly Func<AppQuery, AppQuery> _dobLabel = e => e.Text("DOB");
        readonly Func<AppQuery, AppQuery> _dateOfDeathLabel = e => e.Text("Date of Death");
        readonly Func<AppQuery, AppQuery> _preferredLanguageLabel = e => e.Text("Preferred Language");
        readonly Func<AppQuery, AppQuery> _responsibleTeamLabel = e => e.Text("Responsible Team");
        readonly Func<AppQuery, AppQuery> _profilePictureLabel = e => e.Text("Profile Picture");
        readonly Func<AppQuery, AppQuery> _nhsNoLabel = e => e.Text("NHS No.");
        readonly Func<AppQuery, AppQuery> _reasonsForNoNHSNoLabel = e => e.Text("Reason for no NHS No.");
        readonly Func<AppQuery, AppQuery> _ethnicityLabel = e => e.Text("Ethnicity");
        readonly Func<AppQuery, AppQuery> _maritalStatusLabel = e => e.Text("Marital Status");
        readonly Func<AppQuery, AppQuery> _ageLabel = e => e.Text("Age");
        readonly Func<AppQuery, AppQuery> _interpreterRequiredLabel = e => e.Text("Interpreter Required?");

        readonly Func<AppQuery, AppQuery> _addressTypeLabel = e => e.Text("Address Type");
        readonly Func<AppQuery, AppQuery> _propertyTypeLabel = e => e.Text("Property Type");
        readonly Func<AppQuery, AppQuery> _propertyNameLabel = e => e.Text("Property Name");
        readonly Func<AppQuery, AppQuery> _propertyNoLabel = e => e.Text("Property No.");
        readonly Func<AppQuery, AppQuery> _streetLabel = e => e.Text("Street");
        readonly Func<AppQuery, AppQuery> _vlgDistrictLabel = e => e.Text("Village/District");
        readonly Func<AppQuery, AppQuery> _townCityLabel = e => e.Text("Town/City");
        readonly Func<AppQuery, AppQuery> _countyLabel = e => e.Text("County");
        readonly Func<AppQuery, AppQuery> _postcodeLabel = e => e.Text("Postcode");
        readonly Func<AppQuery, AppQuery> _uprnLabel = e => e.Text("UPRN");
        readonly Func<AppQuery, AppQuery> _businessPhoneLabel = e => e.Text("Business Phone");
        readonly Func<AppQuery, AppQuery> _homePhoneLabel = e => e.Text("Home Phone");
        readonly Func<AppQuery, AppQuery> _mobilePhoneLabel = e => e.Text("Mobile Phone");
        readonly Func<AppQuery, AppQuery> _primaryEmailLabel = e => e.Text("Primary Email");
        readonly Func<AppQuery, AppQuery> _telephone1Label = e => e.Text("Telephone 1");
        readonly Func<AppQuery, AppQuery> _telephone2Label = e => e.Text("Telephone 2");
        readonly Func<AppQuery, AppQuery> _telephone3Label = e => e.Text("Telephone 3");
        readonly Func<AppQuery, AppQuery> _secondaryEmailLabel = e => e.Text("Secondary Email");


        readonly Func<AppQuery, AppQuery> _idField = e => e.Marked("Field_ae5cef502b3ce91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _titleField = e => e.Marked("Field_1ad4ed5a8b19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _firstNameField = e => e.Marked("Field_ac7036ddc880e61180d20050560502cc");
        readonly Func<AppQuery, AppQuery> _middleNameField = e => e.Marked("Field_28783714c980e61180d20050560502cc");
        readonly Func<AppQuery, AppQuery> _lastNameField = e => e.Marked("Field_6c2a90e6c880e61180d20050560502cc");
        readonly Func<AppQuery, AppQuery> _preferredNameField = e => e.Marked("Field_2f95feaa8b19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _statedGenderField = e => e.Marked("Field_4f95df20c980e61180d20050560502cc");
        readonly Func<AppQuery, AppQuery> _dobField = e => e.Marked("Field_576efa2ac980e61180d20050560502cc");
        readonly Func<AppQuery, AppQuery> _dateOfDeathField = e => e.Marked("Field_640ac642c980e61180d20050560502cc");
        readonly Func<AppQuery, AppQuery> _preferredLanguageField = e => e.Marked("Field_d29eb8038c19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _responsibleTeamField = e => e.Marked("Field_5cc7bf0b8c19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _imagePhotoButton = e => e.Marked("imagePhoto").Class("ImageRenderer");
        readonly Func<AppQuery, AppQuery> _imagePictureButton = e => e.Marked("imagePicture");
        readonly Func<AppQuery, AppQuery> _personPicture = e => e.Marked("entityImage");
        readonly Func<AppQuery, AppQuery> _nhsNoField = e => e.Marked("Field_ca03fd498b19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _reasonsForNoNHSNoField = e => e.Marked("Field_3946adaa2b3ce91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _ethnicityField = e => e.Marked("Field_30989cf58b19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _maritalStatusField = e => e.Marked("Field_985001e88b19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _ageField = e => e.Marked("Field_2b6aa9b92b3ce91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _interpreterRequiredField = e => e.Marked("Field_d85d66547e8ee911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _addressTypeField = e => e.Marked("Field_a7ba82428c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _propertyTypeField = e => e.Marked("Field_0d8857558c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _propertyNameField = e => e.Marked("Field_9b2cea698c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _propertyNoField = e => e.Marked("Field_50028c758c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _streetField = e => e.Marked("Field_6eef3d7f8c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _vlgDistrictField = e => e.Marked("Field_ce6675a98c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _townCityField = e => e.Marked("Field_3a5e4eb28c19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _countyField = e => e.Marked("Field_dc491fbc8c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _postcodeField = e => e.Marked("Field_a71062d48c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _uprnField = e => e.Marked("Field_2c8012dd8c19e91180dc0050560502cc").Class("LabelAppCompatRenderer");
        readonly Func<AppQuery, AppQuery> _businessPhoneField = e => e.Marked("Field_4ff30f808d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _homePhoneField = e => e.Marked("Field_74e747248d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _mobilePhoneField = e => e.Marked("Field_5ea29e2b8d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _primaryEmailField = e => e.Marked("Field_e1aa32348e19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _telephone1Field = e => e.Marked("Field_fd235c008d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _telephone2Field = e => e.Marked("Field_ccd31b35fc3fe911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _telephone3Field = e => e.Marked("Field_c6c9513efc3fe911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _secondaryEmailField = e => e.Marked("Field_e4f07a48fc3fe911a2c40050569231cf");

        readonly Func<AppQuery, AppQuery> _businessPhoneScrollableField = e => e.Marked("Field_4ff30f808d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _homePhoneScrollableField = e => e.Marked("Field_74e747248d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _mobilePhoneScrollableField = e => e.Marked("Field_5ea29e2b8d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _primaryEmailScrollableField = e => e.Marked("Field_e1aa32348e19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _telephone1ScrollableField = e => e.Marked("Field_fd235c008d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _telephone2ScrollableField = e => e.Marked("Field_ccd31b35fc3fe911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _telephone3ScrollableField = e => e.Marked("Field_c6c9513efc3fe911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _secondaryEmailScrollableField = e => e.Marked("Field_e4f07a48fc3fe911a2c40050569231cf");


        #endregion

        #region Phone and SMS buttons

        readonly Func<AppQuery, AppQuery> _businessPhoneSMSButton = e => e.Marked("SMSImage_businessphone");
        readonly Func<AppQuery, AppQuery> _homePhoneSMSButton = e => e.Marked("SMSImage_homephone");
        readonly Func<AppQuery, AppQuery> _mobilePhoneSMSButton = e => e.Marked("SMSImage_mobilephone");
        readonly Func<AppQuery, AppQuery> _telephone1SMSButton = e => e.Marked("SMSImage_telephone1");
        readonly Func<AppQuery, AppQuery> _telephone2SMSButton = e => e.Marked("SMSImage_telephone2");
        readonly Func<AppQuery, AppQuery> _telephone3SMSButton = e => e.Marked("SMSImage_telephone3");

        readonly Func<AppQuery, AppQuery> _businessPhoneCallButton = e => e.Marked("CallImage_businessphone");
        readonly Func<AppQuery, AppQuery> _homePhoneCallButton = e => e.Marked("CallImage_homephone");
        readonly Func<AppQuery, AppQuery> _mobilePhoneCallButton = e => e.Marked("CallImage_mobilephone");
        readonly Func<AppQuery, AppQuery> _telephone1CallButton = e => e.Marked("CallImage_telephone1");
        readonly Func<AppQuery, AppQuery> _telephone2CallButton = e => e.Marked("CallImage_telephone2");
        readonly Func<AppQuery, AppQuery> _telephone3CallButton = e => e.Marked("CallImage_telephone3");

        #endregion

        #region Footer
        readonly Func<AppQuery, AppQuery> _createdByFooterLabel = e => e.Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdOnFooterLabel = e => e.Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedByFooterLabel = e => e.Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedOnFooterLabel = e => e.Marked("FooterLabel_modifiedon");
        #endregion

        #region Related Items

        readonly Func<AppQuery, AppQuery> _relatedItemsButton = e => e.Marked("RelatedItemsButton");
        readonly Func<AppQuery, AppQuery> _personDetailsArea = e => e.Text("Personal Details");
        readonly Func<AppQuery, AppQuery> _addressArea = e => e.Text("Address");
        readonly Func<AppQuery, AppQuery> _phoneAndEmailArea = e => e.Text("Phone & E-mail");


        readonly Func<AppQuery, AppQuery> _relatedItemsContentView = e => e.Marked("RelatedItemsContentView");
        readonly Func<AppQuery, AppQuery> _activitiesLabel = e => e.Marked("Activities_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _relatedItemsLabel = e => e.Marked("RelatedItems_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _healthLabel = e => e.Marked("Health_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _financeLabel = e => e.Marked("Finance_CategoryLabel");

        readonly Func<AppQuery, AppQuery> _tasksRelatedItem = e => e.Marked("Activities_Item_Tasks");
        readonly Func<AppQuery, AppQuery> _appointmentsRelatedItem = e => e.Marked("Activities_Item_Appointment");
        readonly Func<AppQuery, AppQuery> _caseNotesRelatedItem = e => e.Marked("Activities_Item_CaseNotes");

        readonly Func<AppQuery, AppQuery> _personFormsRelatedItem = e => e.Marked("RelatedItems_Item_PersonForm");
        readonly Func<AppQuery, AppQuery> _PersonAlertAndHazardRelatedItem = e => e.Marked("RelatedItems_Item_PersonAlertAndHazard");
        readonly Func<AppQuery, AppQuery> _PersonRelationshipsRelatedItem = e => e.Marked("RelatedItems_Item_PersonRelationships");

        readonly Func<AppQuery, AppQuery> _personAllergiesRelatedItem = e => e.Marked("Health_Item_PersonAllergies");
        readonly Func<AppQuery, AppQuery> _personDisabilitiesRelatedItem = e => e.Marked("Health_Item_PersonDisabilityImpairments");
        readonly Func<AppQuery, AppQuery> _personBodyMapsRelatedItem = e => e.Marked("Health_Item_BodyMaps");

        readonly Func<AppQuery, AppQuery> _financeDetailsRelatedItem = e => e.Marked("Finance_Item_FinanceDetails");
        readonly Func<AppQuery, AppQuery> _financialAssessmentssRelatedItem = e => e.Marked("Financial Assessment");

        readonly Func<AppQuery, AppQuery> _personDNARRecordsRelatedItem = e => e.Text("Records of DNAR");

        #endregion


        public PersonPage(IApp app)
        {
            _app = app;
        }



        #region Related Items

        public PersonPage WaitForRelatedItemsSubMenuToOpen()
        {
            this._app.WaitForElement(_relatedItemsContentView);
            this._app.WaitForElement(_activitiesLabel);
            this._app.WaitForElement(_relatedItemsLabel);
            this._app.WaitForElement(_relatedItemsLabel);


            return this;
        }

        public PersonPage TapRelatedItemsButton()
        {
            this._app.Tap(_relatedItemsButton);

            return this;
        }

        public PersonPage TapActivitiesArea_RelatedItems()
        {
            Tap(_activitiesLabel);

            return this;
        }

        public PersonPage TapRelatedItemsArea_RelatedItems()
        {
            this._app.Tap(_relatedItemsLabel);

            return this;
        }

        public PersonPage TapHealthArea_RelatedItems()
        {
            this._app.Tap(_healthLabel);

            return this;
        }


        public PersonPage TapHealthArea_RecordsOfDNAR_RelatedItems()
        {
            ScrollToElement(_personDNARRecordsRelatedItem);
            this._app.Tap(_personDNARRecordsRelatedItem);

            return this;
        }
        public PersonPage TapFinanceArea_RelatedItems()
        {
            Tap(_financeLabel);

            return this;
        }


        public TasksPage TapTasksIcon_RelatedItems()
        {
            this._app.Tap(_tasksRelatedItem);

            return new TasksPage(this._app);
        }

        public PersonPage TapCaseNotesIcon_RelatedItems()
        {
            this._app.Tap(_caseNotesRelatedItem);

            return this;
        }

        public PersonPage TaAppointmentsIcon_RelatedItems()
        {
            this._app.Tap(_appointmentsRelatedItem);

            return this;
        }


        public PersonPage TapAlertAndHazardIcon_RelatedItems()
        {
            this._app.Tap(_PersonAlertAndHazardRelatedItem);

            return this;
        }

        public PersonPage TapPersonFormsIcon_RelatedItems()
        {
            this._app.Tap(_personFormsRelatedItem);

            return this;
        }

        public PersonPage TapPersonAllergiesIcon_RelatedItems()
        {
            this._app.Tap(_personAllergiesRelatedItem);

            return this;
        }

        public PersonPage TapPersonRelationshipsIcon_RelatedItems()
        {
            this._app.Tap(_PersonRelationshipsRelatedItem);

            return this;
        }

        public PersonPage TapPersonDisabilitiesIcon_RelatedItems()
        {
            this._app.Tap(_personDisabilitiesRelatedItem);

            return this;
        }

        //


        public PersonPage TapBodyMapsIcon_RelatedItems()
        {
            Tap(_personBodyMapsRelatedItem);

            return this;
        }


        public PersonPage TapFinanceDetailsIcon_RelatedItems()
        {
            Tap(_financeDetailsRelatedItem);

            return this;
        }


        public PersonPage TapFinancialAssessmentsIcon_RelatedItems()
        {
            Tap(_financialAssessmentssRelatedItem);

            return this;
        }


        public PersonPage ValidateActivitiesAreaNotVisible_RelatedItems()
        {
            WaitForElementNotVisible(_activitiesLabel);

            return this;
        }

        public PersonPage ValidateActivitiesAreaElementsVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_tasksRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");

            elementVisible = this._app.Query(_appointmentsRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_appointmentsRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseNotesRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_caseNotesRelatedItem element was not visible");

            return this;
        }

        public PersonPage ValidateActivitiesAreaElementsNotVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_tasksRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");

            elementVisible = this._app.Query(_appointmentsRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_appointmentsRelatedItem element was not visible");

            elementVisible = this._app.Query(_caseNotesRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_caseNotesRelatedItem element was not visible");

            return this;
        }

        public PersonPage ValidateRelatedItemsAreaElementsVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_personFormsRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_personFormsRelatedItem element was not visible");


            return this;
        }

        public PersonPage ValidateRelatedItemsAreaElementsNotVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_personFormsRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");


            return this;
        }

        public PersonPage ValidateHealthAreaElementsVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_personAllergiesRelatedItem).Any();
            if (!elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");


            return this;
        }

        public PersonPage ValidatePersonBodyMapsElementVisible_RelatedItems()
        {
            bool elementVisible = this.CheckIfElementVisible(_personBodyMapsRelatedItem);
            if (!elementVisible)
                throw new Exception("_personBodyMapsRelatedItem element was not visible");


            return this;
        }

        public PersonPage ValidateHealthAreaElementsNotVisible_RelatedItems()
        {
            bool elementVisible = this._app.Query(_personAllergiesRelatedItem).Any();
            if (elementVisible)
                throw new Exception("_tasksRelatedItem element was not visible");


            return this;
        }

        public PersonPage ValidateFinanceAreaNotVisible_RelatedItems()
        {
            WaitForElementNotVisible(_financeLabel);

            return this;
        }

        #region Mobile View Mode



        #endregion




        #endregion


        public PersonPage WaitForPersonPageToLoad(string PersonName)
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_navigationButton);
            _app.WaitForElement(_peoplePageIconButton);
            _app.WaitForElement(_pageTitle(PersonName));

            _app.WaitForElement(_relatedItemsButton);

            return this;
        }
        public PersonPage ValidateFieldLabelsVisible()
        {
            ScrollToElement(_idField);
            WaitForElement(_idLabel);

            ScrollToElement(_imagePictureButton);
            WaitForElement(_profilePictureLabel);

            ScrollToElement(_titleField);
            WaitForElement(_titleLabel);

            ScrollToElement(_firstNameField);
            WaitForElement(_firstNameLabel);

            ScrollToElement(_middleNameField);
            WaitForElement(_middleNameLabel);

            ScrollToElement(_lastNameField);
            WaitForElement(_lastNameLabel);

            ScrollToElement(_preferredNameField);
            WaitForElement(_preferredNameLabel);

            ScrollToElement(_statedGenderField);
            WaitForElement(_statedGenderLabel);

            ScrollToElement(_dobField);
            WaitForElement(_dobLabel);

            ScrollToElement(_dateOfDeathField);
            WaitForElement(_dateOfDeathLabel);

            ScrollToElement(_preferredLanguageField);
            WaitForElement(_preferredLanguageLabel);

            ScrollToElement(_responsibleTeamField);
            WaitForElement(_responsibleTeamLabel);
            
            

            ScrollToElement(_nhsNoField);
            
            WaitForElement(_nhsNoLabel);

            ScrollToElement(_reasonsForNoNHSNoField);
            
            WaitForElement(_reasonsForNoNHSNoLabel);

            ScrollToElement(_ethnicityField);
            
            WaitForElement(_ethnicityLabel);

            ScrollToElement(_maritalStatusField);
            
            WaitForElement(_maritalStatusLabel);

            ScrollToElement(_ageField);
            
            WaitForElement(_ageLabel);

            ScrollToElement(_interpreterRequiredField);
            WaitForElement(_interpreterRequiredLabel);

            //////////////////////////

            ScrollToElement(_addressTypeField);
            
            WaitForElement(_addressTypeLabel);

            ScrollToElement(_propertyTypeField);
            
            WaitForElement(_propertyTypeLabel);

            ScrollToElement(_propertyNameField);
            
            WaitForElement(_propertyNameLabel);

            ScrollToElement(_propertyNoField);
            
            WaitForElement(_propertyNoLabel);

            ScrollToElement(_streetField);
            
            WaitForElement(_streetLabel);

            ScrollToElement(_vlgDistrictField);
            
            WaitForElement(_vlgDistrictLabel);

            ScrollToElement(_townCityField);
            
            WaitForElement(_townCityLabel);

            ScrollToElement(_countyField);
            
            WaitForElement(_countyLabel);

            ScrollToElement(_postcodeField);
            
            WaitForElement(_postcodeLabel);

            ScrollToElement(_uprnField);
            
            WaitForElement(_uprnLabel);

            ///////////////////////

            ScrollToElement(_businessPhoneField);
            
            WaitForElement(_businessPhoneLabel);

            ScrollToElement(_homePhoneField);
            
            WaitForElement(_homePhoneLabel);

            ScrollToElement(_mobilePhoneField);
            
            WaitForElement(_mobilePhoneLabel);

            ScrollToElement(_primaryEmailField);
            
            WaitForElement(_primaryEmailLabel);

            ScrollToElement(_telephone1Field);
            
            WaitForElement(_telephone1Label);

            ScrollToElement(_telephone2Field);
            
            WaitForElement(_telephone2Label);

            ScrollToElement(_telephone3Field);
            
            WaitForElement(_telephone3Label);

            ScrollToElement(_secondaryEmailField);
            
            WaitForElement(_secondaryEmailLabel);

            return this;
        }


        public PersonPage ExpandTopBanner()
        {
            this._app.Tap(_toogleIcon_TopBanner);

            return this;
        }
        public PersonPage CollapseTopBanner()
        {
            this._app.Tap(_toogleIcon_TopBanner);

            return this;
        }
        public TeamPage TapResponsibleTeamField()
        {
            ScrollToElement(_responsibleTeamField);
            ScrollDown();

            this._app.Tap(_responsibleTeamField);

            return new TeamPage(this._app);
        }

        public PersonPage TapbackButton()
        {
            Tap(_backButton);

            return this;
        }

        public PersonPage ValidateMainTopBannerLabelsVisible()
        {
            bool elementVisible = this._app.Query(_bornLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Born label");

            elementVisible = this._app.Query(_genderLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Gender label");

            elementVisible = this._app.Query(_nhsLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the NHS Number label");

            elementVisible = this._app.Query(_preferredNameLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Preferred Name label");


            return this;
        }
        public PersonPage ValidateSecondayTopBannerLabelsNotVisible()
        {
            bool elementVisible = this._app.Query(_primaryAddressLabel_TopBanner).Any();
            if (elementVisible)
                throw new Exception("Unable to find the primary address label");

            elementVisible = this._app.Query(_phoneAndEmailLabel_TopBanner).Any();
            if (elementVisible)
                throw new Exception("Unable to find the phone and email label");

            elementVisible = this._app.Query(_homeLabel_TopBanner).Any();
            if (elementVisible)
                throw new Exception("Unable to find the Home label");

            elementVisible = this._app.Query(_workLabel_TopBanner).Any();
            if (elementVisible)
                throw new Exception("Unable to find the Work label");

            elementVisible = this._app.Query(_mobileLabel_TopBanner).Any();
            if (elementVisible)
                throw new Exception("Unable to find the Mobile label");

            elementVisible = this._app.Query(_emailLabel_TopBanner).Any();
            if (elementVisible)
                throw new Exception("Unable to find the Email label");


            return this;
        }
        public PersonPage ValidateSecondayTopBannerLabelsVisible()
        {
            bool elementVisible = this._app.Query(_primaryAddressLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the primary address label");

            elementVisible = this._app.Query(_phoneAndEmailLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the phone and email label");

            elementVisible = this._app.Query(_homeLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Home label");

            elementVisible = this._app.Query(_workLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Work label");

            elementVisible = this._app.Query(_mobileLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Mobile label");

            elementVisible = this._app.Query(_emailLabel_TopBanner).Any();
            if (!elementVisible)
                throw new Exception("Unable to find the Email label");


            return this;
        }

        public PersonPage ValidatePersonNameAndId_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_personNameAndId_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateBornText_TopBanner(DateTime DateOfBirth)
        {
            string expectedFieldText = GetFullBirthDateText(DateOfBirth);
            string fieldText = this._app.Query(_bornText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(expectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateGenderText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_genderText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateNHSNoText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_nhsText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePreferredNameText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_preferredNameText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateAddressText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_primaryAddressText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateHomePhoneText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_homeText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateWorkPhoneText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_workText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateMobilePhoneText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_mobileText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateEmailText_TopBanner(string ExpectedFieldText)
        {
            string fieldText = this._app.Query(_emailText_TopBanner).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }



        public PersonPage ValidatePersonIDField(string ExpectedFieldText)
        {
            ScrollToElement(_idField);
            ScrollDown();
            ScrollToElement(_idField);
            string fieldText = this._app.Query(_idField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateTitleField(string ExpectedFieldText)
        {
            ScrollToElement(_titleField);

            string fieldText = this._app.Query(_titleField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateFirstNameField(string ExpectedFieldText)
        {
            ScrollToElement(_firstNameField);

            string fieldText = this._app.Query(_firstNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateMidleNameField(string ExpectedFieldText)
        {
            ScrollToElement(_middleNameField);

            string fieldText = this._app.Query(_middleNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateLastNameField(string ExpectedFieldText)
        {
            ScrollToElement(_lastNameField);

            string fieldText = this._app.Query(_lastNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePreferredNameField(string ExpectedFieldText)
        {
            ScrollToElement(_preferredNameField);

            string fieldText = this._app.Query(_preferredNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateStatedGenderField(string ExpectedFieldText)
        {
            ScrollToElement(_statedGenderField);

            string fieldText = this._app.Query(_statedGenderField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateDOBField(string ExpectedFieldText)
        {
            ScrollToElement(_dobField);

            string fieldText = this._app.Query(_dobField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateDateOfDeathField(string ExpectedFieldText)
        {
            ScrollToElement(_dateOfDeathField);
            
            string fieldText = this.GetElementText(_dateOfDeathField);
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePreferredLanguageField(string ExpectedFieldText)
        {
            ScrollToElement(_preferredLanguageField);

            string fieldText = this._app.Query(_preferredLanguageField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateResponsibleTeamField(string ExpectedFieldText)
        {
            ScrollToElement(_responsibleTeamField);

            string fieldText = this._app.Query(_responsibleTeamField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePictureAreaField()
        {
            ScrollToElement(_imagePhotoButton);

            bool elementVisible = this._app.Query(_imagePhotoButton).Any();
            Assert.IsTrue(elementVisible);

            ScrollToElement(_imagePictureButton);

            elementVisible = this._app.Query(_imagePictureButton).Any();
            Assert.IsTrue(elementVisible);

            ScrollToElement(_personPicture);

            elementVisible = this._app.Query(_personPicture).Any();
            Assert.IsTrue(elementVisible);

            return this;
        }
        public PersonPage ValidateNHSNoField(string ExpectedFieldText)
        {
            ScrollToElement(_nhsNoField);

            string fieldText = this._app.Query(_nhsNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateReasonsForNoNHSNoField(string ExpectedFieldText)
        {
            ScrollToElement(_reasonsForNoNHSNoField);

            string fieldText = this._app.Query(_reasonsForNoNHSNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateEthnicityField(string ExpectedFieldText)
        {
            ScrollToElement(_ethnicityField);

            string fieldText = this._app.Query(_ethnicityField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateMaritalStatusField(string ExpectedFieldText)
        {
            ScrollToElement(_maritalStatusField);

            string fieldText = this._app.Query(_maritalStatusField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateAgeField(string ExpectedFieldText)
        {
            ScrollToElement(_ageField);

            string fieldText = this._app.Query(_ageField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }




        public PersonPage ValidateAddressTypeField(string ExpectedFieldText)
        {
            ScrollToElement(_addressTypeField);

            string fieldText = this._app.Query(_addressTypeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePropertyTypeField(string ExpectedFieldText)
        {
            ScrollToElement(_propertyTypeField);

            string fieldText = this._app.Query(_propertyTypeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePropertyNameField(string ExpectedFieldText)
        {
            ScrollToElement(_propertyNameField);

            string fieldText = this._app.Query(_propertyNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePropertyNoField(string ExpectedFieldText)
        {
            ScrollToElement(_propertyNoField);

            string fieldText = this._app.Query(_propertyNoField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateStreetField(string ExpectedFieldText)
        {
            ScrollToElement(_streetField);

            string fieldText = this._app.Query(_streetField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateVlgDistrictField(string ExpectedFieldText)
        {
            ScrollToElement(_vlgDistrictField);

            string fieldText = this._app.Query(_vlgDistrictField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateTownCityField(string ExpectedFieldText)
        {
            ScrollToElement(_townCityField);

            string fieldText = this._app.Query(_townCityField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateCountyField(string ExpectedFieldText)
        {
            ScrollToElement(_countyField);

            string fieldText = this._app.Query(_countyField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePostCodeField(string ExpectedFieldText)
        {
            ScrollToElement(_postcodeField);

            string fieldText = this._app.Query(_postcodeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateUPRNField(string ExpectedFieldText)
        {
            ScrollToElement(_uprnField);

            string fieldText = this._app.Query(_uprnField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }



        public PersonPage ValidateBusinessPhoneField(string ExpectedFieldText)
        {
            ScrollToElement(_businessPhoneScrollableField);
            ScrollDown();
            ScrollToElement(_businessPhoneScrollableField);

            string fieldText = this._app.Query(_businessPhoneField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateHomePhoneField(string ExpectedFieldText)
        {
            ScrollToElement(_homePhoneScrollableField);

            string fieldText = this._app.Query(_homePhoneField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateMobilePhoneField(string ExpectedFieldText)
        {
            ScrollToElement(_mobilePhoneScrollableField);

            string fieldText = this._app.Query(_mobilePhoneField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidatePrimaryEmailField(string ExpectedFieldText)
        {
            ScrollToElement(_primaryEmailScrollableField);

            string fieldText = this._app.Query(_primaryEmailField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateTelephone1Field(string ExpectedFieldText)
        {
            ScrollToElement(_telephone1ScrollableField);

            string fieldText = this._app.Query(_telephone1Field).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateTelephone2Field(string ExpectedFieldText)
        {
            ScrollToElement(_telephone2ScrollableField);

            string fieldText = this._app.Query(_telephone2Field).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateTelephone3Field(string ExpectedFieldText)
        {
            ScrollToElement(_telephone3ScrollableField);

            string fieldText = this._app.Query(_telephone3Field).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        public PersonPage ValidateSecondaryEmailField(string ExpectedFieldText)
        {
            ScrollToElement(_secondaryEmailScrollableField);
            ScrollDown();
            string fieldText = this._app.Query(_secondaryEmailField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }


        public PersonPage ValidateBusinessPhoneSMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_businessPhoneSMSButton);

            bool enabled = this._app.Query(_businessPhoneSMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateHomePhoneSMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_homePhoneSMSButton);

            bool enabled = this._app.Query(_homePhoneSMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateMobilePhoneSMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_mobilePhoneSMSButton);

            bool enabled = this._app.Query(_mobilePhoneSMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateTelephone1SMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_telephone1SMSButton);

            bool enabled = this._app.Query(_telephone1SMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateTelephone2SMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_telephone2SMSButton);

            bool enabled = this._app.Query(_telephone2SMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateTelephone3SMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_telephone3SMSButton);

            bool enabled = this._app.Query(_telephone3SMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }

        public PersonPage ValidateBusinessPhoneCallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_businessPhoneCallButton);

            bool enabled = this._app.Query(_businessPhoneCallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateHomePhoneCallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_homePhoneCallButton);

            bool enabled = this._app.Query(_homePhoneCallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateMobilePhoneCallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_mobilePhoneCallButton);

            bool enabled = this._app.Query(_mobilePhoneCallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateTelephone1CallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_telephone1CallButton);

            bool enabled = this._app.Query(_telephone1CallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateTelephone2CallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_telephone2CallButton);

            bool enabled = this._app.Query(_telephone2CallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        public PersonPage ValidateTelephone3CallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_telephone3CallButton);

            bool enabled = this._app.Query(_telephone3CallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }


        public PersonPage ValidateCreatedOnFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_createdOnFooterLabel);

            string fieldText = this._app.Query(_createdOnFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public PersonPage ValidateCreatedByFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_createdByFooterLabel);

            string fieldText = this._app.Query(_createdByFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public PersonPage ValidateModifiedOnFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_modifiedOnFooterLabel);

            string fieldText = this._app.Query(_modifiedOnFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public PersonPage ValidateModifiedByFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_modifiedByFooterLabel);

            string fieldText = this._app.Query(_modifiedByFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        private string GetFullBirthDateText(DateTime DateOfBirth)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            DateTime currentDate = DateTime.Now.Date;

            TimeSpan span = currentDate - DateOfBirth;

            // Because we start at year 1 for the Gregorian
            // c\ndar, we must subtract a year here.
            int years = (zeroTime + span).Year - 1;
            int month = (zeroTime + span).Month - 1;


            if ((zeroTime + span).Year >= 18)
            {
                System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
                return string.Format("{0} ({1} Years)", DateOfBirth.ToString("dd'/'MM'/'yyyy"), years);
            }
            else
            {
                System.Globalization.CultureInfo enUS = new System.Globalization.CultureInfo("en-US");
                if (month == 1)
                {
                    return string.Format("{0} ({1} Years, {2} Month)", DateOfBirth.ToString("dd'/'MM'/'yyyy"), years, month);
                }
                else
                {
                    return string.Format("{0} ({1} Years, {2} Months)", DateOfBirth.ToString("dd'/'MM'/'yyyy"), years, month);
                }
            }



        }
    }
}
