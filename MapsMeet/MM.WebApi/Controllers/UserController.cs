using MM.ApplicationCore.Entities;
using MM.Infrastructure.Repository;
using MM.WebApi.Models;
using System;
using System.Collections.Generic;
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

		[HttpPost]
		[Route("match/get")]
		public List<MatchedUser> GetMatchingUser(MatchData matchData)
		{
			List<MatchedUser> matchedUsers = new List<MatchedUser>();
			try
			{
				List<Users> users = InterestRepository.GetInterestMatch(matchData.Id, matchData.Gender, matchData.Area, matchData.Longitude, matchData.Latitude);

				foreach (var data in users)
				{
					List<MstData> mstDatas = new List<MstData>();
					double distance = Helper.GeoDistance(matchData.Latitude, matchData.Longitude, data.Location.Latitude, data.Location.Longitude, 'K');
					if (distance <= matchData.Area && matchData.Gender.ToLower().Equals(data.Gender.ToLower()))
					{
						foreach (var intr in data.Interests)
						{
							mstDatas.Add(new MstData { Id = intr.Id, Name = intr.Name });
						}
						matchedUsers.Add(new MatchedUser { UserId = data.UserId, Name = data.Username, Gender = data.Gender, Interests = mstDatas, Latitude = data.Location.Latitude, Longitude = data.Location.Longitude });
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				matchedUsers = null;
			}

			return matchedUsers;
		}
	}

    public class UserLocation
    {
        public int UserId { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
}
