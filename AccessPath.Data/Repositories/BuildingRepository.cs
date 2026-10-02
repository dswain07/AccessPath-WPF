using AccessPath.Data.Database;
using AccessPath.Data.Models;
using Microsoft.Data.SqlClient;

namespace AccessPath.Data.Repositories
{
    public class BuildingRepository
    {
        public List<Building> GetAllBuildings()
        {
            List<Building> buildings = new List<Building>();

            string query = @"
                SELECT
                    BuildingID,
                    BuildingName,
                    BuildingType,
                    OpeningHours,
                    ClosingHours,
                    BuildingLatitude,
                    BuildingLongitude,
                    BuildingDescription,
                    AddressID
                FROM Buildings;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Building building = new Building();

                building.BuildingID = reader.GetInt32(reader.GetOrdinal("BuildingID"));

                building.BuildingName = reader.GetString(reader.GetOrdinal("BuildingName"));

                building.BuildingType = reader.GetString(reader.GetOrdinal("BuildingType"));

                building.BuildingLatitude = reader.GetDecimal(reader.GetOrdinal("BuildingLatitude"));

                building.BuildingLongitude = reader.GetDecimal(reader.GetOrdinal("BuildingLongitude"));

                int descriptiveIndex = reader.GetOrdinal("BuildingDescription");

                if (reader.IsDBNull(descriptiveIndex))
                {
                    building.BuildingDescription = null;
                }
                else
                {
                    building.BuildingDescription = reader.GetString(descriptiveIndex);
                }

                int openingIndex = reader.GetOrdinal("OpeningHours");
                if (reader.IsDBNull(openingIndex))
                {
                    building.OpeningHours = null;
                }
                else
                {
                    building.OpeningHours = reader.GetTimeSpan(openingIndex);
                }

                int closingIndex = reader.GetOrdinal("ClosingHours");
                if (reader.IsDBNull(closingIndex))
                {
                    building.ClosingHours = null;
                }
                else
                {
                    building.ClosingHours = reader.GetTimeSpan(closingIndex);
                }

                building.AddressID = reader.GetInt32(reader.GetOrdinal("AddressID"));

                buildings.Add(building);

            }

            return buildings;

        }
        public List<Building> SearchBuildings(string searchTerm)
        {
            List<Building> buildings = new List<Building>();

            string query = @"
                SELECT
                    BuildingID,
                    BuildingName,
                    BuildingType,
                    OpeningHours,
                    ClosingHours,
                    BuildingLatitude,
                    BuildingLongitude,
                    BuildingDescription,
                    AddressID
                FROM Buildings
                WHERE BuildingName LIKE @SearchTerm
                   OR BuildingType LIKE @SearchTerm;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@SearchTerm",
                "%" + searchTerm + "%"
                );

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Building building = new Building();

                building.BuildingID = reader.GetInt32(reader.GetOrdinal("BuildingID"));

                building.BuildingName = reader.GetString(reader.GetOrdinal("BuildingName"));

                building.BuildingType = reader.GetString(reader.GetOrdinal("BuildingType"));

                building.BuildingLatitude = reader.GetDecimal(reader.GetOrdinal("BuildingLatitude"));

                building.BuildingLongitude = reader.GetDecimal(reader.GetOrdinal("BuildingLongitude"));

                int descriptiveIndex = reader.GetOrdinal("BuildingDescription");

                if (reader.IsDBNull(descriptiveIndex))
                {
                    building.BuildingDescription = null;
                }
                else
                {
                    building.BuildingDescription = reader.GetString(descriptiveIndex);
                }

                int openingIndex = reader.GetOrdinal("OpeningHours");
                if (reader.IsDBNull(openingIndex))
                {
                    building.OpeningHours = null;
                }
                else
                {
                    building.OpeningHours = reader.GetTimeSpan(openingIndex);
                }

                int closingIndex = reader.GetOrdinal("ClosingHours");
                if (reader.IsDBNull(closingIndex))
                {
                    building.ClosingHours = null;
                }
                else
                {
                    building.ClosingHours = reader.GetTimeSpan(closingIndex);
                }

                building.AddressID = reader.GetInt32(reader.GetOrdinal("AddressID"));

                buildings.Add(building);

            }

            return buildings;
        }

        public void AddBuilding(Building building)
        {
            string query = @"
                INSERT INTO Buildings
                (
                    BuildingName,
                    BuildingType,
                    OpeningHours,
                    ClosingHours,
                    BuildingLatitude,
                    BuildingLongitude,
                    BuildingDescription,
                    AddressID
                )
                VALUES
                (
                    @BuildingName,
                    @BuildingType,
                    @OpeningHours,
                    @ClosingHours,
                    @BuildingLatitude,
                    @BuildingLongitude,
                    @BuildingDescription,
                    @AddressID
                );";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BuildingName", building.BuildingName);

            command.Parameters.AddWithValue("@BuildingType", building.BuildingType);

            command.Parameters.AddWithValue("@OpeningHours", (object?)building.OpeningHours ?? DBNull.Value);

            command.Parameters.AddWithValue("@ClosingHours", (object?)building.ClosingHours ?? DBNull.Value);

            command.Parameters.AddWithValue("@BuildingLatitude", building.BuildingLatitude);

            command.Parameters.AddWithValue("@BuildingLongitude", building.BuildingLongitude);

            command.Parameters.AddWithValue("@BuildingDescription", (object?)building.BuildingDescription ?? DBNull.Value);

            command.Parameters.AddWithValue("@AddressID", building.AddressID);

            command.ExecuteNonQuery();
        }

        public void UpdateBuilding(Building building)
        {
            string query = @"
                UPDATE Buildings
                SET
                    BuildingName = @BuildingName,
                    BuildingType = @BuildingType,
                    OpeningHours = @OpeningHours,
                    ClosingHours = @ClosingHours,
                    BuildingLatitude = @BuildingLatitude,
                    BuildingLongitude = @BuildingLongitude,
                    BuildingDescription = @BuildingDescription,
                    AddressID = @AddressID
                WHERE BuildingID = @BuildingID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BuildingName", building.BuildingName);

            command.Parameters.AddWithValue("@BuildingType", building.BuildingType);

            command.Parameters.AddWithValue("@OpeningHours", (object?)building.OpeningHours ?? DBNull.Value);

            command.Parameters.AddWithValue("@ClosingHours", (object?)building.ClosingHours ?? DBNull.Value);

            command.Parameters.AddWithValue("@BuildingLatitude", building.BuildingLatitude);

            command.Parameters.AddWithValue("@BuildingLongitude", building.BuildingLongitude);

            command.Parameters.AddWithValue("@BuildingDescription", (object?)building.BuildingDescription ?? DBNull.Value);

            command.Parameters.AddWithValue("@AddressID", building.AddressID);

            command.Parameters.AddWithValue("@BuildingID", building.BuildingID);

            command.ExecuteNonQuery();
        }

        public void DeleteBuilding(int buildingID)
        {
            string query = @"
                DELETE FROM Buildings
                WHERE BuildingID = @BuildingID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@BuildingID", buildingID);

            command.ExecuteNonQuery();
        }

    }
}
