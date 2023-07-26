using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Net.Configuration;
using GreenOakWeb.Models;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.Serialization.Json;
using Microsoft.Web.Helpers;
using System.Text.RegularExpressions;

namespace GreenOakWeb.Controllers
{
    public class HomeController : Controller
    {
        string lblForMessage = "";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Verification()
        {
            ViewBag.Message = "Are you sure you're not a robot?";
            return View(new RECaptcha());
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            ContactModel _contactModel = new ContactModel();
            return View(_contactModel);
        }

        public ActionResult CustomerPortal()
        {
            ViewBag.Message = "Your personal portal.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            ContactModel _model = new ContactModel();
            ViewBag.Message = "Your contact page.";
            if (ModelState.IsValid)
            {
                if (model.Email != null && ValidateContactSubmit(model) == false)
                {
                    ContactModel _modelError = new ContactModel();
                    _modelError.Name = model.Name;
                    _modelError.Subject = model.Subject;
                    _modelError.Body = model.Body;
                    _modelError.Email = model.Email;

                    return View(_modelError);
                }

                //Read SMTP section from Web.Config.
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage(smtpSection.From, smtpSection.Network.UserName))
                {
                    mm.Subject = model.Subject;
                    mm.Body = "Name: " + model.Name + "<br /><br />Email: " + model.Email + "<br />" + model.Body;
                    if (model.Attachment != null)
                    {
                        if (model.Attachment.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(model.Attachment.FileName);
                            mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                        }
                    }

                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Host = smtpSection.Network.Host;
                        smtp.EnableSsl = true;
                        NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = networkCred;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Send(mm);
                        ViewBag.Message = "Email sent sucessfully.";
                    }
                }
                _model.SubmissionSuccess = true;
            }
            else
            {
                _model.SubmissionSuccess = false;
            }
            
            _model.Name = "";
            _model.Subject = "";
            _model.Body = "";
            _model.Email = "";

            return View(_model);
        }

        private bool ValidateContactSubmit(ContactModel Model)
        {
            var emailRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            var isValidEmail = Regex.Match(Model.Email, emailRegex, RegexOptions.IgnoreCase);
            if (!isValidEmail.Success)
            {
                ModelState.AddModelError("error", "Email address is invalid");
                return false;
            }
            return true;
        }

        [HttpPost]
        public JsonResult AjaxMethod(string response)
        {
            RECaptcha recaptcha = new RECaptcha();
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + recaptcha.Secret + "&response=" + response;
            recaptcha.Response = (new WebClient()).DownloadString(url);
            return Json(recaptcha);
        }

        public ActionResult Portfolio()
        {
            ViewBag.Message = "portfolio?";
            return View();
        }
    }
}