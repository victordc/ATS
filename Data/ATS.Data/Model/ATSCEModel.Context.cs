﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATS.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ATSCEEntities : DbContext
    {
        public ATSCEEntities()
            : base("name=ATSCEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CodeTable> CodeTables { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<LeaveCategory> LeaveCategories { get; set; }
        public DbSet<LeavePlan> LeavePlans { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TimeSheetDetail> TimeSheetDetails { get; set; }
        public DbSet<TimeSheetMaster> TimeSheetMasters { get; set; }
        public DbSet<ObjectAccess> ObjectAccesses { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
    }
}
