using System.Web;
using System.Web.Mvc;

namespace ProyectoWebApp_Plat2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
