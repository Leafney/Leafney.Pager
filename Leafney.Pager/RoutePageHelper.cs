using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Leafney.Pager
{
    /// <summary>
    /// 高级路由分页帮助类
    /// </summary>
    internal static class RoutePageHelper
    {


        #region 生成数字页码
        /// <summary>
        /// 生成数字页码
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="routeName">高级分页路由规则名称</param>
        /// <param name="pageBreak">分页符(默认为page)对应于路由规则中的分页参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="viewPage">可见页码总数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="rvdRoute">路由规则参数</param>
        /// <param name="rvdQS">QueryString参数</param>
        /// <returns></returns>
        public static string CreateRoutePageNumbers(HtmlHelper helper, string routeName, string pageBreak, int currentPageIndex, int viewPage, int pageCount, RouteValueDictionary rvdRoute, RouteValueDictionary rvdQS)
        {
            StringBuilder builder = new StringBuilder();
            //第一页
            int firstIndex = currentPageIndex <= (viewPage / 2 + 1) ? 1 : (currentPageIndex - viewPage / 2);
            //可见页末页
            int lastIndex = (firstIndex + viewPage - 1) >= pageCount ? pageCount : (firstIndex + viewPage - 1);
            //如果计算出的末页页码大于总的页码，则重置第一页的页码
            if (lastIndex >= pageCount)
            {
                firstIndex = lastIndex - viewPage + 1;
            }
            //如果重新计算出的第一页页码小于1，则重置首页页码为1
            // 2015-2-2 11:28:33 修改这个bug
            if (firstIndex < 1)
            {
                firstIndex = 1;
            }

            //当前数字页码
            string u_curr = "<span class=\"x-curr\">{0}<i class=\"xmid\"> / {1}</i></span>";
            //其他数字页码
            string u_item = "<span class=\"x-pitem x-num\">{0}</span>";

            for (int i = firstIndex; i < lastIndex + 1; i++)
            {
                if (i == currentPageIndex)
                {
                    //当前页码
                    //v1.0  <span class="xcurr">5</span>
                    //更新v2.0：<span class="xcurr">10<i class="xmid"> / 19</i></span>
                    builder.AppendFormat(u_curr, i, pageCount);
                }
                else
                {
                    //<span class="x-pitem"><a href="/?page=2">2</a></span>
                    //更新v2.0 <span class="x-pitem x-num"><a href="/?page=4">4</a></span>
                    // v2.1 


                    //处理参数集合
                    var r_page = new RouteValueDictionary();
                    r_page.Add(pageBreak, i);
                    var r_temp = RoutePageHelper.CombineRouteValue(rvdRoute, rvdQS, r_page);
                    builder.AppendFormat(u_item, LinkExtensions.RouteLink(helper, i.ToString(), routeName, r_temp));
                }
            }
            return builder.ToString();
        }
        #endregion


        #region 合并三个RouteValueDictionary集合，如果key已存在，则用后者替换前者中的相应值
        /// <summary>
        /// 合并三个RouteValueDictionary集合，如果key已存在，则用后者替换前者中的相应值
        /// </summary>
        /// <param name="rValue1">路由键值对集合</param>
        /// <param name="rValue2">路由键值对集合</param>
        /// <param name="rValue3">路由键值对集合</param>
        /// <returns></returns>
        public static RouteValueDictionary CombineRouteValue(RouteValueDictionary rValue1, RouteValueDictionary rValue2, RouteValueDictionary rValue3)
        {
            RouteValueDictionary tempRVD = new RouteValueDictionary();
            foreach (var rvd1 in rValue1)
            {
                tempRVD.Add(rvd1.Key, rvd1.Value);
            }

            foreach (var rvd2 in rValue2)
            {
                if (tempRVD.ContainsKey(rvd2.Key))
                {
                    //如果v1中已包含v2中的key,那么则用后者替换掉前者
                    tempRVD.Remove(rvd2.Key);
                }
                tempRVD.Add(rvd2.Key, rvd2.Value);
            }

            foreach (var rvd3 in rValue3)
            {
                if (tempRVD.ContainsKey(rvd3.Key))
                {
                    //如果前两者中已包含v3中的key,那么则用后者替换掉前者
                    tempRVD.Remove(rvd3.Key);
                }
                tempRVD.Add(rvd3.Key, rvd3.Value);
            }

            return tempRVD;
        }
        #endregion


    }
}
