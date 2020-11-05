namespace ThuongMaiDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coupons", "check", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Coupons", "check");
        }
    }
}
