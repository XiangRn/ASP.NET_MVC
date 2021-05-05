using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using Rotativa;
using Models.DAO;
using Models.Framework;
using QRCoder;
using System.Drawing;
using System.IO;
namespace OnlineShop.Controllers
{
    public class SampleController : Controller
    {
        OnlineShopDBContext db = new OnlineShopDBContext();
       
    
        public ActionResult GeneratePDF()
        {
            // string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/feedback.html"));
            List<Sample> lst = new List<Sample>();
            lst.Add(new Sample() { Name = "Huong", Email = "cuadao1710@gmail.com" });
            lst.Add(new Sample() { Name = "Xiang", Email = "namhuong797@gmail.com" });
            ViewBag.lst = lst;
            QRCodeGenerator ObjQr = new QRCodeGenerator();

            QRCodeData qrCodeData = ObjQr.CreateQrCode(Session["Bill"].ToString(), QRCodeGenerator.ECCLevel.Q);

            Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);

            using (MemoryStream ms = new MemoryStream())

            {

                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] byteImage = ms.ToArray();

                ViewBag.Url = "data:image/png;base64," + Convert.ToBase64String(byteImage);

            }
            return new PartialViewAsPdf("View") { FileName = "ToysShop.pdf" };
        }

    }
}