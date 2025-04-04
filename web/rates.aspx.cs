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

public partial class rates : System.Web.UI.Page
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
        DataTable dt = new Rate().SelectAll(0);
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" <table id='datatable' class='table table-striped table-bordered'>");
        sb.AppendLine("     <thead>");
        sb.AppendLine("         <tr>");
        sb.AppendLine("             <th>ID</th>");
        sb.AppendLine("             <th>Location Name</th>");
        sb.AppendLine("             <th>Start Date</th>");
        sb.AppendLine("             <th>End Date</th>");
        sb.AppendLine("             <th>Rates</th>");
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
                sb.AppendLine("         <td style='width: 80px;'>" + DataHelper.longParse(row["RateId"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["LocationName"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.dateParse(row["StartDate"]).ToString("dd-MMM-yy") + "</td>");
                sb.AppendLine("         <td>" + DataHelper.dateParse(row["EndDate"]).ToString("dd-MMM-yy") + "</td>");
                sb.AppendLine("         <td>" + DataHelper.decimalParse(row["Rates"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.dateParse(row["CreatedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("         <td>" + DataHelper.dateParse(row["ModifiedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("         <td><a href='add_rate.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["RateId"])) + "&action=" + Encryption.Encrypt("edit") +
                    "' class='btn btn-info btn-sm'><i class='glyphicon glyphicon-edit icon-white'></i> Edit</a>" +
                    " <a href='#' class='btn btn-danger btn-sm btnDelete' id='"+ DataHelper.longParse(row["RateId"]) +"'><i class='glyphicon glyphicon-trash icon-white'></i> Delete</a></td>");
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