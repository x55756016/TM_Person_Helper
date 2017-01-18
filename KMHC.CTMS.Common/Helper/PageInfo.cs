using System;
using System.Collections;

namespace Project.Common.Helper
{
    /// <summary>
    /// 分页类
    /// author:liumeifang
    /// </summary>
    [Serializable]
    public class PageInfo
    {
        private int pageSize;
        private int pageIndex;

        /// <summary>
        /// 每页个数
        /// </summary>
        public int PageSize
        {
            get { return pageSize <= 0 ? 10 : pageSize; }
            set { pageSize = value <= 0 ? 10 : value; }
        }

        /// <summary>
        /// 当前页号
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex <= 0 ? 1 : pageIndex; }
            set { pageIndex = value <= 0 ? 1 : value; }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderField { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderEnum Order { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PagesCount
        {
            get
            {
                if (Total <= 0 || PageSize <= 0) return 0;
                return (Total / PageSize) + (Total % PageSize == 0 ? 0 : 1);
            }
        }
    }


    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderEnum
    {
        asc=0,//顺序
        desc//倒序
    }


    [Serializable]
    public class PageJson//<T> where T:class 
    {
        public int total { get; set; }
        public IEnumerable rows { get; set; }

        public PageJson(IEnumerable rows, int total)
        {
            this.rows = rows;
            this.total = total;
        }
    }

    public static class PagingHelper//<T> where T:class 
    {
        public static string ConvertJson(IEnumerable rows, int total)
        {
            var pageList = new PageJson(rows, total);
            return pageList.JsonSerialize();
        }

    }
}
