using AccessPath.Data.Database;
using AccessPath.Data.Models;
using Microsoft.Data.SqlClient;

namespace AccessPath.Data.Repositories
{
    public class RouteReportRepository
    {
        public List<RouteReport> GetAllRouteReports()
        {
            List<RouteReport> reports = new List<RouteReport>();

            string query = @"
                SELECT
                    RouteReportID,
                    RouteReportType,
                    RouteReportDescription,
                    RouteReportStatus,
                    RouteReportDate,
                    RouteID,
                    UserID
                FROM RouteReports;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
               RouteReport routeReport = new RouteReport();

                routeReport.RouteReportID = reader.GetInt32(reader.GetOrdinal("RouteReportID"));

                routeReport.RouteReportType = reader.GetString(reader.GetOrdinal("RouteReportType"));

                int descriptiveIndex = reader.GetOrdinal("RouteReportDescription");

                if (reader.IsDBNull(descriptiveIndex))
                {
                    routeReport.RouteReportDescription = null;
                }
                else
                {
                    routeReport.RouteReportDescription = reader.GetString(descriptiveIndex);
                }

                routeReport.RouteReportStatus = reader.GetString(reader.GetOrdinal("RouteReportStatus"));

                routeReport.RouteReportDate = reader.GetDateTime(reader.GetOrdinal("RouteReportDate"));

                routeReport.RouteID = reader.GetInt32(reader.GetOrdinal("RouteID"));

                routeReport.UserID = reader.GetInt32(reader.GetOrdinal("UserID"));

                reports.Add(routeReport);
            }

            return reports;
        }

        public void UpdateReportStatus(int routeReportID, string routeReportStatus)
        {
            string query = @"
                UPDATE RouteReports
                SET
                    RouteReportStatus = @RouteReportStatus
                WHERE RouteReportID = @RouteReportID;";

            using SqlConnection connection = DatabaseConnection.GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@RouteReportStatus", routeReportStatus);

            command.Parameters.AddWithValue("@RouteReportID", routeReportID);

            command.ExecuteNonQuery();
        }

    }
}
