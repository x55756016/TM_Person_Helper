using Project.BLL;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
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
    public class DictionaryManageController : ApiController
    {
        DictionaryBLL bll = new DictionaryBLL();

        public IHttpActionResult Get(int CurrentPage = 1)
        {
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CreateDate",
                Order = OrderEnum.desc
            };
            Expression<Func<hr_dictionary, bool>> predicate = p => p.isdeleted == 0 && p.fatherid.Equals("0");

            IEnumerable<HrDictionary> list = bll.GetList(pageInfo, predicate);

            Response<IEnumerable<HrDictionary>> response = new Response<IEnumerable<HrDictionary>>
            {
                Data = list,
                PagesCount = pageInfo.PagesCount
            };

            return Ok(response);
        }

        public IHttpActionResult Get(string Id)
        {
            try
            {
                HrDictionary model = bll.GetOne(p => p.dictionaryid.Equals(Id));
                if (model != null)
                {
                    HrDictionary father = bll.GetOne(p => p.dictionaryid.Equals(model.FatherId));
                    if (father != null)
                    {
                        model.FatherId = string.Format("{0}#{1}", father.DictionaryId, father.DictionaryName);
                    }
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest("异常！");
            }
        }

        public IHttpActionResult Get(int CurrentPage, int Type, string Name = "")
        {
            PageInfo pageInfo = new PageInfo()
            {
                PageIndex = CurrentPage,
                PageSize = 10,
                OrderField = "CreateDate",
                Order = OrderEnum.desc
            };
            Expression<Func<hr_dictionary, bool>> predicate = p => p.isdeleted == 0;
            if (!string.IsNullOrEmpty(Name))
                predicate = p => p.isdeleted == 0 && p.dictioncategory.Contains(Name);

            IEnumerable<HrDictionary> list = bll.GetList(pageInfo, predicate);

            Response<IEnumerable<HrDictionary>> response = new Response<IEnumerable<HrDictionary>>
            {
                Data = list,
                PagesCount = pageInfo.PagesCount
            };

            return Ok(response);
        }

        public IHttpActionResult Post([FromBody]Request<HrDictionary> request)
        {
            try
            {
                HrDictionary model = request.Data;
                bool result = true;
                if (string.IsNullOrEmpty(model.DictionaryId))
                {
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

        public IHttpActionResult Delete(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return BadRequest("非法请求！");
            }

            try
            {
                HrDictionary model = bll.GetOne(p => p.dictionaryid.Equals(Id));
                if (model == null)
                {
                    return Ok("ok");
                }

                model.IsDeleted = true;
                bll.Edit(model);

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
        private bool Insert(HrDictionary model)
        {
            V_ctms_sys_userinfo currentUser = new ctms_sys_userinfoBLL().GetCurrentUser();
            string userName = currentUser == null ? "" : currentUser.LOGINNAME;

            model.CreateDate = DateTime.Now;
            model.CreateUser = userName;
            model.SystemNeed = "0";
            model.IsDeleted = false;

            if (string.IsNullOrEmpty(bll.Add(model)))
                return false;
            return true;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool Update(HrDictionary model)
        {
            V_ctms_sys_userinfo currentUser = new ctms_sys_userinfoBLL().GetCurrentUser();
            string userName = currentUser == null ? "" : currentUser.LOGINNAME;

            model.UpdateDate = DateTime.Now;
            model.UpdateUser = userName;

            return bll.Edit(model);
        }
    }
}
