using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDS.Service;
namespace MDS.Blog
{
    public class ArticleService : Service.Service
    {
        public ArticleService()
            :base(typeof(Article))
        {
            
        }

    }
}
