using System.Collections.Generic;

namespace MM.WebApi.ViewModels
{
    public class EmployeeData
    {
        public string token { get; set; }
        public virtual string Name { get; set; }
        public virtual string EmailId { get; set; }
        public virtual long EmployeeId { get; set; }
        public virtual string EmployeeNo { get; set; }
        public virtual long ContactNo { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual bool NewUser { get; set; }

        public virtual Data ReportingManager { get; set; }
        public virtual Data Designation { get; set; }
        public virtual Data Department { get; set; }
        public virtual Data Organization { get; set; }
        public virtual List<Data> Roles { get; set; }
        public virtual Data ReviewCycle { get; set; }
    }

	public class Data
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}