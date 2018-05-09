using System;
using System.Collections.Generic;

namespace MM.WebApi.Models
{
	public class User
	{
		public string status { get; set; }
		public string message { get; set; }
		public int user_id { get; set; }
		public string token { get; set; }
		public string name { get; set; }
		public DateTime dob { get; set; }
		public string gender { get; set; }
		public string mobile { get; set; }
		public string login_type { get; set; }
		public string email { get; set; }
		public string profile_pic { get; set; }
		public List<MstData> Interests { get; set; }
	}

	public class UserData
	{
		public virtual int UserId { get; set; }
		public virtual string Username { get; set; }
		public virtual string Email { get; set; }
		public virtual string Gender { get; set; }
		public virtual DateTime DOB { get; set; }
		public virtual string MobileNo { get; set; }
		public virtual string Token { get; set; }
		public virtual string LoginType { get; set; }
		public virtual string ImageUrl { get; set; }
	}

	public class MstData
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}