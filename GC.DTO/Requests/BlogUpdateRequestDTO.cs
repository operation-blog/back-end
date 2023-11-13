using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Requests
{
    public class BlogUpdateRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(50)]
        [MaxLength(50000)]
        public string Data { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public int[] Authors { get; set; }
    }
}
