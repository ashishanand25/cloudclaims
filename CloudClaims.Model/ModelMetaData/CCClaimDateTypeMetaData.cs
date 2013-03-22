using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudClaims.Model {
    [MetadataType(typeof(CCClaimDateType))]
    public partial class CCClaimDateType {
    }
    
    public class CCClaimDateTypeMetaData {
        [DisplayName("Claim Event Date Type")]
        [StringLength(30)]
        public object claimDateType { get; set; }
    }
}
