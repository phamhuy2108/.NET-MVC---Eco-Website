namespace ThuongMaiDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietGioHangs",
                c => new
                    {
                        mactgiohang = c.Int(nullable: false, identity: true),
                        makh = c.Int(nullable: false),
                        masanpham = c.Int(nullable: false),
                        soluong = c.Int(nullable: false),
                        thanhtien = c.Double(nullable: false),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.mactgiohang)
                .ForeignKey("dbo.GioHangs", t => t.makh, cascadeDelete: true)
                .ForeignKey("dbo.SanPhams", t => t.masanpham, cascadeDelete: true)
                .Index(t => t.makh)
                .Index(t => t.masanpham);
            
            CreateTable(
                "dbo.ChiTietHoaDons",
                c => new
                    {
                        mactgiohang = c.Int(nullable: false),
                        mahd = c.Int(nullable: false),
                        masanpham = c.Int(nullable: false),
                        soluong = c.Int(nullable: false),
                        dongia = c.Double(nullable: false),
                        thanhtien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.mactgiohang)
                .ForeignKey("dbo.ChiTietGioHangs", t => t.mactgiohang)
                .ForeignKey("dbo.HoaDons", t => t.mahd, cascadeDelete: true)
                .Index(t => t.mactgiohang)
                .Index(t => t.mahd);
            
            CreateTable(
                "dbo.HoaDons",
                c => new
                    {
                        mahd = c.Int(nullable: false, identity: true),
                        makh = c.Int(nullable: false),
                        tongtien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.mahd)
                .ForeignKey("dbo.KhachHangs", t => t.makh, cascadeDelete: true)
                .Index(t => t.makh);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        makh = c.Int(nullable: false, identity: true),
                        ho = c.String(),
                        ten = c.String(),
                        diachi = c.String(),
                        sdt = c.String(),
                        email = c.String(),
                        matkhau = c.String(),
                        nhaplaimatkhau = c.String(),
                        quyen = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.makh);
            
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        makh = c.Int(nullable: false),
                        tengiohang = c.String(),
                        tongtien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.makh)
                .ForeignKey("dbo.KhachHangs", t => t.makh)
                .Index(t => t.makh);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        masanpham = c.Int(nullable: false, identity: true),
                        matl = c.Int(nullable: false),
                        tensanpham = c.String(),
                        gia = c.Double(nullable: false),
                        hinh = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.masanpham)
                .ForeignKey("dbo.TheLoais", t => t.matl, cascadeDelete: true)
                .Index(t => t.matl);
            
            CreateTable(
                "dbo.ChiTietSanPhams",
                c => new
                    {
                        masanpham = c.Int(nullable: false),
                        tenchitiet = c.String(),
                        chitiet = c.String(),
                    })
                .PrimaryKey(t => t.masanpham)
                .ForeignKey("dbo.SanPhams", t => t.masanpham)
                .Index(t => t.masanpham);
            
            CreateTable(
                "dbo.Coupons",
                c => new
                    {
                        macoupon = c.Int(nullable: false, identity: true),
                        tencoupon = c.String(),
                        phantramgiam = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.macoupon);
            
            CreateTable(
                "dbo.TheLoais",
                c => new
                    {
                        matl = c.Int(nullable: false, identity: true),
                        tentheloai = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.matl);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SanPhams", "matl", "dbo.TheLoais");
            DropForeignKey("dbo.ChiTietGioHangs", "masanpham", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietSanPhams", "masanpham", "dbo.SanPhams");
            DropForeignKey("dbo.GioHangs", "makh", "dbo.KhachHangs");
            DropForeignKey("dbo.ChiTietGioHangs", "makh", "dbo.GioHangs");
            DropForeignKey("dbo.HoaDons", "makh", "dbo.KhachHangs");
            DropForeignKey("dbo.ChiTietHoaDons", "mahd", "dbo.HoaDons");
            DropForeignKey("dbo.ChiTietHoaDons", "mactgiohang", "dbo.ChiTietGioHangs");
            DropIndex("dbo.ChiTietSanPhams", new[] { "masanpham" });
            DropIndex("dbo.SanPhams", new[] { "matl" });
            DropIndex("dbo.GioHangs", new[] { "makh" });
            DropIndex("dbo.HoaDons", new[] { "makh" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "mahd" });
            DropIndex("dbo.ChiTietHoaDons", new[] { "mactgiohang" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "masanpham" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "makh" });
            DropTable("dbo.TheLoais");
            DropTable("dbo.Coupons");
            DropTable("dbo.ChiTietSanPhams");
            DropTable("dbo.SanPhams");
            DropTable("dbo.GioHangs");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.HoaDons");
            DropTable("dbo.ChiTietHoaDons");
            DropTable("dbo.ChiTietGioHangs");
        }
    }
}
