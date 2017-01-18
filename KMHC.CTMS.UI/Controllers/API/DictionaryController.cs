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
    public class DictionaryController : ApiController
    {
        private readonly DictionaryBLL _dictionary = new DictionaryBLL();
        private readonly ctms_sys_userinfoBLL _user = new ctms_sys_userinfoBLL();

        public IHttpActionResult Get([FromUri]Request<string> request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Keyword))
                    return BadRequest("参数错误");

                List<HrDictionary> list = _dictionary.GetDictionaryByCategory(request.Keyword);
                Response<List<HrDictionary>> rsp = new Response<List<HrDictionary>>();
                rsp.Data = list;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete([FromUri]Request<string> req)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Keyword))
                    return BadRequest("参数错误");

                _dictionary.DeleteDictionary(req.Keyword);

                Response<string> rsp = new Response<string>();
                rsp.Data = "删除成功";
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Post([FromBody]Request<TreeModel> req)
        {
            Response<HrDictionary> rsp = new Response<HrDictionary>();
            try
            {
                TreeModel tree = req.Data as TreeModel;

                HrDictionary model = null;

                V_ctms_sys_userinfo user = _user.GetCurrentUser();
                if (user == null)
                    return base.Redirect("/User/Login#/Login");

                if (string.IsNullOrEmpty(tree.nodeId))
                {
                    model = new HrDictionary();
                    string ID = Guid.NewGuid().ToString();
                    model.DictionaryId = ID;
                    model.DictionCategory = tree.category;
                    model.DictionCategoryName = tree.category;
                    model.CreateUser = user.LOGINNAME;
                    model.CreateDate = DateTime.Now;
                    model.UpdateUser = user.LOGINNAME;
                    model.UpdateDate = DateTime.Now;
                    model.FatherId = tree.parentId == null ? "0" : tree.parentId;
                    model.DictionaryName = tree.text;
                    model.DictionaryValue = tree.value;
                    model.Remark = "";
                    _dictionary.Add(model);
                }
                else
                {
                    model = _dictionary.GetOne(p => p.dictionaryid == tree.nodeId);
                    if (model == null)
                        return NotFound();
                    model.UpdateDate = DateTime.Now;
                    model.UpdateUser = user.LOGINNAME;
                    model.DictionaryName = tree.text;
                    model.DictionaryValue = tree.value;
                    _dictionary.Edit(model);
                }
                rsp.Data = model;
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest(ex.ToString());
            }
        }
    }
    public class TreeModel
    {
        public string text { get; set; }

        public string value { get; set; }

        public string parentId { get; set; }

        public string nodeId { get; set; }

        public string category { get; set; }
    }
}
