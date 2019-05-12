using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HIS_c.Service;
using HIS_c.Models;

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

        [HttpPost]
        public ApiResult<List<MedicalRecord>> getAll()
        {
            return recordService.getAll();
        }
    }
}
