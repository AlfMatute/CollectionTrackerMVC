using CollectionTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace CollectionTrackerMVC.Controllers
{
    public class LoginController : Controller
    {
        string ErrorTitle = "Login - Error";
        string ErrorPart = "An error occurred when trying to Login: ";
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        //Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(LoginViewModel model)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    model.Register = true;
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var postJob = client.PostAsJsonAsync<LoginViewModel>("Login", model);
                    postJob.Wait();

                    var postResult = postJob.Result;
                    if(postResult.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var readError = postResult.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Title = ErrorTitle;
                ViewBag.ErrorMessage = ErrorPart + ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    model.Register = false;
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var getJob = client.PostAsJsonAsync<LoginViewModel>("Login", model);
                    getJob.Wait();

                    var result = getJob.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        LoginViewModel.Logged = true;
                        LoginViewModel.UserMail = model.Email;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var readError = result.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                        return View(model);
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Title = ErrorTitle;
                ViewBag.ErrorMessage = ErrorPart + ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var getJob = client.GetAsync("login");
                    getJob.Wait();

                    var result = getJob.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        LoginViewModel.Logged = false;
                        LoginViewModel.UserMail = String.Empty;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Title = ErrorTitle;
                ViewBag.ErrorMessage = ErrorPart + ex.Message;
                return View("Error");
            }
        }
    }
}