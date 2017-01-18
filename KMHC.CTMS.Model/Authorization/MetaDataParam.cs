using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Authorization
{
    public class MetaDataParam : BaseModel
    {
        /// <summary>
        /// 主键，自增
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 元数据ID
        /// </summary>
        public int MetaDataID { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParamValue { get; set; }
    }
}
