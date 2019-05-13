using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Models;
using System.Data.OracleClient;
using HIS_c.Utils;

namespace HIS_c.Dao
{
    public class DepartmentDao
    {
        public int addDepartment(Department department)
        {
            string sql = "insert into his.b_department(dept_no,dept_name,address,manager,phone,introduction,members,creator)" +
                "values(:dept_no,:dept_name,:address,:manager,:phone,:introduction,:members,:creator)";
            OracleParameter[] parameters =
            {
                new OracleParameter("dept_no",department.deptNo),
                new OracleParameter("dept_name",department.deptName),
                new OracleParameter("address",department.address),
                new OracleParameter("manager",department.manager),
                new OracleParameter("phone",department.phone),
                new OracleParameter("introduction",department.introduction),
                new OracleParameter("members",department.members),
                new OracleParameter("creator",department.creator),
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public List<Department> getDept(Department dept,int currentPage,int pageSize)
        {
            string front = "select * from (select a.*,rownum rn from (";
            string sql = "select t.dept_no,t.dept_name,t.address,t.manager,t.phone,t.introduction,t.members,t.creator,t.create_time,t.updater,t.update_time from his.b_department t where 1=1 ";
            if (dept != null)
            {
                if (isNotBlank(dept.deptName))
                {
                    sql = sql + " and dept_name = '" + dept.deptName + "'";
                }
                if (isNotBlank(dept.address))
                {
                    sql = sql + " and address = '%" + dept.address + "'%";
                }
            }
            string back = ") a where rownum<=:max) where rn>=:min";
            sql = front + sql + back;
            OracleParameter[] parameters =
            {
                new OracleParameter("min",pageSize*(currentPage-1)+1),
                new OracleParameter("max",pageSize*currentPage)
            };
            OracleDataReader reader = OracleHelper.ExecuteReader(sql, parameters);
            List<Department> list = new List<Department>();
            while (reader.Read())
            {
                Department department = new Department();
                department.deptNo = reader["dept_no"].ToString();
                department.deptName = reader["dept_name"].ToString();
                department.address = reader["address"].ToString();
                department.manager = reader["manager"].ToString();
                department.phone = reader["phone"].ToString();
                department.introduction = reader["introduction"].ToString();
                department.members = reader["members"].ToString();
                department.creator = reader["creator"].ToString();
                department.createTime = reader["create_time"].ToString();
                department.updater = reader["updater"].ToString();
                department.updateTime = reader["update_time"].ToString();
                list.Add(department);
            }
            return list;
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