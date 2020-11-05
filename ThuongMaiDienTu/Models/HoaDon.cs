using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class HoaDon
    {
        [Key]
        public int mahd { get; set; }
        public int makh { get; set; }
        public double tongtien { get; set; }
        public KhachHang KhachHang { get; set; }
        public ICollection<ChiTietHoaDon> DSCTHD { get; set; }

        public static void addHoaDon(int makh, double tongtien)
        {
            using (var db = new Context())
            {
                db.HoaDon.Add(new HoaDon{ makh = makh,tongtien=tongtien });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
    }
}