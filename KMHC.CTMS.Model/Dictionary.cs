using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Dictionary
    {
        public Dictionary()
        {
            nodes = new List<Dictionary>();
        }

        #region 模型属性
        /// <summary>
        /// 字典ID
        /// </summary>
        public string nodeId { get; set; }

        public string tempNodeId
        {
            get
            {
                string temp = nodeId;
                return temp;
            }
        }

        /// <summary>
        /// 父字典ID
        /// </summary>
        public string parentId { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictionCategory { get; set; }

        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string DictionCategoryName { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string value { get; set; }


        public int ivalue
        {
            get
            {
                int i = 0;
                if (string.IsNullOrEmpty(value)) return i;
                try { i = int.Parse(value); }
                catch { }
                return i;

            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        #endregion

        /// <summary>
        /// 子节点列表
        /// </summary>
        public List<Dictionary> nodes { get; set; }
    }
}
