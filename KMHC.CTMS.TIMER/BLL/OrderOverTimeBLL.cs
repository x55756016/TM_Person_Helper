using KMHC.CTMS.TIMER.DAL;
using Project.Common.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHC.CTMS.TIMER.BLL
{
    public class OrderOverTimeBLL
    {
        public static void DoWork()
        {
            try
            {
//                #region 检测过期的订单
//                using (var db = new CRDatabase())
//                {
//                    string sql = @"select c.REQUESTQUEUEID,c.MYPRODUCTID,c.USERID,c.DOCTORID,c.REGID,c.QUEUEINDEX,c.STARTDATE,c.REQUESSTATUE,c.REQUESTTYPE,c.ENDDATE,c.CREATEUSERID,c.CREATEUSERNAME,c.CREATEDATETIME,c.EDITUSERID,c.EDITUSERNAME,c.EDITDATETIME,c.OWNERID,c.OWNERNAME,c.ISDELETED from (
//select rownum as fn,a.* from CTMS_SUP_REQUESTRECORD  a
//where a.ISDELETED=0 and a.MYPRODUCTID is not null) c
//
//INNER JOIN (
//select MIN(ROWNUM) as rn from (
//select rownum,a.STARTDATE from CTMS_SUP_REQUESTRECORD  a
//where a.ISDELETED=0 and a.MYPRODUCTID is not null) b 
//group by trunc(b.STARTDATE)) d 
//
//on c.fn=d.rn
//
//order by c.STARTDATE";
//                    DbRawSqlQuery<TempRequestRecord> srrList = db.Database.SqlQuery<TempRequestRecord>(sql);

//                    //var srrList = db.Set<CTMS_SUP_REQUESTRECORD>().Where(p => p.REQUESSTATUE == "0" && !p.ISDELETED && !string.IsNullOrEmpty(p.MYPRODUCTID)).ToList();

//                    foreach (var item in srrList)
//                    {
//                        /*if (DateTime.Now > item.STARTDATE)
//                            if ((DateTime.Now - item.STARTDATE).TotalHours < 6)
//                                continue;*/
//                        if (item.REQUESSTATUE != "0")
//                            continue;
//                        if (DateTime.Now > item.STARTDATE)
//                            if ((DateTime.Now - item.STARTDATE).TotalHours < 6)
//                                continue;
//                        var myPro = db.Set<CTMS_MYPRODUCT>().FirstOrDefault(p => p.MYPRODUCTID == item.MYPRODUCTID);
//                        if (myPro != null)
//                        {
//                            #region 判断是否存在该待办了
//                            var tempUE = db.Set<CTMS_USEREVENT>().FirstOrDefault(p => p.MODELID == myPro.ORDERID && p.LINKURL == "OrderServiceTrace");
//                            if (tempUE != null)
//                            {
//                                continue;
//                            }
//                            #endregion
//                            #region 获取订单信息
//                            var order = db.Set<CTMS_OD_ORDERS>().FirstOrDefault(p => p.ORDERCODE == myPro.ORDERID);
//                            #endregion
//                            #region 发送待办
//                            if (order != null)
//                            {
//                                CTMS_USEREVENT ueModel = new CTMS_USEREVENT();
//                                ueModel.EVENTID = Guid.NewGuid().ToString();
//                                ueModel.USERAPPLYID = "";
//                                ueModel.ACTIONTYPE = "1";
//                                ueModel.ACTIONINFO = string.Format("{1}订单号{0}医生未及时回复，请跟踪！", order.ORDERCODE, order.PRODUCTNAME);
//                                ueModel.RECEIPTTIME = System.DateTime.Now;
//                                ueModel.ACTIONSTATUS = ((int)ActionStatus.Progress).ToString();
//                                ueModel.FROMUSER = "";
//                                var toUserId = db.Set<CTMS_SYS_USERINFO>().FirstOrDefault(p => p.USERTYPE == (decimal)UserType.客服);
//                                ueModel.TOUSER = toUserId.USERID;
//                                ueModel.CREATETIME = System.DateTime.Now;
//                                ueModel.LINKURL = "OrderServiceTrace"; //客服订单未回复待办
//                                ueModel.REMARKS = order.OWNERDOCID + "|" + order.USERID;
//                                ueModel.EDITDATETIME = item.STARTDATE;
//                                ueModel.MODELID = myPro.ORDERID;
//                                ueModel.CREATEUSERID = "";
//                                db.CTMS_USEREVENT.Add(ueModel);
//                            }
//                            #endregion
//                        }
//                    }
//                    db.SaveChanges();
//                }
//                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteError("查询过期未回复订单失败，时间是：" + DateTime.Now);
            }
        }
    }

    public class TempRequestRecord
    {
        public string MYPRODUCTID { get; set; }
        public DateTime STARTDATE { get; set; }
        public string REQUESSTATUE { get; set; }
    }
}
