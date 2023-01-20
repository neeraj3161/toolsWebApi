using Microsoft.AspNetCore.Mvc;
using toolsWebApi.IServices;
using toolsWebApi.IServices.hibernateConfig;
using toolsWebApi.Models;
using toolsWebApi.Entity;
using toolsWebApi.IServices.Email;
using NHibernate;
using Microsoft.AspNetCore.DataProtection;

namespace toolsWebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginService _loginService;

        private readonly IHibernateConfig _repository;

        private readonly IEmailConfig _emailConfig;

        private readonly IGenerateOtp _otp;

        private readonly IDataProtectionProvider _dataProtector;


        public LoginController(IloginService loginService, IHibernateConfig reporsitory, IEmailConfig emailConfig, IGenerateOtp otp,IDataProtectionProvider dataProtector) {
            _loginService = loginService;
            _repository = reporsitory;
            _emailConfig = emailConfig;
            _otp = otp;
            _dataProtector = dataProtector; 
        }

        public IActionResult Index()
        {
            CookieOptions options = new CookieOptions();
            options.Expires=DateTime.Now.AddDays(1);
            options.Path = "/";
            var protectedName = _dataProtector.CreateProtector("neeraj3161");
            var pn = protectedName.Protect("neeraj3161");
            Response.Cookies.Append("username",pn,options);

            var newProtectedName = _dataProtector.CreateProtector("neeraj3161");

            var unProtectedName = newProtectedName.Unprotect(pn);

            return View();
        }

        [HttpPost]
        public JsonResult login(string emailId, string pwd)
        {
            var isValidUser = false;
           
            if (emailId != null && pwd != null)
            {
                isValidUser = _loginService.UserLogin(emailId, pwd);
            }
            return Json(new { isValidUser = isValidUser, success = true });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public JsonResult SendOtp(RegistrationEntity registration) 
        {
            int otp = _otp.GenerateOtp(6);
            HttpContext.Session.SetInt32("otp", otp);
            string template = _emailConfig.OtpTemplate(otp);
            _emailConfig.SendOtpVerificationEmail(registration.Email, "Welcome to toolsWebApi", template);
            return Json(new { sucess = true }) ;
        }

        [HttpPost]

        public JsonResult UserRegistration(RegistrationEntity registration,int enteredOtp) 
        {
            bool verifyOtp = VerifyOtp(enteredOtp);

            if (registration == null || !(verifyOtp)) { return Json(new { success = false,otp=false, alreadyExists = true }); }

            _repository.OpenSessionFactory();
            //save data to db

            try
            {
                _repository.PersistData<RegistrationEntity>(registration);
            }
            catch (NHibernate.Exceptions.GenericADOException ex)
            
                {
                    return Json(new { success = false, otp = true, alreadyExists = true });
                }
             

            _repository.DisposeSession();

            return Json(new {success=true,otp = true, alreadyExists = true});
        }

        bool VerifyOtp(int enteredOtp) 
        {
            if (enteredOtp == HttpContext.Session.GetInt32("otp"))
                return true;
            return false; 
        }





}

}


