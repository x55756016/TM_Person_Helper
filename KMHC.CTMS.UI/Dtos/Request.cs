using System.Collections.Generic;

namespace Project.UI.Dtos
{
    public class Request<T> 
    {   
        /// <summary>
        /// 模型
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 模型列表
        /// </summary>
        public List<T> DataList { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 参数键值对 对应前端Params：[{Key:"aa",Value:1},{Key:"Isaa",Value:true}]
        /// </summary>
        public List<KeyValuePair<string, object>> Params { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        public bool ContainsDeleted { get; set; }


    }
}
