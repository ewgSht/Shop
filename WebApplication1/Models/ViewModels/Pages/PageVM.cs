﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models.Data;

namespace WebApplication1.Models.ViewModels.Pages
{
    public class PageVM
    {
        public PageVM()
        {

        }
        public PageVM(PagesDTO row)
        {
            Id = row.Id;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSideBar = row.HasSideBar;
        }
        public int Id { get; set; }
        [Required,StringLength(50,MinimumLength =3)]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required, StringLength(int.MaxValue, MinimumLength = 3)]
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSideBar { get; set; }
    }
}