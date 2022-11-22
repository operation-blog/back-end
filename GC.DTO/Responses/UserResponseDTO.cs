using GF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Responses
{
    public class UserResponseDTO : BaseResponseDTO
    {
        public string Username { get; set; }

        public string Picture { get; set; }

        public Role Role { get; set; }
    }
}
