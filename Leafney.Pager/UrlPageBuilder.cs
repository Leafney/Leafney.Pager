using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Leafney.Pager
{
    /// <summary>
    /// 通过url参数方式生成分页的链接和页码
    /// </summary>
    internal partial class UrlPageBuilder
    {

        #region 生成分页数据html代码
        /// <summary>
        /// 生成分页数据html代码
        /// </summary>
        /// <param name="pageBreak">分页符(默认为page)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="viewPage">可见页码总数(默认10个)</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页</param>
        /// <param name="showFirstLastBtn">是否显示首页末页</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        internal MvcHtmlString GetUrlPager(string pageBreak, int currentPageIndex, int pageSize, int totalCount, int viewPage = 10, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {
            string url_Content = GetPager(pageBreak, currentPageIndex, pageSize, totalCount, viewPage, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, showPageCount);
            return MvcHtmlString.Create(url_Content);
        } 
        #endregion


        #region 生成分页数据html代码
        /// <summary>
        /// 生成分页数据html代码
        /// </summary>
        /// <param name="pageBreak">分页符(默认为page)</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="viewPage">可见页码总数(默认10个)</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页</param>
        /// <param name="showFirstLastBtn">是否显示首页末页</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        internal string GetPager(string pageBreak, int currentPageIndex, int pageSize, int totalCount, int viewPage = 10, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {

            //如果每页大小pagesize小于0 重置为1
            if (pageSize <= 0)
            {
                pageSize = 1;
            }
            //如果当前页pageindex小于1，则重置为1
            if (currentPageIndex <= 1)
            {
                currentPageIndex = 1;
            }

            //总页数
            int pageCount = (int)Math.Ceiling((decimal)totalCount / pageSize);

            //如果当前页pageindex大于总页数，则重置为总页数
            if (currentPageIndex > pageCount)
            {
                currentPageIndex = pageCount;
            }

            #region 控制当总数据条数只够显示一页时，是否仍显示分页插件 2015-2-13 16:36:26添加

            //当没有数据显示时，默认不显示分页插件
            if (pageCount <= 0)
            {
                return string.Empty;
            }

            if (!showOnlyOneDisplayed && pageCount == 1)
            {
                return string.Empty;
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
            //url
            StringBuilder surl = new StringBuilder();
            string basePath = HttpContext.Current.Request.Url.AbsolutePath;

            //surl.Append(basePath + "?page={0}");
            //增加自定义分页符
            surl.AppendFormat("{0}?{1}", basePath, pageBreak);
            surl.Append("={0}");

            #region 将url中原有的参数拼接到page后面
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != pageBreak.ToLower())
                {
                    surl.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
                }
            }
            #endregion

            // surl = 192.168.1.221/home/?page={0}&sid=1&age=23 

            #region 是否显示首页 末页
            //是否显示首页 末页
            if (showFirstLastBtn)
            {
                //<span class="x-pitem"><a href="/?page=1">首页</a></span>
                //更新v2.0 <span class="x-pitem x-fied"><a href="/?page=1">首页</a></span>
                string u_first = "<span class=\"x-pitem x-fied\"><a href=\"{0}\">首页</a></span>";
                if (currentPageIndex <= 1)
                {
                    sbuilder.AppendFormat(u_first, "javascript:void(0);");
                }
                else
                {
                    string u_temp = string.Format(surl.ToString(), 1);
                    sbuilder.AppendFormat(u_first, u_temp);
                }
            }
            #endregion

            #region 是否显示上一页 下一页
            //是否显示上一页 下一页
            if (showPrevNextBtn)
            {
                // <span class="x-pitem"><a href="/?page=4"><i class="xprev"></i>上一页</a></span>
                string u_prev = "<span class=\"x-pitem\"><a href=\"{0}\"><i class=\"xprev\"></i>上一页</a></span>";
                if (currentPageIndex > 1)
                {
                    string u_temp = string.Format(surl.ToString(), currentPageIndex - 1);
                    sbuilder.AppendFormat(u_prev, u_temp);
                }
                else
                {
                    sbuilder.AppendFormat(u_prev, "javascript:void(0);");
                }
            }
            #endregion

            #region 生成显示页码
            //页码
            sbuilder.Append(UrlPageHelper.CreateUrlPageNumbers(currentPageIndex, viewPage, pageCount, surl));
            #endregion

            #region 是否显示上一页 下一页
            //是否显示上一页 下一页
            if (showPrevNextBtn)
            {
                //<span class="x-pitem"><a href="/?page=6">下一页<i class="xnext"></i></a></span>
                string u_next = "<span class=\"x-pitem\"><a href=\"{0}\">下一页<i class=\"xnext\"></i></a></span>";
                if (currentPageIndex < pageCount)
                {
                    string u_temp = string.Format(surl.ToString(), currentPageIndex + 1);
                    sbuilder.AppendFormat(u_next, u_temp);
                }
                else
                {
                    sbuilder.AppendFormat(u_next, "javascript:void(0);");
                }
            }
            #endregion

            #region 是否显示首页 末页
            //是否显示首页 末页
            if (showFirstLastBtn)
            {
                string u_last = "<span class=\"x-pitem x-fied\"><a href=\"{0}\">末页</a></span>";
                if (currentPageIndex == pageCount)
                {
                    sbuilder.AppendFormat(u_last, "javascript:void(0);");
                }
                else
                {
                    string u_temp = string.Format(surl.ToString(), pageCount);
                    sbuilder.AppendFormat(u_last, u_temp);
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
            //return MvcHtmlString.Create(sbuilder.ToString());
            return sbuilder.ToString();
        }
        #endregion
    }
}
