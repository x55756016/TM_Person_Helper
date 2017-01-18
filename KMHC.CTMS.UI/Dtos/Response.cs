namespace Project.UI.Dtos
{
    public class Response<T>
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// 总页数
        /// </summary>
        public int PagesCount { get; set; }

        public int RecordsCount { get; set; }

        private bool _isSuccess = true;
        /// <summary>
        /// 访问成功
        /// </summary>
        public bool IsSuccess { 
            get{ return _isSuccess;}
            set { _isSuccess = value; } 
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
