using System.Web;
using System.Web.Mvc;

namespace JinChuanQiXiang.TDT_net4._8
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
