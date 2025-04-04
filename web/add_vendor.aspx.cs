using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class add_vendor : System.Web.UI.Page
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
        string VendorName = txtVendorName.Text;
        string Email = txtEmail.Text;
        string Address = txtAddress.Text;
        string CNIC = txtCNIC.Text;
        string MobileNo = txtMobileNo.Text;
        bool IsActive = chkIsActive.Checked;

        string url = "vendors.aspx";

        if (new Vendors().Save(Id, VendorName, Email, Address, CNIC, MobileNo, IsActive, clsSession.LoginId, mode))
        {
            Session["Vendor_save"] = true;
        }
        else
        {
            Session["Vendor_save"] = false;
            Session["Vendor_save_id"] = txtId.Text;
        }
        Response.Redirect(url, false);
        return;
    }

    private void getValues(long Id)
    {
        DataTable dt = new Vendors().SelectAll(Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];
            txtId.Text = row["VendorId"].ToString();
            txtVendorName.Text = row["Name"].ToString();
            txtEmail.Text = row["Email"].ToString();
            txtAddress.Text = row["Address"].ToString();
            txtCNIC.Text = row["CNIC"].ToString();
            txtMobileNo.Text = row["MobileNo"].ToString();
            chkIsActive.Checked = DataHelper.boolParse(row["IsActive"]);
        }
    }
}