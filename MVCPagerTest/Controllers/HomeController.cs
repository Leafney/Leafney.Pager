using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Leafney.Pager;
using PagerTestData;

namespace MVCPagerTest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index(int p=1)
        {

            //设置总数据条数
            int Datacount = 524;

            int total = 0;

            #region 通过url参数方式分页  
            /*
            //查询分页数据
            var _list = new MakeData().GetPageList(Datacount, page, 10, out total);
            //填充分页控件
            PageList<Article> pagelist = new PageList<Article>(_list, page, 10, total);

            return View(pagelist); 
             */
            #endregion

            #region 通过路由规则方式分页
            
            //查询分页数据 (分页参数设置为p)
            var _listR = new MakeData().GetPageList(Datacount,p,10,out total);
            //填充分页控件
            PageList<Article> pagelistR = new PageList<Article>(_listR,p,10,total);
            return View(pagelistR);
             
            #endregion

            #region 普通url方式分页
            /*
            //查询分页数据
            var _listUrl = new MakeData().GetPageList(Datacount, p, 10, out total);
            //填充分页控件
            PageList<Article> pagelistUrl = new PageList<Article>(_listUrl, p, 10, total);

            return View(pagelistUrl); 
            */
            #endregion


        }

    }
}
