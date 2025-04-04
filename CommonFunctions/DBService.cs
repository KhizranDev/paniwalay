using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace WebTech.Common
{
    enum ServiceType
    {
        ClassLevel,
        Asp_Net,
        WCF
    }



    public static class DBService
    {
        #region Public Methods and Functions

        
        public static void ExecuteQuery(string strQuery, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            layer.ExecuteQuery(strQuery, ref strError);
        }


        

        public static void ExecuteSP(string SPName, SqlParameter sqlprms, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            layer.ExecuteSP(SPName, sqlprms, ref strError);
        }

        public static bool ExecuteSP(string SPName, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.ExecuteSP(SPName, sqlprms, ref strError);
        }

        public static void ExecuteSP(string SPName, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            layer.ExecuteSP(SPName, ref strError);
        }

        public static DataTable SubmitPOSOrder(string SPName, SqlParameter[] sqlprms, XmlDocument docOutlets, XmlDocument docItems, XmlDocument docAttributes, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.SubmitPOSOrder(SPName, sqlprms, docOutlets, docItems, docAttributes, ref strError);
        }


        public static void ExecuteSPNonQuery(string SPName, SqlParameter sqlprms, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            layer.ExecuteSPNonQuery(SPName, sqlprms, ref strError);
        }

        public static void ExecuteSPNonQuery(string SPName, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            layer.ExecuteSPNonQuery(SPName, ref strError);
        }

        

        public static DataTable FetchTable(string strQuery)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchTable(strQuery);
        }

        public static DataTable FetchTable(string strQuery, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchTable(strQuery, ref strError);
        }

        public static DataTable FetchTable(string strQuery, SqlConnection conn, SqlTransaction trans, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchTable(strQuery, conn, trans, ref strError);
        }

        public static DataTable FetchTable(string SPName, SqlParameter[] sqlprms)
        {
            DBLayer layer = new DBLayer();
            DataTableCollection collection = FetchFromSP(SPName, sqlprms);
            if (collection != null)
            {
                return collection[0];
            }
            else
            {
                return null;
            }
        }

        public static DataTable FetchTableSchema(string strQuery)
        {
            ErrorResponse strError = new ErrorResponse();
            return FetchTableSchema(strQuery, ref strError);
        }

        public static DataTable FetchTableSchema(string strQuery, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchTableSchema(strQuery, ref strError);
        }

        public static DataTable FetchFromSP(string SPName, SqlParameter sqlprms, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchFromSP(SPName, sqlprms, ref strError);
        }

        public static DataTableCollection FetchFromSP(string SPName, SqlParameter[] sqlprms)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchFromSP(SPName, sqlprms);
        }

        public static DataTableCollection FetchFromSP(string SPName, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchFromSP(SPName, sqlprms, ref strError);
        }

        public static DataTable FetchTableFromSP(string SPName, SqlParameter[] sqlprms)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchTableFromSP(SPName, sqlprms);
        }


        public static DataSet FetchRecordsDS(string strQuery)
        {
            ErrorResponse strError = new ErrorResponse();
            return FetchRecordsDS(strQuery, ref strError);
        }

        public static DataSet FetchRecordsDS(string strQuery, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchRecordsDS(strQuery, ref strError);
        }


        public static DataSet FetchRecordsDS(string StoredProcedure, SqlParameter[] sqlprms)
        {
            ErrorResponse strError = new ErrorResponse();
            return FetchRecordsDS(StoredProcedure, sqlprms, ref strError);
        }

        public static DataSet FetchRecordsDS(string StoredProcedure, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.FetchRecordsDS(StoredProcedure, sqlprms, ref strError);
        }


        public static string FinalWhereClause { get; set; }

        
        public static string DLookupDB(string SQL)
        {
            ErrorResponse response = new ErrorResponse();
            return DLookupDB(SQL, ref response);
        }

        public static string DLookupDB(string SQL, ref ErrorResponse strError)
        {
            DBLayer layer = new DBLayer();
            return layer.DLookupDB(SQL, ref strError);
        }

        public static string DLookupDB(string StoredProcedure, SqlParameter[] param)
        {
            DBLayer layer = new DBLayer();
            return layer.DLookupDB(StoredProcedure, param);
        }

        public static string GetApplicationSetting(string strKey)
        {
            DBLayer layer = new DBLayer();
            return layer.GetApplicationSetting(strKey);
        }


        
        #endregion Public Methods and Functions


    }




    
}
