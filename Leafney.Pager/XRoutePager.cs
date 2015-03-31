using Leafney.Pager;

namespace System.Web.Mvc
{
    /// <summary>
    /// 高级路由分页
    /// 通过RouteLink()匹配相应路由规则生成a链接
    /// 根据路由规则自动匹配相应参数
    /// </summary>
    public static class XRoutePager
    {

        /// <summary>
        /// 高级路由分页，匹配相应路由规则生成分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="routeName">自定义分页路由规则名称</param>
        /// <returns></returns>
        public static MvcHtmlString PagerR(this HtmlHelper helper, IPageList pageList, string routeName)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerR(helper, pageList, routeName,null,false,true, true, false);
        }

        /// <summary>
        /// 高级路由分页，匹配相应路由规则生成分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="routeName">自定义分页路由规则名称</param>
        /// <param name="pageBreak">分页符（默认为page，必须和自定义分页路由规则中的值相同）</param>
        /// <returns></returns>
        public static MvcHtmlString PagerR(this HtmlHelper helper, IPageList pageList, string routeName, string pageBreak)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerR(helper, pageList, routeName,pageBreak, false, true, true, false);
        }


        /// <summary>
        /// 高级路由分页，匹配相应路由规则生成分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="routeName">自定义分页路由规则名称</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <returns></returns>
        public static MvcHtmlString PagerR(this HtmlHelper helper, IPageList pageList, string routeName, bool showOnlyOneDisplayed = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerR(helper, pageList, routeName,showOnlyOneDisplayed, true, true, false);
        }

        /// <summary>
        /// 高级路由分页，匹配相应路由规则生成分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="routeName">自定义分页路由规则名称</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <returns></returns>
        public static MvcHtmlString PagerR(this HtmlHelper helper, IPageList pageList, string routeName, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerR(helper, pageList, routeName,null,showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, false);
        }


        /// <summary>
        /// 高级路由分页，匹配相应路由规则生成分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="routeName">自定义分页路由规则名称</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        public static MvcHtmlString PagerR(this HtmlHelper helper, IPageList pageList, string routeName, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerR(helper, pageList, routeName, null, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, showPageCount);
        }

        /// <summary>
        /// 高级路由分页，匹配相应路由规则生成分页链接
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="routeName">自定义分页路由规则名称</param>
        /// <param name="pageBreak">分页符（默认为page，必须和自定义分页路由规则中的值相同）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        public static MvcHtmlString PagerR(this HtmlHelper helper, IPageList pageList, string routeName, string pageBreak, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            //返回生成的分页字符串
            return new RoutePageBuilder().GetRoutePager(helper, routeName, pageBreak, pageList.CurrentPageindex, pageList.PageSize, pageList.TotalCount, pageList.ViewPages, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, showPageCount);
        }
       

    }
}
