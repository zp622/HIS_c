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
        public String JOB_NUMBER { get; set; }
        // 登录密码
        public String PASSWORD { get; set; }
        // 姓名
        public String NAME { get; set; }
        // 角色
        public String ROLE { get; set; }
        // 英文姓名
        public String NAME_EN { get; set; }
        // 登录标志
        public String LOGIN_FLAG { get; set; }
        // 创建者
        public String CREATOR { get; set; }
        // 创建时间
        public String CREATE_TIME { get; set; }
        // 更新者
        public String UPDATER { get; set; }
        // 更新时间
        public String UPDATE_TIME { get; set; }
        
        public UserModel() { }
    }
}