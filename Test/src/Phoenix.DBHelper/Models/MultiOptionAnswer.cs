using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MultiOptionAnswer : BaseClass
    {

        private string tableName = "MultiOptionAnswer";
        private string primaryKeyName = "MultiOptionAnswerId";

        public MultiOptionAnswer()
        {
            AuthenticateUser();
        }

        public MultiOptionAnswer(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateMultiOptionAnswer(string Name, Guid QuestionCatalogueId, int DisplayPosition, int DecimalWeightage, int NumericWeightage)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "QuestionCatalogueId", QuestionCatalogueId);
            AddFieldToBusinessDataObject(dataObject, "DisplayPosition", DisplayPosition);
            AddFieldToBusinessDataObject(dataObject, "DecimalWeightage", DecimalWeightage);
            AddFieldToBusinessDataObject(dataObject, "NumericWeightage", NumericWeightage);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }



        public List<Guid> GetByNameAndQuestionCatalogueID(string Name, Guid QuestionCatalogueId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);
            this.BaseClassAddTableCondition(query, "QuestionCatalogueId", ConditionOperatorType.Equal, QuestionCatalogueId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



    }
}
