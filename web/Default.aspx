<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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

            <form action="Default.aspx" method="post">
              <h1>Login Form</h1>
              <div class="form-group">
                <input type="text" class="form-control" placeholder="User Id" id="txtUserId" name="txtUserId" />
              </div>
              <div class="form-group">
                <input type="password" class="form-control" placeholder="Password" id="txtPassword" name="txtPassword" />
              </div>
              <div>
                <button type="submit" class="btn btn-default submit" name="submit">SIGN IN</button>
                <a style="display:none;" class="reset_pass to_forgot" href="#forgot_pass">Lost your password?</a>
              </div>
              
              <div class="input-error form-control-input" id="divError" runat="server" style="color: Red;" visible="false">Invalid User Id or Password</div>

              <div class="clearfix"></div>

              <div class="separator">
                <div>
                  <p>©<%= DateTime.Now.Year %> All Rights Reserved. Pani Walay. Privacy and Terms</p>
                </div>
              </div>
            </form>
          </section>
        </div>

        <div id="forgot" class="animate form registration_form">
          <section class="login_content">
            <form>
              <h1>Forgot Password</h1>
              <div>
                <input type="email" class="form-control" placeholder="Email" required="" />
              </div>
              <div>
                <a class="btn btn-default submit" href="index.html">Submit</a>
              </div>

              <div class="clearfix"></div>

              <div class="separator">
                <p class="change_link">Already a member ?
                  <a href="#signin" class="to_register"> Log in </a>
                </p>

                <div class="clearfix"></div>
                <br />

                <div>
                  <h1><i class="fa fa-medium"></i>  Pani Walay!</h1>
                  <p>©<%= DateTime.Now.Year %> All Rights Reserved.  Pani Walay. Privacy and Terms</p>
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

            $("form").submit(function () {

                var err = false;
                var alreadyfocus = false;

                if ($("#txtUserId").val() == '') {
                    $("#txtUserId").parent().addClass('has-error');
                    $("#txtUserId").focus();
                    err = true;
                    alreadyfocus = true;
                }
                else {
                    $("#txtUserId").parent().removeClass('has-error');
                }

                if ($("#txtPassword").val() == '') {
                    $("#txtPassword").parent().addClass('has-error');

                    if (!alreadyfocus) {
                        $("#txtPassword").focus();
                        alreadyfocus = true;
                    }

                    err = true;
                }
                else {
                    $("#txtPassword").parent().removeClass('has-error');
                }


                if (err == true)
                    return false;
            });
        });

    </script>

  </body>
</html>
