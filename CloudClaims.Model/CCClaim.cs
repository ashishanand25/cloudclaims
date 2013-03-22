//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CloudClaims.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CCClaim
    {
        public CCClaim()
        {
            this.CCClaimPersons = new HashSet<CCClaimPerson>();
            this.CCClaimDropdownValues = new HashSet<CCClaimDropdownValue>();
            this.CCClaimTextboxValues = new HashSet<CCClaimTextboxValue>();
            this.CCClaimCheckboxValues = new HashSet<CCClaimCheckboxValue>();
            this.CCClaimMedias = new HashSet<CCClaimMedia>();
        }
    
        public int claimID { get; set; }
        public System.Guid claimGUID { get; set; }
        public string claimAlias1 { get; set; }
        public string claimAlias2 { get; set; }
        public string claimAlias3 { get; set; }
        public int clientID { get; set; }
    
        public virtual ICollection<CCClaimPerson> CCClaimPersons { get; set; }
        public virtual CCClient CCClient { get; set; }
        public virtual ICollection<CCClaimDropdownValue> CCClaimDropdownValues { get; set; }
        public virtual ICollection<CCClaimTextboxValue> CCClaimTextboxValues { get; set; }
        public virtual ICollection<CCClaimCheckboxValue> CCClaimCheckboxValues { get; set; }
        public virtual ICollection<CCClaimMedia> CCClaimMedias { get; set; }
    }
}
