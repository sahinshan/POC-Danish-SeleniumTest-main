using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Interfaces
{
    interface IAttachments
    {
        Entities.Portal.AttachmentsInfoResponse GetAttachmentInfo(Guid websiteId, string type, Guid? id, string SecurityToken, string PortalUserToken);
        string Get(Guid websiteId, string type, Entities.Portal.FileType fileType, Guid id, string SecurityToken, string PortalUserToken);
        string CheckUploadLimit(Guid websiteId, string SecurityToken, string PortalUserToken);
        string Post(Guid websiteId, string type, Guid parentid, string parentfieldname, string FilePath, string SecurityToken, string PortalUserToken);
        string Put(Guid websiteId, string type, Guid id, string FilePath, string SecurityToken, string PortalUserToken);
        string Put(Guid websiteId, Entities.Portal.FileType fileType, string type, string fileField, Guid id, string FilePath, string SecurityToken, string PortalUserToken);

    }
}
