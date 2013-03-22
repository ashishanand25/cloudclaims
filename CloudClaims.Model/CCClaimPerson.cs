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
    
    public partial class CCClaimPerson
    {
        public CCClaimPerson()
        {
            this.CCClaimMedias = new HashSet<CCClaimMedia>();
        }
    
        public int claimPersonID { get; set; }
        public int clientID { get; set; }
        public int claimID { get; set; }
        public int personID { get; set; }
        public int claimPersonnelTypeID { get; set; }
        public System.DateTime creationDate { get; set; }
        public bool claimPersonDeleted { get; set; }
    
        public virtual CCClaim CCClaim { get; set; }
        public virtual CCClient CCClient { get; set; }
        public virtual CCPerson CCPerson { get; set; }
        public virtual CCSetting_ClaimPersonnelTypes CCSetting_ClaimPersonnelTypes { get; set; }
        public virtual ICollection<CCClaimMedia> CCClaimMedias { get; set; }
    }
}