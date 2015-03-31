using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Leafney.Pager
{
    /// <summary>
    /// 高级路由分页生成
    /// </summary>
    internal partial class RoutePageBuilder
    {

        #region 高级路由分页插件
        /// <summary>
        /// 高级路由分页插件
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="routeName">分页路由规则的名称</param>
        /// <param name="pageBreak">分页符(默认为page) 对应于路由规则中的分页参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="viewPage">可见页码数</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        internal MvcHtmlString GetRoutePager(HtmlHelper helper, string routeName, string pageBreak, int currentPageIndex, int pageSize, int totalCount, int viewPage = 10, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {

            #region 验证输入项
            //如果每页大小pagesize小于0 重置为1
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            //如果当前页pageindex小于1，则重置为1
            if (currentPageIndex < 1)
            {
                currentPageIndex = 1;
            }

            //总页数
            int pageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);

            /*
               如果当前没有分页数据（初始化或查询出0条数据），则总数为0，那么下面的if()判断会执行，则会把currentPageIndex重置为0，那么在下面设置首页链接的时候就会将首页链接设置为 “\” 而不是void(0)；所以，下面在首页设置页码时将条件判断 currentPageIndex == 1修改为currentPageIndex <= 1 
           */

            //如果当前页pageindex大于总页数，则重置为总页数
            if (currentPageIndex > pageCount)
            {
                currentPageIndex = pageCount;
            }
            #endregion

            #region 控制当总数据条数只够显示一页时，是否仍显示分页插件 2015-2-13 16:36:26添加

            //当没有数据显示时，默认不显示分页插件
            if (pageCount <= 0)
            {
                return MvcHtmlString.Empty;
            }

            if (!showOnlyOneDisplayed && pageCount == 1)
            {
                return MvcHtmlString.Empty;
            }
            #endregion


            #region 指定分页符 默认为page
            if (string.IsNullOrEmpty(pageBreak))
            {
                pageBreak = "page";
            }
            #endregion

            //拼接生成的字符串
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("<div class=\"x-pager\">");
            //string u_br = "\r\n";//换行(用于输出到页面中看着整齐) 

            #region 处理路由参数及QueryString参数

            //获取所有匹配路由规则的路由参数
            RouteValueDictionary rvdRoute = helper.ViewContext.RouteData.Values;
            //保存QueryString中的url参数
            RouteValueDictionary rvdQS = new RouteValueDictionary();
            //获取QueryString中的url参数
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                rvdQS.Add(keys[i], collection[keys[i]]);
            }
            /*
                参数包括：路由规则中的匹配参数；QueryString中的参数；自定义的page参数
                如果向RouteValueDictionary中添加已存在的参数则会保存，所以对三个参数集合进行处理合并，合并时对于已存在的值用后面集合中的值替换前面集合中的相应值
             */

            #endregion

            // surl = 192.168.1.221/home/?page={0}&sid=1&age=23 

            #region 是否显示首页 末页
            //是否显示首页 末页
            if (showFirstLastBtn)
            {
                //<span class="x-pitem"><a href="/?page=1">首页</a></span>
                //更新v2.0 <span class="x-pitem x-fied"><a href="/?page=1">首页</a></span>
                //更新v2.1 <span class="x-pitem x-fied"><a href="javascript:void(0);">首页</a></span>
                string u_first = "<span class=\"x-pitem x-fied\">{0}</span>";
                if (currentPageIndex <= 1)
                {
                    sbuilder.AppendFormat(u_first, LinkExtensions.RouteLink(helper, "首页", routeName, new { }, new { href = "javascript:void(0);" }));
                }
                else
                {
                    //v2.1 <span class="x-pitem x-fied"><a href="/1">首页</a></span>
                    //处理参数集合
                    var r_page = new RouteValueDictionary();
                    r_page.Add(pageBreak, 1);
                    var r_temp = RoutePageHelper.CombineRouteValue(rvdRoute, rvdQS, r_page);
                    sbuilder.AppendFormat(u_first, LinkExtensions.RouteLink(helper, "首页", routeName, r_temp));
                }
            }
            #endregion

            #region 是否显示上一页 下一页
            //是否显示上一页 下一页
            if (showPrevNextBtn)
            {
                // <span class="x-pitem"><a href="javascript:void(0);"><i class="xprev"></i>上一页</a></span>
                //利用两次字符串格式化插入的方式
                string u_spanPN = "<span class=\"x-pitem\">{0}</span>";
                string u_prev = "<i class=\"xprev\"></i>上一页";
                if (currentPageIndex > 1)
                {
                    //处理参数集合
                    var r_page = new RouteValueDictionary();
                    r_page.Add(pageBreak, currentPageIndex - 1);
                    var r_temp = RoutePageHelper.CombineRouteValue(rvdRoute, rvdQS, r_page);
                    string u_temp = string.Format(u_spanPN, LinkExtensions.RouteLink(helper, "{0}", routeName, r_temp));
                    sbuilder.AppendFormat(u_temp, u_prev);
                }
                else
                {
                    string u_temp = string.Format(u_spanPN, LinkExtensions.RouteLink(helper, "{0}", routeName, new { }, new { href = "javascript:void(0);" }));
                    sbuilder.AppendFormat(u_temp, u_prev);
                }
            }
            #endregion

            #region 生成显示页码
            //页码
            sbuilder.Append(RoutePageHelper.CreateRoutePageNumbers(helper, routeName, pageBreak, currentPageIndex, viewPage, pageCount, rvdRoute, rvdQS));
            #endregion

            #region 是否显示下一页 上一页
            //是否显示上一页 下一页
            if (showPrevNextBtn)
            {
                //<span class="x-pitem"><a href="/2">下一页<i class="xnext"></i></a></span>
                //利用两次字符串格式化插入的方式
                string u_spanPN = "<span class=\"x-pitem\">{0}</span>";
                string u_next = "下一页<i class=\"xnext\"></i>";
                if (currentPageIndex < pageCount)
                {
                    //处理参数集合
                    var r_page = new RouteValueDictionary();
                    r_page.Add(pageBreak, currentPageIndex + 1);
                    var r_temp =RoutePageHelper.CombineRouteValue(rvdRoute, rvdQS, r_page);
                    string u_temp = string.Format(u_spanPN, LinkExtensions.RouteLink(helper, "{0}", routeName, r_temp));
                    sbuilder.AppendFormat(u_temp, u_next);
                }
                else
                {
                    string u_temp = string.Format(u_spanPN, LinkExtensions.RouteLink(helper, "{0}", routeName, new { }, new { href = "javascript:void(0);" }));
                    sbuilder.AppendFormat(u_temp, u_next);
                }
            }
            #endregion

            #region 是否显示末页 首页
            //是否显示首页 末页
            if (showFirstLastBtn)
            {
                //<span class="x-pitem x-fied"><a href="/38">末页</a></span>
                string u_last = "<span class=\"x-pitem x-fied\">{0}</span>";
                if (currentPageIndex == pageCount)
                {
                    sbuilder.AppendFormat(u_last, LinkExtensions.RouteLink(helper, "末页", routeName, new { }, new { href = "javascript:void(0);" }));
                }
                else
                {
                    //处理参数集合
                    var r_page = new RouteValueDictionary();
                    r_page.Add(pageBreak, pageCount);
                    var r_temp =RoutePageHelper.CombineRouteValue(rvdRoute, rvdQS, r_page);
                    sbuilder.AppendFormat(u_last, LinkExtensions.RouteLink(helper, "末页", routeName, r_temp));
                }
            }
            #endregion

            #region 是否显示总页数  共n页
            if (showPageCount)
            {
                //<span class="x-pitem">&nbsp;共<i class="xpcount">80</i>页</span>
                string u_pcount = "<span class=\"x-pitem x-pcon\"> 共<i class=\"xpcount\">{0}</i>页</span>";
                sbuilder.AppendFormat(u_pcount, pageCount);
            }
            #endregion

            sbuilder.Append("</div>");
            return MvcHtmlString.Create(sbuilder.ToString());
        }
        #endregion

    }
}
