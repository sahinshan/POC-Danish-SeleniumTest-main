using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class AttachmentsInfoResponse
    {
        public string AllowedExtensions { get; set; }
        public int? MaxFileSize { get; set; }
        public bool? CanUpload { get; set; }
        public int? Message { get; set; }


        public bool? CanDownload { get; set; }
        public Guid? FileId { get; set; }
        public string Name { get; set; }
    }

}
