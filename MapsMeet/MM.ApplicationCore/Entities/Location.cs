namespace MM.ApplicationCore.Entities
{
	public class Location : BaseEntity
	{
		public virtual double Longitude { get; set; }
		public virtual double Latitude { get; set; }

		public virtual Users Users { get; set; }
	}
}