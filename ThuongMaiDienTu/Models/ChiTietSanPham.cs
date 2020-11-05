using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThuongMaiDienTu.Models
{
    public class ChiTietSanPham
    {
        [Key]
        //public int machitietsach { get; set; }
        //[ForeignKey("Sach")]
        [ForeignKey("SanPham")]
        public int masanpham { get;set;  }
        public string tenchitiet { get; set; }
        public string chitiet { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}