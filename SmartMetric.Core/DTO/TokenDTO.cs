using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class TokenDTO
    {
        [Required(ErrorMessage ="Token is required")]
        public string? Token { get; set; }
        [Required(ErrorMessage = "RefreshToken is required")]
        public string? RefreshToken { get; set; }
    }
}
