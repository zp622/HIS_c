using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    public class MedicalRecord
    {
        public string record_no { get; set; }
        public string register_no { get; set; }
        public string patient_name { get; set; }
        public string patient_no { get; set; }
        public string hospital { get; set; }
        public string department { get; set; }
        public string visit_time { get; set; }
        public string doctor { get; set; }
        public string chief_action { get; set; }
        public string present_illness { get; set; }
        public string history_illness { get; set; }
        public string phy_exam { get; set; }
        public string tent_diag { get; set; }
        public string trpl { get; set; }
        public string auxi_exam { get; set; }
        public string creator { get; set; }
        public string create_time { get; set; }
        public string updater { get; set; }
        public string update_time { get; set; }
    }
}