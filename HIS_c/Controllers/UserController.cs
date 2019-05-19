using HIS_c.Models;
using HIS_c.Service;
using HIS_c.Utils;
using Newtonsoft.Json.Linq;
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
    //    [EnableCors(origins: "http://localhost:8080", headers: "*",
    //methods: "*", SupportsCredentials = true)]
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
        [HttpPost]
        public ApiResult<UserModel> exitLogin([FromBody]UserModel user)
        {
            return userService.exitLogin(user.jobNumber);
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
        [HttpPost]
        public ApiResult<List<UserModel>> delUser([FromBody]List<UserModel> user)
        {
            return userService.delUser(user);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<UserModel>> updUser([FromBody]UserModel user)
        {
            return userService.updUser(user);
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<UserModel>> getAll([FromBody]JObject obj)
        {
            UserModel user = obj["user"].ToObject<UserModel>();
            int currentPage = obj["currentPage"].ToObject<Int32>();
            int pageSize = obj["pageSize"].ToObject<Int32>();
            return userService.getAll(user,currentPage,pageSize);
        }

        /// <summary>
        /// 判断职工号知否存在，如果已经存在返回已有职工的信息
        /// </summary>
        /// <param name="jobNumber"></param> 
        /// <returns></returns>
        [HttpPost]
        public ApiResult<UserModel> isExit([FromBody]UserModel user)
        {
            return userService.isExits(user.jobNumber);
        }

        /// <summary>
        /// 多字段模糊查询
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<UserModel>> search([FromBody]UserModel user)
        {
            return userService.search(user.jobNumber, user.name, user.role);
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="user">前台传参为jobNumber和password</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<UserModel> updPwd([FromBody]UserModel user)
        {
            return userService.updPwd(user.jobNumber,user.password);
        }

        /// <summary>
        /// 校验旧密码
        /// </summary>
        /// <param name="user">前台传参为jobNumber和password</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Boolean> validatePwd([FromBody]UserModel user)
        {
            return userService.validatePwd(user.jobNumber, user.password);
        }

        /// <summary>
        /// 发送邮箱验证码，成功返回六位验证码
        /// </summary>
        /// <param name="mailTo"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Boolean> sendEmail(string mailTo)
        {
            return userService.SendEmail(mailTo);
        }

    }
}