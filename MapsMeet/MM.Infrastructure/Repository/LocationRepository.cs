using MM.ApplicationCore.Entities;
using MM.Infrastructure.DatabaseHelper;
using NHibernate;
using System;
using System.Linq;

namespace MM.Infrastructure.Repository
{
    public class LocationRepository
    {
        private static ISession _session;

        public static bool Add(int id, double longitude, double latitude)
        {
            bool status = false;
            try
            {
                using (_session = MMDatabaseHelper.Create().Session)
                {
                    Users users = _session.CreateCriteria<Users>().List<Users>()
                        .FirstOrDefault(u => u.UserId.Equals(id));
                    if (users != null)
                    {
                        NHibernateUtil.Initialize(users.Location);

                        Location location = users.Location;

                        if (location == null)
                        {
                            location = new Location();
                            location.Longitude = longitude;
                            location.Latitude = latitude;
                            location.CreatedOn = DateTime.Now;
                        }
                        else
                        {
                            location.Longitude = longitude;
                            location.Latitude = latitude;
                            location.UpdatedOn = DateTime.Now;
                        }

                        _session.SaveOrUpdate(location);

                        status = true;
                    }
                    else
                    {
                        status = false;
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
    }
}