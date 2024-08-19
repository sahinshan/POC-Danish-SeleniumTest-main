using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using Phoenix.UITests.Framework.WebAppAPI.Classes;

namespace Phoenix.UITests.Framework.WebAppAPI.Proxies
{
    public class DataProxy : IData
    {
        public DataProxy()
        {
            _Data = new Data();
        }

        private IData _Data;


        public T GetBusinessObjectData<T>(string BusinessObjectName, Guid RecordID, string SecurityToken)
        {
            return _Data.GetBusinessObjectData<T>(BusinessObjectName, RecordID, SecurityToken);
        }

        public T GetBusinessObjectData<T>(string BusinessObjectName, string SecurityToken)
        {
            return _Data.GetBusinessObjectData<T>(BusinessObjectName, SecurityToken);
        }
    }
}
