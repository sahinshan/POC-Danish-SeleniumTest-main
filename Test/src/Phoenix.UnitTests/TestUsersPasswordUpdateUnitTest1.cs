﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Phoenix.UnitTests
{
    [TestClass]
    public class TestUsersPasswordUpdateUnitTest1
    {
        List<string> usernames = new List<string> { "test_user_26043", "test_user_26044", "test_user_26045", "test_user_26046", "test_user_26047", "test_user_26048", "test_user_26049", "test_user_26050", "test_user_26051", "test_user_26052", "test_user_26053", "test_user_26054", "test_user_26055", "test_user_26056", "test_user_26057", "test_user_26058", "test_user_26059", "test_user_26060", "test_user_26061", "test_user_26062", "test_user_26063", "test_user_26064", "test_user_26065", "test_user_26066", "test_user_26067", "test_user_26068", "test_user_26069", "test_user_26070", "test_user_26071", "test_user_26072", "test_user_26073", "test_user_26074", "test_user_26075", "test_user_26076", "test_user_26077", "test_user_26078", "test_user_26079", "test_user_26080", "test_user_26081", "test_user_26082", "test_user_26083", "test_user_26084", "test_user_26085", "test_user_26086", "test_user_26087", "test_user_26088", "test_user_26089", "test_user_26090", "test_user_26091", "test_user_26092", "test_user_26093", "test_user_26094", "test_user_26095", "test_user_26096", "test_user_26097", "test_user_26098", "test_user_26099", "test_user_26100", "test_user_26101", "test_user_26102", "test_user_26103", "test_user_26104", "test_user_26105", "test_user_26106", "test_user_26107", "test_user_26108", "test_user_26109", "test_user_26110", "test_user_26111", "test_user_26112", "test_user_26113", "test_user_26114", "test_user_26115", "test_user_26116", "test_user_26117", "test_user_26118", "test_user_26119", "test_user_26120", "test_user_26121", "test_user_26122", "test_user_26123", "test_user_26124", "test_user_26125", "test_user_26126", "test_user_26127", "test_user_26128", "test_user_26129", "test_user_26130", "test_user_26131", "test_user_26132", "test_user_26133", "test_user_26134", "test_user_26135", "test_user_26136", "test_user_26137", "test_user_26138", "test_user_26139", "test_user_26140", "test_user_26141", "test_user_26142", "test_user_26143", "test_user_26144", "test_user_26145", "test_user_26146", "test_user_26147", "test_user_26148", "test_user_26149", "test_user_26150", "test_user_26151", "test_user_26152", "test_user_26153", "test_user_26154", "test_user_26155", "test_user_26156", "test_user_26157", "test_user_26158", "test_user_26159", "test_user_26160", "test_user_26161", "test_user_26162", "test_user_26163", "test_user_26164", "test_user_26165", "test_user_26166", "test_user_26167", "test_user_26168", "test_user_26169", "test_user_26170", "test_user_26171", "test_user_26172", "test_user_26173", "test_user_26174", "test_user_26175", "test_user_26176", "test_user_26177", "test_user_26178", "test_user_26179", "test_user_26180", "test_user_26181", "test_user_26182", "test_user_26183", "test_user_26184", "test_user_26185", "test_user_26186", "test_user_26187", "test_user_26188", "test_user_26189", "test_user_26190", "test_user_26191", "test_user_26192", "test_user_26193", "test_user_26194", "test_user_26195", "test_user_26196", "test_user_26197", "test_user_26198", "test_user_26199", "test_user_26200", "test_user_26201", "test_user_26202", "test_user_26203", "test_user_26204", "test_user_26205", "test_user_26206", "test_user_26207", "test_user_26208", "test_user_26209", "test_user_26210", "test_user_26211", "test_user_26212", "test_user_26213", "test_user_26214", "test_user_26215", "test_user_26216", "test_user_26217", "test_user_26218", "test_user_26219", "test_user_26220", "test_user_26221", "test_user_26222", "test_user_26223", "test_user_26224", "test_user_26225", "test_user_26226", "test_user_26227", "test_user_26228", "test_user_26229", "test_user_26230", "test_user_26231", "test_user_26232", "test_user_26233", "test_user_26234", "test_user_26235", "test_user_26236", "test_user_26237", "test_user_26238", "test_user_26239", "test_user_26240", "test_user_26241", "test_user_26242", "test_user_26243", "test_user_26244", "test_user_26245", "test_user_26246", "test_user_26247", "test_user_26248", "test_user_26249", "test_user_26250", "test_user_26251", "test_user_26252", "test_user_26253", "test_user_26254", "test_user_26255", "test_user_26256", "test_user_26257", "test_user_26258", "test_user_26259", "test_user_26260", "test_user_26261", "test_user_26262", "test_user_26263", "test_user_26264", "test_user_26265", "test_user_26266", "test_user_26267", "test_user_26268", "test_user_26269", "test_user_26270", "test_user_26271", "test_user_26272", "test_user_26273", "test_user_26274", "test_user_26275", "test_user_26276", "test_user_26277", "test_user_26278", "test_user_26279", "test_user_26280", "test_user_26281", "test_user_26282", "test_user_26283", "test_user_26284", "test_user_26285", "test_user_26286", "test_user_26287", "test_user_26288", "test_user_26289", "test_user_26290", "test_user_26291", "test_user_26292", "test_user_26293", "test_user_26294", "test_user_26295", "test_user_26296", "test_user_26297", "test_user_26298", "test_user_26299", "test_user_26300", "test_user_26301", "test_user_26302", "test_user_26303", "test_user_26304", "test_user_26305", "test_user_26306", "test_user_26307", "test_user_26308", "test_user_26309", "test_user_26310", "test_user_26311", "test_user_26312", "test_user_26313", "test_user_26314", "test_user_26315", "test_user_26316", "test_user_26317", "test_user_26318", "test_user_26319", "test_user_26320", "test_user_26321", "test_user_26322", "test_user_26323", "test_user_26324", "test_user_26325", "test_user_26326", "test_user_26327", "test_user_26328", "test_user_26329", "test_user_26330", "test_user_26331", "test_user_26332", "test_user_26333", "test_user_26334", "test_user_26335", "test_user_26336", "test_user_26337", "test_user_26338", "test_user_26339", "test_user_26340", "test_user_26341", "test_user_26342", "test_user_26343", "test_user_26344", "test_user_26345", "test_user_26346", "test_user_26347", "test_user_26348", "test_user_26349", "test_user_26350", "test_user_26351", "test_user_26352", "test_user_26353", "test_user_26354", "test_user_26355", "test_user_26356", "test_user_26357", "test_user_26358", "test_user_26359", "test_user_26360", "test_user_26361", "test_user_26362", "test_user_26363", "test_user_26364", "test_user_26365", "test_user_26366", "test_user_26367", "test_user_26368", "test_user_26369", "test_user_26370", "test_user_26371", "test_user_26372", "test_user_26373", "test_user_26374", "test_user_26375", "test_user_26376", "test_user_26377", "test_user_26378", "test_user_26379", "test_user_26380", "test_user_26381", "test_user_26382", "test_user_26383", "test_user_26384", "test_user_26385", "test_user_26386", "test_user_26387", "test_user_26388", "test_user_26389", "test_user_26390", "test_user_26391", "test_user_26392", "test_user_26393", "test_user_26394", "test_user_26395", "test_user_26396", "test_user_26397", "test_user_26398", "test_user_26399", "test_user_26400", "test_user_26401", "test_user_26402", "test_user_26403", "test_user_26404", "test_user_26405", "test_user_26406", "test_user_26407", "test_user_26408", "test_user_26409", "test_user_26410", "test_user_26411", "test_user_26412", "test_user_26413", "test_user_26414", "test_user_26415", "test_user_26416", "test_user_26417", "test_user_26418", "test_user_26419", "test_user_26420", "test_user_26421", "test_user_26422", "test_user_26423", "test_user_26424", "test_user_26425", "test_user_26426", "test_user_26427", "test_user_26428", "test_user_26429", "test_user_26430", "test_user_26431", "test_user_26432", "test_user_26433", "test_user_26434", "test_user_26435", "test_user_26436", "test_user_26437", "test_user_26438", "test_user_26439", "test_user_26440", "test_user_26441", "test_user_26442", "test_user_26443", "test_user_26444", "test_user_26445", "test_user_26446", "test_user_26447", "test_user_26448", "test_user_26449", "test_user_26450", "test_user_26451", "test_user_26452", "test_user_26453", "test_user_26454", "test_user_26455", "test_user_26456", "test_user_26457", "test_user_26458", "test_user_26459", "test_user_26460", "test_user_26461", "test_user_26462", "test_user_26463", "test_user_26464", "test_user_26465", "test_user_26466", "test_user_26467", "test_user_26468", "test_user_26469", "test_user_26470", "test_user_26471", "test_user_26472", "test_user_26473", "test_user_26474", "test_user_26475", "test_user_26476", "test_user_26477", "test_user_26478", "test_user_26479", "test_user_26480", "test_user_26481", "test_user_26482", "test_user_26483", "test_user_26484", "test_user_26485", "test_user_26486", "test_user_26487", "test_user_26488", "test_user_26489", "test_user_26490", "test_user_26491", "test_user_26492", "test_user_26493", "test_user_26494", "test_user_26495", "test_user_26496", "test_user_26497", "test_user_26498", "test_user_26499", "test_user_26500", "test_user_26501", "test_user_26502", "test_user_26503", "test_user_26504", "test_user_26505", "test_user_26506", "test_user_26507", "test_user_26508", "test_user_26509", "test_user_26510", "test_user_26511", "test_user_26512", "test_user_26513", "test_user_26514", "test_user_26515", "test_user_26516", "test_user_26517", "test_user_26518", "test_user_26519", "test_user_26520", "test_user_26521", "test_user_26522", "test_user_26523", "test_user_26524", "test_user_26525", "test_user_26526", "test_user_26527", "test_user_26528", "test_user_26529", "test_user_26530", "test_user_26531", "test_user_26532", "test_user_26533", "test_user_26534", "test_user_26535", "test_user_26536", "test_user_26537", "test_user_26538", "test_user_26539", "test_user_26540", "test_user_26541", "test_user_26542" };

        [TestMethod]
        public void TestUsersPasswordUpdate_TestMethod1()
        {
            using (CareDirectorCore_CDEntities entity = new CareDirectorCore_CDEntities())
            {
                string value = "Passw0rd_!";
                Guid jbrazetaUserID = new Guid("32972024-0839-E911-A2C5-005056926FE4");
                List<SystemUser> systemUsers = new List<SystemUser>();
                systemUsers.AddRange(entity.SystemUsers.Where(x => x.UserName.StartsWith("test_user_") && x.CreatedBy == jbrazetaUserID).ToList());

                foreach (SystemUser su in systemUsers)
                {
                    string secret = su.SystemUserId.ToString();

                    var secretBytes = Encoding.UTF8.GetBytes(secret);
                    var valueBytes = Encoding.UTF8.GetBytes(value);
                    string signature;

                    using (var hmac = new HMACSHA256(secretBytes))
                    {
                        var hash = hmac.ComputeHash(valueBytes);
                        signature = Convert.ToBase64String(hash);
                    }

                    su.Password = signature;
                }

                entity.SaveChanges();
            }
        }
    }
}
