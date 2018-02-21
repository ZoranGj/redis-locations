using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using SSRQ.ServiceInterface.DTO;
using System.Linq;

namespace SSRQ.ServiceInterface
{
    public class LocationsService : Service
    {
        private IRedisTypedClient<Location> redisLocations;

        public LocationsService()
        {
            var redisManager = new RedisManagerPool();
            var redisClient = redisManager.GetClient();
            redisLocations = redisClient.As<Location>();
        }

        public LocationsResponse Get(Location filter)
        {
            var locations = redisLocations.GetAll();
            return new LocationsResponse { Locations = locations.ToList() };
        }

        public UpdateLocationsResponse Post(Location location)
        {
            redisLocations.Store(location);
            return new UpdateLocationsResponse { Result = "Success" };
        }
    }
}
