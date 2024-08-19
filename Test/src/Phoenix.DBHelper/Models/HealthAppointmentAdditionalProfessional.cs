﻿using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentAdditionalProfessional : BaseClass
    {

        public string TableName = "HealthAppointmentAdditionalProfessional";
        public string PrimaryKeyName = "HealthAppointmentAdditionalProfessionalId";

        public HealthAppointmentAdditionalProfessional()
        {
            AuthenticateUser();
        }

        public HealthAppointmentAdditionalProfessional(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateHealthAppointmentAdditionalProfessional(Guid ownerid, Guid healthappointmentid, Guid healthprofessionalid, Guid personid, Guid caseid,
            DateTime startdate, TimeSpan starttime, DateTime enddate, TimeSpan endtime,
            bool professionalremainingforfullduration, bool addtraveltimetoappointment, bool returntobaseafterappointment)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthappointmentid", healthappointmentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "healthprofessionalid", healthprofessionalid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);
            AddFieldToBusinessDataObject(buisinessDataObject, "professionalremainingforfullduration", professionalremainingforfullduration);
            AddFieldToBusinessDataObject(buisinessDataObject, "addtraveltimetoappointment", addtraveltimetoappointment);
            AddFieldToBusinessDataObject(buisinessDataObject, "returntobaseafterappointment", returntobaseafterappointment);
            AddFieldToBusinessDataObject(buisinessDataObject, "homevisit", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "syncedwithmailbox", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);



            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetHealthAppointmentAdditionalProfessionalByHealthAppointmentID(Guid HealthAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "HealthAppointmentId", ConditionOperatorType.Equal, HealthAppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHealthAppointmentAdditionalProfessionalByID(Guid HealthAppointmentAdditionalProfessionalId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentAdditionalProfessionalId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteHealthAppointmentAdditionalProfessional(Guid HealthAppointmentAdditionalProfessionalId)
        {
            this.DeleteRecord(TableName, HealthAppointmentAdditionalProfessionalId);
        }
    }
}
