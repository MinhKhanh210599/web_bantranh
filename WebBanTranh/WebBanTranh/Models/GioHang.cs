using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanTranh.Models
{
    public class GioHang
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public string iMATRANH { get; set; }

        public string sTENTRANH { get; set; }

        public string sANHBIA { get; set; }

        public double dGIABAN { get; set; }

        public int iSOLUONG { get; set; }

        public double dTHANHTIEN
        {
            get { return iSOLUONG * dGIABAN; }
        }

        public GioHang(string MATRANH)
        {
            iMATRANH = MATRANH;
            TRANH tranh = data.TRANHs.Single(m => m.MATRANH == iMATRANH);
            sTENTRANH = tranh.TENTRANH;
            sANHBIA = tranh.ANHBIA;
            dGIABAN = double.Parse(tranh.GIABAN.ToString());
            iSOLUONG = 1;
        }
    }
}