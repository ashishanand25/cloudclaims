using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class SignIn {
        public Guid userGUID { get; set; }
        public int clientID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }


}