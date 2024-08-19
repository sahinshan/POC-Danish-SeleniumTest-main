using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;
using System.Collections.Generic;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class PickList: CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _frameLayout = e => e.Marked("custom");
        readonly Func<AppQuery, AppQuery> _cancelButton = e => e.Marked("button2");
        readonly Func<AppQuery, AppQuery> _okButton = e => e.Marked("button1");

        public PickList(IApp app)
        {
            _app = app;
        }


        public PickList WaitForPickListToLoad()
        {
            WaitForElement(_frameLayout);
            WaitForElement(_cancelButton);
            WaitForElement(_okButton);

            return this;
        }




        /// <summary>
        /// Method that will scroll down a pick list to select one of the available options
        /// </summary>
        /// <param name="app">IApp instance to communicate with the app</param>
        /// <param name="FrameLayoutID">The ID of the Picklist Frame layout</param>
        /// <param name="NumberOfPositionsToScroll">Number of positions that will be scroll down</param>
        public PickList ScrollDownPicklist(int NumberOfPositionsToScroll)
        {
            this.WaitForElement(_frameLayout);

            AppResult[] picklistFrameLayoutElement = _app.Query(_frameLayout);

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

        /// <summary>
        /// Method that will scroll up a pick list to select one of the available options
        /// </summary>
        /// <param name="app">IApp instance to communicate with the app</param>
        /// <param name="FrameLayoutID">The ID of the Picklist Frame layout</param>
        /// <param name="NumberOfPositionsToScroll">Number of positions that will be scroll down</param>
        public PickList ScrollUpPicklist(int NumberOfPositionsToScroll)
        {
            this.WaitForElement(_frameLayout);

            AppResult[] picklistFrameLayoutElement = _app.Query(_frameLayout);

            if (!picklistFrameLayoutElement.Any())
                return this;

            //Get the middle position for the X axis
            float centerX = picklistFrameLayoutElement[0].Rect.CenterX;

            //Get the middle position for the Y axis
            float centerY = picklistFrameLayoutElement[0].Rect.CenterY;

            //Calculate one-Fourth the Height 
            //This will allow the test to click abouve the middle point of the Y axis (Middle Y + 1/4 Height)
            float oneFourthTheDistanceY = picklistFrameLayoutElement[0].Rect.Height / 4;

            //calculate the Y position to Tap
            float YposicToTap = centerY + oneFourthTheDistanceY;

            //Tap bellow the middle position of the picklist to cause the scroll down
            for (int i = 0; i < NumberOfPositionsToScroll; i++)
                _app.TapCoordinates(centerX, YposicToTap);

            return this;
        }

        public PickList TapOKButton()
        {
            Tap(_okButton);

            return this;
        }

        public PickList TapCancelButton()
        {
            Tap(_cancelButton);

            return this;
        }

        public PickList ClosePicklistIfOpen()
        {
            TryWaitForElement(_cancelButton);

            bool buttonVisible = _app.Query(_cancelButton).Any();

            if (buttonVisible)
                _app.Tap(_cancelButton);

            return this;
        }
    }
}
