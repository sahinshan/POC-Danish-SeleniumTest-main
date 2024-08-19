using System;

namespace Phoenix.DBHelper.Models
{
    public class TableQuestionCell : BaseClass
    {

        private string tableName = "TableQuestionCell";
        private string primaryKeyName = "TableQuestionCellId";

        public TableQuestionCell()
        {
            AuthenticateUser();
        }

        public TableQuestionCell(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateTableQuestionCell(Guid ParentQuestionId, Guid QuestionId, int RowPosition, int ColumnPosition, int PrintRowPosition, int PrintColumnPosition)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ParentQuestionId", ParentQuestionId);
            AddFieldToBusinessDataObject(dataObject, "QuestionId", QuestionId);
            AddFieldToBusinessDataObject(dataObject, "RowPosition", RowPosition);
            AddFieldToBusinessDataObject(dataObject, "ColumnPosition", ColumnPosition);
            AddFieldToBusinessDataObject(dataObject, "PrintRowPosition", PrintRowPosition);
            AddFieldToBusinessDataObject(dataObject, "PrintColumnPosition", PrintColumnPosition);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return this.CreateRecord(dataObject);
        }





    }
}
