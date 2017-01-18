using Project.Common;
using Project.Common.Helper;
using Project.DAL.Database;
using Project.Model.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project.BLL.Authorization
{
    public class RoleFunctionBLL
    {
        private readonly string logTitle = "访问RoleFunctionBLL类";
        public RoleFunctionBLL()
        {

        }

        /// <summary>
        /// 新增角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(RoleFunction model)
        {
            if (model == null) return string.Empty;
            using (DbContext db = new tmpmEntities2())
            {
                model.RoleFunctionID = Guid.NewGuid().ToString();
                db.Set<ctms_sys_rolefunction>().Add(ModelToEntity(model));

                db.SaveChanges();
                return model.RoleFunctionID;
            }
        }

        /// <summary>
        /// 修改角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Edit(RoleFunction model)
        {
            if (string.IsNullOrEmpty(model.RoleFunctionID))
            {
                LogHelper.WriteInfo("试图修改为空的RoleFunction实体！");
                throw new KeyNotFoundException();
            }
            using (DbContext db = new tmpmEntities2())
            {
                db.Entry(ModelToEntity(model)).State = EntityState.Modified;

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 删除角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                LogHelper.WriteInfo("试图删除为空的RoleFunction实体！");
                throw new KeyNotFoundException();
            }
            RoleFunction model = Get(id);
            if (model != null)
            {
                model.IsDeleted = true;
                return Edit(model);
            }
            return false;
        }

        /// <summary>
        /// 删除角色功能权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteById(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var entity = db.Set<ctms_sys_rolefunction>().Find(id);
                if (entity != null)
                {
                    db.Set<ctms_sys_rolefunction>().Remove(entity);
                }
                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RoleFunction GetOne(Expression<Func<ctms_sys_rolefunction, bool>> predicate = null)
        {
            using (DbContext db = new tmpmEntities2())
            {
                var set = db.Set<ctms_sys_rolefunction>().AsNoTracking();
                return (predicate == null)
                    ? EntityToModel(set.FirstOrDefault())
                    : EntityToModel(set.FirstOrDefault(predicate));
            }
        }

        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public RoleFunction Get(string id)
        {
            using (DbContext db = new tmpmEntities2())
            {
                ctms_sys_rolefunction entity = db.Set<ctms_sys_rolefunction>().Find(id);
                if (entity == null || string.IsNullOrEmpty(entity.ROLEFUNCTIONID)) return null;
                return EntityToModel(entity);
            }
        }

        /// <summary>
        /// 获取查询列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public List<RoleFunction> GetList()
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_rolefunction> query = db.Set<ctms_sys_rolefunction>().AsNoTracking().Where(o => o.ISDELETED == 0).ToList();
                List<RoleFunction> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<RoleFunction> GetList(Expression<Func<ctms_sys_rolefunction, bool>> predicate = null)
        {
            using (DbContext db = new tmpmEntities2())
            {
                IEnumerable<ctms_sys_rolefunction> query = db.Set<ctms_sys_rolefunction>().AsNoTracking().Where(predicate).ToList();
                List<RoleFunction> list = (from m in query select EntityToModel(m)).ToList();
                return list;
            }
        }

        /// <summary>
        /// 根据userID获取有权限的功能点
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="isMenu">是否为菜单</param>
        /// <returns></returns>
        public List<RoleFunction> GetAuthorizedList(string userID)
        {
            using (DbContext db = new tmpmEntities2())
            {
                if (string.IsNullOrEmpty(userID))
                    return new List<RoleFunction>();

                var roleIDList = db.Set<ctms_sys_userrole>()
                    .AsNoTracking()
                    .Where(o => o.isdeleted == 0 && o.userid.Equals(userID))
                    .Select(o => o.roleid)
                    .ToList();

                return db.Set<ctms_sys_rolefunction>()
                    .AsNoTracking()
                    .Where(o => o.ISDELETED == 0 && roleIDList.Contains(o.ROLEID))
                    .Select(EntityToModel)
                    .ToList();
            }
        }

        /// <summary>
        /// 根据用户ID，功能编码和权限类型 获取角色功能权限实体
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="authCode">权限编码</param>
        /// <param name="permission">权限类型</param>
        /// <returns></returns>
        public List<RoleFunction> GetAuthorizedList(string userID, string authCode, PermissionType permission)
        {
            List<RoleFunction> roleFunctionList = new List<RoleFunction>();
            if (string.IsNullOrEmpty(authCode) || string.IsNullOrEmpty(authCode))
                return roleFunctionList;
            using (DbContext db = new tmpmEntities2())
            {
                var function = db.Set<ctms_sys_function>().AsNoTracking().Where(o => o.functioncode.Equals(authCode)).ToList();
                if (function == null || function.Count == 0) return roleFunctionList;
                string functionid = function[0].functionid;
                var roleIDList = db.Set<ctms_sys_userrole>()
                   .AsNoTracking()
                   .Where(o => o.isdeleted == 0 && o.userid.Equals(userID))
                   .Select(o => o.roleid)
                   .ToList();

                roleFunctionList = db.Set<ctms_sys_rolefunction>()
                    .AsNoTracking()
                    .Where
                    (
                        o => o.ISDELETED == 0
                        && roleIDList.Contains(o.ROLEID)
                        && o.FUNCTIONID.Equals(functionid)
                        && o.PERMISSIONVALUE == (int)permission
                     )
                    .Select(EntityToModel)
                    .ToList();
                return roleFunctionList;
            }
        }

        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="authCode">权限编码</param>
        /// <param name="permission">权限类型</param>
        /// <returns></returns>
        public bool IsHavePermission(string userID, string authCode, PermissionType permission)
        {
            return !string.IsNullOrEmpty(userID) && (string.IsNullOrEmpty(authCode) || GetAuthorizedList(userID, authCode, permission).Count > 0);
        }

        /// <summary>
        /// 获取范围筛选的过滤字符串和参数
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="authCode">权限编码</param>
        /// <param name="permission">权限类型</param>
        /// <param name="args">参数值列表</param>
        /// <returns></returns>
        public string GetFilterString(string userID, string authCode, PermissionType permission, ref ArrayList args)
        {
            List<RoleFunction> roleFunctionList = GetAuthorizedList(userID, authCode, permission);
            if (roleFunctionList.Count == 0)
            {
                return string.Empty;
            }
            else if (roleFunctionList.Count == 1)
            {
                return GetFilerStringSummary(roleFunctionList[0], ref args);

            }
            else
            {
                string filterString = string.Empty;
                foreach (RoleFunction roleFunction in roleFunctionList)
                {
                    filterString += string.Format("{0}({1})", string.IsNullOrEmpty(filterString) ? "" : " or", GetFilerStringSummary(roleFunction, ref args));
                }
                return filterString;
            }

        }

        /// <summary>
        /// 根据xml获取过滤表达式
        /// </summary>
        /// <param name="xml">范围xml</param>
        /// <returns></returns>
        public string GetFilterByXml(string xml, ref ArrayList args)
        {
            if (string.IsNullOrEmpty(xml)) return " 1=1 ";
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);

                if (doc == null) throw new Exception("错误的xml格式");
                XmlNodeList itemNodeList = doc.SelectNodes("/data/item");
                if (itemNodeList == null || itemNodeList.Count == 0) return " 1=1 ";
                string filterString = string.Empty;
                foreach (XmlNode node in itemNodeList)
                {
                    XmlElement xe = (XmlElement)node;
                    string relationship = xe.SelectNodes("relationship")[0].InnerXml;
                    string column = xe.SelectNodes("column")[0].InnerXml;
                    string operation = xe.SelectNodes("operation")[0].InnerXml;
                    string value = xe.SelectNodes("value")[0].InnerXml;
                    filterString += string.Format(" {0} {1}{2}{3}", string.IsNullOrEmpty(filterString) ? "" : relationship, column, operation, "@" + args.Count);
                    args.Add(value);
                }
                return filterString;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取所有权限，包括范围权限和组织架构权限
        /// </summary>
        /// <param name="roleFunction"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string GetFilerStringSummary(RoleFunction roleFunction, ref ArrayList args)
        {
            string filterExpress = GetFilterByXml(roleFunction.DataRange, ref args);
            //获取组织架构权限
            string organFilterExpress = GetOrgFilter(roleFunction.RoleFunctionID, ref args);

            if (string.IsNullOrEmpty(filterExpress) && string.IsNullOrEmpty(organFilterExpress)) //无范围权限管控,无组织架构权限管控
            {
                return "";
            }
            else if (string.IsNullOrEmpty(filterExpress) && !string.IsNullOrEmpty(organFilterExpress)) //无范围权限管控,有组织架构权限管控
            {
                filterExpress = organFilterExpress;
            }
            else if (!string.IsNullOrEmpty(filterExpress) && string.IsNullOrEmpty(organFilterExpress)) //有范围权限管控,无组织架构权限管控
            {

            }
            else
            {
                filterExpress = string.Format("({0}) and ({1})", filterExpress, organFilterExpress);//有范围权限管控,有组织架构权限管控
            }
            return filterExpress;
        }

        /// <summary>
        /// 获取组织架构过滤表达式
        /// </summary>
        /// <param name="roleFunctionID"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string GetOrgFilter(string roleFunctionID, ref  ArrayList args)
        {
            List<RoleFunctionOrg> list = new RoleFunctionOrgBLL().GetList(o => o.rolefunctionid.Equals(roleFunctionID));
            if (list == null || list.Count == 0)
            {
                return "";
            }
            else
            {
                string filterString = string.Empty;
                foreach (RoleFunctionOrg rfo in list)
                {
                    string relationship = "or";
                    string column = "";
                    int orgType = 2;
                    int.TryParse(rfo.OrgType, out orgType);
                    switch ((OrganUserType)orgType)
                    {
                        case OrganUserType.Company:
                            column = "OWNERCOMPANYID";
                            break;
                        case OrganUserType.Department:
                            column = "OWNERDEPARTMENTID";
                            break;
                        case OrganUserType.Position:
                            column = "OWNERPOSTID";
                            break;
                        default:
                            column = "OWNERPOSTID";
                            break;
                    }
                    string operation = "=";
                    string value = rfo.OrgID;
                    filterString += string.Format(" {0} {1}{2}{3}", string.IsNullOrEmpty(filterString) ? "" : relationship, column, operation, "@" + args.Count);
                    args.Add(value);
                }
                return filterString;
            }
        }

        private ctms_sys_rolefunction ModelToEntity(RoleFunction model)
        {
            if (model == null) return null;
            return new ctms_sys_rolefunction()
            {
                ROLEFUNCTIONID = string.IsNullOrEmpty(model.RoleFunctionID) ? Guid.NewGuid().ToString() : model.RoleFunctionID,
                FUNCTIONID = model.FunctionID,
                PERMISSIONVALUE = model.PermissionValue,
                ROLEID = model.RoleID,
                REMARK = model.Remark,
                DATARANGE = model.DataRange,
                CREATEDATETIME = model.CreateDateTime,
                CREATEUSERID = model.CreateUserID,
                CREATEUSERNAME = model.CreateUserName,
                EDITDATETIME = model.EditTime,
                EDITUSERID = model.EditUserID,
                EDITUSERNAME = model.EditUserName,
                OWNERID = model.OwnerID,
                OWNERNAME = model.OwnerName,
                ISDELETED = model.IsDeleted ? 1 : 0,
                ISSETORG = model.IsSetOrg
            };
        }

        private RoleFunction EntityToModel(ctms_sys_rolefunction entity)
        {
            if (entity == null) return null;
            return new RoleFunction()
            {
                RoleFunctionID = entity.ROLEFUNCTIONID,
                RoleID = entity.ROLEID,
                FunctionID = entity.FUNCTIONID,
                PermissionValue = (int)entity.PERMISSIONVALUE,
                Remark = entity.REMARK,
                DataRange = entity.DATARANGE,
                CreateDateTime = entity.CREATEDATETIME,
                CreateUserID = entity.CREATEUSERID,
                CreateUserName = entity.CREATEUSERNAME,
                EditTime = entity.EDITDATETIME,
                EditUserID = entity.EDITUSERID,
                EditUserName = entity.EDITUSERNAME,
                OwnerID = entity.OWNERID,
                OwnerName = entity.OWNERNAME,
                IsDeleted = entity.ISDELETED == 1 ? true : false,
                IsSetOrg = entity.ISSETORG
            };
        }
    }
}
