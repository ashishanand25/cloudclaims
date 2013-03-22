using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudClaims.Model {
    [MetadataType(typeof(CCPersonMetaData))]
    public partial class CCPerson {
    }
    public class CCPersonMetaData {
        [Required]
        [DisplayName("first name")]
        [StringLength(100)]
        public object personFName { get; set; }
        
        [Required]
        [DisplayName("last name")]
        [StringLength(100)]
        public object personLName { get; set; }

        [Required]
        [DisplayName("username")]
        [StringLength(100)]
        public object personUserName { get; set; }

        [Required]
        [DisplayName("password")]
        [StringLength(100)]
        public object personPassword { get; set; }
        
    }
}