using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.Api.Core.Domain
{
    public class Patient
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string BloodGroup { get; set; } //O−	O+	A−	A+	B−	B+	AB−	AB+ h/h
    }
}
