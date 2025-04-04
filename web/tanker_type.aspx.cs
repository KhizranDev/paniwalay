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


public partial class tanker_type : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            displayDeleteMessages();

            string action = "";
            long Id = 0;

            if (Request.QueryString["action"] != null && Request.QueryString["id"] != null)
            {
                action = Encryption.Decrypt(Request.QueryString["action"].ToString(), Response);
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }

            if (action == "delete")
                deleteRecord(Id);

            DisplayData();
            displaySavingMessages();
        }
    }

    private void DisplayData()
    {
        DataTable dt = new TankerType().SelectAll(0);
        StringBuilder sb = new StringBuilder();

        sb.AppendLine(" <table id='datatable' class='table table-striped table-bordered'>");
        sb.AppendLine("     <thead>");
        sb.AppendLine("         <tr>");
        sb.AppendLine("             <th>ID</th>");
        sb.AppendLine("             <th>Tanker Type Name</th>");
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
                sb.AppendLine("         <td style='width: 80px;'>" + DataHelper.longParse(row["TankerTypeId"]) + "</td>");
                sb.AppendLine("         <td>" + DataHelper.stringParse(row["TankerTypeName"]) + "</td>");
                sb.AppendLine("         <td style='width: 100px; text-align: center;'><input type='checkbox' class='js-switch' " + (DataHelper.boolParse(row["IsActive"]) ? "checked='checked'" : "") + "  disabled='disabled' /></td>");
                sb.AppendLine("         <td style='width: 160px;'>" + DataHelper.dateParse(row["CreatedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("         <td style='width: 160px;'>" + DataHelper.dateParse(row["ModifiedOn"]).ToString("dd-MMM-yy hh:mm tt") + "</td>");
                sb.AppendLine("         <td style='width: 250px;'><a href='add_tanker_type.aspx?id=" + Encryption.Encrypt(DataHelper.stringParse(row["TankerTypeId"])) + "&action=" + Encryption.Encrypt("edit") +
                    "' class='btn btn-info btn-sm'><i class='glyphicon glyphicon-edit icon-white'></i> Edit</a>" +
                    " <a href='javascript:;' class='btn btn-warning btn-sm btnArchive' id='" + DataHelper.longParse(row["TankerTypeId"]) + "'><i class='fa fa-archive'></i> Archive</a></td>");
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
        if (Session["Tanker_Type_save"] != null)
        {
            bool sessionValue = DataHelper.boolParse(Session["Tanker_Type_save"]);

            if (sessionValue)
            {
                txtHiddenValue.Value = "insert_success";
            }
            else
            {
                txtHiddenValue.Value = "insert_error";
            }
            Session.Remove("Tanker_Type_save");
        }
    }

    private void displayDeleteMessages()
    {
        if (Session["Tanker_Type_delete"] != null)
        {
            bool sessionValue = DataHelper.boolParse(Session["Tanker_Type_delete"]);

            if (sessionValue)
                txtHiddenValue.Value = "delete_success";
            else
                txtHiddenValue.Value = "delete_error";

            Session.Remove("Tanker_Type_delete");
        }
    }

    private void deleteRecord(long Id)
    {
        //if (new TankerType().Delete(Id))
        //    Session["Tanker_Type_delete"] = true;
        //else
        //    Session["Tanker_Type_delete"] = false;

        //Response.Redirect("tanker_type.aspx", false);
    }
}