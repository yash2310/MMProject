using System;
using System.ComponentModel.DataAnnotations;

namespace MM.ApplicationCore.Entities
{
    public class BaseEntity
    {
        [Required]
        public virtual int Id { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? UpdatedOn { get; set; }
    }
}
