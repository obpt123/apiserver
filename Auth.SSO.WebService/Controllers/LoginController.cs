using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.SSO.WebService.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string appkey,string return_url)
        {
            return View();
        }

       

        
    }
}