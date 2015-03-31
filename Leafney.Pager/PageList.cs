using System.Collections.Generic;

namespace Leafney.Pager
{
    /// <summary>
    /// 绑定分页数据，设置分页参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T> : List<T>, IPageList
    {
        /// <summary>
        /// 绑定分页数据，设置分页参数
        /// 默认每页10条数据，默认显示10个页码
        /// </summary>
        /// <param name="items">待分页显示的数据集合</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="totalCount">总数据条数</param>
        public PageList(IEnumerable<T> items, int pageIndex, int totalCount)
        {
            if (items != null)
            {
                AddRange(items);
            }
            CurrentPageindex = pageIndex;
            PageSize = 10;//默认每页10条数据
            TotalCount = totalCount;
            ViewPages = 10;//默认显示10个页码
        }

        /// <summary>
        /// 绑定分页数据，设置分页参数
        /// </summary>
        /// <param name="items">待分页显示的数据集合</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数(默认每页10条)</param>
        /// <param name="totalCount">总数据条数</param>
        public PageList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
        {
            if (items != null)
            {
                AddRange(items);
            }
            CurrentPageindex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            ViewPages = 10;//默认显示10个页码
        }
        /// <summary>
        /// 绑定分页数据，设置分页参数
        /// </summary>
        /// <param name="items">待分页显示的数据集合</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数(默认每页10条)</param>
        /// <param name="totalCount">总数据条数</param>
        /// <param name="viewPages">可见页码总数(默认10个)</param>
        public PageList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount, int viewPages)
        {
            if (items != null)
            {
                AddRange(items);
            }
            CurrentPageindex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            ViewPages = viewPages;
        }


        #region IPageList 成员

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPageindex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 可见页码数
        /// </summary>
        public int ViewPages { get; set; }

        #endregion

    }
}
