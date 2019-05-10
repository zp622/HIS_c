using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    // 用户表
    public class UserModel
    {
        // 职工号(pk)
        public String jobNumber { get; set; }
        // 登录密码
        public String password { get; set; }
        // 姓名
        public String name { get; set; }
        // 角色
        public String role { get; set; }
        // 英文姓名
        public String nameEn { get; set; }
        // 登录标志
        public String loginFlag { get; set; }
        // 创建者
        public String creator { get; set; }
        // 创建时间
        public String createTame { get; set; }
        // 更新者
        public String updater { get; set; }
        // 更新时间
        public String updaterTime { get; set; }
        //用户状态
        public string userStatus { get; set; }
        
        public UserModel() { }
    }
}