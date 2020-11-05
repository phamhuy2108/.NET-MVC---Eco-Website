using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class Coupon
    {
        [Key]
        public int macoupon { get; set; }
        public string tencoupon { get; set; }
        public double phantramgiam { get; set; }
        public int makh { get; set; } 
        public bool check { get; set; }
        public KhachHang KhachHang { get; set; }
        public static Coupon CheckCoupon(int id)
        {
            Coupon b = null;
            using (var db = new Context())
            {
                b = db.Coupon.Find(id);
                b.check = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    } 
}