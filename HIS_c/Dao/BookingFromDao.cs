﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Models;
using HIS_c.Utils;
using System.Data.OracleClient;
using System.Text;
using System.Speech.Synthesis;

namespace HIS_c.Dao
{
    public class BookingFromDao
    {
        public int addBookingForm(BookingForm form)
        {
            string sql = "insert into his.b_bookingform(patient_no,register_time,register_fee,register_type,register_dept,waiting_no,creator,patient_name,doctor,address)" +
                "values(:patient_no,:register_time,:register_fee,:register_type,:register_dept,:waiting_no,:creator,:patient_name,:doctor,:address)";
            OracleParameter[] parameters =
            {
                new OracleParameter("patient_no",form.patientNo),
                new OracleParameter("register_time",form.registerTime),
                new OracleParameter("register_fee",form.registerFee),
                new OracleParameter("register_type",form.registerType),
                new OracleParameter("register_dept",form.registerDept),
                new OracleParameter("waiting_no",form.waitingNo),
                new OracleParameter("creator",form.creator!=null?form.creator:""),
                new OracleParameter("patient_name",form.patientName),
                new OracleParameter("doctor",form.doctor),
                new OracleParameter("address",form.address),
            };
            List<BookingForm> list = new List<BookingForm>();
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public List<BookingForm> getBookingForms(BookingForm form, int currentPage, int pageSize)
        {
            string front = "select * from (select a.*,rownum rn from (";
            string sql = "select t.register_no,t.patient_no,t.register_time,t.register_fee,t.register_type,t.register_dept,t.waiting_no,t.creator," +
                "t.updater,t.create_time,t.update_time,t.patient_name,t.doctor,t.address,t.status from his.b_bookingform t where 1=1 ";
            if (form != null)
            {
                if (isNotBlank(form.registerNo))
                {
                    sql = sql + " and t.register_no = '" + form.registerNo + "'";
                }
                if (isNotBlank(form.registerTime))
                {
                    sql = sql + " and t.register_time = '" + form.registerTime + "'";
                }
                if (isNotBlank(form.status))
                {
                    sql = sql + " and t.status = '" + form.status + "'";
                }
                if (isNotBlank(form.doctor))
                {
                    sql = sql + " and t.doctor = '" + form.doctor + "'";
                }
                if (isNotBlank(form.patientNo))
                {
                    sql = sql + " and t.patient_no = '" + form.patientNo + "'";
                }
                if (isNotBlank(form.registerDept))
                {
                    sql = sql + " and t.register_dept = '" + form.registerDept + "'";
                }
                if (isNotBlank(form.registerType))
                {
                    sql = sql + " and t.register_type = '" + form.registerType + "'";
                }
                if (isNotBlank(form.waitingNo))
                {
                    sql = sql + " and t.waiting_no = '" + form.waitingNo + "'";
                }
            }
            sql = sql + " order by register_no asc";
            string back = ") a where rownum<=:max) where rn>=:min";
            sql = front + sql + back;
            OracleParameter[] parameters =
            {
                new OracleParameter("min",pageSize*(currentPage-1)+1),
                new OracleParameter("max",pageSize*currentPage)
            };
            OracleDataReader reader = OracleHelper.ExecuteReader(sql, parameters);
            List<BookingForm> list = new List<BookingForm>();
            while (reader.Read())
            {
                BookingForm bookingForm = new BookingForm();
                bookingForm.registerNo = reader["register_no"].ToString();
                bookingForm.patientNo = reader["patient_no"].ToString();
                bookingForm.registerTime = reader["register_time"].ToString();
                bookingForm.registerFee = reader["register_fee"].ToString();
                bookingForm.registerType = reader["register_type"].ToString();
                bookingForm.registerDept = reader["register_dept"].ToString();
                bookingForm.waitingNo = reader["waiting_no"].ToString();
                bookingForm.creator = reader["creator"].ToString();
                bookingForm.createTime = reader["create_time"].ToString();
                bookingForm.updater = reader["updater"].ToString();
                bookingForm.updateTime = reader["update_time"].ToString();
                bookingForm.patientName = reader["patient_name"].ToString();
                bookingForm.doctor = reader["doctor"].ToString();
                bookingForm.address = reader["address"].ToString();
                bookingForm.status = reader["status"].ToString();
                list.Add(bookingForm);
            }
            return list;
        }

        public int getCount(BookingForm form)
        {
            string sql = "select * from his.b_bookingform t where t.register_dept = :register_dept and t.doctor = :doctor and t.register_time like '%"+form.registerTime+"%'";
            OracleParameter[] parameters =
            {
                new OracleParameter("register_dept",form.registerDept),
                new OracleParameter("doctor",form.doctor),
            };
            OracleDataReader reader = OracleHelper.ExecuteReader(sql, parameters);
            List<BookingForm> list = new List<BookingForm>();
            while (reader.Read())
            {
                BookingForm bookingForm = new BookingForm();
                bookingForm.registerNo = reader["register_no"].ToString();
                list.Add(bookingForm);
            }
            return list.Count;
        }

        /// <summary>
        /// @autor zp
        /// 获取已挂号数量
        /// </summary>
        /// <param name="str">科室 医生 日期 挂号类别</param>
        /// <returns></returns>
        public int getHasCount(BookingForm form)
        {
            StringBuilder sql = new StringBuilder("select t.* from his.b_bookingform t where 1=1");
            if (isNotBlank(form.registerDept))
            {
                sql.Append(" and t.register_dept = '" + form.registerDept + "'");
            }
            if (isNotBlank(form.doctor))
            {
                sql.Append(" and t.doctor = '" + form.doctor + "'");
            }
            if (isNotBlank(form.registerTime))
            {
                sql.Append(" and t.register_time = '" + form.registerTime + "'");
            }
            if (isNotBlank(form.registerType))
            {
                sql.Append(" and t.register_type = '" + form.registerType + "'");
            }
            List<BookingForm> list = OracleHelper.ExecuteToList<BookingForm>(sql.ToString());
            int count = 0;
            if (list==null)
            {
                return count;
            }
            else
            {
                return list.Count; 
            }
        }

        public int updBookingForm(BookingForm bookingForm)
        {
            string sql = "update his.b_bookingform set status = :status, updater = :updater where register_no = :register_no";
            OracleParameter[] parameters =
            {
                new OracleParameter("status",bookingForm.status),
                new OracleParameter("register_no",bookingForm.registerNo),
                new OracleParameter("updater",bookingForm.updater)
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public int ttsFun(string msg)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            try
            {
                // Configure the audio output.   
                synth.SetOutputToDefaultAudioDevice();
                synth.Speak(msg);
                return 1;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
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