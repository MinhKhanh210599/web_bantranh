using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebBanTranh.Models;

namespace WebBanTranh.Controllers
{

    public class KhachHangController : Controller
    {
        //
        // GET: /KhachHang/
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG qlbt)
        {
            var hoten = collection["HOTEN"];
            //var ngaysinh = String.Format("{0.MM:dd:yyyy}", collection["NGAYSINH"]);
            var gioitinh = collection["GIOITINH"];
            var dienthoai = collection["DIENTHOAI"];
            var taikhoan = collection["TAIKHOAN"];
            var matkhau = collection["MATKHAU"];
            var email = collection["EMAIL"];
            var diachi = collection["DIACHI"];

            if (String.IsNullOrEmpty(hoten))
                ViewData["Loi2"] = "Họ tên khách hàng không được trống";
            //else if (String.IsNullOrEmpty(ngaysinh))
            //    ViewData["Loi3"] = "Phải nhập ngày sinh";
            else if (String.IsNullOrEmpty(dienthoai))
                ViewData["Loi5"] = "Phải nhập số điện thoại !";
            else if (String.IsNullOrEmpty(taikhoan))
                ViewData["Loi6"] = "Phải nhập tài khoản đăng nhập !";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi7"] = "Mật khẩu không được bỏ trống";
            else if (String.IsNullOrEmpty(email))
                ViewData["Loi8"] = "Email không được bỏ trống !";
            else if (String.IsNullOrEmpty(diachi))
                ViewData["Loi9"] = "Địa chỉ không được bỏ trống !";
            else
            {
                qlbt.HOTEN = hoten;
               // qlbt.NGAYSINH = DateTime.Parse(ngaysinh);
                qlbt.GIOITINH = gioitinh;
                qlbt.DIENTHOAI = dienthoai;
                qlbt.TAIKHOAN = taikhoan;
                qlbt.MATKHAU = matkhau;
                qlbt.EMAIL = email;
                qlbt.DIACHI = diachi;
                db.KHACHHANGs.InsertOnSubmit(qlbt);
                db.SubmitChanges();
                ViewBag.Thongbao = "Bạn đã đăng ký thành công !!!";
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collction)
        {
            var tendn = collction["TAIKHOAN"];
            var matkhau = collction["MATKHAU"];
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi6"] = "Tên đăng nhập không được bỏ trống";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi7"] = "Phải nhập mật khẩu";
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TAIKHOAN == tendn && n.MATKHAU == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng bạn đã đăng nhập thành công <3";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("DatHang", "GioHang");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng nhập lại !!!";
                }
            }
            return View();
        }
    }
}
