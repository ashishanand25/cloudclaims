using CloudClaims.Data;
using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class vmPersonPicker {
        public CustomEnums.PersonType personType { get; set; }
        public List<CCPerson> person { get; set; }
        public int mileage { get { return 100; } }

        //public CCGenericContactCard contactCard { get; set; }

        //This commend was added in the main branch. Lets see if this shows up in the child branch AFTER MERGING
        public string test { get; set; }
    }
}