using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadME.Models
{
    public class OTP
    {
        public int Id { get; set; }

        
        public int UserId { get; set; }

        public long Otp { get; set; }

        public DateTime OtpTime { get; set; }
    }
}
