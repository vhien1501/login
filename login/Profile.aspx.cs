using System;
using DIT;
using System.Text.RegularExpressions;

namespace changeprofile_masterpage
{
    public partial class Profile : System.Web.UI.Page
    {
        private const int SALTSIZE = 64;
        private string HQUser;
        HQUserItem items;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["hq_user"] != null)
            {
                HQUser = Session["hq_user"].ToString();
                lblUsers.Text = "Welcome to HQ " + HQUser;
            }
            else
            {
                Response.Redirect("login.aspx");
            }


            if (!IsPostBack)
            {

                items = DITUsers.GetUsersByLogin(HQUser);
                txtFirstName.Text = items.FirstName;
                txtLastName.Text = items.LastName;
                txtPhone.Text = items.Phone;
                txtEmail.Text = items.Email;
                if (items.Gender == 1)
                {
                    ddlGender.SelectedIndex = 1;

                }
                else if (items.Gender == 2)
                {
                    ddlGender.SelectedIndex = 2;
                }
                txtDOB.Text = Convert.ToString(items.Birthday);
                if (items.PreferedLanguage == "en")
                {
                    ddlLanguage.SelectedIndex = 0;
                }
                else if (items.PreferedLanguage == "ko")
                {
                    ddlLanguage.SelectedIndex = 1;
                }
                else if (items.PreferedLanguage == "vi")
                {
                    ddlLanguage.SelectedIndex = 2;
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            items = new HQUserItem();
            items.Username = HQUser;
            items.FirstName = txtFirstName.Text;
            items.LastName = txtLastName.Text;
            items.Email = txtEmail.Text;
            items.Gender = Convert.ToInt32(ddlGender.SelectedValue);
            items.Birthday = Convert.ToDateTime(txtDOB.Text);
            items.PreferedLanguage = ddlLanguage.SelectedValue;
            items.Phone = txtPhone.Text;
            if (DITHelper.isEmptyString(txtFirstName.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your first name.");
                return;
            }

            if (DITHelper.isEmptyString(txtLastName.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your last name.");
                return;
            }

            if (DITHelper.isEmptyString(txtEmail.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your email.");
                return;
            }

            if (DITHelper.isEmptyString(txtPhone.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your phone.");
                return;
            }
            if (DITHelper.isEmptyString(txtDOB.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your Date of Birth.");
                return;
            }


            if (ddlGender.SelectedIndex == 0)
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please choose your gender.");
                return;
            }


            if (DITUsers.SetData(items))
            {
                DITHelper.ShowMessage(this, MessageType.Success, "Update successfull!");
            }
            else
            {
                if (DITUsers.CheckEmail(txtEmail.Text, HQUser))
                {
                    DITHelper.ShowMessage(this, MessageType.Error, txtEmail.Text + " was used.");
                }
                else
                {
                    DITHelper.ShowMessage(this, MessageType.Error, "Fail To Update");
                }
            }
        }


        private void ChangeCurrentPassword()
        {
            if (DITHelper.isEmptyString(txtCurrentPassword.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your current password.");
                return;
            }

            if (DITHelper.isEmptyString(txtNewPassword.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your new password.");
                return;
            }

            if (DITHelper.isEmptyString(txtConfirmPassword.Text))
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please insert your confirm password.");
                return;
            }

            if (DITUsers.CheckCurrentPassword(HQUser, txtCurrentPassword.Text))
            {
                if (txtNewPassword.Text == txtConfirmPassword.Text)
                {

                    if (txtConfirmPassword.Text.Length > 6)
                    {
                        string salt = DITUsers.GenerateSalt(SALTSIZE);
                        DITUsers.SetNewSalt(salt, HQUser);

                        string hash = DITUsers.GenerateHash(txtConfirmPassword.Text, salt);
                        DITUsers.SetNewPassword(hash, HQUser);

                        DITHelper.ShowMessage(this, MessageType.Success, "Change success");

                        txtCurrentPassword.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPassword.Text = "";
                        
                    }
                    else
                    {
                        DITHelper.ShowMessage(this, MessageType.Error, "Password have at least 6 letters");
                    }
                }
                else
                {
                    DITHelper.ShowMessage(this, MessageType.Error, "Confirm password wrong");

                }
            }
            else
            {

                DITHelper.ShowMessage(this, MessageType.Error, "Current password wrong");
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangeCurrentPassword();
        }




    }
}