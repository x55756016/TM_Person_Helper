using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System.Web.Configuration;
using System;
using System.Web;
using EntityFramework.Audit;
using Project.UI.Dtos;


namespace Project.UI.Controllers.API
{
    public class BaseController<TEntity> : ApiController
    {
        //protected Repository<TEntity> dal = new Repository<TEntity>();
        //protected HRDatabase db = new HRDatabase();
        protected virtual IQueryable<TEntity> CreateQueryable(Request<TEntity> request)
        {
            //IQueryable<TEntity> q = dal.Set.AsNoTracking<TEntity>();
            //return q;
            return null;
        }

        [Route("")]
        public virtual IHttpActionResult Get([FromUri]Request<TEntity> request)
        {
            //Response<IEnumerable<TEntity>> response = new Response<IEnumerable<TEntity>>();
            //try
            //{
            //    IQueryable<TEntity> q = this.CreateQueryable(request);
            //    if(!request.ContainsDeleted)
            //    {
            //        q = q.Where(m => m.IsDeleted == false);
            //    }
            //    int count = q.Count();
            //    if (request.PageSize > 0)
            //    {
            //        q = q.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
            //        count = count / request.PageSize;
            //        if (count % request.PageSize > 0)
            //        {
            //            count = +1;
            //        }
            //    }
            //    else
            //    {
            //        count = 1;
            //    }
            //    response.PagesCount = count;
            //    response.Data = q.ToList();
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteError(ex.ToString());
            //    return BadRequest(ex.Message);
            //}
            //return Ok(response);
            return Ok();
        }
        //[BasicAuthentication]
        [Route("{id:long}")]
        public virtual IHttpActionResult Get(int id)
        {
            //Response<TEntity> response = new Response<TEntity>();
            //TEntity model;
            //try
            //{
            //    model = dal.Find(id);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteError(ex.ToString());
            //    return BadRequest(ex.Message);
            //}
            //if (model == null)
            //{
            //    return NotFound();
            //}
            //response.Data = model;
            //return Ok(response);
            return Ok();
        }

        [Route("")]
        public virtual IHttpActionResult Post([FromBody]Request<TEntity> request)
        {
            //try
            //{
            //    if (request.Data.ID == 0)
            //    {   
            //        request.Data.CreationTime = DateTime.Now;
            //        request.Data.IsDeleted = false;
            //        dal.Insert(request.Data);
            //    }
            //    else
            //    {
            //        request.Data.LastModificationTime = DateTime.Now;
            //        dal.Update(request.Data);
            //    }
            //}
            //catch(Exception ex)
            //{
            //    LogHelper.WriteError(ex.ToString());
            //    return BadRequest(ex.Message);
            //}
            //return Ok();
            return Ok();
        }

        [Route("{id:long}")]
        public IHttpActionResult Delete(long id)
        {
            //try
            //{
            //    dal.Delete(id);
            //}
            //catch (Exception ex)
            //{
            //    LogHelper.WriteError(ex.ToString());
            //    return BadRequest(ex.Message);
            //}
            return Ok();
        }
    }


}
