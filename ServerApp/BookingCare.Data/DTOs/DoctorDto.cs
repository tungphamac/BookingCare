using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.DTOs
{
    public class DoctorDto
    {
        public int UserId { get; set; }
        public string Achievement { get; set; }
        public string Description { get; set; }
        public int SpecializationId { get; set; }
        public bool Gender { get; set; }
        public int ClinicId { get; set; }
        public string UserName { get; set; }
        
        public string Email { get; set; }
    }

}
