using AccessPath.Data.Database;
using AccessPath.Data.Models;
using Microsoft.Data.SqlClient;
using AccessPath.Data.Security;

namespace AccessPath.Data.Repositories
{
    public class UserRepository
    {
        public User? GetUserByUsername(string username)
        {
            string query = @"
                SELECT
                    UserID,
                    Username,
                    PasswordHash,
                    Theme,
                    UserRole
                FROM Users
                WHERE Username = @Username;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", username);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                User user = new User();

                user.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));

                user.Username = reader.GetString(reader.GetOrdinal("Username"));

                user.PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"));

                user.Theme = reader.GetString(reader.GetOrdinal("Theme"));

                user.UserRole = reader.GetString(reader.GetOrdinal("UserRole"));

                return user;
            }

            return null;
        }

        public User? AuthenticateUser(string username, string password)
        {
            User? user = GetUserByUsername(username);

            if (user == null)
            {
                return null;
            }

            bool passwordCorrect = PasswordHasher.VerifyPassword(password, user.PasswordHash);

            if (!passwordCorrect)
            {
                return null;
            }

            return user;
        }
    }
}
