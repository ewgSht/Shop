﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.Data;

namespace WebApplication1.Models.ViewModels.Pages
{
    public class SidebarVM
    {
        public SidebarVM()
        {

        }
        public SidebarVM(SidebarDTO row)
        {
            Id = row.Id;
            Body = row.Body;
        }
        public int Id { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}