﻿using HIS_c.Dao;
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

        private ApiResult<List<Patient>> apiResult = new ApiResult<List<Patient>>();

        public ApiResult<List<Patient>> addPatient(Patient patient)
        {
            
            if (patientDao.addPatient(patient) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = patientDao.getAll();
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
            }
            return apiResult;
        }

        public ApiResult<List<Patient>> getAll()
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = patientDao.getAll();
            return apiResult;
        }
    }
}