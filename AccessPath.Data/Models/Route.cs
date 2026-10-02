

namespace AccessPath.Data.Models
{
    public class Route
    {
        public int RouteID { get; set; }
        public int EstimatedMinutes { get; set; }
        public decimal RouteDistance { get; set; }
        public string RouteStatus { get; set; } = string.Empty;
        public string? RouteDescription { get; set; }
        public int StartBuildingID { get; set; }
        public int DestinationBuildingID { get; set; }
    }
}
