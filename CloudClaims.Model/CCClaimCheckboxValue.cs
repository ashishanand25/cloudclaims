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
    
    public partial class CCClaimCheckboxValue
    {
        public int recordID { get; set; }
        public int clientID { get; set; }
        public int claimID { get; set; }
        public int checkboxTypeID { get; set; }
        public bool @checked { get; set; }
    
        public virtual CCClaim CCClaim { get; set; }
        public virtual CCClient CCClient { get; set; }
        public virtual CCSetting_ClaimCheckboxTypes CCSetting_ClaimCheckboxTypes { get; set; }
    }
}