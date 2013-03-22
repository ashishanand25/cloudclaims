using CloudClaims.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClaims.ViewModels {
    public class vmClaimDocuments {
        public IEnumerable<CCClaimMedia> claimDocuments { get; set; }
    }
}