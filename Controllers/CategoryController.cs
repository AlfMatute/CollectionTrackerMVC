using CollectionTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace CollectionTrackerMVC.Controllers
{
    public class CategoryController : Controller
    {
        string ErrorTitle = "Category - Error";
        string ErrorPart = "An error occurred when loading the page: ";
        // GET: Category
        public ActionResult Index()
        {
            try
            {
                IEnumerable<CategoryViewModel> category = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    bool? active = null;
                    var responseTask = client.GetAsync("category/" + active);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<IList<CategoryViewModel>>();
                        readJob.Wait();
                        category = readJob.Result;
                    }
                    else
                    {
                        category = Enumerable.Empty<CategoryViewModel>();
                        var readError = result.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                    }
                }
                return View(category);
            }
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var postJob = client.PostAsJsonAsync<CategoryViewModel>("Category", model);
                    postJob.Wait();

                    var postResult = postJob.Result;
                    if(postResult.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
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
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                CategoryViewModel category = null;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var responseTask = client.GetAsync("category/" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<CategoryViewModel>();
                        readTask.Wait();
                        category = readTask.Result;
                    }
                }
                return View(category);
            }
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var putJob = client.PutAsJsonAsync<CategoryViewModel>("Category", model);
                    putJob.Wait();

                    var putResult = putJob.Result;
                    if(putResult.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var readError = putResult.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                    }
                }
                return View(model);
            }
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var deleteTask = client.DeleteAsync("category/" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if(!result.IsSuccessStatusCode)
                    {
                        var readError = result.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                    }
                }
            }
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
            return RedirectToAction("Index");
        }
    }
}