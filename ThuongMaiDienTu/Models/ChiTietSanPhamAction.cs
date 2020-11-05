using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiTietSanPhamAction
    {
        public static void addCTSach(int masanpham, string tenchitiet, string chitiet)
        {
            using (var db = new Context())
            {
                db.ChiTietSanPham.Add(new ChiTietSanPham { masanpham=masanpham,tenchitiet=tenchitiet,chitiet=chitiet });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static void addCTSach1 (int masanpham)
        {
            using (var db = new Context())
            {
                db.ChiTietSanPham.Add(new ChiTietSanPham { masanpham = masanpham });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static ChiTietSanPham FindCTSach(int id)
        {
            ChiTietSanPham b = null;
            using (var db = new Context())
            {
                b = db.ChiTietSanPham.Find(id);
                db.Dispose();
            }
            return b;
        }
        public static ChiTietSanPham UpdateCTSach(int masach, string tenchitiet,string chitiet)
        {
            ChiTietSanPham b = null;
            using (var db = new Context())
            {
                b = db.ChiTietSanPham.Find(masach);
                b.tenchitiet = tenchitiet;
                b.chitiet = chitiet;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    }
}