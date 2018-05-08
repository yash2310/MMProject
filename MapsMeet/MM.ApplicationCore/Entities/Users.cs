using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MM.ApplicationCore.Entities
{
    public class Users
    {
        [Required]
        public virtual int UserId { get; set; }

        [StringLength(50, ErrorMessage = "Username should not be more than 50 character")]
        public virtual string Username { get; set; } // FirstName for User

        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public virtual string Email { get; set; }

        public virtual string Gender { get; set; }

        public virtual DateTime DOB { get; set; }

        public virtual string MobileNo { get; set; }

        [Required(ErrorMessage = "Token Required")]
        public virtual string Token { get; set; }

        [Required(ErrorMessage = "Login Type Required")]
        public virtual string LoginType { get; set; }

        public virtual string ImageUrl { get; set; }

        [Required(ErrorMessage = "Created Date Required")]
        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

		public virtual Location Location { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }
    }
}
