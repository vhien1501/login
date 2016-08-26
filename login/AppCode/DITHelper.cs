using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace DIT
{
    public class DITHelper
    {

        //public static string SQLFixNull(
        public static void SelectComboByValue(DropDownList combo, string value)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].Value == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }

            // fallback to 0
            combo.SelectedIndex = 0;
        }

        public static void SelectComboByName(DropDownList combo, string value)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].Text == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }

            // fallback to 0
            combo.SelectedIndex = 0;
        }

        //public static string GenerateComboHTML(string comboName, string comboID, List<DITOptionListItem> list, string value)
        //{
        //    string html = "";
        //    bool hasMatch = false;

        //    html += "<select name=\"" + comboName + "\" id=\"" + comboID + "\" class=\"form-control input-sm\">";
        //    html += "<option selected=\"selected\" value=\"" + comboID + "_0\">- Select -</option>";

        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        html += "<option value=\"" + comboID + "_" + list[i].ID.ToString() + "\"";

        //        if (value == list[i].ID.ToString()) {
        //            html += " selected=\"selected\" ";
        //            hasMatch = true;
        //        }


        //        html += ">" + list[i].Name + "</option>";
        //    }

        //    html += "</select>";


        //    if (hasMatch)
        //    {
        //        html = html.Replace("<option selected=\"selected\" value=\"0\">", "<option value=\"0\">");
        //    }



        //    return html;


        //}

        //public static void PopulateComboByValue(DropDownList combo, List<DITOptionListItem> list, bool blank, string comboText)
        //{
        //    combo.Items.Clear();
        //    bool hasBlank = false;
        //    string firstOptionText = "-- Select Item --";
        //    if (!DITHelper.isEmptyString(comboText))
        //    {
        //        firstOptionText = comboText;
        //    }

        //    foreach (DITOptionListItem i in list)
        //    {

        //        if (blank && i.Name == "None")
        //        {
        //            ListItem item = new ListItem(i.Name, i.Value);

        //            //i.Name = "-- Select Item --";
        //            i.Name = firstOptionText;
        //            combo.Items.Insert(0, item);
        //            hasBlank = true;
        //        }
        //        else
        //        {
        //            ListItem item = new ListItem(i.Name, i.Value);
        //            if (!i.Enabled)
        //            {
        //                item.Text += " (disabled)";
        //                item.Attributes.Add("disabled", "true");
        //                item.Attributes.Add("style", "background-color:#f3f3f4;");
        //            }
        //            combo.Items.Add(item);
        //        }
        //    }

        //    if (!hasBlank && blank)
        //    {
        //        // add blank selection at home
        //        //combo.Items.Insert(0, new ListItem("-- Select Item --", "0"));
        //        combo.Items.Insert(0, new ListItem(firstOptionText, "0"));
        //    }
        //}
        //public static void PopulateComboByID(DropDownList combo, List<DITOptionListItem> list, bool blank, bool checkEnabled, string comboText)
        //{
        //    // make everything true
        //    if (!checkEnabled)
        //    {
        //        foreach (DITOptionListItem item in list)
        //        {
        //            item.Enabled = true;
        //        }
        //    }

        //    PopulateComboByID(combo, list, blank, comboText);
        //}

        //public static void PopulateComboByID(DropDownList combo, List<DITOptionListItem> list, bool blank, string comboText)
        //{
        //    combo.Items.Clear();
        //    bool hasBlank = false;
        //    string firstOptionText = "-- Select Item --";
        //    if (!DITHelper.isEmptyString(comboText))
        //    {
        //        firstOptionText = comboText;
        //    }
        //    foreach (DITOptionListItem i in list)
        //    {

        //        if (blank && i.Name == "None")
        //        {
        //            //i.Name = "-- Select Item --";
        //            i.Name = firstOptionText;
        //            ListItem item = new ListItem(i.Name, i.ID.ToString());
        //            combo.Items.Insert(0, item);
        //            hasBlank = true;
        //        }
        //        else
        //        {
        //            ListItem item = new ListItem(i.Name, i.ID.ToString());
        //            if (!i.Enabled)
        //            {
        //                item.Text += " (disabled)";
        //                item.Attributes.Add("disabled", "true");
        //                item.Attributes.Add("style", "background-color:#f3f3f4;");
        //            }
        //            combo.Items.Add(item);
        //        }
        //    }

        //    if (!hasBlank && blank)
        //    {
        //        // add blank selection at start
        //        //combo.Items.Insert(0, new ListItem("-- Select Item --", "0"));
        //        combo.Items.Insert(0, new ListItem(firstOptionText, "0"));
        //    }
        //}
        public static string GeneratePaginationHTML(int records, int currentPage, int perPage)
        {

            int pageRange = 2;// +/- 2 current page

            int pages = (int)Math.Ceiling((double)records / (double)perPage);

            string pagehtml = "<ul class=\"pagination\">";

            if (currentPage > 1)
            {
                pagehtml += "<li><a href=\"?page=" + (currentPage - 1) + "\">&laquo;</a></li>";
            }
            else
            {
                pagehtml += "<li class=\"disabled\"><a href=\"#\">&laquo;</a></li>";
            }

            if (currentPage == pageRange + 2)//show page 1 if current page = pageRange + 2
            {
                pagehtml += "<li><a href=\"?page=1\">1</a></li>";
            }
            else if (currentPage == pageRange + 3) // show page 1 and page 2 if current page = pageRange +3
            {
                pagehtml += "<li><a href=\"?page=1\">1</a></li><li><a href=\"?page=2\">2</a></li>";
            }
            else if (currentPage > (pageRange + 3)) // show page 1, page 2 and [...] if current page > pageRage + 3
            {
                pagehtml += "<li><a href=\"?page=1\">1</a></li><li><a href=\"?page=2\">2</a></li><li class=\"disabled\"><span class=\"gap\">...</span></li>";

            }

            // add page controls
            for (int i = 1; i <= pages; i++)
            {
                if (i >= currentPage - pageRange && i <= currentPage + pageRange)//only show the current page +/- 2 page numbers
                {
                    if (currentPage == i)
                    {
                        pagehtml += "<li class=\"active\"><a href=\"?page=" + i + "\">" + i + "</a></li>";
                    }
                    else
                    {
                        pagehtml += "<li><a href=\"?page=" + i + "\">" + i + "</a></li>";
                    }
                }
            }

            if (currentPage == pages - (pageRange + 1))//show last page if current page = total page - (pageRange + 1)
            {
                pagehtml += "<li><a href=\"?page=" + (pages) + "\">" + pages + "</a></li>";
            }
            else if (currentPage == pages - (pageRange + 2))// show last 2 pages if current page = totalpage - (pageRange +2)
            {
                pagehtml += "<li><a href=\"?page=" + (pages - 1) + "\">" + (pages - 1) + "</a></li><li><a href=\"?page=" + (pages) + "\">" + pages + "</a></li>";
            }
            else if (currentPage < (pages - (pageRange + 2)))// show [..] and last 2 pages if current page < (total page - (pageRange + 2))
            {
                pagehtml += "<li class=\"disabled\"><span class=\"gap\">...</span></li><li><a href=\"?page=" + (pages - 1) + "\">" + (pages - 1) + "</a></li><li><a href=\"?page=" + (pages) + "\">" + pages + "</a></li>";
            }

            if (currentPage < pages) { pagehtml += "<li><a href=\"?page=" + (currentPage + 1) + "\">&raquo;</a></li>"; }
            else { pagehtml += "<li class=\"disabled\"><a href=\"#\">&raquo;</a></li>"; }
            pagehtml += "</ul>";

            return pagehtml;
        }


        public static string GeneratePaginationHTML(int records, int currentPage, int perPage, string data)
        {
            int pageRange = 2;// +/- 3 current page

            int pages = (int)Math.Ceiling((double)records / (double)perPage);

            string pagehtml = "<ul class=\"pagination\">";

            if (currentPage > 1)
            {
                pagehtml += "<li><a href=\"?page=" + (currentPage - 1) + "&" + data + "\">&laquo;</a></li>";
            }
            else
            {
                pagehtml += "<li class=\"disabled\"><a href=\"#\">&laquo;</a></li>";
            }


            if (currentPage == pageRange + 2)//show page 1 if current page = pageRange + 2
            {
                pagehtml += "<li><a href=\"?page=1&" + data + "\">1</a></li>";
            }
            else if (currentPage == pageRange + 3) // show page 1 and page 2 if current page = pageRange +3
            {
                pagehtml += "<li><a href=\"?page=1&" + data + "\">1</a></li><li><a href=\"?page=2&" + data + "\">2</a></li>";
            }
            else if (currentPage > (pageRange + 3)) // show page 1, page 2 and [...] if current page > pageRage + 3
            {
                pagehtml += "<li><a href=\"?page=1&" + data + "\">1</a></li><li><a href=\"?page=2&" + data + "\">2</a></li><li class=\"disabled\"><span class=\"gap\">...</span></li>";

            }


            // add page controls
            for (int i = 1; i <= pages; i++)
            {
                if (i >= currentPage - pageRange && i <= currentPage + pageRange)//only show the current page +/- 2 page numbers
                {
                    if (currentPage == i)
                    {
                        pagehtml += "<li class=\"active\"><a href=\"?page=" + i + "&" + data + "\">" + i + "</a></li>";
                    }
                    else
                    {
                        pagehtml += "<li><a href=\"?page=" + i + "&" + data + "\">" + i + "</a></li>";
                    }
                }
            }

            if (currentPage == pages - (pageRange + 1))//show last page number if current page = total page - (pageRange+1)
            {
                pagehtml += "<li><a href=\"?page=" + (pages) + "&" + data + "\">" + pages + "</a></li>";
            }
            else if (currentPage == pages - (pageRange + 2))// show last 2 pages if current page = totalpage - (pageRange +2)
            {
                pagehtml += "<li><a href=\"?page=" + (pages - 1) + "&" + data + "\">" + (pages - 1) + "</a></li><li><a href=\"?page=" + (pages) + "&" + data + "\">" + pages + "</a></li>";
            }
            else if (currentPage < (pages - (pageRange + 2)))// show [..] and last 2 pages if current page < (total page - (pageRange + 2))
            {
                pagehtml += "<li class=\"disabled\"><span class=\"gap\">...</span></li><li><a href=\"?page=" + (pages - 1) + "&" + data + "\">" + (pages - 1) + "</a></li><li><a href=\"?page=" + (pages) + "&" + data + "\">" + pages + "</a></li>";
            }

            if (currentPage < pages) { pagehtml += "<li><a href=\"?page=" + (currentPage + 1) + "&" + data + "\">&raquo;</a></li>"; }
            else { pagehtml += "<li class=\"disabled\"><a href=\"#\">&raquo;</a></li>"; }
            pagehtml += "</ul>";

            return pagehtml;
        }
        public static int ConvertToInt(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(input);
            }
        }

        public static decimal ConvertToDecimal(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(input);
            }
        }

        public static bool IsInteger(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            int no;

            if (int.TryParse(input, out no)) {
                return true;
            } else {
                return false;
            }
        }


        public static bool IsTimeSpan(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            TimeSpan time;

            if (TimeSpan.TryParse(input, out time))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDecimal(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            decimal no;

            if (decimal.TryParse(input, out no))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDate(string input, bool nullvalid)
        {
            DateTime date;

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {

                if (nullvalid)
                {
                    return true;
                } else
                {
                    return false;
                }
            }

            if (DateTime.TryParse(input, out date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDate(string input)
        {
            DateTime date;

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            if (DateTime.TryParse(input, out date))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string FormatTimeSpan(TimeSpan time)
        {
            return string.Format("{0:00:}{1:00}:{2:00}", (int)time.TotalHours, time.Minutes, time.Seconds);
        }

        public static void ShowMessage(System.Web.UI.Page page, MessageType type, string message)
        {
            switch (type)
            {
                case MessageType.Error:
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).Attributes.Add("class", "alert alert-danger m-t-lg");
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).InnerHtml = "<strong>Error!</strong> " + message;
                    break;

                case MessageType.Warning:
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).Attributes.Add("class", "alert alert-warning m-t-lg");
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).InnerHtml = "<strong>Warning!</strong> " + message;
                    break;

                case MessageType.Success:

                    ((HtmlGenericControl)page.Master.FindControl("divResult")).Attributes.Add("class", "alert alert-success m-t-lg");
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).InnerHtml = "<strong>Success!</strong> " + message;
                    break;

                case MessageType.Clear:
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).Attributes.Clear(); ;
                    ((HtmlGenericControl)page.Master.FindControl("divResult")).InnerHtml = "";
                    break;
                default:

                    break;
            }
        }

        public static void ShowMessageNested(System.Web.UI.Page page, MessageType type, string message)
        {
            switch (type) {
                case MessageType.Error:
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).Attributes.Add("class", "alert alert-danger");
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).InnerHtml = "<strong>Error!</strong> " + message;
                    break;

                case MessageType.Warning:
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).Attributes.Add("class", "alert alert-warning");
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).InnerHtml = "<strong>Warning!</strong> " + message;
                    break;

                case MessageType.Success:

                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).Attributes.Add("class", "alert alert-success");
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).InnerHtml = "<strong>Success!</strong> " + message;
                    break;

                case MessageType.Clear:
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).Attributes.Clear(); ;
                    ((HtmlGenericControl)page.Master.Master.FindControl("divResult")).InnerHtml = "";
                    break;
                default:

                    break;
            }
        }

        public static void ShowMessageSelf(System.Web.UI.Page page, MessageType type, string message)
        {
            switch (type)
            {
                case MessageType.Error:
                    ((HtmlGenericControl)page.FindControl("divResult")).Attributes.Add("class", "alert alert-danger m-t");
                    ((HtmlGenericControl)page.FindControl("divResult")).InnerHtml = "<strong>Error!</strong> " + message;
                    break;

                case MessageType.Warning:
                    ((HtmlGenericControl)page.FindControl("divResult")).Attributes.Add("class", "alert alert-warning m-t");
                    ((HtmlGenericControl)page.FindControl("divResult")).InnerHtml = "<strong>Warning!</strong> " + message;
                    break;

                case MessageType.Success:

                    ((HtmlGenericControl)page.FindControl("divResult")).Attributes.Add("class", "alert alert-success m-t");
                    ((HtmlGenericControl)page.FindControl("divResult")).InnerHtml = "<strong>Success!</strong> " + message;
                    break;

                case MessageType.Clear:
                    ((HtmlGenericControl)page.FindControl("divResult")).Attributes.Clear(); ;
                    ((HtmlGenericControl)page.FindControl("divResult")).InnerHtml = "";
                    break;
                default:

                    break;
            }
        }

        public static bool QuerystringEquals(string key, string value)
        {
            if (key == null)
            {
                return false;
            }
            else
            {
                if (key == value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool QuerystringEquals(NameValueCollection querystrings, string key, string value)
        {
            string result = querystrings[key];

            if (result == null)
            {
                return false;
            }
            else
            {
                if (result == value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        #region String Functions
        public static string Linkify(string text)
        {
            return Regex.Replace(text,
                @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
                "<a target='_blank' href='$1'>$1</a>");
        }

        public static bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static bool isEmptyString(string str)
        {
            if (!string.IsNullOrEmpty(str) &&
                !string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string FitString(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }
        #endregion

        #region Date Functions
        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("about {0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("about {0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("about {0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("about {0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("about {0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("about {0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }

        public static int GetWeek(DateTime date)
        {
            System.Globalization.CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("no");
            System.Globalization.Calendar cal = cult_info.Calendar;
            int weekCount = cal.GetWeekOfYear(date, cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            return weekCount;

        }

        public static DateTime FirstDateOfWeek(int year, int weekNum, CalendarWeekRule rule)
        {
            Debug.Assert(weekNum >= 1);

            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);
            Debug.Assert(firstMonday.DayOfWeek == DayOfWeek.Monday);

            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(jan1, rule, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            DateTime result = firstMonday.AddDays(weekNum * 7);

            return result;
        }

        public static DateTime GetDayOfWeek(DateTime date, DayOfWeek day)
        {
            DateTime d = date;

            if (date.DayOfWeek != DayOfWeek.Sunday)
            {
                // want other day 
                if (day != DayOfWeek.Sunday)
                {
                    d = date.AddDays((int)day - (int)date.DayOfWeek);
                }
                else
                {
                    // want sunday
                    d = date.AddDays(7 - (int)date.DayOfWeek);
                }
            }
            else
            {
                // sunday
                if (day != DayOfWeek.Sunday)
                {
                    d = date.AddDays(-7 + (int)day);
                    //d = date.AddDays(-(7 - (int)day));
                }
            }

            return d;
        }

        public static bool DateIsWeekend(DateTime date)
        {
            if ((int)date.DayOfWeek == 0 || (int)date.DayOfWeek == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region misc stuff
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string GenerateTempName(int length)
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_";

            Random rand = new Random();
            string key = "";

            for (int i = 0; i < length; i++)
            {
                key += characters[rand.Next(0, characters.Length)];
            }

            return key;
        }
        public static string BytesToString(long bytes)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB" };
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return num.ToString() + suf[place];
        }


        #endregion

        //public static List<DITOptionListItem> GetDaysInMonth()
        //{
        //    List<DITOptionListItem> list = new List<DITOptionListItem>();
        //    for (int i = 1; i <= 31; i++)
        //    {
        //        DITOptionListItem item = new DITOptionListItem();
        //        item.Name = i.ToString();
        //        item.Value = i.ToString();
        //        item.ID = i;
        //        item.Enabled = true;
        //        list.Add(item);
        //    }
        //    return list;
        //}
        //public static List<DITOptionListItem> GetMonthsInYear()
        //{
        //    List<DITOptionListItem> list = new List<DITOptionListItem>();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        DITOptionListItem item = new DITOptionListItem();
        //        item.Name = i.ToString();
        //        item.Value = i.ToString();
        //        item.ID = i;
        //        item.Enabled = true;
        //        list.Add(item);
        //    }
        //    return list;
        //}
        //public static List<DITOptionListItem> GetYears()
        //{
        //    List<DITOptionListItem> list = new List<DITOptionListItem>();
        //    for (int i = DateTime.Now.Year; i > DateTime.Now.Year -100; i--)
        //    {
        //        DITOptionListItem item = new DITOptionListItem();
        //        item.Name = i.ToString();
        //        item.Value = i.ToString();
        //        item.ID = i;
        //        item.Enabled = true;
        //        list.Add(item);
        //    }
        //    return list;
        //}
       
    }

    public enum MessageType { Error, Warning, Success, Clear };
}