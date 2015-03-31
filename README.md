#Leafney.Pager ASP.NET MVC分页插件

1. 支持URL参数分页和MVC路由参数分页
2. 支持自定义分页参数，默认为page
***
## Test
### 通过URL参数分页

	int total = 0;
	//查询分页数据
	var _list = new MakeData().GetPageList(Datacount, page, 10, out total);
	//填充分页控件
	PageList<Article> pagelist = new PageList<Article>(_list, page, 10, total);
	return View(pagelist); 
	
***
### 通过路由规则方式分页

####调用：
	int total = 0;
	//查询分页数据 (分页参数设置为p)
	var _listR = new MakeData().GetPageList(Datacount,p,10,out total);
	//填充分页控件
	PageList<Article> pagelistR = new PageList<Article>(_listR,p,10,total);
	return View(pagelistR);

####自定义路由规则：
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

####在Global.asax 文件中注册自定义路由规则
    //在默认路由规则之前注册自定义分页插件的路由规则
    RouteConfig.RegisterRoutesFenYe(RouteTable.Routes);
	
***

### 需要添加对System.Web.Mvc.dll的引用
