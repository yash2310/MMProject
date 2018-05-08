using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MM.ApplicationCore.Entities
{
    public class Interest : BaseEntity
    {
        [Required(ErrorMessage = "Name Required")]
        public virtual string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
