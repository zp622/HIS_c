using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HIS_c.Models;
using System.Data.OracleClient;
using HIS_c.Utils;

namespace HIS_c.Dao
{
    public class MedicalRecordDao
    {
        public int addRecord(MedicalRecord record)
        {
            string sql = "insert into his.B_MEDICAL_RECORD(record_no,register_no,patient_name,patient_no,hospital,department,doctor,chief_action,present_illness,history_illness,phy_exam,tent_diag,trpl,auxi_exam,creator)" +
                "values(:record_no,:register_no,:patient_name,:patient_no,:hospital,:department,:doctor,:chief_action,:present_illness,:history_illness,:phy_exam,:tent_diag,:trpl,:auxi_exam,:creator)";
            Random rd = new Random();
            string recordNo = DateTime.Now.ToString("yyyyMMddHHmmssms") + rd.Next().ToString();
            OracleParameter[] parameters =
            {
                new OracleParameter("record_no",recordNo),
                new OracleParameter("register_no",record.registerNo),
                new OracleParameter("patient_name",record.patientName),
                new OracleParameter("patient_no",record.patientNo),
                new OracleParameter("hospital",record.hospital),
                new OracleParameter("department",record.department),
                //new OracleParameter("visit_time",record.visitTime),
                new OracleParameter("doctor",record.doctor),
                new OracleParameter("chief_action",record.chiefAction),
                new OracleParameter("present_illness",record.presentIllness),
                new OracleParameter("history_illness",record.historyIllness),
                new OracleParameter("phy_exam",record.phyExam),
                new OracleParameter("tent_diag",record.tentDiag),
                new OracleParameter("trpl",record.trpl),
                new OracleParameter("auxi_exam",record.auxiExam),
                new OracleParameter("creator",record.creator),
            };
            return OracleHelper.ExecuteSql(sql, parameters);
        }

        public List<MedicalRecord> getAll()
        {
            string sql = "select t.record_no,t.register_no,t.patient_name,t.patient_no,t.hospital,t.department,t.visit_time,t.doctor,t.chief_action," +
                "t.present_illness,t.history_illness,t.phy_exam,t.tent_diag,t.trpl,t.auxi_exam,t.creator,t.create_time,t.updater,t.update_time from B_MEDICAL_RECORD t";
            OracleDataReader reader = OracleHelper.ExecuteReader(sql);
            List<MedicalRecord> list = new List<MedicalRecord>();
            while (reader.Read())
            {
                MedicalRecord record = new MedicalRecord();
                record.recordNo = reader["record_no"].ToString();
                record.registerNo = reader["register_no"].ToString();
                record.patientName = reader["patient_name"].ToString();
                record.patientNo = reader["patient_no"].ToString();
                record.hospital = reader["hospital"].ToString();
                record.department = reader["department"].ToString();
                record.visitTime = reader["visit_time"].ToString();
                record.doctor = reader["doctor"].ToString();
                record.chiefAction = reader["chief_action"].ToString();
                record.presentIllness = reader["present_illness"].ToString();
                record.historyIllness = reader["history_illness"].ToString();
                record.phyExam = reader["phy_exam"].ToString();
                record.tentDiag = reader["tent_diag"].ToString();
                record.trpl = reader["trpl"].ToString();
                record.auxiExam = reader["auxi_exam"].ToString();
                record.creator = reader["creator"].ToString();
                record.createTime = reader["create_time"].ToString();
                record.updater = reader["updater"].ToString();
                record.updateTime = reader["update_time"].ToString();
                list.Add(record);
            }
            return list;
        }
    }
}