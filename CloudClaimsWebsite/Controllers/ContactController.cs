using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudClaimsWebsite.Controllers;

namespace CloudClaimsWebsite.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult JsonDepositEmailAddress(string email)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (SqlConnection c1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {


                        SqlCommand command = c1.CreateCommand();
                        command.CommandText = "AddEmailSubscription";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@emailAddress", email));
                        command.Parameters.Add(new SqlParameter("@IPAddress", new HomeController().GetUserIP()));
                        c1.Open();
                        command.ExecuteNonQuery();
                        return Json(new { success = true });


                    }

                }
                return Json(new { success = false });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Exception calling JSonAddEmailSubscription SP! --> " + e.Message);
                //throw new Exception("Exception calling JSonAddEmailSubscription SP! --> " + e.Message);
                return Json(new { success = false });
            }

        }

    }
}
