using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Requests
{
    public class UserUpdateRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Picture { get; set; }
    }
}
