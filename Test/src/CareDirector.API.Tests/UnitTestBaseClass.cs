using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Phoenix.DBHelper;
using Phoenix.UITests.Framework.WebAppAPI;

namespace CareDirector.API.Tests
{
    public abstract class UnitTestBaseClass
    {
        public WebAPIHelper WebAPIHelper;
        public DatabaseHelper dbHelper;


        

        [SetUp()]
        public void SetupTest()
        {
            WebAPIHelper = new WebAPIHelper();
            dbHelper = new DatabaseHelper();

            //Authenticate against the portal API and extract the Security Token
            this.WebAPIHelper.PortalSecurityProxy.GetToken();
        }

        [TearDown()]
        public virtual void MyTestCleanup()
        {
            



        }


    }
}
