using MM.Infrastructure.Repository;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MM.Test
{
	[TestFixture]
	public class EmployeesTest
	{
		[Test]
		public void AllReportees()
		{
            var reportees = EmployeeRepository.GetAllReportee(1);
		}

		[Test]
		public void AllGoals()
		{
            var goals = EmployeeRepository.GetAllGoalsByUser(1);
		}

		[Test]
		public void AllReviewer()
		{
            var reviewers = EmployeeRepository.GetAllReviewerById(1);
            var goals = EmployeeRepository.GetAllGoalsByUser(1);
		}
	}
}
