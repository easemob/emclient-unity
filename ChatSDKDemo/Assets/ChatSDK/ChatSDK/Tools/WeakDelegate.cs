using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatSDK
{

    public class WeakDelegater<T>
    {
        private List<WeakReference> InnerList = new List<WeakReference>();

        public IEnumerable<T> List
        {
            get
            {
                return (InnerList.Where(x => x.Target is T).Select(x => (T)x.Target));
            }
        }

        public void Add(T obj)
        {
            WeakReference wref = new WeakReference(obj);
            InnerList.Add(wref);
        }

        public void Clear()
        {
            InnerList.Clear();
        }
    }
}