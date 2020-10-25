using System.Data.SqlClient;

namespace Infrastructure.SqlServer
{
    public static class Database
    {
        private static readonly string ConnectionString = "Server=.\\SQLExpress;Database=mymoneymanager;Integrated Security=SSPI";
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}