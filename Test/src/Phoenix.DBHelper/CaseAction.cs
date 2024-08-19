//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Phoenix.DBHelper
{
    using System;
    using System.Collections.Generic;
    
    public partial class CaseAction
    {
        public System.Guid CaseActionId { get; set; }
        public byte[] VersionStamp { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public System.Guid OwnerId { get; set; }
        public Nullable<System.Guid> ResponsibleUserId { get; set; }
        public System.Guid OwningBusinessUnitId { get; set; }
        public string Title { get; set; }
        public Nullable<bool> Inactive { get; set; }
        public Nullable<int> ImportCreateSequence { get; set; }
        public Nullable<int> ImportUpdateSequence { get; set; }
        public string LegacyId { get; set; }
        public Nullable<System.Guid> DataRestrictionId { get; set; }
        public System.Guid PersonId { get; set; }
        public System.Guid CaseId { get; set; }
        public Nullable<System.Guid> HealthAppointmentId { get; set; }
        public Nullable<System.Guid> CaseActionTypeId { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.Guid> CasePriorityId { get; set; }
        public string ActionDetails { get; set; }
        public Nullable<System.Guid> CaseActionStatusId { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public Nullable<int> NextAppointmentDay { get; set; }
        public Nullable<int> TimePeriodId { get; set; }
        public Nullable<System.DateTime> TargetDate { get; set; }
    
        public virtual Case Case { get; set; }
        public virtual Person Person { get; set; }
    }
}
