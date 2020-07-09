using CollectionTrackerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace CollectionTrackerMVC.Controllers
{
    public class ConditionController : Controller
    {
        // GET: Condition
        public ActionResult Index()
        {
            IEnumerable<ConditionViewModel> condition = null;
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                bool? active = null;
                var responseTask = client.GetAsync("condition/" + active);
                responseTask.Wait();

                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<ConditionViewModel>>();
                    readJob.Wait();
                    condition = readJob.Result;
                }
                else
                {
                    condition = Enumerable.Empty<ConditionViewModel>();
                    var readError = result.Content.ReadAsStringAsync();
                    readError.Wait();
                    ModelState.AddModelError(String.Empty, readError.Result);
                }
            }
            return View(condition);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ConditionViewModel model)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var postJob = client.PostAsJsonAsync<ConditionViewModel>("Condition", model);
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
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ConditionViewModel condition = null;
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var responseTask = client.GetAsync("condition/" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if(result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ConditionViewModel>();
                        readTask.Wait();
                        condition = readTask.Result;
                    }
                }
                return View(condition);
            }
            catch { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ConditionViewModel model)
        {
            try
            {
                using(var client =  new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var putJob = client.PutAsJsonAsync<ConditionViewModel>("Condition", model);
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
            catch { }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Properties.Settings.Default.APISetting);
                    var deleteTask = client.DeleteAsync("condition/" + id.ToString());
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
            catch { }
            return RedirectToAction("Index");
        }
    }
}