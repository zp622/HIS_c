﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Dao;
using HIS_c.Models;

namespace HIS_c.Service
{
    public class DepartmentService
    {
        private DepartmentDao departmentDao = new DepartmentDao();

        private ApiResult<List<Department>> apiResult = new ApiResult<List<Department>>();

        public ApiResult<List<Department>> addDept(Department department)
        {
            if (departmentDao.addDepartment(department)==1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = departmentDao.getDept(null, 1, 10);
                apiResult.total = departmentDao.getDept(null, 1, 1000000).Count;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "添加失败";
                apiResult.data = null;
            }
            return apiResult;
        }

        public ApiResult<List<Department>> getDept(Department dept,int currentPage,int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = departmentDao.getDept(dept, currentPage, pageSize);
            apiResult.total = departmentDao.getDept(dept, 1, 1000000).Count;
            return apiResult;
        }

        /// <summary>
        /// 修改科室信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ApiResult<Int32> updDept(Department dept)
        {
            int i = departmentDao.updDept(dept);
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

        public ApiResult<Int32> delDept(List<Department> dept)
        {
            int i = departmentDao.delDept(dept);
            ApiResult<Int32> apiResult = new ApiResult<Int32>();
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "删除成功";
                apiResult.data = 1;
                apiResult.total = 1;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "删除失败";
            }
            return apiResult;
        }
    }
}