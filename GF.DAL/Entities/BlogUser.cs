using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Entities
{
    public class BlogUser
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int BlogId { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
