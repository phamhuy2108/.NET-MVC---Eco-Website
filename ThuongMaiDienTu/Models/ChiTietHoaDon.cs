using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public int macthoadon { get; set; }
        public int mahd { get; set; }
        public int masanpham { get; set; }
        public int soluong { get; set; }
        public double dongia { get; set; }
        public double thanhtien { get; set; }
        public string ptthanhtoan { get; set; }
        public string ptvanchuyen { get; set; }
        public HoaDon HoaDon { get; set; }
        public static void addCTHoaDon(int mahd, int masanpham, int soluong,double dongia, double thanhtien)
        {
            using (var db = new Context())
            {
                db.ChiTietHoaDon.Add(new ChiTietHoaDon { mahd=mahd, masanpham=masanpham, soluong=soluong,dongia=dongia,thanhtien=thanhtien });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
    }
}