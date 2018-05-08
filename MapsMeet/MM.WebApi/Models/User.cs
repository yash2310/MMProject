using System;
using System.Collections.Generic;

namespace MM.WebApi.Models
{
    public class User
    {
        public string status { get; set; }
        public string message { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
        public DateTime dob { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string login_type { get; set; }
        public string email { get; set; }
        public string profile_pic { get; set; }
        public List<MstData> Interests { get; set; }

        //public int UserId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Email { get; set; }
        //public string Gender { get; set; }
        //public DateTime DOB { get; set; }
        //public string MobileNo { get; set; }
        //public string Token { get; set; }
        //public string LoginType { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public DateTime UpdatedOn { get; set; }
    }

    public class MstData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}