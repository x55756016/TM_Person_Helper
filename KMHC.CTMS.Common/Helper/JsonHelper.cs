namespace Project.Common.Helper
{
    /// <summary>
    /// <para>Copyright (C) 2015 康美健康云服务有限公司版权所有</para>
    /// <para>文 件 名： JsonHelper.cs</para>
    /// <para>文件功能： json与对象相互转化类</para>
    /// <para>开发部门： 平台部</para>
    /// <para>创 建 人： lmf</para>
    /// <para>创建日期： 2015.10.19</para>
    /// <para>修 改 人： </para>
    /// <para>修改日期： </para>
    /// <para>备    注： </para>
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 对象转化为json格式字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object val)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(val);
        }

        /// <summary>
        /// json格式字符串转化为对象
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static object JsonDeserialize(this string val)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(val);
        }

        /// <summary>
        /// json格式字符串转化为指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string val)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(val);
        }
    }
}
