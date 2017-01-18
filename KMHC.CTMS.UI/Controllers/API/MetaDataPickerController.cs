using Project.BLL.Authorization;
using Project.Common.Helper;
using Project.Model.Authorization;
using Project.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.UI.Controllers.API
{
    public class MetaDataPickerController : ApiController
    {
        private readonly MetaDataBLL bll = new MetaDataBLL();
        public IHttpActionResult Get([FromUri]Request<MetaData> request)
        {
            try
            {
                Response<List<TreeItem>> response = new Response<List<TreeItem>>();
                List<TreeItem> treeList = bll.GetTreeList(request.Keyword);
                response.Data = treeList;
                return Ok(response);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString() + ex.InnerException);
                return BadRequest(ex.Message);
            }
        }
    }
}
