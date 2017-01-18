using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Examine
{
    public class SupDoctor : BaseModel
    {
        /// <summary>
        /// 专家ID
        /// </summary>
        public string DoctorID { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string DocName { get; set; }
        /// <summary>
        /// 性别：0女，1男，-1未知
        /// </summary>
        public int? DocSex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public Nullable<System.DateTime> BrithDay { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNumber { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        public string DocProvince { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string DocCity { get; set; }
        /// <summary>
        /// 所在医院
        /// </summary>
        public string DocHospital { get; set; }
        /// <summary>
        /// 所在科室
        /// </summary>
        public string DocDepartment { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        public string DocTitle { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string DocMajor { get; set; }
        /// <summary>
        /// 擅长疾病
        /// </summary>
        public string DocDisease { get; set; }
        /// <summary>
        /// 擅长疾病编码
        /// </summary>
        public string DiseaseCode { get; set; }
        /// <summary>
        /// 专家介绍
        /// </summary>
        public string DocDescrip { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string CheckStatus { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>
        public string OnlineStatus { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 年龄，通过生日计算
        /// </summary>
        public int Age
        {
            get
            {
                if (BrithDay == null)
                    return 0;
                return DateTime.Today.Year - BrithDay.Value.Year;
            }
        }
        /// <summary>
        /// 审核不通过的原因
        /// </summary>
        public string CheckRemark { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string IconPath { get; set; }


        /// <summary>
        /// 是否关注
        /// </summary>
        public string IsAttention { get; set; }
        /// <summary>
        /// 是否主诊医生
        /// </summary>
        public string IsAttentionDoctor { get; set; }



        /// <summary>
        /// 是否 我的医生  0不是  1是
        /// </summary>
        public string IsMyFollowDoctor { get; set; }





        /// <summary>
        /// 当前是否值班状态
        /// </summary>
        public string IsOnDuty { get; set; }

        /// <summary>
        /// 所在省份名称
        /// </summary>
        public string DocProvinceName { get; set; }

        /// <summary>
        /// 所在城市名称
        /// </summary>
        public string DocCityName { get; set; }

        /// <summary>
        /// 常用联系方式
        /// </summary>
        public string CommonContact { get; set; }
    }
}
