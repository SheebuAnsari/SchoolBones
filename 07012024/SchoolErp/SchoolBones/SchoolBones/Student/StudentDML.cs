using DomLibrary;
using SchoolConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolBones.Student
{
    public class StudentDML
    {
        SaveStatus obj = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        SqlConnection oCon = null;


        public SaveStatus SaveStudentDetails(string strQuery, SqlParameter[] param)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                obj = new SaveStatus();
                cmd = new SqlCommand(strQuery, oCon);
                foreach (var item in param)
                    cmd.Parameters.Add(item);
                obj.iStatus = cmd.ExecuteNonQuery();
                return obj;
            }
        }

        //public List<StudentDetails> LoadStudentDetailsDML(string txt)
        //{
        //    List<StudentDetails> lstStudentDetails = new List<StudentDetails>();

        //    StudentDetails oStudentDetails = null;

        //    cmd = new SqlCommand();
        //    cmd.CommandText = txt;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.Connection = ConnectionHelper.ConOpen();

        //    reader = cmd.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            oStudentDetails = new StudentDetails();
        //            oStudentDetails.StdId = Convert.ToInt32(reader["iStdId"]);// reader.GetInt64(0);
        //            oStudentDetails.Mobile = Convert.ToInt64(reader["bIntMobile"]);
        //            oStudentDetails.Email = Convert.ToString(reader["sEmail"]);//reader.GetSqlValue(2).ToString();
        //            oStudentDetails.Password = Convert.ToString(reader["sPassword"]);
        //            oStudentDetails.StdName = Convert.ToString(reader["sStdName"]);
        //            oStudentDetails.AdmissionDate = Convert.ToString(reader["sAdmissionDate"]);
        //            oStudentDetails.Address = Convert.ToString(reader["sAddress"]);
        //            oStudentDetails.AdmissionInClass = Convert.ToInt32(reader["iAdmissionInClass"]);
        //            oStudentDetails.DOB = Convert.ToString(reader["DOB"]);

        //            lstStudentDetails.Add(oStudentDetails);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No rows found.");
        //    }
        //    reader.Close();
        //    return lstStudentDetails;
        //}
        public List<StudentCurrentInfo> LoadStudentForMarksDML(string strQry)
        {
            List<StudentCurrentInfo> lstStudentCurrentInfo = new List<StudentCurrentInfo>();

            StudentCurrentInfo oStudentCurrentInfo = null;
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    oStudentCurrentInfo = new StudentCurrentInfo();
                    oStudentCurrentInfo.StdId = Convert.ToInt32(reader["iStdId"]);// reader.GetInt64(0);
                    oStudentCurrentInfo.StdName = Convert.ToString(reader["sLastName"]);
                    oStudentCurrentInfo.Class = Convert.ToInt32(reader["iCurrentClass"]);
                    oStudentCurrentInfo.Year = Convert.ToInt32(reader["iCurrentYear"]);
                    lstStudentCurrentInfo.Add(oStudentCurrentInfo);
                }
                reader.Close();
                return lstStudentCurrentInfo;
            }
        }

        public bool CheckMarkSheetDML(string strQry)
        {
            using (oCon = ConnectionHelper.Stablish)
            {
                cmd = new SqlCommand(strQry, oCon);
                return Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

    }
}
