using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Xml;
using System.Data.SqlTypes;


namespace WebTech.Common
{

    public class DBLayer
    {
        string connString = "";
        bool UseIdKey = true;

        public DBLayer()
        {
            connString = GetApplicationSetting(ConfigParameters.SQLConnection.ToString());
            //string temp = WTEncryption.Encrypt("Password=system; Persist Security Info = True; User ID=sa; Initial Catalog=db_paniwala; Data Source=(local)");
        }

        #region Public Methods and Functions

        // ExecuteQuery2
        public void ExecuteQuery(string strQuery, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(strQuery, SqlCon))
                        {
                            cmd.Transaction = SqlTrans;
                            cmd.ExecuteNonQuery(); 
                        }

                        SqlTrans.Commit();
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteQuery2(" + strQuery + ")", "SQL", sqle.Message);
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteQuery2(" + strQuery + ")", "General", ae.Message);
                    } 
                } 
            }
        }

        // ExecuteQuery4
        public bool ExecuteQuery(string strQuery, SqlConnection conn, SqlTransaction trans, ref ErrorResponse strError)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(strQuery, conn))
                {
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (SqlException sqle)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "ExecuteQuery2(" + strQuery + ")", "SQL", sqle.Message);

                return false;
            }
            catch (Exception ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "ExecuteQuery2(" + strQuery + ")", "General", ae.Message);

                return false;
            }
        }

        // ExecuteQuery3
        public void ExecuteQuery(List<lQueries> queries, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        foreach (lQueries item in queries)
                        {
                            using (SqlCommand cmd = new SqlCommand(item.Query, SqlCon))
                            {
                                cmd.Transaction = SqlTrans;
                                cmd.ExecuteNonQuery(); 
                            }
                        }

                        SqlTrans.Commit();
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteQuery3(" + queries.ToArray().ToString() + ")", "SQL", sqle.Message);
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteQuery3(" + queries.ToArray().ToString() + ")", "General", ae.Message);
                    } 
                } 
            }
        }

        // ExecuteSP
        public void ExecuteSP(string SPName, SqlParameter sqlprms, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(sqlprms);
                            cmd.Transaction = SqlTrans;
                            cmd.ExecuteNonQuery();
                        }

                        SqlTrans.Commit();
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP(" + SPName + "," + getspstring(sqlprms) + ")", "SQL", sqle.Message);
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP(" + SPName + "," + getspstring(sqlprms) + ")", "General", ae.Message);
                    }  
                }
            }


        }

        // ExecuteSP1
        public void ExecuteSP(string SPName, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Transaction = SqlTrans;
                            cmd.ExecuteNonQuery();
                        }

                        SqlTrans.Commit();
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP1(" + SPName + ")", "SQL", sqle.Message);
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP1(" + SPName + ")", "General", ae.Message);
                    } 
                } 
            }
        }

        // ExecuteSP2
        public bool ExecuteSP(string SPName, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (sqlprms != null)
                            {
                                foreach (SqlParameter param in sqlprms)
                                    cmd.Parameters.Add(param);
                            }

                            cmd.Transaction = SqlTrans;
                            cmd.ExecuteNonQuery();
                        }

                        SqlTrans.Commit();

                        return true;
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP(" + SPName + "," + getspsstring(sqlprms) + ")", "SQL", sqle.Message);

                        return false;
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP(" + SPName + "," + getspsstring(sqlprms) + ")", "General", ae.Message);

                        return false;
                    }  
                }
            }
        }


        // ExecuteSP
        public DataTable SubmitPOSOrder(string SPName, SqlParameter[] sqlprms, XmlDocument docOutlets, XmlDocument docItems, XmlDocument docAttributes, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();

                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(SPName, SqlCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = SqlTrans;

                        foreach (SqlParameter param in sqlprms)
                            cmd.Parameters.Add(param);


                        cmd.Parameters.Add(new SqlParameter("@Outlets", SqlDbType.Xml)
                        {
                            Value = new SqlXml(new XmlTextReader(docOutlets.InnerXml, XmlNodeType.Document, null))
                        });

                        cmd.Parameters.Add(new SqlParameter("@Items", SqlDbType.Xml)
                        {
                            Value = new SqlXml(new XmlTextReader(docItems.InnerXml, XmlNodeType.Document, null))
                        });


                        cmd.Parameters.Add(new SqlParameter("@Attributes", SqlDbType.Xml)
                        {
                            Value = new SqlXml(new XmlTextReader(docAttributes.InnerXml, XmlNodeType.Document, null))
                        });


                        DataTable dt = new DataTable();
                        SqlDataAdapter sdapt = new SqlDataAdapter(cmd);
                        sdapt.Fill(dt);

                        SqlTrans.Commit();

                        dt.TableName = "OrderTable";
                        return dt;
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP(" + SPName + "," + getspsstring(sqlprms) + ")", "SQL", sqle.Message);

                        return null;
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSP(" + SPName + "," + getspsstring(sqlprms) + ")", "General", ae.Message);

                        return null;
                    }
                }
            }


        }


        // ExecuteSPNonQuery
        public void ExecuteSPNonQuery(string SPName, SqlParameter sqlprms, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(sqlprms);
                            cmd.Transaction = SqlTrans;
                            cmd.ExecuteNonQuery(); 
                        }

                        SqlTrans.Commit();
                    }
                    catch (SqlException sqle)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = sqle.ErrorCode, Message = sqle.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSPNonQuery(" + SPName + "," + getspstring(sqlprms) + ")", "SQL", sqle.Message);
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSPNonQuery(" + SPName + "," + getspstring(sqlprms) + ")", "SQL", ae.Message);
                    } 
                } 
            }


        }

        // ExecuteSPNonQuery1
        public void ExecuteSPNonQuery(string SPName, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery(); 
                        }

                        SqlTrans.Commit();
                    }
                    catch (SqlException ae)
                    {
                        SqlTrans.Rollback();

                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSPNonQuery1(" + SPName + ")", "SQL", ae.Message);
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();

                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "ExecuteSPNonQuery1(" + SPName + ")", "General", ae.Message);
                    } 
                } 
            }
        }

        // FetchTable
        public DataTable FetchTable(string strQuery)
        {
            ErrorResponse _response = new ErrorResponse();
            return FetchTable(strQuery, ref _response);
        }

        // FetchTable1
        public DataTable FetchTable(string strQuery, ref ErrorResponse strError)
        {
            if (strQuery == "") return null;

            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                try
                {
                    using (SqlDataAdapter sdapt = new SqlDataAdapter(strQuery, SqlCon))
                    {
                        DataTable dt = new DataTable("Table1");
                        sdapt.Fill(dt);

                        return dt;
                    }
                }
                catch (SqlException ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchTable1(" + strQuery + ")", "SQL", ae.Message);

                    return null;
                }
                catch (Exception ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchTable1(" + strQuery + ")", "General", ae.Message);

                    return null;
                } 
            }
        }

        // FetchTable2
        public DataTable FetchTable(string strQuery, SqlConnection conn, SqlTransaction trans, ref ErrorResponse strError)
        {
            try
            {
                using (SqlCommand command = new SqlCommand(strQuery, conn))
                {
                    command.Transaction = trans;
                    SqlDataAdapter sdapt = new SqlDataAdapter(command);
                    DataTable dt = new DataTable("Table1");
                    sdapt.Fill(dt);
                    return dt; 
                }

            }
            catch (SqlException ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "FetchTable1(" + strQuery + ")", "SQL", ae.Message);

                return null;
            }
            catch (Exception ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "FetchTable1(" + strQuery + ")", "General", ae.Message);

                return null;
            }
        }

        // FetchTableSchema
        public DataTable FetchTableSchema(string strQuery, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                try
                {
                    using (SqlDataAdapter sdapt = new SqlDataAdapter(strQuery, SqlCon))
                    {
                        DataTable dt = new DataTable("Table1");
                        dt = sdapt.FillSchema(dt, SchemaType.Source);

                        return dt;
                    }
                }
                catch (SqlException ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchTableSchema(" + strQuery + ")", "SQL", ae.Message);

                    return null;
                }
                catch (Exception ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchTableSchema(" + strQuery + ")", "General", ae.Message);

                    return null;
                } 
            }
        }

        // FetchTableDB
        public DataTable FetchTableDB(string strQuery, ref ErrorResponse strError, SqlConnection conn, SqlTransaction trans)
        {
            try
            {
                DataTable dt = new DataTable("Table1");
                SqlCommand cmd = new SqlCommand(strQuery, conn);
                cmd.Transaction = trans;
                SqlDataAdapter sdapt = new SqlDataAdapter(cmd);
                sdapt.Fill(dt);
                return dt;
            }
            catch (SqlException ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "FetchTableDB(" + strQuery + ")", "SQL", ae.Message);
                return null;
            }
            catch (Exception ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "FetchTableDB(" + strQuery + ")", "General", ae.Message);
                return null;
            }
        }

        // FetchFromSP
        public DataTable FetchFromSP(string SPName, SqlParameter sqlprms, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(sqlprms);
                            cmd.Transaction = SqlTrans;
                            SqlDataAdapter sdapt = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable("Table1");
                            sdapt.Fill(dt);

                            SqlTrans.Commit();
                            return dt;
                        }

                    }
                    catch (SqlException ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "FetchFromSP(" + SPName + "," + getspstring(sqlprms) + ")", "SQL", ae.Message);

                        return null;
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();

                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "FetchFromSP(" + SPName + "," + getspstring(sqlprms) + ")", "General", ae.Message);

                        return null;
                    } 
                } 
            }
        }


        // FetchFromSP1
        public DataTableCollection FetchFromSP(string SPName, SqlParameter[] sqlprms)
        {
            ErrorResponse _response = new ErrorResponse();
            return FetchFromSP(SPName, sqlprms, ref _response);
        }

        // FetchFromSP2
        public DataTableCollection FetchFromSP(string SPName, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        DataSet ds = new DataSet("DataSet1");
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Transaction = SqlTrans;
                            if (sqlprms != null)
                            {
                                foreach (SqlParameter sqlp in sqlprms)
                                    cmd.Parameters.Add(sqlp);
                            }

                            using (SqlDataAdapter sdapt = new SqlDataAdapter(cmd))
                            {
                                sdapt.Fill(ds);
                            }
                        }

                        SqlTrans.Commit();
                        return ds.Tables;
                    }
                    catch (SqlException ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "FetchFromSP2(" + SPName + "," + getspsstring(sqlprms) + ")", "SQL", ae.Message);

                        return null;
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();
                        strError.Error = true;
                        strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                        Logs.WriteError("DBLayer", "FetchFromSP2(" + SPName + "," + getspsstring(sqlprms) + ")", "General", ae.Message);
                        return null;
                    }  
                }
            }
        }

        // FetchTableFromSP
        public DataTable FetchTableFromSP(string SPName, SqlParameter[] sqlprms)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                using (SqlTransaction SqlTrans = SqlCon.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SPName, SqlCon, SqlTrans))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (sqlprms != null)
                            {
                                foreach (SqlParameter param in sqlprms)
                                    cmd.Parameters.Add(param);
                            }
                            DataTable dt = new DataTable("Table1");
                            using (SqlDataAdapter sdapt = new SqlDataAdapter(cmd))
                            {
                                sdapt.Fill(dt);
                            }

                            SqlTrans.Commit();

                            return dt;
                        }
                    }
                    catch (SqlException ae)
                    {
                        SqlTrans.Rollback();

                        Logs.WriteError("DBLayer", "FetchFromSP3(" + SPName + "," + getspsstring(sqlprms) + ")", "SQL", ae.Message);
                        return null;
                    }
                    catch (Exception ae)
                    {
                        SqlTrans.Rollback();

                        Logs.WriteError("DBLayer", "FetchFromSP(" + SPName + "," + getspsstring(sqlprms) + ")", "General", ae.Message);
                        return null;
                    } 
                } 
            }
        }

        


        // FetchRecordsDS
        public DataSet FetchRecordsDS(string strQuery, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                try
                {
                    Regex reg = new Regex("~@~");
                    string[] queries = reg.Split(strQuery);

                    DataSet ds = new DataSet();

                    int i = 0;

                    foreach (string qry in queries)
                    {
                        using (SqlDataAdapter sdapt = new SqlDataAdapter(qry, SqlCon))
                        {
                            using (DataTable dt = new DataTable("Table1"))
                            {
                                sdapt.Fill(dt);
                                ds.Tables.Add(dt);
                            }
                        }
                        i++;
                    }
                    return ds;
                }
                catch (SqlException ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchRecordsDS(" + strQuery + ")", "SQL", ae.Message);

                    return null;
                }
                catch (Exception ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchRecordsDS(" + strQuery + ")", "General", ae.Message);

                    return null;
                } 
            }
        }

        // FetchRecordsDS
        public DataSet FetchRecordsDS(string StoredProcedure, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                try
                {
                    DataSet ds = new DataSet();
                    using (SqlCommand cmd = SqlCon.CreateCommand())
                    {
                        cmd.CommandText = StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (sqlprms != null)
                        {
                            foreach (SqlParameter param in sqlprms)
                                cmd.Parameters.Add(param);
                        }


                        SqlDataAdapter sdapt = new SqlDataAdapter(cmd);
                        sdapt.Fill(ds);
                    }

                    return ds;
                }
                catch (SqlException ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchRecordsDS(" + StoredProcedure + ")", "SQL", ae.Message);

                    return null;
                }
                catch (Exception ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "FetchRecordsDS(" + StoredProcedure + ")", "General", ae.Message);

                    return null;
                }
            }
        }


        // FinalWhereClause
        public string FinalWhereClause { get; set; }

        // DLookupDB
        public string DLookupDB(string SQL, ref ErrorResponse strError)
        {
            string Val = string.Empty;
            DataTable dt = new DataTable("Table1");
            dt = FetchTable(SQL, ref strError);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Val = row[0].ToString();
                }
            return Val;
        }

        public string DLookupDB(string StoredProcedure, SqlParameter[] sqlprms)
        {
            ErrorResponse strError = new ErrorResponse();
            return DLookupDB(StoredProcedure, sqlprms, ref strError);
        }

        public string DLookupDB(string StoredProcedure, SqlParameter[] sqlprms, ref ErrorResponse strError)
        {
            string Val = string.Empty;

            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                try
                {
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = SqlCon.CreateCommand())
                    {
                        cmd.CommandText = StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (sqlprms != null)
                        {
                            foreach (SqlParameter param in sqlprms)
                                cmd.Parameters.Add(param);
                        }


                        SqlDataAdapter sdapt = new SqlDataAdapter(cmd);
                        sdapt.Fill(dt);
                    }

                    if (DataHelper.HasRows(dt))
                    {
                        Val = dt.Rows[0][0].ToString();
                    }
                }
                catch (SqlException ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "DLookupDB(" + StoredProcedure + ")", "SQL", ae.Message);

                    return null;
                }
                catch (Exception ae)
                {
                    strError.Error = true;
                    strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                    Logs.WriteError("DBLayer", "DLookupDB(" + StoredProcedure + ")", "General", ae.Message);

                    return null;
                }
            }

            return Val;
        }

        // DLookupDB1
        public string DLookupDB(string SQL, ref ErrorResponse strError, SqlConnection conn, SqlTransaction SqlTrans)
        {
            string Val = string.Empty;
            DataTable dt = new DataTable("Table1");
            dt = FetchTableDB(SQL, ref strError, conn, SqlTrans);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Val = row[0].ToString();
                }
            return Val;
        }

        // DLookupDB2
        public string DLookupDB(string StoredProcedure, SqlParameter[] param, SqlConnection conn, SqlTransaction trans)
        {
            ErrorResponse _response = new ErrorResponse();
            return DLookupDB(StoredProcedure, param, conn, trans, ref _response);
        }

        // DLookupDB3
        public string DLookupDB(string StoredProcedure, SqlParameter[] param, SqlConnection conn, SqlTransaction trans, ref ErrorResponse strError)
        {
            try
            {
                SqlCommand command = new SqlCommand(StoredProcedure, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = trans;

                if (param != null)
                {
                    foreach (SqlParameter p in param)
                        command.Parameters.Add(p);
                }

                return (string)command.ExecuteScalar();
            }
            catch (SqlException ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "DBLookupDB3(" + StoredProcedure + "," + getspsstring(param) + ")", "SQL", ae.Message);

                return "";
            }
            catch (Exception ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "DBLookupDB3(" + StoredProcedure + "," + getspsstring(param) + ")", "General", ae.Message);

                return "";
            }
        }

        // DLookupDB4
        public string DLookupDB(string SQL, SqlConnection conn, SqlTransaction trans, ref ErrorResponse strError)
        {
            string Val = string.Empty;
            DataTable dt = new DataTable("Table1");
            dt = FetchTable(SQL, conn, trans, ref strError);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Val = row[0].ToString();
                }
            return Val;
        }

        // GetApplicationSetting
        public string GetApplicationSetting(string strKey)
        {
            string val = string.Empty;
            try
            {
                string key = WTEncryption.Encrypt(strKey);

                if (ConfigurationManager.ConnectionStrings[key] != null)
                    val = System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
                else if (ConfigurationManager.AppSettings[key] != null)
                    val = ConfigurationManager.AppSettings[key];
                
            }
            catch (Exception ae)
            {
                Logs.WriteError("DBLayer", "GetApplicationSetting(" + strKey + ")", "General", ae.Message);

                throw new Exception("Connection String not found");
            }

            return WTEncryption.Decrypt(val);
        }

        private static string FieldPattern(DataTable dtblMaster, string Qry)
        {
            string strqry = Qry;

            string pattern = ":";
            IList<int> indeces = new List<int>();
            foreach (Match match in Regex.Matches(strqry, pattern))
            {
                indeces.Add(match.Index);
            }

            for (int i = 0; i < indeces.Count; i++)
            {
                string fieldName = strqry.Substring(indeces[i] + 1, indeces[i + 1] - indeces[i] - 1);
                if (dtblMaster.Columns[fieldName] != null)
                {
                    string fieldValue = DataHelper.stringParse(dtblMaster.Rows[0][fieldName]);
                    strqry = strqry.Replace(":" + fieldName + ":", "'" + fieldValue + "'");
                }
                i++;
            }
            return strqry;
        }

        private string GenerateTransactionNo(ref ErrorResponse strError, int SystemId, long OptionId, DateTime SystemDate, string Branch, string TransactionStoredProcedure, DataTable dtblMaster, SqlConnection SqlConn, SqlTransaction SqlTrans)
        {
            List<SqlParameter> param = new List<SqlParameter>();

            if (TransactionStoredProcedure == "TransactionNo")
            {
                param.Add(new SqlParameter() { ParameterName = "@SystemId", Value = SystemId });
                param.Add(new SqlParameter() { ParameterName = "@OptionId", Value = OptionId });
                param.Add(new SqlParameter() { ParameterName = "@SystemDate", Value = SystemDate });
                param.Add(new SqlParameter() { ParameterName = "@BranchCode", Value = Branch });
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement doc = xmldoc.CreateElement("doc");
                xmldoc.AppendChild(doc);

                XmlElement item = xmldoc.CreateElement("item");
                doc.AppendChild(item);

                foreach (DataColumn column in dtblMaster.Columns)
                {
                    item.AppendChild(xmldoc.CreateElement(column.ColumnName)).InnerText = DataHelper.stringParse(dtblMaster.Rows[0][column]).Replace("'", "''");
                }

                param.Add(new SqlParameter() { ParameterName = "@Row", Value = doc.OuterXml });
            }



            return DLookupDB(TransactionStoredProcedure, param.ToArray(), SqlConn, SqlTrans, ref strError);
        }


        private string GetWhereCriteria(DataRow row, string masterTableName, string[] PKs)
        {
            string strVal = "";

            foreach (string col in PKs)
            {
                strVal = strVal + " AND " + masterTableName + "." + col + "=" + GetFormattedValue(row, row.Table.Columns[col]);
            }

            if (strVal.Length > 5)
                return strVal.Substring(5);
            else
                return strVal;
            //this.PK + "','" + tmpMaster.Rows[0][this.PK].ToString();

        }

        private string GetWhereCriteriaDeletedRow(DataRow row, string masterTableName, string[] PKs)
        {
            string strVal = "";

            foreach (string col in PKs)
            {
                strVal = strVal + " AND " + masterTableName + "." + col + "=" + GetFormattedValue(row, row.Table.Columns[col], DataRowVersion.Original);
            }

            if (strVal.Length > 5)
                return strVal.Substring(5);
            else
                return strVal;
            //this.PK + "','" + tmpMaster.Rows[0][this.PK].ToString();

        }


        // TableHasStatusField
        public bool TableHasStatusField(string strTableName)
        {
            using (SqlConnection SqlCon = new SqlConnection(connString))
            {
                SqlCon.Open();
                try
                {
                    string qry = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + strTableName + "' AND COLUMN_NAME='Status'";
                    SqlDataAdapter sdapt = new SqlDataAdapter(qry, SqlCon);
                    DataTable dt = new DataTable("Table1");
                    sdapt.Fill(dt);

                    return dt.Rows.Count > 0;

                }
                catch (SqlException ae)
                {
                    Logs.WriteError("DBLayer", "TableHasStatusField(TableName: " + strTableName + ")", "SQL", ae.Message);

                    return false;
                }
                catch (Exception ae)
                {
                    Logs.WriteError("DBLayer", "TableHasStatusField(TableName: " + strTableName + ")", "General", ae.Message);

                    return false;
                } 
            }
        }

        // ExecuteQueryDB3
        public bool ExecuteSP(string StoredProcedure, SqlParameter[] param, SqlConnection conn, SqlTransaction trans, ref ErrorResponse strError)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(StoredProcedure, conn, trans))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (param != null)
                    {
                        foreach (SqlParameter prm in param)
                            cmd.Parameters.Add(prm);
                    }

                    cmd.ExecuteNonQuery();

                    return true; 
                }
            }
            catch (SqlException ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = ae.ErrorCode, Message = ae.Message, ErrorType = "SQL", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "ExecuteQueryDB3(" + StoredProcedure + ")", "SQL", ae.Message);

                return false;
            }
            catch (Exception ae)
            {
                strError.Error = true;
                strError.ErrorList.Add(new ErrorList() { ErrorCode = 999, Message = ae.Message, ErrorType = "General", WarningLevel = Level.Error });

                Logs.WriteError("DBLayer", "ExecuteQueryDB3(" + StoredProcedure + ")", "General", ae.Message);

                return false;
            }
        }

        #endregion Public Methods and Functions


        #region Private Methods and Functions

        // GetQuote
        private string GetQuote(string Type)
        {
            string rt = "";
            switch (Type)
            {
                case "System.String":
                    rt = "'";
                    break;

                case "System.DateTime":
                    rt = "'";
                    break;
            }
            return rt;
        }


        // GetFormattedValue
        private string GetFormattedValue(DataRow row, DataColumn col)
        {
            string strVal = "";
            switch (col.DataType.Name)
            {
                case "Boolean":
                    if ((bool)row[col] == true)
                        strVal = @"1";
                    else
                        strVal = @"0";
                    break;
                case "Byte":
                    strVal = "True";
                    break;
                case "DateTime":
                case "TimeSpan":
                    if (string.IsNullOrEmpty(row[col].ToString()))
                        strVal = "'01-jan-1900'";
                    else
                        strVal = "'" + ((DateTime)row[col]).ToString("dd-MMM-yyyy hh:mm:ss") + "'";
                    break;
                case "Single":
                    strVal = row[col].ToString();
                    break;
                case "Decimal":
                case "Double":
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    strVal = row[col].ToString();
                    if (strVal == "")
                        strVal = "0";
                    break;
                case "SByte":
                    strVal = row[col].ToString();
                    break;
                case "String":
                case "Char":
                    strVal = "'" + row[col].ToString().Replace("'","''") + "'";
                    break;
            }
            return strVal;
        }

        // GetFormattedValue1
        private string GetFormattedValue(DataRow row, DataColumn col, DataRowVersion rowVersion)
        {
            string strVal = "";
            switch (col.DataType.Name)
            {
                case "Boolean":
                    if ((bool)row[col] == true)
                        strVal = @"1";
                    else
                        strVal = @"0";
                    break;
                case "Byte":
                    strVal = "True";
                    break;
                case "DateTime":
                case "TimeSpan":
                    //if (string.IsNullOrEmpty(row[col].ToString()))
                    if (string.IsNullOrEmpty(row[col, rowVersion].ToString()))
                        strVal = "'01-jan-1900'";
                    else
                        strVal = "'" + ((DateTime)row[col, rowVersion]).ToString("dd-MMM-yyyy hh:mm:ss") + "'";
                    break;
                case "Single":
                    strVal = row[col, rowVersion].ToString();
                    break;
                case "Decimal":
                case "Double":
                case "Int16":
                case "Int32":
                case "Int64":
                case "UInt16":
                case "UInt32":
                case "UInt64":
                    strVal = row[col, rowVersion].ToString();
                    if (strVal == "")
                        strVal = "0";
                    break;
                case "SByte":
                    strVal = row[col, rowVersion].ToString();
                    break;
                case "String":
                case "Char":
                    strVal = "'" + row[col, rowVersion].ToString() + "'";
                    break;
            }
            return strVal;
        }

        // setMasterFieldsintoDetail
        private void setMasterFieldsintoDetail(DataTable dtbDetail, DataTable dtblMaster, List<MasterDetailRelation> MDRelation, long NewTrailNo)
        {
            try
            {
                if (dtbDetail != null && dtblMaster != null)
                {
                    foreach (DataColumn column in dtbDetail.Columns)
                    {
                        if (MDRelation != null)
                        {
                            var result = MDRelation.Where(o => o.DetailFld.ToUpper() == column.ColumnName.ToUpper()).FirstOrDefault();
                            if (result != null)
                            {
                                foreach (DataRow row in dtbDetail.Rows)
                                {
                                    if (dtbDetail.Columns[result.DetailFld] != null)
                                        row[column] = dtblMaster.Rows[0][result.MasterFld].ToString();
                                }
                            }
                            else if (column.ColumnName == "TrailNo")
                            {
                                foreach (DataRow row in dtbDetail.Rows)
                                {
                                    row[column] = NewTrailNo;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ae)
            {
                Logs.WriteError("DBLayer", "SetMasterFieldsintoDetail()", "General", ae.Message);
            }
        }

        

        // GetPks
        private List<string> GetPks(string TableName)
        {
            List<string> list = new List<string>();

            list.Add("TrailNo");

            if (this.UseIdKey)
            {
                list.Add("Id");
            }
            else
            {
                string SQL = "execute sp_pkeys '" + TableName + "'";
                ErrorResponse strError = new ErrorResponse();
                DataTable dtb = FetchTable(SQL, ref strError);

                foreach (DataRow row in dtb.Rows)
                {
                    list.Add(row["COLUMN_NAME"].ToString());
                }
            }

            return list;
        }

        // GetPksWOTrail
        private string GetPksWOTrail(string TableName, DataRow row)
        {
            List<string> lst = GetPksWOTrail(TableName);
            StringBuilder strPk = new StringBuilder();
            string strVal = "";
            string strPks = "";

            foreach (string item in lst)
            {
                strVal = GetFormattedValue(row, row.Table.Columns[item]);
                strPks += " AND " + item + "=" + strVal;
            }

            return strPks;
        }

        // GetPksWOTrail1
        private List<string> GetPksWOTrail(string TableName)
        {
            List<string> lst = new List<string>();
            if (this.UseIdKey)
            {
                lst.Add("Id");
            }
            else
            {
                string SQL = "execute sp_pkeys '" + TableName + "'";
                ErrorResponse strError = new ErrorResponse();
                DataTable dtb = FetchTable(SQL, ref strError);
                foreach (DataRow row in dtb.Rows)
                    if (row["COLUMN_NAME"].ToString() != "TrailNo")
                        lst.Add(row["COLUMN_NAME"].ToString());

                //var i = from mine in dtb.AsEnumerable() select new { COLUMN_NAME = mine.Field<string>("COLUMN_NAME") };
                //lst = (List<string>)i.AsEnumerable();
            }

            return lst;
        }

        // GetQueries
        private List<string> GetQueries(lQueries[] Qries, QueryExecutionState QryState)
        {
            List<string> myList = new List<string>();

            if (Qries != null)
            {
                foreach (lQueries item in Qries)
                {
                    if (item.ExecutionState == QryState)
                    {
                        myList.Add(item.Query);
                    }
                }
            }

            return myList;
        }

        // getspstring
        private string getspstring(SqlParameter sqlprms)
        {
            string str = string.Empty;
            if (sqlprms != null)
            {
                str += "ParameterName: " + sqlprms.ParameterName + ", ParameterValue: " + sqlprms.Value;
            }

            return str;
        }

        // getspsstring
        private string getspsstring(SqlParameter[] sqlprms)
        {
            string str = string.Empty;
            if (sqlprms != null)
            {
                foreach (SqlParameter item in sqlprms)
                {
                    str += "ParameterName: " + item.ParameterName + ", ParameterValue: " + item.Value + ";";
                }
            }

            return str;
        }


        #endregion Private Methods and Functions


    }

    public enum ConfigParameters
    {
        SQLConnection,
        UseRefKey,
        DeskCodeInCompanyAccount
    }
}


