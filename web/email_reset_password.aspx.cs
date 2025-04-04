using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebTech.Common;
using System.Data.SqlClient;
using System.Data;

public partial class email_reset_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = "";
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"].ToString();
            }
            else
            {
                Response.Redirect("~/default.aspx", true);
            }


            SqlParameter[] param = {
                                    new SqlParameter() { ParameterName = "@id", Value = id }
                               };
            ErrorResponse response = new ErrorResponse();
            DataTableCollection dtbls = DBService.FetchFromSP("SP_PasswordTokenCheck", param, ref response);

            if (dtbls[0].Rows.Count > 0)
            {
                DataRow row = dtbls[0].Rows[0];
                txtuserid.Value = row["UserId"].ToString();
                txtaccountid.Value = row["AccountId"].ToString();
                txttoken.Value = id;
            }
            else
            {
                Response.Redirect("~/error_password.aspx");
            }

        }
    }
  
}