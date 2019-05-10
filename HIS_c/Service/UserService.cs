using HIS_c.Dao;
using HIS_c.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Service
{
    
    public class UserService
    {
        private UserDao userDao = new UserDao();
        private ApiResult<List<UserModel>> apiResult = new ApiResult<List<UserModel>>();

        public ApiResult<List<UserModel>> userInfoGet(UserModel userInfo)
        {
            List<UserModel> user = new List<UserModel>();
            user = userDao.getUserInfo(userInfo);
            if (user != null && user.Count > 0)
            {
                apiResult.code = 200;
                apiResult.message = "登录成功";
                apiResult.data = user;
            }
            return apiResult;
        }

        public ApiResult<UserModel> exitLogin(string jobNumber)
        {
            ApiResult<UserModel> apiResult = new ApiResult<UserModel>();
            if (userDao.exitLogin(jobNumber) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "注销成功";
                apiResult.data = null;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "注销失败";
                apiResult.data = null;
            }
            return apiResult;
        }
    }
}