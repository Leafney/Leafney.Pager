using System;

namespace Leafney.Pager
{
    /// <summary>
    /// URL参数分页，可用于WebForm和MVC中
    /// （page作为QueryString参数值）
    /// </summary>
    public static class XStrPager
    {

        /// <summary>
        /// URL参数分页，page作为QueryString参数值
        /// </summary>
        /// <param name="pageList">分页数据集合</param>
        /// <returns></returns>
        public static string Pager(IPageList pageList)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return XStrPager.Pager(pageList, null, false, true, true, false);
        }

        /// <summary>
        /// URL参数分页，page作为QueryString参数值
        /// </summary>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <returns></returns>
        public static string Pager(IPageList pageList, bool showOnlyOneDisplayed = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return XStrPager.Pager(pageList, null, showOnlyOneDisplayed, true, true, false);
        }


        /// <summary>
        /// URL参数分页，page作为QueryString参数值
        /// </summary>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <returns></returns>
        public static string Pager(IPageList pageList, string pageBreak)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return XStrPager.Pager(pageList, pageBreak,false, true, true, false);
        }


        /// <summary>
        /// URL参数分页，page作为QueryString参数值
        /// </summary>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <returns></returns>
        public static string Pager(IPageList pageList, string pageBreak, bool showOnlyOneDisplayed = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return XStrPager.Pager(pageList, pageBreak, showOnlyOneDisplayed, true, true, false);
        }


        /// <summary>
        /// URL参数分页，page作为QueryString参数值
        /// </summary>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <returns></returns>
        public static string Pager(IPageList pageList, string pageBreak, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return XStrPager.Pager(pageList, pageBreak, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, false);
        }



        /// <summary>
        /// URL参数分页，page作为QueryString参数值
        /// </summary>
        /// <param name="pageList">分页数据集合</param>
        /// <param name="pageBreak">分页符（默认为page）</param>
        /// <param name="showOnlyOneDisplayed">当分页总数据条数只够显示一页时，是否显示分页插件 默认不显示</param>
        /// <param name="showPrevNextBtn">是否显示上一页下一页按钮</param>
        /// <param name="showFirstLastBtn">是否显示首页末页按钮</param>
        /// <param name="showPageCount">是否显示总页数</param>
        /// <returns></returns>
        public static string Pager(IPageList pageList, string pageBreak, bool showOnlyOneDisplayed = false, bool showPrevNextBtn = true, bool showFirstLastBtn = true, bool showPageCount = false)
        {
            if (pageList == null)
            {
                throw new Exception("pageList不能为null");
            }
            return new UrlPageBuilder().GetPager(pageBreak, pageList.CurrentPageindex, pageList.PageSize, pageList.TotalCount, pageList.ViewPages, showOnlyOneDisplayed, showPrevNextBtn, showFirstLastBtn, showPageCount);
        }

    }
}
