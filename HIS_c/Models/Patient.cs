using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    public class Patient
    {
        public string patientNo { get; set; }

        public string cardType { get; set; }

        public string cardId { get; set; }

        public string name { get; set; }

        public string sex { get; set; }

        public string birthday { get; set; }

        public string phone { get; set; }

        public string address { get; set; }

        public string allergyMedicine { get; set; }

        public string medicalRecord { get; set; }

        public string creator { get; set; }

        public string createTime { get; set; }

        public string updater { get; set; }

        public string updateTime { get; set; }

        public string famous { get; set; }
    }
}