using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingCare.Business.ViewModels
{
    public class UpdatePartientVm
    {
        public string UserName { get; set; }   
        public bool Gender { get; set; }         
        public string Address { get; set; }     
        public string Phone {  get; set; }  
        public string Email { get; set; }        // Email (lấy từ User nếu cần)
        public string Avatar { get; set; }
    }
}
