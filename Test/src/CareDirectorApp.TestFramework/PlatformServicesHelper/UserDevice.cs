using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class UserDevice : BaseClass
    {

        public UserDevice(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public List<Guid> GetUserDeviceByUserID(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject("UserDevice", false, "UserDeviceId");
            
            this.AddReturnField(query, "UserDevice", "UserDeviceId");

            this.AddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, "UserDeviceId");
        }

        public void UpdateFullSyncRequired(Guid UserDeviceId, bool FullSyncRequired)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("UserDevice", "UserDeviceId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "UserDeviceId", UserDeviceId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "FullSyncRequired", FullSyncRequired);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFullSyncRequired(List<Guid> UserDeviceIds, bool FullSyncRequired)
        {
            foreach (var UserDeviceId in UserDeviceIds)
            {
                var buisinessDataObject = GetBusinessDataBaseObject("UserDevice", "UserDeviceId");

                this.AddFieldToBusinessDataObject(buisinessDataObject, "UserDeviceId", UserDeviceId);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "FullSyncRequired", FullSyncRequired);

                this.UpdateRecord(buisinessDataObject);
            }
            
        }
    }
}
