using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class vendors_rate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            long Id = 0;

            if (Request.QueryString["id"] != null)
            {
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }

            fillDropDowns(Id);
        }
    }

    private void fillDropDowns(long Id)
    {
        ddlVendors.DataSource = new Vendors().GetItemForDropDown();
        ddlVendors.DataBind();

        ddlTankerType.DataSource = new TankerType().GetItemForDropDown();
        ddlTankerType.DataBind();

        ddlVendors.SelectedValue = DataHelper.longParse(Id).ToString();
        txtVendorId.Value = DataHelper.longParse(Id).ToString();

        if (Id > 0)
        {
            divVendor.Visible = false;
        }
    }
}