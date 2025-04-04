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


public partial class accounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DisplayData();
        }
    }

    private void DisplayData()
    {
        DataTable dt = new Accounts().SelectAll(0);
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" <table id='datatable' class='table table-striped table-bordered'>");
        sb.AppendLine("     <thead>");
        sb.AppendLine("         <tr>");
        sb.AppendLine("             <th>Account Id</th>");
        sb.AppendLine("             <th>User Id</th>");
        sb.AppendLine("             <th>Full Name</th>");
        sb.AppendLine("             <th>Email</th>");
        sb.AppendLine("             <th>Contact No.</th>");
        sb.AppendLine("             <th>Created On</th>");
        sb.AppendLine("             <th>Active On</th>");
        sb.AppendLine("             <th>Verified</th>");
        sb.AppendLine("         </tr>");
        sb.AppendLine("     </thead>");
        sb.AppendLine("     <tbody>");

        try
        {
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("         <tr>");
                sb.AppendLine("             <td style='width: 80px;'>" + DataHelper.longParse(row["AccountId"]) + "</td>");
                sb.AppendLine("             <td>" + DataHelper.stringParse(row["UserId"]) + "</td>");
                sb.AppendLine("             <td>" + DataHelper.stringParse(row["FullName"]) + "</td>");
                sb.AppendLine("             <td>" + DataHelper.stringParse(row["EmailAddress"]) + "</td>");
                sb.AppendLine("             <td>" + DataHelper.stringParse(row["ContactNo"]) + "</td>");
                sb.AppendLine("             <td style=''>" + DataHelper.dateParse(row["CreatedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("             <td style=''>" + DataHelper.dateParse(row["ActiveOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("             <td style='text-align: center;'><input type='checkbox' class='js-switch' id='" + DataHelper.longParse(row["AccountId"]) + "'  " + (DataHelper.boolParse(row["IsVerified"]) ? "checked='checked'" : "") + " " + (DataHelper.boolParse(row["IsVerified"]) ? "disabled" : "") + "  /></td>");
                //sb.AppendLine("             <td style='text-align: center;'><a href='#' class='btn btn-info small-btn btnVerified' id='" + DataHelper.longParse(row["AccountId"]) + "'> " + (DataHelper.boolParse(row["IsActive"]) ? "<i class='glyphicon glyphicon-ok icon-white'></i>" : "<i class='glyphicon glyphicon-remove icon-white'></i>") + "  </a></td>");
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
}