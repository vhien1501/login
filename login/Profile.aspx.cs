using System;
using DIT;
using System.Text.RegularExpressions;

namespace changeprofile_masterpage
{
    public partial class Profile : System.Web.UI.Page
    {
        private const int SALTSIZE = 64;

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAndSet();

            string HQUser =  Session["hq_user"].ToString();

            lblUsers.Text = HQUser;

            if (!IsPostBack)
            {

                DITUsers.GetUsersByLogin(HQUser);
                lblUsers.Text = HQUsersItem.Login;
                txtFirstName.Text = HQUsersItem.Firstname;
                txtLastName.Text = HQUsersItem.Lastname;
                txtPhone.Text = HQUsersItem.Phone;
                txtEmail.Text = HQUsersItem.Email;
                if (HQUsersItem.Gender == 1)
                {
                    ddlGender.SelectedIndex = 1;

                }
                else if (HQUsersItem.Gender == 2)
                {
                    ddlGender.SelectedIndex = 2;
                }
                txtDOB.Text = HQUsersItem.Dob;
                if (HQUsersItem.Language == "en")
                {
                    ddlLanguage.SelectedIndex = 0;
                }
                else if (HQUsersItem.Language == "ko")
                {
                    ddlLanguage.SelectedIndex = 1;
                }
                else if (HQUsersItem.Language == "vi")
                {
                    ddlLanguage.SelectedIndex = 2;
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
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

            if (ddlGender.SelectedIndex == 0)
            {
                DITHelper.ShowMessage(this, MessageType.Error, "Please choose your gender.");
                return;
            }


            if (DITUsers.SetData(HQUsersItem.Login, HQUsersItem.Firstname, HQUsersItem.Lastname, HQUsersItem.Phone, HQUsersItem.Email, HQUsersItem.Gender, HQUsersItem.Dob, HQUsersItem.Language))
            {
                DITHelper.ShowMessage(this, MessageType.Success, "Update successfull!");
            }
            else
            {
                if (DITUsers.CheckEmail(txtEmail.Text, lblUsers.Text))
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

            if (DITUsers.CheckCurrentPassword(lblUsers.Text, txtCurrentPassword.Text))
            {
                if (txtNewPassword.Text == txtConfirmPassword.Text)
                {

                    if (txtConfirmPassword.Text.Length > 6)
                    {
                        string salt = DITUsers.GenerateSalt(SALTSIZE);
                        DITUsers.SetNewSalt(salt, lblUsers.Text);

                        string hash = DITUsers.GenerateHash(txtConfirmPassword.Text, salt);
                        DITUsers.SetNewPassword(hash, lblUsers.Text);

                        DITHelper.ShowMessage(this, MessageType.Error, "Success");
                    }
                    else
                    {
                        DITHelper.ShowMessage(this, MessageType.Error, "at least 6 letters");
                    }
                }
                else
                {
                    DITHelper.ShowMessage(this, MessageType.Error, "Confirm Password Wrong");

                }
            }
            else
            {

                DITHelper.ShowMessage(this, MessageType.Error, "Current Password Wrong");
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangeCurrentPassword();
        }

        private void GetAndSet()
        {
            HQUsersItem.Login = lblUsers.Text;
            HQUsersItem.Firstname = txtFirstName.Text;
            HQUsersItem.Lastname = txtLastName.Text;
            HQUsersItem.Email = txtEmail.Text;
            HQUsersItem.Dob = txtDOB.Text;
            HQUsersItem.Phone = txtPhone.Text;
            HQUsersItem.Gender = Convert.ToInt32(ddlGender.SelectedValue);
            HQUsersItem.Language = ddlLanguage.SelectedValue;
        }

    }
}