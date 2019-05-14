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
                apiResult.data = recordDao.getAll(null,1,10);
                apiResult.total = recordDao.getAll(null, 1, 1000000).Count;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
            }
            return apiResult;
        }

        public ApiResult<List<MedicalRecord>> getAll(MedicalRecord obj, int currentPage, int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = recordDao.getAll(obj,currentPage,pageSize);
            apiResult.total = recordDao.getAll(obj, 1, 1000000).Count;
            return apiResult;
        }
    }
}