using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDS.Model
{
    public class PagedList<T>:List<T>
    {
        public int RecordCount { get; set; }


        public int PageIndex { get; set; }

        public int PageCount
        {
            get
            {
                return RecordCount/PageSize +((RecordCount % PageSize) == 0 ? 0 : 1);
            }
        }

        public int PageSize { get; set; }
    }
}
