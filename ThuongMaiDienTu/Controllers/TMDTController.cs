using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuongMaiDienTu.Models;

namespace ThuongMaiDienTu.Controllers
{
    public class TMDTController : Controller
    {
        // GET: TMDT
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult Feature()
        {
            using (var db = new Context())
            {
                if (checkLogin() == false)
                {
                    List<ChiTietGioHang> home = new List<ChiTietGioHang>();
                    home = (List<ChiTietGioHang>)Session["Cart"];
                    if (Session["Cart"] != null)
                    {
                        int quantity = 0;
                        double totalsummary = 0;
                        foreach (var a in home)
                        {
                            quantity = quantity + a.soluong;
                            totalsummary = totalsummary + a.thanhtien;
                        }
                        ViewBag.TotalSummary = totalsummary;
                        Session["QuantityCart"] = quantity;
                    }
                    else
                    {
                        ViewBag.TotalSummary = null;
                        Session["QuantityCart"] = 0;
                    }
                    ViewBag.Feature = Session["Cart"];

                }
                else
                {
                    int accountid = Convert.ToInt32(Session["id"]);
                    var feature = db.ChiTietGioHang.Where(s => s.makh == accountid).ToList();
                    ViewBag.Feature = feature;
                    int quantity = 0;
                    double totalsummary = 0;
                    foreach (var a in feature)
                    {
                        quantity = quantity + a.soluong;
                        totalsummary = totalsummary + a.thanhtien;
                    }
                    ViewBag.TotalSummary = totalsummary;
                    Session["TotalSummary"] = totalsummary;
                    if (quantity != 0)
                    {
                        Session["QuantityCart"] = quantity;
                    }
                    else
                    {
                        Session["QuantityCart"] = 0;
                    }
                }
                //List<ChiTietSanPham> feature = new List<ChiTietSanPham>();
                //feature = (List<ChiTietSanPham>)Session["Cart"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Feature(int[] cartdetailid, int[] quantity, string coupon)
        {
            using (var db = new Context())
            {
                if (checkLogin() == false)
                {
                    if (Session["Cart"] != null)
                    {
                        List<ChiTietGioHang> feature = new List<ChiTietGioHang>();
                        feature = (List<ChiTietGioHang>)Session["Cart"];
                        int index = 0;
                        foreach (var updatequantity in feature)
                        {
                            //if (updatequantity.mactgiohang == cartdetailid)
                            //{
                            updatequantity.soluong = quantity[index];
                            var a = db.SanPham.Where(s => s.masanpham == updatequantity.masanpham).FirstOrDefault();
                            updatequantity.thanhtien = updatequantity.soluong * a.gia;
                            //}
                            index++;
                        }
                        return RedirectToAction("Feature", "TMDT");
                    }
                    else
                    {
                        return RedirectToAction("Feature", "TMDT");
                    }
                }
                else
                {
                    int userid = Convert.ToInt32(Session["id"]);
                    int index = 0;
                    foreach (var c in cartdetailid)
                    {
                        var findcart = db.ChiTietGioHang.Where(s => s.mactgiohang == c && s.makh == userid).FirstOrDefault();
                        var price = db.SanPham.Where(s => s.masanpham == findcart.masanpham).FirstOrDefault();
                        double summary = price.gia * quantity[index];
                        ChiTietGioHangAction.UpdateCTGioHang(findcart.mactgiohang, quantity[index], summary);
                        index++;
                    }
                    if (coupon != "")
                    {
                        var discount = db.Coupon.Where(s => s.tencoupon == coupon && s.check == false && s.makh == userid).FirstOrDefault();
                        if (discount != null)
                        {
                            double totalsummary = Convert.ToDouble(Session["TotalSummary"]);
                            double pricediscount = totalsummary * discount.phantramgiam;
                            double updatetotalsummary = totalsummary - pricediscount;
                            TempData["TotalSummaryUPDATE"] = updatetotalsummary;
                            TempData["Coupon"] = discount.tencoupon;
                        }
                    }
                    //double price = findcart.thanhtien / findcart.soluong;
                    //ChiTietGioHangAction.UpdateCTGioHang(findcart.mactgiohang, quantity, price * quantity);
                    return RedirectToAction("Feature", "TMDT");
                }
            }
        }
        [HttpPost]
        public ActionResult Buy(string coupon, string phuongthucthanhtoan, string phuongthucvanchuyen)
        {
            using (var db = new Context())
            {
                if (checkLogin() == true)
                {
                    //string coupon = TempData["Coupon"].ToString();
                    int userid = Convert.ToInt32(Session["id"]);
                    var timkiem = db.Coupon.Where(s => s.makh == userid && s.tencoupon == coupon && s.check == false).FirstOrDefault();
                    var ctgiohang = ChiTietGioHangAction.ListCTGioHang();
                    var ctgiohang1 = ctgiohang.Where(s => s.isdeleted == false && s.makh == userid);
                    if (timkiem != null)
                    {
                        //Cần sửa lại giá tiền nếu áp coupon
                        Coupon.CheckCoupon(timkiem.macoupon);
                        foreach (var valueindex in ctgiohang1)
                        {
                            LichSuMuaHang.addLSMuaHang(valueindex.makh, valueindex.masanpham, valueindex.soluong, valueindex.thanhtien);
                            ChiTietGioHangAction.XoaChiTietGioHang(valueindex.mactgiohang);
                        }
                    }
                    else
                    {
                        foreach (var valueindex in ctgiohang1)
                        {
                            LichSuMuaHang.addLSMuaHang(valueindex.makh, valueindex.masanpham, valueindex.soluong, valueindex.thanhtien);
                            ChiTietGioHangAction.XoaChiTietGioHang(valueindex.mactgiohang);
                        }
                    }
                }
                else
                {
                    Session.Remove("Cart");
                }

                return RedirectToAction("Feature", "TMDT");
            }
        }
        public ActionResult Sale()
        {
            return View();
        }
        public ActionResult Shop()
        {
            List<SanPham> shop = SanPhamAction.ListSanPham();
            ViewBag.Shop = shop.OrderByDescending(a => a.masanpham);
            return View();
        }
        public ActionResult ProductDetail(int productid)
        {
            using (var db = new Context())
            {
                var productdetail = SanPhamAction.FindSanPham(productid);
                var type = db.TheLoai.Where(s => s.matl == productdetail.matl).FirstOrDefault();
                var description = db.ChiTietSanPham.Where(s => s.masanpham == productdetail.masanpham).FirstOrDefault();
                ViewBag.Description = description;
                ViewBag.Type = type;
                ViewBag.ProductDetail = productdetail;
                ViewBag.ListProduct = SanPhamAction.ListSanPham().OrderByDescending(s => s.masanpham);
                return View();
            }
        }
        public ActionResult Home()
        {
            using (var db = new Context())
            {
                int accountid = Convert.ToInt32(Session["id"]);
                var feature = db.ChiTietGioHang.Where(s => s.makh == accountid).ToList();
                int quantity = 0;
                foreach (var a in feature)
                {
                    quantity = quantity + a.soluong;
                }
                if (quantity != 0)
                {
                    Session["QuantityCart"] = quantity;
                }
                else
                {
                    Session["QuantityCart"] = 0;
                }
                ViewBag.Home = SanPhamAction.ListSanPham();
                return View();
            }
        }
        public ActionResult ManageProduct()
        {
            var listproduct = SanPhamAction.ListSanPham();
            ViewBag.Product = listproduct;

            return View();
        }

        public ActionResult InsertProduct()
        {
            using (var db = new Context())
            {
                ViewBag.Type = TheLoai.ListTheLoai();
                return View();
            }
        }
        [HttpPost]
        public ActionResult InsertProduct(int matl, string tensanpham, double gia, HttpPostedFileBase myfile)
        {
            try
            {
                string _path = "";
                if (myfile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(myfile.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadFiles"), _FileName);
                    myfile.SaveAs(_path);
                    SanPhamAction.addSanPham(matl, tensanpham, gia, _FileName);
                }
                return RedirectToAction("ManageProduct", "TMDT");
            }
            catch
            {
                return RedirectToAction("ManageProduct", "TMDT");
            }
        }

        public ActionResult UpdateProduct(int keyword)
        {
            using (var db = new Context())
            {
                var product = SanPhamAction.FindSanPham(keyword);
                ViewBag.Product = product;
                ViewBag.Type = db.TheLoai.Where(s => s.matl != product.matl).ToList();
                ViewBag.TypeUpdate = db.TheLoai.Where(s => s.matl == product.matl).FirstOrDefault();
                return View();
            }
        }
        [HttpPost]
        public ActionResult UpdateProduct(int masanpham, int matl, string tensanpham, double gia, HttpPostedFileBase myfile, string hinh)
        {
            try
            {
                string _path = "";
                if (myfile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(myfile.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadFiles"), _FileName);
                    myfile.SaveAs(_path);
                    SanPhamAction.UpdateSanPham(masanpham, matl, tensanpham, gia, _FileName);
                }
                return RedirectToAction("ManageProduct", "TMDT");
            }
            catch
            {
                SanPhamAction.UpdateSanPham(masanpham, matl, tensanpham, gia, hinh);
                return RedirectToAction("ManageProduct", "TMDT");
            }
        }
        public ActionResult DeleteProduct(int keyword)
        {
            SanPhamAction.XoaSanPham(keyword);
            return RedirectToAction("ManageProduct", "TMDT");
        }

        public ActionResult AccountManager()
        {
            var listaccount = KhachHangAction.ListKhachHang();
            ViewBag.Account = listaccount;

            return View();
        }
        public ActionResult InsertAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertAccount(string ho, string ten, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau, string quyen)
        {
            if (matkhau == nhaplaimatkhau)
            {
                KhachHangAction.addKhachHang(ten, ho, diachi, sdt, email, matkhau, nhaplaimatkhau);
                return RedirectToAction("AccountManager", "TMDT");
            }
            else
            {
                TempData["LoiAccount"] = "Nhập lại mật khẩu sai";
                return View();
            }
        }

        public ActionResult UpdateAccount(int keyword)
        {
            ViewBag.Account = KhachHangAction.FindKhach(keyword);
            return View();
        }
        [HttpPost]
        public ActionResult UpdateAccount(int makh, string ho, string ten, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau, string quyen)
        {
            if (matkhau == nhaplaimatkhau)
            {
                KhachHangAction.UpdateKhachHang(makh, ho, ten, diachi, sdt, email, matkhau, nhaplaimatkhau, quyen);
                return RedirectToAction("AccountManager", "TMDT");
            }
            else
            {
                TempData["LoiAccount"] = "Nhập lại mật khẩu sai";
                return View();
            }
        }

        public ActionResult DeleteAccount(int keyword)
        {
            KhachHangAction.XoaKhachHang(keyword);
            return RedirectToAction("AccountManager", "TMDT");
        }

        public ActionResult TypeProductManager()
        {
            var listtype = TheLoai.ListTheLoai();
            ViewBag.Type = listtype;

            return View();
        }
        public ActionResult InsertType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InsertType(string loai)
        {

            TheLoai.addTheLoai(loai);
            return RedirectToAction("TypeProductManager", "TMDT");
        }
        public ActionResult UpdateType(int keyword)
        {
            ViewBag.Type = TheLoai.FindTheLoai(keyword);
            return View();
        }
        [HttpPost]
        public ActionResult UpdateType(int maloai, string loai)
        {
            TheLoai.UpdateTheLoai(maloai, loai);
            return RedirectToAction("TypeProductManager", "TMDT");
        }

        public ActionResult DeleteType(int keyword)
        {
            TheLoai.UpdateTheLoai1(keyword);
            return RedirectToAction("TypeProductManager", "TMDT");
        }



        [HttpPost]
        public ActionResult addCart(int productid, int quantity)
        {
            using (var db = new Context())
            {
                if (checkLogin() == false)
                {
                    var cart = SanPhamAction.FindSanPham(productid);
                    double summary = quantity * cart.gia;
                    //var appear = (from a in listcartdetail
                    //              where a.masanpham == productid && a.isdeleted == false
                    //              select a).ToList();
                    if (Session["Cart"] == null)
                    {
                        List<ChiTietGioHang> listcartdetail = new List<ChiTietGioHang>();
                        ChiTietGioHang cartdetail = new ChiTietGioHang { mactgiohang = listcartdetail.Count + 1, makh = 1, masanpham = productid, soluong = quantity, thanhtien = summary };
                        listcartdetail.Add(cartdetail);
                        Session["Cart"] = listcartdetail;
                    }
                    else
                    {
                        List<ChiTietGioHang> listcartdetail = new List<ChiTietGioHang>();
                        listcartdetail = (List<ChiTietGioHang>)Session["Cart"];
                        var appear = (from a in listcartdetail
                                      where a.masanpham == productid && a.isdeleted == false
                                      select a).ToList();
                        if (appear.Count == 0)
                        {
                            ChiTietGioHang cartdetail = new ChiTietGioHang { mactgiohang = listcartdetail.Count + 1, makh = 1, masanpham = productid, soluong = quantity, thanhtien = summary };
                            listcartdetail.Add(cartdetail);
                            Session["Cart"] = listcartdetail;
                        }
                        else
                        {
                            foreach (var productappear in listcartdetail)
                            {
                                if (productappear.masanpham == productid)
                                {
                                    productappear.soluong = productappear.soluong + quantity;
                                    productappear.thanhtien = productappear.thanhtien + summary;
                                }
                            }
                            Session["Cart"] = listcartdetail;
                        }
                    }
                }
                else
                {
                    int userid = Convert.ToInt32(Session["id"]);
                    var appear = db.ChiTietGioHang.Where(s => s.masanpham == productid && s.makh == userid).ToList();
                    var cart = SanPhamAction.FindSanPham(productid);
                    double summary = cart.gia * quantity;
                    if (appear.Count == 0)
                    {
                        ChiTietGioHangAction.addCTGioHang(userid, productid, quantity, summary);
                    }
                    else
                    {
                        ChiTietGioHangAction.DaTonTai(appear[0].mactgiohang, quantity, summary);
                    }
                }
                return RedirectToAction("Feature", "TMDT");

            }
        }
        public ActionResult DisplayName(int keyword)
        {
            using (var db = new Context())
            {
                ViewBag.DisplayName = db.SanPham.Where(s => s.masanpham == keyword).FirstOrDefault();
                return View();
            }
        }
        public ActionResult DisplayImage(int keyword)
        {
            using (var db = new Context())
            {
                ViewBag.DisplayImage = db.SanPham.Where(s => s.masanpham == keyword).FirstOrDefault();
                return View();
            }
        }
        public ActionResult DisplayPrice(int keyword)
        {
            using (var db = new Context())
            {
                ViewBag.DisplayPrice = db.SanPham.Where(s => s.masanpham == keyword).FirstOrDefault();
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string ho, string ten, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau, string quyen)
        {
            if (matkhau == nhaplaimatkhau)
            {
                KhachHangAction.addKhachHang(ten, ho, diachi, sdt, email, matkhau, nhaplaimatkhau);
                return RedirectToAction("Login", "TMDT");
            }
            else
            {
                TempData["LoiAccount"] = "Something wrong in password";
                return View();
            }
        }
        public ActionResult Login()
        {
            if (checkLogin() == false)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "TMDT");
            }
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            using (var db = new Context())
            {
                var login = (from a in db.KhachHang
                             where a.email == email && a.matkhau == password
                             select a).ToList();
                if (login.Count() != 0)
                {
                    var quyen = (from a in db.KhachHang
                                 where a.email == email && a.matkhau == password
                                 select a.quyen).FirstOrDefault();
                    var id = (from a in db.KhachHang
                              where a.email == email && a.matkhau == password
                              select a.makh).FirstOrDefault();
                    var ten = (from a in db.KhachHang
                               where a.email == email && a.matkhau == password
                               select a.ten).FirstOrDefault();
                    Session.Add("id", id.ToString());
                    Session.Add("taikhoan", email);
                    Session.Add("matkhau", password);
                    Session.Add("quyen", quyen.ToString());
                    Session.Add("ten", ten.ToString());
                    var appear = (from a in db.GioHang
                                  where a.makh == id
                                  select a).ToList();
                    var appearproduct = ChiTietGioHangAction.ListCTGioHang();
                    if (Session["Cart"] != null)
                    {
                        if (appear.Count == 0)
                        {
                            GioHangAction.addGioHang(id, ten);
                            List<ChiTietGioHang> addCart = new List<ChiTietGioHang>();
                            addCart = (List<ChiTietGioHang>)Session["Cart"];
                            foreach (var a in addCart)
                            {
                                foreach (var b in appearproduct)
                                {
                                    if (a.mactgiohang != b.mactgiohang)
                                    {
                                        ChiTietGioHangAction.addCTGioHang(id, a.masanpham, a.soluong, a.thanhtien);
                                    }
                                    else
                                    {
                                        ChiTietGioHangAction.DaTonTai(a.mactgiohang, a.soluong, a.thanhtien);
                                    }
                                }
                            }
                            Session.Remove("Cart");
                        }
                        else
                        {
                            List<ChiTietGioHang> addCart = new List<ChiTietGioHang>();
                            addCart = (List<ChiTietGioHang>)Session["Cart"];
                            ChiTietGioHang findproduct = new ChiTietGioHang();
                            foreach (var a in addCart)
                            {
                                foreach (var b in appearproduct)
                                {
                                    if (b.makh == id && b.masanpham == a.masanpham)
                                    {
                                        findproduct = new ChiTietGioHang { mactgiohang = b.mactgiohang, makh = b.makh, masanpham = b.masanpham, soluong = b.soluong, thanhtien = b.thanhtien };
                                    }
                                }
                                if (findproduct.masanpham == 0)
                                {
                                    ChiTietGioHangAction.addCTGioHang(id, a.masanpham, a.soluong, a.thanhtien);
                                }
                                else if (findproduct != null)
                                {
                                    ChiTietGioHangAction.DaTonTai(findproduct.mactgiohang, a.soluong, a.thanhtien);
                                }
                            }
                            Session.Remove("Cart");
                        }
                    }
                    else
                    {
                        if (appear.Count == 0)
                        {
                            GioHangAction.addGioHang(id, ten);
                        }
                    }
                    if (quyen.ToString() == "User")
                    {
                        return RedirectToAction("Home", "TMDT");
                    }
                    else
                    {
                        return RedirectToAction("DashBoard", "TMDT");
                    }
                }
                else
                {
                    TempData["ErrorLogin"] = "Sai tên đăng nhập hoặc mật khẩu";
                    return RedirectToAction("Login", "TMDT");
                }
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Home", "TMDT");
        }
        public ActionResult BuyHistory(int userid)
        {
            ViewBag.BuyHistory = LichSuMuaHang.ListLichSuMuaHang().Where(s => s.makh == userid).ToList();
            return View();
        }

        public ActionResult Description(int keyword)
        {
            ViewBag.Description = ChiTietSanPhamAction.FindCTSach(keyword);
            return View();
        }

        public ActionResult FindAccount(string keyword)
        {
            using (var db = new Context())
            {
                List<KhachHang> timkiem = (from b in db.KhachHang
                                           where b.ten.Contains(keyword) || b.ten.StartsWith(keyword) || b.ten.EndsWith(keyword) || keyword.StartsWith(b.ten)
                                           select b).ToList();
                ViewBag.Find = timkiem;
                return View();
            }
        }

        public ActionResult FindProduct(string keyword)
        {
            using (var db = new Context())
            {
                List<SanPham> timkiem = (from b in db.SanPham
                                           where b.tensanpham.Contains(keyword) || b.tensanpham.StartsWith(keyword) || b.tensanpham.EndsWith(keyword) || keyword.StartsWith(b.tensanpham)
                                           select b).ToList();
                ViewBag.Find = timkiem;
                return View();
            }
        }
        [HttpPost]
        public ActionResult FindShop(string keyword)
        {
            using (var db = new Context())
            {
                List<SanPham> timkiem = (from b in db.SanPham
                                         where b.tensanpham.Contains(keyword) || b.tensanpham.StartsWith(keyword) || b.tensanpham.EndsWith(keyword) || keyword.StartsWith(b.tensanpham)
                                         select b).ToList();
                ViewBag.Find = timkiem;
                return View();
            }
        }

        public ActionResult Dashboard()
        {
            using (var db = new Context())
            {
                var abc = (from b in db.ThongKeSachBan
                           select new { letter = b.thang, frequency = b.soluongsachbanduoc }).ToList();
                ViewBag.BCD = abc;
                return View();
            }
        }

        public bool checkLogin()
        {
            if (Session["taikhoan"] != null && !Session["taikhoan"].Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
