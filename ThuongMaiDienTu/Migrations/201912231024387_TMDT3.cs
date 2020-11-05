namespace ThuongMaiDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChiTietHoaDons", "mactgiohang", "dbo.ChiTietGioHangs");
            DropIndex("dbo.ChiTietHoaDons", new[] { "mactgiohang" });
            DropPrimaryKey("dbo.ChiTietHoaDons");
            AddColumn("dbo.ChiTietGioHangs", "ChiTietHoaDon_macthoadon", c => c.Int());
            AddColumn("dbo.ChiTietHoaDons", "macthoadon", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ChiTietHoaDons", "ptthanhtoan", c => c.String());
            AddColumn("dbo.ChiTietHoaDons", "ptvanchuyen", c => c.String());
            AddPrimaryKey("dbo.ChiTietHoaDons", "macthoadon");
            CreateIndex("dbo.ChiTietGioHangs", "ChiTietHoaDon_macthoadon");
            AddForeignKey("dbo.ChiTietGioHangs", "ChiTietHoaDon_macthoadon", "dbo.ChiTietHoaDons", "macthoadon");
            DropColumn("dbo.ChiTietHoaDons", "mactgiohang");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChiTietHoaDons", "mactgiohang", c => c.Int(nullable: false));
            DropForeignKey("dbo.ChiTietGioHangs", "ChiTietHoaDon_macthoadon", "dbo.ChiTietHoaDons");
            DropIndex("dbo.ChiTietGioHangs", new[] { "ChiTietHoaDon_macthoadon" });
            DropPrimaryKey("dbo.ChiTietHoaDons");
            DropColumn("dbo.ChiTietHoaDons", "ptvanchuyen");
            DropColumn("dbo.ChiTietHoaDons", "ptthanhtoan");
            DropColumn("dbo.ChiTietHoaDons", "macthoadon");
            DropColumn("dbo.ChiTietGioHangs", "ChiTietHoaDon_macthoadon");
            AddPrimaryKey("dbo.ChiTietHoaDons", "mactgiohang");
            CreateIndex("dbo.ChiTietHoaDons", "mactgiohang");
            AddForeignKey("dbo.ChiTietHoaDons", "mactgiohang", "dbo.ChiTietGioHangs", "mactgiohang");
        }
    }
}
