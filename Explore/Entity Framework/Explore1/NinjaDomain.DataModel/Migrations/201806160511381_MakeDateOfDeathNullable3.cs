namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDateOfDeathNullable3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ninjas", "DateOfDeath", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
        }
    }
}
