using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector
{
    public class AuditSearch
    {
        public string SearchText { get; set; }
        public string CurrentPage { get; set; }
        public string SortOptions { get; set; }
        public string CurrentViewId { get; set; }
        public string TypeName { get; set; }
        public string ParentId { get; set; }
        public string ParentTypeName { get; set; }
        public string SearchViewId { get; set; }
        public string SearchViewDisplayName { get; set; }
        public string RelationshipId { get; set; }
        public string BusinessObjectId { get; set; }
        public string RecordsPerPage { get; set; }
        public string ViewType { get; set; }
        public string ParentIdFieldName { get; set; }
        public string AllowMultiSelect { get; set; }
        public string OtherIdField { get; set; }
        public string ViewGroup { get; set; }
        public string DataQueryXml { get; set; }
        public string DateFrom { get; set; }
        public string Year { get; set; }
        public string RecordType { get; set; }
        public string UserId { get; set; }
        public int Operation { get; set; }
        public bool IsGeneralAuditSearch { get; set; }
        public bool UsePaging { get; set; }
        public int PageNumber { get; set; }
    }
}
