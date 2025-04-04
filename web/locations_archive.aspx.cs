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

public partial class locations_archive : System.Web.UI.Page
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
        DataTable dt = new Locations().SelectAllArchive();
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" <table id='datatable' class='table table-striped table-bordered'>");
        sb.AppendLine("     <thead>");
        sb.AppendLine("         <tr>");
        sb.AppendLine("             <th>ID</th>");
        sb.AppendLine("             <th>Location Name</th>");
        sb.AppendLine("             <th>Status</th>");
        sb.AppendLine("             <th>Created On</th>");
        sb.AppendLine("             <th>Modified On</th>");
        sb.AppendLine("             <th>Actions</th>");
        sb.AppendLine("         </tr>");
        sb.AppendLine("     </thead>");
        sb.AppendLine("     <tbody>");

        try
        {
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("         <tr>");
                sb.AppendLine("         <td style='width: 80px;'>" + DataHelper.longParse(row["LocationId"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                sb.AppendLine("         <td style='width: 100px; text-align: center;'><input type='checkbox' class='js-switch' " + (DataHelper.boolParse(row["IsActive"]) ? "checked='checked'" : "") + "  disabled='disabled' /></td>");
                sb.AppendLine("         <td style='width: 160px;'>" + DataHelper.dateParse(row["CreatedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("         <td style='width: 160px;'>" + DataHelper.dateParse(row["ModifiedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("         <td style='width: 250px;'> <a href='javascript:;' class='btn btn-info btn-sm btnRestore' id='" + DataHelper.longParse(row["LocationId"]) + "'><i class='glyphicon glyphicon-repeat icon-white'></i> Restore</a> <a href='javascript:;' class='btn btn-danger btn-sm btnDelete' id='" + DataHelper.longParse(row["LocationId"]) + "'><i class='glyphicon glyphicon-trash icon-white'></i> Delete</a> </td>");
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