using System;
using System.Data;

namespace Domain.Endpoint.DataAttributes.SQL
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlPropertyAttribute : Attribute
    {
        public string Name { get; set; }
        public SqlDbType SqlDbType { get; set; }
    }
}
