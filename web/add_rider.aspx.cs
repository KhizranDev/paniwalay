using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class add_rider : System.Web.UI.Page
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
        InsertMode mode = txtId.Text.Trim() == "" ? InsertMode.Add : InsertMode.Edit;
        long Id = DataHelper.longParse(txtId.Text);
        string RiderName = txtRiderName.Text;
        string ContactNo = txtContactNo.Text;
        string CNIC = txtCNIC.Text;
        string RiderAddress = txtAddress.Text;
        bool IsActive = chkIsActive.Checked;

        string url = "riders.aspx";

        if (new Riders().Save(Id, RiderName, ContactNo, CNIC, RiderAddress, IsActive, clsSession.LoginId, mode))
        {
            Session["Rider_save"] = true;
        }
        else
        {
            Session["Rider_save"] = false;
            Session["Rider_save_id"] = txtId.Text;
        }
        Response.Redirect(url, false);
        return;
    }

    private void getValues(long Id)
    {
        DataTable dt = new Riders().SelectAll(Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];
            txtId.Text = row["RiderId"].ToString();
            txtRiderName.Text = row["RiderName"].ToString();
            txtContactNo.Text = row["ContactNo"].ToString();
            txtCNIC.Text = row["CNIC"].ToString();
            txtAddress.Text = row["RiderAddress"].ToString();
            chkIsActive.Checked = DataHelper.boolParse(row["IsActive"]);
        }
    }
}