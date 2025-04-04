using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using WebTech.Common;

namespace BLL
{
    public class Customer
    {
        public long LoginId { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string PinNo { get; set; }
        public int LoginType { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }

        public decimal Amount { get; set; }
        public bool FullPayment { get; set; }
        public string OwnerPKNo { get; set; }
        public string OwnerPinNo { get; set; }
        public string DisplayName { get; set; }
        public int CountryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
        public string LiberytyAccountNo { get; set; }
        public bool Franchise { get; set; }
        public long FranchiserId { get; set; }
        public string FranchiserCode { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
        public string Bank_Name { get; set; }
        public string Swift_Code { get; set; }
        public string IBAN { get; set; }
        public DataTable dtItems { get; set; }
        public long BaseLoginId { get; set; }
        public bool IsWinner { get; set; }
        public string ProductImageURL { get; set; }
        public bool IsSubFranchise { get; set; }
        public bool IsBusinessFranchise { get; set; }
        public bool isdeactive { get; set; }
        public string ImagePath { get; set; }
        public string RewardName { get; set; }
        public DateTime RewardDate { get; set; }
        public long OriginalLoginId { get; set; }

        public List<Customer> Customers()
        {
            DataTable dt = DBService.FetchTable("getAllCustomers", null);

            List<Customer> list = new List<Customer>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Customer()
                {
                    LoginId = DataHelper.longParse(row["Id"]),
                    FullName = DataHelper.stringParse(row["FullName"]),
                    EmailAddress = DataHelper.stringParse(row["Email"]),
                    Country = DataHelper.stringParse(row["Country"]),
                    ContactNo = DataHelper.stringParse(row["ContactNo"]),
                    Address = DataHelper.stringParse(row["Address"]),
                    Password = DataHelper.stringParse(row["Password"]),
                    PinNo = DataHelper.stringParse(row["PinNo"]),
                    LoginType = DataHelper.intParse(row["LoginType"]),
                    Active = DataHelper.boolParse(row["IsActive"])
                });
            }

            return list;
        }

        public List<Customer> CustomersBalance()
        {
            DataTable dt = DBService.FetchTable("WB_GetCustomerPaymentListAdmin", null);

            List<Customer> list = new List<Customer>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Customer()
                {
                    LoginId = DataHelper.longParse(row["Id"]),
                    FullName = DataHelper.stringParse(row["FullName"]),
                    EmailAddress = DataHelper.stringParse(row["Email"]),
                    Country = DataHelper.stringParse(row["Country"]),
                    ContactNo = DataHelper.stringParse(row["ContactNo"]),
                    Address = DataHelper.stringParse(row["Address"]),
                    Balance = DataHelper.decimalParse(row["Balance"]),
                    Active = DataHelper.boolParse(row["IsActive"])
                });
            }

            return list;
        }

        public Customer getCustomer(long loginId)
        {
            SqlParameter[] param = {
                                       new SqlParameter() { ParameterName="@LoginId", Value=loginId }
                                   };

            DataTable dt = DBService.FetchTable("getCustomerInformation", param);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Customer()
                {
                    LoginId = DataHelper.longParse(row["Id"]),
                    FullName = DataHelper.stringParse(row["FullName"]),
                    EmailAddress = DataHelper.stringParse(row["Email"]),
                    Country = DataHelper.stringParse(row["Country"]),
                    ContactNo = DataHelper.stringParse(row["ContactNo"]),
                    Address = DataHelper.stringParse(row["Address"]),
                    Password = DataHelper.stringParse(row["Password"]),
                    PinNo = DataHelper.stringParse(row["PinNo"]),
                    LoginType = DataHelper.intParse(row["LoginType"]),
                    Active = DataHelper.boolParse(row["IsActive"])
                };
            }
            else
            {
                return null;
            }
        }

        public Customer getCustomerByUserId(string userId)
        {
            SqlParameter[] param = {
                                       new SqlParameter() { ParameterName="@UserId", Value=userId }
                                   };

            DataTable dt = DBService.FetchTable("getCustomerInformationByUserId", param);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Customer()
                {
                    LoginId = DataHelper.longParse(row["Id"]),
                    FullName = DataHelper.stringParse(row["FullName"]),
                    EmailAddress = DataHelper.stringParse(row["Email"]),
                    Country = DataHelper.stringParse(row["Country"]),
                    ContactNo = DataHelper.stringParse(row["ContactNo"]),
                    Address = DataHelper.stringParse(row["Address"]),
                    Password = DataHelper.stringParse(row["Password"]),
                    PinNo = DataHelper.stringParse(row["PinNo"]),
                    LoginType = DataHelper.intParse(row["LoginType"]),
                    Active = DataHelper.boolParse(row["IsActive"]),
                };
            }
            else
            {
                return null;
            }
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            SqlParameter[] param = {
                                       new SqlParameter() { ParameterName="@LoginId", Value=this.LoginId },
                                       new SqlParameter() { ParameterName="@OldPassword", Value=oldPassword },
                                       new SqlParameter() { ParameterName="@NewPassword", Value=newPassword }
                                   };

            ErrorResponse response = new ErrorResponse();
            DataTableCollection dtbls = DBService.FetchFromSP("ChangePassword", param, ref response);
            if (response.Error)
                throw new Exception("Invalid");

            string result = dtbls[0].Rows[0]["ErrorCode"].ToString();

            if (result == "000")
                return true;
            else
                return false;
        }

        public bool ChangePinNo(string oldPin, string newPin)
        {
            SqlParameter[] param = {
                                       new SqlParameter() { ParameterName="@LoginId", Value=this.LoginId },
                                       new SqlParameter() { ParameterName="@OldPin", Value=oldPin },
                                       new SqlParameter() { ParameterName="@NewPin", Value=newPin }
                                   };

            ErrorResponse response = new ErrorResponse();
            DataTableCollection dtbls = DBService.FetchFromSP("ChangePinNo", param, ref response);
            if (response.Error)
                throw new Exception("Invalid");

            string result = dtbls[0].Rows[0]["ErrorCode"].ToString();

            if (result == "000")
                return true;
            else
                return false;
        }

        public DataTable getPassword(string emailid)
        {
            DataTable dt = new DataTable();

            try
            {
                SqlParameter[] param = { 
                                           new SqlParameter() { ParameterName = "@emailid", Value = emailid }
                                       };

                ErrorResponse response = new ErrorResponse();
                DataTableCollection dtbls = DBService.FetchFromSP("getforgetpassword", param, ref response);
                if (response.Error)
                    throw new Exception("Invalid");

                dt = dtbls[0];

            }
            catch (Exception ae)
            {
                Logs.WriteError("Customers.cs", "getPassword(" + emailid + ")", "General", ae.Message);
            }

            return dt;

        }
    }
}
