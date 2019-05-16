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

        private ApiResult<List<Patient>> apiResult = new ApiResult<List<Patient>>();

        public ApiResult<List<Patient>> addPatient(Patient patient)
        {
            
            if (patientDao.addPatient(patient) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = patientDao.getAll(null,1,10);
                apiResult.total = patientDao.getAll(null, 1, 1000000).Count;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
            }
            return apiResult;
        }

        public ApiResult<List<Patient>> getAll(Patient patient, int currentPage, int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = patientDao.getAll(patient,currentPage,pageSize);
            apiResult.total = patientDao.getAll(patient, 1, 1000000).Count;
            return apiResult;
        }

        /// <summary>
        /// 修改患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public ApiResult<Int32> updPatient(Patient patient)
        {
            int i = patientDao.updPatient(patient);
            ApiResult<Int32> apiResult = new ApiResult<Int32>();
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "修改成功";
                apiResult.data = 1;
                apiResult.total = 1;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "修改失败";
            }
            return apiResult;
        }
    }
}