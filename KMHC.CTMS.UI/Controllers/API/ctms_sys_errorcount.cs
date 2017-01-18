using Project.BLL;
using Project.Common.Helper;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ctms_sys_errorcount")]
    public class ctms_sys_errorcountController : ApiController
    {
        private readonly ctms_sys_errorcountBLL bll = new ctms_sys_errorcountBLL();

        public IHttpActionResult Get(int pageIndex = 1, int pageSize = 10, string kwd = "", DateTime? dtStart = null, DateTime? dtEnd = null)
        {
            Response<IEnumerable<V_ctms_sys_errorcount>> rsp = new Response<IEnumerable<V_ctms_sys_errorcount>>();

            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "InputTime",
                    Order = OrderEnum.desc
                };
                var list = bll.GetList(pageInfo, kwd, dtStart, dtEnd);
                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.Data = null;
                rsp.ErrorMsg = ex.Message;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
        }

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_errorcount> request)
        {
            Response<bool> rsp = new Response<bool>();
            try
            {
                V_ctms_sys_errorcount model = request.Data as V_ctms_sys_errorcount;
                if (string.IsNullOrEmpty(model.ErrorId))
                {
                    bll.Add(model);
                }
                else
                {
                    bll.Edit(model);
                }
                rsp.Data = true;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (DbEntityValidationException dbex)
            {
                rsp.Data = false;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = dbex.Message;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                rsp.Data = false;
                rsp.IsSuccess = true;
                rsp.ErrorMsg = ex.Message;
                return Ok(rsp);
            }
        }
    }
}
