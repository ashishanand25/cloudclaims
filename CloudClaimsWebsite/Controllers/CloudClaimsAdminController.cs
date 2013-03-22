using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudClaimsWebsite.Models;

namespace CloudClaimsWebsite.Controllers
{
    public class CloudClaimsAdminController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Map(int id)
        {
            try
            {
                using (var db = new CloudClaimsEntities())
                {

                    DateTime dateFrom = (id != null && id > 1) ? DateTime.Now.AddDays(-id) : DateTime.Now.AddDays(-1);
                    List<string> IPAddresses = new List<string>();
                    IPAddresses = db.IPLogs
                        .Where(i => i.timeStamp <= DateTime.Now)
                        .Where(i => i.timeStamp >= dateFrom)
                        .Select(i => i.IPAddress)
                        .ToList();
                    return View(IPAddresses);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Subscribers()
        {
            List<emailSubscription> newSubscribers = new List<emailSubscription>();
            newSubscribers = new CloudClaimsEntities()
                .emailSubscriptions
                .OrderByDescending(x => x.subscriberID)
                .ToList();
            return PartialView("Subscribers", newSubscribers);
        }
    }
}
