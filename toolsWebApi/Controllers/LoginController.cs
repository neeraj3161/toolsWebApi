using Microsoft.AspNetCore.Mvc;
using toolsWebApi.IServices;
using toolsWebApi.IServices.hibernateConfig;
using toolsWebApi.Models;
using toolsWebApi.Entity;

namespace toolsWebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IloginService _loginService;

        private readonly IHibernateConfig _repository;


        public LoginController(IloginService loginService, IHibernateConfig reporsitory) { 
            _loginService=loginService;
            _repository=reporsitory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult login(string emailId, string pwd) 
        {
            var isValidUser = false;
            _repository.OpenSessionFactory();


            //get specific user
            var userData=_repository.Read<Users>(1);

            //get all users
            var userDataQuery = _repository.Query<Users>();

            _repository.DisposeSession();
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
