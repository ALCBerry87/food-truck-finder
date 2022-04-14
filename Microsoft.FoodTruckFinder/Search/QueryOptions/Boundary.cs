namespace Microsoft.FoodTruckFinder.CLI.Search.QueryOptions
{
    public class Boundary
    {
        //TODO: add boundary validation (lat/long within appropriate range
        public Boundary(string boundaryField, double latitude, double longitude, int radiusInMeters)
        {
            _boundaryField = boundaryField;
            _latitude = latitude;
            _longitude = longitude;
            _radiusInMeters = radiusInMeters;
        }

        private string _boundaryField;
        public string BoundaryField => _boundaryField;
        private double _latitude { get; set; }
        public double Latitude => _latitude;
        private double _longitude { get; set; }
        public double Longitude => _longitude;
        private int _radiusInMeters { get; set; }
        public int RadiusInMeters => _radiusInMeters;
    }
}
