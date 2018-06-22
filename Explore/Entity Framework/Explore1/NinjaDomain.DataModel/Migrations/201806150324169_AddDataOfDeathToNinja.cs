namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataOfDeathToNinja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ninjas", "DateOfDeath", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ninjas", "DateOfDeath");
        }
    }
}
