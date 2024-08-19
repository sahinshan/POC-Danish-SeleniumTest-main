using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{

    public enum FileType
    {
        Unknown = 0,
        Document = 1,
        Image = 2,
        Audio = 3,
        Video = 4,
    }

}
