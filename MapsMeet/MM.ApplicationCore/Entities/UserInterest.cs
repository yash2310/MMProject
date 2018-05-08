using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.ApplicationCore.Entities
{
    public class UserInterest : BaseEntity
    {
        public virtual Users Users { get; set; }
        public virtual Interest Interest { get; set; }
    }
}
