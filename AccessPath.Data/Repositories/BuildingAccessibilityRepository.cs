using AccessPath.Data.Database;
using AccessPath.Data.Models;
using Microsoft.Data.SqlClient;

namespace AccessPath.Data.Repositories
{
    public class BuildingAccessibilityRepository
    {
        public List<BuildingAccessibility> GetAllAccessibility()
        {
            List<BuildingAccessibility> accessibilityRecords = new List<BuildingAccessibility>();

            string query = @"
                SELECT
                   AccessibilityID,
                   AccessibilityFeature,
                   AccessibilityDescription,
                   BuildingID
                FROM BuildingAccessibility;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                BuildingAccessibility buildingAccessibility = new BuildingAccessibility();

                buildingAccessibility.AccessibilityID = reader.GetInt32(reader.GetOrdinal("AccessibilityID"));

                buildingAccessibility.AccessibilityFeature = reader.GetString(reader.GetOrdinal("AccessibilityFeature"));

                int descriptiveIndex = reader.GetOrdinal("AccessibilityDescription");

                if (reader.IsDBNull(descriptiveIndex))
                {
                    buildingAccessibility.AccessibilityDescription = null;
                }
                else
                {
                    buildingAccessibility.AccessibilityDescription = reader.GetString(descriptiveIndex);
                }

                buildingAccessibility.BuildingID = reader.GetInt32(reader.GetOrdinal("BuildingID"));
                
                accessibilityRecords.Add(buildingAccessibility);

            }

            return accessibilityRecords;
        }

        public void AddAccessibility(BuildingAccessibility accessibility)
        {
            string query = @"
                INSERT INTO BuildingAccessibility
                (
                    AccessibilityFeature,
                    AccessibilityDescription,
                    BuildingID
                )
                VALUES
                (
                    @AccessibilityFeature,
                    @AccessibilityDescription,
                    @BuildingID
                );";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccessibilityFeature", accessibility.AccessibilityFeature);

            command.Parameters.AddWithValue("@AccessibilityDescription", (object?)accessibility.AccessibilityDescription ?? DBNull.Value);

            command.Parameters.AddWithValue("@BuildingID", accessibility.BuildingID);

            command.ExecuteNonQuery();
        }

        public void UpdateAccessibility(BuildingAccessibility accessibility)
        {
            string query = @"
                UPDATE BuildingAccessibility
                SET
                    AccessibilityFeature = @AccessibilityFeature,
                    AccessibilityDescription = @AccessibilityDescription,
                    BuildingID = @BuildingID
                WHERE AccessibilityID = @AccessibilityID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccessibilityFeature", accessibility.AccessibilityFeature);

            command.Parameters.AddWithValue("@AccessibilityDescription", (object?)accessibility.AccessibilityDescription ?? DBNull.Value);

            command.Parameters.AddWithValue("@BuildingID", accessibility.BuildingID);

            command.Parameters.AddWithValue("@AccessibilityID", accessibility.AccessibilityID);

            command.ExecuteNonQuery();
        }

        public void DeleteAccessibility(int accessibilityID)
        {
            string query = @"
                DELETE FROM BuildingAccessibility
                WHERE AccessibilityID = @AccessibilityID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AccessibilityID", accessibilityID);

            command.ExecuteNonQuery();
        }
    }
}
