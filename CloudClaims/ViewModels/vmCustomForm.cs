using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class vmCustomForm {
        public int clientID { get; set; }
        public int formID { get; set; }
        public string formName { get; set; }
        public string formType { get; set; }
        public string serializedForm { get; set; }
    }
}