using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.ViewModels
{
    public class RegisterVm
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Gender { get; set; }    // Tùy chỉnh
        public string Address { get; set; }   // Tùy chỉnh
        public string Avatar { get; set; }    // Tùy chỉnh (có thể là URL hoặc tên file)
        public int MedicalHistory { get; set; }
    }
}
