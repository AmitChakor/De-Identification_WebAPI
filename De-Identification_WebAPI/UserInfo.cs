using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace De_Identification_WebAPI
{
    public class UserInfo
    {
        public string birthDate;
        public string zipCode;
        public string admissionDate;
        public string dischargeDate;
        public string notes;

        public UserInfo(string birthDate, string zipCode, string admissionDate, string dischargeDate, string notes)
        {
            this.birthDate = birthDate;
            this.zipCode = zipCode;
            this.admissionDate = admissionDate;
            this.dischargeDate = dischargeDate;
            this.notes = notes;
        }

    }
}
