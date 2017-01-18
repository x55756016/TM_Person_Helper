using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class BaseMonitor
    {
        public NameValueCollection GetParameters()
        {
            NameValueCollection list = new NameValueCollection();
            foreach (PropertyInfo pi in this.GetType().GetProperties())
            {
                object obj = pi.GetValue(this);
                list.Add("Data[" + pi.Name + "]", obj == null ? string.Empty : obj.ToString());
            }
            return list;
        }
    }
}
