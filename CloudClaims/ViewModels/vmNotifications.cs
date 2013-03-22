using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CloudClaims.Data;

namespace CloudClaims.ViewModels {
    public class vmNotifications {
        public string message { get; set; }
        public CustomEnums.NotificationType type { get; set; }
    }
}