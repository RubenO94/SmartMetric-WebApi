using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class AuthenticationResponse
    {
        public string? UserName { get; set; } = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public string? RefreshToken {  get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
