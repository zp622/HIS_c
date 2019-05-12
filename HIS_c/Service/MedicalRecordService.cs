using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Models;
using HIS_c.Dao;

namespace HIS_c.Service
{
    public class MedicalRecordService
    {
        private MedicalRecordDao recordDao = new MedicalRecordDao();

        private ApiResult<List<MedicalRecord>> apiResult = new ApiResult<List<MedicalRecord>>();

        public ApiResult<List<MedicalRecord>> addRecord(MedicalRecord record)
        {
            
            if (recordDao.addRecord(record) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = recordDao.getAll();
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
            }
            return apiResult;
        }

        public ApiResult<List<MedicalRecord>> getAll()
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = recordDao.getAll();
            return apiResult;
        }
    }
}