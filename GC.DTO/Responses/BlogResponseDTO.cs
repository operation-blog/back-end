using GF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Responses
{
    public class BlogResponseDTO : BaseResponseDTO
    {
        public string Title { get; set; }

        public ICollection<UserResponseDTO> Authors { get; set; }
    }
}
