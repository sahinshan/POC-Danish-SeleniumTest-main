using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WorkflowTestFramework
{

    public class NullSafeDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey key]
        {
            get
            {
                TValue value;
                return TryGetValue(key, out value) ? value : default(TValue);
            }
            set
            {
                base[key] = value;
            }
        }
    }

}

