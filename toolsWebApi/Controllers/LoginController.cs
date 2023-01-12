using Microsoft.AspNetCore.Mvc;
using toolsWebApi.IServices;
using toolsWebApi.Models;

namespace toolsWebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginService _loginService;

        public LoginController(IloginService loginService) { 
            _loginService=loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult login(string emailId, string pwd) 
        {
            var isValidUser = false;
            if(emailId !=null && pwd != null) 
            {
                isValidUser = _loginService.UserLogin(emailId, pwd);
            }
            return Json(new { isValidUser = isValidUser, success = true });
        }

        public IActionResult Register()
        {
            return View();
        }
    }

    

}
