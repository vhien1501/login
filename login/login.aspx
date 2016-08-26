<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="login.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>HQ Gaming</title>

    <link href="assets/css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" media="screen" />
    <link href="assets/css/animate.css" rel="stylesheet" media="screen" />
    <link href="assets/css/style.css" rel="stylesheet" media="screen" />
    <link href="assets/css/bootstrap-datetimepicker.min.css" rel="stylesheet" media="screen" />

</head>
<body class="top-navigation">
    <form id="MainForm" runat="server">
        <div id="wrapper">
            <div id="page-wrapper" class="gray-bg">
                <div class="row border-bottom white-bg">
                    <nav class="navbar navbar-static-top" role="navigation">
                        <div class="navbar-header">
                            <button aria-controls="navbar" aria-expanded="false" data-target="#navbar" data-toggle="collapse" class="navbar-toggle collapsed" type="button">
                                <i class="fa fa-reorder"></i>
                            </button>
                            <div class="navbar-brand">
                                <h3><strong>HQ Gaming</strong></h3>
                            </div>
                        </div>
                    </nav>
                </div>

                <div class="row m-t">
                    <div class="col-lg-12">
                        <div id="divResult" runat="server">
                        </div>
                    </div>
                </div>

                <div class="wrapper wrapper-content">
                    <div class="container">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Login to HQ Gaming</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Username</label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox CssClass="form-control" ID="txtID" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-sm-4 control-label">Password</label>
                                                    <div class="col-sm-4">
                                                        <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" type="password"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-offset-5 col-sm-2">
                                                        <asp:Button CssClass="btn btn-info" type="submit" ID="btnLogin" runat="server" Text="Sign in" OnClick="btnLogin_Click"/>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="footer">
                    <div>
                        <strong>Copyright</strong> DIT &copy; <% Response.Write(DateTime.Now.Year); %>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/js/jquery-2.1.1.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/inspinia.js"></script>
    <script src="assets/js/metisMenu.min.js"></script>
    <script src="assets/js/jquery.slimscroll.min.js"></script>
    <script src="assets/js/bootbox.min.js" type="text/javascript"></script>
    <script src="assets/js/moment-with-locales.min.js" type="text/javascript"></script>
    <script src="assets/js/moment-duration-format.js" type="text/javascript"></script>
    <script src="assets/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="assets/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="assets/js/Chart.bundle.min.js" type="text/javascript"></script>

</body>
</html>
