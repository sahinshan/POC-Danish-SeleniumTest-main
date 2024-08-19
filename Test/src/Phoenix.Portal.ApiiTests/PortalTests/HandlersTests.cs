using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.Portal.ApiTests.PortalTests
{
    [TestClass]
    public class HandlersTests: UnitTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-8298

        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-9881")]
        [Description("Validate that it is possible to execute a Plugin Type using the new handler API")]
        public void Handlers_TestMethod001()
        {
            var websiteid = new Guid("7cc9f859-9834-eb11-a2d6-005056926fe4"); //Automation - Web Site 16

            var userLoginInfo = new UITests.Framework.WebAppAPI.Entities.Portal.WebsiteUserLogin
            {
                ApplicationKey = "BC80C1F0B3544A118BF2A3DC14B86A6EDE0C8B56BCB2455C8946DE667A729CC2",
                ApplicationSecret = "Passw0rd_!",
                EnvironmentId = new Guid("0b6b1ab7-6661-460f-9199-5f47d90f2abd"),
                Username = "website16user1@mail.com",
                Password = "Passw0rd_!"
            };

            var userLoginReturnedData = WebAPIHelper.WebsiteUserLoginProxy.LoginUser(websiteid, userLoginInfo, this.WebAPIHelper.PortalSecurityProxy.Token);


            var HandlerRequest = new UITests.Framework.WebAppAPI.Entities.Portal.ExecuteWebsiteHandlerRequest();
            HandlerRequest.Transactional = true;
            HandlerRequest.Handler = "CareDirector.HealthAndCare.Handlers.PhysicalObservationAndMonitoring.GetPersonDOB";
            HandlerRequest.JsonData = "{\"PersonId\":\"e32939f0-8166-41a0-8113-a6b43441d83f\"}";

            var handlerResponse = this.WebAPIHelper.HandlersProxy.ExecuteCode(HandlerRequest, websiteid, this.WebAPIHelper.PortalSecurityProxy.Token, userLoginReturnedData.AccessToken);

            Assert.AreEqual("{\"PreviousWeightKilos\":null,\"AgeAtTimeTaken\":null,\"DateOfBirth\":\"1965-08-25T00:00:00\"}", handlerResponse.jsonResponse);

        }

        #endregion

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }
    }
}
