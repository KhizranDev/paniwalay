using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class add_notification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = "";
        long Id = 0;
        if (!IsPostBack)
        {
            chkIsActive.Checked = true;

            if (Request.QueryString["action"] != null && Request.QueryString["id"] != null)
            {
                action = Encryption.Decrypt(Request.QueryString["action"].ToString(), Response);
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }

            if (action == "edit")
            {
                getValues(Id);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        string ImageURL = "";

        if (FileUpload1.HasFile)
        {
            string current_time = DateTime.Now.ToString("yyyyMMddHHmmssms");
            ImageURL = current_time + "_" + FileUpload1.FileName;
            FileUpload1.SaveAs(Server.MapPath("content/notifications/" + ImageURL));
        }
        else
        {
            ImageURL = txtImageURL.Value;
        }


        InsertMode mode = txtId.Text.Trim() == "" ? InsertMode.Add : InsertMode.Edit;
        long Id = DataHelper.longParse(txtId.Text);
        string Title = txtTitle.Text;
        string Description = txtDescription.Text;
        DateTime StartDate = DataHelper.dateParse(txtStartDate.Value.ToString());
        DateTime EndDate = DataHelper.dateParse(txtEndDate.Value.ToString());
        bool IsActive = chkIsActive.Checked;

        string url = "notification.aspx";

        if (new Notifications().Save(Id, Title, Description, StartDate, EndDate, ImageURL, IsActive, clsSession.LoginId, mode))
        {
            Session["Notification_save"] = true;
        }
        else
        {
            Session["Notification_save"] = false;
            Session["Notification_save_id"] = txtId.Text;
        }
        Response.Redirect(url, false);
        return;

    }

    private void getValues(long Id)
    {
        DataTable dt = new Notifications().SelectAll(Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];
            txtId.Text = row["Id"].ToString();
            txtTitle.Text = row["Title"].ToString();
            txtDescription.Text = row["Description"].ToString();
            txtStartDate.Value = DataHelper.dateParse(row["StartDate"]).ToString("dd-MMM-yyyy");
            txtEndDate.Value = DataHelper.dateParse(row["EndDate"]).ToString("dd-MMM-yyyy");
            txtImageURL.Value = row["ImageURL"].ToString();
            chkIsActive.Checked = DataHelper.boolParse(row["IsActive"]);
        }
    }
}