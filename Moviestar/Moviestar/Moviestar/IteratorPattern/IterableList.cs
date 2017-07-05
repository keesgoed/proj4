using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moviestar.IteratorPattern
{
    public class IterableList<T> : Iterator<T>
    {
        private List<T> list;
        private int current = 0;
        public IterableList(List<T> list)
        {
            this.list = list;
        }

        public T CurrentItem()
        {
            return list[current];
        }

        //get first item
        public T First()
        {
            return list[0];
        }

        // get next iteration item
        public T Next()
        {
            T ret = default(T);
            if (current < list.Count - 1)
            {
                ret = list[++current];
            }
            return ret;
        }
    }
}
