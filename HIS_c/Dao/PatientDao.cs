using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using HIS_c.Models;
using HIS_c.Utils;

namespace HIS_c.Dao
{
    public class PatientDao
    {
        public int addPatient(Patient patient)
        {
            string sql = "insert into his.b_patient(card_type,card_id,name,sex,birthday,famous,phone,address,allergy_medicine,medical_record,creator)" +
                "values(:patient_no,:card_type,:card_id,:name,:sex,:birthday,:famous,:phone,:address,:allergy_medicine,:medical_record,:creator)";
            Random rd = new Random();
            string patientNo = DateTime.Now.ToString("yyyyMMddHHmmssms") + rd.Next().ToString();
            OracleParameter[] parameters =
            {
                new OracleParameter("card_type",patient.cardType),
                new OracleParameter("card_id",patient.cardId),
                new OracleParameter("name",patient.name),
                new OracleParameter("sex",patient.sex),
                new OracleParameter("birthday",patient.birthday),
                new OracleParameter("famous",patient.famous),
                new OracleParameter("phone",patient.phone),
                new OracleParameter("address",patient.address),
                new OracleParameter("allergy_medicine",patient.allergyMedicine),
                new OracleParameter("medical_record",patient.medicalRecord),
                new OracleParameter("creator",patient.creator),
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public List<Patient> getAll(Patient patient,int currentPage,int pageSize)
        {
            string front = "select * from (select a.*,rownum rn from (";
            string sql = "select t.patient_no,t.card_type,t.card_id,t.name,t.sex,t.birthday,t.phone,t.address,t.allergy_medicine,t.medical_record,t.creator,t.create_time,t.updater,t.update_time,t.famous from B_PATIENT t where 1=1";
            if (patient != null)
            {
                if (isNotBlank(patient.cardType))
                {
                    sql = sql + " and card_type = " + patient.cardType;
                }
                if (isNotBlank(patient.cardId))
                {
                    sql = sql + " and card_id = " + patient.cardId;
                }
                if (isNotBlank(patient.patientNo))
                {
                    sql = sql + " and patient_no = " + patient.patientNo;
                }
                if (isNotBlank(patient.name))
                {
                    sql = sql + " and name like '%" + patient.name + "%'";
                }
                if (isNotBlank(patient.phone))
                {
                    sql = sql + " and phone = " + patient.phone;
                }
            }
            string back = ") a where rownum<=:max) where rn>=:min";
            sql = front + sql + back;
            OracleParameter[] parameters =
            {
                new OracleParameter("min",pageSize*(currentPage-1)+1),
                new OracleParameter("max",pageSize*currentPage)
            };
            OracleDataReader reader = OracleHelper.ExecuteReader(sql,parameters);
            List<Patient> list = new List<Patient>();
            while (reader.Read())
            {
                Patient result = new Patient();
                result.patientNo = reader["patient_no"].ToString();
                result.cardType = reader["card_type"].ToString();
                result.cardId = reader["card_id"].ToString();
                result.name = reader["name"].ToString();
                result.sex = reader["sex"].ToString();
                result.birthday = reader["birthday"].ToString();
                result.phone = reader["phone"].ToString();
                result.address = reader["address"].ToString();
                result.allergyMedicine = reader["allergy_medicine"].ToString();
                result.medicalRecord = reader["medical_record"].ToString();
                result.creator = reader["creator"].ToString();
                result.createTime = reader["create_time"].ToString();
                result.updater = reader["updater"].ToString();
                result.updateTime = reader["update_time"].ToString();
                result.famous = reader["famous"].ToString();
                list.Add(patient);
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