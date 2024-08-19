using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;

namespace CareCloudTestFramework.PageObjects
{
    public class GenericPicklistPopUp
    {
        readonly Func<AppQuery, AppQuery> _parentPanel = e => e.Id("parentPanel");

        readonly Func<AppQuery, AppQuery> _customPanel = e => e.Id("custom");

        readonly Func<AppQuery, AppQuery> _okButton = e => e.Id("button1").Text("OK");
        readonly Func<AppQuery, AppQuery> _cancelButton = e => e.Id("button2").Text("Cancel");

        readonly IApp _app;

        public GenericPicklistPopUp(IApp app)
        {
            _app = app;
        }

        public GenericPicklistPopUp WaitForPicklistToLoad()
        {
            _app.WaitForElement(_parentPanel);

            _app.WaitForElement(_customPanel);

            _app.WaitForElement(_okButton);
            _app.WaitForElement(_cancelButton);

            return this;
        }

        public GenericPicklistPopUp TapOkButton()
        {
            _app.Tap(_okButton);

            return this;
        }

        public GenericPicklistPopUp ScrollDownPickList(int NumberOfPositionsToScroll)
        {
            AppResult[] picklistFrameLayoutElement = _app.Query(_customPanel);

            if (!picklistFrameLayoutElement.Any())
                return this;

            //Get the middle position for the X axis
            float centerX = picklistFrameLayoutElement[0].Rect.CenterX;

            //Get the middle position for the Y axis
            float centerY = picklistFrameLayoutElement[0].Rect.CenterY;

            //Calculate one-Fourth the Height 
            //This will allow the test to click bellow the middle point of the Y axis (Middle Y + 1/4 Height)
            float oneFourthTheDistanceY = picklistFrameLayoutElement[0].Rect.Height / 4;

            //calculate the Y position to Tap
            float YposicToTap = centerY - oneFourthTheDistanceY;

            //Tap bellow the middle position of the picklist to cause the scroll down
            for (int i = 0; i < NumberOfPositionsToScroll; i++)
                _app.TapCoordinates(centerX, YposicToTap);

            return this;
        }

        public GenericPicklistPopUp ScrollUpPickList(int NumberOfPositionsToScroll)
        {
            AppResult[] picklistFrameLayoutElement = _app.Query(_customPanel);

            if (!picklistFrameLayoutElement.Any())
                return this;

            //Get the middle position for the X axis
            float centerX = picklistFrameLayoutElement[0].Rect.CenterX;

            //Get the middle position for the Y axis
            float centerY = picklistFrameLayoutElement[0].Rect.CenterY;

            //Calculate one-Fourth the Height 
            //This will allow the test to click bellow the middle point of the Y axis (Middle Y + 1/4 Height)
            float oneFourthTheDistanceY = picklistFrameLayoutElement[0].Rect.Height / 4;

            //calculate the Y position to Tap
            float YposicToTap = centerY + oneFourthTheDistanceY;

            //Tap bellow the middle position of the picklist to cause the scroll down
            for (int i = 0; i < NumberOfPositionsToScroll; i++)
                _app.TapCoordinates(centerX, YposicToTap);

            return this;
        }

    }
}
