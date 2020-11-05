using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class LichSuMuaHang
    {
        [Key]
        public int lichsumuahang { get; set; }
        public int makh { get; set; }
        public int masanpham { get; set; }
        public int soluong { get; set; }
        public double thanhtien { get; set; }
        public SanPham SanPham { get; set; }
        public KhachHang KhachHang { get; set; }
        public static void addLSMuaHang(int makh, int masanpham, int soluong, double thanhtien)
        {
            using (var db = new Context())
            {
                db.LichSuMuaHang.Add(new LichSuMuaHang { makh = makh, masanpham = masanpham, soluong = soluong, thanhtien = thanhtien });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<LichSuMuaHang> ListLichSuMuaHang()
        {
            List<LichSuMuaHang> b = null;
            using (var db = new Context())
            {
                var c = db.LichSuMuaHang.ToList();
                b = c;
                db.Dispose();
            }
            return b;
        }
    }
}