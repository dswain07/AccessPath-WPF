using Microsoft.Data.SqlClient;

namespace AccessPath.Data.Database
{
    public class DatabaseConnection
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=AccessPath;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

    }
}
