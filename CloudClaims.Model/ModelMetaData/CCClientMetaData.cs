using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudClaims.Model {
    [MetadataType(typeof(CCClientMetaData))]
    public partial class CCClient {
    }

    public class CCClientMetaData {
        [Required]
        [StringLength(300)]
        public object clientName { get; set; }

        [Required]
        [DisplayName("client notes")]
        [StringLength(2000)]
        public object clientNotes { get; set; }
    }

}
