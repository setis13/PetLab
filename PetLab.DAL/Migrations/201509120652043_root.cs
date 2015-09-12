namespace PetLab.DAL.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class root : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.order", "shift_id", "dbo.shift");
            DropIndex("dbo.order", new[] { "shift_id" });
            DropColumn("dbo.order", "shift_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.order", "shift_id", c => c.Int(nullable: false));
            CreateIndex("dbo.order", "shift_id");
            AddForeignKey("dbo.order", "shift_id", "dbo.shift", "shift_id");
        }
    }
}
