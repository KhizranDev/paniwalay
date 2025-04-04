using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class add_rate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = "";
        long Id = 0;
        if (!IsPostBack)
        {
            if (Request.QueryString["action"] != null && Request.QueryString["id"] != null)
            {
                action = Encryption.Decrypt(Request.QueryString["action"].ToString(), Response);
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }

            fillDropDowns();

            if (action == "edit")
            {
                getValues(Id);
            }
        }
    }

    private void getValues(long Id)
    {
        DataTable dt = new Rate().SelectAll(Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];
            txtId.Text = row["RateId"].ToString();
            ddlLocation.SelectedValue = row["LocationId"].ToString();
            txtStartDate.Value = DataHelper.dateParse(row["StartDate"]).ToString("dd-MMM-yyyy");
            txtEndDate.Value = DataHelper.dateParse(row["EndDate"]).ToString("dd-MMM-yyyy");
            txtRate.Value= row["Rates"].ToString();
        }
    }

    private void fillDropDowns()
    {
        ddlLocation.DataSource = new Locations().GetItemForDropDown();
        ddlLocation.DataBind();
    }
}