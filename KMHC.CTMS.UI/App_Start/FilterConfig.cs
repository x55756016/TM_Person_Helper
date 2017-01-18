using System.Web;
using System.Web.Mvc;
using Project.UI.Attribute;

namespace Project.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionAttribute());
            //filters.Add(new AuthorizedAttribute());
        }
    }
}