using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ThongKeSachBan
    {
        public int id { get; set; }
        public string thang { get; set; }
        public double soluongsachbanduoc { get; set; }
        public static List<ThongKeSachBan> ListSachBan()
        {
            List<ThongKeSachBan> b = null;
            using (var db = new Context())
            {
                var c = db.ThongKeSachBan.ToList();
                b = c;
                db.Dispose();
            }
            return b;
        }
    }
}