using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using GoodsService.Core;
using GoodsService.Core.Models;
using System.Data;
using System.Data.Service;
using Microsoft.AspNetCore.Authorization;
using System.Data.Entity;
using System.Threading.Tasks;

namespace GoodsService.WebApi
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController()]
    public class CategoryController : BaseCURDController<ICategoryService,Category>
    {
        public CategoryController(ICategoryService categoryService):base(categoryService)
        {

        }
    }



    public class CategoryService : ICategoryService
    {
        public Task<ResultData<Category>> Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<ResultData<int>> BatchOperation(BatchData<Category> batchInfos)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count(SearchCondition conditions)
        {
            throw new NotImplementedException();
        }

        public Task<ResultData<Category>> Create()
        {
            throw new NotImplementedException();
        }

        public Task<ResultInfo> DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> FindItem(SearchCondition condition)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> ListAll(SearchCondition condition, OrderCondition orderitems)
        {
            throw new NotImplementedException();
        }

        public Task<LimitData<Category>> ListLimit(SearchCondition condition, OrderCondition orderItems, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<PageData<Category>> ListPage(SearchCondition condition, OrderCondition orderitems, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ResultData<Category>> Patch(Category entity, params string[] fields)
        {
            throw new NotImplementedException();
        }

        public Task<ResultData<Category>> Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }

    public class BaseCURDController<TService, TEntity> : BaseCURDController<TService, TEntity, Guid>
        where TService : ICURDAll<TEntity, Guid>
        where TEntity : class, IId<Guid>
    {
        public BaseCURDController(TService service):base(service)
        {
        }
    }


    public class BaseCURDController<TService, TEntity,TKey> : ControllerBase
        where TService : ICURDAll<TEntity, TKey>
        where TEntity : class, IId<TKey> 
    {
        public BaseCURDController(TService service)
        {
            this.Service = service;
        }
        protected virtual TService Service { get; set; }

        public virtual async Task<ActionResult<ResultData<TEntity>>> Add(TEntity entity)
        {
            var res= await this.Service.Add(entity);
            if (res.Success)
            {
                return NotFound();
            }
            return res;
        }

        public virtual Task<ResultData<int>> BatchOperation(BatchData<TEntity> batchInfos)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<int>> Count(SearchCondition conditions)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<TEntity>> Create()
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultInfo> DeleteById(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<TEntity>> FindById(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<TEntity>> FindItem(SearchCondition condition)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<List<TEntity>>> ListAll(SearchCondition condition, OrderCondition orderitems)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<LimitData<TEntity>>> ListLimit(SearchCondition condition, OrderCondition orderItems, int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<PageData<TEntity>>> ListPage(SearchCondition condition, OrderCondition orderitems, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<TEntity>> Patch(TEntity entity, params string[] fields)
        {
            throw new NotImplementedException();
        }

        public virtual Task<ResultData<TEntity>> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
