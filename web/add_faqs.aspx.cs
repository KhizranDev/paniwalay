using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class add_faqs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillDropDowns();

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
        long FaqsCategoryId = DataHelper.longParse(txtHiddenCategory.Value);
        string Title = txtTitle.Text;
        string Description = txtDescription.Text;
        bool IsActive = chkIsActive.Checked;

        string url = "faqs.aspx";

        if (new Faqs().Save(Id, FaqsCategoryId, Title, Description, IsActive, clsSession.LoginId, mode))
        {
            Session["Faq_save"] = true;
        }
        else
        {
            Session["Faq_save"] = false;
            Session["Faq_save_id"] = txtId.Text;
        }
        Response.Redirect(url, false);
        return;
    }

    private void getValues(long Id)
    {
        DataTable dt = new Faqs().SelectAll(Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];
            txtId.Text = row["FaqId"].ToString();
            ddlCategory.SelectedValue = DataHelper.longParse(row["FaqCatId"]).ToString();
            txtTitle.Text = row["Title"].ToString();
            txtDescription.Text = row["Description"].ToString();
            chkIsActive.Checked = DataHelper.boolParse(row["IsActive"]);
        }
    }

    private void fillDropDowns()
    {
        ddlCategory.DataSource = new FaqsCategory().GetItemForDropDown();
        ddlCategory.DataBind();
    }
}