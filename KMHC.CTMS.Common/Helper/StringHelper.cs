using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Xml.Serialization;

namespace Project.Common.Helper
{
    /// <summary>
    /// <para>Copyright (C) 2015 康美健康云服务有限公司版权所有</para>
    /// <para>文 件 名： StringExtension.cs</para>
    /// <para>文件功能： 字符串转化其它类型的类</para>
    /// <para>开发部门： 平台部</para>
    /// <para>创 建 人： lmf</para>
    /// <para>创建日期： 2015.10.19</para>
    /// <para>修 改 人： </para>
    /// <para>修改日期： </para>
    /// <para>备    注： </para>
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 字符串转化为字节,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte? ToByte(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            byte result;
            if (byte.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 字符串转化为整数,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToInt(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 字符串转化为浮点型,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float? ToFloat(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            float result;
            if (float.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 字符串转化为小数型,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Decimal? ToDecimal(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Decimal result;
            if (Decimal.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 字符串转化为日期,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string value)
        {
            return ToDateTime(value, null);
        }

        /// <summary>
        /// 字符串转化为日期,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format">日期格式</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string value, string format)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            DateTime result;
            if (string.IsNullOrWhiteSpace(format))
            {
                if (DateTime.TryParse(value, out result))
                {
                    return result;
                }
                return null;
            }
            if (DateTime.TryParseExact(value, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;
        }



        /// <summary>
        /// 字符串转化为枚举类型,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            try
            {
                var TEnum = (T)Enum.Parse(typeof(T), value);
                return TEnum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 字符串转化为GUID,当字符串为空,返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Guid? ToGuid(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Guid result;
            if (Guid.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static string ToMd5(this string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value is null");
            return FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5").ToUpper();
        }

        /// <summary>
        /// 将字符串转为double，否则返回默认值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(this string value, double defaultValue)
        {
            double.TryParse(value, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 将字符串转为int，否则返回默认值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt(this string value, int defaultValue)
        {
            int.TryParse(value, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 将字符串转为decimal，否则返回默认值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string value, decimal defaultValue)
        {
            decimal.TryParse(value, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string getIPAddress()
        {
            string result = String.Empty;
            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // 如果使用代理，获取真实IP
            if (result != null && result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                result = null;
            else if (result != null)
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];    //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;    //代理中的内容 非IP，取IP 
            }
            if (null == result || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (result == null || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.UserHostAddress;
            return result;
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        /// <summary>
        /// 判断email格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            string res = string.Empty;
            string expression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            if (!Regex.IsMatch(str, expression, RegexOptions.Compiled))
                return false;
            return true;
        }

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int getTimeStamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toaddress">收件人邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件内容</param>
        public static void SendMail(string toaddress, string subject, string body)
        {
            MailAddress from = new MailAddress("AntiCancer365@kmhealthcloud.com", "康爱365");
            MailAddress to = new MailAddress(toaddress, toaddress);
            MailMessage mail = new MailMessage();
            mail.From = from;
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Host = "mail.kmhealthcloud.com";
            client.Port = 587;
            client.Credentials = new NetworkCredential("AntiCancer365@kmhealthcloud.com", "Abc123456");

            client.Send(mail);
        }

    }


}
