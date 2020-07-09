using CollectionTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CollectionTrackerMVC.Controllers
{
    public class CollectionController : Controller
    {
        string ErrorTitle = "Collection - Error";
        string ErrorPart = "An error occurred when loading the page: ";
        // GET: Collection
        public ActionResult Index()
        {
            try
            {
                IEnumerable<CollectionViewModel> collection = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var responseTask = client.GetAsync("collection/");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readJob = result.Content.ReadAsAsync<IList<CollectionViewModel>>();
                        readJob.Wait();
                        collection = readJob.Result;
                    }
                    else
                    {
                        collection = Enumerable.Empty<CollectionViewModel>();
                        var readError = result.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                    }
                }
                return View(collection);
            }
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CollectionViewModel model)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var postJob = client.PostAsJsonAsync<CollectionViewModel>("collection", model);
                    postJob.Wait();

                    var postResult = postJob.Result;
                    if (postResult.IsSuccessStatusCode)
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
                CollectionViewModel collection = null;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var responseTask = client.GetAsync("collection/" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<CollectionViewModel>();
                        readTask.Wait();
                        collection = readTask.Result;
                    }
                }
                return View(collection);
            }
            catch (Exception ex) { ViewBag.ErrorTitle = ErrorTitle; ViewBag.ErrorMessage = ErrorPart + ex.Message; return View("Error"); }
        }

        [HttpPost]
        public ActionResult Edit(CollectionViewModel model)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var putJob = client.PutAsJsonAsync<CollectionViewModel>("Collection", model);
                    putJob.Wait();

                    var putResult = putJob.Result;
                    if (putResult.IsSuccessStatusCode)
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
                    var deleteTask = client.DeleteAsync("collection/" + id.ToString());
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