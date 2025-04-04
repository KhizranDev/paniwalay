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
public partial class riders : System.Web.UI.Page
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
        DataTable dt = new Riders().SelectAll(0);
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" <table id='datatable' class='table table-striped table-bordered'>");
        sb.AppendLine("     <thead>");
        sb.AppendLine("         <tr>");
        sb.AppendLine("             <th>ID</th>");
        sb.AppendLine("             <th>Rider Name</th>");
        sb.AppendLine("             <th>Contact No.</th>");
        sb.AppendLine("             <th>CNIC</th>");
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
                sb.AppendLine("         <td style='width: 80px;'>" + DataHelper.longParse(row["RiderId"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["RiderName"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["ContactNo"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["CNIC"]) + "</td>");
                sb.AppendLine("         <td style='width: 100px; text-align: center;'><input type='checkbox' class='js-switch' " + (DataHelper.boolParse(row["IsActive"]) ? "checked='checked'" : "") + "  disabled='disabled' /></td>");
                sb.AppendLine("         <td style='width: 250px;'><a href='add_rider.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["RiderId"])) + "&action=" + Encryption.Encrypt("edit") +
                    "' class='btn btn-info btn-sm'><i class='glyphicon glyphicon-edit icon-white'></i> Edit</a>" +
                    " <a href='javascript:;' class='btn btn-danger btn-sm btnDelete' id='" + DataHelper.longParse(row["RiderId"]) + "'><i class='glyphicon glyphicon-trash icon-white'></i> Delete</a></td>");
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
        if (Session["Rider_save"] != null)
        {
            bool sessionValue = DataHelper.boolParse(Session["Rider_save"]);

            if (sessionValue)
            {
                txtHiddenValue.Value = "insert_success";
            }
            else
            {
                txtHiddenValue.Value = "insert_error";
            }
            Session.Remove("Rider_save");
        }
    }
}