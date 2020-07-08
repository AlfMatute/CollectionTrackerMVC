using CollectionTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
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
                client.BaseAddress = new Uri("https://localhost:44394/api/");
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
                    ModelState.AddModelError(String.Empty, result.Content.ToString());
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/Brand");
                var postJob = client.PostAsJsonAsync<BrandViewModel>("Brand", model);
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
        public ActionResult Edit(int id)
        {
            BrandViewModel brand = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/");
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
            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(BrandViewModel model)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44394/api/Brand");
                var putJob = client.PutAsJsonAsync<BrandViewModel>("Brand", model);
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
    }
}