using System;

namespace API_SAFEGUARD.Models
{
    public class UserToken
    {
        public String? Token { get; set; }
        public DateTime Expiration { get; set; }
        public IList<string> Roles { get; internal set; }
    }
}
