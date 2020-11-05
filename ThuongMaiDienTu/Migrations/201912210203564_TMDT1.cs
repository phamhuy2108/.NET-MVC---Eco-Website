namespace ThuongMaiDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Coupons", "makh", c => c.Int(nullable: false));
            CreateIndex("dbo.Coupons", "makh");
            AddForeignKey("dbo.Coupons", "makh", "dbo.KhachHangs", "makh", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Coupons", "makh", "dbo.KhachHangs");
            DropIndex("dbo.Coupons", new[] { "makh" });
            DropColumn("dbo.Coupons", "makh");
        }
    }
}
