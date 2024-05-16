using System;
using Microsoft.AspNetCore.Identity;

namespace API_SAFEGUARD.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int Cpf { get; set; }
    }
}
