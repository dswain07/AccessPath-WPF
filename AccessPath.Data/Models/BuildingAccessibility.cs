namespace AccessPath.Data.Models
{
    public class BuildingAccessibility
    {
        public int AccessibilityID { get; set; }
        public string AccessibilityFeature { get; set; } = string.Empty;
        public string? AccessibilityDescription { get; set; }
        public int BuildingID { get; set; }
    }
}
