﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IWebsiteContact
    {
        Guid CreateContact(Entities.Portal.WebsiteContact WebsiteContact, Guid WebsiteID, string SecurityToken);
    }
}
