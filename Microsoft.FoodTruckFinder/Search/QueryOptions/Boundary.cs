namespace Microsoft.FoodTruckFinder.Search.QueryOptions
{
    internal class Boundary
    {
        public Boundary(string boundaryField, decimal latitude, decimal longitude, int radiusInMeters)
        {
            _boundaryField = boundaryField;
            _latitude = latitude;
            _longitude = longitude;
            _radiusInMeters = radiusInMeters;
        }

        private string _boundaryField;
        public string BoundaryField => _boundaryField;
        private decimal _latitude { get; set; }
        public decimal Latitude => _latitude;
        private decimal _longitude { get; set; }
        public decimal Longitude => _longitude;
        private int _radiusInMeters { get; set; }
        public int RadiusInMeters => _radiusInMeters;
    }
}
