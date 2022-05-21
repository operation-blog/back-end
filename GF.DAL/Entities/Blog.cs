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
    public class Blog : BaseEntity
    {
        public Blog() { Authors = new List<BlogUser>(); CreatedDate = DateTime.Now.Date; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Data { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<BlogUser> Authors { get; set; }
    }
}
