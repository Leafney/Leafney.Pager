namespace Leafney.Pager
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public interface IPageList
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        int CurrentPageindex { get; set; }
        /// <summary>
        /// 每页数据条数
        /// </summary>
        int PageSize { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount { get; set; }
        /// <summary>
        /// 可见页码总数
        /// </summary>
        int ViewPages { get; set; }

    }
}
