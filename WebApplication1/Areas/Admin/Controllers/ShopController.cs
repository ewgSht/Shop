using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;
using WebApplication1.Models.ViewModels.Shop;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
        // GET: Admin/Shop
        public ActionResult Categories()
        {
            List<CategoryVM> categoryVMList;
            using (Db db=new Db())
            {
                categoryVMList = db.Categories
                                .ToArray()
                                .OrderBy(x=>x.Sorting)
                                .Select(x=>new CategoryVM(x))
                                .ToList();
            }
            return View(categoryVMList);
        }
    }
}