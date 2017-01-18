using Project.BLL;
using Project.Common.Helper;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    [System.Web.Http.RoutePrefix("api/ctms_sys_modelsetting")]
    public class ctms_sys_modelsettingController : ApiController
    {
        private readonly ctms_sys_modelsettingBLL bll = new ctms_sys_modelsettingBLL();
        private V_ctms_sys_userinfo curUser = new ctms_sys_userinfoBLL().GetCurrentUser();

        public IHttpActionResult Get(int CurrentPage,int PageSize = 10,string kwd = "",int type = 0)
        {
            Response<IEnumerable<V_ctms_sys_modelsetting>> rsp = new Response<IEnumerable<V_ctms_sys_modelsetting>>();
            try
            {
                PageInfo pageInfo = new PageInfo (){
                    PageIndex = CurrentPage,
                    PageSize = PageSize,
                    OrderField = "LastModifyDateTime",
                    Order = OrderEnum.desc
                };

                List<V_ctms_sys_modelsetting> list = bll.GetList(pageInfo, type, kwd).ToList();
                
                rsp.Data = list;
                rsp.PagesCount = pageInfo.PagesCount;
                rsp.IsSuccess = true;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                rsp.IsSuccess = true;
                rsp.Data = null;
                rsp.ErrorMsg = "服务器异常";
                return Ok(rsp);
            }
        }

        public IHttpActionResult Get(string ID)
        {
            Response<V_ctms_sys_modelsetting> rsp = new Response<V_ctms_sys_modelsetting>();
            V_ctms_sys_modelsetting model = bll.Get(p => p.ModelSettingId == ID);
            rsp.IsSuccess = true;
            rsp.Data = model;
            return Ok(rsp);
        }

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_modelsetting> request)
        {
            Response<bool> rsp = new Response<bool>();
            V_ctms_sys_modelsetting model = request.Data as V_ctms_sys_modelsetting;
            if (string.IsNullOrEmpty(model.ModelSettingId))
            {
                model.CreateUserAccount = curUser.LOGINNAME;
                model.CreateDateTime = DateTime.Now;
                model.IsValid = 1;
                model.IsDeleted = 0;
            }
            model.LastModifyUserAccount = curUser.LOGINNAME;
            model.LastModifyDateTime = DateTime.Now;

            if (string.IsNullOrEmpty(model.ModelSettingId))
            {
                rsp.Data = string.IsNullOrEmpty(bll.Add(model)) ? false : true;
            }
            else
            {
                rsp.Data = bll.Edit(model);
            }
            rsp.IsSuccess = true;
            return Ok(rsp);
        }

        public IHttpActionResult Delete(string ID)
        {
            Response<bool> rsp = new Response<bool> ();
            rsp.Data = bll.Delete(ID);
            rsp.IsSuccess = true;
            return Ok(rsp);
        }
    }
}
