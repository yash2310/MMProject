using System.Collections.Generic;

namespace MM.WebApi.ViewModels
{
    public class ReporteeData
    {
        public string Name { get; set; }
        public string EmployeeNo { get; set; }
        public string Designation { get; set; }
        public string EmailId { get; set; }
        public int GoalsCount { get; set; }
        public string GoalStatus { get; set; }
        public List<string> Role { get; set; }
        public int ReporteeId { get; set; }
    }
}