using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HIS_c.Service;
using HIS_c.Models;
using Newtonsoft.Json.Linq;

namespace HIS_c.Controllers
{
    public class MedicalRecordController : ApiController
    {
        private MedicalRecordService recordService = new MedicalRecordService();

        [HttpPost]
        public ApiResult<List<MedicalRecord>> addRecord(MedicalRecord record)
        {
            return recordService.addRecord(record);
        }

        /// <summary>
        /// 获取病历信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<MedicalRecord>> getRecord([FromBody]JObject obj)
        {
            MedicalRecord record = obj["medicalRecord"].ToObject<MedicalRecord>();
            int currentPage = obj["currentPage"].ToObject<Int32>();
            int pageSize = obj["pageSize"].ToObject<Int32>();
            return recordService.getAll(record,currentPage,pageSize);
        }
    }
}
