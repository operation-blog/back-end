using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.DTO.Requests
{
    public class UserRegisterRequestDTO : UserRequestDTO
    {
        [Required]
        public string Token { get; set; }
    }
}
