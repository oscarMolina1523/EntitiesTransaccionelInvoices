using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Endpoint.Data
{
    public interface ISqlDbConnection
    {
        //SqlConnection connection { get; set; }
        void OpenConnection();
        void CloseConnection();
        TProperty GetDataRowValue<TProperty>(DataRow row, string index, TProperty defaultValue = default);
        Task<DataTable> ExecuteQueryCommandAsync(string sql);
        Task<DataTable> ExecuteQueryCommandAsync(SqlCommand command);
        Task<int> ExecuteNonQueryCommandAsync(SqlCommand command);
        SqlDataAdapter CreateDataApdapter(string query);
        Task<bool> RunTransactionAsync(params SqlCommand[] commands);
    }
}