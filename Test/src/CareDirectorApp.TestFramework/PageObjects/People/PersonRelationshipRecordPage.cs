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
    public class PersonRelationshipRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personrelationship_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personrelationship_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _PrimaryPerson_FieldTitle = e => e.Marked("Primary Person");
        readonly Func<AppQuery, AppQuery> _Relationship_FieldTitle = e => e.Marked("Relationship");
        readonly Func<AppQuery, AppQuery> _RelatedPerson_FieldTitle = e => e.Marked("Related Person");
        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _RelatedRelationship_FieldTitle = e => e.Marked("Related Relationship");
        readonly Func<AppQuery, AppQuery> _To_FieldTitle = e => e.Marked("To");

        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.All().Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _Description_FieldTitle = e => e.Marked("Description");

        readonly Func<AppQuery, AppQuery> _InsideHousehold_FieldTitle = e => e.Marked("Inside Household");
        readonly Func<AppQuery, AppQuery> _FamilyMember_FieldTitle = e => e.Marked("Family Member");
        readonly Func<AppQuery, AppQuery> _NextofKinFieldTitle = e => e.Marked("Next of Kin");
        readonly Func<AppQuery, AppQuery> _Advocate_FieldTitle = e => e.Marked("Advocate");
        readonly Func<AppQuery, AppQuery> _IsBirthParent_FieldTitle = e => e.Marked("Is Birth Parent");
        readonly Func<AppQuery, AppQuery> _PowersOfAttorney_FieldTitle = e => e.Marked("Powers of Attorney");
        readonly Func<AppQuery, AppQuery> _FinancialRepresentative_FieldTitle = e => e.Marked("Financial Representative");
        readonly Func<AppQuery, AppQuery> _PrimaryCaseWorkerExternalContact_FieldTitle = e => e.Marked("Primary Case Worker (External Contact)");
        readonly Func<AppQuery, AppQuery> _EmergencyContact_FieldTitle = e => e.Marked("Emergency Contact");
        readonly Func<AppQuery, AppQuery> _KeyHolder_FieldTitle = e => e.Marked("Key Holder");
        readonly Func<AppQuery, AppQuery> _LegalGuardian_FieldTitle = e => e.Marked("Legal Guardian");
        readonly Func<AppQuery, AppQuery> _MHANearestRelative_FieldTitle = e => e.Marked("MHA Nearest Relative");
        readonly Func<AppQuery, AppQuery> _PrimaryCarer_FieldTitle = e => e.Marked("Primary Carer");
        readonly Func<AppQuery, AppQuery> _SecondaryCaregiver_FieldTitle = e => e.Marked("Secondary Caregiver");
        readonly Func<AppQuery, AppQuery> _HasParentalResponsibility_FieldTitle = e => e.Marked("Has Parental Responsibility");
        readonly Func<AppQuery, AppQuery> _PCHR_FieldTitle = e => e.Marked("PCHR");




        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _PrimaryPerson_Field = e => e.Marked("Field_5403244d9c19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Relationship_Field = e => e.Marked("Field_9c1bb06b9c19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _RelatedPerson_Field = e => e.Marked("Field_fa91307f9c19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_8c50e1bcc482e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _RelatedRelationship_Field = e => e.Marked("Field_62bed33e9d19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _To_Field = e => e.Marked("Field_128a34d0c482e911a2c50050569231cf");

        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_389e9a659919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_bc4eca6d9919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_a4ece6759919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Description_Field = e => e.Marked("Field_83b737999919e91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _InsideHousehold_Field = e => e.All().Marked("Field_d5f76da79919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _FamilyMember_Field = e => e.All().Marked("Field_7b695cc89919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _NextofKinField = e => e.All().Marked("Field_7eed3ee19919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Advocate_Field = e => e.All().Marked("Field_73e428f79919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _IsBirthParent_Field = e => e.All().Marked("Field_6f7cb9389a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _PowersOfAttorney_Field = e => e.All().Marked("Field_35cd134c9a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _FinancialRepresentative_Field = e => e.All().Marked("Field_8249dd689a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _PrimaryCaseWorkerExternalContact_Field = e => e.All().Marked("Field_4493b68e9a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _EmergencyContact_Field = e => e.All().Marked("Field_fca4ecbc9919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _KeyHolder_Field = e => e.All().Marked("Field_9cb72fd29919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _LegalGuardian_Field = e => e.All().Marked("Field_e00803ec9919e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _MHANearestRelative_Field = e => e.All().Marked("Field_22caa8309a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _PrimaryCarer_Field = e => e.All().Marked("Field_956414469a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _SecondaryCaregiver_Field = e => e.All().Marked("Field_bf49725d9a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _HasParentalResponsibility_Field = e => e.All().Marked("Field_7f3a7e759a19e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _PCHR_Field = e => e.All().Marked("Field_bc0d45ad9a19e91180dc0050560502cc");


        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion





        public PersonRelationshipRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonRelationshipRecordPage WaitForPersonRelationshipRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);

            return this;
        }

        public PersonRelationshipRecordPage WaitForPersonRelationshipRecordPageToLoad()
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);

            WaitForElement(_topBannerArea);

            return this;
        }



        public PersonRelationshipRecordPage ValidatePrimaryPersonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PrimaryPerson_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PrimaryPerson_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PrimaryPerson_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PrimaryPerson_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelationshipFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Relationship_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Relationship_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Relationship_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Relationship_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelatedPersonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_RelatedPerson_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_RelatedPerson_FieldTitle));
            }
            else
            {
                TryScrollToElement(_RelatedPerson_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_RelatedPerson_FieldTitle));
            }

            return this;
        }


        public PersonRelationshipRecordPage ValidatePersonFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Person_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Person_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Person_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Person_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelatedRelationshipFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_RelatedRelationship_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_RelatedRelationship_FieldTitle));
            }
            else
            {
                TryScrollToElement(_RelatedRelationship_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_RelatedRelationship_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateToFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_To_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_To_FieldTitle));
            }
            else
            {
                TryScrollToElement(_To_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_To_FieldTitle));
            }

            return this;
        }


        public PersonRelationshipRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_StartDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_StartDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_StartDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_StartDate_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_EndDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_EndDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_EndDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_EndDate_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Description_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Description_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Description_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Description_FieldTitle));
            }

            return this;
        }



        public PersonRelationshipRecordPage ValidateInsideHouseholdFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_InsideHousehold_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_InsideHousehold_FieldTitle));
            }
            else
            {
                TryScrollToElement(_InsideHousehold_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_InsideHousehold_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateFamilyMemberFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_FamilyMember_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_FamilyMember_FieldTitle));
            }
            else
            {
                TryScrollToElement(_FamilyMember_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_FamilyMember_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateNextofKinFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_NextofKinFieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_NextofKinFieldTitle));
            }
            else
            {
                TryScrollToElement(_NextofKinFieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_NextofKinFieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateAdvocateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Advocate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Advocate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Advocate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Advocate_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateIsBirthParentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_IsBirthParent_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_IsBirthParent_FieldTitle));
            }
            else
            {
                TryScrollToElement(_IsBirthParent_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_IsBirthParent_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidatePowersOfAttorneyFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PowersOfAttorney_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PowersOfAttorney_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PowersOfAttorney_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PowersOfAttorney_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateFinancialRepresentativeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_FinancialRepresentative_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_FinancialRepresentative_FieldTitle));
            }
            else
            {
                TryScrollToElement(_FinancialRepresentative_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_FinancialRepresentative_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidatePrimaryCaseWorkerExternalContactFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PrimaryCaseWorkerExternalContact_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PrimaryCaseWorkerExternalContact_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PrimaryCaseWorkerExternalContact_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PrimaryCaseWorkerExternalContact_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateEmergencyContactFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_EmergencyContact_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_EmergencyContact_FieldTitle));
            }
            else
            {
                TryScrollToElement(_EmergencyContact_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_EmergencyContact_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateKeyHolderFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_KeyHolder_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_KeyHolder_FieldTitle));
            }
            else
            {
                TryScrollToElement(_KeyHolder_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_KeyHolder_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateLegalGuardianFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_LegalGuardian_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_LegalGuardian_FieldTitle));
            }
            else
            {
                TryScrollToElement(_LegalGuardian_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_LegalGuardian_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateMHANearestRelativeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_MHANearestRelative_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MHANearestRelative_FieldTitle));
            }
            else
            {
                TryScrollToElement(_MHANearestRelative_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MHANearestRelative_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidatePrimaryCarerFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PrimaryCarer_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PrimaryCarer_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PrimaryCarer_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PrimaryCarer_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateSecondaryCaregiverFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_SecondaryCaregiver_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_SecondaryCaregiver_FieldTitle));
            }
            else
            {
                TryScrollToElement(_SecondaryCaregiver_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_SecondaryCaregiver_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidateHasParentalResponsibilityFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_HasParentalResponsibility_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_HasParentalResponsibility_FieldTitle));
            }
            else
            {
                TryScrollToElement(_HasParentalResponsibility_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_HasParentalResponsibility_FieldTitle));
            }

            return this;
        }
        public PersonRelationshipRecordPage ValidatePCHRFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PCHR_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PCHR_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PCHR_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PCHR_FieldTitle));
            }

            return this;
        }






        public PersonRelationshipRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdBy_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdOn_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }

            return this;
        }

        public PersonRelationshipRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }

            return this;
        }






        public PersonRelationshipRecordPage ValidatePrimaryPersonFieldText(string ExpectText)
        {
            ScrollToElement(_PrimaryPerson_Field);
            string fieldText = GetElementText(_PrimaryPerson_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelationshipFieldText(string ExpectText)
        {
            ScrollToElement(_Relationship_Field);
            string fieldText = GetElementText(_Relationship_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelatedPersonFieldText(string ExpectText)
        {
            ScrollToElement(_RelatedPerson_Field);
            string fieldText = GetElementText(_RelatedPerson_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonRelationshipRecordPage ValidatePersonFieldText(string ExpectDateText)
        {
            ScrollToElement(_Person_Field);

            string fieldText = GetElementText(_Person_Field);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateRelatedRelationshipFieldText(string ExpectDateText)
        {
            ScrollToElement(_RelatedRelationship_Field);

            string fieldText = GetElementText(_RelatedRelationship_Field);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateToFieldText(string ExpectText)
        {
            ScrollToElement(_To_Field);
            string fieldText = GetElementText(_To_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public PersonRelationshipRecordPage ValidateStartDateFieldText(string ExpectText)
        {
            ScrollToElement(_StartDate_Field);
            string fieldText = GetElementText(_StartDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_Field);
            string fieldText = GetElementText(_EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateResponsibleTeamFieldText(string ExpectText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            string fieldText = GetElementText(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateDescriptionFieldText(string ExpectText)
        {
            ScrollToElement(_Description_Field);
            string fieldText = GetElementText(_Description_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }



        public PersonRelationshipRecordPage ValidateInsideHouseholdFieldText(string ExpectText)
        {
            ScrollToElement(_InsideHousehold_Field);
            string fieldText = GetElementText(_InsideHousehold_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateFamilyMemberFieldText(string ExpectText)
        {
            ScrollToElement(_FamilyMember_Field);
            string fieldText = GetElementText(_FamilyMember_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateNextofKinFieldText(string ExpectText)
        {
            ScrollToElement(_NextofKinField);
            string fieldText = GetElementText(_NextofKinField);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateAdvocateFieldText(string ExpectText)
        {
            ScrollToElement(_Advocate_Field);
            string fieldText = GetElementText(_Advocate_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateIsBirthParentFieldText(string ExpectText)
        {
            ScrollToElement(_IsBirthParent_Field);
            string fieldText = GetElementText(_IsBirthParent_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidatePowersOfAttorneyFieldText(string ExpectText)
        {
            ScrollToElement(_PowersOfAttorney_Field);
            string fieldText = GetElementText(_PowersOfAttorney_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateFinancialRepresentativeFieldText(string ExpectText)
        {
            ScrollToElement(_FinancialRepresentative_Field);
            string fieldText = GetElementText(_FinancialRepresentative_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidatePrimaryCaseWorkerExternalContactFieldText(string ExpectText)
        {
            ScrollToElement(_PrimaryCaseWorkerExternalContact_Field);
            string fieldText = GetElementText(_PrimaryCaseWorkerExternalContact_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateEmergencyContactFieldText(string ExpectText)
        {
            ScrollToElement(_EmergencyContact_Field);
            string fieldText = GetElementText(_EmergencyContact_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateKeyHolderFieldText(string ExpectText)
        {
            ScrollToElement(_KeyHolder_Field);
            string fieldText = GetElementText(_KeyHolder_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateLegalGuardianFieldText(string ExpectText)
        {
            ScrollToElement(_LegalGuardian_Field);
            string fieldText = GetElementText(_LegalGuardian_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateMHANearestRelativeFieldText(string ExpectText)
        {
            ScrollToElement(_MHANearestRelative_Field);
            string fieldText = GetElementText(_MHANearestRelative_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidatePrimaryCarerFieldText(string ExpectText)
        {
            ScrollToElement(_PrimaryCarer_Field);
            string fieldText = GetElementText(_PrimaryCarer_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateSecondaryCaregiverFieldText(string ExpectText)
        {
            ScrollToElement(_SecondaryCaregiver_Field);
            string fieldText = GetElementText(_SecondaryCaregiver_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidateHasParentalResponsibilityFieldText(string ExpectText)
        {
            ScrollToElement(_HasParentalResponsibility_Field);
            string fieldText = GetElementText(_HasParentalResponsibility_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }
        public PersonRelationshipRecordPage ValidatePCHRFieldText(string ExpectText)
        {
            ScrollToElement(_PCHR_Field);
            string fieldText = GetElementText(_PCHR_Field);
            Assert.AreEqual(ExpectText, fieldText);
            return this;
        }




        public PersonRelationshipRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonRelationshipRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






    

    }
}
