using Leafney.Pager;

namespace System.Web.Mvc
{
    /// <summary>
    /// MVC中分页参数page作为QueryString参数值实现分页
    /// </summary>
    public static class XUrlPager
    {

        #region 分页数据显示
        /// <summary>
        /// 分页数据显示（page作为QueryString参数值）
        /// QueryString形式page=1
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <returns></returns>
        public static MvcHtmlString PagerU(this HtmlHelper helper, IPageList pageList)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerU(helper, pageList, null, false, true, true, false);
        }

        /// <summary>
        /// 分页数据显示（page作为QueryString参数值）
        /// QueryString形式page=1
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <returns></returns>
        public static MvcHtmlString PagerU(this HtmlHelper helper, IPageList pageList, bool showOnlyOneDisplayed = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerU(helper, pageList, null, showOnlyOneDisplayed, true, true, false);
        }



        /// <summary>
        /// 分页数据显示（page作为QueryString参数值）
        /// QueryString形式page=1
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <returns></returns>
        public static MvcHtmlString PagerU(this HtmlHelper helper, IPageList pageList, string pageBreak)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerU(helper, pageList, pageBreak, false, true, true, false);
        }

        /// <summary>
        /// 一般分页数据显示（page作为QueryString参数值）
        /// QueryString形式page=1
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <returns></returns>
        public static MvcHtmlString PagerU(this HtmlHelper helper, IPageList pageList, string pageBreak, bool showOnlyOneDisplayed = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerU(helper, pageList, pageBreak, showOnlyOneDisplayed, true, true, false);
        }

        /// <summary>
        /// 一般分页数据显示（page作为QueryString参数值）
        /// QueryString形式page=1
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <returns></returns>
        public static MvcHtmlString PagerU(this HtmlHelper helper, IPageList pageList, string pageBreak, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return PagerU(helper, pageList, pageBreak, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, false);
        }


        /// <summary>
        /// 分页数据显示（page作为QueryString参数值）
        /// QueryString形式page=1
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        public static MvcHtmlString PagerU(this HtmlHelper helper, IPageList pageList, string pageBreak, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return new UrlPageBuilder().GetUrlPager(pageBreak, pageList.CurrentPageindex, pageList.PageSize, pageList.TotalCount, pageList.ViewPages, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, showPageCount);
        }
        #endregion

    }
}
