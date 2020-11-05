using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class SanPhamAction
    {
        public static void addSanPham(int matl, string tensanpham,double gia,string hinh)
        {
            using (var db = new Context())
            {
                db.SanPham.Add(new SanPham {matl = matl, tensanpham = tensanpham,gia=gia,hinh=hinh, isdeleted = false });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<SanPham> ListSanPham()
        {
            List<SanPham> b = null;
            using (var db = new Context())
            {
                var sach = (from c in db.SanPham
                            where c.isdeleted == false
                            select c);
                b = sach.ToList();
                db.Dispose();
            }
            return b;
        }
        public static SanPham FindSanPham(int id)
        {
            SanPham b = null;
            using (var db = new Context())
            {
                b = db.SanPham.Find(id);
                db.Dispose();
            }
            return b;
        }
        public static SanPham UpdateSanPham(int masanpham,int matl,string tensanpham,double gia,string hinh)
        {
            SanPham b = null;
            using (var db = new Context())
            {
                b = db.SanPham.Find(masanpham);
                b.matl = matl; //saiten
                b.tensanpham = tensanpham;
                b.gia = gia;
                b.hinh = hinh;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static SanPham XoaSanPham(int masach)
        {
            SanPham b = null;
            using (var db = new Context())
            {
                b = db.SanPham.Find(masach);
                b.isdeleted=true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    }
}