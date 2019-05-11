using HIS_c.Models;
using HIS_c.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HIS_c.Dao
{
    public class MemberDao
    {
        public int addMember(Member member)
        {
            string sql = "insert into his.h_member(job_number,id,name,sex,famous,birthday,title_rank,career_experience,address,email,phone,work_date,work_term,degree,creator)" +
                "values(:job_number,:id,:name,:sex,:famous,:birthday,:title_rank,:career_experience,:address,:email,:phone,:work_date,:work_term,:degree,:creator)";
            string now = "to_date('" + System.DateTime.Now.ToString("yyyy-MM-dd HH24:mi:ss") + "','yyyy-MM-dd HH24:mi:ss')";
            OracleParameter[] parameters =
            {
                new OracleParameter("job_number",member.jobNumber),
                new OracleParameter("id",member.id),
                new OracleParameter("name",member.name),
                new OracleParameter("sex",member.sex),
                new OracleParameter("famous",member.famous),
                new OracleParameter("birthday",member.birthday),
                new OracleParameter("title_rank",member.titleRank),
                new OracleParameter("career_experience",member.careerExperince),
                new OracleParameter("address",member.address),
                new OracleParameter("email",member.email),
                new OracleParameter("phone",member.phone),
                new OracleParameter("work_date",member.workDate),
                new OracleParameter("work_term",member.workTerm),
                new OracleParameter("degree",member.degree),
                new OracleParameter("creator",member.creator),
                //new OracleParameter("create_time",now),
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public List<Member> getAll()
        {
            string sql = "select t.job_number,t.id,t.name,t.sex,t.famous,t.birthday,t.title_rank,t.career_experience,t.address,t.email,t.phone,t.work_date,t.work_term,t.degree,t.creator,t.create_time,t.updater,t.update_time from his.h_member t";
            List<Member> list = new List<Member>();
            
            OracleDataReader reader = OracleHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                Member member = new Member();
                member.jobNumber = reader["job_number"].ToString();
                member.id = reader["id"].ToString();
                member.name = reader["name"].ToString();
                member.sex = reader["sex"].ToString();
                member.famous = reader["famous"].ToString();
                member.birthday = reader["birthday"].ToString();
                member.titleRank = reader["title_rank"].ToString();
                member.careerExperince = reader["career_experience"].ToString();
                member.address = reader["address"].ToString();
                member.email = reader["email"].ToString();
                member.phone = reader["phone"].ToString();
                member.workDate = reader["work_date"].ToString();
                member.workTerm = reader["work_term"].ToString();
                member.degree = reader["degree"].ToString();
                member.creator = reader["creator"].ToString();
                if (reader["create_time"] != DBNull.Value)
                {
                    member.createTime = Convert.ToDateTime(reader["create_time"]);
                }
                member.updater = reader["updater"].ToString();
                if (reader["update_time"] != DBNull.Value)
                {
                    member.updateTime = Convert.ToDateTime(reader["update_time"]);
                }
                list.Add(member);
            }
            return list;
        }

        public int updMember(Member member)
        {
            string sql = "update his.h_member t set ";
            if (isNotBlank(member.id))
            {
                sql = sql + "id = '" + member.id + "',";
            }
            if (isNotBlank(member.name))
            {
                sql = sql + "name = '" + member.name + "',";
            }
            if (isNotBlank(member.sex))
            {
                sql = sql + "sex = '" + member.sex + "',";
            }
            if (isNotBlank(member.famous))
            {
                sql = sql + "famous = '" + member.famous + "',";
            }
            if (isNotBlank(member.birthday))
            {
                sql = sql + "birthday = '" + member.birthday + "',";
            }
            if (isNotBlank(member.titleRank))
            {
                sql = sql + "title_rank = '" + member.titleRank + "',";
            }
            if (isNotBlank(member.careerExperince))
            {
                sql = sql + "career_experience = '" + member.careerExperince + "',";
            }
            if (isNotBlank(member.address))
            {
                sql = sql + "address = '" + member.address + "',";
            }
            if (isNotBlank(member.email))
            {
                sql = sql + "email = '" + member.email + "',";
            }
            if (isNotBlank(member.phone))
            {
                sql = sql + "phone = '" + member.phone + "',";
            }
            if (isNotBlank(member.workDate))
            {
                sql = sql + "work_date = '" + member.workDate + "',";
            }
            if (isNotBlank(member.workTerm))
            {
                sql = sql + "work_term = '" + member.workTerm + "',";
            }
            if (isNotBlank(member.degree))
            {
                sql = sql + "degree = '" + member.degree + "',";
            }
            if (isNotBlank(member.updater))
            {
                sql = sql + "updater = '" + member.updater + "',";
            }
            sql = sql + "update_time = to_timestamp('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','yyyy-mm-dd hh24:mi:ss.ff')";
            if (isNotBlank(member.jobNumber))
            {
                sql = sql + "where job_number = " + member.jobNumber;
            }
            else
            {
                return -1;
            }
            return OracleHelper.ExecuteSql(sql);
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