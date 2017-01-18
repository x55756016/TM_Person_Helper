using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model.Examine
{
    public class SupDoctorPatientDto
    {
        public string DOCTORPATIENTID { get; set; }
        public string USERID { get; set; }
        public string UserName { get; set; }
        public string PATIENTGROUPID { get; set; }
        public string GROUPLABEL { get; set; }
        public string GROUPNAME { get; set; }
        public string FANSUSERID { get; set; }
        public string FANSNAME { get; set; }
        public int CHECKSTATUS { get; set; }
        public string CREATEUSERID { get; set; }
        public string CREATEUSERNAME { get; set; }
        public Nullable<System.DateTime> CREATEDATETIME { get; set; }
        public string EDITUSERID { get; set; }
        public string EDITUSERNAME { get; set; }
        public Nullable<System.DateTime> EDITDATETIME { get; set; }
        public string OWNERID { get; set; }
        public string OWNERNAME { get; set; }
        public bool ISDELETED { get; set; }
        public Nullable<System.DateTime> BIRTHDATE { get; set; }
        public string LoginName { get; set; }


        public string DoctorDesc { get; set; }
        public string OnlineStatus { get; set; }

        public string PHONENUM { get; set; }

        public string DoctorLoginName { get; set; }

        public string CurrentStep { get; set; }
        public string CurrentStepName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public Nullable<int> Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CityName { get; set; }


        public string IconPath { get; set; }



        public string DiseaseName { get; set; }
    }
}
