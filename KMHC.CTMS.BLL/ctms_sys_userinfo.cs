/*
 * 描述:
 *  
 * 修订历史: 
 * 日期                    修改人              Email                   内容
 * 2016-11-16                                         创建 
 *  
 */

using Project.Common;
using Project.Common.Cached;
using Project.Common.Helper;
using Project.DAL;
using Project.DAL.Database;
using Project.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Project.BLL
{
    public class ctms_sys_userinfoBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(V_ctms_sys_userinfo model)
        {
            if (model == null)
                return string.Empty;

            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                ctms_sys_userinfo entity = ModelToEntity(model);
                entity.USERID = string.IsNullOrEmpty(model.USERID) ? Guid.NewGuid().ToString("N") : model.USERID;

                return dal.Add(entity);
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <returns></returns>
        public V_ctms_sys_userinfo Get(Expression<Func<ctms_sys_userinfo, bool>> predicate = null)
        {
            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                ctms_sys_userinfo entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public List<V_ctms_sys_userinfo> GetList(ref PageInfo pager, string keyWord, string filter = "", ArrayList args = null)
        {
            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                int userType = (int)UserType.管理员;
                var list = dal.FindAll(p => p.USERTYPE != userType);
                if (!string.IsNullOrEmpty(keyWord))
                {
                    list = list.Where(u => u.USERNAME.Contains(keyWord));
                }
                if (!string.IsNullOrEmpty(filter) && args != null)
                    list = list.where(filter, args.ToArray());
                return list.Paging(ref pager).ToList().Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<V_ctms_sys_userinfo> Get()
        {
            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                List<ctms_sys_userinfo> entitys = dal.Get().ToList();
                List<V_ctms_sys_userinfo> list = new List<V_ctms_sys_userinfo>();
                foreach (ctms_sys_userinfo item in entitys)
                {
                    list.Add(EntityToModel(item));
                }
                return list;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<V_ctms_sys_userinfo> GetList(PageInfo page)
        {
            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                var list = dal.Get();

                return list.Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(V_ctms_sys_userinfo model)
        {
            if (model == null) return false;
            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                ctms_sys_userinfo entitys = ModelToEntity(model);

                return dal.Edit(entitys);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return false;
            using (ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL())
            {
                return dal.Delete(id);
            }
        }

        /// <summary>
        /// Model转Entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ctms_sys_userinfo ModelToEntity(V_ctms_sys_userinfo model)
        {
            if (model != null)
            {
                ctms_sys_userinfo entity = new ctms_sys_userinfo()
                {
                    USERID = model.USERID,
                    LOGINNAME = model.LOGINNAME,
                    USERNAME = model.USERNAME,
                    LOGINPWD = model.LOGINPWD,
                    MOBILEPHONE = model.MOBILEPHONE,
                    EMAIL = model.EMAIL,
                    USERTYPE = model.USERTYPE,
                    USERSTATUS = model.USERSTATUS,
                    MEMBERID = model.MEMBERID,
                    MEMBERSTARTDATE = model.MEMBERSTARTDATE,
                    MEMBERENDDATE = model.MEMBERENDDATE,
                    ACCOUNT = model.ACCOUNT,
                    ICONPATH = model.ICONPATH,
                    CREATEUSERNAME = model.CREATEUSERNAME,
                    EDITUSERNAME = model.EDITUSERNAME,
                    OWNERNAME = model.OWNERNAME,
                    CREATEUSERID = model.CREATEUSERID,
                    CREATEDATETIME = model.CREATEDATETIME,
                    EDITUSERID = model.EDITUSERID,
                    EDITDATETIME = model.EDITDATETIME,
                    ISDELETED = model.ISDELETED,
                    OWNERID = model.OWNERID,
                    OWNERPOSTID = model.OWNERPOSTID,
                    OWNERDEPARTMENTID = model.OWNERDEPARTMENTID,
                    OWNERCOMPANYID = model.OWNERCOMPANYID,
                };

                return entity;
            }
            return null;
        }

        /// <summary>
        /// Entity转Model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private V_ctms_sys_userinfo EntityToModel(ctms_sys_userinfo entity)
        {
            if (entity != null)
            {
                V_ctms_sys_userinfo model = new V_ctms_sys_userinfo()
                {
                    USERID = entity.USERID,
                    LOGINNAME = entity.LOGINNAME,
                    USERNAME = entity.USERNAME,
                    LOGINPWD = entity.LOGINPWD,
                    MOBILEPHONE = entity.MOBILEPHONE,
                    EMAIL = entity.EMAIL,
                    USERTYPE = entity.USERTYPE,
                    USERSTATUS = entity.USERSTATUS,
                    MEMBERID = entity.MEMBERID,
                    MEMBERSTARTDATE = entity.MEMBERSTARTDATE,
                    MEMBERENDDATE = entity.MEMBERENDDATE,
                    ACCOUNT = entity.ACCOUNT.Value,
                    ICONPATH = entity.ICONPATH,
                    CREATEUSERNAME = entity.CREATEUSERNAME,
                    EDITUSERNAME = entity.EDITUSERNAME,
                    OWNERNAME = entity.OWNERNAME,
                    CREATEUSERID = entity.CREATEUSERID,
                    CREATEDATETIME = entity.CREATEDATETIME,
                    EDITUSERID = entity.EDITUSERID,
                    EDITDATETIME = entity.EDITDATETIME,
                    ISDELETED = entity.ISDELETED,
                    OWNERID = entity.OWNERID,
                    OWNERPOSTID = entity.OWNERPOSTID,
                    OWNERDEPARTMENTID = entity.OWNERDEPARTMENTID,
                    OWNERCOMPANYID = entity.OWNERCOMPANYID,
                };

                return model;
            }

            return null;
        }



        private ICached _cached = new LocalCached();

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
        /// 缓存字符串
        /// </summary>
        private string CacheString
        {
            get
            {
                return ConfigurationManager.AppSettings["CacheString"];
            }
        }

        protected static ctms_sys_userinfoDAL dal = new ctms_sys_userinfoDAL();
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPwd">登录密码</param>
        /// <returns></returns>
        public V_ctms_sys_userinfo Login(string loginName, string loginPwd)
        {
            ctms_sys_userinfo userInfo = dal.GetUserInfoByLoginName(loginName);
            if (userInfo == null)
                return EntityToModel(userInfo);

            loginPwd = loginPwd.ToMd5();
            if (!userInfo.LOGINPWD.Equals(loginPwd))
                return null;
            return EntityToModel(userInfo);
        }

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public V_ctms_sys_userinfo GetUserInfoByID(string id)
        {
            ctms_sys_userinfo userInfo = dal.GetUserInfoByID(id);
            return EntityToModel(userInfo);
        }

        /// <summary>
        /// 根据登录名获取用户信息
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <returns></returns>
        public V_ctms_sys_userinfo GetUserByLoginName(string loginName)
        {
            ctms_sys_userinfo userInfo = dal.GetUserInfoByLoginName(loginName);
            return EntityToModel(userInfo);
        }

        /// <summary>
        /// 通过邮箱获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public V_ctms_sys_userinfo GetUserByEmail(string email)
        {
            ctms_sys_userinfo userInfo = dal.GetUserInfoByEmail(email);
            return EntityToModel(userInfo);
        }

        /// <summary>
        /// 通过手机获取用户信息
        /// </summary>
        /// <param name="mobilePhone"></param>
        /// <returns></returns>
        public V_ctms_sys_userinfo GetUserByMobilePhone(string mobilePhone)
        {
            ctms_sys_userinfo userInfo = dal.GetUserInfoByMobilePhone(mobilePhone);
            return EntityToModel(userInfo);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUserInfo(V_ctms_sys_userinfo user)
        {
            if (user == null) return false;
            ctms_sys_userinfo userinfo = ModelToEntity(user);

            return dal.AddUserInfo(userinfo);
        }

        /// <summary>
        /// 通过id更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUserInfo(V_ctms_sys_userinfo user)
        {
            if (user == null) return false;
            ctms_sys_userinfo userinfo = ModelToEntity(user);

            return dal.UpdateUserInfo(userinfo);
        }

        /// <summary>
        /// 缓存信息
        /// </summary>
        /// <param name="tokenValue"></param>
        /// <param name="userInfo"></param>
        public void CacheInfo(string tokenValue, object userInfo)
        {
            _cached.Set(tokenValue, userInfo, CacheTime);
        }

        /// <summary>
        /// 根据令牌获取用户登录信息(没有则返回null)
        /// </summary>
        /// <param name="tokenValue">令牌</param>
        /// <returns></returns>
        public V_ctms_sys_userinfo GetLoginInfo(string tokenValue)
        {
            if (string.IsNullOrEmpty(tokenValue))
                return null;
            V_ctms_sys_userinfo userInfo = _cached.Get(tokenValue) as V_ctms_sys_userinfo;
            return userInfo;
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            try
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
                if (cookie == null)
                    return true;
                _cached.Remove(cookie.Value + CacheString);
                _cached.Remove(cookie.Value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断当前是否已登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogin()
        {
            bool isLogin = false;
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            if (cookie != null)
            {
                V_ctms_sys_userinfo user = GetLoginInfo(cookie.Value);
                if (user != null)
                {
                    isLogin = true;
                }
            }
            return isLogin;
        }

        public V_ctms_sys_userinfo GetCurrentUser()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Token"];
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                return null;
            }
            return GetLoginInfo(cookie.Value);
        }


    }
}
