namespace ThuongMaiDienTu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TMDT5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThongKeSachBans",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        thang = c.String(),
                        soluongsachbanduoc = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ThongKeSachBans");
        }
    }
}
