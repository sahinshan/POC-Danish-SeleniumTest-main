using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
	public class KeyworkerNotesAttachmentRecordPage : CommonMethods
	{

        public KeyworkerNotesAttachmentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        //readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By cwDialogIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpkeyworkernotesattachment&')]");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Attachment (For Keyworker Notes): ']");
        readonly By BackButton = By.XPath("//*[@id='BackButton']");
		readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
		readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");

		readonly By Title = By.XPath("//*[@id='CWField_title']");
		readonly By Date = By.XPath("//*[@id='CWField_date']");
		readonly By Date_DatePicker = By.XPath("//*[@id='CWField_date_DatePicker']");
		readonly By Date_Time = By.XPath("//*[@id='CWField_date_Time']");
		readonly By Date_Time_TimePicker = By.XPath("//*[@id='CWField_date_Time_TimePicker']");
		readonly By CareRecordLink = By.XPath("//*[@id='CWField_cppersonkeyworkernoteid_Link']");
		readonly By CareRecordClearButton = By.XPath("//*[@id='CWClearLookup_cppersonkeyworkernoteid']");
		readonly By CareRecordLookupButton = By.XPath("//*[@id='CWLookupBtn_cppersonkeyworkernoteid']");
		readonly By DocumentTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_documenttypeid']");
		readonly By DocumentSubTypeLookupButton = By.XPath("//*[@id='CWLookupBtn_documentsubtypeid']");
		readonly By ResponsibleTeamLink = By.XPath("//*[@id='CWField_ownerid_Link']");
		readonly By ResponsibleTeamClearButton = By.XPath("//*[@id='CWClearLookup_ownerid']");
		readonly By ResponsibleTeamLookupButton = By.XPath("//*[@id='CWLookupBtn_ownerid']");
		readonly By Caption = By.XPath("//*[@id='CWField_caption']");

		readonly By FileFieldLabel = By.XPath("//*[@id='CWLabelHolder_fileid']/label");
        readonly By FileField = By.XPath("//input[@id='CWField_fileid']");
        readonly By FileFieldOuterDiv = By.XPath("//*[@id='CWField_fileid_FileSection']");
		readonly By File_FileLink = By.XPath("//*[@id = 'CWField_fileid_FileLink']");

        public KeyworkerNotesAttachmentRecordPage WaitForPageToLoad()
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


        public KeyworkerNotesAttachmentRecordPage ClickBackButton()
		{
			WaitForElementToBeClickable(BackButton);
			Click(BackButton);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickSaveButton()
		{
			WaitForElementToBeClickable(SaveButton);
			Click(SaveButton);

			WaitForElementNotVisible("CWRefreshPanel", 15);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickSaveAndCloseButton()
		{
			WaitForElementToBeClickable(SaveAndCloseButton);
			Click(SaveAndCloseButton);

			return this;
		}

		//verify title field is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateTitleFieldIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(Title);
                ScrollToElement(Title);
                WaitForElementVisible(Title);
            }
            else
			{
                WaitForElementNotVisible(Title, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ValidateTitleText(string ExpectedText)
		{
			WaitForElement(Title);
			ScrollToElement(Title);
			ValidateElementValue(Title, ExpectedText);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage InsertTextOnTitle(string TextToInsert)
		{
			WaitForElementToBeClickable(Title);
			SendKeys(Title, TextToInsert + Keys.Tab);
			
			return this;
		}

		//verify Date field is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateDateFieldIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(Date);
                ScrollToElement(Date);
                WaitForElementVisible(Date);
            }
            else
			{
                WaitForElementNotVisible(Date, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ValidateDateText(string ExpectedText)
		{
			WaitForElement(Date);
			ScrollToElement(Date);
			ValidateElementValue(Date, ExpectedText);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage InsertTextOnDate(string TextToInsert)
		{
			WaitForElementToBeClickable(Date);
			ScrollToElement(Date);
			SendKeys(Date, TextToInsert + Keys.Tab);
			
			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickDate_DatePicker()
		{
			WaitForElementToBeClickable(Date_DatePicker);
			Click(Date_DatePicker);

			return this;
		}

		//verify Date_Time field is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateDate_TimeFieldIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(Date_Time);
                ScrollToElement(Date_Time);
                WaitForElementVisible(Date_Time);
            }
            else
			{
                WaitForElementNotVisible(Date_Time, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ValidateDate_TimeText(string ExpectedText)
		{
			WaitForElement(Date_Time);
			ScrollToElement(Date_Time);
			ValidateElementValue(Date_Time, ExpectedText);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage InsertTextOnDate_Time(string TextToInsert)
		{
			WaitForElement(Date_Time);
			ScrollToElement(Date_Time);
			Click(Date_Time);
			SendKeys(Date_Time, TextToInsert + Keys.Tab);
			
			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickDate_Time_TimePicker()
		{
			WaitForElement(Date_Time_TimePicker);
			ScrollToElement(Date_Time_TimePicker);
			Click(Date_Time_TimePicker);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickCareRecordLink()
		{
			WaitForElement(CareRecordLink);
			ScrollToElement(CareRecordLink);
			Click(CareRecordLink);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ValidateCareRecordLinkText(string ExpectedText)
		{
			WaitForElement(CareRecordLink);
			ScrollToElement(CareRecordLink);
			ValidateElementText(CareRecordLink, ExpectedText);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickCareRecordLinkClearButton()
		{
			WaitForElement(CareRecordClearButton);
			ScrollToElement(CareRecordClearButton);
			Click(CareRecordClearButton);

			return this;
		}

		//verify Care Record Lookup button is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateCareRecordLookupButtonIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(CareRecordLookupButton);
                ScrollToElement(CareRecordLookupButton);
                WaitForElementVisible(CareRecordLookupButton);
            }
            else
			{
                WaitForElementNotVisible(CareRecordLookupButton, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ClickCareRecordLookupButton()
		{
			WaitForElement(CareRecordLookupButton);
			ScrollToElement(CareRecordLookupButton);
			Click(CareRecordLookupButton);

			return this;
		}

		//verify Document Type Lookup button is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateDocumentTypeLookupButtonIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(DocumentTypeLookupButton);
                ScrollToElement(DocumentTypeLookupButton);
                WaitForElementVisible(DocumentTypeLookupButton);
            }
            else
			{
                WaitForElementNotVisible(DocumentTypeLookupButton, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ClickDocumentTypeLookupButton()
		{
			WaitForElement(DocumentTypeLookupButton);
			ScrollToElement(DocumentTypeLookupButton);
			Click(DocumentTypeLookupButton);

			return this;
		}

		//verify Document Sub Type Lookup button is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateDocumentSubTypeLookupButtonIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(DocumentSubTypeLookupButton);
                ScrollToElement(DocumentSubTypeLookupButton);
                WaitForElementVisible(DocumentSubTypeLookupButton);
            }
            else
			{
                WaitForElementNotVisible(DocumentSubTypeLookupButton, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ClickDocumentSubTypeLookupButton()
		{
			WaitForElement(DocumentSubTypeLookupButton);
            ScrollToElement(DocumentSubTypeLookupButton);
			Click(DocumentSubTypeLookupButton);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickResponsibleTeamLink()
		{
			WaitForElement(ResponsibleTeamLink);
            ScrollToElement(ResponsibleTeamLink);
			Click(ResponsibleTeamLink);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ValidateResponsibleTeamLinkText(string ExpectedText)
		{
			WaitForElement(ResponsibleTeamLink);
            ScrollToElement(ResponsibleTeamLink);
			ValidateElementText(ResponsibleTeamLink, ExpectedText);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickResponsibleTeamClearButton()
		{
			WaitForElement(ResponsibleTeamClearButton);
            ScrollToElement(ResponsibleTeamClearButton);
			Click(ResponsibleTeamClearButton);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage ClickResponsibleTeamLookupButton()
		{
			WaitForElement(ResponsibleTeamLookupButton);
            ScrollToElement(ResponsibleTeamLookupButton);
			Click(ResponsibleTeamLookupButton);

			return this;
		}

		//verify Caption field is displayed or not displayed
		public KeyworkerNotesAttachmentRecordPage ValidateCaptionFieldIsDisplayed(bool IsDisplayed)
		{
            if (IsDisplayed)
			{
                WaitForElement(Caption);
                ScrollToElement(Caption);
                WaitForElementVisible(Caption);
            }
            else
			{
                WaitForElementNotVisible(Caption, 3);
            }

            return this;
        }

		public KeyworkerNotesAttachmentRecordPage ValidateCaptionText(string ExpectedText)
		{
			WaitForElement(Caption);
			ScrollToElement(Caption);
			ValidateElementValue(Caption, ExpectedText);

			return this;
		}

		public KeyworkerNotesAttachmentRecordPage InsertTextOnCaption(string TextToInsert)
		{
			WaitForElement(Caption);
			ScrollToElement(Caption);
			SendKeys(Caption, TextToInsert + Keys.Tab);
			
			return this;
		}

		//verify if File field label and file field are displayed
		public KeyworkerNotesAttachmentRecordPage ValidateFileFieldIsDisplayed()
		{
            WaitForElementVisible(FileFieldLabel);
			WaitForElement(FileFieldOuterDiv);
            ScrollToElement(FileFieldOuterDiv);
			WaitForElementVisible(FileFieldOuterDiv);

            return this;
        }

		//Upload file in file field
		public KeyworkerNotesAttachmentRecordPage UploadFile(string filePath)
		{
            WaitForElement(FileField);
            ScrollToElement(FileField);
			SendKeys(FileField, filePath);
		
            return this;
        }

		//validate file link
		public KeyworkerNotesAttachmentRecordPage ValidateFileLinkText(string ExpectedText)
		{
            WaitForElement(File_FileLink);
            ScrollToElement(File_FileLink);
            ValidateElementByTitle(File_FileLink, ExpectedText);

            return this;
        }

	}
}
