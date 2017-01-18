/*
 * 描述:作为定时任务设置的接口
 *  
 * 修订历史: 
 * 日期               修改人              Email                  内容
 * 20160329   		  林德力         	takalin@qq.com   		 创建 
 *  
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KMHC.CTMS.TIMER.DAL;
using KMHC.CTMS.TIMER.Model;
using Project.DAL.Database;
using Project.Common.Helper;

namespace KMHC.CTMS.TIMER.BLL
{
    public class PushTimeTaskDefineBLL
    {
        #region 轮询任务表
        /// <summary>
        /// 执行反射的函数
        /// </summary>
        /// <param name="funtionName"></param>
        /// <param name="parameter"></param>
        private static bool InvokeReflection(string timeTaskID, string className, string parameter)
        {
            //Assembly dll = Assembly.LoadFile(System.Environment.CurrentDirectory + "\\KMHC.CTMS.BLL.dll");
            try
            {
                //string url = AppDomain.CurrentDomain.BaseDirectory + "\\BLL\\" + className.Split(',')[0];
                string url = AppDomain.CurrentDomain.BaseDirectory + "BLL\\" + className.Split(',')[0]; //-- 发布到正式环境
                Assembly dll = Assembly.LoadFrom(url);
                Type function = dll.GetType(className.Split(',')[1], true);
                //var obj = dll.CreateInstance(className.Split(',')[1]) as ITimeTask;
                //return obj.ExcuteTimeTask(parameter);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteInfo("反射执行出现了错误：" + ex.Message + "," + ex.InnerException + ";错误的数据是:" + timeTaskID);
                throw;
            }
        }

        /// <summary>
        /// 轮询定时任务表
        /// </summary>
        public static void PollingTimeTask()
        {
            using (var _context = new tmpmEntities2())
            {
                //DbContext _context = DbSessionFactory.GetCurrentDbContext();
                //1.取出需要执行的任务
                //DateTime minTime = Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " 00:00:00");
                DateTime maxTime = DateTime.Now;
                var list =
                    _context.Set<ctms_tk_timedtask>()
                        .Where(p => p.STARTDATE <= maxTime && p.TASKSTATUS == "0");

                //2.执行
                foreach (var item in list)
                {
                    try
                    {
                        //TaskAsyncHelper.RunAsync(
                        //    () => { InvokeReflection(item.PROCESSFUNCNAME, item.PROCESSFUNCPAMETER); }, null); //多线程反射的情况下可能会有冲突
                        if(InvokeReflection(item.TIMEDTASKID, item.PROCESSFUNCNAME, item.PROCESSFUNCPAMETER))
                        {
                            //_context.Set<CTMS_TK_TIMEDTASK>().Remove(item);
                            //CTMS_TK_TIMEDTASKHIS itemhis = new CTMS_TK_TIMEDTASKHIS();
                            //itemhis.CREATEDATE = item.CREATEDATE;
                            //itemhis.CREATETIME=itemhis.CREATETIME;
                            //    itemhis.ENDDATE=itemhis.ENDDATE;
                            //    itemhis.ENDTIME=itemhis.ENDTIME;
                            //    itemhis.EXECUTTIME = DateTime.Now;
                            //    itemhis.FUNCTIONMARK=itemhis.FUNCTIONMARK;
                            //    itemhis.MESSAGEBODY=itemhis.MESSAGEBODY;
                            //    itemhis.MODELCODE=itemhis.MODELCODE;
                            //    itemhis.MSGLINKURL=itemhis.MSGLINKURL;
                            //    itemhis.PROCESSFUNCNAME=itemhis.PROCESSFUNCNAME;
                            //    itemhis.PROCESSFUNCPAMETER=itemhis.PROCESSFUNCPAMETER;
                            //    itemhis.PROCESSWCFURL=itemhis.PROCESSWCFURL;
                            //    itemhis.RECEIVEROLE=itemhis.RECEIVEROLE;
                            //    itemhis.RECEIVEUSER= itemhis.RECEIVEUSER;
                            //    itemhis.STARTDATE= itemhis.STARTDATE;
                            //    itemhis.STARTTIME=itemhis.STARTTIME;
                            //    itemhis.TASKSTATUS=itemhis.TASKSTATUS;
                            //    itemhis.TIMEDTASKDEFINEID=itemhis.TIMEDTASKDEFINEID;
                            //    itemhis.TIMEDTASKID=itemhis.TIMEDTASKID;
                            //    itemhis.TRIGGERTYPE=itemhis.TRIGGERTYPE;
                            //    itemhis.UPDATEDATE=itemhis.UPDATEDATE;
                            //    itemhis.UPDATETIME = DateTime.Now;
                        }
                        //执行
                        item.TASKSTATUS = "1";
                        item.EXECUTTIME = DateTime.Now;
                        item.UPDATEDATE = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        item.TASKSTATUS = "0";
                        LogHelper.WriteError("执行定时任务出错:" + ex.Message + "," + ex.InnerException);
                    }
                }
                _context.SaveChanges();
            }
        }
        #endregion


        #region 轮询设置表

        /// <summary>
        /// 轮询定时任务设置表
        /// </summary>
        public static void RollTimeDefine()
        {
            var _context = DbSessionFactory.GetCurrentDbContext();
            var nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var taskList = _context.Set<ctms_tk_timedtaskdefine>().Where(p => p.ENDDATE >= nowDate).ToList();  //结束时间
            //var taskList = _context.Set<CTMS_TK_TIMEDTASKDEFINE>().Where(p => p.TIMEDTASKDEFINEID == "888").ToList();  //结束时间
            //循环计算时间
            foreach (var taskItem in taskList)
            {
                if (IsSend(taskItem))
                {
                    AddTimeTask(taskItem);
                }
            }
            _context.SaveChanges();
        }


        private static void AddTimeTask(ctms_tk_timedtaskdefine entity)
        {
            TimedTaskDto timeModel = new TimedTaskDto();
            timeModel.TIMEDTASKID = Guid.NewGuid().ToString();
            timeModel.TIMEDTASKDEFINEID = entity.TIMEDTASKDEFINEID;
            timeModel.TRIGGERTYPE = entity.TRIGGERTYPE;
            timeModel.RECEIVEUSER = entity.RECEIVEUSER;
            timeModel.RECEIVEROLE = entity.RECEIVEROLE;
            timeModel.MESSAGEBODY = entity.MESSAGEBODY;
            timeModel.MODELCODE = entity.MODELCODE;
            timeModel.MSGLINKURL = entity.MSGLINKURL;
            timeModel.PROCESSWCFURL = entity.PROCESSWCFURL;
            timeModel.PROCESSFUNCNAME = entity.PROCESSFUNCNAME;
            timeModel.PROCESSFUNCPAMETER = entity.PROCESSFUNCPAMETER;
            timeModel.FUNCTIONMARK = entity.FUNCTIONMARK;
            timeModel.TASKSTATUS = "0";

            var tempTime = String.IsNullOrEmpty(entity.STARTTIME) ? DateTime.Now.ToShortTimeString() : entity.STARTTIME;
            timeModel.STARTDATE = Convert.ToDateTime(DateTime.Now.ToLongDateString() + " " + tempTime);

            timeModel.ENDDATE = DateTime.Now;
            timeModel.EXECUTTIME = null;
            timeModel.CREATEDATE = DateTime.Now;
            timeModel.UPDATEDATE = DateTime.Now;

            Mapper.CreateMap<TimedTaskDto, ctms_tk_timedtask>();
            DbSessionFactory.GetCurrentDbContext().Set<ctms_tk_timedtask>().Add(Mapper.Map<ctms_tk_timedtask>(timeModel));
        }
        #endregion

        /// <summary>
        /// 判断是否满足时间条件发送任务
        /// </summary>
        /// <returns></returns>
        private static  bool IsSend(ctms_tk_timedtaskdefine model)
        {
            bool flag = false;

            var nowDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan ts = Convert.ToDateTime(model.ENDDATE) - Convert.ToDateTime(model.STARTDATE);

            int yearDiff = Convert.ToDateTime(model.ENDDATE).Year - Convert.ToDateTime(model.STARTDATE).Year;
            int monthDiff = Convert.ToDateTime(model.ENDDATE).Month - Convert.ToDateTime(model.STARTDATE).Month;
            int monthSumDiff = yearDiff*12 + monthDiff;//相差总月份

            DateTime doTime=DateTime.Now;
            switch ((int)model.TIMEDTYPE)
            {
                #region  按天
                case 1:
                    if (model.DAYORWORKDAY == 1) //判断工作日
                    {
                        var week = DateTime.Now.DayOfWeek.ToString().ToLower();
                        flag = (week != "saturday" && week != "sunday");
                    }
                    else //按照每隔几天
                    {
                        //相隔时间不能超过开始时间跟结束时间差
                        for (int i = (int)model.HOWLONGDAY; i <= ts.Days; i += (int)model.HOWLONGDAY)
                        {
                            doTime = Convert.ToDateTime(model.STARTDATE.Value.AddDays(i).ToShortDateString());

                            if (nowDate == doTime)
                            //当天时间 == 隔了几天
                            {
                                flag = true;
                                break;
                            }
                            if (nowDate < doTime)
                            //判断当前日期小于了所隔天数
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    break; //按天
                #endregion
                #region 按周
                case 2:
                    for (int i = (int) model.HOWLONGWEEKS; i < (ts.Days)/7; i += (int) model.HOWLONGWEEKS)
                    {
                        DateTime[] dtWeek = DatesIncludeDay(model.STARTDATE.Value.AddDays(7*i));
                        if (nowDate >= dtWeek[0] && nowDate <= dtWeek[1])
                        {
                            if ((int)nowDate.DayOfWeek==model.DAYOFWEEK) //星期几 相同才执行
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (nowDate < dtWeek[0]) //区间超过了周范围
                        {
                            break;
                        }
                    }
                    break; //按周
                #endregion
                #region 按月
                case 3:
                    for (int i = (int) model.HOWLONGMONTH; i < monthSumDiff; i += (int) model.HOWLONGMONTH)
                    {
                        if (model.HOWLONGMONTHWEEKDAY ==0) // 每多少月多少日   模式
                        {
                            var maxMonthDay = LastDayOfMonth(Convert.ToDateTime(model.STARTDATE.Value.AddMonths(i).Year + "-" + model.STARTDATE.Value.AddMonths(i).Month + "-01")).Day;
                            if (maxMonthDay >= model.HOWLONGMONTHDAY) //判断是否超出每月的最大一天
                            {
                                doTime = Convert.ToDateTime(model.STARTDATE.Value.AddMonths(i).Year + "-" + model.STARTDATE.Value.AddMonths(i).Month + "-" + ((model.HOWLONGMONTHDAY == 0) ? maxMonthDay : model.HOWLONGMONTHDAY));
                            }
                        }
                        else
                        {
                            doTime = Convert.ToDateTime(model.STARTDATE.Value.AddMonths(i).Year + "-" + model.STARTDATE.Value.AddMonths(i).Month + "-" +
                                                   DayOfMonthWeek(Convert.ToDateTime(model.STARTDATE.Value.AddMonths(i).Year + "-" + model.STARTDATE.Value.AddMonths(i).Month + "-01"), (int)model.HOWLONGMONTHWEEK, (int)model.HOWLONGMONTHWEEKDAY).Day);
                        }
                        if (nowDate == doTime)
                        {
                            flag = true;
                            break;
                        }
                        else if (nowDate <= doTime)
                        {
                            break;
                        }
                    }
                    break; //按月
                #endregion
                #region 按年
                case 4:
                    if (model.HOWLONGYEARWEEKDAY == 0) //每年的几月几日模式
                    {
                        doTime = Convert.ToDateTime(nowDate.Year + "-" + model.HOWLONGYEARMONTH + "-" +
                                                    (model.HOWLONGYEARDAY == 0
                                                        ? LastDayOfMonth(
                                                            Convert.ToDateTime(nowDate.Year + "-" +
                                                                               model.HOWLONGYEARMONTH + "-1")).Day
                                                        : model.HOWLONGYEARDAY));
                    }
                    else
                    {
                        var tempDay = DayOfMonthWeek(Convert.ToDateTime(nowDate.Year +"-" + model.HOWLONGYEARMONTH +"-1"), (int) model.HOWLONGYEARWEEK,(int) model.HOWLONGYEARWEEKDAY).Day;
                        doTime =Convert.ToDateTime(nowDate.Year + "-" + model.HOWLONGYEARMONTH+"-"+tempDay);
                    }
                    flag = nowDate == doTime;
                    break;
                #endregion
                default:
                    flag = false;
                    break;
            }
            return flag;
        }

        #region 通用日期转换方法
        /// <summary>
        /// 传入某一天，输出这一周的起始日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static  DateTime[] DatesIncludeDay(DateTime dt)
        {
            DateTime[] dts = new DateTime[2];
            int dayofweek = DayOfWeek(dt);
            dts[0] = dt.AddDays(1 - dayofweek);
            dts[1] = dt.AddDays(7 - dayofweek);
            return dts;
        }

        /// <summary>
        /// 计算星期几，转换为数字
        /// </summary>
        /// <param name="dt">某天的日期</param>
        /// <returns></returns>
        private static  int DayOfWeek(DateTime dt)
        {
            string strDayOfWeek = dt.DayOfWeek.ToString().ToLower();
            int intDayOfWeek = 0;
            switch (strDayOfWeek)
            {
                case "monday":
                    intDayOfWeek = 1;
                    break;
                case "tuesday":
                    intDayOfWeek = 2;
                    break;
                case "wednesday":
                    intDayOfWeek = 3;
                    break;
                case "thursday":
                    intDayOfWeek = 4;
                    break;
                case "friday":
                    intDayOfWeek = 5;
                    break;
                case "saturday":
                    intDayOfWeek = 6;
                    break;
                case "sunday":
                    intDayOfWeek = 7;
                    break;
            }
            return intDayOfWeek;
        }

        /// <summary>
        /// 取得某月的第一天
        /// </summary>
        /// <param name="datetime">要取得月份第一天的时间</param>
        /// <returns></returns>
        private   DateTime FirstDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day);
        }

        /// <summary>
        /// 取得某月的最后一天
        /// </summary>
        /// <param name="datetime">要取得月份最后一天的时间</param>
        /// <returns></returns>
        private static  DateTime LastDayOfMonth(DateTime datetime)
        {
            return datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1);
        }
        
        /// <summary>
        /// 取得这个月一共有多少周
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private  int CountWeekOfMonth(DateTime day)
        {
            //WeekStart
            //周一至周日 为一周
            DateTime firstofMonth = Convert.ToDateTime(day.Date.Year + "-" + day.Date.Month + "-" + 1);
            int i = (int)firstofMonth.Date.DayOfWeek == 0 ? 7 : (int)firstofMonth.Date.DayOfWeek;
            int totalDay = LastDayOfMonth(firstofMonth).Day - (8 - i);
            int countWeek = 1;
            if (totalDay%7 == 0)
            {
                countWeek +=  totalDay/7;
            }
            else
            {
                countWeek +=  totalDay/7 + 1;
            }
            return countWeek;
        }

        private  int WeekOfMonth(DateTime day, int WeekStart)
        {
            //WeekStart
            //1表示 周一至周日 为一周
            //2表示 周日至周六 为一周
            DateTime firstofMonth= Convert.ToDateTime(day.Date.Year + "-" + day.Date.Month + "-" + 1);
            int i = (int)firstofMonth.Date.DayOfWeek;
            if (i == 0)
            {
                i = 7;
            }

            if (WeekStart == 1)
            {
                return (day.Date.Day + i - 2) / 7 + 1;
            }
            if (WeekStart == 2)
            {
                return (day.Date.Day + i - 1) / 7;

            }
            return 0;
            //错误返回值0
        }

        /// <summary>
        ///  取出这个年月下的第几个周几
        /// </summary>
        /// <param name="dtTime"></param>
        /// <param name="j">第几个（1-4，5为最后一个）</param>
        /// <param name="weekNum">星期一到七</param>
        /// <returns></returns>
        private static  DateTime DayOfMonthWeek(DateTime dtTime,int j ,int weekNum)
        {
            DateTime firstofMonth = Convert.ToDateTime(dtTime.Date.Year + "-" + dtTime.Date.Month + "-" + 1);
            int dayOfWeek = (int)firstofMonth.Date.DayOfWeek;  //这个月第一天是周几
            dayOfWeek = dayOfWeek == 0 ? 7 : dayOfWeek;
            int finalDay = 0;

            if (j == 0) //最后一个
            {
                DateTime lastMonthDay = LastDayOfMonth(dtTime);
                int dayOfLastWeek = (int) lastMonthDay.Date.DayOfWeek == 0 ? 7 : (int) lastMonthDay.Date.DayOfWeek;

                if (dayOfLastWeek - weekNum > 0)
                {
                    finalDay = lastMonthDay.AddDays(weekNum - dayOfLastWeek).Day;
                }
                else if (dayOfLastWeek - weekNum < 0)
                {
                    finalDay = lastMonthDay.AddDays(-7).AddDays(weekNum - dayOfLastWeek).Day;
                }
                else
                {
                    finalDay = lastMonthDay.Day;
                }
            }else if (j <= 4)
            {
                //此处可能有bug。。。。    ----林德力
                if (dayOfWeek - weekNum > 0)
                {
                    finalDay = j*7 - (dayOfWeek - weekNum) + 1;
                }
                else
                {
                    finalDay = (j - 1)*7 + weekNum - dayOfWeek + 1;
                }
                //finalDay = (j - 1) * 7 - (dayOfWeek - weekNum - 1);
            }
            return Convert.ToDateTime(dtTime.Date.Year + "-" + dtTime.Date.Month + "-" + finalDay);
        }

        #endregion
    }
}