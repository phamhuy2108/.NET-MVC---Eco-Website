namespace ThuongMaiDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LichSuMuaHangs",
                c => new
                    {
                        lichsumuahang = c.Int(nullable: false, identity: true),
                        makh = c.Int(nullable: false),
                        masanpham = c.Int(nullable: false),
                        soluong = c.Int(nullable: false),
                        thanhtien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.lichsumuahang)
                .ForeignKey("dbo.KhachHangs", t => t.makh, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.masanpham, cascadeDelete: true)
                .Index(t => t.makh)
                .Index(t => t.masanpham);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LichSuMuaHangs", "masanpham", "dbo.SanPhams");
            DropForeignKey("dbo.LichSuMuaHangs", "makh", "dbo.KhachHangs");
            DropIndex("dbo.LichSuMuaHangs", new[] { "masanpham" });
            DropIndex("dbo.LichSuMuaHangs", new[] { "makh" });
            DropTable("dbo.LichSuMuaHangs");
        }
    }
}
