using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MM.ApplicationCore.Entities
{
    public class Users
    {
        [Required]
        public virtual int UserId { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [StringLength(30, ErrorMessage = "First Name should not be more than 30 character")]
        public virtual string FirstName { get; set; } // FirstName for User

        [StringLength(30, ErrorMessage = "Last Name should not be more than 30 character")]
        public virtual string LastName { get; set; } // LastName for User

        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        public virtual string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth Required")]
        public virtual DateTime DOB { get; set; }

        public virtual string MobileNo { get; set; }

        public virtual string Token { get; set; }

        [Required(ErrorMessage = "Login Type Required")]
        public virtual string LoginType { get; set; }

        public virtual string ImageUrl { get; set; }

        [Required(ErrorMessage = "Created Date Required")]
        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }

        public virtual IList<Interest> Interests { get; set; }
    }
}
