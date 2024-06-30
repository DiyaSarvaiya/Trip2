using Microsoft.AspNetCore.Mvc;
using System.Data;
using Trip2.Areas.Users_Area.Models;
using Trip2.DAL;
using Trip2.BAL;
using Microsoft.AspNetCore.Http;

namespace Trip2.Areas.Users_Area.Controllers
{
    
    [Area("Users")]
    [Route("[controller]/[action]")]
    public class UsersController : Controller
    {
        private IConfiguration Configuration;
        public UsersController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

       /* [Route("~/Users_Area/Users/Login")]*/
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View("SignIn");
        }
        #region Login

        [HttpPost]
        public IActionResult UserLogin(UsersModel usersModel)
        {
           
            string connStr = this.Configuration.GetConnectionString("myConnectionString");
            string error = null;
            if(usersModel.Username == null)
            {
                error += "User Name is required";
            }
            if (usersModel.Password == null)
            {
                error += "<br/>Password is required";
            }
            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Login");
            }
            else
            {
                DAL_Base dal = new DAL_Base();
                DataTable dt = dal.PR_Users_Login(usersModel.Username, usersModel.Password);
                if(dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetInt32("UserID", Convert.ToInt32(dr["UserID"].ToString()));
                        HttpContext.Session.SetString("Username", dr["Username"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("PhoneNo", dr["PhoneNo"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid !";
                    return RedirectToAction("Login");
                }

                if(HttpContext.Session.GetString("Username") != null && HttpContext.Session.GetString("Password") != null && HttpContext.Session.GetString("IsAdmin") == "True")
                {
                    
                    return RedirectToAction("Index","Admin" ,new {area = "Admin"});
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        #endregion

        #region LOGOUT
        public IActionResult UserLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        #endregion

        #region SIGNIN
        [HttpPost]
        public IActionResult UserSignin(UsersModel userModel)
		{
            DAL_Base dal = new DAL_Base();
            DataTable dt = dal.PR_Users_SignIn(userModel);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                    HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                    HttpContext.Session.SetString("Password", dr["Password"].ToString());
                    HttpContext.Session.SetString("Email", dr["Email"].ToString());
                    HttpContext.Session.SetString("PhoneNo", dr["PhoneNo"].ToString());
                    HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                    break;
                }
            }
            return RedirectToAction("Login");
		}
		#endregion
	}
}
