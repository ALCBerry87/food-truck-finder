namespace Microsoft.FoodTruckFinder.Search
{
    internal class SearchResult
    {
        public double LocationId { get; set; }
        public string? Applicant { get; set; }
        public string? FacilityType { get; set; } //TODO: convert to enum
        public double Cnn { get; set; }
        public string? LocationDescription { get; set; }
        public string? Address { get; set; }
        public string Blocklot { get; set; }
        public string Block { get; set; }
        public string Lot { get; set; }
        public string Permit { get; set; }
        public string Status { get; set; }
        public string FoodItems { get; set; }
        //public string 
    }
}
