using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebBanTranh.Models;

namespace WebBanTranh.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/

        public ActionResult Index()
        {
            return View();
        }

        DataClasses1DataContext data = new DataClasses1DataContext();
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

        public ActionResult ThemGioHang(string iMATRANH, string strURL)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sanpham = lstGiohang.Find(n => n.iMATRANH == iMATRANH);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMATRANH);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSOLUONG++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSOLUONG);
            }
            return iTongSoLuong;
        }

        private double tinhTongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dTHANHTIEN);
            }
            return iTongTien;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = tinhTongTien();
            return View(lstGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = tinhTongTien();
            return PartialView();
        }

        /// <summary>
        /// /////////////////
        /// </summary>
        /// <param name="maTg"></param>
        /// <returns></returns>


        //public ActionResult Xoa(string maTranh)
        //{
        //    var xoa = data.TRANHs.First(m => m.MATRANH == maTranh);
        //    return View(xoa);
        //}

        //[HttpPost]
        //public ActionResult Xoa(string maTranh, FormCollection collection)
        //{
        //    var xoa = data.TRANHs.First(m => m.MATRANH == maTranh);
        //    data.TRANHs.DeleteOnSubmit(xoa);
        //    data.SubmitChanges();
        //    return RedirectToAction("QuanLyBanTranh");
        //}
        public ActionResult XoaGioHang(string maTranh)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sanPham = lstGiohang.SingleOrDefault(n => n.iMATRANH == maTranh);
            if (sanPham != null)
            {
                lstGiohang.RemoveAll(n => n.iMATRANH == maTranh);
                return RedirectToAction("GioHang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CapNhatGioHang(string matranh, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMATRANH == matranh);
            if (sanpham != null)
            {
                sanpham.iSOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TAIKHOAN"] == null || Session["TAIKHOAN"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = tinhTongTien();

            return View(lstGioHang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            KHACHHANG kh = (KHACHHANG)Session["TAIKHOAN"];
            DONHANG dh = new DONHANG();
            List<GioHang> gh = LayGioHang();
            dh.MAKH = kh.MAKH;
            dh.NGAYDAT = DateTime.Now;
            dh.DATHANHTOAN = Convert.ToString(0);
            data.DONHANGs.InsertOnSubmit(dh);
            
            data.SubmitChanges();
            foreach (var item in gh)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.MADH = dh.MADH;
                ctdh.MATRANH = item.iMATRANH;
                ctdh.SOLUONG = item.iSOLUONG;
                ctdh.DONGIA = (decimal)item.dTHANHTIEN;
                data.CHITIETDONHANGs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang","GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}
