/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2016-11-16                            创建 
 *  
 */
 
using Project.BLL;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;

namespace KMHC.CTMS.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ctms_hr_department")]
    public class ctms_hr_departmentController : ApiController
    {
        private ctms_hr_departmentBLL bll = new ctms_hr_departmentBLL();

        public IHttpActionResult Get(int CurrentPage)
        {
            //申明参数
            int _pageSize = 10;

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = CurrentPage,
                    PageSize = _pageSize,
                    OrderField = "DEPARTMENTID",
                    Order = OrderEnum.asc
                };
                var list = bll.GetList(pageInfo);
                Response<IEnumerable<V_ctms_hr_department>> response = new Response<IEnumerable<V_ctms_hr_department>>
                {
                    Data = list,
                    PagesCount = pageInfo.Total / _pageSize
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest("异常");
            }
        }

        public IHttpActionResult Get(string ID)
        {
            V_ctms_hr_department model = bll.Get(p=>p.DEPARTMENTID==ID);
            return Ok(model);
        }

        public IHttpActionResult Post([FromBody]Request<V_ctms_hr_department> request)
        {
            V_ctms_hr_department model = request.Data as V_ctms_hr_department;
            if (string.IsNullOrEmpty(model.DEPARTMENTID))
            {
                bll.Add(model);
            }
            else
            {
                bll.Edit(model);
            }

            return Ok("ok");
        }

        public IHttpActionResult Delete(string ID)
        {
            bll.Delete(ID);

            return Ok("ok");
        }
        
    }
}
