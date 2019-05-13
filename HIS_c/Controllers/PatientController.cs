﻿using System;
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

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<Patient>> getAll(Patient patient, int currentPage, int pageSize)
        {
            return patientService.getAll(patient,currentPage,pageSize);
        }
    }
}
