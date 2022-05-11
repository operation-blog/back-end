using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.DAL.Entities
{
    public class AccessToken : BaseEntity
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Used { get; set; }

        public virtual User Creator { get; set; }

        public virtual User UsedBy { get; set; }
    }
}
