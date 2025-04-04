using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BLL;
using WebTech.Common;
using System.Web;

namespace BLL
{
    public class Vendors
    {
        public long VendorId { get; set; }
        public string VendorName { get; set; }

        public DataTable SelectAll(long Id)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetAllVendors", param);
                dt = dtbls[0];
            }
            catch (Exception ae)
            {

            }

            return dt;
        }

        public bool Save(long Id, string Name, string Email, string Address, string CNIC, string MobileNo, bool IsActive, long LoginId, InsertMode Mode)
        {
            try
            {
                SqlParameter[] param = {
                                           new SqlParameter() { ParameterName="@Id", Value=Id },
                                           new SqlParameter() { ParameterName="@Name", Value=Name },
                                           new SqlParameter() { ParameterName="@Email", Value=Email },
                                           new SqlParameter() { ParameterName="@Address", Value=Address },
                                           new SqlParameter() { ParameterName="@CNIC", Value=CNIC },
                                           new SqlParameter() { ParameterName="@MobileNo", Value=MobileNo },
                                           new SqlParameter() { ParameterName="@IsActive", Value=IsActive },
                                           new SqlParameter() { ParameterName="@LoginId", Value=LoginId },
                                           new SqlParameter() { ParameterName="@Mode", Value=(int)Mode }
                                       };

                ErrorResponse response = new ErrorResponse();
                return DBService.ExecuteSP("WB_CreateVendor", param, ref response);
            }
            catch (Exception ae)
            {
                Logs.WriteError("Vendors.cs", "Save(" + Id + ",'" + Name + "', '" + Email + "', '" + Address + "', '" + MobileNo + "', " + IsActive + "," + LoginId + ")", "General", ae.Message);
                return false;
            }
        }

        #region delete vendor
        //public void Delete(HttpContext context)
        //{
        //    try
        //    {
        //        long Id = DataHelper.longParse(context.Request.Form["id"]);

        //        ErrorResponse response = new ErrorResponse();
        //        SqlParameter[] param = { new SqlParameter() { ParameterName = "@Id", Value = Id } };
        //        bool result = DBService.ExecuteSP("WB_DeleteBank", param, ref response);

        //        if (result)
        //        {
        //            context.Response.Clear();
        //            context.Response.ContentType = "text/plain";
        //            context.Response.Write("000");
        //        }
        //        else
        //        {
        //            context.Response.Clear();
        //            context.Response.ContentType = "text/plain";
        //            context.Response.Write("001");
        //        }
        //    }
        //    catch (Exception ae)
        //    {
        //        Logs.WriteError("Vendors.cs", "Delete()", "General", ae.Message);
        //        context.Response.Clear();
        //        context.Response.ContentType = "text/plain";
        //        context.Response.Write("999");
        //    }
        //}
        #endregion

        public DataTable GetItemForDropDown()
        {
            return DBService.FetchTable("SELECT 0 VendorId, 'Select Vendor' AS VendorName UNION ALL SELECT VendorId, Name VendorName FROM Vendors WHERE ISNULL(IsActive,0) = 1");
        }

        public void GetVendor(HttpContext context)
        {
            try
            {
                DataTable dt = DBService.FetchTable("SELECT VendorId, Name VendorName FROM Vendors WHERE ISNULL(IsActive,0) = 1");
                List<Vendors> list = new List<Vendors>();

                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Vendors()
                    {
                        VendorId = DataHelper.longParse(row["VendorId"].ToString()),
                        VendorName = DataHelper.stringParse(row["VendorName"].ToString())
                    });
                }

                context.Response.Clear();
                context.Response.ContentType = "application/json; charset=utf-8";

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(new
                {
                    data = list
                });

                context.Response.Write(json);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetVendorRemainingAmount(HttpContext context)
        {
            long VendorId = DataHelper.longParse(context.Request.Form["vendor_id"]);

            try
            {
                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetVendorRemainingAmount", param);
                DataTable dt = dtbls[0];
                DataRow row = dt.Rows[0];
                decimal RemainingAmount = DataHelper.decimalParse(row["RemainingAmount"]);

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(RemainingAmount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void GetVendorStatement(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();

            DateTime DateFrom = DataHelper.dateParse(context.Request.Form["date_from"]);
            DateTime DateTo = DataHelper.dateParse(context.Request.Form["date_to"]);
            decimal VendorId = DataHelper.decimalParse(context.Request.Form["vendor_id"]);
            decimal balance = 0;
            decimal debit_total = 0;
            decimal credit_total = 0;
            decimal balance_total = 0;

            try
            {

                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@DateFrom", Value = DateFrom },
                                           new SqlParameter() { ParameterName = "@DateTo", Value = DateTo },
                                           new SqlParameter() { ParameterName = "@VendorId", Value = VendorId },
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetVendorsAccountsStatement", param);
                dt = dtbls[0];

                sb.AppendLine(" <table id='' class='table table-striped table-bordered'>");
                sb.AppendLine("     <thead>");
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <th>Transaction No.</th>");
                sb.AppendLine("             <th>Transaction Date</th>");
                sb.AppendLine("             <th>Description</th>");
                sb.AppendLine("             <th>Debit</th>");
                sb.AppendLine("             <th>Credit</th>");
                sb.AppendLine("             <th>Balance</th>");
                sb.AppendLine("         </tr>");
                sb.AppendLine("     </thead>");
                sb.AppendLine("     <tbody>");

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        balance += DataHelper.decimalParse(row["Debit"]) - DataHelper.decimalParse(row["Credit"]);
                        debit_total += DataHelper.decimalParse(row["Debit"]);
                        credit_total += DataHelper.decimalParse(row["Credit"]);

                        sb.AppendLine("         <tr>");

                        if (DataHelper.decimalParse(row["Debit"]) == 0)
                            sb.AppendLine("             <td>" + "P-" + DataHelper.longParse(row["OrderNumber"]).ToString("0000") + "</td>");
                        else
                            sb.AppendLine("             <td>" + "V-" + DataHelper.longParse(row["OrderNumber"]).ToString("0000") + "</td>");

                        sb.AppendLine("             <td>" + DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.stringParse(row["Description"]) + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["Debit"]).ToString("#,#0") + "</td>");
                        sb.AppendLine("             <td>" + DataHelper.decimalParse(row["Credit"]).ToString("#,#0") + "</td>");
                        sb.AppendLine("             <td>" + balance.ToString("#,#0") + "</td>");
                        sb.AppendLine("         </tr>");
                    }
                }

                sb.AppendLine("     </tbody>");

                balance_total = (debit_total - credit_total);

                sb.AppendLine("     <tfoot> ");
                sb.AppendLine("         <tr> ");
                sb.AppendLine("             <th style='text-align:right;' colspan='3'><b> Total : </b></th> ");
                sb.AppendLine("             <td><b>" + debit_total.ToString("#,#0") + " PKR" + "</b></td> ");
                sb.AppendLine("             <td><b>" + credit_total.ToString("#,#0") + " PKR" + "</b></td> ");
                sb.AppendLine("             <td><b>" + balance_total.ToString("#,#0") + " PKR" + "</b></td> ");
                sb.AppendLine("         </tr> ");
                sb.AppendLine("     </tfoot> ");

                sb.AppendLine(" </table>");

                context.Response.Clear();
                context.Response.ContentType = "text/plain";
                context.Response.Write(sb.ToString());
            }
            catch (Exception ae)
            {
                Logs.WriteError("Vendors.cs", "GetVendorsPayment(" + DateFrom + ", " + DateTo + ", " + VendorId + ")", "General", ae.Message);
            }

        }
    }
}
