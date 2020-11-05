using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiTietGioHangAction
    {
        public static void addCTGioHang(int makh,int masanpham,int soluong, double thanhtien)
        {
            using (var db = new Context())
            {
                db.ChiTietGioHang.Add(new ChiTietGioHang { makh=makh,masanpham=masanpham,soluong=soluong,thanhtien=thanhtien  });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<ChiTietGioHang> ListCTGioHang()
        {
            List<ChiTietGioHang> b = null;
            using (var db = new Context())
            {
                var c = db.ChiTietGioHang.Where(s => s.isdeleted == false).ToList();
                b = c;
                db.Dispose();
            }
            return b;
        }
        public static List<ChiTietGioHang> ListCTGioHang1()
        {
            List<ChiTietGioHang> b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.ToList();
                db.Dispose();
            }
            return b;
        }
        public static ChiTietGioHang UpdateCTGioHang(int id, int soluong,double thanhtien)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                b.soluong = soluong;
                b.thanhtien = thanhtien;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static void XoaChiTietGioHang(int id)
        {
            using (var db = new Context())
            {
                var ctgiohang = db.ChiTietGioHang.Find(id);
                db.Entry(ctgiohang).State = EntityState.Deleted;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static ChiTietGioHang DaTonTai(int id, int soluong,double thanhtien)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                b.soluong = b.soluong+soluong;
                b.thanhtien = b.thanhtien + thanhtien;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static ChiTietGioHang Xoa(int id)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static ChiTietGioHang FindCTGioHang(int id)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                db.Dispose();
            }
            return b;
        }
    }
}