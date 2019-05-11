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

        public ApiResult<List<UserModel>> addUser(UserModel user)
        {
            if (userDao.addUser(user) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = userDao.getAllUser();
            }
            else{
                apiResult.code = 199;
                apiResult.message = "添加失败";
                apiResult.data = null;
            }

            return apiResult;
        }

        public ApiResult<List<UserModel>> delUser(string jobNumber)
        {
            if (userDao.delUser(jobNumber) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "删除成功";
                apiResult.data = userDao.getAllUser();
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "删除失败";
                apiResult.data = null;
            }

            return apiResult;
        }

        public ApiResult<List<UserModel>> updUser(UserModel user)
        {
            int i = userDao.updUser(user);
            if (i == 1)
            {
                apiResult.code = 200;
                apiResult.message = "修改成功";
                apiResult.data = userDao.getAllUser();
            }
            else if (i == -1)
            {
                apiResult.code = 198;
                apiResult.message = "职工号必传";
                apiResult.data = null;
            }
            return apiResult;
        }

        public ApiResult<List<UserModel>> getAll()
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = userDao.getAllUser();
            return apiResult;
        }

        public ApiResult<UserModel> isExits(string jobNumber)
        {
            UserModel user = userDao.isExits(jobNumber);
            ApiResult<UserModel> apiResult = new ApiResult<UserModel>();
            if (user == null)
            {
                apiResult.code = 200;
                apiResult.message = "职工号合法";
                apiResult.data = null;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "职工号已经存在";
                apiResult.data = user;
            }
            return apiResult;
        }
    }
}