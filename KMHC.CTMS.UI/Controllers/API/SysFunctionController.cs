using Project.BLL.Authorization;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model.Authorization;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    [RoutePrefix("Api/SysFunction")]
    public class SysFunctionController : ApiController
    {
        public IHttpActionResult Get(string Name = "")
        {
            Expression<Func<ctms_sys_function, bool>> predicate = p => p.isdeleted == 0;
            if (!string.IsNullOrEmpty(Name))
                predicate = p => p.isdeleted == 0 && p.menuname.Contains(Name);

            List<MenuInfo> list = new MenuInfoBLL().GetList(predicate);
            //list = GetTree(list, "");

            return Ok(list);
        }

        public IHttpActionResult Get(string ID, string Act)
        {
            MenuInfo model = new MenuInfoBLL().GetOne(ID);
            if (model != null)
            {
                MenuInfo parent = new MenuInfoBLL().GetOne(model.ParentID);
                model.ParentID = (parent == null ? "" : string.Format("{0}#{1}", parent.ID, parent.Name));
            }

            return Ok(model);
        }

        public IHttpActionResult Post([FromBody]Request<MenuInfo> request)
        {
            try
            {
                MenuInfo model = request.Data;
                bool result = true;
                if (string.IsNullOrEmpty(model.ID))
                {
                    model.IsMenu = true;
                    result = Insert(model);
                }
                else
                {
                    result = Update(model);
                }

                return Ok(result == true ? "ok" : "false");
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest(ex.ToString());
            }
        }

        public IHttpActionResult Delete(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return BadRequest("非法请求！");
            }

            try
            {
                MenuInfo model = new MenuInfoBLL().GetOne(ID);
                if (model == null)
                {
                    return Ok("ok");
                }

                model.IsDeleted = true;
                new MenuInfoBLL().Edit(model);

                return Ok("ok");
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest("异常！");
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Insert(MenuInfo model)
        {
            if (string.IsNullOrEmpty(new MenuInfoBLL().Add(model)))
                return false;
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Update(MenuInfo model)
        {
            return new MenuInfoBLL().Edit(model);
        }

        private List<MenuInfo> GetTree(List<MenuInfo> list, string parentId = null)
        {
            List<MenuInfo> tree = new List<MenuInfo>();
            foreach (MenuInfo item in list)
            {
                if (item.ParentID == parentId)
                {
                    //List<MenuInfo> temp = list.rem
                    //item.ChildrenList = GetTree(temp, item.ID);
                    tree.Add(item);
                }
            }

            return tree;
        }
    }
}
