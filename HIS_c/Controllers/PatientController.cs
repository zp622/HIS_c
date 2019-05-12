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
    public class PatientController : ApiController
    {
        private PatientService patientService = new PatientService();

        /// <summary>
        /// 添加病患信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<Patient>> addPatient(Patient patient)
        {
            return patientService.addPatient(patient);
        }

        [HttpPost]
        public ApiResult<List<Patient>> getAll()
        {
            return patientService.getAll();
        }
    }
}
