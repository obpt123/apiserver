using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GoodsService.Core.Models;
namespace GoodsService.EF
{
    [DbContextClass]
    public class GoodsServiceContext:DbContext
    {
        protected GoodsServiceContext()
        {

        }
        public GoodsServiceContext(DbContextOptions<GoodsServiceContext> options):base(options)
        {

        }

        public virtual DbSet<Category> Categories { get; set; }
    }
}
