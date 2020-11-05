using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ThuongMaiDienTu.Models
{
    public class Context : DbContext
    {
        public Context() : base("TMDT")
        {
            //string databasename = "NhaSach";
            //this.Database.Connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + databasename + ";Trusted_Connection=Yes";
        }
        public DbSet<ChiTietGioHang> ChiTietGioHang { get; set; }
        public DbSet<ChiTietSanPham> ChiTietSanPham { get; set; }
        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<TheLoai> TheLoai { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<LichSuMuaHang> LichSuMuaHang { get; set; }
        public DbSet<ThongKeSachBan> ThongKeSachBan { get; set; }

    }
}