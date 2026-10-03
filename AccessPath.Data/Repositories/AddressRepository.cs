using AccessPath.Data.Database;
using AccessPath.Data.Models;
using Microsoft.Data.SqlClient;

namespace AccessPath.Data.Repositories
{
    public class AddressRepository
    {
        public List<Address> GetAllAddresses()
        {
            List<Address> addresses = new List<Address>();
            
            string query = @"
                SELECT
                    AddressID,
                    Street,
                    City,
                    State,
                    Zipcode
                FROM Addresses
                ORDER BY Street;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Address address = new Address();

                address.AddressID = reader.GetInt32(reader.GetOrdinal("AddressID"));

                address.Street = reader.GetString(reader.GetOrdinal("Street"));

                address.City = reader.GetString(reader.GetOrdinal("City"));

                address.State = reader.GetString(reader.GetOrdinal("State"));

                address.Zipcode = reader.GetString(reader.GetOrdinal("Zipcode"));

                addresses.Add(address);
            }

            return addresses;
        }
    }
}
