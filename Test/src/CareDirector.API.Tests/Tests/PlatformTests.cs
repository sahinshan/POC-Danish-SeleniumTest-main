using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CareDirector.API.Tests
{
    [TestFixture]
    public class PlatformTests: UnitTestBaseClass
    {
        #region https://advancedcsg.atlassian.net/browse/CDV6-9120

        [Test]
        [Description("API Route for 'core/data/person' (Get Single Record) is linked to 'Consumer Portal' Application - " +
            "User Consumer Portal Application Key and Secret to get an authentication token - Use the token to access core/data/person - " +
            "Set 'person' as the business object and supply a valid person id - Validate that the person information is returned by the API")]
        public void CHIEAPISecure_TestMethod_0001()
        {
            var boName = "person";
            var recordID = new Guid("F03D83F1-FF96-EB11-A323-005056926FE4");

            var personInfo = this.WebAPIHelper.DataProxy.GetBusinessObjectData<Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector.Person>(boName, recordID, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.IsTrue(personInfo != null);
            Assert.AreEqual(personInfo.fullname, "Datam20210406184531 Master");
        }

        [Test]
        [Description("NO API Route for 'core/data/case' (Get Single Record) is linked to 'Consumer Portal' Application - " +
            "User Consumer Portal Application Key and Secret to get an authentication token - Use the token to access core/data/case - " +
            "Set 'case' as the business object and supply a valid case id - Validate that the API will return an exception")]
        public void CHIEAPISecure_TestMethod_0002()
        {
            var boName = "case";
            var recordID = new Guid("9DF6305A-2E57-EB11-A2FD-005056926FE4");
            try
            {
                this.WebAPIHelper.DataProxy.GetBusinessObjectData<string>(boName, recordID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The requested route was not found or you do not have access for this route. core/data/case\",\"errorCode\":\"RouteNotFound\"}", ex.Message);
            }
        }

        [Test]
        [Description("API Route for 'core/data/case' (Get Multiple Records) is linked to 'Consumer Portal' Application - " +
            "User Consumer Portal Application Key and Secret to get an authentication token - Use the token to access core/data/case - " +
            "Set 'case' as the business object - Validate that the Case records information is returned by the API")]
        public void CHIEAPISecure_TestMethod_0003()
        {
            var boName = "case";

            var cases = this.WebAPIHelper.DataProxy.GetBusinessObjectData<Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector.CaseList>(boName, this.WebAPIHelper.PortalSecurityProxy.Token);

            Assert.IsTrue(cases != null);
            Assert.IsTrue(cases.data.Count > 0);
        }

        [Test]
        [Description("NO API Route for 'core/data/person' (Get Multiple Records) is linked to 'Consumer Portal' Application - " +
            "User Consumer Portal Application Key and Secret to get an authentication token - Use the token to access core/data/person - " +
            "Set 'person' as the business object - Validate that the API will return an exception")]
        public void CHIEAPISecure_TestMethod_0004()
        {
            var boName = "person";

            try
            {
                this.WebAPIHelper.DataProxy.GetBusinessObjectData<string>(boName, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The requested route was not found or you do not have access for this route. core/data/person\",\"errorCode\":\"RouteNotFound\"}", ex.Message);
            }
        }

        [Test]
        [Description("API Route for 'core/data/professional' (Get Single Record) is linked to 'Consumer Portal' Application - The API Route is inactive - " +
            "User Consumer Portal Application Key and Secret to get an authentication token - Use the token to access core/data/professional - " +
            "Set 'professional' as the business object and supply a valid professional id - Validate that the API will return an exception")]
        public void CHIEAPISecure_TestMethod_0005()
        {
            var boName = "professional";
            var recordID = new Guid("2C97BCF8-343F-E911-A2C5-005056926FE4");

            try
            {
                this.WebAPIHelper.DataProxy.GetBusinessObjectData<string>(boName, recordID, this.WebAPIHelper.PortalSecurityProxy.Token);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("{\"errorMessage\":\"The requested route was not found or you do not have access for this route. core/data/professional\",\"errorCode\":\"RouteNotFound\"}", ex.Message);
            }
        }

        #endregion
    }
}
