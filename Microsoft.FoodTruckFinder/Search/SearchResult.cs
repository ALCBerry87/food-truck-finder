namespace Microsoft.FoodTruckFinder.Search
{
    internal class SearchResult
    {
        //Not including all props for the sake of time
        public double LocationId { get; set; }
        public string Applicant { get; set; }
        public string? FacilityType { get; set; } //TODO: convert to enum
        public string? Address { get; set; }
        public string? FoodItems { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal Distance { get; set; }
    }
}
