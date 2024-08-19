using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FAQ : BaseClass
    {
        public string TableName { get { return "FAQ"; } }
        public string PrimaryKeyName { get { return "FAQid"; } }


        public FAQ()
        {
            AuthenticateUser();
        }

        public FAQ(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFAQ(Guid ownerid, Guid faqcategoryid, Guid languageid, string title, string keywords, string contents, int numberofupvotes, int numberofdownvotes, string SeoName)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "faqcategoryid", faqcategoryid);
            AddFieldToBusinessDataObject(dataObject, "languageid", languageid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "keywords", keywords);
            AddFieldToBusinessDataObject(dataObject, "contents", contents);
            AddFieldToBusinessDataObject(dataObject, "numberofupvotes", numberofupvotes);
            AddFieldToBusinessDataObject(dataObject, "numberofdownvotes", numberofdownvotes);
            AddFieldToBusinessDataObject(dataObject, "seoname", SeoName);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);
            AddFieldToBusinessDataObject(dataObject, "statusid", 2);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);



            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAllPublishedFAQsOrderedByUpvotes()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            query.Orders.Add(new OrderBy("numberofupvotes", SortOrder.Descending, TableName));

            this.AddRelatedTableRelationship(query, "ApplicationComponent", "ComponentId", TableName, PrimaryKeyName, JoinOperator.InnerJoin, "ApplicationComponent");

            this.BaseClassAddTableCondition(query, "StatusId", ConditionOperatorType.Equal, 2);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Distinct = true;

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAllPublishedFAQsOrderedByUpvotes(Guid LinkedApplicationID)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            query.Orders.Add(new OrderBy("numberofupvotes", SortOrder.Descending, TableName));

            this.AddRelatedTableRelationship(query, "ApplicationComponent", "ComponentId", TableName, PrimaryKeyName, JoinOperator.InnerJoin, "ApplicationComponent");

            this.AddRelatedTableCondition(query, "ApplicationComponent", "ApplicationId", ConditionOperatorType.Equal, LinkedApplicationID);

            this.BaseClassAddTableCondition(query, "StatusId", ConditionOperatorType.Equal, 2);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Distinct = true;

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAllPublishedFAQsOrderedByUpvotes(Guid LinkedApplicationID, Guid FAQCategoryId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            query.Orders.Add(new OrderBy("numberofupvotes", SortOrder.Descending, TableName));

            this.AddRelatedTableRelationship(query, "ApplicationComponent", "ComponentId", TableName, PrimaryKeyName, JoinOperator.InnerJoin, "ApplicationComponent");

            this.AddRelatedTableCondition(query, "ApplicationComponent", "ApplicationId", ConditionOperatorType.Equal, LinkedApplicationID);

            this.BaseClassAddTableCondition(query, "StatusId", ConditionOperatorType.Equal, 2);
            this.BaseClassAddTableCondition(query, "FAQCategoryId", ConditionOperatorType.Equal, FAQCategoryId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Distinct = true;

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByKeywords(string keywords)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "keywords", ConditionOperatorType.Contains, keywords);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByTitle(string title)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetFAQByID(Guid FAQId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("FAQ", false, "FAQId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "FAQId", ConditionOperatorType.Equal, FAQId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateFAQ(Guid FAQId, int upvotes, int downvotes)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, FAQId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "NumberOfUpvotes", upvotes);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "NumberOfDownvotes", downvotes);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteFAQ(Guid FAQId)
        {
            this.DeleteRecord(TableName, FAQId);
        }
    }
}
