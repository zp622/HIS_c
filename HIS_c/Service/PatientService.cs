using HIS_c.Dao;
using HIS_c.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Service
{
    public class PatientService
    {
        private PatientDao patientDao = new PatientDao();

        public ApiResult<Patient> addPatient(Patient patient)
        {
            ApiResult<Patient> apiResult = new ApiResult<Patient>();
            if (patientDao.addPatient(patient) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
            }
            return apiResult;
        }
    }
}