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
    
    public partial class CCSetting_ClaimTextboxTypes
    {
        public CCSetting_ClaimTextboxTypes()
        {
            this.CCClaimTextboxValues = new HashSet<CCClaimTextboxValue>();
        }
    
        public int textboxTypeID { get; set; }
        public int clientID { get; set; }
        public string textboxName { get; set; }
        public Nullable<bool> textboxDisabled { get; set; }
        public Nullable<bool> textboxDeleted { get; set; }
    
        public virtual ICollection<CCClaimTextboxValue> CCClaimTextboxValues { get; set; }
        public virtual CCClient CCClient { get; set; }
    }
}
