using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data;

public partial class order_details_bk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        long Id = 0;
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                Id = DataHelper.longParse(Encryption.Decrypt(Request.QueryString["id"].ToString(), Response));
            }

            fillDropDowns(Id);
            getValues(Id);
        }
    }

    private void getValues(long Id)
    {
        DataTable dt = new Order().OrderDetails(0, Id);
        if (dt != null)
        {
            DataRow row = dt.Rows[0];

            txtOrderId.Value = DataHelper.longParse(row["OrderId"]).ToString();
            txtOrder.InnerHtml = "PW-" + DataHelper.longParse(row["OrderId"]).ToString("0000");
            txtOrderDate.InnerHtml = DataHelper.dateParse(row["OrderDate"]).ToString("dd-MMM-yyyy") + " " + DataHelper.dateParse(row["OrderTime"]).ToString("hh:mm tt");
            txtCustomer.InnerHtml = DataHelper.stringParse(row["CustomerName"]);
            txtEmail.InnerHtml = DataHelper.stringParse(row["EmailAddress"]);
            txtLocation.InnerHtml = DataHelper.stringParse(row["LocationName"]);
            txtAddress.InnerHtml = DataHelper.stringParse(row["Address"]);
            txtAmount.InnerHtml = DataHelper.decimalParse(row["Amount"]).ToString("#,#0") + " PKR";
            txtQuantity.InnerHtml = DataHelper.intParse(row["Quantity"]).ToString();
            txtTotalAmount.InnerHtml = DataHelper.decimalParse(row["TotalAmount"]).ToString("#,#0") + " PKR";
            txtStatus.InnerHtml = DataHelper.stringParse(row["OrderStatus"]);


            if (DataHelper.boolParse(row["IsVerified"]))
                txtVerified.Visible = true;
            else
                txtUnVerified.Visible = true;


            if (DataHelper.intParse(row["OrderTypeId"]) == 1)
                txtOrderType.InnerHtml = "Normal";
            else
                txtOrderType.InnerHtml = "Scheduled " + DataHelper.dateParse(row["ScheduleDate"]).ToString("dd-MMM-yyyy");
        }
    }

    private void fillDropDowns(long OrderId)
    {
        ddlStatus.DataSource = new OrderStatus().SelectAllByOrder(OrderId);
        ddlStatus.DataBind();
    }
}