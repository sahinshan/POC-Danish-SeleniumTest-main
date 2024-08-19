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

    public class RetrievePortalDataViewRequest
    {
        public Guid DataViewId { get; set; }
        public int PageNumber { get; set; }

        public string SearchBy { get; set; }
        public Guid? ParentRecordId { get; set; }
        public string FilterByFieldName { get; set; }
        public Guid? RelationshipId { get; set; }
        public Guid WebsiteId { get; set; }
    }

}
