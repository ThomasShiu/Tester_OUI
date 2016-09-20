using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Tester_OperateDatabase
{
   public class TestSummary_Service
    {
       private DBUtility dbRMSHelper = new DBUtility("RMSConnection");

       public DataTable GetHardBinAndSoftBinTable(string deviceid, string mctype)
       {
           try
           {
               string strSql = "SELECT DEVICE_ID,HARD_BIN,SOFT_BIN FROM TESTER_HARDANDSOFTBINS WHERE DEVICE_ID='" + deviceid + "' ORDER BY HARD_BIN";
               DataTable dt = dbRMSHelper.ExecuteDataTable(strSql, null, CommandType.Text);

               DataTable dtResult = new DataTable("HardAndSoftBins");
               dtResult.Columns.Add("HARD_BIN", typeof(string));
               dtResult.Columns.Add("SOFT_BIN", typeof(string));

               for (int i = 0; i < dt.Rows.Count; )
               {
                   DataRow dr = dtResult.NewRow();
                   string hardBinNum = dt.Rows[i]["HARD_BIN"].ToString();
                   string softBinNum = "";
                   for (; i < dt.Rows.Count; )
                   {
                       if (hardBinNum == dt.Rows[i]["HARD_BIN"].ToString())
                       {
                           softBinNum += dt.Rows[i]["SOFT_BIN"].ToString() + ",";
                           i++;
                       }
                       else
                       {
                           break;
                       }
                   }

                   dr["HARD_BIN"] = hardBinNum;
                   dr["SOFT_BIN"] = softBinNum.TrimEnd(',');
                   dtResult.Rows.Add(dr);
               }

               return dtResult;
           }
           catch (Exception ex)
           {
               return null;
           }
       }

       public string UploadFile(byte[] buffer, string sRecipeName, string sMcNo, string sMcType, string sLotNo,
          string sRecipeStatus, string sEmpNo, string sReason, bool bDevicelevel)
       {
           try
           {
               string sPackage ="SPMTEST";

               string strSql = string.Empty;
               if (bDevicelevel)
               {
                   strSql = "PROC_UploadPRDeviceRecipe";
               }
               else
               {
                   strSql = "PROC_UploadDARecipe";
               }

               DbParameter[] parameters = new DbParameter[10];
               parameters[0] = new SqlParameter("@RecipeName", SqlDbType.VarChar);
               parameters[0].Value = sRecipeName;

               parameters[1] = new SqlParameter("@LotNumber", SqlDbType.VarChar);
               parameters[1].Value = sLotNo;

               parameters[2] = new SqlParameter("@McNO", SqlDbType.VarChar);
               parameters[2].Value = sMcNo;

               parameters[3] = new SqlParameter("@McType", SqlDbType.VarChar);
               parameters[3].Value = sMcType;

               parameters[4] = new SqlParameter("@Package", SqlDbType.VarChar);
               parameters[4].Value = sPackage;

               parameters[5] = new SqlParameter("@EmpNo", SqlDbType.VarChar);
               parameters[5].Value = sEmpNo;

               parameters[6] = new SqlParameter("@RecipeStatus", SqlDbType.VarChar);
               parameters[6].Value = sRecipeStatus;

               parameters[7] = new SqlParameter("@Data", SqlDbType.Image);
               parameters[7].Value = buffer;

               parameters[8] = new SqlParameter("@UploadDate", SqlDbType.DateTime);
               parameters[8].Value = DateTime.Now;

               parameters[9] = new SqlParameter("@UploadReason", SqlDbType.VarChar);
               parameters[9].Value = sReason;

               int iRet = dbRMSHelper.ExecuteNonQuery(strSql, parameters, CommandType.StoredProcedure);

               return iRet.ToString();
           }
           catch (Exception ex)
           {
               return ex.Message;
           }
       } 
   }
}
