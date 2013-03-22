using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudClaims.Model {
    [MetadataType(typeof(CCGenericContactCardMetaData))]
    public partial class CCGenericContactCard {
    }

    public class CCGenericContactCardMetaData {
        [DisplayName("Address (Line 1)")]
        [StringLength(200)]
        public object genericContactAddrLine1 { get; set; }

        [DisplayName("Address (Line 2)")]
        [StringLength(200)]
        public object genericContactAddrLine2 { get; set; }

        [DisplayName("City")]
        [StringLength(100)]
        public object genericContactCity { get; set; }

        [DisplayName("State")]
        [StringLength(100)]
        public object genericContactState { get; set; }

        [DisplayName("ZIP Code")]
        [StringLength(20)]
        public object genericContactZIP { get; set; }

        [DisplayName("Country")]
        [StringLength(20)]
        public object genericContactCountry { get; set; }

        [DisplayName("Phone (Business)")]
        [StringLength(20)]
        public object genericContactPhone1 { get; set; }

        [DisplayName("Phone (Home)")]
        [StringLength(20)]
        public object genericContactPhone2 { get; set; }

        [DisplayName("Phone (Cell)")]
        [StringLength(20)]
        public object genericContactPhone3 { get; set; }

        [DisplayName("Phone (4)")]
        [StringLength(20)]
        public object genericContactPhone4 { get; set; }

        [DisplayName("Phone (5)")]
        [StringLength(20)]
        public object genericContactPhone5 { get; set; }

        [DisplayName("Fax")]
        [StringLength(20)]
        public object genericContactFax { get; set; }

        [DisplayName("Primary Email")]
        [StringLength(100)]
        public object genericContactEMail1 { get; set; }

        [DisplayName("Secondary Email")]
        [StringLength(100)]
        public object genericContactEMail2 { get; set; }

        [DisplayName("Website")]
        [StringLength(200)]
        public object genericContactWebsite { get; set; }

        
    }
}
