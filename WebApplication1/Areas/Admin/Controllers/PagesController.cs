using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Models.ViewModels.Pages;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            List<PageVM> pageList;
            using (Db db=new Db())
            {
                pageList = db.pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
            }
            return View(pageList);
        }

        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }
        // POST: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                using (Db db=new Db())
                {
                    string slug;
                    PagesDTO dTO = new PagesDTO();
                    dTO.Title = model.Title.ToUpper();
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(' ', '-').ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(' ', '-').ToLower();
                    }
                    if (db.pages.Any(x=>x.Title==model.Title))
                    {
                        ModelState.AddModelError("", "That title already exist");
                        return View(model);
                    }
                    else if (db.pages.Any(x=>x.Slug==model.Slug))
                    {
                        ModelState.AddModelError("", "That slug already exist");
                        return View(model);
                    }
                    dTO.Slug = slug;
                    dTO.Body = model.Body;
                    dTO.HasSideBar = model.HasSideBar;
                    dTO.Sorting = 100;
                    db.pages.Add(dTO);
                    db.SaveChanges();
                }
                TempData["SM"] = "You added new page";
                return RedirectToAction("Index");
            }
        }
    }
}