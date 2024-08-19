using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IData
    {
        T GetBusinessObjectData<T>(string BusinessObjectName, Guid RecordID, string SecurityToken);
        T GetBusinessObjectData<T>(string BusinessObjectName, string SecurityToken);
    }
}
