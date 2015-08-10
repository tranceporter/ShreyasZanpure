using RestSharp;
using SohamsFirstBirthday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SohamsFirstBirthday.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendRsvp(RsvpViewModel model)
        {
            var body = new StringBuilder();

            body.AppendLine(string.Format("Attending: {0}", model.Attending ? "Yes" : "No"));
            body.AppendLine(string.Format("Vegetarian: {0}", model.Vegetarian ? "Yes" : "No"));

            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "key-0ff892513b00ca2f0344f2174f1c601d");

            RestRequest request = new RestRequest();
            request.AddParameter("domain", "app15b4e0688ac44b97816dd29bf0c25411.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "shreyas.zanpure@gmail.com");
            request.AddParameter("to", "preeti.p.kulkarni@gmail.com");
            request.AddParameter("cc", "shreyas.zanpure@gmail.com");
            request.AddParameter("subject", string.Format("Soham's Birthday RSVP for {0}", model.FamilyName));
            request.AddParameter("text", body.ToString());
            request.Method = Method.POST;

            var result = client.Execute(request);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Json(new { messageSent = true });
            }

            return Json(new { messageSent = false });
        }
    }
}
