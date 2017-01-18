using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Model;
using Project.BLL;
using Project.DAL;
using KMHC.CTMS.UI.Controllers;
using Project.Common.Helper;
using System.Xml.Serialization;
using System.IO;

namespace Project.UI.Controllers
{
    public class UserController:Controller 
    {
        private ctms_sys_userinfoBLL bll = new ctms_sys_userinfoBLL();

        #region 属性
        /// <summary>
        /// 缓存字符串
        /// </summary>
        private string CacheString
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheString"];
            }
        }

        /// <summary>
        /// 缓存时间
        /// </summary>
        private int CacheTime
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheTime"].ToInt(30);
            }
        }

        /// <summary>
        /// 重置密码链接有效时长
        /// </summary>
        private int ResetPasswordTime
        {
            get
            {
                return ConfigurationManager.AppSettings["ResetPasswordTime"].ToInt(24);
            }
        }
        #endregion

        //
        // GET: /User/
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UserLogin(V_ctms_sys_userinfo user)
        {
            try
            {
                //object obj = Session["_vercode_"];
                //if (obj == null)
                //return Json(new { Status = -9, Data = string.Empty, Msg = "验证码失效，\n请重新获取验证码" });
                //string code = obj.ToString();
                //if (string.IsNullOrEmpty(code))
                //return Json(new { Status = -8, Data = string.Empty, Msg = "验证码无效，\n请重新获取验证码" });
                if (user == null)
                    return Json(new { Status = -7, Data = string.Empty, Msg = "参数错误，\n请输入用户信息" });
                //if(user.Vercode.Trim() != code.Trim())
                //return Json(new { Status = -6, Data = string.Empty, Msg = "验证码错误，\n请重新输入验证码" });
                if (string.IsNullOrEmpty(user.LOGINNAME) || string.IsNullOrEmpty(user.LOGINPWD))//参数错误
                    return Json(new { Status = -5, Data = string.Empty, Msg = "参数错误" });

                V_ctms_sys_userinfo userInfo = bll.Login(user.LOGINNAME, user.LOGINPWD);

                if (userInfo == null)//密码错误
                    return Json(new { Status = -4, Data = string.Empty, Msg = "密码错误" });

                //产生令牌
                string tokenValue = this.GetGuidString();
                bll.CacheInfo(tokenValue + CacheString, tokenValue);

                bll.CacheInfo(tokenValue, userInfo);

                HttpCookie tokenCookie = new HttpCookie("Token");
                tokenCookie.Value = tokenValue;
                tokenCookie.Expires = DateTime.Now.AddMinutes(CacheTime);
                tokenCookie.Path = "/";
                Response.AppendCookie(tokenCookie);

                return Json(new { Status = 1, Data = tokenValue, Msg = string.Empty });
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }            
        }

        public ActionResult UserLogout()
        {
            try
            {
                bll.Logout();
                return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult UserRegister(V_ctms_sys_userinfo user)
        {
            try
            {
                V_ctms_sys_userinfo u = bll.GetUserByLoginName(user.LOGINNAME);
                if (u != null)
                    return Json(new { Status = -9, Data = string.Empty, Msg = "用户名已存在" });

                if(user.LOGINPWD.Trim() != user.ConfirmLoginPwd.Trim())
                    return Json(new { Status = -8, Data = string.Empty, Msg = "两次输入密码不一致" });

                user.USERID = GetGuidString();
                user.LOGINPWD = user.LOGINPWD.ToMd5();
                user.USERTYPE =  1;
                user.CREATEDATETIME = DateTime.Now;
                user.CREATEUSERID = string.Empty;
                user.CREATEUSERNAME = string.Empty;
                user.EDITDATETIME = DateTime.Now;
                user.EDITUSERID = string.Empty;
                user.EDITUSERNAME = string.Empty;
                user.OWNERID = string.Empty;
                user.OWNERNAME = string.Empty;
                bool result = bll.AddUserInfo(user);
                if(!result)
                    return Json(new { Status = -7, Data = string.Empty, Msg = "操作失败" });

                return Json(new { Status = 1, Data = string.Empty, Msg = "操作成功" });
            }
            catch (Exception ex)
            {
                return Json(new { Status = -99, Data = string.Empty, Msg = ex.Message });
            }            
        }

        /// <summary>
        /// 获取令牌(GUID)
        /// </summary>
        /// <returns></returns>
        private string GetGuidString()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        #region 获取验证码模块
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVercode()
        {
            Response.ContentType = "image/jpeg";
            //获得验证码符号
            string code = GetCode();
            Session["_vercode_"] = code;
            Image img = GetImage(code);
            img.Save(Response.OutputStream, ImageFormat.Jpeg);
            Response.Flush();
            return null;
        }

        #region 生成验证码图片
        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static Image GetImage(string code)
        {
            Bitmap bitmap = new Bitmap(100, 30);
            //画板
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, 100, 30));
            //在画板上输出符号
            g.DrawString(code, new Font("楷体", 24), Brushes.CornflowerBlue, 10, 0);
            return bitmap;
        }
        #endregion

        #region 获取随机验证码
        /// <summary>
        /// 获取随机验证码
        /// </summary>
        /// <returns></returns>
        private static string GetCode()
        {
            string temp = "0123456789abcdefghijklmnopqrstuvwxyz";
            string code = string.Empty;
            Random r = new Random();
            for (int i = 0; i < 4; i++)
            {
                //存储验证码符号
                code += temp[r.Next(0, temp.Length)];
            }
            return code;
        }
        #endregion
        #endregion

        
    }
}