using GoodsService.Core;
using System;
using System.Collections.Generic;
using System.Text;
using GoodsService.Core.Models;
using System.Data.Store;

namespace GoodsService.Entity.Impl
{
    [ServiceImplClass(typeof(ICategoryService))]
    public class CategoryService: System.Data.Service.BaseService<Category,Guid>, ICategoryService
    {
        public CategoryService(IEntityStore<Category> store):base(store)
        {

        }
    }
}
