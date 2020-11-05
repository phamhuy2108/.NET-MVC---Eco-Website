using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiTietGioHang
    {
        [Key]
        public int mactgiohang { get; set; }
        //[ForeignKey("GioHang")]
        public int makh { get; set; }
        //[ForeignKey("Sach")]
        public int masanpham { get; set; }
        public int soluong { get; set; }
        public double thanhtien { get; set;}
        public SanPham SanPham { get; set; }
        public GioHang GioHang { get; set; }
        public bool isdeleted { get; set; }
        public virtual ChiTietHoaDon ChiTietHoaDon { get; set; }

    }
}