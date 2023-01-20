using System.Net.Mail;
using System.Net;
using toolsWebApi.IServices.Email;

namespace toolsWebApi.Services
{
    public class EmailConfig : IEmailConfig 
    {

        private readonly IConfiguration Config;

        public EmailConfig(IConfiguration configuration)
        {
            Config = configuration;
        }

        //efnqhtxbysoygfbq

        private readonly static string smtpAddress = "smtp.gmail.com";
        private readonly static int portNumber = 587;
        private readonly static bool enableSSL = true;
       // private readonly static string emailFromAddress = ""; //Sender Email Address  
        //private readonly static string password = ""; //Sender Password  
        public void SendOtpVerificationEmail(string emailId,string subject,string body)
        {
            
            SendEmail(emailId,subject,body);
        }

   
        public void SendEmail(string emailAddress,string subject,string body)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(Config["EmailConfig:user"]);
                mail.To.Add(emailAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(Config["EmailConfig:user"], Config["EmailConfig:key"]);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }

        public string OtpTemplate(int otp)
        {
            string template = "<div style=\"font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2\">\r\n  <div style=\"margin:50px auto;width:70%;padding:20px 0\">\r\n    <div style=\"border-bottom:1px solid #eee\">\r\n      <a href=\"\" style=\"font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600\">toolsWebApi</a>\r\n    </div>\r\n    <p style=\"font-size:1.1em\">Hi,</p>\r\n    <p>Thank you for choosing toolsWebApi. Use the following OTP to complete your Sign Up procedures. OTP is valid for 5 minutes</p>\r\n    <h2 style=\"background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;\">"+otp+"</h2>\r\n    <p style=\"font-size:0.9em;\">Regards,<br />toolsWebApi</p>\r\n    <hr style=\"border:none;border-top:1px solid #eee\" />\r\n    <div style=\"float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300\">\r\n      <p>toolsWebApi</p>\r\n      <p>2023</p>\r\n      <p>Developer: </p>\r\n    </div>\r\n  </div>\r\n</div>";

            return template;
        }

        

         
    }
}
