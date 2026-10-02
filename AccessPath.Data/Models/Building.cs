using System;


namespace AccessPath.Data.Models
{
    public class Building
    {
        public int BuildingID { get; set; }
        public string BuildingName { get; set; } = string.Empty;
        public string BuildingType { get; set; } = string.Empty;
        public TimeSpan? OpeningHours { get; set; }
        public TimeSpan? ClosingHours { get; set; }
        public decimal BuildingLatitude { get; set; }
        public decimal BuildingLongitude { get; set; }
        public string? BuildingDescription { get; set; } 
        public int AddressID { get; set; }

    }
}
