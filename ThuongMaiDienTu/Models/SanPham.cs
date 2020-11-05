using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class SanPham
    {
        [Key]

        public int masanpham { get; set; }
        //[ForeignKey("NhaCungCap")]
        public int matl { get; set; }
        public string tensanpham { get; set; }
        public double gia { get; set; }
        public string hinh { get; set; }
        public bool isdeleted { get; set; }
        //public int mactsach { get; set; }
        public ICollection<ChiTietGioHang> DSCTGH { get; set; }
        public ICollection<LichSuMuaHang> DSLSMH { get; set; }

        public virtual ChiTietSanPham ChiTietSach { get; set; }
    }
}