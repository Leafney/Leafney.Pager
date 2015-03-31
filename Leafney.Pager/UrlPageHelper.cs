using System.Text;

namespace Leafney.Pager
{
    /// <summary>
    /// url参数分页帮助类
    /// </summary>
   internal class UrlPageHelper
    {

        #region 生成数字页码
        /// <summary>
        /// 生成数字页码
        /// </summary>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="viewPage">可见页码总数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="url">根url</param>
        /// <returns></returns>
        public static string CreateUrlPageNumbers(int currentPageIndex, int viewPage, int pageCount, StringBuilder url)
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
            string u_item = "<span class=\"x-pitem x-num\"><a href=\"{0}\">{1}</a></span>";

            for (int i = firstIndex; i < lastIndex + 1; i++)
            {
                if (i == currentPageIndex)
                {
                    //当前页码v1.0  <span class="xcurr">5</span>
                    //更新v2.0：<span class="xcurr">10<i class="xmid"> / 19</i></span>
                    builder.AppendFormat(u_curr, i, pageCount);
                }
                else
                {
                    //<span class="x-pitem"><a href="/?page=2">2</a></span>
                    //更新v2.0 <span class="x-pitem x-num"><a href="/?page=4">4</a></span>
                    string u_temp = string.Format(url.ToString(), i);
                    builder.AppendFormat(u_item, u_temp, i);
                }
            }
            return builder.ToString();
        }
        #endregion

    }
}
