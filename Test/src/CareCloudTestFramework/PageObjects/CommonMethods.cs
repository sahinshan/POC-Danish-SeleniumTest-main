using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;


namespace CareCloudTestFramework.PageObjects
{
    public abstract class CommonMethods
    {
        public IApp _app;


        internal string GetElementClass(Func<AppQuery, AppQuery> query)
        {
            var queryResult = _app.Query(query);
            if (queryResult != null && queryResult.Count() > 0)
            {
                var elementsWithSize = queryResult.Where(c => c.Rect.Width > 0 && c.Rect.Height > 0).ToList();

                if (elementsWithSize != null && elementsWithSize.Count() > 0)
                {
                    return elementsWithSize.FirstOrDefault().Class;
                }

                return queryResult.FirstOrDefault().Class;
            }

            return null;
        }

        internal bool CheckIfElementVisible(Func<AppQuery, AppQuery> query)
        {
            try
            {
                var queryResult = _app.Query(query);

                if (queryResult != null)
                    return queryResult.Where(c => c.Rect.Width > 0 && c.Rect.Height > 0).Any();
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        internal bool CheckIfElementVisible(Func<AppQuery, AppWebQuery> query)
        {
            try
            {
                var queryResult = _app.Query(query);

                if (queryResult != null)
                    return queryResult.Where(c => c.Rect.Width > 0 && c.Rect.Height > 0).Any();
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        internal void ValidateElementVisibility(Func<AppQuery, AppQuery> query, bool ExpectElementVisible)
        {
            bool elementVisibility = false;

            var queryResult = _app.Query(query);

            if (queryResult != null)
                elementVisibility = queryResult.Any();

            if (ExpectElementVisible != elementVisibility)
                throw new Exception("Expected : " + ExpectElementVisible.ToString() + " | Actual: " + elementVisibility.ToString());

        }

        internal void ValidateElementVisibility(Func<AppQuery, AppWebQuery> query, bool ExpectElementVisible)
        {
            bool elementVisibility = false;

            var queryResult = _app.Query(query);

            if (queryResult != null)
                elementVisibility = queryResult.Any();

            if (ExpectElementVisible != elementVisibility)
                throw new Exception("Expected : " + ExpectElementVisible.ToString() + " | Actual: " + elementVisibility.ToString());

        }


        internal void Tap(Func<AppQuery, AppQuery> query)
        {
            _app.Tap(query);
        }

        internal void Tap(Func<AppQuery, AppWebQuery> query)
        {
            _app.Tap(query);
        }

        /// <summary>
        /// in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
        /// This may cause a problem when the framework tries to tap, because sometimes an incorrect element is selected
        /// the only solution found is to retrieve all elements that match the query extract one that has height and width.
        /// </summary>
        /// <param name="query"></param>
        internal void TapOnElementWithWidthAndHeight(Func<AppQuery, AppQuery> query)
        {
            try
            {
                AppResult[] results = this._app.Query(query);
                AppResult elementWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault();
                if (elementWithSize != null && elementWithSize.Id != null)
                {
                    (this._app as Xamarin.UITest.Android.AndroidApp).Tap(elementWithSize.Id);
                    return;
                }
            }
            catch { }

            /*if the previous query fails then, as a last resource, try to perform a regular tap*/
            _app.Tap(query);
        }

        /// <summary>
        /// Try to tap on the target element. this method will not throw any exception if the target element is not found
        /// </summary>
        /// <param name="query"></param>
        internal void TryTap(Func<AppQuery, AppQuery> query)
        {
            try
            {
                _app.Tap(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal void WaitForElement(Func<AppQuery, AppQuery> query)
        {
            _app.WaitForElement(query);

        }

        internal void WaitForElement(Func<AppQuery, AppWebQuery> query)
        {
            _app.WaitForElement(query);

        }

        internal void TryWaitForElement(Func<AppQuery, AppQuery> query)
        {
            try
            {
                _app.WaitForElement(query, "Timed out waiting for element", new TimeSpan(0, 0, 3));
            }
            catch { }

        }

        internal void WaitForElementNotVisible(Func<AppQuery, AppQuery> query)
        {
            _app.WaitForNoElement(query);
        }

        internal void WaitForElementNotVisible(Func<AppQuery, AppQuery> query, TimeSpan Timeout)
        {
            _app.WaitForNoElement(query, "Element still visible", Timeout);
        }

        internal string GetElementText(Func<AppQuery, AppQuery> query)
        {
            var results = _app.Query(query);

            if (results != null && results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).Any())
                return results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault().Text;

            return results != null && results.Count() > 0 ? results.FirstOrDefault().Text : "";
        }

        internal string GetWebElementText(Func<AppQuery, AppWebQuery> query)
        {
            var results = _app.Query(query);

            if (results != null && results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).Any())
                return results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault().TextContent;

            return results.FirstOrDefault().TextContent;
        }

        internal void ValidateElementText(Func<AppQuery, AppQuery> query, string ExpetedText)
        {
            var results = _app.Query(query);
            string elementText = "";

            if (results != null && results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).Any())
                elementText = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault().Text;
            else
                elementText = results != null && results.Count() > 0 ? results.FirstOrDefault().Text : "";

            if (elementText != ExpetedText)
                throw new Exception("Expected text: '" + ExpetedText + "' | Actual text: '" + elementText + "'");
        }

       

        internal void ValidateElementText(Func<AppQuery, AppWebQuery> query, string ExpetedText)
        {
            var results = _app.Query(query);
            string elementText = "";

            if (results != null && results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).Any())
                elementText = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault().TextContent;
            else
                elementText = results != null && results.Count() > 0 ? results.FirstOrDefault().TextContent : "";

            if (elementText != ExpetedText)
                throw new Exception("Expected text: '" + ExpetedText + "' | Actual text: '" + elementText + "'");
        }

        internal void ScrollToVerticalEnd()
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                (this._app as Xamarin.UITest.Android.AndroidApp).ScrollToVerticalEnd();
            }
            else
            {

            }
        }

        internal void ScrollUpWithinElement(Func<AppQuery, AppWebQuery> ElementToScrollUp)
        {
            var element = _app.Query(ElementToScrollUp).FirstOrDefault();

            _app.DragCoordinates(element.Rect.CenterX, element.Rect.CenterY, element.Rect.CenterX, element.Rect.CenterY / 2);
        }

        internal void ScrollDownWithinElement(Func<AppQuery, AppWebQuery> ElementToScrollUp, Func<AppQuery, AppQuery> WebViewElement)
        {
            var element = _app.Query(ElementToScrollUp).FirstOrDefault();
            
            if (element != null)
                return;

            var webView = _app.Query(WebViewElement).FirstOrDefault();

            var i = 0;
            while (i < 15)
            {
                _app.DragCoordinates(webView.Rect.CenterX, webView.Rect.CenterY, webView.Rect.CenterX, webView.Rect.CenterY / 2);
                element = _app.Query(ElementToScrollUp).FirstOrDefault();
                if (element != null)
                    return;
                i++;
            }
        }

        /*internal void ScrollDownWithinElement(Func<AppQuery, AppWebQuery>  ElementToScrollUp)
        {
            var element = _app.Query(ElementToScrollUp).FirstOrDefault();

            _app.DragCoordinates(element.Rect.CenterX, element.Rect.CenterY, element.Rect.CenterX, element.Rect.CenterY / 2);
        }*/

        internal void ScrollToElement(Func<AppQuery, AppQuery> query)
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                /*in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
                 This may cause a problem when the framework tries to scroll, because sometimes an incorrect element is selected
                 the only solution found is to retrieve all elements that match the query extract one that has height and width.
                 */
                try
                {
                    AppResult[] results = this._app.Query(query);
                    List<AppResult> elementsWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).ToList();
                    foreach (var elementWithSize in elementsWithSize)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                    }
                }
                catch { }


                /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
            }
            else
            {
                (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
            }
        }

        internal void ScrollToElement(Func<AppQuery, AppWebQuery>query)
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                /*in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
                 This may cause a problem when the framework tries to scroll, because sometimes an incorrect element is selected
                 the only solution found is to retrieve all elements that match the query extract one that has height and width.
                 */
                try
                {
                    AppWebResult[] results = this._app.Query(query);
                    List<AppWebResult> elementsWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).ToList();
                    foreach (var elementWithSize in elementsWithSize)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                    }
                }
                catch { }


                /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
            }
            else
            {
                (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
            }
        }

        internal void ScrollToElementWithAdditionalScrollDown(Func<AppQuery, AppQuery> query)
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                /*in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
                 This may cause a problem when the framework tries to scroll, because sometimes an incorrect element is selected
                 the only solution found is to retrieve all elements that match the query extract one that has height and width.
                 */
                try
                {
                    AppResult[] results = this._app.Query(query);
                    List<AppResult> elementsWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).ToList();
                    foreach (var elementWithSize in elementsWithSize)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                        ScrollDown();
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                    }

                }
                catch
                {
                    /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                    (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
                }

            }
            else
            {
                (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
            }
        }

        internal void ScrollToElementWithAdditionalScrollDown(Func<AppQuery, AppWebQuery> query)
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                /*in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
                 This may cause a problem when the framework tries to scroll, because sometimes an incorrect element is selected
                 the only solution found is to retrieve all elements that match the query extract one that has height and width.
                 */
                try
                {
                    AppWebResult[] results = this._app.Query(query);
                    List<AppWebResult> elementsWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).ToList();
                    foreach (var elementWithSize in elementsWithSize)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                        ScrollDown();
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                    }

                }
                catch
                {
                    /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                    (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
                }

            }
            else
            {
                (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
            }
        }

        internal void ScrollDown()
        {
            this._app.ScrollDown();
        }

        /// <summary>
        /// in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
        /// This may cause a problem when the framework tries to scroll, because sometimes an incorrect element is selected
        /// the only solution found is to retrieve all elements that match the query extract one that has height and width.
        /// </summary>
        /// <param name="query"></param>
        internal void ScrollToElementWithWidthAndHeight(Func<AppQuery, AppQuery> query)
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                try
                {
                    AppResult[] results = this._app.Query(query);
                    AppResult elementWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault();
                    if (elementWithSize != null && elementWithSize.Id != null)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                        return;
                    }
                }
                catch { }

                /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
            }
            else
            {
                try
                {
                    AppResult[] results = this._app.Query(query);
                    AppResult elementWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault();
                    if (elementWithSize != null && elementWithSize.Id != null)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                        return;
                    }
                }
                catch { }

                /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
            }
        }

        /// <summary>
        /// in some cases we may have 2 (or more) elements matching the query (when we navigate from a parent record to a child record the parent records remain accessible to the framework). 
        /// This may cause a problem when the framework tries to scroll, because sometimes an incorrect element is selected
        /// the only solution found is to retrieve all elements that match the query extract one that has height and width.
        /// </summary>
        /// <param name="query"></param>
        internal void ScrollToElementWithWidthAndHeight(Func<AppQuery, AppWebQuery> query)
        {
            if (this._app is Xamarin.UITest.Android.AndroidApp)
            {
                try
                {
                    AppWebResult[] results = this._app.Query(query);
                    AppWebResult elementWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault();
                    if (elementWithSize != null && elementWithSize.Id != null)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                        return;
                    }
                }
                catch { }

                /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
            }
            else
            {
                try
                {
                    AppWebResult[] results = this._app.Query(query);
                    AppWebResult elementWithSize = results.Where(c => c.Rect.Height > 0 && c.Rect.Width > 0).FirstOrDefault();
                    if (elementWithSize != null && elementWithSize.Id != null)
                    {
                        (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(elementWithSize.Id);
                        return;
                    }
                }
                catch { }

                /*if the previous query fails then, as a last resource, try to perform a regular scroll*/
                (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
            }
        }

        /// <summary>
        /// try to scroll to a matching element. No exception is thrown if no element is found
        /// </summary>
        /// <param name="query"></param>
        internal void TryScrollToElement(Func<AppQuery, AppQuery> query)
        {
            try
            {

                if (this._app is Xamarin.UITest.Android.AndroidApp)
                {
                    (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
                }
                else
                {
                    (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
                }

            }
            catch
            {
            }
        }

        internal void TryScrollToElement(Func<AppQuery, AppWebQuery> query)
        {
            try
            {

                if (this._app is Xamarin.UITest.Android.AndroidApp)
                {
                    (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
                }
                else
                {
                    (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
                }

            }
            catch
            {
            }
        }

        /// <summary>
        /// try to scroll to a matching element. No exception is thrown if no element is found
        /// </summary>
        /// <param name="query"></param>
        internal void TryScrollToElement(string query)
        {
            try
            {

                if (this._app is Xamarin.UITest.Android.AndroidApp)
                {
                    (this._app as Xamarin.UITest.Android.AndroidApp).ScrollTo(query);
                }
                else
                {
                    (this._app as Xamarin.UITest.iOS.iOSApp).ScrollDownTo(query);
                }

            }
            catch
            {
            }
        }


        internal void EnterText(Func<AppQuery, AppQuery> Query, string TextToInsert)
        {
            this._app.DismissKeyboard();

            System.Threading.Thread.Sleep(400);
            this._app.ClearText(Query);

            System.Threading.Thread.Sleep(400);
            this._app.DismissKeyboard();

            System.Threading.Thread.Sleep(400);
            this._app.EnterText(Query, TextToInsert);

            System.Threading.Thread.Sleep(400);
            this._app.DismissKeyboard();

        }

    }
}
