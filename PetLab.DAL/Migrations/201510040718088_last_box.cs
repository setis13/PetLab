namespace PetLab.DAL.Context
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last_box : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.equipment", "last_box", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.equipment", "last_box");
        }
    }
}
