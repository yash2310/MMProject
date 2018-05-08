using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MM.Infrastructure.Repository;

namespace MM.WebApi.Controllers
{
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
                return LocationRepository.Add(location.Id, location.longitude, location.latitude);
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
        public int Id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
