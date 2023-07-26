using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreenOakWeb.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "Please enter a subject.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Message body required.")]
        [AllowHtml]
         public string Body { get; set; }
         public HttpPostedFileBase Attachment { get; set; }

        public bool SubmissionSuccess { get; set; } = false;

        public string Key = "6LdSyMUkAAAAACznFZ7wa2m_xXRjbJ-K4uF9QY0H";

        public string Secret = "6LdSyMUkAAAAAATFJSdM5gKrdo2EW9w4QGcoEdFW";
        public string Response { get; set; }

    }
}