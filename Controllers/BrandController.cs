using CollectionTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace CollectionTrackerMVC.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            IEnumerable<BrandViewModel> brand = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                var responseTask = client.GetAsync("Brand");
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<BrandViewModel>>();
                    readJob.Wait();
                    brand = readJob.Result;
                }
                else
                {
                    brand = Enumerable.Empty<BrandViewModel>();
                    var readError = result.Content.ReadAsStringAsync();
                    readError.Wait();
                    ModelState.AddModelError(String.Empty, readError.Result);
                }
            }
            return View(brand);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BrandViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var postJob = client.PostAsJsonAsync<BrandViewModel>("Brand", model);
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
            catch { }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            try
            {
                BrandViewModel brand = ReadFromApi(id);
                return View(brand);
            }
            catch { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(BrandViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var putJob = client.PutAsJsonAsync<BrandViewModel>("Brand", model);
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
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var deleteTask = client.DeleteAsync("brand/" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (!result.IsSuccessStatusCode)
                    {
                        var readError = result.Content.ReadAsStringAsync();
                        readError.Wait();
                        ModelState.AddModelError(String.Empty, readError.Result);
                    }
                }
            }
            catch { }
            return RedirectToAction("Index");
        }

        public BrandViewModel ReadFromApi(int id)
        {
            BrandViewModel brand = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                var responseTask = client.GetAsync("brand/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BrandViewModel>();
                    readTask.Wait();
                    brand = readTask.Result;
                }
            }
            return brand;
        }
    }
}