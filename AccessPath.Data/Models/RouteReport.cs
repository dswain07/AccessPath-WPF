

namespace AccessPath.Data.Models
{
    public class RouteReport
    {
        public int RouteReportID { get; set; }
        public string RouteReportType { get; set; } = string.Empty;
        public string? RouteReportDescription { get; set; }
        public string RouteReportStatus { get; set; } = string.Empty;
        public DateTime RouteReportDate { get; set; }
        public int RouteID { get; set; }
        public int UserID { get; set; }
    }
}
