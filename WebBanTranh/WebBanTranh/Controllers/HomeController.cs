using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanTranh.Models;

namespace WebBanTranh.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        DataClasses1DataContext data = new DataClasses1DataContext();

        public ActionResult Index()
        {
            var loadtranh = from TRANH in data.TRANHs select TRANH;
            return View(loadtranh);
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
    }
}
