using MM.Infrastructure.Repository;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MM.Test
{
	[TestFixture]
	public class MasterTest
	{
		[Test]
		public void CycleTest()
		{
			var data = MasterRepository.GetCurrentCycle();
		}
	}
}
