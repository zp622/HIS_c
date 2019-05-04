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
    }
}