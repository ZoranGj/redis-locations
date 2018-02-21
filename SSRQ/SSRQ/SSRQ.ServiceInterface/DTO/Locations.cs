using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRQ.ServiceInterface.DTO
{
    [Route("/locations")]
    [Route("/locations/{name}/{longitude}/{latitude}")]
    public class Location : IReturn<LocationsResponse>
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string Name { get; set; }
    }

    public class LocationsResponse
    {
        public List<Location> Locations { get; set; }
        public DateTime CacheUpdated { get; set; }
    }

    public class UpdateLocationsResponse
    {
        public string Result { get; set; }
    }
}
