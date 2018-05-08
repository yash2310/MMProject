using MM.ApplicationCore.Entities;
using MM.Infrastructure.DatabaseHelper;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Infrastructure.Repository
{
	public class LocationRepository
	{
		private static ISession _session;
		private static ITransaction _transaction;

		public bool Add(int Id, int Longitude, int Latitude)
		{
			bool status = false;
			try
			{
				using (_session = MMDatabaseHelper.Create().Session)
				{
					Users users = _session.CreateCriteria<Users>().List<Users>().FirstOrDefault(u => u.UserId.Equals(Id));
					if (users != null)
					{
						NHibernateUtil.Initialize(users.Location);

						Location location = users.Location;
						location.Longitude = Longitude;
						location.Latitude = Latitude;

						if (location == null)
							location.CreatedOn = DateTime.Now;
						else
							location.UpdatedOn = DateTime.Now;

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