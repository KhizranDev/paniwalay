<%@ Page Language="C#" AutoEventWireup="true" CodeFile="email_reset_password.aspx.cs" Inherits="email_reset_password" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>::.Pani Walay.::</title>

    <!-- Bootstrap -->
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- Animate.css -->
    <link href="vendors/animate.css/animate.min.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <link href="build/css/custom.min.css" rel="stylesheet">
  </head>

  <body class="login">
    <div>
      <a class="hiddenanchor" id="forgot_pass"></a>
      <a class="hiddenanchor" id="signin"></a>

      <div class="login_wrapper">
        <div class="animate form login_form">
          <section class="login_content">

            <img src="images/admin-logo.png" alt="logo" style="width:60%;">

            <form action="email_reset_password.aspx" method="post" runat="server">
                
                <input type="hidden" id="txtuserid" runat="server" />
                <input type="hidden" id="txtaccountid" runat="server" />
                <input type="hidden" id="txttoken" runat="server" />

              <h1>Forget Password</h1>
              <div class="form-group">
                <input type="password" class="form-control" placeholder="New Password" id="txtNewPassword" name="txtNewPassword" />
              </div>
              <div class="form-group">
                <input type="password" class="form-control" placeholder="Confirm Password" id="txtConfirmPassword" name="txtConfirmPassword" />
              </div>
              <div>
                <button type="button" id="btnSubmit" name="btnSubmit" class="btn btn-primary" >SUBMIT</button>
                <a style="display:none;" class="reset_pass to_forgot" href="#forgot_pass">Lost your password?</a>
              </div>
              <div class="input-error form-control-input" id="divMessage" style="color: Red; display:none;"></div>
              <div class="clearfix"></div>

              <div class="separator">
                <div>
                  <p>©<%= DateTime.Now.Year %> All Rights Reserved. Pani Walay. Privacy and Terms</p>
                </div>
              </div>
            </form>
          </section>
        </div>

      </div>
    </div>


     <!-- jQuery -->
    <script src="vendors/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="vendors/bootstrap/dist/js/bootstrap.min.js"></script>

    <script>

        $().ready(function () {

            $("#btnSubmit").click(function () {

                if (!Validation()) {

                    $("#btnSubmit").button('loading');

                    $.ajax({
                        type: "POST",
                        url: "RequestHandler.ashx?action=reset_password",
                        data: {
                            'user_id': $("#txtuserid").val(),
                            'account_id': $("#txtaccountid").val(),
                            'token': $("#txttoken").val(),
                            'new_password': $("#txtNewPassword").val()
                        }
                    }).done(function (data) {

                        $("#btnSubmit").button('reset');

                        if (data == "000") {
                            window.location.href = "success_password.aspx";
                            //ClearValues();
                        } else {
                            $("#divMessage").html('Something goes wrong..!');
                            $("#divMessage").show();
                            setTimeout(function () { $('#divMessage').fadeOut('fast'); }, 5000);
                        }


                    });
                }

            });

        });

        function Validation() {

            var err = false;

            if ($("#txtNewPassword").val() == '' || $("#txtConfirmPassword").val() == '') {
                $("#divMessage").html('Kindly enter password');
                $("#divMessage").show();
                $("#txtNewPassword").focus();
                err = true;
            }
            else {
                if ($("#txtNewPassword").val() != $("#txtConfirmPassword").val()) {
                    $("#divMessage").html('Kindly enter same password');
                    $("#divMessage").show();
                    $("#txtConfirmPassword").focus();
                    err = true;
                }
                else {
                    $("#divMessage").html('');
                    $("#divMessage").show();
                    err = false;
                }
            }
            
            return err;
        }

        function ClearValues() {
            $("#txtNewPassword").val('');
            $("#txtConfirmPassword").val('');
        }

    </script>

  </body>
</html>
