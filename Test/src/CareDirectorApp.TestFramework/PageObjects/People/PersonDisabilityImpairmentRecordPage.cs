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
    public class PersonDisabilityImpairmentRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("persondisabilityimpairments_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("persondisabilityimpairments_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Disability_FieldTitle = e => e.Marked("Disability");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _Severity_FieldTitle = e => e.Marked("Severity");
        readonly Func<AppQuery, AppQuery> _CVIReceivedDate_FieldTitle = e => e.Marked("CVI Received Date");
        readonly Func<AppQuery, AppQuery> _OnsetDate_FieldTitle = e => e.Marked("Onset Date");

        readonly Func<AppQuery, AppQuery> _Impairment_FieldTitle = e => e.Marked("Impairment");
        readonly Func<AppQuery, AppQuery> _DiagnosisDate_FieldTitle = e => e.Marked("Diagnosis Date");
        readonly Func<AppQuery, AppQuery> _NotifiedDate_FieldTitle = e => e.Marked("Notified Date");
        readonly Func<AppQuery, AppQuery> _RegisteredDisabilityNo_FieldTitle = e => e.Marked("Registered Disability No");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Disability_Field = e => e.Marked("Field_efb204888319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_dc5132a58319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _Severity_Field = e => e.Marked("Field_99dfa3ae8319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _CVIRecievedDate_Field = e => e.Marked("Field_ac8066c88319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _OnsetDate_Field = e => e.All().Marked("Field_42062ae18319e91180dc0050560502cc");
        
        readonly Func<AppQuery, AppQuery> _Impairment_Field = e => e.Marked("Field_152388958319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _DiagnosisDate_Field = e => e.Marked("Field_95dfa3ae8319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _NotifiedDate_Field = e => e.Marked("Field_29f2cad58319e91180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _RegisteredDisabilityNo_Field = e => e.Marked("Field_2d0427bf8319e91180dc0050560502cc");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion



        public PersonDisabilityImpairmentRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonDisabilityImpairmentRecordPage WaitForPersonDisabilityImpairmentRecordPageToLoad(string PageTitleText)
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

        public PersonDisabilityImpairmentRecordPage WaitForPersonDisabilityImpairmentRecordPageToLoad()
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);

            WaitForElement(_topBannerArea);

            return this;
        }



        public PersonDisabilityImpairmentRecordPage ValidateDisabilityFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Disability_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Disability_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Disability_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Disability_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonDisabilityImpairmentRecordPage ValidateSeverityFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Severity_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Severity_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Severity_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Severity_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateCVIRecievedDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_CVIReceivedDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_CVIReceivedDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_CVIReceivedDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_CVIReceivedDate_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateOnsetDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_OnsetDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_OnsetDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_OnsetDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_OnsetDate_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateImpairmentFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Impairment_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Impairment_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Impairment_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Impairment_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateDiagnosisDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DiagnosisDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DiagnosisDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DiagnosisDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DiagnosisDate_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateNotifiedDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_NotifiedDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_NotifiedDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_NotifiedDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_NotifiedDate_FieldTitle));
            }

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateRegisteredDisabilityNoFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_RegisteredDisabilityNo_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_RegisteredDisabilityNo_FieldTitle));
            }
            else
            {
                TryScrollToElement(_RegisteredDisabilityNo_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_RegisteredDisabilityNo_FieldTitle));
            }

            return this;
        }







        public PersonDisabilityImpairmentRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public PersonDisabilityImpairmentRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public PersonDisabilityImpairmentRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public PersonDisabilityImpairmentRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public PersonDisabilityImpairmentRecordPage ValidateDisabilityFieldText(string ExpectText)
        {
            ScrollToElement(_Disability_Field);
            string fieldText = GetElementText(_Disability_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateStartDateFieldText(string ExpectText)
        {
            ScrollToElement(_StartDate_Field);
            string fieldText = GetElementText(_StartDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateSeverityFieldText(string ExpectText)
        {
            ScrollToElement(_Severity_Field);
            string fieldText = GetElementText(_Severity_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateCVIRecievedDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_CVIRecievedDate_Field);

            string fieldText = GetElementText(_CVIRecievedDate_Field);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateOnsetDateFieldText(string ExpectText)
        {
            ScrollToElement(_OnsetDate_Field);
            string fieldText = GetElementText(_OnsetDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        
        public PersonDisabilityImpairmentRecordPage ValidateImpairmentFieldText(string ExpectText)
        {
            ScrollToElement(_Impairment_Field);
            string fieldText = GetElementText(_Impairment_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateDiagnosisDateFieldText(string ExpectText)
        {
            ScrollToElement(_DiagnosisDate_Field);
            string fieldText = GetElementText(_DiagnosisDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateNotifiedDateFieldText(string ExpectText)
        {
            ScrollToElement(_NotifiedDate_Field);
            string fieldText = GetElementText(_NotifiedDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateRegisteredDisabilityNoFieldText(string ExpectText)
        {
            ScrollToElement(_RegisteredDisabilityNo_Field);
            string fieldText = GetElementText(_RegisteredDisabilityNo_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }





        public PersonDisabilityImpairmentRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDisabilityImpairmentRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






        

    }
}
