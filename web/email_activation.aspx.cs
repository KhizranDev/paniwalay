using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebTech.Common;
using System.Data.SqlClient;
using System.Data;

public partial class email_activation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["activation_code"] != null)
            {
                string activation_code = Request.QueryString["activation_code"].ToString();
                Activation(activation_code);
            }
            
        }
    }

    private void Activation(string activation_code)
    {
        activation_code = Encryption.Decrypt(activation_code, this.Response);

        SqlParameter[] param = {
                                     new SqlParameter("@ActivationCode", activation_code),
                                };

        ErrorResponse response = new ErrorResponse();
        DataTableCollection dtbls = DBService.FetchFromSP("SP_ActivationCode", param, ref response);
        if (response.Error)
        {
            throw new Exception(response.ErrorList[0].Message);
        }

        DataRow dr = dtbls[0].Rows[0];
        if (dr["ErrorCode"].ToString() == "000")
        {
            Response.Write("success");
        }
        else
        {
            Response.Write("fail");
        }

    }
}