using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nexmo.Api;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HelloWorld.Controllers
{
    public class SMSController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CheckVerify()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckVerify(string verify)
        {
            if (ModelState.IsValid)
            {
                if (verify == Session["verify"].ToString())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Mã xác nhận không đúng");
                }
            }
            
            return View();
        }
        [HttpGet]
        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(string phone)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                // Find your Account Sid and Auth Token at twilio.com/console
                const string accountSid = "ACacb7c00d9f9520d18adede2923d23500";
                const string authToken = "67ff9cc15849ba9abb5f76b29368a28c";
                string verify = rd.Next(1000, 9999).ToString();
                Session["verify"] = verify;
                TwilioClient.Init(accountSid, authToken);
                
                var to = new PhoneNumber("+84" + phone);

                var message = MessageResource.Create(
                    to,
                    //First navigate to your test credentials https://www.twilio.com/user/account/developer-tools/test-credentials
                    // then you need to get Twilio number from https://www.twilio.com/console/voice/numbers
                    from: new PhoneNumber("+16124008790"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ). 
                    messagingServiceSid : "MG5a864b1582534353daec2d1504526776",
                body: $"Mã xác nhận Toys-Shop : " + verify + " .Vui lòng không chia sẻ mật mã này cho bất kỳ ai trong bất kỳ trường hợp nào");


                ViewBag.Message = "Message Sent";
                return RedirectToAction("CheckVerify");
            }
            else
            {
                return View();
            }
            //Random rd = new Random();
            //string verify = rd.Next(1000, 9999).ToString();
            //var results = SMS.Send(new SMS.SMSRequest
            //{

            //    from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
            //    to = to,
            //    text = verify

            //}) ;
            //Session["verify"] = verify;
            //return View("CheckVerify");
        }
    }
}