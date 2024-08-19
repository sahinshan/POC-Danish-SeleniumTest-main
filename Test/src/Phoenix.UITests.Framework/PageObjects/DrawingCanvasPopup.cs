using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class DrawingCanvasPopup : CommonMethods
    {
        public DrawingCanvasPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWEditDrawingWindow = By.Id("iframe_CWEditDrawingWindow");

        readonly By popupHeader = By.XPath("//h1[@id='PageTitle']");


        readonly By backButton = By.Id("CWClose");
        readonly By saveButton = By.Id("CWSave");
        readonly By saveAndCloseButton = By.Id("CWSaveAndClose");
        readonly By clearButton = By.Id("CWClear");
        readonly By undoButton = By.Id("CWUndo");
        
        readonly By pencilButton = By.Id("pencil");
        readonly By eraserButton = By.Id("eraser");
        readonly By straightLineButton = By.Id("straightLine");
        readonly By rectangleButton = By.Id("rect");
        readonly By circleButton = By.Id("circle");

        readonly By lineWidthLabel = By.XPath("//label[@for='lineWidth']");
        readonly By lineWidth = By.XPath("//*[@id='lineWidth']");

        readonly By lineColourLabel = By.XPath("//label[@id='lineColorLabel']");
        readonly By lineColour = By.XPath("//*[@id='lineColor']");

        readonly By templateChoiceLabel = By.XPath("//label[@id='templateChoiceLabel']");
        readonly By templateChoice = By.XPath("//*[@id='templateChoice']");
        
        readonly By drawingCanvas = By.XPath("//*[@id='drawingContainer']/canvas");



        public DrawingCanvasPopup WaitForDrawingCanvasPopupToLoad()
        {
            WaitForElement(iframe_CWEditDrawingWindow);
            SwitchToIframe(iframe_CWEditDrawingWindow);

            WaitForElement(popupHeader);

            WaitForElement(backButton);
            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            WaitForElement(clearButton);
            WaitForElement(undoButton);

            WaitForElement(pencilButton);
            WaitForElement(eraserButton);
            WaitForElement(straightLineButton);
            WaitForElement(rectangleButton);
            WaitForElement(circleButton);

            WaitForElement(lineWidthLabel);
            WaitForElement(lineWidth);

            WaitForElement(lineColourLabel);
            WaitForElement(lineColour);

            WaitForElement(templateChoiceLabel);
            WaitForElement(templateChoice);

            WaitForElement(drawingCanvas);

            return this;
        }

        public DrawingCanvasPopup ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public DrawingCanvasPopup ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }
        public DrawingCanvasPopup ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public DrawingCanvasPopup ClickClearButton()
        {
            WaitForElementToBeClickable(clearButton);
            Click(clearButton);

            return this;
        }
        public DrawingCanvasPopup ClickUndoButton()
        {
            WaitForElementToBeClickable(undoButton);
            Click(undoButton);

            return this;
        }


        public DrawingCanvasPopup ClickPencilButton()
        {
            WaitForElementToBeClickable(pencilButton);
            Click(pencilButton);

            return this;
        }
        public DrawingCanvasPopup ClickEraserButton()
        {
            WaitForElementToBeClickable(eraserButton);
            Click(eraserButton);

            return this;
        }
        public DrawingCanvasPopup ClickLineButton()
        {
            WaitForElementToBeClickable(straightLineButton);
            Click(straightLineButton);

            return this;
        }
        public DrawingCanvasPopup ClickCircleButton()
        {
            WaitForElementToBeClickable(circleButton);
            Click(circleButton);

            return this;
        }
        public DrawingCanvasPopup ClickRectangleButton()
        {
            WaitForElementToBeClickable(rectangleButton);
            Click(rectangleButton);

            return this;
        }


        public DrawingCanvasPopup IncreaseLineWidth(int IncreaseAmount)
        {
            WaitForElementToBeClickable(lineWidth);

            for (int i = 0; i < IncreaseAmount; i++)
                SendKeysWithoutClearing(lineWidth, Keys.ArrowRight);

            return this;
        }

        public DrawingCanvasPopup DecreaseLineWidth(int DecreaseAmount)
        {
            WaitForElementToBeClickable(lineWidth);

            for (int i = 0; i < DecreaseAmount; i++)
                SendKeys(lineWidth, Keys.ArrowLeft);

            return this;
        }

        public DrawingCanvasPopup ChangeLineColor(string LineColorCode)
        {
            WaitForElementToBeClickable(lineColour);

            ChangeColorInputLineColorProperty("lineColor", LineColorCode);

            return this;
        }

        public DrawingCanvasPopup SelectTemplate(string TextToSelect)
        {
            WaitForElementToBeClickable(lineWidth);
            
            SelectPicklistElementByText(templateChoice, TextToSelect);

            return this;
        }


        public DrawingCanvasPopup ClicAndDragonOnCanvas(int offsetX, int offsetY)
        {
            WaitForElementToBeClickable(drawingCanvas);
            ClickAndDrag(drawingCanvas, offsetX, offsetY);

            return this;
        }

        public DrawingCanvasPopup ClicAndDragonAndReleaseOnCanvas(int offsetX, int offsetY)
        {
            WaitForElementToBeClickable(drawingCanvas);
            ClickAndDragAndRelese(drawingCanvas, offsetX, offsetY);

            return this;
        }

    }
}
