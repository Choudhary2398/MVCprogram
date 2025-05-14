using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace Emailtestprogram
{
    public class Email_logic
    {
        public bool SendEmail(out string errorMessage)
        {
            try
            {
                var fromEmail = ConfigurationManager.AppSettings["EmailUsername"];
                var fromPassword = ConfigurationManager.AppSettings["EmailPassword"];

                var message = new MailMessage();
                message.From = new MailAddress(fromEmail, "Arif Choudhary");
                message.To.Add(new MailAddress("choudharyarif2398@gmail.com", "Recipient One"));
                message.To.Add(new MailAddress("choudharyarif595@gmail.com", "Recipient Two"));
                message.Subject = "Test Email from ASP.NET MVC";
                message.Body = "Hello! This is a test email sent from an ASP.NET MVC app. By Arif";
                message.IsBodyHtml = false;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(fromEmail, fromPassword)
                };

                smtp.Send(message);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}