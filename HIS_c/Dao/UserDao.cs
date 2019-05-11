﻿using HIS_c.Models;
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
            string sql = "update his.h_user t set t.user_status = '无效' where t.job_number = :jobNumber";
            OracleParameter[] parameters =
            {
                new OracleParameter("jobNumber",jobNumber)
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public int updUser(UserModel user)
        {
            string sql = "update his.h_user set ";
            if (isNotBlank(user.password))
            {
                sql = sql + "password = '" + MD5Encrypt32(user.password) + "',";
            }
            if (isNotBlank(user.userStatus))
            {
                sql = sql + "user_status = '" + user.userStatus + "',";
            }
            if (isNotBlank(user.name))
            {
                sql = sql + "name = '" + user.name + "',";
            }
            if(isNotBlank(user.role))
            {
                sql = sql + "role = '" + user.role + "',";
            }
            if (isNotBlank(user.nameEn))
            {
                sql = sql + "name_en = '" + user.nameEn + "',";
            }
            if (isNotBlank(user.updater))
            {
                sql = sql + "updater = '" + user.updater + "',";
            }
            sql = sql + "update_time = to_timestamp('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','yyyy-mm-dd hh24:mi:ss.ff')";
            if (isNotBlank(user.jobNumber))
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
                userModel.createTime = reader["create_time"].ToString();
                userModel.userStatus = reader["user_status"].ToString();
                list.Add(userModel);
            }
            return list;
        }

        public UserModel isExits(string jobNumber)
        {
            string sql = "select select t.JOB_NUMBER,t.NAME,t.ROLE,t.NAME_EN,t.LOGIN_FLAG,t.creator,t.create_time,t.UPDATER,t.UPDATE_TIME,t.USER_STATUS from H_USER t where t.job_number=:jobNumber";
            OracleParameter[] parameters =
            {
                new OracleParameter("jobNumber",jobNumber)
            };
            OracleDataReader reader = OracleHelper.ExecuteReader(sql, parameters);
            UserModel user = new UserModel();
            while (reader.Read())
            {
                user.jobNumber = reader["JOB_NUMBER"].ToString();
                user.name = reader["NAME"].ToString();
                user.role = reader["ROLE"].ToString();
                user.nameEn = reader["NAME_EN"].ToString();
                user.loginFlag = reader["LOGIN_FLAG"].ToString();
                user.updater = reader["UPDATER"].ToString();
                user.updaterTime = reader["UPDATE_TIME"].ToString();
                user.creator = reader["creator"].ToString();
                user.createTime = reader["create_time"].ToString();
                user.userStatus = reader["user_status"].ToString();
            }
            return user;
        }

        public List<UserModel> search(string jobNumber,string name,string role)
        {
            string sql = "select t.JOB_NUMBER,t.NAME,t.ROLE,t.NAME_EN,t.LOGIN_FLAG,t.creator,t.create_time,t.UPDATER,t.UPDATE_TIME,t.USER_STATUS from H_USER t where 1 = 1 ";
            if (isNotBlank(jobNumber)){
                sql = sql + "and t.job_number like '%" + jobNumber + "%' ";
            }
            if (isNotBlank(name))
            {
                sql = sql + "and t.name like '%" + name + "%' ";
            }
            if (isNotBlank(role))
            {
                sql = sql + "and t.role = '" + role + "'";
            }
            OracleDataReader reader = OracleHelper.ExecuteReader(sql);
            UserModel user = new UserModel();
            List<UserModel> list = new List<UserModel>();
            while (reader.Read())
            {
                user.jobNumber = reader["JOB_NUMBER"].ToString();
                user.name = reader["NAME"].ToString();
                user.role = reader["ROLE"].ToString();
                user.nameEn = reader["NAME_EN"].ToString();
                user.loginFlag = reader["LOGIN_FLAG"].ToString();
                user.updater = reader["UPDATER"].ToString();
                user.updaterTime = reader["UPDATE_TIME"].ToString();
                user.creator = reader["creator"].ToString();
                user.createTime = reader["create_time"].ToString();
                user.userStatus = reader["user_status"].ToString();
                list.Add(user);
            }
            return list;
        }

        public UserModel getUserByNum(string jobNumber)
        {
            String sql = "select t.JOB_NUMBER,t.PASSWORD,t.NAME,t.ROLE,t.NAME_EN,t.LOGIN_FLAG,t.UPDATER,t.UPDATE_TIME from H_USER t where t.JOB_NUMBER = :jobNumber";
            List<UserModel> list = new List<UserModel>();
            OracleParameter[] parameters = {
                new OracleParameter("jobNumber", jobNumber)
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
            if(list.Count>0){
                return list[0];
            }
            else
            {
                return null;
            }
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

        public static bool isNotBlank(string str)
        {
            if (str != null && str.Length != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}