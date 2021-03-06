﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Standard.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RoadFlowWebFormEntities : DbContext
    {
        public RoadFlowWebFormEntities()
            : base("name=RoadFlowWebFormEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AppLibrary> AppLibrary { get; set; }
        public DbSet<DBConnection> DBConnection { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Organize> Organize { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleApp> RoleApp { get; set; }
        public DbSet<TempTest> TempTest { get; set; }
        public DbSet<TempTest_CustomForm> TempTest_CustomForm { get; set; }
        public DbSet<TempTest_News> TempTest_News { get; set; }
        public DbSet<TempTest_Purchase> TempTest_Purchase { get; set; }
        public DbSet<TempTest_PurchaseList> TempTest_PurchaseList { get; set; }
        public DbSet<TempTest_WorkOrder> TempTest_WorkOrder { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersApp> UsersApp { get; set; }
        public DbSet<UsersInfo> UsersInfo { get; set; }
        public DbSet<UsersRelation> UsersRelation { get; set; }
        public DbSet<UsersRole> UsersRole { get; set; }
        public DbSet<WorkFlow> WorkFlow { get; set; }
        public DbSet<WorkFlowArchives> WorkFlowArchives { get; set; }
        public DbSet<WorkFlowButtons> WorkFlowButtons { get; set; }
        public DbSet<WorkFlowComment> WorkFlowComment { get; set; }
        public DbSet<WorkFlowDelegation> WorkFlowDelegation { get; set; }
        public DbSet<WorkFlowForm> WorkFlowForm { get; set; }
        public DbSet<WorkFlowTask> WorkFlowTask { get; set; }
        public DbSet<WorkGroup> WorkGroup { get; set; }
    }
}
