using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class AttachmentsProxy
    {
        public AttachmentsProxy()
        {
            _Attachment = new Attachments();
        }


        private IAttachments _Attachment;


        public Entities.Portal.AttachmentsInfoResponse GetAttachmentInfo(Guid websiteId, string type, Guid? id, string SecurityToken, string PortalUserToken)
        {
            return _Attachment.GetAttachmentInfo(websiteId, type, id, SecurityToken, PortalUserToken);
        }

        public string Get(Guid websiteId, string type, Entities.Portal.FileType fileType, Guid id, string SecurityToken, string PortalUserToken)
        {
            return _Attachment.Get(websiteId, type, fileType, id, SecurityToken, PortalUserToken);
        }

        public string CheckUploadLimit(Guid websiteId, string SecurityToken, string PortalUserToken)
        {
            return _Attachment.CheckUploadLimit(websiteId, SecurityToken, PortalUserToken);
        }

        public string Post(Guid websiteId, string type, Guid parentid, string parentfieldname, string FilePath, string SecurityToken, string PortalUserToken)
        {
            return _Attachment.Post(websiteId, type, parentid, parentfieldname, FilePath, SecurityToken, PortalUserToken);
        }

        public string Put(Guid websiteId, string type, Guid id, string FilePath, string SecurityToken, string PortalUserToken)
        {
            return _Attachment.Put(websiteId, type, id, FilePath, SecurityToken, PortalUserToken);
        }

        public string Put(Guid websiteId, Entities.Portal.FileType fileType, string type, string fileField, Guid id, string FilePath, string SecurityToken, string PortalUserToken)
        {
            return _Attachment.Put(websiteId, fileType, type, fileField, id, FilePath, SecurityToken, PortalUserToken);
        }
    }
}
