using HIS_c.Models;
using HIS_c.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HIS_c.Dao
{
    public class UserDao
    {
        public List<UserModel> getUserInfo(UserModel userInfo)
        {
            string MD5_PWD = MD5Encrypt32(userInfo.password);
            String sql = "select t.JOB_NUMBER,t.PASSWORD,t.NAME,t.ROLE,t.NAME_EN,t.LOGIN_FLAG,t.UPDATER,t.UPDATE_TIME from H_USER t where t.JOB_NUMBER = :jobNumber and t.PASSWORD = :password";
            List<UserModel> list = new List<UserModel>();
            OracleParameter[] parameters = {
                new OracleParameter("jobNumber", userInfo.jobNumber),
                new OracleParameter("password", MD5_PWD)
            };
            OracleDataReader reader = OracleHelper.ExecuteReader(sql, parameters);
            while (reader.Read())
            {
                UserModel userModel = new UserModel();
                userModel.jobNumber = reader["JOB_NUMBER"].ToString();
                userModel.name = reader["NAME"].ToString();
                userModel.role = reader["ROLE"].ToString();
                userModel.nameEn = reader["NAME_EN"].ToString();
                userModel.loginFlag = reader["LOGIN_FLAG"].ToString();
                userModel.updater = reader["UPDATER"].ToString();
                userModel.updaterTime = reader["UPDATE_TIME"].ToString();
                list.Add(userModel);
            }
            if (list != null && list.Count > 0)
            {
                string updateSql = "update H_USER set LOGIN_FLAG = 'Y' where JOB_NUMBER = :jobNumber";
                OracleParameter[] updateParameters = {
                    new OracleParameter("jobNumber", userInfo.jobNumber)
                };
                int result = OracleHelper.ExecuteSql(updateSql, updateParameters);
                if(result != 1)
                {
                    return null;
                }
                else
                {
                    var newlist = list.Where(o => o.jobNumber == userInfo.jobNumber).ToList();
                    foreach (var user in newlist)
                    {
                        user.loginFlag = "Y";
                    }
                }
            }
            reader.Close();
            return list;
        }

        public int exitLogin(string jobNumber)
        {
            string sql = "update h_user set login_flag = :loginFlag where job_number = :jobNumber";
            OracleParameter[] parameters = {
                new OracleParameter("jobNumber", jobNumber),
                new OracleParameter("loginFlag", "N")
            };
            return OracleHelper.ExecuteSql(sql, parameters);
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