namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateColumns : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clans", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Clans", "DateModified", c => c.DateTime());
            AlterColumn("dbo.Ninjas", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.Ninjas", "DateModified", c => c.DateTime());
            AlterColumn("dbo.NinjaEquipments", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.NinjaEquipments", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NinjaEquipments", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.NinjaEquipments", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Ninjas", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Ninjas", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Clans", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Clans", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
