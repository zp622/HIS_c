using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HIS_c.Models
{
    public class Member
    {
        //职工号
        public string jobNumber { get; set; }
        //身份证号
        public string id { get; set; }
        //姓名
        public string name { get; set; }
        //性别
        public string sex { get; set; }
        //民族
        public string famous { get; set; }
        //出生日期
        public string birthday { get; set; }
        //职称等级
        public string titleRank { get; set; }
        //从业经历
        public string careerExperince { get; set; }
        //住址
        public string address { get; set; }
        //邮箱
        public string email { get; set; }
        //手机号
        public string phone { get; set; }
        //工作日期
        public string workDate { get; set; }
        //从业年限
        public string workTerm { get; set; }
        //学历
        public string degree { get; set; }

        public string belongDept { get; set; }
        
        public string creator { get; set; }

        public DateTime createTime { get; set; }

        public string updater { get; set; }

        public DateTime updateTime { get; set; }
    }
}