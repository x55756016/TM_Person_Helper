using Project.BLL;
using Project.BLL.Authorization;
using Project.Common;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model;
using Project.UI.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    public class UserManageController : ApiController
    {
        private readonly ctms_sys_userinfoBLL _user = new ctms_sys_userinfoBLL();

        public IHttpActionResult Get(int pageIndex, string keyWord, int pageSize = 10)
        {
            try
            {
                PageInfo pageInfo = new PageInfo()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    OrderField = "LoginName",
                    Order = OrderEnum.desc
                };

                V_ctms_sys_userinfo currentUser = _user.GetCurrentUser();
                ArrayList args = new ArrayList();
                string filterString = new RoleFunctionBLL().GetFilterString(currentUser.USERID, "CTMS_SYS_USERINFO", PermissionType.View, ref args);
                List<V_ctms_sys_userinfo> list = _user.GetList(ref pageInfo, keyWord, filterString, args);

                Response<IEnumerable<V_ctms_sys_userinfo>> response = new Response<IEnumerable<V_ctms_sys_userinfo>>
                {
                    Data = list,
                    PagesCount = pageInfo.PagesCount
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get(string uid)
        {
            try
            {
                V_ctms_sys_userinfo User = _user.GetUserInfoByID(uid);
                Response<V_ctms_sys_userinfo> response = new Response<V_ctms_sys_userinfo>
                {
                    Data = User,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Get()
        {
            try
            {
                V_ctms_sys_userinfo User = _user.GetCurrentUser();
                Response<V_ctms_sys_userinfo> response = new Response<V_ctms_sys_userinfo>
                {
                    Data = User,
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<V_ctms_sys_userinfo> request)
        {
            try
            {
                V_ctms_sys_userinfo user = request.Data as V_ctms_sys_userinfo;

                V_ctms_sys_userinfo currUser = _user.GetCurrentUser();

                if (user != null)
                {
                    _user.Edit(user);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
