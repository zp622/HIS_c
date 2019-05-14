using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    public class ApiResult<T>
    {
        public int code { get; set; }

        public string message { get; set; }

        public T data { get; set; }

        public int total { get; set; }
    }
}