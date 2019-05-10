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

        public int addUser(UserModel user)
        {
            string sql = "insert into his.h_user(job_number,password,name,role,name_en,login_flag,creator,user_status)" +
                "values(:job_number,:password,:name,:role,:name_en,:login_flag,:creator,:user_status)";
            OracleParameter[] parameters =
            {
                new OracleParameter("job_number",user.jobNumber),
                new OracleParameter("password",MD5Encrypt32(user.password)),
                new OracleParameter("name",user.name),
                new OracleParameter("role",user.role),
                new OracleParameter("name_en",user.nameEn),
                new OracleParameter("login_flag","N"),
                new OracleParameter("creator",user.creator),
                new OracleParameter("user_status","有效"),
            };
            return OracleHelper.ExecuteSql(sql,parameters);
        }

        public int delUser(string jobNumber)
        {
            string sql = "delete from his.h_user t where t.job_number = :jobNumber";
            OracleParameter[] parameters =
            {
                new OracleParameter("jobNumber",jobNumber)
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public int updUser(UserModel user)
        {
            string sql = "update his.h_user set ";
            if (user.password != null && user.password.Length != 0)
            {
                sql = sql + "password = '" + MD5Encrypt32(user.password) + "',";
            }
            if (user.userStatus != null && user.userStatus.Length != 0)
            {
                sql = sql + "user_status = '" + user.userStatus + "',";
            }
            if (user.name != null && user.name.Length != 0)
            {
                sql = sql + "name = '" + user.name + "',";
            }
            if(user.role!=null && user.role.Length != 0)
            {
                sql = sql + "role = '" + user.role + "',";
            }
            if (user.nameEn != null && user.nameEn.Length != 0)
            {
                sql = sql + "name_en = '" + user.nameEn + "',";
            }
            if (user.updater != null && user.updater.Length != 0)
            {
                sql = sql + "updater = '" + user.updater + "',";
            }
            sql = sql + "update_time = to_timestamp('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','yyyy-mm-dd hh24:mi:ss.ff')";
            if (user.jobNumber != null && user.jobNumber.Length != 0)
            {
                sql = sql + "where job_number = " + user.jobNumber;
            }
            else
            {
                return -1;
            }
            return OracleHelper.ExecuteSql(sql);
        }

        public List<UserModel> getAllUser()
        {
            string sql = "select t.JOB_NUMBER,t.NAME,t.ROLE,t.NAME_EN,t.LOGIN_FLAG,t.creator,t.create_time,t.UPDATER,t.UPDATE_TIME,t.USER_STATUS from H_USER t";
            OracleDataReader reader = OracleHelper.ExecuteReader(sql);
            List<UserModel> list = new List<UserModel>();
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
                userModel.creator = reader["creator"].ToString();
                userModel.createTame = reader["create_time"].ToString();
                userModel.userStatus = reader["user_status"].ToString();
                list.Add(userModel);
            }
            return list;
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