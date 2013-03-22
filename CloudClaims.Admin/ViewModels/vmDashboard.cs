using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.Admin.ViewModels {
    public class vmDashboard {
        public int x { get; set; }
        public IEnumerable<CCClient> clients {
            get {
                return new CCPortalEntities().CCClients.ToList();
            }
        }

    }
}