using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTranh.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WebBanTranh.Controllers
{
    public class QuanLiController : Controller
    {
        //
        // GET: /QuanLi/

        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult Tranh( int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            return View(db.TRANHs.ToList().OrderBy(n => n.MATRANH).ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
                ViewData["Loi1"] = "Tên đăng nhập không được để trống";
            else if (String.IsNullOrEmpty(matkhau))
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            else
            {
                ADMIN kh = db.ADMINs.SingleOrDefault(m => m.TENDANGNHAP == tendn && m.MATKHAU == matkhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Tranh", "QuanLi");
                }
                else
                {
                    ViewBag.Thongbao = "Sai tên đăng nhập hoặc mật khẩu";
                }
            }
            return View();
        }

        public ActionResult TheLoaiTranh(string id)
        {
            var chitiettranh = db.TRANHs.Where(m => m.MATRANH == id).First();
            return View(chitiettranh);
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;

            }
            return lstGiohang;
        }

        public ActionResult XoaGioHang(string maTranh)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sanPham = lstGiohang.SingleOrDefault(n => n.iMATRANH == maTranh);
            if (sanPham != null)
            {
                lstGiohang.RemoveAll(n => n.iMATRANH == maTranh);
                return RedirectToAction("Tranh");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Tranh", "QuanLi");
            }
            return RedirectToAction("Tranh");
        }

        [HttpGet]
        public ActionResult XoaTranh(string id)
        {
            TRANH tranh = db.TRANHs.SingleOrDefault(n => n.MATRANH == id);
            ViewBag.MaTranh = tranh.MATRANH;
            if (tranh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tranh);
        }
        [HttpPost,ActionName("XoaTranh")]

        public ActionResult XacNhanXoa(string id)
        {
            TRANH tranh = db.TRANHs.SingleOrDefault(n => n.MATRANH == id);
            ViewBag.Matranh = tranh.MATRANH;
            if (tranh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.TRANHs.DeleteOnSubmit(tranh);
            db.SubmitChanges();
            return RedirectToAction("Tranh");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTranhMoi(TRANH tranh, HttpPostedFileBase fileUpload)
        {
            ViewBag.Macd = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TENCD), "MACD", "TENCD");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Anh"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình này đã tồn tại";
                    }
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    tranh.ANHBIA = fileName;
                    db.TRANHs.InsertOnSubmit(tranh);
                    db.SubmitChanges();
                }
                return RedirectToAction("Tranh");
            }
        }
    }

}
