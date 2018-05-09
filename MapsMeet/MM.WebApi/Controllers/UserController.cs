using MM.Infrastructure.Repository;
using System;
using System.Web.Http;

namespace MM.WebApi.Controllers
{
	[RoutePrefix("api/user")]
	public class UserController : ApiController
    {
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("location/add")]
        public bool AddLocation(UserLocation location)
        {
            try
            {
                return LocationRepository.Add(location.UserId, location.longitude, location.latitude);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }

    public class UserLocation
    {
        public int UserId { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
