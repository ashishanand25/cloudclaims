using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClaimsWebsite.Controllers
{
    public class HomeController : Controller
    {
        public string GetUserIP()
        {
            if (IsValidIP(Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]))
                return Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"];
            else
                return "";
        }

        private Boolean IsValidIP(string ip)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}"))
            {
                string[] ips = ip.Split('.');
                if (ips.Length == 4 || ips.Length == 6)
                {
                    if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256
                        & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                        return !IsCrawlerRequest();
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private Boolean IsCrawlerRequest()
        {
            var crawlerList = new String[] { "google", "bing", "msn", "yahoo", "baidu", "sosospider", "sogou", "youdao" };
            if (!String.IsNullOrEmpty(Request.UserAgent))
                foreach (String bot in crawlerList)
                    if (Request.UserAgent.ToLower(CultureInfo.InvariantCulture).Contains(bot))
                        return true; // It is a crawler
            return false;
        }

        [HttpGet]
        public ActionResult Index(string l, string r)
        {
            try
            {
                if (l != "1" && GetUserIP().Length > 1)
                {
                    using (SqlConnection c1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        SqlCommand command = c1.CreateCommand();
                        command.CommandText = "AddIPLog";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@IPAddress", GetUserIP()));
                        if (r != null && r.Length > 0)
                            command.Parameters.Add(new SqlParameter("@data", r));
                        c1.Open();
                        command.ExecuteNonQuery();
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {   //Still return the view, maybe the db didn't respond and that shouldn't make the site go down!      
                return View();
            }
        }

    }
}
