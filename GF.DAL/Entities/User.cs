using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Entities
{
    public class User : BaseEntity
    {
        public User() { CreatedDate = DateTime.Now.Date; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        public string Picture { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        [DefaultValue(Role.User)]
        public Role Role { get; set; }

        public virtual ICollection<Blog> OwnedBlogs { get; set; }

        public virtual ICollection<BlogUser> Blogs { get; set; }
    }
}
