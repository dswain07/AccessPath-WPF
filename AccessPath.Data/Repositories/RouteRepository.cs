using AccessPath.Data.Database;
using AccessPath.Data.Models;
using Microsoft.Data.SqlClient;

namespace AccessPath.Data.Repositories
{
    public class RouteRepository
    {
        public List<Route> GetAllRoutes()
        {
            List<Route> routes = new List<Route>();

            string query = @"
                SELECT
                    RouteID,
                    EstimatedMinutes,
                    RouteDistance,
                    RouteStatus,
                    RouteDescription,
                    StartBuildingID,
                    DestinationBuildingID
                FROM Routes;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Route route = new Route();

                route.RouteID = reader.GetInt32(reader.GetOrdinal("RouteID"));

                route.EstimatedMinutes = reader.GetInt32(reader.GetOrdinal("EstimatedMinutes"));

                route.RouteDistance = reader.GetDecimal(reader.GetOrdinal("RouteDistance"));

                route.RouteStatus = reader.GetString(reader.GetOrdinal("RouteStatus"));

                int descriptiveIndex = reader.GetOrdinal("RouteDescription");

                if (reader.IsDBNull(descriptiveIndex))
                {
                    route.RouteDescription = null;
                }
                else
                {
                    route.RouteDescription = reader.GetString(descriptiveIndex);
                }

                route.StartBuildingID = reader.GetInt32(reader.GetOrdinal("StartBuildingID"));

                route.DestinationBuildingID = reader.GetInt32(reader.GetOrdinal("DestinationBuildingID"));

                routes.Add(route);

            }

            return routes;
        }

        public void AddRoute(Route route)
        {
            string query = @"
                INSERT INTO Routes
                (
                    EstimatedMinutes,
                    RouteDistance,
                    RouteStatus,
                    RouteDescription,
                    StartBuildingID,
                    DestinationBuildingID
                )
                VALUES
                (
                    @EstimatedMinutes,
                    @RouteDistance,
                    @RouteStatus,
                    @RouteDescription,
                    @StartBuildingID,
                    @DestinationBuildingID
                );";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@EstimatedMinutes", route.EstimatedMinutes);

            command.Parameters.AddWithValue("@RouteDistance", route.RouteDistance);

            command.Parameters.AddWithValue("@RouteStatus", route.RouteStatus);

            command.Parameters.AddWithValue("@RouteDescription", (object?)route.RouteDescription ?? DBNull.Value);

            command.Parameters.AddWithValue("@StartBuildingID", route.StartBuildingID);

            command.Parameters.AddWithValue("@DestinationBuildingID", route.DestinationBuildingID);

            command.ExecuteNonQuery();
        }

        public void UpdateRoute(Route route)
        {
            string query = @"
                UPDATE Routes
                SET
                    EstimatedMinutes = @EstimatedMinutes,
                    RouteDistance = @RouteDistance,
                    RouteStatus = @RouteStatus,
                    RouteDescription = @RouteDescription,
                    StartBuildingID = @StartBuildingID,
                    DestinationBuildingID = @DestinationBuildingID
                WHERE RouteID = @RouteID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@EstimatedMinutes", route.EstimatedMinutes);

            command.Parameters.AddWithValue("@RouteDistance", route.RouteDistance);

            command.Parameters.AddWithValue("@RouteStatus", route.RouteStatus);

            command.Parameters.AddWithValue("@RouteDescription", (object?)route.RouteDescription ?? DBNull.Value);

            command.Parameters.AddWithValue("@StartBuildingID", route.StartBuildingID);

            command.Parameters.AddWithValue("@DestinationBuildingID", route.DestinationBuildingID);

            command.Parameters.AddWithValue("@RouteID", route.RouteID);

            command.ExecuteNonQuery();
        }

        public void DeleteRoute(int routeID)
        {
            string query = @"
                DELETE FROM Routes
                WHERE RouteID = @RouteID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RouteID", routeID);

            command.ExecuteNonQuery();
        }
    }
}
