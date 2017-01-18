using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public enum OrganUserType
    {
        Company = 0,
        Department = 1,
        Position = 2,
        User = 3
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        [Description("查看")]
        View = 1,

        [Description("查询")]
        Search = 2,

        [Description("新增")]
        Add = 4,

        [Description("修改")]
        Modify = 8,

        [Description("删除")]
        Delete = 16,

        [Description("导入")]
        Upload = 32,

        [Description("导出")]
        DownLoad = 64
    }

    #region 用户类型信息
    /// <summary>
    /// USERINFO的UserType字段
    /// </summary>
    public enum UserType
    {
        平台医生 = 1,
        患者 = 2,
        管理员 = 3,
        医学编辑 = 4,
        客服 = 5,
        肿瘤专家 = 6
    }
    #endregion

    #region 服务器告警类型
    public enum ServerAlarmType
    {
        CPU告警 = 0,
        内存告警 = 1,
        硬盘告警 = 2
    }
    #endregion

    #region 权限相关
    /// <summary>
    /// 功能状态
    /// </summary>
    public enum FunctionStatus
    {
        [Description("正常")]
        Normal = 0,

        [Description("禁用")]
        Disabled = 1,

        [Description("暂停使用")]
        Paused = 2
    }

    /// <summary>
    /// 元数据 数据类型
    /// </summary>
    public enum DataType
    {
        [Description("字符串")]
        String = 0,

        [Description("整数")]
        Int = 1,

        [Description("小数")]
        Double = 2,

        [Description("布尔值")]
        Bool = 3
    }

    public static class EnumHelper
    {
        /// <summary>
        /// 获取枚举的Description
        /// </summary>
        /// <param name="value"></param>
        /// <param name="nameInstead"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, Boolean nameInstead = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstead == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }

        /// <summary>
        /// 获取他的int值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValueString(this Enum value)
        {
            return (Convert.ToInt32(value)).ToString();
        }
    }

    /// <summary>
    /// 数据来源类型
    /// </summary>
    public enum DataSourceType
    {
        [Description("表")]
        Table = 0,

        [Description("函数")]
        Func = 1,

        [Description("存储过程")]
        StoreProcess = 2
    }
    #endregion
}
