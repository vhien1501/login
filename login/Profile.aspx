<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="changeprofile_masterpage.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">

    <div class="wrapper wrapper-content">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>HQ Gaming - Profiles</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                            <asp:Label ID="lblUsers" runat="server" CssClass="control-label col-sm-6" style="font-size: 20px"></asp:Label>
                                    </div>
                                    <div class="form-group">
                                        <label id="lblFirstName" class="col-sm-4 control-label">First Name</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lblLastName" class="col-sm-4 control-label">Last Name</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtLastName" ClientIDMode="Static" CssClass="form-control input-sm"  runat="server"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lblEmail" class="col-sm-4 control-label">Email</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtEmail" class="email" ClientIDMode="Static" CssClass="form-control input-sm"  runat="server"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lblDOB" class="col-sm-4 control-label">Date of Birth</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtDOB" ClientIDMode="Static" CssClass="form-control"  runat="server"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lblPhone" class="col-sm-4 control-label">Phone Number</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtPhone" ClientIDMode="Static" CssClass="form-control input-sm"  runat="server"> </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lblGender" class="col-sm-4 control-label">Gender</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlGender" ClientIDMode="Static" CssClass="form-control input-sm" runat="server">
                                                <asp:ListItem Value="0">Gender </asp:ListItem>
                                                <asp:ListItem Value="1">Male </asp:ListItem>
                                                <asp:ListItem Value="2">Female </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label id="lblLanguage" class="col-sm-4 control-label">Language</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlLanguage" CssClass="form-control input-md" runat="server">
                                                <asp:ListItem Value="en">English </asp:ListItem>
                                                <asp:ListItem Value="ko">Korean </asp:ListItem>
                                                <asp:ListItem Value="vi">Vietnamese </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-offset-5 col-sm-4">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" Width="150" CssClass="btn btn-info" OnClick="btnSave_Click" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Current Password</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox type="password" class="form-control" ID="txtCurrentPassword"  runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">New Password</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox type="password" class="form-control" ID="txtNewPassword" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-4 control-label">Confirm Password</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox type="password" class="form-control" ID="txtConfirmPassword"  runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-offset-5 col-sm-4">
                                            <asp:Button ID="btnChangePassword" ClientIDMode="Static" runat="server" UseSubmitBehavior="false" CssClass="btn btn-info" Text="Change Password" OnClick="btnChangePassword_Click" />
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



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#txtDOB').datetimepicker({
                format: 'DD/MM/YYYY',
                showTodayButton: true,
                showClear: true,
                showClose: true,
                useCurrent: false
            });

            $.validator.addMethod('dropdown', function (value, element) {
                return (value != '0');
            }, 'Please select a option.');

            $("#MainForm").validate();




            $.validator.addMethod("regex", function (value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            }, "Only characters from A-Z");



            $('#txtFirstName').rules('add', {
                required: true,
                messages: {
                    required: "First name is required."
                }
            });


            $('#txtLastName').rules('add', {
                required: true,
                messages: {
                    required: "Last name is required."
                }
            });

            $('#txtDOB').rules('add', {
                required: true,
                messages: {
                    required: "DOB is required."
                }
            });



            $('#txtPhone').rules('add', {
                required: true,
                number: true,
                messages: {
                    required: "Phone is required",
                    number: "Please insert only number."
                }
            });


            $('#txtEmail').rules('add', {
                required: true,
                regex: /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i,
                messages: {
                    required: "Email is required.",
                    regex: "hqgaming@abc.com"
                }
            });


            $('#ddlGender').rules('add', {
                required: true,
                dropdown: true,

            });


            $('#txtConfirmPassword').rules('add', {
                equalTo: "#txtNewPassword",
                messages:
            {
                equalTo: "Password is not match."
            }
            })
        });

    </script>
</asp:Content>
