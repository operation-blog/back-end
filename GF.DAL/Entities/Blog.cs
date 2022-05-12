using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Entities
{
    public class Blog : BaseEntity
    {
        public Blog() { Authors = new List<BlogUser>(); }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<BlogUser> Authors { get; set; }
    }
}
