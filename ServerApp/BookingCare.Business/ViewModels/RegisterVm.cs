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
<<<<<<< HEAD
=======
        public string Phone  { get; set; }
>>>>>>> 5cc3c2d29b2c8e643c59e13f12e0d21a5db57a06
        public string Address { get; set; }   // Tùy chỉnh
        public string Avatar { get; set; }    // Tùy chỉnh (có thể là URL hoặc tên file)
        public int MedicalHistory { get; set; }
    }
}
