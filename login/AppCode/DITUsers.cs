using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DIT;

namespace DIT
{
    public class HQUserItem
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public string Email;
        public string Username;
        public string Password;
        public DateTime Birthday;
        public int Gender;
        public string Phone;
        public String PreferedLanguage;
        public string VerificationCode;
        public DateTime VerificationExpiry;
        public Int64 Hours;
        public Int64 BonusHours;
        public DateTime Created;
        public string SignUpRef;

    }


    public class DITUsers
    {
        private static string ConnectionString = "Server=192.168.1.19;Initial Catalog=dev;user id=user;Password=1";
        public static HQUserItem GetUsersByLogin(string username)
        {
            HQUserItem item = new HQUserItem();
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string selectQuery = "SELECT login,first_name,last_name,email,dob,mobile,gender,lang FROM users WHERE login=@username";
            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    item.Username = reader["login"].ToString();
                    item.FirstName = reader["first_name"].ToString();
                    item.LastName = reader["last_name"].ToString();
                    item.Phone = reader["mobile"].ToString();
                    item.Email = reader["email"].ToString();

                    if (!reader.IsDBNull(reader.GetOrdinal("gender")))
                        item.Gender = Convert.ToInt32(reader["gender"]);
                    if (!reader.IsDBNull(reader.GetOrdinal("dob")))
                        item.Birthday = Convert.ToDateTime(reader["dob"]);

                    item.PreferedLanguage = reader["lang"].ToString().ToLower();

                }
            }
            connection.Close();
            return item;
        }
      

        public static bool SetData(HQUserItem item)
        {
           
            
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();

                string updateQuery = "UPDATE users SET first_name=@first_name, last_name=@last_name, email=@email, dob=@dob, mobile=@mobile, gender=@gender, lang=@lang where login=@login";

                SqlCommand cmd = new SqlCommand(updateQuery, connection);


                if (item.Username == "")
                {
                    cmd.Parameters.AddWithValue("@login", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@login", item.Username);
                }


                if (item.FirstName == "")
                {
                    cmd.Parameters.AddWithValue("@first_name", DBNull.Value);
                }

                else
                {
                    cmd.Parameters.AddWithValue("@first_name", item.FirstName);
                }


                if (item.LastName == "")
                {
                    cmd.Parameters.AddWithValue("@last_name", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@last_name", item.LastName);
                }

                if (item.Email == "")
                {
                    cmd.Parameters.AddWithValue("@email", DBNull.Value);
                }
                else
                {
                    if (CheckEmail(item.Email, item.Username))
                    {
                    }

                    else if (Regex.Match(item.Email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$").Success)
                    {
                        cmd.Parameters.AddWithValue("@email", item.Email);
                    }
                }
                if (item.Birthday==null)
                {
                    cmd.Parameters.AddWithValue("@dob", DBNull.Value);
                }
                else
                {
                  
                    cmd.Parameters.AddWithValue("@dob", item.Birthday);
                }

                if (item.Phone == "")
                {
                    cmd.Parameters.AddWithValue("@mobile", DBNull.Value);
                }
                else if (Regex.Match(item.Phone, @"^([0-9]{9,15})$").Success)
                {
                    cmd.Parameters.AddWithValue("@mobile", item.Phone);
                }

                if (item.Gender == 0)
                {
                    cmd.Parameters.AddWithValue("@gender", DBNull.Value);
                }
                else if (item.Gender != 0)
                {
                    cmd.Parameters.AddWithValue("@gender", item.Gender);
                }

                if (item.PreferedLanguage == "")
                {
                    cmd.Parameters.AddWithValue("@lang", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@lang", item.PreferedLanguage);
                }

                cmd.ExecuteNonQuery();
                connection.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void SetNewSalt(string salt, string login)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "UPDATE users SET salt = @salt WHERE login = @login";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.ExecuteNonQuery();
            }
        }

        public static void SetNewPassword(string password, string login)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string sql = "UPDATE users SET password = @password WHERE login = @login";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool CheckEmail(string myEmail, string userLogin)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT email FROM users WHERE login NOT IN (SELECT login FROM users WHERE login=@login) AND email=@email";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@login", userLogin);
                cmd.Parameters.AddWithValue("@email", myEmail);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (!DITHelper.isEmptyString(reader["email"].ToString()))
                        {
                            return true; // not empty = have email value.
                        }
                    }
                }
            }
            return false;//empty= dont have email value.
        }


        public static bool CheckCurrentPassword(string login, string currentpassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT password,salt FROM users WHERE login = @login";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@login", login);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            string salt = reader["salt"].ToString();
                            string CurrentPassword = GenerateHash(currentpassword, salt);

                            if (reader["password"].ToString() == CurrentPassword)
                            {

                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool CheckUsernameAndPassword(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(Config.ConnectionString))
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

                        password = DITUsers.GenerateHash(password, salt);

                        if (reader["password"].ToString() == password)
                        {
                            return true;
                        }


                    }
                }
            }
            return false;
        }


        #region Security
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
    }

   
}