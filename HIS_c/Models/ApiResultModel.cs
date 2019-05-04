using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace HIS_c.Models
{
    [Serializable]
    public class ApiResultModel
    {
        //请求返回的状态码
        public HttpStatusCode Status { get; set; }
        //请求返回的数据
        public object Data { get; set; }
        //请求返回的message
        public string Message { get; set; }
    }
}