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

        // GET: Admin/Pages/EditPage/id

        [HttpGet]
        public ActionResult EditPage(int id)
        {
            PageVM model ;
            using (Db db=new Db())
            {
                PagesDTO dto = db.pages.Find(id);
                if (dto==null)
                {
                    return Content("The page does not exist");
                }
                else
                {
                    model = new PageVM(dto);
                }
            }
            return View(model);
        }


        // GET: Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (Db db=new Db())
            {
                int id = model.Id;
                string slug = "home";
                PagesDTO dTO = db.pages.Find(id);
                dTO.Title = model.Title;
                if (model.Slug!="home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug=model.Slug.Replace(" ", "-").ToLower();
                    }
                }
                if (db.pages.Where(x=>x.Id!=id).Any(x=>x.Title==model.Title))
                {
                    ModelState.AddModelError("", "That title already exist");
                    return View(model);
                }
                else if (db.pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That slug already exist");
                    return View(model);
                }
                dTO.Slug = slug;
                dTO.Body = model.Body;
                dTO.HasSideBar = model.HasSideBar;
                db.SaveChanges();   
            }
            TempData["SM"] = "You have edited the page";
            return RedirectToAction("EditPage");
        }


        // GET: Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            PageVM model;
            using (Db db=new Db())
            {
                PagesDTO dto = db.pages.Find(id);
                if (dto==null)
                {
                    return Content("The page does not exist");
                }
                else
                {
                    model = new PageVM(dto);
                }
            }
            return View(model);
        }


        //GET: Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {
            using (Db db=new Db())
            {
                PagesDTO DTO = db.pages.Find(id);
                db.pages.Remove(DTO);
                db.SaveChanges();
            }
            TempData["SM"] = "You have delete a page!";
            return RedirectToAction("Index");
        }


        //Создаем метод сортировки
        //POST: Admin/Pages/ReoredPages
        [HttpPost]
        public void ReoredPages(int [] ids)
        {
            using (Db db=new Db())
            {
                int count = 1;
                PagesDTO dTO;
                foreach (var pageId in ids)
                {
                    dTO = db.pages.Find(pageId);
                    dTO.Sorting = count;
                    db.SaveChanges();
                    count++;
                }
            }
        }

        //Get: Admin/Pages/EditSideBar
        [HttpGet]
        public ActionResult EditSideBar()
        {
            SidebarVM model;
            using (Db db=new Db())
            {
                SidebarDTO dTO = db.Sidebars.Find(1);
                model = new SidebarVM(dTO);
            }
            return View(model);
        }

        //Post: Admin/Pages/EditSideBar/
        [HttpPost]
        public ActionResult EditSideBar(SidebarVM model)
        {
            using (Db db = new Db())
            {
                SidebarDTO dTO = db.Sidebars.Find(1);
                dTO.Body = model.Body;
                db.SaveChanges();
            }
            TempData["SM"] = "You have edited sidebar";
            return RedirectToAction("EditSideBar");
        }
    }
}