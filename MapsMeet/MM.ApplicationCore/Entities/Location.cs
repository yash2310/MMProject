using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.ApplicationCore.Entities
{
	public class Location : BaseEntity
	{
		public virtual double Longitude { get; set; }
		public virtual double Latitude { get; set; }

		public virtual Users Users { get; set; }
	}
}