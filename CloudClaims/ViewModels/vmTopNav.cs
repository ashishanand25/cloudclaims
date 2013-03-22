using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class vmTopNav {
        public string UserFName { get; set; }

        public vmTopNav(CCPerson p) {
            
                this.UserFName = p.personFName;
            
            
        }
    }
}