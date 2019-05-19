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
    public class DepartmentController : ApiController
    {
        private DepartmentService departmentService = new DepartmentService();

        [HttpPost]
        public ApiResult<List<Department>> addDept([FromBody]Department department)
        {
            return departmentService.addDept(department);
        }


        [HttpPost]
        public ApiResult<List<Department>> getDept([FromBody]JObject obj)
        {
            Department dept = obj["department"].ToObject<Department>();
            int currentPage = obj["currentPage"].ToObject<Int32>();
            int pageSize = obj["pageSize"].ToObject<Int32>();
            return departmentService.getDept(dept, currentPage, pageSize);
        }

        [HttpPost]
        public ApiResult<Int32> updDept([FromBody]Department department)
        {
            return departmentService.updDept(department);
        }

        /// <summary>
        /// 包含批量删除 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Int32> delDept([FromBody]List<Department> dept)
        {
            return departmentService.delDept(dept);
        }
    }
}
