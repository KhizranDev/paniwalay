using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using WebTech.Common;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace BLL
{
    public class Dashboard
    {
        public void GetDashboard(HttpContext context)
        {
            
            DataTable dt = new DataTable();

            //DateTime StartDate = DataHelper.dateParse(context.Request.Form["StartDate"]);
            //DateTime EndDate = DataHelper.dateParse(context.Request.Form["EndDate"]);
            //DateTime datefrom = BLL.DateHandler.GetDate(context.Request.Form["StartDate"]);
            //DateTime dateto = BLL.DateHandler.GetDate(context.Request.Form["StartDate"]);

            string StartDate = DataHelper.dateParse(context.Request.Form["StartDate"]).ToString("yyyy-M-d");
            string EndDate = DataHelper.dateParse(context.Request.Form["EndDate"]).ToString("yyyy-M-d");

            try
            {
                SqlParameter[] param = { 
                                          new SqlParameter() { ParameterName = "@StartDate", Value = StartDate },
                                          new SqlParameter() { ParameterName = "@EndDate", Value = EndDate }
                                       };
                DataTableCollection dtbls = DBService.FetchFromSP("WB_GetDashBoard", param);

                if (dtbls.Count == 4)
                {

                    #region Status Counts

                    DataRow rowCounts = dtbls[0].Rows[0];

                    StringBuilder sbCounts = new StringBuilder();

                    sbCounts.AppendLine("<div class='col-md-2 col-sm-4 col-xs-6 tile_stats_count'> ");
                    sbCounts.AppendLine("	<span class='count_top'><i class='fa fa-user'></i>Total New Orders</span> ");
                    sbCounts.AppendLine("	<div class='count green'>" + DataHelper.decimalParse(rowCounts["NewOrders"]).ToString("#,#0") + "</div> ");
                    sbCounts.AppendLine("</div> ");
                    sbCounts.AppendLine("<div class='col-md-2 col-sm-4 col-xs-6 tile_stats_count'> ");
                    sbCounts.AppendLine("	<span class='count_top'><i class='fa fa-clock-o'></i>Total Received Orders</span> ");
                    sbCounts.AppendLine("	<div class='count'>" + DataHelper.decimalParse(rowCounts["ReceivedOrders"]).ToString("#,#0") + "</div> ");
                    sbCounts.AppendLine("</div> ");
                    sbCounts.AppendLine("<div class='col-md-2 col-sm-4 col-xs-6 tile_stats_count'> ");
                    sbCounts.AppendLine("	<span class='count_top'><i class='fa fa-user'></i>Total In-Progress Orders</span> ");
                    sbCounts.AppendLine("	<div class='count'>" + DataHelper.decimalParse(rowCounts["InProgressOrders"]).ToString("#,#0") + "</div> ");
                    sbCounts.AppendLine("</div> ");
                    sbCounts.AppendLine("<div class='col-md-2 col-sm-4 col-xs-6 tile_stats_count'> ");
                    sbCounts.AppendLine("	<span class='count_top'><i class='fa fa-user'></i>Total Delivered Orders</span> ");
                    sbCounts.AppendLine("	<div class='count'>" + DataHelper.decimalParse(rowCounts["DeliveredOrders"]).ToString("#,#0") + "</div> ");
                    sbCounts.AppendLine("</div> ");
                    sbCounts.AppendLine("<div class='col-md-2 col-sm-4 col-xs-6 tile_stats_count'> ");
                    sbCounts.AppendLine("	<span class='count_top'><i class='fa fa-user'></i>Total Finished Orders</span> ");
                    sbCounts.AppendLine("	<div class='count'>" + DataHelper.decimalParse(rowCounts["FinishedOrders"]).ToString("#,#0") + "</div> ");
                    sbCounts.AppendLine("</div> ");
                    sbCounts.AppendLine("<div class='col-md-2 col-sm-4 col-xs-6 tile_stats_count'> ");
                    sbCounts.AppendLine("	<span class='count_top'><i class='fa fa-user'></i>Total Cancel Orders</span> ");
                    sbCounts.AppendLine("	<div class='count red'>" + DataHelper.decimalParse(rowCounts["CancelledOrders"]).ToString("#,#0") + "</div> ");
                    sbCounts.AppendLine("</div> ");

                    #endregion

                    #region Locations

                    StringBuilder sbLocations = new StringBuilder();
                    sbLocations.AppendLine("<h4>Location across percentage</h4> ");


                    if (dtbls[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in dtbls[1].Rows)
                        {
                            sbLocations.AppendLine("<div class='widget_summary'> ");
                            sbLocations.AppendLine("	<div class='w_left w_25'> ");
                            sbLocations.AppendLine("		<span>"+ row["LocationName"] +"</span> ");
                            sbLocations.AppendLine("	</div> ");
                            sbLocations.AppendLine("	<div class='w_center w_55'> ");
                            sbLocations.AppendLine("		<div class='progress'> ");
                            sbLocations.AppendLine("			<div class='progress-bar bg-green' role='progressbar' aria-valuenow='60' aria-valuemin='0' ");
                            sbLocations.AppendLine("				aria-valuemax='100' style='width: "+ DataHelper.stringParse(row["Percentage"]) +"%;'> ");
                            sbLocations.AppendLine("				<span class='sr-only'>60% Complete</span> ");
                            sbLocations.AppendLine("			</div> ");
                            sbLocations.AppendLine("		</div> ");
                            sbLocations.AppendLine("	</div> ");
                            sbLocations.AppendLine("	<div class='w_right w_20'> ");
                            sbLocations.AppendLine("		<span>" + DataHelper.stringParse(row["Total"]) + "</span> ");
                            sbLocations.AppendLine("	</div> ");
                            sbLocations.AppendLine("	<div class='clearfix'> ");
                            sbLocations.AppendLine("	</div> ");
                            sbLocations.AppendLine("</div> ");
                        }
                    }
                    

                    #endregion

                    #region Tanker Types

                    List<sTankerTypes> list = new List<sTankerTypes>();
                    foreach (DataRow row in dtbls[2].Rows)
                    {
                        list.Add(new sTankerTypes() { TankerTypeName = DataHelper.stringParse(row["TankerTypeName"]), Percentage = DataHelper.floatParse(row["Percentage"].ToString()) });
                    }
                    var jsonTankerType = JsonConvert.SerializeObject(list);

                    #endregion Tanker Types

                    #region Order Counts

                    List<sOrderCounts> list2 = new List<sOrderCounts>();
                    foreach (DataRow row in dtbls[3].Rows)
                    {
                        list2.Add(new sOrderCounts() { Date = DataHelper.dateParse(row["Date"]).ToString("MMM-d"), TotalRecords = DataHelper.intParse(row["TotalRecords"].ToString()) });
                    }
                    var jsonOrderCounts = JsonConvert.SerializeObject(list2);

                    #endregion Order Counts

                    context.Response.Clear();
                    context.Response.ContentType = "application/json; charset=utf-8";

                    string json = JsonConvert.SerializeObject(new
                    {
                        StatusCounts = sbCounts.ToString(),
                        Locations = sbLocations.ToString(),
                        TankerType = jsonTankerType,
                        OrderCounts = jsonOrderCounts
                    });

                    context.Response.Write(json);
                }

            }
            catch (Exception ae)
            {
                Logs.WriteError("Dashboard.cs", "GetDashboard(" + StartDate + "," + EndDate + ")", "General", ae.Message);
            }
        }

        class sTankerTypes
        {
            public string TankerTypeName { get; set; }
            public float Percentage { get; set; }
        }

        class sOrderCounts
        {
            public string Date { get; set; }
            public int TotalRecords { get; set; }
        }
    }
}
