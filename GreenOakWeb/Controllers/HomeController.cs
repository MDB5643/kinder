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

        public ActionResult Info()
        {
            ViewBag.Message = "Info!";

            return View();
        }

        public ActionResult Bailey()
        {
            ViewBag.Message = "Mrs. Bailey's class!";
            return View();
        }

        [HttpGet]
        public ActionResult AtHomeLearning()
        {
            ViewBag.Message = "At home learning!";

            ContactModel _contactModel = new ContactModel();
            return View(_contactModel);
        }

        public ActionResult SnackSchedule()
        {
            ViewBag.Message = "Snack Schedule!";
            return View();
        }
    }
}