﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CCPortalEntities : DbContext
    {
        public CCPortalEntities()
            : base("name=CCPortalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CCClaimDate> CCClaimDates { get; set; }
        public DbSet<CCClaimDateType> CCClaimDateTypes { get; set; }
        public DbSet<CCClaimPerson> CCClaimPersons { get; set; }
        public DbSet<CCClaim> CCClaims { get; set; }
        public DbSet<CCClientOrganization> CCClientOrganizations { get; set; }
        public DbSet<CCClient> CCClients { get; set; }
        public DbSet<CCCustomLabel> CCCustomLabels { get; set; }
        public DbSet<CCGenericContactCard> CCGenericContactCards { get; set; }
        public DbSet<CCGenericContactCardType> CCGenericContactCardTypes { get; set; }
        public DbSet<CCPerson> CCPersons { get; set; }
        public DbSet<CCSetting_ClaimDropdownTypes> CCSetting_ClaimDropdownTypes { get; set; }
        public DbSet<CCSetting_ClaimDropdownValues> CCSetting_ClaimDropdownValues { get; set; }
        public DbSet<CCSetting_ClaimPersonnelTypes> CCSetting_ClaimPersonnelTypes { get; set; }
        public DbSet<CCClaimDropdownValue> CCClaimDropdownValues { get; set; }
        public DbSet<CCClaimTextboxValue> CCClaimTextboxValues { get; set; }
        public DbSet<CCSetting_ClaimTextboxTypes> CCSetting_ClaimTextboxTypes { get; set; }
        public DbSet<CCSetting_ClaimCheckboxTypes> CCSetting_ClaimCheckboxTypes { get; set; }
        public DbSet<CCClaimCheckboxValue> CCClaimCheckboxValues { get; set; }
        public DbSet<CCSetting_CustomFormSections> CCSetting_CustomFormSections { get; set; }
        public DbSet<CCSetting_CustomFormSectionSectionItems> CCSetting_CustomFormSectionSectionItems { get; set; }
        public DbSet<CCSetting_CustomForms> CCSetting_CustomForms { get; set; }
        public DbSet<CCClaimMedia> CCClaimMedias { get; set; }
    }
}
