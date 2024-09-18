using System;

namespace Domain.Endpoint.DataAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        public string TableName { get; set; }
        public string Schema { get; set; } = string.Empty;


        public EntityAttribute(string tableName)
        {
            TableName = tableName;
        }
        
        public EntityAttribute(string schema, string tableName)
        {
            Schema = schema;
            TableName = tableName;
        }
    }
}
