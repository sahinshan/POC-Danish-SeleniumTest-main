using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Accommodationstatusid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Addresspropertytypeid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Addresstypeid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Agegroupid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
    }

    public class Ethnicityid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Genderid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Languageid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Livesalonetypeid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Maritalstatusid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }


    public class Nhsnumberstatusid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Person
    {
        public string id { get; set; }
        public Accommodationstatusid accommodationstatusid { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string addressline3 { get; set; }
        public string addressline4 { get; set; }
        public string addressline5 { get; set; }
        public Addresspropertytypeid addresspropertytypeid { get; set; }
        public Addresstypeid addresstypeid { get; set; }
        public string adultsafeguardingflag { get; set; }
        public string age { get; set; }
        public Agegroupid agegroupid { get; set; }
        public string allergiesnotrecorded { get; set; }
        public string allowemail { get; set; }
        public string allowmail { get; set; }
        public string allowphone { get; set; }
        public string allowsms { get; set; }
        public string childprotectionflag { get; set; }
        public string country { get; set; }
        public Createdby createdby { get; set; }
        public string createdon { get; set; }
        public string dateofbirth { get; set; }
        public string deceased { get; set; }
        public Ethnicityid ethnicityid { get; set; }
        public string excludefromdbs { get; set; }
        public string firstname { get; set; }
        public string fulladdress { get; set; }
        public string fullname { get; set; }
        public Genderid genderid { get; set; }
        public string inactive { get; set; }
        public string interpreterrequired { get; set; }
        public string isexternalperson { get; set; }
        public string islookedafterchild { get; set; }
        public string knownallergies { get; set; }
        public Languageid languageid { get; set; }
        public string lastname { get; set; }
        public string linkedaddressid { get; set; }
        public Livesalonetypeid livesalonetypeid { get; set; }
        public Maritalstatusid maritalstatusid { get; set; }
        public Modifiedby modifiedby { get; set; }
        public string modifiedon { get; set; }
        public string nhsnumber { get; set; }
        public Nhsnumberstatusid nhsnumberstatusid { get; set; }
        public string noknownallergies { get; set; }
        public Ownerid ownerid { get; set; }
        public string pdsisdeferred { get; set; }
        public string pdsnhsnosuperseded { get; set; }
        public string personnumber { get; set; }
        public string postcode { get; set; }
        public string propertyname { get; set; }
        public string recordedinerror { get; set; }
        public string relatedadultsafeguardingflag { get; set; }
        public string relatedchildprotectionflag { get; set; }
        public string representalertorhazard { get; set; }
        public string retaininformationconcern { get; set; }
        public string suppressstatementinvoices { get; set; }
        public string updatepersonaddress { get; set; }
        public string uprn { get; set; }
    }


}
