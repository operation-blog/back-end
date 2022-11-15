using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [DefaultValue(Role.User)]
        public Role Role { get; set; }

        public virtual ICollection<Blog> OwnedBlogs { get; set; }

        public virtual ICollection<BlogUser> Blogs { get; set; }
    }
}
