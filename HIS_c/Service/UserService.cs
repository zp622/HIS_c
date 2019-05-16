using HIS_c.Dao;
using HIS_c.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

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
            else
            {
                apiResult.code = 199;
                apiResult.message = "登录失败";
                apiResult.data = null;
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
            UserModel exitUser = userDao.getUserByNum(user.jobNumber);
            if (user != null)
            {
                List<UserModel> list = new List<UserModel>();
                list.Add(exitUser);
                apiResult.code = 199;
                apiResult.message = "职工号已存在";
                apiResult.data = list;
                return apiResult;
            }
            if (userDao.addUser(user) == 1)
            {
                apiResult.code = 200;
                apiResult.message = "添加成功";
                apiResult.data = userDao.getUser(null,1,10);
                apiResult.total = userDao.getUser(null, 1, 1000000).Count;
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
                apiResult.data = userDao.getUser(null,1,10);
                apiResult.total = userDao.getUser(null, 1, 1000000).Count;
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
                apiResult.data = userDao.getUser(null,1,10);
                apiResult.total = userDao.getUser(null, 1, 1000000).Count;
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

        public ApiResult<List<UserModel>> getAll(UserModel user, int currentPage, int pageSize)
        {
            apiResult.code = 200;
            apiResult.message = "查询成功";
            apiResult.data = userDao.getUser(user,currentPage,pageSize);
            apiResult.total = userDao.getUser(user, 1, 1000000).Count;
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

        public ApiResult<UserModel> updPwd(string jobNumber,string newPwd)
        {
            UserModel user = userDao.getUserByNum(jobNumber);
            ApiResult<UserModel> apiResult = new ApiResult<UserModel>();
            if (user != null)
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
                apiResult.message = "用户不存在";
                apiResult.data = null;
            }
            return apiResult;
        }

        public ApiResult<Boolean> validatePwd(string jobNumber, string oldPwd)
        {
            ApiResult<Boolean> apiResult = new ApiResult<Boolean>();
            UserModel user = userDao.getUserByNum(jobNumber);
            if (MD5Encrypt32(oldPwd).Equals(user.password))
            {
                apiResult.code = 200;
                apiResult.message = "旧密码验证通过";
                apiResult.data = true;
            }
            else
            {
                apiResult.code = 199;
                apiResult.message = "旧密码验证失败";
                apiResult.data = false;
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

        /// <summary>

        /// 发送邮件

        /// </summary>

        /// <param name="mailTo">要发送的邮箱</param>

        /// <param name="mailSubject">邮箱主题</param>

        /// <param name="mailContent">邮箱内容</param>

        /// <returns>返回发送邮箱的结果</returns>

        public ApiResult<Boolean> SendEmail(string mailTo)

        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = "smtp.qq.com"; //SMTP服务器
            string mailFrom = "1054632915@qq.com"; //登陆用户名
            string userPassword = "rifcbfzgcmnzbaif";//登陆密码
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            Random rd = new Random();
            string code = rd.Next(100000, 999999).ToString();
            Cache cache = new Cache();
            cache.Add(mailTo, code,null, DateTime.Now.AddSeconds(60*10),TimeSpan.Zero,CacheItemPriority.Normal,null);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码
            // 发送邮件设置       
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
            mailMessage.Subject = "验证码";//主题
            mailMessage.Body = "验证码"+ code +",10分钟内有效，如非本人操作请忽略。";//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级
            ApiResult<Boolean> apiResult = new ApiResult<Boolean>();
            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                apiResult.code = 200;
                apiResult.message = cache.Get(mailTo).ToString();
                apiResult.data = true;
            }
            catch (SmtpException ex)
            {
                apiResult.code = 199;
                apiResult.message = "邮件发送失败";
                apiResult.data = false;
            }
            return apiResult;
        }
    }
}