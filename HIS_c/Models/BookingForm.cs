using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    public class BookingForm
    {
        public string registerNo { get; set; }

        public string patientNo { get; set; }

        public string registerTime { get; set; }

        public string registerFee { get; set; }

        public string registerType { get; set; }

        public string registerDept { get; set; }

        public string waitingNo { get; set; }

        public string creator { get; set; }

        public string createTime { get; set; }

        public string updater { get; set; }

        public string updateTime { get; set; }

        public string patientName { get; set; }

        public string doctor { get; set; }

        public string address { get; set; }

        public string status { get; set; }
    }
}