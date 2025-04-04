using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class add_bank : System.Web.UI.Page
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
        string BankName = txtBankName.Text;
        string AccountTitle = txtAccountTitle.Text;
        string AccountNo = txtAccountNumber.Text;
        string BankAddress = txtAddress.Text;
        bool IsActive = chkIsActive.Checked;

        string url = "banks.aspx";

        if (new Banks().Save(Id, BankName, AccountTitle, AccountNo, BankAddress, IsActive, clsSession.LoginId, mode))
        {
            Session["Bank_save"] = true;
        }
        else
        {
            Session["Bank_save"] = false;
            Session["Bank_save_id"] = txtId.Text;
        }
        Response.Redirect(url, false);
        return;
    }

    private void getValues(long Id)
    {
        DataTable dt = new Banks().SelectAll(Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];
            txtId.Text = row["BankId"].ToString();
            txtBankName.Text = row["BankName"].ToString();
            txtAccountTitle.Text = row["AccountTitle"].ToString();
            txtAccountNumber.Text = row["AccountNo"].ToString();
            txtAddress.Text = row["BankAddress"].ToString();
            chkIsActive.Checked = DataHelper.boolParse(row["IsActive"]);
        }
    }
}