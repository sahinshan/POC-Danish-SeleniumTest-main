
using Phoenix.DBHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Portal.UITests.Websites.StaffordshireCitizenPortal
{
    public class CommonMethodsDB
    {
        private DBHelper.DatabaseHelper dbHelper;


        public CommonMethodsDB(DBHelper.DatabaseHelper DBHelper)
        {
            dbHelper = DBHelper;
        }

        

        public string UpdateSystemUserLastPasswordChange(string username, string dataEncoded)
        {
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);
            return username;
        }


    }
}