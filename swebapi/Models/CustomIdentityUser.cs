using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace swebapi.Models
{
    public class CustomIdentityUser : IdentityUser
    {
        [PersonalData]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }
    }
}
