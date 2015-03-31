using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCPagerTest
{
    public class RouteConfig
    {


        //自定义分页插件路由规则
        //在Global文件中注册自定义分页路由规则
        public static void RegisterRoutesFenYe(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.AppendTrailingSlash = true;//是否在生成的Url末尾添加/ 斜线(如果没有存在的话)
            //routes.LowercaseUrls = true;//实现生成的Url小写
            routes.MapRoute(
               name: "RouteFY",
               url: "{controller}/{action}/{p}/{id}",
               defaults: new { controller = "Home", action = "Index", p = 1, id = UrlParameter.Optional },
               constraints: new { p = @"[\d]*" }//p只能是数字  分页参数page可以自定义，这里设置为p，注意在页面中要指定
           );
        }


        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}