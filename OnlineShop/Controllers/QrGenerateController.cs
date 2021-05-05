using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using QRCoder;
using System.Drawing;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
namespace OnlineShop.Controllers
{
    public class QrGenerateController : Controller
    {
       

        public ActionResult ActionQrCode()

        {
            
            QRCodeGenerator ObjQr = new QRCodeGenerator();

            QRCodeData qrCodeData = ObjQr.CreateQrCode(Session["Bill"].ToString(), QRCodeGenerator.ECCLevel.Q);

            Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);

            using (MemoryStream ms = new MemoryStream())

            {

                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                byte[] byteImage = ms.ToArray();

                ViewBag.Url = "data:image/png;base64," + Convert.ToBase64String(byteImage);

            }

            return View();

        }
    }
}