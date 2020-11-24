using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace De_Identification_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeIdentificationController : ControllerBase
    {

        [HttpGet]
        public void GetDeIdentifiedUserInfo()
        {
            List<UserInfo> items = new List<UserInfo>();
            List<UserInfo> itemsDeIdentified = new List<UserInfo>();
            DeIdentification UserInfoValidation = new DeIdentification();
            using (StreamReader r = new StreamReader("Models/UserRecords.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<UserInfo>>(json);
                foreach (var item in items)
                {
                    string BirthDate = UserInfoValidation.DeIdentifyBirthDate(item.birthDate);
                    string ZipCode = UserInfoValidation.DeIdentifyZipCodes(item.zipCode);
                    string AdmissionDate = UserInfoValidation.DeIdentifyDate(item.admissionDate);
                    string DischargeDate = UserInfoValidation.DeIdentifyDate(item.dischargeDate);
                    string Notes = UserInfoValidation.DeIdentifyNotes(item.notes);
                    // parse here
                    itemsDeIdentified.Add(new UserInfo(BirthDate, ZipCode, AdmissionDate, DischargeDate, Notes));
                    string JSONresult = JsonConvert.SerializeObject(itemsDeIdentified);
                    string path = @"D:\DeIdentifiedUserRecords.json";
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        using (var tw = new StreamWriter(path, true))
                        {
                            tw.WriteLine(JSONresult.ToString());
                            tw.Close();
                        }
                    }
                    else if (!System.IO.File.Exists(path))
                    {
                        System.IO.File.Create(path);
                        using (var tw = new StreamWriter(path, true))
                        {
                            tw.WriteLine(JSONresult.ToString());
                            tw.Close();
                        }
                    }

                }
            }


        }

    }
}
