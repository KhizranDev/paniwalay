using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTech.Common;
using BLL;
using System.Data;
using System.Text;

public partial class vendors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string action = "";
            long Id = 0;

            if (Request.QueryString["action"] != null && Request.QueryString["id"] != null)
            {
                action = Encryption.Decrypt(Request.QueryString["action"].ToString(), Response);
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }

            DisplayData();
            displaySavingMessages();
        }
    }

    private void DisplayData()
    {
        DataTable dt = new Vendors().SelectAll(0);
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" <table id='datatable' class='table table-striped table-bordered'>");
        sb.AppendLine("     <thead>");
        sb.AppendLine("         <tr>");
        sb.AppendLine("             <th>ID</th>");
        sb.AppendLine("             <th>Name</th>");
        sb.AppendLine("             <th>Email</th>");
        sb.AppendLine("             <th>Mobile #</th>");
        sb.AppendLine("             <th>Status</th>");
        sb.AppendLine("             <th>Actions</th>");
        sb.AppendLine("         </tr>");
        sb.AppendLine("     </thead>");
        sb.AppendLine("     <tbody>");

        try
        {
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("         <tr>");
                sb.AppendLine("         <td style='width: 80px;'>" + DataHelper.longParse(row["VendorId"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["Name"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["Email"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["MobileNo"]) + "</td>");
                sb.AppendLine("         <td style='width: 100px; text-align: center;'><input type='checkbox' class='js-switch' " + (DataHelper.boolParse(row["IsActive"]) ? "checked='checked'" : "") + "  disabled='disabled' /></td>");
                sb.AppendLine("         <td style='width: 250px;'><a title='Edit' href='add_vendor.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["VendorId"])) + "&action=" + Encryption.Encrypt("edit") +
                    "' class='btn btn-info btn-xs'><i class='glyphicon glyphicon-edit icon-white'></i> </a>" +
                    " <a href='javascript:;' class='btn btn-danger btn-xs btnDelete' title='Delete' id='" + DataHelper.longParse(row["VendorId"]) + "'><i class='glyphicon glyphicon-trash icon-white'></i> </a> " +
                    " <a href='vendors_rate.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["VendorId"])) + "' class='btn btn-warning btn-xs' title='Rates'><i class='glyphicon glyphicon-usd icon-white'></i> </a> " +
                    " <a href='vendors_order.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["VendorId"])) + "' class='btn btn-info btn-xs' title='Orders'><i class='glyphicon glyphicon-shopping-cart icon-white'></i> </a> " +
                    "</td>");
                sb.AppendLine("         </tr>");
            }
        }
        catch (Exception ae)
        {

        }

        sb.AppendLine("     </tbody>");
        sb.AppendLine(" </table>");

        divTable.InnerHtml = sb.ToString();
    }

    private void displaySavingMessages()
    {
        if (Session["Vendor_save"] != null)
        {
            bool sessionValue = DataHelper.boolParse(Session["Vendor_save"]);

            if (sessionValue)
            {
                txtHiddenValue.Value = "insert_success";
            }
            else
            {
                txtHiddenValue.Value = "insert_error";
            }
            Session.Remove("Vendor_save");
        }
    }
}