using Microsoft.AspNetCore.Mvc;
using toolsWebApi.IServices;
using toolsWebApi.IServices.hibernateConfig;
using toolsWebApi.Models;
using toolsWebApi.Entity;
using toolsWebApi.IServices.Email;
using NHibernate;

namespace toolsWebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginService _loginService;

        private readonly IHibernateConfig _repository;

        private readonly IEmailConfig _emailConfig;

        private readonly IGenerateOtp _otp;


        public LoginController(IloginService loginService, IHibernateConfig reporsitory, IEmailConfig emailConfig, IGenerateOtp otp) {
            _loginService = loginService;
            _repository = reporsitory;
            _emailConfig = emailConfig;
            _otp = otp;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult login(string emailId, string pwd)
        {
            var isValidUser = false;
            //generate random number

           
            //_repository.OpenSessionFactory();


            //get specific user
            //var userData=_repository.Read<Users>(1);

            ////get all users
            //var userDataQuery = _repository.Query<Users>();

            //_repository.DisposeSession();
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

            if (registration == null || !(verifyOtp)) { return Json(new { success = false }); }

            _repository.OpenSessionFactory();
            //save data to db

            try
            {
                _repository.PersistData<RegistrationEntity>(registration);
            }
            catch (Exception ex)
            { 
                return Json(new { success = true,response="User already exists" });
            }

            _repository.DisposeSession();

            return Json(new {success=true});
        }

        bool VerifyOtp(int enteredOtp) 
        {
            if (enteredOtp == HttpContext.Session.GetInt32("otp"))
                return true;
            return false; 
        }





}

}


