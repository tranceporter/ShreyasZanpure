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

            var msg = new MailMessage();

            msg.From = new MailAddress("shreyas.zanpure@gmail.com", "Shreyas Zanpure");
            msg.To.Add(new MailAddress("preeti.p.kulkarni@gmail.com", "Preeti Kulkarni"));
            msg.Priority = MailPriority.High;
            msg.Subject = string.Format("Soham's Birthday RSVP for {0}", model.FamilyName);
            msg.Body = body.ToString();
            msg.IsBodyHtml = false;

            var client = new SmtpClient();

            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("shreyas.zanpure@gmail.com", "Shrpun123");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(msg);

            return Json(new { messageSent = true });
        }
    }
}
