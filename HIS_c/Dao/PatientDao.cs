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
    }
}