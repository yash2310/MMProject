using MM.ApplicationCore.Entities;
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

		[HttpPost]
		[Route("location/get")]
		public UserLocation UserLocation(int id)
		{
			UserLocation userLocation = new UserLocation();
			try
			{
				Location location = LocationRepository.GetLocationById(id);

				if (location == null)
				{
					return null;
				}
				else
				{
					userLocation = new UserLocation { UserId = location.Users.UserId, longitude = location.Longitude, latitude = location.Latitude };
					return userLocation;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
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
