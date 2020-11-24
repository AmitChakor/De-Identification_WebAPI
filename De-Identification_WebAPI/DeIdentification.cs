using System;
using System.Text;
using System.Text.RegularExpressions;

namespace De_Identification_WebAPI
{
    public class DeIdentification
    {

        public string _Reg_Email_Pattern = @"(?<=[\w]{1})[\w-\._\+%\\]*(?=[\w]{1}@)|(?<=@[\w]{1})[\w-_\+%]*(?=\.)";

        /// <summary>
        /// function to get only the year part for the provided dates.
        /// </summary>
        public string DeIdentifyDate(string date)
        {
            DateTime dt = Convert.ToDateTime(date);
            return dt.Year.ToString();
        }

        /// <summary>
        /// function to calculate age from the provided date and round of any age above 89 years with "90+".
        /// </summary>
        public string DeIdentifyBirthDate(string date)
        {
            DateTime dateOfBirth = Convert.ToDateTime(date);

            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            var age = (a - b) / 10000;
            string Finalage = "";
            if(age > 89)
            {
                Finalage = "90+";
            }
            else
            {
                Finalage = age.ToString();
            }

            return Finalage;
        }

        /// <summary>
        /// Function to Replaces ssn no or digits from provided string with "x".
        /// </summary>
        public string DeIdentifyNotes(string Notes)
        {
            string deIdentifiedNotes = Regex.Replace(Notes, @"\d", "x");
            deIdentifiedNotes = MaskEmail(deIdentifiedNotes);

            
            return deIdentifiedNotes;
        }
             

        /// <summary>
        // Funcction to mask Email Addresses in the provided string
        /// <summary>
        public string MaskEmail(string s)
        {
         
            if (s.Split('@')[0].Length < 4)
                return @"*@*.*";
            return Regex.Replace(s, _Reg_Email_Pattern, m => new string('*', m.Length));
        }

        /// <summary>
        //Function to strip the zip codes to first 3 digits and replace the rest all with "0"
        /// <summary>
        public string DeIdentifyZipCodes(string ZipCode)
        {            
            string newExtenssionString = new string('0', ZipCode.Length - 3);
            string deIdentifiedZipCode = ZipCode.Substring(0, 3)+ newExtenssionString;
            return deIdentifiedZipCode;
        }
     
       
    }
}
