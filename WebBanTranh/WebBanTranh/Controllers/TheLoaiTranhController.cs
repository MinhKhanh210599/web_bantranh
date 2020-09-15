using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTranh.Models;

namespace WebBanTranh.Controllers
{
    public class TheLoaiTranhController : Controller
    {
        //
        // GET: /TheLoaiTranh/
        DataClasses1DataContext data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TranhPhongCanh()
        {
            //var loadtranh = from TRANH in data.TRANHs select TRANH;
            //return View(loadtranh);
            List<TRANH> TR = new List<TRANH>();
            var chitiet = data.TRANHs.Where(m => m.MACD == "CD02").ToList();
            return View(chitiet);
        }
        public ActionResult TranhTreoTuong3D()
        {
            List<TRANH> TR = new List<TRANH>();
            var chitiet = data.TRANHs.Where(m => m.MACD == "CD01").ToList();
            return View(chitiet);
        }
        public ActionResult TheLoai(string id)
        {
            var chitiet = data.TRANHs.Where(m => m.MATRANH == id).First();
            return View(chitiet);
        }

    }
}
