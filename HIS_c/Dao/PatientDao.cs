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
            string sql = "insert into his.b_patient(patient_no,card_type,card_id,name,sex,birthday,famous,phone,address,allergy_medicine,medical_record,creator)" +
                "values(:patient_no,:card_type,:card_id,:name,:sex,:birthday,:famous,:phone,:address,:allergy_medicine,:medical_record,:creator)";
            Random rd = new Random();
            string patientNo = DateTime.Now.ToString("yyyyMMddHHmmssms") + rd.Next().ToString();
            OracleParameter[] parameters =
            {
                new OracleParameter("patient_no",patientNo),
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

        public List<Patient> getAll()
        {
            string sql = "select t.patient_no,t.card_type,t.card_id,t.name,t.sex,t.birthday,t.phone,t.address,t.allergy_medicine,t.medical_record,t.creator,t.create_time,t.updater,t.update_time,t.famous from B_PATIENT t";
            OracleDataReader reader = OracleHelper.ExecuteReader(sql);
            List<Patient> list = new List<Patient>();
            while (reader.Read())
            {
                Patient patient = new Patient();
                patient.patientNo = reader["patient_no"].ToString();
                patient.cardType = reader["card_type"].ToString();
                patient.cardId = reader["card_id"].ToString();
                patient.name = reader["name"].ToString();
                patient.sex = reader["sex"].ToString();
                patient.birthday = reader["birthday"].ToString();
                patient.phone = reader["phone"].ToString();
                patient.address = reader["address"].ToString();
                patient.allergyMedicine = reader["allergy_medicine"].ToString();
                patient.medicalRecord = reader["medical_record"].ToString();
                patient.creator = reader["creator"].ToString();
                patient.createTime = reader["create_time"].ToString();
                patient.updater = reader["updater"].ToString();
                patient.updateTime = reader["update_time"].ToString();
                patient.famous = reader["famous"].ToString();
                list.Add(patient);
            }
            return list;
        }
    }
}