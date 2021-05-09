using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Models.DAO;
using Models.Framework;
using HelloWorld.Models;
namespace HelloWorld.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User user)
        {
            return View();
        }
        public JsonResult LoadProvince()
        {
            XDocument xDocument = XDocument.Load(Server.MapPath(@"~/Data/Provinces_Data.xml"));
            var xElement = xDocument.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            List<Province> provinces = new List<Province>() ;
            Province province = null; 
            foreach (var item in xElement)
            {
                province = new Province();
                province.ID = Convert.ToInt32(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                provinces.Add(province);
            }
            return Json(new
            {
                data=provinces,
                status=true
            });
        }
        public JsonResult LoadDistrict(int id)
        {
            XDocument xDocument = XDocument.Load(Server.MapPath(@"~/Data/Provinces_Data.xml"));
            var xElement = xDocument.Element("Root").Elements("Item").
                Where(x => x.Attribute("type").Value == "province" && Convert.ToInt32(x.Attribute("id").Value) == id).Elements("Item").
                Where(x => x.Attribute("type").Value == "district");
            List<District> districts = new List<District>();
            District district = null;
            foreach (var item in xElement)
            {
                district = new District();
                district.ID = Convert.ToInt32(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                districts.Add(district);
            }
            return Json(new
            {
                data=districts,
                status=true
            });
        }
        public JsonResult LoadTown(int id)
        {
            XDocument xDocument = XDocument.Load(Server.MapPath(@"~/Data/Provinces_Data.xml"));
            var xElement = xDocument.Element("Root").Elements("Item").Elements("Item").
                Where(x => x.Attribute("type").Value == "district" && Convert.ToInt32(x.Attribute("id").Value) == id).Elements("Item").
                Where(x => x.Attribute("type").Value == "precinct");
            List<Town> towns = new List<Town>();
            Town town = null;
            foreach (var item in xElement)
            {
                town = new Town();
                town.ID = Convert.ToInt32(item.Attribute("id").Value);
                town.Name = item.Attribute("value").Value;
                towns.Add(town);
            }
            return Json(new
            {
                data=towns,
                status=true
            });
        }
    }
}