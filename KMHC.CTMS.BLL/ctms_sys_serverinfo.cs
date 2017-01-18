using Project.Common;
using Project.Common.Helper;
using Project.DAL;
using Project.DAL.Database;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL
{
    public class ctms_sys_serverinfoBLL
    {
        private readonly ctms_sys_serveralarmBLL saBll = new ctms_sys_serveralarmBLL();

        public string Add(V_ctms_sys_serverinfo model)
        {
            if (model == null)
                return string.Empty;

            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                ctms_sys_serverinfo entity = ModelToEntity(model);
                entity.ServerInfoId = string.IsNullOrEmpty(model.ServerInfoId) ? Guid.NewGuid().ToString("N") : model.ServerInfoId;
                return dal.Add(entity);
            }
        }

        public V_ctms_sys_serverinfo Get(Expression<Func<ctms_sys_serverinfo, bool>> predicate = null)
        {
            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                ctms_sys_serverinfo entity = dal.Get(predicate);
                return EntityToModel(entity);
            }
        }

        public IEnumerable<V_ctms_sys_serverinfo> GetAll()
        {
            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                return dal.GetAll().Select(EntityToModel).ToList();
            }
        }

        public IEnumerable<V_ctms_sys_serverinfo> GetAll(Expression<Func<ctms_sys_serverinfo, bool>> predicate = null)
        {
            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                return dal.GetAll(predicate).Select(EntityToModel).ToList();
            }
        }

        public IEnumerable<V_ctms_sys_serverinfo> GetList(PageInfo page, string ip)
        {
            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                if (string.IsNullOrEmpty(ip))
                    return dal.GetAll(p => p.IPAddress == ip).Paging(ref page).Select(EntityToModel).ToList();
                else
                    return dal.GetAll().Paging(ref page).Select(EntityToModel).ToList();
            }
        }

        public bool Edit(V_ctms_sys_serverinfo model)
        {
            if (model == null)
                return false;

            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                return dal.Edit(ModelToEntity(model));
            }
        }

        public bool Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            using (ctms_sys_serverinfoDAL dal = new ctms_sys_serverinfoDAL())
            {
                return dal.Delete(id);
            }
        }

        public ctms_sys_serverinfo ModelToEntity(V_ctms_sys_serverinfo model)
        {
            if (model != null)
            {
                ctms_sys_serverinfo entity = new ctms_sys_serverinfo()
                {
                    ServerInfoId = model.ServerInfoId,
                    IPAddress = model.IPAddress,
                    CPUValue = model.CPUValue,
                    MemoryValue = model.MemoryValue,
                    DiskValue = model.DiskValue,
                    InputTime = model.InputTime,
                    SystemId = model.SystemId
                };
                return entity;
            }
            return null;
        }

        public V_ctms_sys_serverinfo EntityToModel(ctms_sys_serverinfo entity)
        {
            if (entity != null)
            {
                V_ctms_sys_serverinfo model = new V_ctms_sys_serverinfo()
                {
                    ServerInfoId = entity.ServerInfoId,
                    IPAddress = entity.IPAddress,
                    CPUValue = entity.CPUValue,
                    MemoryValue = entity.MemoryValue,
                    DiskValue = entity.DiskValue,
                    InputTime = entity.InputTime,
                    SystemId = entity.SystemId
                };
                return model;
            }
            return null;
        }

        /// <summary>
        /// 检测服务器信息，是否发Email告警
        /// </summary>
        /// <param name="sm"></param>
        /// <param name="valType"></param>
        public void CheckServerInfoValue(V_ctms_sys_servermonitor sm, string valType)
        {
            if (string.IsNullOrEmpty(valType))
                return;

            int type = -1;
            if (valType == "CPU")
            {
                type = (int)ServerAlarmType.CPU告警;
            }
            else if (valType == "内存")
            {
                type = (int)ServerAlarmType.内存告警;
            }
            else if (valType == "硬盘")
            {
                type = (int)ServerAlarmType.硬盘告警;
            }
            var saOld = saBll.GetAll(p => p.IPAddress == sm.IPAddress && p.Type == type).OrderByDescending(p => p.InputTime).FirstOrDefault();
            if (saOld == null)
            {
                SendAlarmToUser(0, sm, valType,type);
            }
            else
            {
                TimeSpan ts = DateTime.Now - saOld.InputTime.Value;
                if (ts.TotalMinutes >= 30)
                {
                    SendAlarmToUser(0, sm, valType,type);
                }
            }
        }

        /// <summary>
        /// 发送告警信息，type为0表示通过Email发送
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sm"></param>
        /// <param name="valType"></param>
        public void SendAlarmToUser(int type,V_ctms_sys_servermonitor sm,string valType,int saType)
        {
            if (type == 0)
            {
                //发送Email
                if (!string.IsNullOrEmpty(sm.Email))
                {
                    float val = 0;
                    if (valType == "CPU")
                        val = sm.CPUMaxValue.Value;
                    else if (valType == "内存")
                        val = sm.MemoryMaxValue.Value;
                    else if (valType == "硬盘")
                        val = sm.DiskMaxValue.Value;
                    StringExtension.SendMail(sm.Email, "告警提示", string.Format("服务器ip地址{0}{2}使用率超过{1}%，请处理。", sm.IPAddress, val, valType));
                    V_ctms_sys_serveralarm sa = new V_ctms_sys_serveralarm()
                    {
                        InputTime = DateTime.Now,
                        IPAddress = sm.IPAddress,
                        Message = string.Format("{1}使用率超过{0}%", val, valType),
                        Status = 1,
                        Type = saType
                    };
                    saBll.Add(sa);
                }
            }
        }
    }
}
