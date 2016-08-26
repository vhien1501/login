using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using DIT;

namespace login
{
    public partial class login : System.Web.UI.Page
    {
        private static string ConnectionString = "Server=192.168.1.19;Initial Catalog=dev;user id=user;Password=1;";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool CheckUsernameAndPassword(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT password,salt FROM users WHERE login=@login";

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@login", username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        string salt = reader["salt"].ToString();

                        password = GenerateHash(password, salt);

                        if (reader["password"].ToString() == password)
                        {
                            return true;
                        }


                    }
                }
            }
            return false;
        }



        #region Secure
        public static string GenerateSalt(int length)
        {
            byte[] randomArray = new byte[length];
            string randomString;

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);

            randomString = Convert.ToBase64String(randomArray);

            return BitConverter.ToString(randomArray).Replace("-", "").ToLower(); ;

        }
        public static string GenerateHash(string str, string salt)
        {
            byte[] plainText = Encoding.UTF8.GetBytes(str);
            byte[] s = Encoding.UTF8.GetBytes(salt);

            HashAlgorithm algorithm = new SHA512Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }

            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = s[i];
            }

            byte[] hash = algorithm.ComputeHash(plainTextWithSaltBytes);

            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
        #endregion

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (CheckUsernameAndPassword(txtID.Text, txtPassword.Text))
            {
                Session["hq_user"] = txtID.Text;
                Response.Redirect("Profile.aspx");
            }
            else
            {
                Response.Redirect("login.aspx");

                DITHelper.ShowMessage(this, MessageType.Error, "user isn't exist or passwrd incorrect");
            }
        }
    }
}