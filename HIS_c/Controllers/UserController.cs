using HIS_c.Models;
using HIS_c.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HIS_c.Controllers
{
    public class UserController : ApiController
    {
        private UserService userService = new UserService();
        // Post api/User/
        [EnableCors(origins: "http://localhost:8080", headers: "*",
    methods: "*", SupportsCredentials = true)]
        [HttpPost]
        public ApiResult<List<UserModel>> userLogin([FromBody]UserModel userInfo)
        {
            return userService.userInfoGet(userInfo);
        }

        [HttpPost]
        public ApiResult<List<String>> login([FromBody]string userInfo)
        {
            return null;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="jobNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<UserModel> exitLogin(string jobNumber)
        {
            return userService.exitLogin(jobNumber);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<UserModel>> addUser([FromBody]UserModel user)
        {
            return userService.addUser(user);
        }

        /// <summary>
        /// 根据职工号删除用户
        /// </summary>
        /// <param name="jobNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<UserModel>> delUser(string jobNumber)
        {
            return userService.delUser(jobNumber);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<UserModel>> updUser(UserModel user)
        {
            return userService.updUser(user);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<UserModel>> getAll()
        {
            return userService.getAll();
        }

        /// <summary>
        /// 判断职工号知否存在，如果已经存在返回已有职工的信息
        /// </summary>
        /// <param name="jobNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<UserModel> isExit(string jobNumber)
        {
            return userService.isExits(jobNumber);
        }

        /// <summary>
        /// 多字段模糊查询
        /// </summary>
        /// <param name="jobNumber">职工号</param>
        /// <param name="name">姓名</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<UserModel>> search(string jobNumber,string name,string role)
        {
            return userService.search(jobNumber, name, role);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="jobNumber"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<UserModel> updPwd(string jobNumber,string oldPwd,string newPwd)
        {
            return userService.updPwd(jobNumber, oldPwd, newPwd);
        }
    }
}