using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.Data
{
    public class Db:DbContext
    {
        public DbSet<PagesDTO> pages { get; set; }
    }
}