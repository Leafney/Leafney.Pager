using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerTestData
{
    public  class MakeData
    {

        /// <summary>
        /// 模拟从数据库中分页查询数据
        /// </summary>
        /// <param name="setCount">手动设置总数据条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="totalCount">总数据条数</param>
        /// <returns></returns>
        public List<Article> GetPageList(int setCount,int pageIndex,int pageSize,out int totalCount)
        {
            List<Article> articleList = new List<Article>();
           
            //总数据条数
            totalCount = setCount;

            for (int i = 1; i <= setCount; i++)
           {
               Article article = new Article();
               article.ID = i;
               article.Title = "文章 "+i;
               articleList.Add(article);
           }
            //分页获取数据
          return articleList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
