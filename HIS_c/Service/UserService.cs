using HIS_c.Dao;
using HIS_c.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            else
            {
                apiResult.code = 199;
                apiResult.message = "修改失败";
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

        public ApiResult<List<UserModel>> search(string jobNumber,string name,string role)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = userDao.search(jobNumber, name, role);
            return apiResult;
        }

        public ApiResult<UserModel> updPwd(string jobNumber,string oldPwd,string newPwd)
        {
            UserModel user = userDao.getUserByNum(jobNumber);
            ApiResult<UserModel> apiResult = new ApiResult<UserModel>();
            if (user != null)
            {
                if (MD5Encrypt32(oldPwd).Equals(user.password))
                {
                    UserModel newUser = new UserModel();
                    newUser.jobNumber = jobNumber;
                    newUser.password = MD5Encrypt32(newPwd);
                    if (userDao.updUser(newUser)==1)
                    {
                        apiResult.code = 200;
                        apiResult.message = "修改密码成功";
                        apiResult.data = userDao.getUserByNum(jobNumber);
                    }
                    else
                    {
                        apiResult.code = 199;
                        apiResult.message = "修改密码失败";
                        apiResult.data = null;
                    }
                }
                else
                {
                    apiResult.code = 199;
                    apiResult.message = "原密码错误";
                    apiResult.data = null;
                }
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "用户不存在";
                apiResult.data = null;
            }
            return apiResult;
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }
    }
}