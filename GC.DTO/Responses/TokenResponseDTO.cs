using GF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Responses
{
    public class TokenResponseDTO : BaseResponseDTO
    {
        public string Token { get; set; }

        public UserResponseDTO Creator { get; set; }

        public UserResponseDTO UsedBy { get; set; }
    }
}
