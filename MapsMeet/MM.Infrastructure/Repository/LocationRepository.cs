using MM.ApplicationCore.Entities;
using MM.Infrastructure.DatabaseHelper;
using NHibernate;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MM.Infrastructure.Repository
{
	public class LocationRepository
	{
		private static ISession _session;
		private static ITransaction _transaction;

	    public static bool Add(int id, double longitude, double latitude, int login)
	    {
	        bool status = false;
	        try
	        {
	            using (_session = MMDatabaseHelper.Create().Session)
	            {
	                using (_transaction = _session.BeginTransaction())
	                {
	                    Users users = _session.CreateCriteria<Users>().List<Users>()
	                        .FirstOrDefault(u => u.UserId.Equals(id));

	                    if (users != null)
	                    {
	                        Location locations = _session.CreateCriteria<Location>().List<Location>()
	                            .FirstOrDefault(l => l.Users.UserId.Equals(id));

	                        if (locations == null)
	                        {
	                            locations = new Location();
	                            locations.Longitude = longitude;
	                            locations.Latitude = latitude;
	                            locations.LoggedIn = login;
	                            locations.CreatedOn = DateTime.Now;
	                            locations.Users = users;
	                            _session.Save(locations);
	                        }
	                        else
	                        {
	                            locations.Longitude = longitude;
	                            locations.Latitude = latitude;
	                            locations.LoggedIn = login;
	                            locations.UpdatedOn = DateTime.Now;
	                            _session.Update(locations);
	                        }
	                        _transaction.Commit();
	                        status = true;
	                    }
	                    else
	                    {
	                        status = false;
	                    }
	                }
	            }
	        }
	        catch (Exception ex)
	        {
	            Console.WriteLine(ex.Message);
	            status = false;
	        }
	        return status;
	    }

	    public static Location GetLocationById(int id)
		{
			Location locations = new Location();
			try
			{
				using (_session = MMDatabaseHelper.Create().Session)
				{
					locations = _session.CreateCriteria<Location>().List<Location>().FirstOrDefault(l => l.Users.UserId.Equals(id));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				locations = null;
			}
			return locations;
		}
	}
}