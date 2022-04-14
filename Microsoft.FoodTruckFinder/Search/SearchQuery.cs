using Microsoft.FoodTruckFinder.Search.QueryOptions;

namespace Microsoft.FoodTruckFinder.Search
{
    internal class SearchQuery
    {
        public SearchQuery(string rootPath, Boundary boundary)
        {
            _rootPath = rootPath;
            _boundary = boundary;
        }

        private string _rootPath { get; set; }
        private Boundary _boundary { get; set; }

        private WhereOptions? _whereOptions { get; set; }

        public void AddWhere(WhereOptions options)
        {
            _whereOptions = options;
        }
 
        public string Build()
        {
            //Where clause will always be included with the query since the boundary is required)
            var query = $"{_rootPath}?$where=";

            var boundaryField = _boundary.BoundaryField;
            var lat = _boundary.Latitude;
            var lng = _boundary.Longitude;
            var radius = _boundary.RadiusInMeters;
            query += $"within_circle({boundaryField}, {lat}, {lng}, {radius})&";

            //add non-required where clause filters
            if (_whereOptions != null)
            {
                if (_whereOptions.Limit != null)
                {
                    query += $"$limit={_whereOptions.Limit}&";
                }

                if (_whereOptions.Status != null)
                {
                    query += $"status={_whereOptions.Status}&";
                }
            }

            //Note that longitude/latitude are reversed from the normal convention here
            var distanceFunction = $"distance_in_meters({boundaryField}, 'POINT({lng} {lat})')";

            //Sorting by distance and returning the calc with the result set
            query += $"$order={distanceFunction}&";
            query += $"$select=*, {distanceFunction} AS distance";

            return query;
        }
    }
}
