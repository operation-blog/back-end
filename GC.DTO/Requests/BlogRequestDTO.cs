using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Requests
{
    public class BlogRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(10000)]
        public string Data { get; set; }

        [Required]
        public int[] Authors { get; set; }
    }
}
