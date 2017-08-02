using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSimple.Service
{
    public interface IService<T> where T: class
    {
        void Add(T entry);
        void Edit(T entry);
        void Remove(T entry);
        IEnumerable<T> Get();
        T Get(Func<T, bool> predicate);
    }
}
