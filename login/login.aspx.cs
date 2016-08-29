using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using DIT;

namespace login
{
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (DITUsers.CheckUsernameAndPassword(txtID.Text, txtPassword.Text))
            {
                Session["hq_user"] = txtID.Text;
                Response.Redirect("Profile.aspx");
            }
            else
            {
                DITHelper.ShowMessageSelf(this, MessageType.Error, "user isn't exist or passwrd incorrect");
            }
        }
    }
}