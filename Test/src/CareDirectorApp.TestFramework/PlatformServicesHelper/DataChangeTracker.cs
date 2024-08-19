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
    public class DataChangeTracker : BaseClass
    {

        public DataChangeTracker(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreateDataChangeTracker(Guid RecordId, string RecordIdTableName, int ChangeType, DateTime CreatedOn, Guid UserId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("DataChangeTracker", "DataChangeTrackerId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DataChangeTrackerId",  Guid.NewGuid());
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecordId",  RecordId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecordIdTableName",  RecordIdTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ChangeType", ChangeType);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CreatedOn", CreatedOn);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "UserId",  UserId);
            
            return this.CreateRecord(buisinessDataObject);
        }
    }
}
