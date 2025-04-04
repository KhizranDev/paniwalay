using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;


public partial class invoices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fillDropDowns();
    }

    private void fillDropDowns()
    {
        ddlBank.DataSource = new Banks().GetItemForDropDown();
        ddlBank.DataBind();

        ddlRider.DataSource = new Riders().GetItemForDropDown();
        ddlRider.DataBind();

        ddlMethod.DataSource = new PaymentMethods().GetItemForDropDown();
        ddlMethod.DataBind();
    }
}