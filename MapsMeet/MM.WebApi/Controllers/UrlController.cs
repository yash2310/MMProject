using System.Collections.Generic;
using System.Web.Http;

namespace MM.WebApi.Controllers
{
    [RoutePrefix("api/service")]
    public class UrlController : ApiController
    {
        [Route("url")]
        public List<UrlData> ServiceUrl()
        {
            List<UrlData> data = new List<UrlData>();

            data.Add(new UrlData
            {
                key = "version",
                value = System.Configuration.ConfigurationManager.AppSettings["app_version"]
            });
            data.Add(new UrlData {key = "user_security", value = "http://webapi.mapsmeet.com/api/Security"});
            data.Add(new UrlData {key = "add_user", value = "http://webapi.mapsmeet.com/api/security/user/add"});
            data.Add(new UrlData {key = "update_user", value = "http://webapi.mapsmeet.com/api/user/update"});
            data.Add(new UrlData {key = "add_location", value = "http://webapi.mapsmeet.com/api/user/location/add "});
            data.Add(new UrlData {key = "get_location", value = "http://webapi.mapsmeet.com/api/user/location/get"});
            data.Add(new UrlData {key = "match_user", value = "http://webapi.mapsmeet.com/api/user/match/get"});
            data.Add(new UrlData {key = "interest_master", value = "http://webapi.mapsmeet.com/api/interest/master"});
            data.Add(new UrlData
            {
                key = "user_interest",
                value = "http://webapi.mapsmeet.com/api/interest/userinterest "
            });

            return data;
        }
    }

    public class UrlData
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
