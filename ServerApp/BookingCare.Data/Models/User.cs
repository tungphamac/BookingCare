using Microsoft.AspNetCore.Identity;

namespace BookingCare.Data.Models
{
    public class User : IdentityUser
    {
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        // Thiết lập quan hệ 1-1 với Doctor và Patient
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
    }
}
