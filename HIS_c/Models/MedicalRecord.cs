using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    public class MedicalRecord
    {
        public string recordNo { get; set; }

        public string registerNo { get; set; }

        public string patientName { get; set; }

        public string patientNo { get; set; }

        public string hospital { get; set; }

        public string department { get; set; }

        public string visitTime { get; set; }

        public string doctor { get; set; }

        public string chiefAction { get; set; }

        public string presentIllness { get; set; }                              

        public string historyIllness { get; set; }

        public string phyExam { get; set; }

        public string tentDiag { get; set; }

        public string trpl { get; set; }

        public string auxiExam { get; set; }

        public string creator { get; set; }

        public string createTime { get; set; }

        public string updater { get; set; }

        public string updateTime { get; set; }
    }
}