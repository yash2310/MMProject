using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MM.Infrastructure.Repository;
using MM.WebApi.Models;

namespace MM.WebApi.Controllers
{
    [RoutePrefix("api/interest")]
    public class InterestController : ApiController
    {
        [Route("master")]
        public List<MasterData> Get()
        {
            List<MasterData> masterDatas = new List<MasterData>();
            try
            {
                masterDatas = InterestRepository.GetInterests()
                    .Select(i => new MasterData { Id = i.Id, Name = i.Name })
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                masterDatas = null;
            }
            return masterDatas;
        }

        [Route("userinterest")]
        public bool Post(int User, List<int> Interest)
        {
            return InterestRepository.UserInterests(User, Interest);
        }
    }
}
