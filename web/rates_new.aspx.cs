using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class rates_new : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //txtDate.Value = DataHelper.dateParse(DateTime.Today).ToString("dd-MMM-yyyy");

        if (!IsPostBack)
        {
            fillDropDowns();
        }
    }

    private void fillDropDowns()
    {
        ddlTankerType.DataSource = new TankerType().GetItemForDropDown();
        ddlTankerType.DataBind();
    }
}